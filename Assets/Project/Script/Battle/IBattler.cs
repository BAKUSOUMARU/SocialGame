
public interface IBattler
{
   Status Status { get; }

   /// <summary>
   /// 戦闘中に死んだ場合の処理をここに書く
   /// </summary>
   void Dead();
}