
# 🏴‍☠️ Treasure Hunt 3D

<p align="center">
  <strong>Xây dựng trò chơi 3D khám phá và truy tìm kho báu bằng Unity</strong>
</p>

<p align="center">
  <img src="https://img.shields.io/badge/Unity-6000.0-black?logo=unity" alt="Unity">
  <img src="https://img.shields.io/badge/C%23-Programming-blue?logo=csharp" alt="C#">
  <img src="https://img.shields.io/badge/Input-New%20Input%20System-orange" alt="Input System">
  <img src="https://img.shields.io/badge/Git-GitHub-black?logo=github" alt="Git">
  <img src="https://img.shields.io/badge/Status-In%20Development-yellow" alt="Status">
</p>

---

# 1. Giới thiệu

**Treasure Hunt 3D** là một trò chơi 3D được xây dựng bằng **Unity** cho môn học **Đồ họa và Hiện thực ảo**.

Người chơi sẽ điều khiển một nhân vật khám phá một khu vực đảo gồm nhiều môi trường khác nhau, tìm kiếm các chìa khóa, vượt qua các khu vực và cuối cùng tìm được kho báu.

Project tập trung vào việc áp dụng các kiến thức đồ họa và lập trình game 3D cơ bản trong Unity, bao gồm:

- Môi trường 3D.
- Transform trong không gian 3D.
- Camera.
- Ánh sáng và bóng đổ.
- Animation.
- Character Controller.
- Collision và Trigger.
- Particle Effect.
- User Interface.
- Scene Management.
- Tương tác giữa Player và các đối tượng trong môi trường.

---

# 2. Mục tiêu của trò chơi

Mục tiêu của người chơi rất đơn giản:

1. Khởi đầu tại khu vực **Beach**.
2. Khám phá môi trường.
3. Tìm **Key 1**.
4. Đi đến **Forest**.
5. Tìm **Key 2**.
6. Đi đến **Cave**.
7. Tìm **Key 3**.
8. Đến **Final Door**.
9. Sử dụng 3 chìa khóa để mở cửa.
10. Đi vào **Treasure Room**.
11. Tìm kho báu.
12. Hoàn thành trò chơi.

Luồng gameplay:

```text
START
  │
  ▼
┌─────────────┐
│    BEACH    │
│   Find Key1 │
└──────┬──────┘
       │
       ▼
┌─────────────┐
│   FOREST    │
│   Find Key2 │
└──────┬──────┘
       │
       ▼
┌─────────────┐
│    CAVE     │
│   Find Key3 │
└──────┬──────┘
       │
       ▼
┌─────────────┐
│ FINAL DOOR  │
│ Need 3 Keys │
└──────┬──────┘
       │
       ▼
┌─────────────┐
│  TREASURE   │
│    ROOM     │
└──────┬──────┘
       │
       ▼
    TREASURE
       │
       ▼
    YOU WIN
````

---

# 3. Thể loại

* 3D Exploration
* Adventure
* Treasure Hunt
* Puzzle nhẹ
* Casual

Game không tập trung vào chiến đấu.

Trọng tâm của project là:

> **Khám phá → Tìm chìa khóa → Mở khu vực → Tìm kho báu → Chiến thắng**

---

# 4. Công nghệ sử dụng

## Engine

**Unity 6**

## Ngôn ngữ

**C#**

## Input

**Unity Input System**

## Version Control

**Git + GitHub**

## Character

**KayKit Adventurers**

Nhân vật Player được lựa chọn:

> **Ranger**

## Animation

Sử dụng hệ thống Animation và Animator Controller của Unity kết hợp với animation từ KayKit.

---

# 5. Điều khiển

| Phím    | Chức năng               |
| ------- | ----------------------- |
| `W`     | Di chuyển về phía trước |
| `S`     | Di chuyển về phía sau   |
| `A`     | Di chuyển sang trái     |
| `D`     | Di chuyển sang phải     |
| `Shift` | Chạy                    |
| `Space` | Nhảy                    |
| `Mouse` | Điều khiển camera       |
| `E`     | Tương tác               |

> Các phím có thể được điều chỉnh trong quá trình phát triển nếu hệ thống Input thay đổi.

---

# 6. Các khu vực trong game

Game được chia thành 4 Scene chính:

```text
Beach
   ↓
