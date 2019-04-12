using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IDamageable {

    void TakeDamage(Object character);
    void TakePoisonDamage(Object character, int value);
    void TakeColdDamage(Object character, int value);
    void TakeFireDamage(Object character, int value);
    void TakeLightDamage(Object character, int value);
}
