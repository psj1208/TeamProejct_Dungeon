# C# 프로젝트 12조 - 뉴비조_Sparta_Dungeon

C# 콘솔을 이용한 턴제 Text RPG게임  
아이템과 경험치를 얻고 이 세계 최강이 되어랏!  

## 멤버소개 

![image](/README/introduce.png)  

## 와이어프레임
![image](/README/wireframe.png)  

## 플레이어 Player.cs , SkillManager.cs
강순종 담당
### [Player](https://github.com/psj1208/TeamProejct_Dungeon/blob/main/TeamProejct_Dungeon/Player.cs)
- Player Status 구현
- Player 능력치 및 공방 구현
### [Skill](https://github.com/psj1208/TeamProejct_Dungeon/blob/main/TeamProejct_Dungeon/SkillManager.cs)
- 직업별 Skill 구현
  - Warrior : 몬스터 선택 시 2배 또는 1~3배 데미지
  - Assassin : 몬스터 2 ~ 4 마리 선택 시 각각 데미지
## 인벤토리 Inventory.cs
- 박성준 담당. 
- 아이템 클래스를 담은 리스트와 이를 관리하는 메서드를 상황에 맞게 구현하여 협업에 무리가 되지 않도록 구현했습니다.
## 상점 Shop.cs, IItem.cs
- 송민경 담당.
- 각 클래스들은 부모 클래스로 IItem을 상속받아 가상 메서드를 재정의합니다.
- 관리가 쉽도록 DataBase클래스를 선언하여 상속받은 세가지 아이템들을 초기화하였습니다.
- IItem 클래스에 접근하는 리스트를 만들어 상점들의 상품 목록을 보여줍니다.
- 구매하면 DeepCopy()메서드로 객체 전달후 Inven에 아이템이 추가되게 구현하였습니다.
- 휴식 기능 구현.
## 퀘스트 QuestManager_PSJ.cs
- 박성준 담당
- 인벤토리와 유사한 구조를 갖고 있습니다.
- 퀘스트의 달성 조건에 관여하는 다른 메서드들이 퀘스트 매니저 스크립트에 신호를 전달하여(몬스터의 죽음,스테이지의 클리어)
- 매니저가 이를 인식하고 조건에 맞는 퀘스트가 있으면 퀘스트를 갱신합니다.
## 몬스터, 스테이지 Monster.cs, Stage.cs
- 이유림 담당
- 리스트를 통해 몬스터의 정보를 관리하고 특정 몬스터, 특정 등급의 몬스터를 반환할 수 있도록 구현했습니다.
- 몬스터의 공격과 피해 처리, 사망 여부를 결정하고 몬스터 사망 시 플레어에게 보상을 지급합니다.
- 몬스터의 레벨별로 스테이지 리스트를 만들어 스테이지의 난이도를 설정했습니다.
- 스테이지 클리어 시 랜덤한 확률로 보상(골드, 아이템)이 지급될 수 있도록 구현했습니다.
## [던전 시스템](https://github.com/psj1208/TeamProejct_Dungeon/blob/main/TeamProejct_Dungeon/Program.cs) Program.cs
- 김태겸 담당
- 몬스터 정보 출력 (showMonsterInfo)
  - 몬스터의 레벨, 이름, HP 상태를 출력합니다.
  - 사망한 몬스터는 Dead 표시와 함께 회색으로 출력됩니다.
  - Shownumber가 true이면 몬스터 앞에 번호를 붙여 출력합니다.
- 전투 화면 출력 (ShowBattleScreen)
  - 전투 화면을 구성하는 시각적 구분선을 출력합니다.
  - ShowMonsterInfo()를 호출하여 몬스터 정보를 표시합니다.
  - Battle_Profile()을 호출하여 플레이어 상태를 출력합니다.
- 플레이어 상태 출력 (Battle_Profile)
  - 플레이어의 레벨, 이름, HP, MP 상태를 출력합니다.
  - 전투 중 언제든 0번을 입력하면 전투를 종료할 수 있도록 안내합니다.
- 몬스터 처치 시 처리 (Battle_Dead)
  - 몬스터가 죽었을 경우에만 실행됩니다.
  - GrantReward()를 호출하여 경험치 및 골드를 지급합니다.
  - 플레이어가 처치 메시지를 확인할 수 있도록 키 입력을 대기합니다.
- 전투 흐름 관리 (Battle)
  - 플레이어의 입력을 받아 행동을 수행합니다. (공격, 스킬, 도망가기)
  - 플레이어의 턴과 적의 턴을 번갈아 진행합니다.
  - 전투 종료 조건을 체크하여, 승리 또는 패배 시 결과를 출력합니다.
- 플레이어의 행동 처리 (ExecutePlayerTurn)
  - 공격 시 대상 몬스터를 선택하고, 이미 죽은 몬스터는 선택할 수 없도록 예외 처리합니다.
  - 스킬을 사용할 경우, 여러 마리의 몬스터가 쓰러질 수도 있으므로 죽은 몬스터 리스트를 체크합니다.
  - 플레이어가 행동을 완료하면 턴을 종료하고, 그렇지 않으면 다시 입력을 받습니다.
- 적의 턴 진행 (EnemyPhase)
  - 랜덤하게 몬스터를 선택하여 플레이어를 공격합니다.
  - 살아있는 몬스터만 공격을 수행하도록 필터링합니다.
- 전투 결과 출력 (Battle_Result)
  - 플레이어가 승리하면 보상을 지급하고, 경험치 및 골드를 출력합니다.
  - 패배하면 일부 체력을 회복하여 다음 전투를 대비할 수 있도록 합니다.
## 저장 QuestManager_PSJ.cs
- JsonConverter 즉, 외부 패키지를 이용하여 Json파일에 문자열 형태로 저장합니다.
- 써본 바로는 딕셔너리 형태와 비슷하게 저장하고 읽을 때도 딕셔너리를 읽듯이 읽어오는 것 같습니다.
## 텍스트 연출 및 입력 관련 메서드 그룹(선택지) Text.cs
- 커서의 포지션을 가져오는 메서드를 이용하여 방향키를 누를 시 유니티의 프레임마다 움직이는 듯한 연출을 줍니다.
- 그 외에도 플레이어가 적합하지 않은 선택을 고르는 방향을 막도록 하였습니다.