Forest
   ↓
Cave
   ↓
TreasureRoom
```

---

## 6.1. Beach

### Mô tả

Đây là khu vực bắt đầu của trò chơi.

Người chơi xuất hiện tại:

```text
BeachSpawn
```

Tại Beach có:

* Mặt đất.
* Cây.
* Đá.
* Cỏ.
* Các vật thể trang trí.
* Key 1.
* Lối đi sang Forest.

### Scene

```text
Assets/Scenes/Beach.unity
```

### Người phụ trách

**Member 1**

---

## 6.2. Forest

### Mô tả

Forest là khu vực thứ hai mà người chơi khám phá.

Tại Forest có:

* Cây.
* Đá.
* Cỏ.
* Bụi cây.
* Đường đi.
* Key 2.
* Lối vào Cave.

### Scene

```text
Assets/Scenes/Forest.unity
```

### Người phụ trách

**Member 2**

---

## 6.3. Cave

### Mô tả

Cave là khu vực thứ ba.

Khu vực này có phong cách khác với Beach và Forest, tập trung vào không gian đá và ánh sáng tối.

Tại Cave có:

* Vách hang.
* Đá.
* Ánh sáng.
* Các vật thể trang trí.
* Key 3.
* Final Door.

### Scene

```text
Assets/Scenes/Cave.unity
```

### Người phụ trách

**Member 3**

---

## 6.4. Treasure Room

### Mô tả

Đây là khu vực cuối cùng của game.

Sau khi thu thập đủ 3 chìa khóa, người chơi vượt qua Final Door và đi vào Treasure Room.

Tại đây có:

* Treasure.
* Ánh sáng.
* Vật thể trang trí.
* Hiệu ứng chiến thắng.

### Scene

```text
Assets/Scenes/TreasureRoom.unity
```

### Người phụ trách

**Member 4**

---

# 7. Cấu trúc gameplay

## 7.1. Di chuyển Player

Player có thể:

* Đi bộ.
* Chạy.
* Nhảy.
* Chịu tác động của Gravity.
* Va chạm với môi trường.
* Xoay theo hướng di chuyển.

Player sử dụng:

```text
CharacterController
```

---

## 7.2. Animation

Animator của Player dự kiến bao gồm:

```text
Idle
Walking
Running
Jump
```

Các trạng thái được điều khiển thông qua Animator Parameters.

Ví dụ:

```text
Speed       Float
JumpShort   Trigger
JumpLong    Trigger
```

---

## 7.3. Thu thập chìa khóa

Khi Player thu thập một chìa khóa:

```text
Player
   ↓
Key
   ↓
Key được thu thập
   ↓
Key biến mất
   ↓
Key Count tăng
   ↓
UI cập nhật
```

Ví dụ:

```text
Keys: 1 / 3
```

---

## 7.4. Final Door

Final Door yêu cầu người chơi có đủ 3 chìa khóa.

Logic:

```text
Player tới Final Door
        │
        ▼
Kiểm tra Key Count
        │
        ├───────────────┐
        │               │
     < 3 Keys        = 3 Keys
        │               │
        ▼               ▼
   Không mở cửa      Mở cửa
                        │
                        ▼
                 Treasure Room
```

Nếu chưa đủ 3 chìa khóa:

```text
You need 3 keys!
```

---

## 7.5. Treasure

Treasure là mục tiêu cuối cùng của game.

Khi người chơi tương tác với Treasure:

```text
Treasure
   ↓
Win Condition
   ↓
