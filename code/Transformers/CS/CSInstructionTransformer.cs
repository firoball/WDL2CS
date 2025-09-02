using System;
using System.Collections.Generic;
using System.Text;

namespace WDL2CS.Transformers.CS
{
    class CSInstructionTransformer : InstructionTransformer
    {
        public override void Transform(object obj, Node command, List<Node> parameters)
        {
            StringBuilder sb = (StringBuilder)obj;
            string o = string.Empty;
            if (command.NodeType == NodeType.GotoLabel)
            {
                o = $"{command}";
            }
            else
            {
                try
                {
                    string scommand = command.Data.ToLower();
                    /* don't format any parameter of IFs, RULEs or GOTOs
                     * detect RULEs either by command or by assignment operator in expression
                     * IFs and RULEs already have their expression parameter formatted
                     * GOTO requires unformatted marker - ambiguous definition might result in wrong formatting otherwise
                     */

                    switch (scommand)
                    {
                        case "abs":
                            o = $"{parameters[0]} = {parameters[2]}({parameters[1]});";
                            break;

                        case "accel":
                            o = $"{parameters[0]}.Accel({parameters[1]});";
                            break;

                        case "add":
                            o = $"{parameters[0]} += {parameters[1]};";
                            break;

                        case "add_string":
                            o = $"{parameters[0]} += {parameters[1]};";
                            break;

                        case "addt":
                            o = $"{parameters[0]} += {parameters[1]} * {parameters[2]};";
                            break;

                        case "and":
                            o = $"{parameters[0]} &= {parameters[1]};";
                            break;

                        case "asin":
                            o = $"{parameters[0]} = {parameters[2]}({parameters[1]});";
                            break;

                        case "beep":
                            o = $"Diag.Beep();";
                            break;

                        case "branch":
                            o = $"Run({parameters[0]}); yield break;";
                            break;

                        case "break":
                            o = $"break;";
                            break;

                        case "call":
                            o = $"Run({parameters[0]});";
                            break;

                        case "do":
                            o = $"Run({parameters[0]});";
                            break;

                        case "drop":
                            o = $"{parameters[0]}.Drop();";
                            break;

                        case "else": //"else" instruction is distinguished from "Else" command via case sensitivity //I think I just broke that?
                            o = $"else";
                            break;

                        case "end":
                            o = $"yield break;";
                            break;

                        case "exclusive":
                            o = $"My.Exclusive();";
                            break;

                        case "exec_rules":
                            o = $"Run({parameters[0]});";
                            break;

                        case "exit":
                            if (parameters.Count > 0)
                                o = $"Environment.Exit({parameters[0]}); yield break;";
                            else
                                o = $"Environment.Exit(); yield break;";
                            break;

                        case "explode":
                            o = $"{parameters[0]}.Explode();";
                            break;

                        case "fade_pal":
                            o = $"{parameters[0]}.Fade({parameters[1]});";
                            break;

                        case "find":
                            o = $"{parameters[0]}.Find({parameters[1]});";
                            break;

                        case "freeze":
                            o = $"Environment.Freeze({parameters[0]}, {parameters[1]});";
                            break;

                        case "getmidi":
                            o = $"{parameters[1]} = Media.Getmidi({parameters[0]});";
                            break;

                        case "goto":
                            o = $"goto {parameters[0]};";
                            break;

                        case "if":
                            o = $"if {parameters[0]}";
                            break;

                        case "if_above":
                            o = $"if ({parameters[0]} > {parameters[1]})";
                            o += TryAddGoto(parameters);
                            break;

                        case "if_below":
                            o = $"if ({parameters[0]} < {parameters[1]})";
                            o += TryAddGoto(parameters);
                            break;

                        case "if_equal":
                            //special treatment for actor target comparison
                            //if (m_parameters[0].EndsWith(".Target"))
                            if (Util.HasProperty(parameters[0], "target"))
                                o = $"if ({parameters[0]}.Equals({parameters[1]}))";
                            else
                                o = $"if ({parameters[0]} == {parameters[1]})";

                            o += TryAddGoto(parameters);
                            break;

                        case "if_nequal":
                            //special treatment for actor target comparison
                            //if (m_parameters[0].EndsWith(".Target"))
                            if (Util.HasProperty(parameters[0], "target"))
                                o = $"if (!{parameters[0]}.Equals({parameters[1]}))";
                            else
                                o = $"if ({parameters[0]} != {parameters[1]})";

                            o += TryAddGoto(parameters);
                            break;

                        case "if_max":
                            o = $"if ({parameters[0]} >= {parameters[1]})";
                            o += TryAddGoto(parameters);
                            break;

                        case "if_min":
                            o = $"if ({parameters[0]} <= {parameters[1]})";
                            o += TryAddGoto(parameters);
                            break;

                        case "inkey":
                            o = $"yield return Scheduler.Inkey({parameters[0]}); {parameters[0]} = Scheduler.Inkey_string;";
                            break;

                        case "inport":
                            o = $"{parameters[0]} = Environment.Inport({parameters[1]});";
                            break;

                        case "level":
                            o = $"Environment.Level({parameters[0]});";
                            break;

                        case "lift":
                            o = $"{parameters[0]}.Lift({parameters[1]});";
                            break;

                        case "load":
                            o = $"Environment.Load({parameters[0]}, {parameters[1]}, false);";
                            break;

                        case "load_info":
                            o = $"Environment.Load({parameters[0]}, {parameters[1]}, true);";
                            break;

                        case "locate":
                            if (parameters.Count > 0)
                                o = $"{parameters[0]}.Locate();";
                            else
                                o = $"Player.Locate();";
                            break;

                        case "map":
                            if (parameters.Count > 0)
                                o = $"Environment.Map({parameters[0]});";
                            else
                                o = $"Environment.Map();";
                            break;

                        case "midi_com":
                            o = $"Media.Midi_com({parameters[0]}, {parameters[1]}, {parameters[2]});";
                            break;

                        case "next_my": //TODO: this needs to be updated
                            o = $"Globals.My = Globals.My.Next();";
                            break;

                        case "next_my_there": //TODO: this needs to be updated
                            o = $"Globals.My = Globals.My.Next_there();";
                            break;

                        case "next_there": //TODO: this needs to be updated
                            o = $"Globals.There = Globals.There.Next();";
                            break;

                        case "nop":
                            o = string.Empty;
                            break;

                        case "outport":
                            o = $"Environment.Outport({parameters[0]} , {parameters[1]});";
                            break;

                        case "place":
                            o = $"{parameters[0]}.Place();";
                            break;

                        case "play_cd":
                            o = $"Media.Play_cd({parameters[0]}, {parameters[1]});";
                            break;

                        case "play_demo":
                            o = $"Environment.Play_demo({parameters[0]}, {parameters[1]}, true);";
                            break;

                        case "play_flic":
                            o = $"Media.Play_flic({parameters[0]});";
                            break;

                        case "play_flicfile":
                            o = $"Media.Play_flic({parameters[0]});";
                            break;

                        case "play_song":
                            o = $"Media.Play_song({parameters[0]}, {parameters[1]}, true);";
                            break;

                        case "play_song_once":
                            o = $"Media.Play_song({parameters[0]}, {parameters[1]}, false);";
                            break;

                        case "play_sound":
                            if (parameters.Count > 2)
                            {
                                //check for specific properties containing an object, e.g. <object>.Genius - checking for dot may lead to false positives
                                if (Registry.Is("thing", parameters[2].Data) || Registry.Is("actor", parameters[2].Data) || Registry.Is("wall", parameters[2].Data) ||
                                    parameters[2].NodeType == NodeType.Global || Util.HasProperty(parameters[2], "genius") ||
                                    string.Equals(parameters[2].Data, "my", StringComparison.OrdinalIgnoreCase)
                                    )
                                    o = $"{parameters[2]}.Play_sound({parameters[0]}, {parameters[1]});";
                                else
                                    o = $"Media.Play_sound({parameters[0]}, {parameters[1]}, {parameters[2]});";
                            }
                            else
                            {
                                o = $"Media.Play_sound({parameters[0]}, {parameters[1]});";
                            }
                            break;

                        case "play_soundfile":
                            if (parameters.Count > 2)
                                o = $"Media.Play_sound({parameters[0]}, {parameters[1]}, {parameters[2]});";
                            else
                                o = $"Media.Play_sound({parameters[0]}, {parameters[1]});";
                            break;

                        case "print":
                        case "print_value":
                        case "print_string":
                            o = $"Diag.Print({parameters[0]});";
                            break;

                        case "printfile":
                            o = $"Diag.Printfile({parameters[0]}, {parameters[1]});";
                            break;

                        case "push":
                            o = $"Player.Push({parameters[0]});";
                            break;

                        case "randomize":
                            o = $"{parameters[0]}.Randomize({parameters[1]});";
                            break;

                        case "rotate":
                            o = $"{parameters[0]}.Rotate({parameters[1]});";
                            break;

                        case "rule":
                            o = $"{parameters[0]};";
                            break;

                        case "save":
                            o = $"Environment.Save({parameters[0]}, {parameters[1]}, false);";
                            break;

                        case "save_demo":
                            o = $"Environment.Save({parameters[0]}, {parameters[1]});";
                            break;

                        case "save_info":
                            o = $"Environment.Save({parameters[0]}, {parameters[1]}, true);";
                            break;

                        case "scan":
                            o = $"Environment.Scan({parameters[0]});";
                            break;

                        case "screenshot":
                            o = $"Environment.Screenshot({parameters[0]}, {parameters[1]});";
                            break;

                        case "set":
                            o = $"{parameters[0]} = {parameters[1]};";
                            break;

                        case "set_all":
                            //split property from object for iteration through all instances
                            if (Util.SplitProperty(parameters[0], out Node target, out Node property))
                            {
                                o = $"foreach (var instance in {target}) instance{property} = {parameters[1]};";
                            }
                            else
                            {
                                //special case: no property has been given
                                o = $"{parameters[0]} = {parameters[1]};";
                                Console.WriteLine($"(W) CSINSTRUCTIONTRANSFORMER malformed SET_ALL instruction replaced with: {o}");
                            }
                            break;

                        case "set_info":
                            o = $"{parameters[0]} = {parameters[1]}.ToString();";
                            break;

                        case "set_skill":
                            o = $"{parameters[0]} = Convert.ToDouble({parameters[1]});";
                            break;

                        case "set_string":
                            o = $"{parameters[0]} = string.Copy({parameters[1]});";
                            break;

                        case "setmidi":
                            o = $"Media.Setmidi({parameters[0]}, {parameters[1]});";
                            break;

                        case "shake":
                            o = $"{parameters[0]}.Shake();";
                            break;

                        case "shift":
                            o = $"{parameters[0]}.Shift({parameters[1]}, {parameters[2]});";
                            break;

                        case "shoot":
                            if (parameters.Count > 0)
                                o = $"{parameters[0]}.Shoot();";
                            else
                                o = $"Player.Shoot();";
                            break;

                        case "skip":
                            o = $"goto {parameters[1]};";
                            break;

                        case "sin":
                            o = $"{parameters[0]} = {parameters[2]}({parameters[1]});";
                            break;

                        case "sqrt":
                            o = $"{parameters[0]} = {parameters[2]}({parameters[1]});";
                            break;

                        case "stop_demo":
                            o = $"Environment.Stop_demo();";
                            break;

                        case "stop_flic":
                            o = $"Media.Stop_flic();";
                            break;

                        case "stop_sound":
                            o = $"Media.Stop_sound();";
                            break;

                        case "sub":
                            o = $"{parameters[0]} -= {parameters[1]};";
                            break;

                        case "subt":
                            o = $"{parameters[0]} -= {parameters[1]} * {parameters[2]};";
                            break;

                        case "tilt":
                            o = $"{parameters[0]}.Tilt({parameters[1]});";
                            break;

                        case "to_string":
                            o = $"{parameters[0]} = {parameters[1]}.ToString();";
                            break;

                        case "wait":
                            o = $"yield return Wait({parameters[0]});";
                            break;

                        case "waitt":
                        case "wait_ticks": //undocumented
                            o = $"yield return Waitt({parameters[0]});";
                            break;

                        case "while":
                            if (parameters.Count < 2) //while(1) patch -> C# needs while(true)
                                o = $"while {parameters[0]}";
                            else
                                o = $"while {parameters[1]}";
                            break;

                        default:
                            //TODO: this part needs review
                            if (parameters.Count == 0)
                            {
                                o = $"{command}";
                            }
                            else
                            {
                                string pars = string.Empty;
                                foreach (Node p in parameters)
                                {
                                    pars += p + " ";
                                }
                                o = /*"//TODO: " +*/ command + " " + pars;
                            }
                            break;
                    }
                }
                catch
                {
                    Console.WriteLine("(E) CSINSTRUCTIONTRANSFORMER discard malformed instruction: " + command + " " + string.Join(", ", parameters));
                }
            }
            sb.Append(o);
        }

        private string TryAddGoto(List<Node> parameters)
        {
            if (parameters.Count > 2 && parameters[2].NodeType != NodeType.Number) //third parameter - if not numeric - is goto label
                return $" {{ goto {parameters[2]}; }}";
            else
                return string.Empty;
        }
    }
}
