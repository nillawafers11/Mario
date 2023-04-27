using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Media;
using sprint0v2.Entities;
using sprint0v2.Entities.ConcreteBlockEntities;
using sprint0v2.Entities.ConcreteEnemyEntites;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Xml.Schema;

namespace sprint0v2.SoundManager
{
    public class SoundEventManager
    {
        private Song hurryUp;
        private Song theme;
        private Song star;
        private Song castle;

        private SoundEffect oneUp;
        private SoundEffect breakBlock;
        private SoundEffect blockBump;
        private SoundEffect coin;
        private SoundEffect fireball;
        private SoundEffect flagpole;
        private SoundEffect gameover;
        private SoundEffect smallJump;
        private SoundEffect superJump;
        private SoundEffect kick;
        private SoundEffect marioDie;
        private SoundEffect pause;
        private SoundEffect pipe;
        private SoundEffect powerup;
        private SoundEffect powerupAppears;
        private SoundEffect stageClear;
        private SoundEffect stomp;
        private SimpleSoundManager _soundManager;



        private Mario mario;

        public SoundEventManager(Game game)
        {
            //SoundEffect.MasterVolume = 0.3f;
            MediaPlayer.Volume = 0.3f;
            _soundManager = new SimpleSoundManager();
            theme = game.Content.Load<Song>("Songs/01-main-theme-overworld");
            hurryUp = game.Content.Load<Song>("Songs/18-hurry-overworld-");
            star = game.Content.Load<Song>("Songs/05-starman");
            castle = game.Content.Load<Song>("Songs/04-castle");

            oneUp = game.Content.Load<SoundEffect>("Songs/smb_1-up");
            breakBlock = game.Content.Load<SoundEffect>("Songs/smb_breakblock");
            blockBump = game.Content.Load<SoundEffect>("Songs/smb_bump");
            coin = game.Content.Load<SoundEffect>("Songs/smb_coin");

            fireball = game.Content.Load<SoundEffect>("Songs/smb_fireball");
            flagpole = game.Content.Load<SoundEffect>("Songs/smb_flagpole");
            gameover = game.Content.Load<SoundEffect>("Songs/smb_gameover");
            smallJump = game.Content.Load<SoundEffect>("Songs/smb_jumpsmall");
            superJump = game.Content.Load<SoundEffect>("Songs/smb_jump-super");
            kick = game.Content.Load<SoundEffect>("Songs/smb_kick");
            marioDie = game.Content.Load<SoundEffect>("Songs/smb_mariodie");
            pause = game.Content.Load<SoundEffect>("Songs/smb_pause");
            pipe = game.Content.Load<SoundEffect>("Songs/smb_pipe");
            powerup = game.Content.Load<SoundEffect>("Songs/smb_powerup");
            powerupAppears = game.Content.Load<SoundEffect>("Songs/smb_powerup_appears");
            stageClear = game.Content.Load<SoundEffect>("Songs/smb_stage_clear");
            stomp = game.Content.Load<SoundEffect>("Songs/smb_stomp");


        }

        public void PlayBackgroundSong(string path)
        {
            if (path.Contains("tilemap") || path.Contains("combined"))
            {
                MediaPlayer.Play(theme);
                MediaPlayer.IsRepeating = true;
            }
            else if (path.Contains("castleTilemap"))
            {
                MediaPlayer.Play(castle);
                MediaPlayer.IsRepeating = true;
            }
        }

        public void PlayerSubscribe(Mario mario)
        {
            this.mario = mario;
            mario.SmallJumpAction += Mario_SmallJumpAction;
            mario.SuperJumpAction += Mario_SuperJumpAction;
            mario.DeadAction += Mario_DeadAction;
            mario.FireballAction += Mario_FireballAction;
            mario.CoinsChanged += Mario_CoinAction;
            mario.PowerupAction += Mario_PowerupAction;
            mario.StageClearAction += Mario_StageClearAction;
            mario.StarmanBeginAction += Mario_StarmanBeginAction;

            mario.LivesChanged += Mario_OneUpAction;
            mario.CollisionHandler.StompAction += CollisionHandler_StompAction;
            mario.CollisionHandler.BumpAction += CollisionHandler_BumpAction;
            mario.CollisionHandler.BreakAction += CollisionHandler_BreakAction;
            mario.CollisionHandler.WarpPipeAction += CollisionHandler_WarpPipeAction;
            mario.CollisionHandler.KoopaKickAction += CollisionHandler_KoopaKickAction;
            mario.CollisionHandler.PowerupAppearsAction += CollisionHandler_PowerupAppearsAction;
            mario.CollisionHandler.FlagpoleAction += CollisionHandler_FlagpoleAction;
        }



        public void GameSubscribe(Game1 game)
        {
            game.GameOverAction += Game_GameOverAction;
            game.PauseAction += Game_PauseAction;
            game.HurryUpAction += Game_HurryUpAction;
        }