YOU WIN
```

---

# 8. Cấu trúc thư mục

Cấu trúc project hiện tại được tổ chức theo nhóm chức năng:

```text
TreasureHunt3D/
│
├── Assets/
│   │
│   ├── Scenes/
│   │   ├── Beach.unity
│   │   ├── Forest.unity
│   │   ├── Cave.unity
│   │   └── TreasureRoom.unity
│   │
│   ├── Scripts/
│   │   │
│   │   ├── Player/
│   │   │   ├── PlayerController.cs
│   │   │   └── CameraController.cs
│   │   │
│   │   ├── Gameplay/
│   │   │   ├── Key.cs
│   │   │   ├── Door.cs
│   │   │   └── Treasure.cs
│   │   │
│   │   ├── SceneManagement/
│   │   │   ├── SceneLoader.cs
│   │   │   └── GameManager.cs
│   │   │
│   │   └── UI/
│   │       └── UIManager.cs
│   │
│   ├── Prefabs/
│   │   ├── Player/
│   │   ├── Key/
│   │   ├── Door/
│   │   └── Treasure/
│   │
│   ├── Environment/
│   │   ├── Beach/
│   │   ├── Forest/
│   │   ├── Cave/
│   │   └── TreasureRoom/
│   │
│   ├── Materials/
│   ├── UI/
│   ├── Audio/
│   │
│   └── KayKit/
│       └── Adventurers/
│
├── Packages/
├── ProjectSettings/
├── .gitignore
└── README.md
```

---

# 9. Phân công thành viên

## Member 1 – Beach

### Phụ trách

```text
Assets/Scenes/Beach.unity
```

### Công việc

* Tạo Beach Scene.
* Tạo Ground.
* Tạo BeachSpawn.
* Dựng môi trường bãi biển.
* Bố trí cây, đá, cỏ và các vật thể trang trí.
* Đặt Key 1.
* Tạo ForestExit.
* Kiểm tra Collider.
* Kiểm tra Player có thể di chuyển trong Scene.

### Không tự ý sửa

* GameManager.cs
* SceneLoader.cs
* PlayerController.cs
* Key.cs
* Door.cs
* UIManager.cs

---

## Member 2 – Forest

### Phụ trách

```text
Assets/Scenes/Forest.unity
```

### Công việc

* Tạo Forest Scene.
* Tạo Ground.
* Tạo ForestSpawn.
* Dựng môi trường rừng.
* Tạo đường đi.
* Đặt Key 2.
* Tạo CaveExit.
* Thiết lập Collider.
* Kiểm tra Player có thể di chuyển.

### Không tự ý sửa

* GameManager.cs
* SceneLoader.cs
* PlayerController.cs
* Key.cs
* Door.cs
* UIManager.cs

---

## Member 3 – Cave

### Phụ trách

```text
Assets/Scenes/Cave.unity
```

### Công việc

* Tạo Cave Scene.
* Tạo CaveSpawn.
* Dựng vách hang.
* Bố trí đá.
* Thiết lập ánh sáng.
* Đặt Key 3.
* Tạo FinalDoor.
* Thiết lập Collider.
* Kiểm tra đường đi trong Cave.

### Không tự ý sửa

* GameManager.cs
* SceneLoader.cs
* PlayerController.cs
* Key.cs
* UIManager.cs

---

## Member 4 – Treasure Room

### Phụ trách

```text
Assets/Scenes/TreasureRoom.unity
```

### Công việc

* Tạo TreasureRoom Scene.
* Tạo TreasureSpawn.
* Dựng môi trường.
* Tạo tường.
* Bố trí ánh sáng.
* Đặt Treasure.
* Tạo khu vực tương tác với Treasure.
* Trang trí khu vực cuối.
* Kiểm tra điều kiện Win.

### Không tự ý sửa

* GameManager.cs
* SceneLoader.cs
* PlayerController.cs

---

## Member 5 – Gameplay & Integration

Member 5 chịu trách nhiệm kết nối các thành phần của project thành một game hoàn chỉnh.

### Player

```text
PlayerController.cs
CameraController.cs
```

### Gameplay

```text
Key.cs
Door.cs
Treasure.cs
```

### Scene Management

```text
GameManager.cs
SceneLoader.cs
```

### UI

```text
UIManager.cs
```

### Integration

* Kết nối các Scene.
* Thiết lập Scene order.
* Kiểm tra Scene transition.
* Kiểm tra Key Count.
* Kiểm tra Final Door.
* Kiểm tra Treasure.
* Kiểm tra Win Condition.
* Kiểm tra toàn bộ gameplay.
* Fix các lỗi liên quan đến hệ thống chung.

---

# 10. Object Naming Convention

Các object quan trọng phải được đặt tên thống nhất.

## Player

```text
Player
```

## Spawn

```text
BeachSpawn
ForestSpawn
CaveSpawn
TreasureSpawn
```

## Key

```text
Key1
Key2
Key3
```

## Exit

```text
ForestExit
CaveExit
```

## Final Door

```text
FinalDoor
```

## Treasure

```text
Treasure
```

Không tự ý đổi tên các object hệ thống nếu chưa trao đổi với người phụ trách.

---

# 11. Scene Naming Convention

Tên Scene thống nhất:

```text
Beach
Forest
Cave
TreasureRoom
```

Không đặt:

```text
Beach_New
Beach_Final
Beach_Final2
Beach_Test
Forest_New
```

trừ khi thật sự cần thiết trong quá trình phát triển.

---

# 12. Script Naming Convention

Script sử dụng PascalCase:

```text
PlayerController.cs
CameraController.cs
Key.cs
Door.cs
Treasure.cs
GameManager.cs
SceneLoader.cs
UIManager.cs
```

Không sử dụng:

```text
player.cs
playercontroller.cs
test.cs
script1.cs
newscript.cs
finalscript.cs
```

---

# 13. Prefab Convention

Các object dùng nhiều lần nên được chuyển thành Prefab.

Ví dụ:

```text
Player.prefab
Key.prefab
Door.prefab
Treasure.prefab
```

Mục tiêu:

* Dễ tái sử dụng.
* Dễ chỉnh sửa.
* Giảm việc copy object thủ công.
* Giảm sai khác giữa các object.

Ví dụ:

```text
Key1
Key2
Key3
```

nên được tạo từ:

```text
Key.prefab
```

---

# 14. Collider & Trigger

Các vật thể cản Player phải có Collider.

Ví dụ:

```text
Ground
Rock
Tree
Wall
Door
```

Các khu vực cần phát hiện Player có thể sử dụng:

```text
Collider
Is Trigger = true
```

Ví dụ:

```text
ForestExit
CaveExit
Treasure
```

---

# 15. Nguyên tắc làm việc với Scene

Đây là quy tắc quan trọng nhất khi làm việc nhóm với Unity.

## Một Scene chỉ có một người phụ trách chính

```text
Beach.unity        → Member 1
Forest.unity       → Member 2
Cave.unity          → Member 3
TreasureRoom.unity → Member 4
```

Không nên có hai người cùng sửa một Scene trong cùng thời điểm.

### Ví dụ

Nếu Member 1 đang sửa:

```text
Beach.unity
```

thì Member 2 không tự ý mở và chỉnh sửa Beach.

Nếu cần thay đổi:

1. Báo người phụ trách Scene.
2. Thống nhất thay đổi.
3. Người phụ trách thực hiện hoặc commit thay đổi rõ ràng.

---

# 16. Nguyên tắc làm việc với Script

Không tự ý sửa Script mà thành viên khác đang phụ trách.

Ví dụ:

Member 2 phát hiện lỗi:

```text
GameManager.cs
```

Nếu Member 5 đang phụ trách file này, Member 2 nên báo lỗi cho Member 5 thay vì tự sửa.

Quy trình:

```text
Member 2
   ↓
