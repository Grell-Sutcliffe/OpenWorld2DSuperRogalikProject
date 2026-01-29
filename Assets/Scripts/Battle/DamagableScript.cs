using System;
using System.Collections.Generic;
using UnityEngine;

public class DamagableScript : MonoBehaviour
{
    public class Creature
    {
        int max_health;
        int current_health;

        int max_attack;
        Damage current_dmg;

        float crit_chance;
        float crit_dmg;

        float max_speed;
        float current_speed;

        float max_defence;
        float current_defence;

        ElementType element;
        List<ElementType> element_on;

        Weapon weapon;

        public Creature(int max_health_, int max_attack_, float max_speed_, float crit_chance = 0f, float crit_dmg = 0f, float max_defence_ = 1.0f, ElementType element_ = ElementType.None, Weapon weapon = null)
        {
            max_health = max_health_;
            current_health = max_health_;

            max_attack = max_attack_;
            int min_delta_dmg = RoundToMax(max_attack_);
            current_dmg = new Damage(min_delta_dmg, weapon.elementalDamage);

            max_defence = max_defence_;
            current_defence = max_defence_;

            max_speed = max_speed_;
            current_speed = max_speed_;

            element = element_;
            element_on = new List<ElementType>();

            this.weapon = weapon;
        }

        public void ChangeHealth(int health_amount)
        {
            current_health += health_amount;

            if (current_health > max_health)
            {
                current_health = max_health;
            }
            if (current_health < 0)
            {
                current_health = 0;
            }

            if (current_health == 0)
            {
                Die();
            }
        }

        public void DealDamage()
        {
            System.Random rand = new System.Random();

            float min_dmg = this.max_attack;
            float max_dmg = this.crit_dmg * this.max_attack;

            int min_delta_dmg = RoundToMax(min_dmg);
            int max_delta_dmg = RoundToMax(max_dmg);

            int delta_dmg = rand.Next(min_delta_dmg, max_delta_dmg + 1);

            this.current_dmg = new Damage(delta_dmg, weapon.elementalDamage);
        }

        public void TakeDamage(Damage damage)
        {
            //int dmg_amount = damage.GetDamage();                       
            // ÈÇ ÇÀ ÔËÎÀÒÀ ÒÅÏÅÐÜ ÍÅ ÐÀÁÎÒÀÅÒ ÑÂÅÐÕÓ
            //float delta_dmg_amount = dmg_amount * this.current_defence;
            //dmg_amount -= (int)delta_dmg_amount;

            //this.ChangeHealth(-dmg_amount);
        }

        void SetOnElement(ElementType element)
        {
            this.element_on.Add(element);

            switch (element)
            {
                case (ElementType.None):
                    break;
                case (ElementType.Cryo):
                    // freeze;
                    break;
                case (ElementType.Pyro):
                    // set on fire;
                    break;
                case (ElementType.Electro):
                    // electrify;
                    break;
                case (ElementType.Anemo):
                    // blow up;
                    break;
            }
        }

        void SetOnNone()
        {
            this.element_on = new List<ElementType>();
        }

        void SetOnCryo()
        {

        }

        public Damage GetDamage()
        {
            return this.current_dmg;
        }

        void Die()
        {

        }

        int RoundToMax(float number)
        {
            return (int)(number * 10 % 10 > 0 ? number + 1 : number);
        }
    }


    

    
}
