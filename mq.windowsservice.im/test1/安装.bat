@echo.��������......  
@echo off  
@sc create ReadImChatMsg binPath= "F:\MQWebSite\MQWebSite.git\trunk\mq.windowsservice.im\bin\Debug\mq.windowsservice.im.exe"  
@net start ReadImChatMsg
@sc config ReadImChatMsg start= AUTO  
@echo off  
@echo.������ϣ�  
@pause  