Phát hiện lỗi
   ↓
Báo Member 5
   ↓
Member 5 sửa
   ↓
Commit + Push
   ↓
Member 2 Git Pull
   ↓
Test lại
```

---

# 17. Git & GitHub

Project sử dụng:

```text
Git
GitHub
```

Git được sử dụng để:

* Lưu lịch sử thay đổi.
* Đồng bộ code giữa các thành viên.
* Khôi phục phiên bản cũ.
* Làm việc nhóm.
* Theo dõi ai đã thay đổi file nào.

---

# 18. Quy trình Git chuẩn

## Bước 1 – Trước khi bắt đầu làm

Luôn chạy:

```bash
git pull
```

Mục đích:

> Lấy phiên bản mới nhất từ GitHub.

---

## Bước 2 – Làm việc

Thực hiện công việc của mình trong Unity.

Sau khi hoàn thành một phần nhỏ:

* Save Scene.
* Save Prefab.
* Kiểm tra Console.
* Play Test.

---

## Bước 3 – Kiểm tra thay đổi

```bash
git status
```

Kiểm tra các file đã thay đổi.

---

## Bước 4 – Add

```bash
git add .
```

---

## Bước 5 – Commit

```bash
git commit -m "Create forest scene"
```

---

## Bước 6 – Push

```bash
git push
```

---

# 19. Commit Message

Commit message phải mô tả đúng công việc.

## Không nên

```text
update
fix
test
done
abc
123
```

## Nên

```text
Initial Unity project
Create beach scene
Create forest scene
Create cave scene
Create treasure room
Add player controller
Add player jump
Add player animation
Add key pickup system
Add game manager
Add scene loader
Add door system
Add UI key counter
Add treasure win system
Fix player collision
Fix scene transition
Fix key pickup
```

---

# 20. Quy trình làm việc chuẩn của một thành viên

```text
START
  │
  ▼
