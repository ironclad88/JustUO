using Server.Items;
using Server.Spells.Seventh;
using Server.Targeting;
using System;

namespace Server.Spells.Zulu.NecroSpells
{
    public class BloodStoneSpell : NecroSpell
    {
        private static readonly SpellInfo m_Info = new SpellInfo(
            "Summon blood stone", "Voca sanguinem lapis",
            203,
            9051,
            Reagent.BloodSpawn,
            Reagent.VialofBlood,
            Reagent.DragonBlood);
        
        public BloodStoneSpell(Mobile caster, Item scroll)
            : base(caster, scroll, m_Info)
        {
        }
        
        public override void OnCast()
        {
            if (this.Caster.Backpack != null && this.CheckSequence())
            {
                Item[] stones = this.Caster.Backpack.FindItemsByType(typeof(bloodStone));

                for (int i = 0; i < stones.Length; i++)
                    stones[i].Delete();

                int amount = (int)(Caster.Skills.Magery.Value / 5);
                this.Caster.PlaySound(0x651);
                this.Caster.Backpack.DropItem(new bloodStone(this.Caster, amount));
                this.Caster.SendLocalizedMessage(1080115); // A Healing Stone appears in your backpack.
            }
        }

        public override TimeSpan CastDelayBase
        {
            get
            {
                return TimeSpan.FromSeconds(1.5);
            }
        }
        public override double RequiredSkill
        {
            get
            {
                return 130;
            }
        }
        public override int RequiredMana
        {
            get
            {
                return 120;
            }
        }
        public override bool DelayedDamage
        {
            get
            {
                return false;
            }
        }
        

    }
}
