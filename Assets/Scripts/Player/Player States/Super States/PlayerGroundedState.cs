﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DeaLoux.Entity
{
    public abstract class PlayerGroundedState : PlayerState
    {
        public PlayerGroundedState(Player player, PlayerStateMachine stateMachine, EntityData data, PlayerData playerData, string animBoolName) : base(player, stateMachine, data, playerData, animBoolName)
        {
        }

        public override void Enter()
        {
            base.Enter();

            _player.JumpState.ResetAmount();
            _player.DashState.ResetDash();
            _player.InputHandler.TickDashInput();
        }

        public override void Exit()
        {
            base.Exit();
        }

        public override void LogicUpdate()
        {
            base.LogicUpdate();

            if (!_exitingState)
            {
                if (_jumpInput && _player.JumpState.CanJump())
                {
                    ChangeStateSH(_player.JumpState);
                }
                else if (_dashInput && _player.DashState.Dashable)
                {
                    ChangeStateSH(_player.DashState);
                }
                else if (!_grounded)
                {
                    ChangeStateSH(_player.AerialState);
                }
                else if (_primAtkInput)
                {
                    switch (_data.slot1.type)
                    {
                        case EquipmentType.FOUR_WAY:
                            if (_stateMachine.CurrState is PlayerMoveState)
                                ChangeStateSH(_player.PrimAtkMoveState);
                            else
                            {
                                ChangeStateSH(_player.PrimAtkIdle4State, true);
                            }
                            break;

                        case EquipmentType.EIGHT_WAY:
                            ChangeStateSH(_player.PrimAtkIdle8State, true);
                            break;
                    }
                }
                else if (_primAtkCharged)
                {
                    switch (_data.slot1.Ctype)
                    {
                        case EquipmentType.FOUR_WAY:
                            _player.AtkChargedState.SetAnim("primAtkIdle4Charged");
                            break;

                        case EquipmentType.EIGHT_WAY:
                            _player.AtkChargedState.SetAnim("primAtkIdle8Charged");
                            break;
                    }
                    ChangeStateSH(_player.AtkChargedState, true);
                }

                else if (_secAtkInput)
                {
                    switch (_data.slot2.type)
                    {
                        case EquipmentType.FOUR_WAY:
                            if (_stateMachine.CurrState is PlayerMoveState)
                                ChangeStateSH(_player.SecAtkMoveState);
                            else
                            {
                                ChangeStateSH(_player.SecAtkIdle4State, true);
                            }

                            break;

                        case EquipmentType.EIGHT_WAY:
                            ChangeStateSH(_player.SecAtkIdle8State, true);
                            break;
                    }
                }
                else if (_secAtkCharged)
                {
                    switch (_data.slot2.Ctype)
                    {
                        case EquipmentType.FOUR_WAY:
                            _player.AtkChargedState.SetAnim("secAtkIdle4Charged");
                            break;

                        case EquipmentType.EIGHT_WAY:
                            _player.AtkChargedState.SetAnim("secAtkIdle8Charged");
                            break;
                    }
                    ChangeStateSH(_player.AtkChargedState, true);
                }
            }
        }
    }
}