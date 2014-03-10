#! /bin/sh

set -e

# 1/ libvlc, libvlccore and its plugins
TESTED_HASH=45df8a6415
if [ ! -d "vlc" ]; then
    echo "VLC source not found, cloning"
    git clone git://git.videolan.org/vlc.git vlc
    cd vlc
    #git am -3 ../patches/*.patch
    #if [ $? -ne 0 ]; then
    #    git am --abort
    #    echo "Applying the patches failed, aborting git-am"
    #    exit 1
    #fi
else
    echo "VLC source found"
    cd vlc
    if ! git cat-file -e ${TESTED_HASH}; then
        cat << EOF
***
*** Error: Your vlc checkout does not contain the latest tested commit ***
***

EOF
        exit 1
    fi
fi

MAKEFLAGS=
if which nproc >/dev/null
then
MAKEFLAGS=-j`nproc`
fi

TARGET_TUPLE=i686-w64-mingw32
[ $# = 1 ] && TARGET_TUPLE=$1

${TARGET_TUPLE}-gcc -dumpspecs | sed -e 's/-lmingwex/-lwinstorecompat -lmingwex -lwinstorecompat -lole32 -lruntimeobject/' -e 's/-lmsvcrt/-lmsvcr110/' > ../newspecfile
NEWSPECFILE="`pwd`/../newspecfile"

WINRTSOCK=`cd ../../winrtsock/winrtsock-86646-build;pwd`
EXTRA_CPPFLAGS="-D_WIN32_WINNT=0x602 -DWINVER=0x602 -DWINSTORECOMPAT -D_UNICODE -DUNICODE"
EXTRA_LDFLAGS="-lnormaliz -lwinstorecompat -lruntimeobject -L$WINRTSOCK"

echo "Building the contribs"
mkdir -p contrib/winrt
cd contrib/winrt
../bootstrap --host=${TARGET_TUPLE} --disable-disc --disable-sout --disable-net \
    --disable-sdl \
    --disable-schroedinger \
    --disable-vncserver \
    --disable-chromaprint \
    --disable-modplug \
    --disable-SDL_image \
    --disable-fontconfig \
    --disable-zvbi \
    --disable-kate \
    --disable-caca \
    --disable-gettext \
    --disable-mpcdec \
    --disable-upnp \
    --disable-gme \
    --disable-taglib \
    --disable-tremor \
    --disable-vorbis \
    --disable-sidplay2 \
    --disable-samplerate \
    --disable-faad2 \
    --disable-harfbuzz \
    --enable-iconv \
    --disable-goom \
    --disable-flac \
    --enable-dca \
    --disable-fontconfig \
    --disable-gcrypt \
    --disable-gpg-error \
    --disable-gnutls \
    --disable-projectM \
    --disable-ass \
    --disable-qt4 \
    --disable-gpl

echo "EXTRA_CFLAGS=-DNDEBUG -DWINAPI_FAMILY=WINAPI_FAMILY_APP ${EXTRA_CPPFLAGS}" >> config.mak
echo "EXTRA_LDFLAGS=${EXTRA_LDFLAGS}" >> config.mak


make fetch
make $MAKEFLAGS

cd ../.. && mkdir -p winrt && cd winrt

echo "Bootstraping"
../bootstrap

echo "Configuring"
CPPFLAGS="${EXTRA_CPPFLAGS}" \
LDFLAGS="${EXTRA_LDFLAGS}" \
CC="${TARGET_TUPLE}-gcc -specs=$NEWSPECFILE -Wl,--disable-runtime-pseudo-reloc" \
CXX="${TARGET_TUPLE}-g++ -specs=$NEWSPECFILE -Wl,--disable-runtime-pseudo-reloc" \
ac_cv_search_connect="-lwinrtsock -lws2_32" \
../../configure.sh --host=${TARGET_TUPLE}

echo "Building"
make $MAKEFLAGS

echo "Package"
make install

rm -rf tmp && mkdir tmp

# Compiler shared DLLs, when using compilers built with --enable-shared
# The shared DLLs may not necessarily be in the first LIBRARY_PATH, we
# should check them all.
library_path_list=`${TARGET_TUPLE}-g++ -v /dev/null 2>&1 | grep ^LIBRARY_PATH|cut -d= -f2` ;
OLD_IFS="$IFS"
IFS=':';
for x in $library_path_list
do for f in stdc++-6 gcc_s_sjlj-1
    do
        [ -f "$x/lib$f.dll" ] && cp "$x/lib$f.dll" "tmp/"
    done
done
IFS="$OLD_IFS"

find _win32/bin -name "*.dll" -exec cp -v {} tmp \;
cp -r _win32/include tmp/
cp -r _win32/lib/vlc/plugins tmp/

find tmp -name "*.la" -exec rm -v {} \;
find tmp -name "*.a" -exec rm -v {} \;
blacklist="
audioscrobbler
audiobargraph_a
wingdi
waveout
dshow
directdraw
windrive
globalhotkeys
direct2d
ntservice
dxva2
dtv
vcd
cdda
quicktime
atmo
logger
oldrc
dmo
sap
panoramix
netsync
"
regexp=
for i in ${blacklist}
do
    if [ -z "${regexp}" ]
    then
        regexp="${i}"
    else
        regexp="${regexp}|${i}"
    fi
done
rm `find tmp/plugins -name 'lib*plugin.dll' | grep -E "lib(${regexp})_plugin.dll"`

find tmp \( -name "*.dll" -o -name "*.exe" \) -exec ../../appcontainer.pl {} \;

cp lib/.libs/libvlc.dll.a tmp/vlc.lib
cp src/.libs/libvlccore.dll.a tmp/vlccore.lib

cd tmp
7z a ../vlc.7z *
cd ..

rm -rf tmp
