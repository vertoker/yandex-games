using System;
using Entities.Data;
using Input.Data;
using UnityEngine;

namespace Input.Providers
{
    public class WasdProvider : BaseProvider
    {
        [SerializeField] private WasdProviderPreset preset;

        private bool _combatUsed;

        private void StartDirection()
        {
            if (!_combatUsed)
                Controller.StartAction();
            _combatUsed = false;
            
            Controller.inputDirection = InputDirection.None;
            Controller.inputCombat = InputCombat.None;
        }
        private void StartCombat(InputCombat combat)
        {
            Controller.inputCombat = combat;
            _combatUsed = true;
            Controller.StartAction();
            
            Controller.inputCombat = InputCombat.None;
        }
        public override void InputUpdate()
        {
            switch (Controller.inputDirection)
            {
                case InputDirection.None:
                    if (preset.IsPressedDown(preset.Left))
                        Controller.inputDirection = InputDirection.Left;
                    else if (preset.IsPressedDown(preset.Right))
                        Controller.inputDirection = InputDirection.Right;
                    else if (preset.IsPressedDown(preset.Up))
                        Controller.inputDirection = InputDirection.Up;
                    else if (preset.IsPressedDown(preset.Down))
                        Controller.inputDirection = InputDirection.Down;
                    break;
                case InputDirection.Up:
                    if (preset.IsPressedDown(preset.Left))
                        Controller.inputDirection = InputDirection.Left;
                    else if (preset.IsPressedDown(preset.Right))
                        Controller.inputDirection = InputDirection.Right;
                    else if (preset.IsPressedUp(preset.Up))
                        StartDirection();
                    else if (preset.IsPressedDown(preset.Down))
                        Controller.inputDirection = InputDirection.Down;
                    break;
                case InputDirection.Down:
                    if (preset.IsPressedDown(preset.Left))
                        Controller.inputDirection = InputDirection.Left;
                    else if (preset.IsPressedDown(preset.Right))
                        Controller.inputDirection = InputDirection.Right;
                    else if (preset.IsPressedDown(preset.Up))
                        Controller.inputDirection = InputDirection.Up;
                    else if (preset.IsPressedUp(preset.Down))
                        StartDirection();
                    break;
                case InputDirection.Left:
                    if (preset.IsPressedUp(preset.Left))
                        StartDirection();
                    else if (preset.IsPressedDown(preset.Right))
                        Controller.inputDirection = InputDirection.Right;
                    else if (preset.IsPressedDown(preset.Up))
                        Controller.inputDirection = InputDirection.Up;
                    else if (preset.IsPressedDown(preset.Down))
                        Controller.inputDirection = InputDirection.Down;
                    break;
                case InputDirection.Right:
                    if (preset.IsPressedDown(preset.Left))
                        Controller.inputDirection = InputDirection.Left;
                    else if (preset.IsPressedUp(preset.Right))
                        StartDirection();
                    else if (preset.IsPressedDown(preset.Up))
                        Controller.inputDirection = InputDirection.Up;
                    else if (preset.IsPressedDown(preset.Down))
                        Controller.inputDirection = InputDirection.Down;
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
            
            if (preset.IsPressedDown(preset.Hand))
                StartCombat(InputCombat.Hand);
            else if (preset.IsPressedDown(preset.Leg))
                StartCombat(InputCombat.Leg);
            else if (preset.IsPressedDown(preset.Block))
                StartCombat(InputCombat.Block);
        }
    }
}