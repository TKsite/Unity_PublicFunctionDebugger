# Unity_PublicFunctionDebugger

![Image](https://user-images.githubusercontent.com/132959865/236965014-dabc2153-0dde-46d4-8d67-85c38fab57a7.png)

## 詳細

unitypackageをインポートすると、ゲームオブジェクトにアタッチされたすべてのpublic関数がエディタ上から実行できるようになります。

引数あり、戻り値ありの関数にも対応しています。
もちろんvoid型にも対応しています。
戻り値についてはエディタ上に表示されます。


＜対応している引数の型＞

int/float/bool/string/Vector2/Vector3/Color/Enum/gameObject

＜対応している戻り値の型＞

ToStringメゾットに対応している型全て


対応しない型の引数があると、引数名の横に"Unsupported parameter type"と表示されますが、ボタンを押せば関数はそのまま実行できます。
その場合、引数はnull渡しとなりますので、ご注意ください。

返り値または引数にgameObjectをもたなければ、エディタ非再生状態で関数実行できますが、当然スクリプト内のStartやAwakeの関数は実行されませんのでStart等で初期化をしている場合には再生状態でご使用ください。

（注）
関数のオーバーロードには対応していません。
スクリプト上の一番上に書かれている関数のみが動き、その他のオーバーロード関数はエラーが出力されるはずですが未検証です。

## 使用方法

unityのアセットにunitypackageをドラッグ＆ドロップでインポートできます。

または、Assets/Editorフォルダ内にPublicFunctionDebugger.csを作成し、ソースコードをコピペしてください。

インポートするとEditorフォルダに本エディタ拡張が、PublicDubuggerTestフォルダにテスト用のスクリプトが生成されます。

インポートのみで機能するようになるため、そのほかの特別な操作は不要です。

## ライセンス
MIT Lisence
