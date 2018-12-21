# MagicFight

## 게임 설멍

> 기획의 계기
>> Unity AssetStore을 돌아보던중 Realistic Effects Pack v4 를 보게되어 
>> 이것을 이용하여 멀티플레이 게임을 개발해 보고자 구매하여 제작에 들어감.

> 참고 자료
>> 유니티 개발자를 위한 C#으로 온라인 게임 서버 만들기  
>> Unity Documents

---------

## 게임 내부 스크립트 역할

> ### Movement
> > Movement.cs : 스크립트을의 기반을 다지기 위한 부모 객체 
> > > NowSpeed : 지금의 속력을 구해 dir 변수에 저장하는 함수.   
> > > Move : NowSpeed에서 구한 속력을 이용하여 움직여주는 함수,움직이는것은 Rigidbody를 이용하여 AddForce로 최대속력을 재한 하고 움직임, 달리는 것을 판단하는것도 이 스크립트 안에서 구분.  
> > > Jump : 점프키를 눌렀을시 점프하는 스크립트를 작성.  
> > > Animation : 애니메이션 Parameter을 적용시키기 위한 함수 각자 다른 Parameter을 가질수 있기에 내부 스크립트는 작성하지 않음.  
> > > CollisionOnGround : tag중 Ground에 부딛혔을 경우 점프가 가능하게 변수를 true로 지정  
> > > Send : 서버로 자신의 좌표, 화전값, 애니메이션 Parameter을 전달  

> > FireMovement.cs : 캐릭터 Fire의 움직임을 담당하는 스크립트 (Movement 를 상속받고 있음)
> > > Start : 처음에 점프가 가능하게금 초기화를 해주고, 만약 지금 자기자신이 내가 조작할수 있는 캐릭터일경우 서버에 자기 자신의 정보를 전해주는 함수인 Send를 0.05초 만큼 보냄.
> > > Update : 애니메이션을 실행하기 위한 함수인 Animation을 실행시켜 주고, 내 자신의 캐릭터이고 죽지 않았으면 Jump와 NowSpeed를 호출함
> > > FixedUpdate : 내 자신의 캐릭터이면 움직이는 Move함수를 부름.


----------