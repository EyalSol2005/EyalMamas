using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Part3
{
    public class LinkedList
    {
        private Node Head { get; set; }
        private Node Tail { get; set; }
        private Node MaxNode { get; set; }
        private Node MinNode { get; set; }

        /// <summary>
        /// The function adds a new node to the end
        /// </summary>
        /// <param name="num"></param>
        public void Append(int num)
        {
            Node newNode = new Node(num);

            updateMaxMinNodes(num);

            if (this.Head == null) // empty list
            {
                Tail = newNode;
                this.Head = Tail;
            }

            else
            {
                Tail.Next = newNode; // add to last node
                Tail = Tail.Next; // move tail to last node
            }
        }

       /// <summary>
       /// The function adds a node to the start
       /// </summary>
       /// <param name="num"></param>
        public void Prepend(int num)
        {
            Node newNode = new Node(num);

            updateMaxMinNodes(num);

            if (this.Head == null) // empty list
            {
                Tail = newNode;
                this.Head = Tail;
            }

            else
            {
                newNode.Next = this.Head; // add node new to start
                this.Head = newNode;
            }
        }

        /// <summary>
        /// The function deletes the last node
        /// </summary>
        /// <returns></returns>
        public int Pop()
        {
            Node second_to_tail = this.Head;
            int value = 0;

            if (this.Head == null)
            {
                return -999; // a symbol for error (can't pop from empty linked list)
            }

            else if (this.Head.Next == null) // linked list made of a single node
            {
                value = this.Head.Value;
                this.Head = null;
                return value;
            }

            while (second_to_tail.Next.Next != null) // looping until reaching the second to last node
            {
                second_to_tail = second_to_tail.Next;
            }

            value = second_to_tail.Next.Value;
            second_to_tail.Next = null; // removing the last node

            // Check if the deleted node was a max or min
            if (value == this.MaxNode.Value)
            {
                findUpdatedMax();
            }

            if (value == this.MinNode.Value)
            {
                findUpdatedMin();
            }
            return value;
        }

        /// <summary>
        /// The function deletes the first node
        /// </summary>
        /// <returns>Value of the first node</returns>
        public int Unqueue()
        {
            if (this.Head == null)
            {
                return -999; // a symbol for error (can't pop from empty linked list)
            }

            int value = this.Head.Value;
            this.Head = (this.Head.Next != null) ? this.Head.Next : null;

            // Check if the deleted node was a max or min
            if (value == this.MaxNode.Value)
            {
                findUpdatedMax();
            }

            if (value == this.MinNode.Value)
            {
                findUpdatedMin();
            }
            return value;
        }

        /// <summary>
        /// The function converts the linked list into an IEnumerable representation of it
        /// </summary>
        /// <returns>IEnumerable with the values of the linked list</returns>
        public IEnumerable<int> ToList()
        {
            Node current = this.Head;

            while (current != null)
            {
                int currentValue = current.Value;
                current = current.Next;
                yield return currentValue;

            }
        }

        /// <summary>
        /// The function checks if the tail node points to the head node (Circular linked list)
        /// </summary>
        /// <returns></returns>
        public bool IsCircular()
        {
            if (this.Head == null && Tail == null)
            {
                return true; // Consider emtpy list as circular
            }

            return Tail.Next == this.Head; // Checking if the last node's next is the first node
        }


        /// <summary>
        /// The function sorts the linked list
        /// </summary>
        public void Sort()
        {
            bool finishedSwaapping = false;
            Node current;

            if (this.Head == null) // empty list
                return;

            while (!finishedSwaapping) // swapping until all nodes are sorted
            {
                finishedSwaapping = true;
                current = this.Head;

                while (current.Next != null)
                {
                    if (current.Value < current.Next.Value) // current is less than the next, so need to swap
                    {
                        swap(current, current.Next);
                        finishedSwaapping = false; // means that a swap was made, so need to check again if all nodes are sorted
                    }
                    current = current.Next;
                }
            }
        }

        /// <summary>
        /// The function checks if after deleting a node, the max node changed, and updates accordingly
        /// </summary>
        private void findUpdatedMax()
        {
            Node current = this.Head;
            Node newMax = this.Head;

            while (current != null)
            {
                if (current.Value > newMax.Value)
                {
                    newMax = current;
                }
                current = current.Next;
            }
            this.MaxNode = newMax;
        }

        /// <summary>
        /// The function checks if after deleting a node, the min node changed, and updates accordingly
        /// </summary>
        private void findUpdatedMin()
        {
            Node current = this.Head;
            Node newMin = this.Head;

            while (current != null)
            {
                if (current.Value < newMin.Value)
                {
                    newMin = current;
                }
                current = current.Next;
            }
            this.MinNode = newMin;
        }

        /// <summary>
        /// The function swaps two nodes' values
        /// </summary>
        /// <param name="node1">First node</param>
        /// <param name="node2">Second node</param>
        private void swap(Node node1, Node node2)
        {
            int temp = node2.Value;
            node2.Value = node1.Value;
            node1.Value = temp;
        }

        public Node GetMaxNode()
        {
            return this.MaxNode;
        }

        public Node GetMinNode()
        {
            return this.MinNode;
        }

        /// <summary>
        /// The function updates the current min and max nodes
        /// </summary>
        /// <param name="num">New number to check (if min / max)</param>
        private void updateMaxMinNodes(int num)
        {
            Node newNode = new Node(num);

            if ((MaxNode == null) && (MinNode == null)) // empty list
            {
                MaxNode = newNode;
                MinNode = newNode;
            }

            else // check and update if it's min / max
            {
                if (num < MinNode.Value)
                {
                    MinNode = newNode;
                }
                else if (num > MaxNode.Value)
                {
                    MaxNode = newNode;
                }
            }
        }


        public override string ToString()
        {
            Node current = this.Head;

            string result = "";

            while (current != null) // looping through the linked list
            {
                result += current.Value + "->";
                current = current.Next;
            }
            result += "\n";

            return result;
        }
    }
}
