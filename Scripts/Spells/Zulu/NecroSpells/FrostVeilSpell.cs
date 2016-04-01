using Server.Items;
using Server.Mobiles;
using Server.Spells.Seventh;
using System;
using System.Collections.Generic;

namespace Server.Spells.Zulu.NecroSpells
{
    public class FrostVeilSpell : NecroSpell
    {
        private static readonly SpellInfo m_Info = new SpellInfo(
            "Veil of Frost", "Frontinus velum", // Corpus Sine Nomine Expergefaceret
            203,
            9051,
            Reagent.Bone,
            Reagent.FertileDirt,
            Reagent.VialofBlood,
            Reagent.Obsidian);
        
        public FrostVeilSpell(Mobile caster, Item scroll)
            : base(caster, scroll, m_Info)
        {
        }

        public override void OnCast()
        {
            if (this.CheckSequence())
            {
                /* Creates a withering frost around the Caster,
                * which deals Cold Damage to all valid targets in a radius of 5 tiles.
                */
                Map map = this.Caster.Map;

                if (map != null)
                {
                    List<Mobile> targets = new List<Mobile>();

                    BaseCreature cbc = this.Caster as BaseCreature;
                    bool isMonster = (cbc != null && !cbc.Controlled && !cbc.Summoned);

                    foreach (Mobile m in this.Caster.GetMobilesInRange(7))
                    {
                        if (this.Caster != m && this.Caster.InLOS(m) && (isMonster || SpellHelper.ValidIndirectTarget(this.Caster, m)) && this.Caster.CanBeHarmful(m, false))
                        {
                            if (isMonster)
                            {
                                if (m is BaseCreature)
                                {
                                    BaseCreature bc = (BaseCreature)m;

                                    if (!bc.Controlled && !bc.Summoned && bc.Team == cbc.Team)
                                        continue;
                                }
                                else if (!m.Player)
                                {
                                    continue;
                                }
                            }

                            targets.Add(m);
                        }
                    }

                    Effects.PlaySound(this.Caster.Location, map, 0x1FB);
                    Effects.PlaySound(this.Caster.Location, map, 0x10B);
                    Effects.SendLocationParticles(EffectItem.Create(this.Caster.Location, map, EffectItem.DefaultDuration), 0x37CC, 1, 40, 97, 3, 9917, 0);

                    for (int i = 0; i < targets.Count; ++i)
                    {
                        Mobile m = targets[i];

                        this.Caster.DoHarmful(m);
                        m.FixedParticles(0x37B9, 1, 15, 9502, 97, 3, (EffectLayer)255);

                        double damage = Utility.RandomMinMax(30, 35);

                        damage *= (300 + (m.Karma / 100) + (this.GetDamageSkill(this.Caster) * 10));
                        damage /= 1000;

                        int sdiBonus = AosAttributes.GetValue(this.Caster, AosAttribute.SpellDamage);


                        damage *= (100 + sdiBonus);
                        damage /= 100;

                        // TODO: cap?
                        //if ( damage > 40 )
                        //	damage = 40;

                        SpellHelper.Damage(this, m, damage, 0, 0, 100, 0, 0);
                    }
                }
            }

            this.FinishSequence();
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
                return 40;
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
