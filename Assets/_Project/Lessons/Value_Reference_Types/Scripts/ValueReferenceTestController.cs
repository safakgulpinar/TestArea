using System;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

namespace _Project.Lessons.Value_Reference_Types.Scripts
{
    public class ValueReferenceTestController : MonoBehaviour
    {
        #region Value Reference (Senin örneklerin)

        [Button(ButtonSizes.Medium)]
        [FoldoutGroup("Value and Reference Types")]
        [HorizontalGroup("Value and Reference Types/Horizontal")]
        [BoxGroup("Value and Reference Types/Horizontal/Value")]
        public void ValueTest()
        {
            Vector3 pos1 = new Vector3(1, 2, 3);
            Vector3 pos2 = pos1;
            pos2.x = 99;

            Debug.Log(pos1.x); // 1
            Debug.Log(pos2.x); // 99
            Debug.Log("Value is copied! (Vector3 struct => kopya)"); 
            // Vector3 bir struct → kopya alınır, referans paylaşılmaz.
        }

        public class Player
        {
            public string name;
        }

        [Button(ButtonSizes.Medium)]
        [FoldoutGroup("Value and Reference Types")]
        [HorizontalGroup("Value and Reference Types/Horizontal")]
        [BoxGroup("Value and Reference Types/Horizontal/Reference")]
        public void ReferenceTest()
        {
            Player p1 = new Player();
            p1.name = "Alice";

            Player p2 = p1;
            p2.name = "Bob";

            Debug.Log(p1.name); // "Bob"
            Debug.Log("Now the same Reference is shown. (class => aynı referans)");
            // p1 ve p2 aynı heap adresini işaret ediyor.
        }

        #endregion


        #region Immutable & Mutable

        // IMMUTABLE ÖRNEKLERİ
        [Button(ButtonSizes.Medium), FoldoutGroup("Immutable & Mutable")]
        [HorizontalGroup("Immutable & Mutable/H")]
        [BoxGroup("Immutable & Mutable/H/Immutable")]
        public void ImmutableTest_String()
        {
            string s1 = "ABC";
            string s2 = s1;             // aynı nesneyi işaret eder
            s1 = s1 + "D";              // YENİ string oluşur, s2 hâlâ "ABC"

            Debug.Log($"s2 (değişmedi): {s2}"); // ABC
            Debug.Log($"s1 (yeni):      {s1}"); // ABCD
            Debug.Log("string IMMUTABLE: içerik yerinde değişmez, yeni nesne üretilir.");
        }

        [Button(ButtonSizes.Medium), FoldoutGroup("Immutable & Mutable")]
        [HorizontalGroup("Immutable & Mutable/H")]
        [BoxGroup("Immutable & Mutable/H/Immutable")]
        public void ImmutableTest_DateTime()
        {
            DateTime d1 = new DateTime(2025, 1, 1);
            DateTime d2 = d1.AddDays(7); // yeni değer döner, d1 değişmez

            Debug.Log($"d1: {d1:yyyy-MM-dd} (değişmedi)");
            Debug.Log($"d2: {d2:yyyy-MM-dd} (yeni kopya)");
            Debug.Log("DateTime IMMUTABLE: AddDays yeni kopya döndürür.");
        }

        // MUTABLE ÖRNEKLERİ
        [Button(ButtonSizes.Medium), FoldoutGroup("Immutable & Mutable")]
        [HorizontalGroup("Immutable & Mutable/H")]
        [BoxGroup("Immutable & Mutable/H/Mutable")]
        public void MutableTest_ListReference()
        {
            var l1 = new List<int> { 1, 2 };
            var l2 = l1; // aynı liste referansı

            l2.Add(3); // aynı nesne yerinde değişir
            Debug.Log($"l1.Count: {l1.Count} (3)");
            Debug.Log($"l2.Count: {l2.Count} (3)");
            Debug.Log("List<T> MUTABLE (class): aynı referans => yerinde değişim.");
        }