git pull
  │
  ▼
Mở Unity
  │
  ▼
Thực hiện công việc
  │
  ▼
Save
  │
  ▼
Play Test
  │
  ▼
Kiểm tra Console
  │
  ├───────────────┐
  │               │
 Có lỗi         Không lỗi
  │               │
  ▼               ▼
Fix lỗi         git status
                  │
                  ▼
               git add .
                  │
                  ▼
               git commit
                  │
                  ▼
               git push
                  │
                  ▼
                 END
```

---

# 21. Không Push Code Chưa Test

Không được:

```text
Code
↓
git add .
↓
git push
```

mà chưa chạy thử.

Phải:

```text
Code
↓
Save
↓
Compile
↓
Play Test
↓
Fix Error
↓
Test lại
↓
Commit
↓
Push
```

---

# 22. Unity Files không cần Push

Không đưa các thư mục Unity sinh tự động vào GitHub:

```text
Library/
Temp/
Obj/
Build/
Builds/
Logs/
UserSettings/
```

Các thư mục này được xử lý thông qua `.gitignore`.

---

# 23. Kiểm tra trước khi Push

Mỗi thành viên phải kiểm tra:

```text
[ ] Scene đã được Save
[ ] Prefab đã được Save
[ ] Không có Compile Error
[ ] Không có Missing Script
[ ] Không có file không liên quan
[ ] Chức năng đã được Test
[ ] Không sửa Scene của thành viên khác
[ ] Không đổi tên Object hệ thống
[ ] Không push Library/
[ ] git status đã được kiểm tra
```

---

# 24. Bug Report

Không báo lỗi kiểu:

> "Game lỗi."

Phải mô tả rõ:

```text
BUG REPORT

Scene:
Forest

Lỗi:
Player đi xuyên qua đá.

Các bước tái hiện:
1. Mở Forest.
2. Di chuyển đến Key2.
3. Đi sát viên đá.
4. Player xuyên qua đá.

Kết quả mong muốn:
Player bị chặn bởi đá.

Kết quả thực tế:
Player đi xuyên qua đá.

