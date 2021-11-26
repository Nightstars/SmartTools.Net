::创建发布文件夹
md update

::复制构建的文件到新文件夹中
echo copy new file
Xcopy .\build\Release .\update /E/H/C/I

::删除无用文件
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
rmdir /s/q .\update\SmartSoft.common_Secure
rmdir /s/q .\update\SmartTools.Net_Secure
rmdir /s/q .\update\Update_Secure
rmdir /s/q .\update\Oupput
rmdir /s/q .\update\temp
del .\update\Update.runtimeconfig.dev.json

::删除旧的zip
del *.zip
echo complete successfully

::压缩发布文件夹
7z a update.zip .\update\**

::删除发布文件夹
rmdir /s/q update

::获取本次发布的版本信息
.\SmartSoft.GetVersion\bin\Release\net5.0\SmartSoft.GetVersion.exe .\build\Release\SmartTools.Net.exe