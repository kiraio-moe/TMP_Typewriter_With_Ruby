# TMP_Typewriter_With_Ruby

[baba-s/TMP_Typewriter](https://github.com/baba-s/TMP_Typewriter)に[TextMeshProRuby](https://github.com/ina-amagami/TextMeshProRuby)でのルビ振りを対応させたものです。ルビタグを付けた文字列を渡すことで、漢字の表示が終わった時点でルビも同時に表示されます。

![TMP_Typewriter_With_Ruby](https://amagamina.jp/blog/wp-content/uploads/2019/12/tmpro-ruby-animation.gif)

Assets/TextMeshProRubyフォルダとTMP_Typewriterフォルダをプロジェクトにコピーして下さい（利用にはDOTweenが必須です）

```Example.cs
public class Example : MonoBehaviour
{
    public TMP_Typewriter   m_typewriter    ;
    public float            m_speed         ;

    private void Update()
    {
        if ( Input.GetKeyDown( KeyCode.Z ) )
        {
            // 1 文字ずつ表示する演出を再生（ルビ対応）
            m_typewriter.Play
            (
                text        : "このテキストは\n<r=かんじ>漢字</r>テキストに\nルビが<r=ふ>振</r>られます",
                speed       : m_speed,
                onComplete  : () => Debug.Log( "完了" ),
                // ルビがある行とない行で高さが変動しないようにするにはtrue
                fixedLineHeight: false,
                // 1行目にルビがある時、TextMeshProのMargin機能を使って位置調整
                autoMarginTop: true
            );
        }
        //...
    }
}
```
