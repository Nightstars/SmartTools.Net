md update
echo copy new file
Xcopy .\build\Release .\update /E/H/C/I
echo delete ignore file
del .\update\SmartTools.Net.runtimeconfig.dev.json
rmdir /s/q .\update\ca-ES
rmdir /s/q .\update\en
rmdir /s/q .\update\fa
rmdir /s/q .\update\fr
rmdir /s/q .\update\ko-KR
rmdir /s/q .\update\pl
rmdir /s/q .\update\pt-BR
rmdir /s/q .\update\ru
rmdir /s/q .\update\tr
del .\update\Update.*
del *.zip
echo complete successfully
7z a update.zip .\update\**
rmdir /s/q update