using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace Scripts.Items
{
    [CreateAssetMenu(fileName = "Recipe", menuName = "Data/Recipe")]
    public class Recipe : ScriptableObject
    {
        [SerializeField] private Item _output;
        [SerializeField] private Item[] _input;

        public Item Output => _output;
        public Item[] Input => _input;

        public void Set(Item output, Item[] input)
        {
            _output = output;
            _input = input;
        }

        public bool Contains(string value)
        {
            if (_output.Name.ToLower().Contains(value))
                return true;
            foreach (var item in _input)
                if (item.Name.ToLower().Contains(value))
                    return true;
            return false;
        }

        public bool GetRecipe(string name1, string name2, string name3, out bool use1, out bool use2, out bool use3)
        {
            //Debug.Log(name3 != string.Empty ? "2" : "3" + "x" + _input.Length.ToString());
            if (name3 != string.Empty)
            {
                if (_input.Length == 3)
                {
                    /*Debug.Log("////////////////");
                    Debug.Log(_output.Name);
                    Debug.Log(_input[0].Name);
                    Debug.Log(_input[1].Name);
                    Debug.Log(_input[2].Name);
                    Debug.Log(name1);
                    Debug.Log(name2);
                    Debug.Log(name3);*/
                    if ((name1 == _input[0].Name && name2 == _input[1].Name && name3 == _input[2].Name) ||
                        (name1 == _input[0].Name && name3 == _input[1].Name && name2 == _input[2].Name) ||
                        (name2 == _input[0].Name && name1 == _input[1].Name && name3 == _input[2].Name) ||
                        (name2 == _input[0].Name && name3 == _input[1].Name && name1 == _input[2].Name) ||
                        (name3 == _input[0].Name && name1 == _input[1].Name && name2 == _input[2].Name) ||
                        (name3 == _input[0].Name && name2 == _input[1].Name && name1 == _input[2].Name))
                    {
                        use1 = true;
                        use2 = true;
                        use3 = true;
                        return true;
                    }
                }
                else
                {
                    if ((name1 == _input[0].Name && name2 == _input[1].Name) || (name2 == _input[0].Name && name1 == _input[1].Name))
                    {
                        use1 = true;
                        use2 = true;
                        use3 = false;
                        return true;
                    }
                    if ((name1 == _input[0].Name && name3 == _input[1].Name) || (name3 == _input[0].Name && name1 == _input[1].Name))
                    {
                        use1 = true;
                        use2 = false;
                        use3 = true;
                        return true;
                    }
                    if ((name2 == _input[0].Name && name3 == _input[1].Name) || (name3 == _input[0].Name && name2 == _input[1].Name))
                    {
                        use1 = false;
                        use2 = true;
                        use3 = true;
                        return true;
                    }
                }
            }
            else if (_input.Length == 2)
            {
                if ((name1 == _input[0].Name && name2 == _input[1].Name) || (name2 == _input[0].Name && name1 == _input[1].Name))
                {
                    use1 = true;
                    use2 = true;
                    use3 = false;
                    return true;
                }
            }
            use1 = false;
            use2 = false;
            use3 = false;
            return false;
        }
    }
}