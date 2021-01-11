# What is CustomUI
CustomUI is a lightweight uGUI extension package.

# UI Components
## CustomText
CustomText is an extension component of uGUI Text. It's easier to change fontType, fontColor etc by only selecting TextType enum.

You need to place <b>CustomTextSettings.xml</b> in order to use TextType selecting. CustomText applies several text settings according to <b>CustomTextSettings.xml</b>

You can know how to write CustomTextSettings.xml by <b>Template/CustomTextSettings.xml</b> The easist way to create CustomTextSettings.xml is just to copy the template into your Resources folder.

| Tag      | Explanation   | Detail                                           |
|----------|---------------|--------------------------------------------------|
| TextType | enum textType | This is related to enum TextType                 |
| FontData | FontData      | This string is path to fontData from Resources   |
| TextSize | TextSize      | This is the size of text according to TextType   |
| Color    | Color(RGB)    | This is the color of text accordiong to TextType |

CustomTextはゲーム内の文字の一括管理を容易にし、ゲームないで統一したテキスト規則を簡単に、かつ柔軟に導入することができます。
具体的には、CustomTextのTextType enumを選択することで、TextSettings.xmlに応じた設定が自動で反映されます。
また、WordBook.jsonで管理された文言データをもとに、キーで該当する文字を取得することができます。これにより、プロジェクト内で汎用的に使う文字の差し替えが容易になるメリットや、多言語対応もWordBook.jsonを差し替えるのみで実装できるようになります。
また、Editor拡張にはWordBookに文言を追加、削除することが可能になっています。