using UnityEngine;
using Random = UnityEngine.Random;

// using Random = System.Random;

public class SoundMan : MonoBehaviour
{
    [Header ("Singleton Variables")]
    public static SoundMan sm;

    public AudioSource playerAudioSource;
    public AudioClip[] playerLaughClips;
    public AudioClip[] playerHitClips;
    public AudioClip[] playerDeathClips;
    public AudioClip[] playerVocalClips;
    public AudioClip[] playerRatedRClips;
    
    public AudioSource enemyAudioSource;
    public AudioClip[] enemyAttackClips;
    public AudioClip[] enemyHitClips;
    
    public AudioSource sfxAudioSource;
    public AudioClip[] bobaClips;
    public AudioClip splatClip;
    
    // public AudioSource menuMusic;
    public AudioSource gameMusic;
    
    private void Awake()
    {
        sm = this;
    }

    private void Start()
    {
        gameMusic.Play();
    }
    
    private void Update()
    {
        // if (Input.GetKeyDown(KeyCode.Q))
        // {
        //     EnemyAttack();
        // }
        // if (Input.GetKeyDown(KeyCode.W))
        // {
        //     EnemyHit();
        // }
        // if (Input.GetKeyDown(KeyCode.E))
        // {
        //     PlayerLaugh();
        // }
        // if (Input.GetKeyDown(KeyCode.R))
        // {
        //     PlayerVocal();
        // }
        // if (Input.GetKeyDown(KeyCode.T))
        // {
        //     PlayerDeath();
        // }
        // if (Input.GetKeyDown(KeyCode.Y))
        // {
        //     PlayerHit();
        // }
        // if (Input.GetKeyDown(KeyCode.U))
        // {
        //     PlayerRatedR();
        // }
        // if (Input.GetKeyDown(KeyCode.I))
        // {
        //     PlayerLaugh();
        // }
        //
        // if (Input.GetKeyDown(KeyCode.O))
        // {
        //     BobaShoot();
        // }
    }

    public void BobaShoot()
    {
        sfxAudioSource.clip = bobaClips[Random.Range(0, bobaClips.Length)];
        sfxAudioSource.PlayOneShot(sfxAudioSource.clip);
    }

    public void BobaHit()
    {
        sfxAudioSource.PlayOneShot(splatClip);
    }
    
    /*
     * ENEMY SOUNDS
     */
    
    public void EnemyAttack()
    {
        enemyAudioSource.clip = enemyAttackClips[Random.Range(0, enemyAttackClips.Length)];
        enemyAudioSource.PlayOneShot(enemyAudioSource.clip);
    }
    
    public void EnemyHit()
    {
        enemyAudioSource.clip = enemyHitClips[Random.Range(0, enemyHitClips.Length)];
        enemyAudioSource.PlayOneShot(enemyAudioSource.clip);
    } 
    
    /*
     * PLAYER SOUNDS
     */
    
    public void PlayerLaugh()
    {
        playerAudioSource.clip = playerLaughClips[Random.Range(0, playerLaughClips.Length)];
        playerAudioSource.Play();
    }

    public void PlayerVocal()
    {
        playerAudioSource.clip = playerVocalClips[Random.Range(0, playerVocalClips.Length)];
        playerAudioSource.Play();
    }
    
    public void PlayerHit()
    {
        playerAudioSource.clip = playerHitClips[Random.Range(0, playerHitClips.Length)];
        playerAudioSource.Play();
    }

    public void PlayerDeath()
    {
        playerAudioSource.clip = playerDeathClips[Random.Range(0, playerDeathClips.Length)];
        playerAudioSource.Play();
    }

    public void PlayerRatedR()
    {
        playerAudioSource.clip = playerRatedRClips[Random.Range(0, playerRatedRClips.Length)];
        playerAudioSource.Play();
    }
} 