        [Button(ButtonSizes.Medium), FoldoutGroup("Immutable & Mutable")]
        [HorizontalGroup("Immutable & Mutable/H")]
        [BoxGroup("Immutable & Mutable/H/Mutable")]
        public void MutableTest_Vector3VsTransform()
        {
            // 1) Vector3: struct ama alanları yazılabilir (mutable); kopyalar bağımsızdır.
            Vector3 v1 = new Vector3(1, 2, 3);
            Vector3 v2 = v1;  // kopya
            v2.x = 99;
            Debug.Log($"Vector3 v1.x: {v1.x} (1) | v2.x: {v2.x} (99)");
            Debug.Log("Vector3: mutable struct (alan yazılabilir), fakat KOPYALAR bağımsızdır.");

            // 2) Transform: class ⇒ aynı referans; yerinde değişir.
            Transform t1 = this.transform;
            Transform t2 = t1; // aynı referans
            Vector3 before = t1.position;
            t2.position = t2.position + Vector3.one * 0.25f;
            Debug.Log($"Transform position before: {before} -> after: {t1.position}");
            Debug.Log("Transform: mutable reference type ⇒ aynı nesne yerinde değişti.");
        }

        #endregion


        #region Nested Types (örnekler)

        // Nested tipleri inspector’da göstermek için:
        [SerializeField] private Stats _stats = new Stats
        {
            hp = 50, attack = 10, range = 3f
        };
        private State _state = State.Idle;
        private int _hp = 100;                 // Outer privates: nested class buraya erişebilir (owner referansı ile)
        private static int _attackCount = 0;   // Outer private static: nested class buraya da erişebilir

        [System.Serializable]
        public struct Stats // Nested VALUE type (Inspector'da düzenlenebilir)
        {
            public int hp;
            public int attack;
            public float range;
        }

        private enum State // Nested enum: sadece bu sınıfa ait durumlar
        {
            Idle,
            Chase,
            Attack
        }

        // Nested helper: Outer'ın privates'ına ulaşmak için bir Outer referansı ister
        private sealed class AttackTimer
        {
            private readonly ValueReferenceTestController _owner;
            private float _next;

            public AttackTimer(ValueReferenceTestController owner) => _owner = owner;

            public bool IsReady()
            {
                if (Time.time >= _next)
                {
                    _next = Time.time + 0.5f;
                    _attackCount++; // Outer'ın private static alanı
                    return true;
                }
                return false;
            }

            public void PerformAttackIfReady()
            {
                if (!IsReady()) return;

                // Outer'ın private alanına erişim (owner üzerinden)
                int damage = Mathf.Max(1, _owner._stats.attack);
                int before = _owner._hp;
                _owner._hp = Mathf.Max(0, _owner._hp - damage);

                Debug.Log($"[AttackTimer] Attack#{_attackCount}: HP {before} -> {_owner._hp} (damage:{damage})");
            }
        }

        [Button(ButtonSizes.Medium)]
        [FoldoutGroup("Nested Types")]
        [BoxGroup("Nested Types/Demo")]
        public void NestedStatsAndEnumDemo()
        {
            _state = State.Chase;

            Debug.Log($"[Nested] Enemy Stats => HP:{_stats.hp}, ATK:{_stats.attack}, RNG:{_stats.range}");
            Debug.Log($"[Nested] State: {_state} (yalnız bu sınıfa ait enum)");
            Debug.Log("Nested struct & enum: ilgili tipleri sınıf içinde tutarak kapsülleme ve düzen sağlanır.");
        }

        [Button(ButtonSizes.Medium)]
        [FoldoutGroup("Nested Types")]
        [BoxGroup("Nested Types/Demo")]
        public void NestedPrivateHelperDemo()
        {
            var timer = new AttackTimer(this);
            // Birkaç deneme ile zaman aralığını göster:
            timer.PerformAttackIfReady(); // muhtemelen hazır (ilk çağrı)
            timer.PerformAttackIfReady(); // 0.5s geçmediyse çalışmaz
            // Demo amaçlı beklemeden ikinci kere zorlamak yerine mesaj veriyoruz:
            Debug.Log("0.5 saniye sonra tekrar tıklarsan yeni saldırı gerçekleşir.");
        }

        // Static nested ile sabitleri gruplayıp düzenleyebilirsin:
        public static class Game
        {
            public static class Tags
            {
                public const string Player = "Player";
                public const string Enemy  = "Enemy";
            }
            public static class Layers
            {
                public const int Player = 3;
                public const int Enemy  = 6;
            }
        }

        #endregion
    }
}
