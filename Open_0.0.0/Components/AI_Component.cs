using Open_0._0._0.Actors;
using Open_0._0._0.Core;
using Open_0._0._0.Enums;
using Open_0._0._0.Utils;
using Open_0._0._0.World;
using SFML.System;

namespace Open_0._0._0.Components
{
     class AI_Component:ITickable
    {
        public Actor Character;
        private Random _random = new Random();
        private float _moveSpeed = 70f;
        private float _patrollingSpeed = 55f;
        private EnemyBehavior _enemyBehavior = EnemyBehavior.Patrolling;
        private float _detectionRange = 280f;
        private Vector2f _destination;
        private bool _hasReachedDestination = true;
        private float _tolerance = 5f;
        private Vector2f _playerPosition;

        private float _attacktimer = 2f;
        private int _attackCooldown = 2;

        public AI_Component(Actor character)
        {
            Character = character;
        }
        public void Tick(float dt)
        {
            if (_attacktimer > 0)
                _attacktimer -= dt;
        }
        public void Update(float deltatime)
        {
            Tick(deltatime);
           
        }
        public void FixedUpdate(float fixedDelta)
        {
            Movement(fixedDelta);
            Character.Movement_Component.UpdatePosition();
        }

        private void Movement(float deltatime)
        {
            DetectPlayer(GameWorld.Player.Position_Component.PixelPosition);

            if (_enemyBehavior == EnemyBehavior.Patrolling)
                FollowRoute(GameWorld.CurrentMap, deltatime);
            else if (_enemyBehavior == EnemyBehavior.Aggro)
                FollowPlayer(GameWorld.Player, GameWorld.CurrentMap, deltatime);
        }

        private void CreateRoute()
        {
            // Zielpunkt direkt speichern, NICHT als Richtungsvektor
            _destination = new Vector2f(
                _random.Next(50, Map.MAXWIDTHPIXEL - 50),
                _random.Next(50, Map.MAXHEIGHTPIXEL - 50)
            );
            _hasReachedDestination = false;
        }

        private Vector2f GetRouteDirection()
        {
            // Richtung vom aktuellen Standort zum Zielpunkt
            Vector2f diff = _destination - Character.Position_Component.PixelPosition;
            return MathHelper.Normalize(diff);
        }

        private void FollowRoute(Map map, float deltatime)
        {
            if (_hasReachedDestination)
            {
                CreateRoute();
            }
            else
            {
                HasReachedDestination();
                Vector2f movement = new Vector2f(
                    GetRouteDirection().X * deltatime * _patrollingSpeed,
                    GetRouteDirection().Y * deltatime * _patrollingSpeed
                );

                Character.Movement_Component.TryMove(movement, Enemy.ENEMY_SIZE);
            }
        }

        private void HasReachedDestination()
        {
            float dx = _destination.X - Character.Position_Component.PixelPosition.X;
            float dy = _destination.Y - Character.Position_Component.PixelPosition.Y;
            _hasReachedDestination = (dx * dx + dy * dy) <= (_tolerance * _tolerance);
        }

        private void FollowPlayer(Player player, Map map, float deltatime)
        {
            Vector2f direction = GetDirectionToPlayer();
            Vector2f movement = direction * deltatime * _moveSpeed;

            if (Character.Collison2D.CollisionPlayerEnemy(
                player.Position_Component.PixelPosition,
                Character.Position_Component.PixelPosition,
                movement,
                Player.PLAYER_SIZE,
                Enemy.ENEMY_SIZE))
            {
                if (_attacktimer <= 0)
                {
                    Console.WriteLine(Character.Name + " hat player angegriffen");
                    Character.Attack_Component.Attack(player);
                    _attacktimer = _attackCooldown;
                }
            }


            // 1. Versuch: volle Bewegung
            if (Character.Movement_Component.TryMove(movement, Enemy.ENEMY_SIZE))
                return;

            // 2. Versuch: nur X
            Vector2f xMove = new Vector2f(movement.X, 0);
            if (Character.Movement_Component.TryMove(xMove, Enemy.ENEMY_SIZE))
                return;

            // 3. Versuch: nur Y
            Vector2f yMove = new Vector2f(0, movement.Y);
            Character.Movement_Component.TryMove(yMove, Enemy.ENEMY_SIZE);
            
            // Attack check bleibt wie gehabt
            
        }

        private void DetectPlayer(Vector2f playerPosition)
        {
            _playerPosition = playerPosition;
            float dx = _playerPosition.X - Character.Position_Component.PixelPosition.X;
            float dy = _playerPosition.Y - Character.Position_Component.PixelPosition.Y;
            float detectionRangeSquared = _detectionRange * _detectionRange;

            float distanceSquared = dx * dx + dy * dy;

            if (distanceSquared <= detectionRangeSquared)
            {
                _enemyBehavior = EnemyBehavior.Aggro;
            }
            else
            {
                _enemyBehavior = EnemyBehavior.Patrolling;
            }
        }


        private Vector2f GetDirectionToPlayer()
        {
            Vector2f difference = _playerPosition - Character.Position_Component.PixelPosition;
            return MathHelper.Normalize(difference);
        }
    }
}