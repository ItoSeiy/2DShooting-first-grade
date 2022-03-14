# 規則

変数名は[キャメルケース](https://e-words.jp/w/%E3%82%AD%E3%83%A3%E3%83%A1%E3%83%AB%E3%82%B1%E3%83%BC%E3%82%B9.html) (先頭小文字)

メンバー変数の接頭辞には「＿」(アンダースコア)を付けること

関数名　スクリプトの名前　プロパティの名前は[パスカルケース](https://wa3.i-3-i.info/word13955.html) (先頭大文字)

ブランチの名前は[スネークケース](https://e-words.jp/w/%E3%82%B9%E3%83%8D%E3%83%BC%E3%82%AF%E3%82%B1%E3%83%BC%E3%82%B9.html#:~:text=%E3%82%B9%E3%83%8D%E3%83%BC%E3%82%AF%E3%82%B1%E3%83%BC%E3%82%B9%E3%81%A8%E3%81%AF%E3%80%81%E3%83%97%E3%83%AD%E3%82%B0%E3%83%A9%E3%83%9F%E3%83%B3%E3%82%B0,%E3%81%AA%E8%A1%A8%E8%A8%98%E3%81%8C%E3%81%93%E3%82%8C%E3%81%AB%E5%BD%93%E3%81%9F%E3%82%8B%E3%80%82)
(すべて小文字単語間は「＿」(アンダースコア))
機能を作成するブランチであれば接頭辞に「feature/」を付けてください

コミットは頻繁に行うこと

# 2Dシューティングゲーム 
Issueに当たり前のことが沢山書いてありますがプログラミングが苦手な人に教える意図があり行っていますのでご承知おきください
# 制作意図

**「大前提として人にゲームを届けたいという想いは確かです」**

元々クラスの中にはライバルがいませんでした

そのため自分が技術的にも成長する刺激が少なく困っていました

そのためライバルを作るために技術やチーム開発をするうえで必要な知識(Github, Sourcetree)を教えたかったのです

バンタンゲームアカデミー1年次の審査会(学んできた技術をゲームを発表するという形で発表する場)が近かったこともありこのプロジェクトを立ち上げました

# 概要

ブラッシュアップ中です

2021バンタンゲームアカデミー1年次後期審査会に向けたチーム制作の2D-シューティングゲームです

Githubをフル活用した制作です

タスク振り分, 進行管理をIssuesやProjectsを用いて行いました

## プレイ動画

[動画を取り直す予定ですが学内発表用に使用したものを掲載します](https://www.youtube.com/watch?v=wLbu2chwMls)

## 作業内容(伊東聖矢) こだわり

**ここにゲーム面での作業内容を記載すると莫大な分量になりますので[こちらのIssue](https://github.com/ItoSeiy/2DShooting-first-grade/issues?q=is%3Aissue+is%3Aclosed+assignee%3AItoSeiy)をご覧ください**

基底クラスやゲームマネージャーを作る際はとにかく汎用性が高く扱いやすいものを意識してつくりました

[EnemyBase](https://github.com/ItoSeiy/2DShooting-first-grade/blob/feature/enemy_bese_class/Assets/1Ito/Scripts%20Ito/Enemy/EnemyBese.cs)
[BulletBase](https://github.com/ItoSeiy/2DShooting-first-grade/blob/feature/bullet_bese_class/Assets/1Ito/Scripts%20Ito/Bullet/BulletBese.cs)
[GameManager](https://github.com/ItoSeiy/2DShooting-first-grade/blob/feature/game_maneger/Assets/1Ito/Scripts%20Ito/GameManager.cs)

プロジェクトメンバーへの技術的なことを教える(プログラミング, Unityの知識)

Issueの作成

**Issueへのこだわり**

例)誰がどのブランチ名のブランチで作業するかを確実に明記する

例)書体,大きさなどを要点とそうでは無いもので相違点をつくる

例)**ファイル整理が大事なので**ファイル名の指定やオブジェクト名の指定を記載した

==================================================================

オブジェクト名やファイル管理が大事だと思った経緯

   ↓

以前に[ゲームジャム](https://github.com/ItoSeiy/1124GameJam)を行った際ゲームが時間内に完成できなかった
   
   ↓　

理由は最後に行う機能同士の組み込みで時間がかかりすぎたため

   ↓
    
機能同士の組み込みで時間がかかった理由はオブジェクト名やファイルの管理が甘く組み込む際に混乱を招いたため

   ↓
   
オブジェクト名やファイル管理が大事だと気づいた

==================================================================

例)技術的なことが分からない人のために参考文献等を記載した

例)Assigneesを設定することで均等にタスクを振り分けられているか確認する

例)IssueやPull requestsにラベルを付けることでどのような作業かを一目でわかるようにした

プロジェクトメンバー全員へのタスク振り分け

キャラクター等のイラスト発注書チェック

コミュニケーションが円滑にとれるようにコミュニケーションツール「ディスコード」のサーバーの管理

## 制作人数

バンタンゲームアカデミーゲーム制作専攻高等部1年 6名

バンタンゲームアカデミーキャラクターデザイン専攻 1名

プログラマー, プロジェクト管理　伊東聖矢

イラストレーター　Kokushi Kanta [Twitter](https://twitter.com/kibahachimonn)

サウンド neko.yanagii [Twitter](https://twitter.com/notreallyrook)

プログラマー 神原琉成

プログラマー 和田有矢 (ゲーム原案, プレイヤー2の弾幕の絵の作成, )

プログラマー 近藤倫太　UIの機能(DoTween),UIのデザイン キャラクター2の弾幕の絵の作成など

プログラマー 安達青　ボスの作成

小島智祐　([キャラクター発注書の作成](https://docs.google.com/spreadsheets/d/1sF1S3a3Yge3sxgV-ppf4J7LAbr12b9YN/edit?rtpof=true&sd=true), シナリオ)

## 制作形式　

Unity,
Github,
Sourcetree,

上記の3点を用いて制作しました
