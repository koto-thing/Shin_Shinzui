# Shin_Shinzui Project Guidelines

このプロジェクトは **Clean Architecture** に基づいて構成されています。各レイヤーの役割と依存関係のルールを厳守してください。

## 1. フォルダ構造とレイヤーの役割

### 📂 Assets/Shin_Shinzui/Scripts/
*   **Application/** (UseCase, Interfaces, DTOs)
    *   **役割**: 純粋なゲームロジック、機能の抽象（インターフェース）。
    *   **依存ルール**: **外部（Presentation, Infrastructure）に依存してはならない。**
    *   **禁止事項**: `UnityEngine` や `View` クラス、`GameObject` を直接参照すること。
*   **Domain/** (Entities)
    *   **役割**: 最も純粋なデータモデルとビジネスルール。
    *   **依存ルール**: 何ものにも依存しない。
*   **Presentation/** (Presenters, Services)
    *   **役割**: UIの制御、ユーザー入力の検知、Viewの操作。
    *   **依存ルール**: `Application` と `View` に依存できる。
*   **View/** (MonoBehaviours)
    *   **役割**: 描画、アニメーション、UI部品の実態。
    *   **依存ルール**: ロジックを持たず、Presenterからの命令を待つ。
*   **Infrastructure/** (External Services)
    *   **役割**: Addressablesのロード、JSONの保存、Input Systemの実装など。
    *   **依存ルール**: `Application` のインターフェースを実装する。

## 2. 依存関係の黄金律
**「依存は常に内側（Application/Domain）に向かう」**

1.  **UseCaseはViewを知らない**:
    *   ❌ `UseCase.Execute(SomeView view)` : **厳禁**
    *   ✅ `Presenter` が `InputService` を購読し、`View` を操作するか、`UseCase` を呼ぶ。
2.  **InfrastructureはViewを知らない**:
    *   ❌ `InfrastructureService` が `View` を直接生成・操作する。
    *   ✅ `Presentation/Services` が `Infrastructure` の Factory で生成された `GameObject` を `View` として扱う。
3.  **UIの進行（スキップなど）はPresenterの責務**:
    *   Viewのアニメーション（Tween）の停止やスキップは `Presentation` 層で完結させる。

## 3. 技術スタックの利用ルール
*   **非同期処理**: `UniTask` を使用。
*   **イベント・購読**: `R3` を使用。
*   **DIコンテナ**: `VContainer` を使用。
*   **アニメーション**: `DOTween` を使用。
*   **データロード**: `Addressables` + `JsonUtility` (Wrapper経由) を使用。

## 4. 命名規則
*   **インターフェース**: `I` から始める (例: `IInputService`)。
*   **実装**: `Infrastructure` に配置する場合は具体的な技術名を冠する (例: `UnityInputService`)。
*   **非同期メソッド**: `Async` を末尾に付ける。
