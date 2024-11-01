# TCP/IP with C#

TCP 기반의 서버 프로그램은 다음과 같은 순서로 구현됨

```
Socket(): 소켓 생성

Bind(): 소켓 주소 할당

Listen(): 연결 요청 대기상태

Accept(): 연결 허용

Read()/Write(): 데이터 송·수신

Close(): 연결 종료
```
