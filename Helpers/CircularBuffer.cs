// <copyright file="CircularBuffer.cs" company="Ensage">
//    Copyright (c) 2017 Ensage.
// </copyright>

namespace Ensage.SDK.Helpers
{
    using System;
    using System.Collections;
    using System.Collections.Generic;

    public sealed class CircularBuffer<T> : IEnumerable<T>
    {
        private readonly T[] values;

        private int head;

        private int rear;

        public CircularBuffer(int max)
        {
            this.Size = max;
            this.Count = 0;
            this.head = 0;
            this.rear = 0;
            this.values = new T[this.Size];
        }

        public int Count { get; private set; }

        public int Size { get; }

        public T Dequeue()
        {
            this.EnsureQueueNotEmpty();

            var res = this.values[this.head];
            this.values[this.head] = default(T);
            this.head = Incr(this.head, this.Size);
            this.Count--;

            return res;
        }

        public void Enqueue(T obj)
        {
            this.values[this.rear] = obj;

            if (this.Count == this.Size)
            {
                this.head = Incr(this.head, this.Size);
            }

            this.rear = Incr(this.rear, this.Size);
            this.Count = Math.Min(this.Count + 1, this.Size);
        }

        public IEnumerator<T> GetEnumerator()
        {
            var index = this.head;
            for (var i = 0; i < this.Count; i++)
            {
                yield return this.values[index];
                index = Incr(index, this.Size);
            }
        }

        public T Peek()
        {
            this.EnsureQueueNotEmpty();
            return this.values[this.head];
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return this.GetEnumerator();
        }

        private static int Incr(int index, int size)
        {
            return (index + 1) % size;
        }

        private void EnsureQueueNotEmpty()
        {
            if (this.Count == 0)
            {
                throw new Exception("Empty buffer");
            }
        }
    }
}