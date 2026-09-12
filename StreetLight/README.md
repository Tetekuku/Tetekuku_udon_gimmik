# Mashiro StreetLight

街灯（streetlight）にプレイヤーが近づいたときだけ、光の落ち（light_fall）を表示するギミックです。

## できること

- 街灯から指定した距離（既定 3m）以内にプレイヤーがいる間だけ、light_fall を表示する
- プレイヤーが離れると自動的に非表示に戻る

## セットアップ

1. 街灯の GameObject に `Add Component > Tetekuku > Street Light` を追加する
2. インスペクタの `Light Fall` に、光の落ちを表す GameObject（例: `light_fall`）を設定する
3. 必要であれば `Radius` を変更する（既定は 3）

## インスペクタ項目

| 項目 | 説明 |
| --- | --- |
| Light Fall | プレイヤーが近づいたときに表示する GameObject |
| Radius | この距離（メートル）以内にプレイヤーがいる間だけ表示する |

## API リファレンス

外部から呼び出す public メソッドはありません。

## 注意点

- 見た目だけの演出のため、ネットワーク同期は行いません。各プレイヤーが自分と街灯の距離だけを見て、自分の画面の light_fall を切り替えます。
- `Light Fall` を設定し忘れると、Console に警告が出ます。
