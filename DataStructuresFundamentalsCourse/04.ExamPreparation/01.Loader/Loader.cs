namespace _01.Loader
{
    using _01.Loader.Interfaces;
    using _01.Loader.Models;
    using System;
    using System.Collections;
    using System.Collections.Generic;

    public class Loader : IBuffer
    {
        private List<IEntity> _entities;

        public Loader()
        {
            this._entities = new List<IEntity>();
        }
        public int EntitiesCount => this._entities.Count;

        public void Add(IEntity entity)
        {
            this._entities.Add(entity);
        }

        public void Clear()
        {
            this._entities.Clear();
        }

        public bool Contains(IEntity entity)
        {
            return this.GetById(entity.Id) != null;
        }

        public IEntity Extract(int id)
        {
            IEntity entity = this.GetById(id);

            if (entity != null)
            {
                this._entities.Remove(entity);
            }

            return entity;
        }

        public IEntity Find(IEntity entity)
        {
            return this.GetById(entity.Id);
        }

        public List<IEntity> GetAll()
        {
            return new List<IEntity>(this._entities);
        }

        public IEnumerator<IEntity> GetEnumerator()
        {
            return this._entities.GetEnumerator();
        }

        public void RemoveSold()
        {
            this._entities.RemoveAll(x => x.Status == BaseEntityStatus.Sold);
        }

        public void Replace(IEntity oldEntity, IEntity newEntity)
        {
            int entityIndex = this._entities.IndexOf(oldEntity);
            this.ValidateEntity(entityIndex);
            this._entities[entityIndex] = newEntity;
        }

        public List<IEntity> RetainAllFromTo(BaseEntityStatus lowerBound, BaseEntityStatus upperBound)
        {
            List<IEntity> result = new List<IEntity>();
            int lowerBoundAsNum = (int)lowerBound;
            int upperBoundAsNum = (int)upperBound;

            for (int i = 0; i < this.EntitiesCount; i++)
            {
                IEntity currentEntity = this._entities[i];
                int currentEntityStatusAsNum = (int)currentEntity.Status;

                if (currentEntityStatusAsNum >= lowerBoundAsNum && currentEntityStatusAsNum <= upperBoundAsNum)
                {
                    result.Add(currentEntity);
                }
            }

            return result;
        }

        public void Swap(IEntity first, IEntity second)
        {
            int firstEntityIndex = this._entities.IndexOf(first);
            this.ValidateEntity(firstEntityIndex);

            int secondEntityIndex = this._entities.IndexOf(second);
            this.ValidateEntity(secondEntityIndex);

            IEntity temp = this._entities[firstEntityIndex];
            this._entities[firstEntityIndex] = this._entities[secondEntityIndex];
            this._entities[secondEntityIndex] = temp;
        }

        public IEntity[] ToArray()
        {
            return this._entities.ToArray();
        }

        public void UpdateAll(BaseEntityStatus oldStatus, BaseEntityStatus newStatus)
        {
            for (int i = 0; i < this.EntitiesCount; i++)
            {
                IEntity currentEntity = this._entities[i];

                if (currentEntity.Status == oldStatus)
                {
                    currentEntity.Status = newStatus;
                }
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return this.GetEnumerator();
        }

        private IEntity GetById(int id)
        {
            for (int i = 0; i < this.EntitiesCount; i++)
            {
                IEntity current = this._entities[i];

                if (current.Id == id)
                {
                    return current;
                }
            }

            return null;
        }

        private void ValidateEntity(int index)
        {
            if (index == -1)
            {
                throw new InvalidOperationException("Entity not found");
            }
        }
    }
}