Nghi ngờ:
Rock chưa có Collider.
```

---

# 25. Checklist cho từng Scene

## Beach

```text
[ ] Ground
[ ] BeachSpawn
[ ] Environment
[ ] Key1
[ ] ForestExit
[ ] Collider
[ ] Player Test
```

## Forest

```text
[ ] Ground
[ ] ForestSpawn
[ ] Environment
[ ] Key2
[ ] CaveExit
[ ] Collider
[ ] Player Test
```

## Cave

```text
[ ] CaveSpawn
[ ] Ground
[ ] Environment
[ ] Lighting
[ ] Key3
[ ] FinalDoor
[ ] Collider
[ ] Player Test
```

## Treasure Room

```text
[ ] TreasureSpawn
[ ] Ground
[ ] Environment
[ ] Lighting
[ ] Treasure
[ ] Win Area
[ ] Player Test
```

---

# 26. Gameplay Checklist

Game hoàn chỉnh khi có thể thực hiện toàn bộ:

```text
[ ] Player xuất hiện tại Beach
[ ] Player di chuyển được
[ ] Player chạy được
[ ] Player nhảy được
[ ] Camera hoạt động
[ ] Key1 có thể thu thập
[ ] Key Count cập nhật
[ ] Beach → Forest
[ ] Key2 có thể thu thập
[ ] Forest → Cave
[ ] Key3 có thể thu thập
[ ] Final Door kiểm tra Key Count
[ ] Final Door mở khi đủ 3 Keys
[ ] Cave → Treasure Room
[ ] Treasure có thể tương tác
[ ] YOU WIN hiển thị
```

---

# 27. Trạng thái phát triển

Tiến độ hiện tại:

```text
[ ] Project Setup
[ ] GitHub Setup
[ ] Player Controller
[ ] Camera
[ ] Player Animation
[ ] Beach
[ ] Forest
[ ] Cave
[ ] Treasure Room
[ ] Key System
[ ] Game Manager
[ ] Scene Loader
[ ] Door System
[ ] UI
[ ] Treasure
[ ] Win System
[ ] Full Game Test
[ ] Final Polish
```

---

# 28. Phạm vi của Project

Do thời gian phát triển có giới hạn, project tập trung vào một gameplay loop đơn giản nhưng hoàn chỉnh.

## Bắt buộc

* Player.
* Movement.
* Camera.
* Gravity.
* Jump.
* Animation.
* 3 Keys.
* Key Counter.
* Door.
* Scene Transition.
* Game Manager.
* UI.
* Treasure.
* Win Condition.

## Có thể bổ sung

Sau khi các tính năng bắt buộc hoàn thành:

* Particle Effect.
* Sound Effect.
* Background Music.
* Animation nâng cao.
* Lighting nâng cao.
* Post Processing.
* UI nâng cao.
* Camera Effect.

## Không nằm trong phạm vi hiện tại

Để tránh project quá lớn so với thời gian:

* Enemy AI.
* Combat.
* Boss.
* NPC.
* Quest System.
* Inventory phức tạp.
* Crafting.
* Skill Tree.
* Multiplayer.
* Save/Load phức tạp.

---

# 29. Thứ tự ưu tiên

Khi phát triển project, ưu tiên theo thứ tự:

```text
1. Gameplay
2. Collision
3. Scene Transition
4. Key System
5. Door System
6. Win Condition
7. Animation
8. Lighting
9. Particle
10. Sound
11. Polish
```

Không dành quá nhiều thời gian trang trí khi gameplay chưa hoạt động.

---

# 30. Nguyên tắc quan trọng

## 1. Gameplay trước, đồ họa sau

Game chạy được trước khi cố làm quá đẹp.

## 2. Chia nhỏ công việc

Làm từng chức năng nhỏ và test từng phần.

## 3. Không tự ý sửa phần của người khác

Nếu cần thay đổi, trao đổi trước.

## 4. Commit rõ ràng

Người khác phải hiểu được commit vừa thay đổi gì.

## 5. Test trước khi Push

Không push code chưa kiểm tra.

## 6. Không mở rộng phạm vi tùy ý

Không tự ý thêm hệ thống lớn ngoài phạm vi project nếu nhóm chưa thống nhất.

---

# 31. Definition of Done

Một công việc chỉ được xem là **Done** khi đáp ứng đủ:

```text
[ ] Đã thực hiện
[ ] Đã Save
[ ] Đã Compile
[ ] Đã Play Test
[ ] Không có lỗi nghiêm trọng
[ ] Không ảnh hưởng phần khác
[ ] Đã Commit
[ ] Đã Push
```

Nói cách khác:

> **Làm xong không đồng nghĩa với Done.**

Một task chỉ thực sự Done khi đã được test và đồng bộ lên GitHub.

---

# 32. Quy trình bàn giao

Khi hoàn thành phần việc, thành viên nên ghi rõ:

```text
========================================
BÀN GIAO CÔNG VIỆC
========================================

