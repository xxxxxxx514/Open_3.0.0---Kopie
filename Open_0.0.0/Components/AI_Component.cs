using Open_0._0._0.Actors;
using Open_0._0._0.Utils;
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

        public AI_Component(Actor character)
        {
            Character = character;
        }
        private float _timer = 0;
        public void Tick(float dt)
        {
            if (_timer > 0)
                _timer -= dt;
        }
        public void Update(float deltatime, Map map, Player player)
        {
            Movement(deltatime, map, player);
            Character.Movement_Component.UpdatePosition();
        }

        private void Movement(float deltatime, Map map, Player player)
        {
            DetectPlayer(player.Position_Component.PixelPosition);

            if (_enemyBehavior == EnemyBehavior.Patrolling)
                FollowRoute(map, deltatime);
            else if (_enemyBehavior == EnemyBehavior.Aggro)
                FollowPlayer(player, map, deltatime);
        }

        private void CreateRoute()
        {
            Vector2f pointA = Character.Position_Component.PixelPosition;
            Vector2f pointB = new Vector2f(
                _random.Next(50, Map.MAXWIDTHPIXEL - 50),
                _random.Next(50, Map.MAXHEIGHTPIXEL - 50)
            );
            _destination = pointB - pointA;
            _hasReachedDestination = false;
        }

        private Vector2f GetRouteDirection()
        {
            return MathHelper.Normalize(_destination);
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

                Character.Movement_Component.TryMove(movement, map, Enemy.ENEMY_SIZE);
            }
        }

        private void HasReachedDestination()
        {
            float currentX = Character.Position_Component.PixelPosition.X;
            float currentY = Character.Position_Component.PixelPosition.Y;

            float minX = _destination.X - _tolerance;
            float maxX = _destination.X + _tolerance;
            float minY = _destination.Y - _tolerance;
            float maxY = _destination.Y + _tolerance;

            _hasReachedDestination = currentX >= minX && currentX <= maxX &&
                                      currentY >= minY && currentY <= maxY;
        }

        private void FollowPlayer(Player player, Map map, float deltatime)
        {
            Vector2f direction = GetDirectionToPlayer();
            Vector2f movement = new Vector2f(
                direction.X * deltatime * _moveSpeed,
                direction.Y * deltatime * _moveSpeed
            );

            Character.Movement_Component.TryMove(movement, map, Enemy.ENEMY_SIZE);

            if (Character.Collison2D.CollisionPlayerEnemy(
                player.Position_Component.PixelPosition,
                Character.Position_Component.PixelPosition,
                movement,
                Player.PLAYER_SIZE,
                Enemy.ENEMY_SIZE))
            {
                Character.Attack_Component.Attack(player);
            }
        }

        private void DetectPlayer(Vector2f playerPosition)
        {
            _playerPosition = playerPosition;
            float dx = _playerPosition.X - Character.Position_Component.PixelPosition.X;
            float dy = _playerPosition.Y - Character.Position_Component.PixelPosition.Y;
            float detectionRangeSquared = _detectionRange * _detectionRange;

            _enemyBehavior = dx * dx + dy * dy <= detectionRangeSquared
                ? EnemyBehavior.Aggro
                : EnemyBehavior.Patrolling;
        }

        private Vector2f GetDirectionToPlayer()
        {
            Vector2f difference = _playerPosition - Character.Position_Component.PixelPosition;
            return MathHelper.Normalize(difference);
        }
    }
}