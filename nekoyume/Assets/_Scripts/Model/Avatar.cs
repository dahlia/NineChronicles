using System.Collections.Generic;
using System.Linq;
using Nekoyume.Move;

namespace Nekoyume.Model
{
    [System.Serializable]
    public class Avatar
    {
        public string name;
        public string class_;
        public int level;
        public int gold;
        public int exp;
        public int hp;
        public string[] items;
        public int world_stage;
        public byte[] user;
        public bool dead
        {
            get
            {
                return hp <= 0;
            }
        }

        public static Avatar FromMoves(IEnumerable<Move.Move> moves)
        {
            var createNovice = moves.FirstOrDefault() as CreateNovice;
            if (createNovice == null)
            {
                return null;
            }

            var ctx = new Context();
            var avatar = createNovice.Execute(ctx).avatar;

            foreach (var move in moves.Skip(1))
            {
                avatar = move.Execute(ctx).avatar;
            }

            return avatar;
        }
    }

    public enum CharacterClass
    {
        Novice,
        Swordman,
        Mage,
        Archer,
        Acolyte
    }
}
