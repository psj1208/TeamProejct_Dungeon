## C# 프로젝트 12조 - 뉴비조_TextRPG

### 멤버소개 

![image](/README/introduce)  

# 와이어프레임
![image](/README/wireframe)  

# 구현된 기능
## 상태창
- text
## 인벤토리 Inventory.cs
- 박성준 담당. 
- 아이템 클래스를 담은 리스트와 이를 관리하는 메서드를 상황에 맞게 구현하여 협업에 무리가 되지 않도록 구현했습니다.
## 상점
- text
## 퀘스트 QuestManager_PSJ.cs
- 박성준 담당
- 인벤토리와 유사한 구조를 갖고 있습니다.
- 퀘스트의 달성 조건에 관여하는 다른 메서드들이 퀘스트 매니저 스크립트에 신호를 전달하여(몬스터의 죽음,스테이지의 클리어)
- 매니저가 이를 인식하고 조건에 맞는 퀘스트가 있으면 퀘스트를 갱신합니다.
## 던전
- text
- 던전 시스템(https://github.com/psj1208/TeamProejct_Dungeon/blob/main/TeamProejct_Dungeon/Program.cs)
## 저장 QuestManager_PSJ.cs
- JsonConverter 즉, 외부 패키지를 이용하여 Json파일에 문자열 형태로 저장합니다.
- 써본 바로는 딕셔너리 형태와 비슷하게 저장하고 읽을 때도 딕셔너리를 읽듯이 읽어오는 것 같습니다.
## 텍스트 연출 및 입력 관련 메서드 그룹(선택지) Text.cs
- 커서의 포지션을 가져오는 메서드를 이용하여 방향키를 누를 시 유니티의 프레임마다 움직이는 듯한 연출을 줍니다.
- 그 외에도 플레이어가 적합하지 않은 선택을 고르는 방향을 막도록 하였습니다.
