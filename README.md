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

> > FireMovement : 캐릭터 Fire의 움직임을 담당하는 스크립트 (Movement 를 상속받고 있음)
> > > Start : 처음에 점프가 가능하게금 초기화를 해주고, 만약 지금 자기자신이 내가 조작할수 있는 캐릭터일경우 서버에 자기 자신의 정보를 전해주는 함수인 Send를 0.05초 만큼 보냄.  
> > > Update : 애니메이션을 실행하기 위한 함수인 Animation을 실행시켜 주고, 내 자신의 캐릭터이고 죽지 않았으면 Jump와 NowSpeed를 호출함  
> > > FixedUpdate : 내 자신의 캐릭터이면 움직이는 Move함수를 부름.  
> > > Animation(override) : Animator의 Parameter 6가지를 설정해줌.  

> > IceMovement : 캐릭터 Ice의 움직임을 담당하는 스크립트 (Movement 를 상속받고 있음)  
> > > Start : 처음에 점프가 가능하게금 초기화를 해주고, 만약 지금 자기자신이 내가 조작할수 있는 캐릭터일경우 서버에 자기 자신의 정보를 전해주는 함수인 Send를 0.05초 만큼 보냄.  
> > > Update : 애니메이션을 실행하기 위한 함수인 Animation을 실행시켜 주고, 내 자신의 캐릭터이고 죽지 않았으면 Jump와 NowSpeed를 호출함  
> > > FixedUpdate : 내 자신의 캐릭터이면 움직이는 Move함수를 부름.  
> > > Animation(override) : Animator의 Parameter 6가지를 설정해줌.  

> > Cude : 캐릭터 Ice와 Fire가 만들어지기 전에 테스트로 사용되었던 것의 움직임을 담당하는 스크립트(Movement를 상속받고 있음)
> > > Start : 처음에 점프가 가능하게금 초기화를 해주고, 만약 지금 자기자신이 내가 조작할수 있는 캐릭터일경우 서버에 자기 자신의 정보를 전해주는 함수인 Send를 0.05초 만큼 보내고, 자신의 색을 파란색으로 바꿈.   
> > > Update : 애니메이션을 실행하기 위한 함수인 Animation을 실행시켜 주고, 내 자신의 캐릭터이고 죽지 않았으면 Jump와 NowSpeed를 호출함  
> > > FixedUpdate : 내 자신의 캐릭터이면 움직이는 Move함수를 부름. 

> RotateObject
> > RotateObject : 3인칭 게임의 마우스 컨트롤을 담당하는 클래스
> > > Start : 마우스 커서가 보이지 않게 설정함.  
> > > FixedUpdate : 플레이어가 죽지 않고 내가 조종하는 캐릭터일 경우 Rotation이란 오브젝트로 
오프셋을 지정해준뒤 Spring Camera로 움직이게 해줌.  

> SceneTransport 
> > SceneTransporter : 씬전환을 자연스러운 씬 전환을 위한 클래스
> > > Awake : 씬을 전환할때 사라지지 않게 하나 내부변수 instance가 이 클래스가 아닌경우 삭제.  
> > > LoadScene : 우선 "LoadingScene"을 로드한뒤 백그라운드로 "Map"을 로드하고 전부 로드되면 매개변수로 넘어온 씬으로 바꾼다.  

> SelectScene
> > MoveCamera : 직업을 고르는 UI, 카메라, 의 음직임과 직업을 고른뒤 Map으로 넘어감.
> > > Start : 초기화 작업.
> > > Update : 마우스 클릭으로 캐릭터의 외관을 볼수있게 하는곳.
> > > Next : 버튼의 클릭이벤트시 다음캐릭터로 넘어가게 하는 함수.
> > > Prev : 버튼의 클릭이벤트시 이전캐릭터로 넘어가게 하는 함수.
> > > Select : 지금 보고있는 직업을 선택한뒤 Map으로 씬을 움직이며, 캐릭터의 기본셋팅을 해주고, 로딩이 완료되었다 보내줌.

> Skill
> > SkillObject : 스킬의 정보를 가지고 있는 클래스, 각 직업마다 스킬의 함수가 다르므로 delegate를 사용하여 모든직업의 함수 스킬을 등록할 수 있게함.<br><br>
> > BaseSkillScripts : 각 직업의 스킬스크립트를 만들기위한 부모 클래스
> > > CoolDown : FixedUpdate에 넣을 함수이며 쿨타임을 감소하게 만들어주는 함수.  
> > > UseSkill : 각 스킬의 키를 눌렀을때 스킬이 발동되게금 하고, 스킬을 사용했다고 서버에 보내줌.  
> > > ChangeAvatar : 캐릭터의 애니메이션의 아바타가 달라 스킬이 발동될때 아바타를 바꿔주고 애니메이션이 끝나면 원래 아바타로 바꿔주는 함수.

> Player : 플레이어 오브젝트에 있는 스크립트에서 필요한 모든정보를 가지고있는 클래스.
----------