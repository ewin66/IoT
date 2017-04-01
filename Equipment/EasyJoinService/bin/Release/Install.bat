%SystemRoot%\Microsoft.NET\Framework\v4.0.30319\installutil.exe EasyJoinService.exe
Net Start EasyJoinService
sc config EasyJoinService start= auto