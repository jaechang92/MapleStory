git init
git remote add origin + repository address
git push origin master
git pull origin master
git status
git add .
git commit -m ""
git config core.eol native
-windows에서는 CRLF 를 사용하고 Linux, OS X 는 LF 만 사용한다.

[windows]
git config core.autocrlf true
-윈도에서는 CRLF 를 사용하므로 저장소에서 가져올 때 LF 를 CRLF 로 변경하고 저장소로 보낼 때는 CRLF 를 LF 로 변경하도록 true 로 설정한다.

[Linux,Max OS]
git config core.autocrlf input
-리눅스, 맥, 유닉스는 LF 만 사용하므로 input 으로 설정한다.

[empty commit]
git commit --allow-empty -m "내용"

※ 락 에러 발생시 - git 폴더안의 Lock파일을 삭제