﻿route delete 0.0.0.0 mask 0.0.0.0 192.168.5.1
route delete 203.154.171.169 mask 255.255.255.255 192.168.5.1 -p
route delete 203.154.171.170 mask 255.255.255.255 192.168.5.1 -p
route delete 103.219.196.169 mask 255.255.255.255 192.168.5.1 -p
route delete 103.219.196.170 mask 255.255.255.255 192.168.5.1 -p

route add 203.154.171.169 mask 255.255.255.255 192.168.5.1 -p
route add 203.154.171.170 mask 255.255.255.255 192.168.5.1 -p
route add 103.219.196.169 mask 255.255.255.255 192.168.5.1 -p
route add 103.219.196.170 mask 255.255.255.255 192.168.5.1 -p
REM : -p คือ เมื่อ Restart ก็ยังคงอยู่ ปกติถ้าไม่ใช้ -p เมื่อ Restart จะหายไปต้องทำการ Add ใหม่เสมอ 