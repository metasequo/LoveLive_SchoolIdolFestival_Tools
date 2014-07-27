LoveLive SchoolIdolFestival Tools
=================================
1. About -このソフトについて-
---------------------------------

### LoveLive SchoolIdolFestival Tools、とは  

スマートフォン向けゲームの「ラブライブ！スクールアイドルフェスティバル」、
通称「スクフェス」のツール集です。  
入っているものは以下のツールとなります。  

1. 各ノード重要度計算機
2. ランクアップ計算機
3. InputFileCreator

の3のツールが入っています。  

### 使い方  

"/LoveLive SchoolIdolFestival Tools/bin/Debug/LoveLive_SchoolIdolFestival_Tools.exe" を起動してください。  

2.1. 各ノード重要度計算機
--------------------------------

### 各ノード重要度計算機、とは  

名前の通り、9つあるボタンの各ノードを計算し、重要度を教えてくれます。  

* このツールが役に立つ時は、  
	* フルコンボは出来るのにスコアSがなかなか取れない。  
	* スコアマッチでの3,4位ばかりになってしまう。  

などの時です。  

### 使い方  

1. タブから「各ノード重要度計算機」を選択します。
2. 譜面ファイルを選択します。（InputFileに難易度HARDの譜面ファイルがいくつか入っています。）
3. 「計算」ボタンをクリックします。
4. 各ボタンの重要度が計算され、出力されます。
5. 結果を保存したい時は、「保存」ボタンをクリックすると、保存出来ます。

2.2. ランクアップ計算機
--------------------------------

### ランクアップ計算機、とは  

その名の通り、ランクアップに必要なライブ回数を教えてくれます。  

* このツールが役立つ時は、
	* ランクアップしたばっかりで、どれくらいライブをすればランクアップするか知りたい。
	* イベント期間中で、連続してプレイ出来るときにランクアップしたい。

などの時です。  

### 使い方

1. タブから「ランクアップ計算機」を選択します。
2. 目標となる経験値、今現在の経験値、今現在のLPを入力します。アイテムイベントの時は"イベントアイテム"にチェックをつけ、所持イベントアイテム数を入力します。
3. プレイする難易度にチェックをつけます。
4. 「計算」ボタンをクリックします。
5. ランクアップまでに必要な各難易度毎（NORMAL、HARD、EXPERT）のプレイ回数、LP消費を最低限に抑えたプレイ回数、必要LP、必要回復時間、消費イベントアイテムが出力されます。
6. 結果を保存したい時は、「保存」ボタンをクリックすると、保存できます。

2.3. InpuFileCreator
--------------------------------

### InpuFileCreator、とは  

各ノード重要度計算機の入力用ファイルを作るツールです。  
入出力ファイルはテキストファイルですが、手入力だと面倒なので作りました。  

### 使い方  

1. タブから「InpuFileCreator」を選択します。
2. ニコニコ動画やYouTubeで譜面ファイルを作りたい曲のプレイ動画を探します。
3. InpuFileCreatorの「記録開始」ボタンをクリックすると記録が始まります。
4. 動画を再生します。
5. ノートが来たら、来た位置に対応するキーを入力します。
キー設定は、単押しは左から1～9の数字キー、長押しは数字キーのしたにあるQ~Oのキーがそれぞれ対応しています。
6. 「リセット」ボタンをクリックすることで、リセットすることが出来ます。
7. 記録が終了したら「記録終了」ボタンをクリックしてください。
8. 曲名や難易度を入力・選択して、保存ボタンをクリックしてください。　　

3. 更新など
--------------------------------

#### Ver1.1　2014/07/27　公開
* ランクアップ計算機がアイテムイベント(いわゆるマカロンイベント)に対応。
* アップデートにより、スコア計算の方法が判明したため、各ノード重要度計算機の計算方法を変更。
	* 各ノード重要度(Ver1.1)
	* ランクアップ計算機(Ver1.1)
　　
#### Ver1.0　2014/07/15　公開
* 基本機能の実装
	* 各ノード重要度計算機(Ver1.0)
	* ランクアップ計算機(Ver1.0)
	* InputFileCreator(Ver1.0)