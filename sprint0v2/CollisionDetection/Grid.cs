using Microsoft.Xna.Framework;
using sprint0v2.Entities;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.InteropServices;

namespace sprint0v2
{
    public class Grid
    {
        private readonly Dictionary<Vector2, HashSet<Entity>> cells;
        private readonly HashSet<Entity> allEntities;
        private readonly HashSet<Entity> entitiesToUpdate;
        public static Grid Instance { get; set; }

        public Grid()
        {
            Instance = this;
            cells = new Dictionary<Vector2, HashSet<Entity>>();
            allEntities = new HashSet<Entity>();
            entitiesToUpdate = new HashSet<Entity>();

        }
        public void Clear()
        {
            // Remove all entities
            allEntities.Clear();

            // Clear the cells dictionary
            cells.Clear();

            // Clear the entitiesToUpdate
            entitiesToUpdate.Clear();
        }
        private Vector2 GetCellIndex(Vector2 position)
        {
            // Bug is causing large cell size to be neccessary.
            int cellSize = 500;

            int x = (int)Math.Floor(position.X / cellSize);
            int y = (int)Math.Floor(position.Y / cellSize);

            return new Vector2(x, y);
        }
        public void MarkForUpdate(Entity entity)
        {
            if (entity != null && allEntities.Contains(entity))
            {
                entitiesToUpdate.Add(entity);
            }
        }


        private IEnumerable<Vector2> GetEntityCells(Entity entity)
        {
            Vector2 min = GetCellIndex(new Vector2(entity.BoundingBox.Left, entity.BoundingBox.Top));
            Vector2 max = GetCellIndex(new Vector2(entity.BoundingBox.Right, entity.BoundingBox.Bottom));

            for (int x = (int)min.X; x <= max.X; x++)
            {
                for (int y = (int)min.Y; y <= max.Y; y++)
                {
                    yield return new Vector2(x, y);
                }
            }
        }
        public List<Entity> GetAllEntities()
        {
            return allEntities.ToList();
        }

        public void AddEntity(Entity entity)
        {
            foreach (Vector2 cellIndex in GetEntityCells(entity))
            {
                if (!cells.ContainsKey(cellIndex))
                {
                    cells[cellIndex] = new HashSet<Entity>();
                }

                cells[cellIndex].Add(entity);
            }

            allEntities.Add(entity);
        }

        public void RemoveEntity(Entity entity)
        {
            if (entity == null)
            {
                return; // Do nothing if the entity is null
            }

            foreach (Vector2 cellIndex in GetEntityCells(entity))
            {
                if (cells.ContainsKey(cellIndex))
                {
                    cells[cellIndex]?.Remove(entity);
                }
            }

            allEntities.Remove(entity);
        }
        public HashSet<Entity> GetEntitiesInCell(Vector2 cellIndex)
        {
            if (cells.TryGetValue(cellIndex, out HashSet<Entity> cell))
            {
                return cell;
            }

            return new HashSet<Entity>();
        }

        public void Update()
        {

            // Create a temporary list to store colliding entity pairs
            List<(Entity, Entity)> collidingEntityPairs = new List<(Entity, Entity)>();

            // Check for collisions and add colliding entity pairs to the temporary list
            foreach (Entity entity in GetAllEntities())
            {
                entity.IsColliding = false;
                foreach (Vector2 cellIndex in GetEntityCells(entity))
                {
                    HashSet<Entity> cell = GetEntitiesInCell(cellIndex);

                    foreach (Entity otherEntity in cell)
                    {
                        if (otherEntity != entity && entity.AABB.Intersects(otherEntity.AABB))
                        {
                            collidingEntityPairs.Add((entity, otherEntity));
                        }
                    }
                }
            }

            // Update collision status and handle OnCollision events for colliding entity pairs
            foreach ((Entity entity1, Entity entity2) in collidingEntityPairs)
            {
                entity1.IsColliding = true;
                entity2.IsColliding = true;
                entity1.OnCollision(entity2);
                entity2.OnCollision(entity1);
            }
        }


        public void AddEntities(List<Entity> entities)
        {
            foreach (Entity entity in entities)
            {
                AddEntity(entity);
            }
        }

        public void SnapshotRepopulation(List<Entity> GridSnapshot) {
            allEntities.Clear();
            AddEntities(GridSnapshot);
        }
    }
}