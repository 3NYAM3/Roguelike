using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flash : MonoBehaviour
{
    [SerializeField] private Material whiteFlash;
    [SerializeField] private float restoreDefaultMatTime = .2f;

    private Material defaultMat;
    private SpriteRenderer spriteRenderer;
    private EnemyHealth enemyHealth;
    private bool startedExplosion=false;

    public bool StartedExplosion {
        set { startedExplosion =value; }
        private get {
            return startedExplosion;
        } 
    }

    private void Awake() {
        spriteRenderer = GetComponent<SpriteRenderer>();
        defaultMat = spriteRenderer.material;
    }
    public float GetRestoreMatTime() {
        return restoreDefaultMatTime;
    }

    public IEnumerator FlashRoutine() {
        if(!startedExplosion) {
            spriteRenderer.material = whiteFlash;
            yield return new WaitForSeconds(restoreDefaultMatTime);
            spriteRenderer.material = defaultMat;
        }
    }
}