        private void Mario_OneUpAction(object sender, LivesChangedEventArgs e)
        {
            _soundManager.PlaySoundEffect(oneUp, 0.0075f);
        }

        private void Mario_PowerupAction()
        {
            _soundManager.PlaySoundEffect(powerup, 0.0075f);
        }

        private void Mario_CoinAction(object sender, CoinsChangedEventArgs e)
        {
            _soundManager.PlaySoundEffect(coin, 0.0075f);
        }

        private void Mario_FireballAction()
        {
            _soundManager.PlaySoundEffect(fireball, 0.0075f);
        }

        private void Mario_DeadAction()
        {
            MediaPlayer.Stop();
            _soundManager.PlaySoundEffect(marioDie, 0.0075f);
        }

        private void Mario_SuperJumpAction()
        {
            _soundManager.PlaySoundEffect(superJump, 0.0075f);
        }

        private void Mario_SmallJumpAction()
        {
            _soundManager.PlaySoundEffect(smallJump, 0.0075f);
        }

        private void Mario_StageClearAction()
        {
            MediaPlayer.Stop();
            _soundManager.PlaySoundEffect(stageClear, 0.0075f);
        }

        private void Mario_StarmanBeginAction()
        {
            MediaPlayer.Stop();
            MediaPlayer.Play(star);
        }

        private void CollisionHandler_BreakAction()
        {
            _soundManager.PlaySoundEffect(breakBlock, 0.0075f);
        }

        private void CollisionHandler_BumpAction()
        {
            _soundManager.PlaySoundEffect(blockBump, 0.0075f);
        }

        private void CollisionHandler_StompAction()
        {
            _soundManager.PlaySoundEffect(stomp, 0.0075f);
        }

        private void CollisionHandler_WarpPipeAction()
        {
            _soundManager.PlaySoundEffect(pipe, 0.0075f);
        }

        private void CollisionHandler_PowerupAppearsAction()
        {
            _soundManager.PlaySoundEffect(powerupAppears, 0.0075f);
        }

        private void CollisionHandler_KoopaKickAction()
        {
            _soundManager.PlaySoundEffect(kick, 0.0075f);
        }

        private void CollisionHandler_FlagpoleAction()
        {
            _soundManager.PlaySoundEffect(flagpole, 0.0075f);
        }

        private void Game_HurryUpAction()
        {
            MediaPlayer.Play(hurryUp);
        }

        private void Game_PauseAction()
        {
            _soundManager.PlaySoundEffect(pause, 0.0075f);
        }

        private void Game_GameOverAction()
        {
            _soundManager.PlaySoundEffect(gameover, 0.0075f);
        }
    }

    public class ManagedSoundEffectInstance : IDisposable
    {
        private SoundEffectInstance _instance;
        private bool _isDisposed;

        public ManagedSoundEffectInstance(SoundEffect soundEffect)
        {
            _instance = soundEffect.CreateInstance();
            _isDisposed = false;
        }

        public SoundEffectInstance Instance => _instance;

        public void Play(float volume)
        {
            if (_instance.State == SoundState.Playing)
            {
                return;
            }

            _instance.Volume = volume;
            _instance.Play();
        }

        public void Dispose()
        {
            if (!_isDisposed)
            {
                _instance.Dispose();
                _isDisposed = true;
            }
        }
    }


    public class SimpleSoundManager
    {
        private Dictionary<SoundEffect, Tuple<Queue<ManagedSoundEffectInstance>, Stopwatch>> _soundEffectInstances;
        private int _maxInstancesPerSound;
        private TimeSpan _minimumTimeBetweenInstances;

        public SimpleSoundManager(int maxInstancesPerSound = 5, int minimumMillisecondsBetweenInstances = 50)
        {
            _soundEffectInstances = new Dictionary<SoundEffect, Tuple<Queue<ManagedSoundEffectInstance>, Stopwatch>>();
            _maxInstancesPerSound = maxInstancesPerSound;
            _minimumTimeBetweenInstances = TimeSpan.FromMilliseconds(minimumMillisecondsBetweenInstances);
        }

        public void PlaySoundEffect(SoundEffect soundEffect, float volume)
        {
            if (!_soundEffectInstances.ContainsKey(soundEffect))
            {
                _soundEffectInstances[soundEffect] = Tuple.Create(new Queue<ManagedSoundEffectInstance>(), new Stopwatch());
            }

            var instancesAndTimer = _soundEffectInstances[soundEffect];
            var instances = instancesAndTimer.Item1;
            var timer = instancesAndTimer.Item2;

            if (timer.Elapsed < _minimumTimeBetweenInstances)
            {
                return;
            }

            while (instances.Count > 0 && instances.Peek().Instance.State == SoundState.Stopped)
            {
                instances.Dequeue().Dispose();
            }

            if (instances.Count >= _maxInstancesPerSound)
            {
                return;
            }

            var newInstance = new ManagedSoundEffectInstance(soundEffect);
            newInstance.Play(volume);
            instances.Enqueue(newInstance);
            timer.Restart();
        }
    }


}