Người thực hiện:
Member X

Phần:
Beach Scene

File:
Assets/Scenes/Beach.unity

Đã hoàn thành:
- Ground
- BeachSpawn
- Environment
- Key1
- ForestExit
- Collider

Đã kiểm tra:
- Player đứng được
- Player di chuyển được
- Key1 có thể tiếp cận
- Không có Missing Script

Chưa hoàn thành:
- SceneLoader chưa gắn

Cần làm tiếp:
- Member 5 gắn SceneLoader cho ForestExit

Lưu ý:
- Không đổi tên ForestExit
========================================
```

---

# 33. Final Game Flow

Gameplay cuối cùng phải hoạt động theo đúng thứ tự:

```text
                        START
                          │
                          ▼
                   ┌─────────────┐
                   │    BEACH    │
                   │             │
                   │   Key 1     │
                   └──────┬──────┘
                          │
                          ▼
                   ┌─────────────┐
                   │   FOREST    │
                   │             │
                   │   Key 2     │
                   └──────┬──────┘
                          │
                          ▼
                   ┌─────────────┐
                   │    CAVE     │
                   │             │
                   │   Key 3     │
                   └──────┬──────┘
                          │
                          ▼
                   ┌─────────────┐
                   │ FINAL DOOR  │
                   │             │
                   │  3 Keys     │
                   └──────┬──────┘
                          │
                          ▼
                   ┌─────────────┐
                   │  TREASURE   │
                   │    ROOM     │
                   └──────┬──────┘
                          │
                          ▼
                       TREASURE
                          │
                          ▼
                       YOU WIN
```

---

# 34. Team Structure

| Thành viên | Phụ trách                     |
| ---------- | ----------------------------- |
| Member 1   | Beach                         |
| Member 2   | Forest                        |
| Member 3   | Cave                          |
| Member 4   | Treasure Room                 |
| Member 5   | Gameplay System & Integration |

---

# 35. Project Goal

Mục tiêu cuối cùng của project không phải là tạo một game có quy mô lớn.

Mục tiêu là tạo ra một game 3D nhỏ nhưng hoàn chỉnh, trong đó:

* Người chơi có thể điều khiển nhân vật.
* Có môi trường 3D.
* Có nhiều khu vực khác nhau.
* Có tương tác với vật thể.
* Có hệ thống thu thập vật phẩm.
* Có điều kiện mở cửa.
* Có chuyển Scene.
* Có UI.
* Có ánh sáng và animation.
* Có điều kiện chiến thắng.

---

# 36. Development Principle

> **Build Small. Test Often. Commit Clearly.**

### Build Small

Chia project thành các phần nhỏ, dễ kiểm soát.

### Test Often

Test ngay sau khi hoàn thành một chức năng.

### Commit Clearly

Mỗi commit phải có nội dung rõ ràng để các thành viên khác biết đã thay đổi gì.

---

# 37. Contact / Team

Project được thực hiện bởi nhóm sinh viên cho môn học:

> **Đồ họa và Hiện thực ảo**

Repository:

```text
GitHub Repository
```

---

# 🏆 Final Objective

Hoàn thành một game 3D có vòng lặp:

```text
EXPLORE
   ↓
FIND KEYS
   ↓
UNLOCK
   ↓
EXPLORE MORE
   ↓
FIND TREASURE
   ↓
WIN
```

**Treasure Hunt 3D**
*Explore. Find. Unlock. Win.*

````

Sau khi lưu file `README.md`, bạn chỉ cần chạy:

```bash
git add README.md
git commit -m "Add complete project README"
git push
````

README này sẽ được GitHub hiển thị ngay ở trang chính của repository.
