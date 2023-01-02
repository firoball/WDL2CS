using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WDL2CS
{
    class Instruction
    {
        private static readonly string s_sepObj = "#[O]#";
        private static readonly string s_sepInst = "#[I]#";
        private static readonly string s_sepPar = "#[P]#";
        private string m_command;
        private bool m_count;
        private List<string> m_parameters;

        public string Command { get => m_command; }
        public bool Count { get => m_count; }
        public List<string> Parameters { get => m_parameters; }

        public Instruction()
        {
            m_command = string.Empty;
            m_count = false;
            m_parameters = new List<string>();
        }

        public Instruction(string command) : this(command, true) { }
        public Instruction(string command, bool count) : this(command, new List<string>(), count) { }
        public Instruction(string command, string parameter) : this(command, parameter, true) { }
        public Instruction(string command, string parameter, bool count) : this(command, new[] { parameter }.ToList(), count) { }
        public Instruction(string command, List<string> parameters) : this(command, parameters, true) { }

        public Instruction(string command, List<string> parameters, bool count) : this()
        {
            m_command = command;
            if (parameters != null)
                m_parameters.AddRange(parameters);
            m_count = count;
        }

        public string Serialize()
        {
            string s = s_sepObj + m_count.ToString() + s_sepInst + m_command;
            s += s_sepInst + string.Join(s_sepPar, m_parameters);
            return s;
        }

        public void Deserialize(string stream)
        {
            //kill any leading object seperator - it is used for serializing multiple instructions only
            string[] fragments = stream.Split(new[] { s_sepObj }, StringSplitOptions.RemoveEmptyEntries);

            fragments = fragments[0].Split(new[] { s_sepInst }, StringSplitOptions.None);
            m_count = Convert.ToBoolean(fragments[0]);
            m_command = fragments[1];
            if (!string.IsNullOrEmpty(fragments[2]))
            {
                m_parameters = fragments[2].Split(new[] { s_sepPar }, StringSplitOptions.None).ToList();
            }
        }

        public string Format()
        {
            string o = string.Empty;
            try
            {
                switch (m_command)
                {
                    case "Abs":
                        o = $"{m_parameters[0]} = {Formatter.FormatMath("Abs")}({m_parameters[1]});";
                        break;

                    case "Accel":
                        o = $"{m_parameters[0]}.Accel({m_parameters[1]});";
                        break;

                    case "Add":
                        o = $"{m_parameters[0]} += {m_parameters[1]};";
                        break;

                    case "Add_string":
                        o = $"{m_parameters[0]} += {m_parameters[1]};";
                        break;

                    case "Addt":
                        o = $"{m_parameters[0]} += {m_parameters[1]} * {Formatter.FormatSkill("Time_corr")};";
                        break;

                    case "And":
                        o = $"{m_parameters[0]} &= {m_parameters[1]};";
                        break;

                    case "Asin":
                        o = $"{m_parameters[0]} = {Formatter.FormatMath("Asin")}({m_parameters[1]});";
                        break;

                    case "Beep":
                        o = $"Diag.Beep();";
                        break;

                    case "Branch":
                        o = $"Scheduler.Run({m_parameters[0]}); yield return break;";
                        break;

                    case "Call":
                        o = $"Scheduler.Run({m_parameters[0]});";
                        break;

                    case "Do":
                        o = $"Scheduler.Run({m_parameters[0]});";
                        break;

                    case "Drop":
                        o = $"{m_parameters[0]}.Drop();";
                        break;

                    //TODO: distinguish else-command from else{}-condition properly
                    case "Else":
                        o = $"else";
                        break;

                    case "End":
                        o = $"yield return break;";
                        break;

                    case "Exclusive":
                        o = $"Globals.My.Exclusive();";
                        break;

                    case "Exec_rules":
                        o = $"Scheduler.Run({m_parameters[0]});";
                        break;

                    case "Exit":
                        if (m_parameters.Count > 0)
                            o = $"Environment.Exit({m_parameters[0]});";
                        else
                            o = $"Environment.Exit();";
                        break;

                    case "Explode":
                        o = $"{m_parameters[0]}.Explode();";
                        break;

                    case "Fade_pal":
                        o = $"{m_parameters[0]}.Fade({m_parameters[1]});";
                        break;

                    case "Freeze":
                        o = $"Environment.Freeze({m_parameters[0]}, {m_parameters[1]});";
                        break;

                    case "Getmidi":
                        o = $"{m_parameters[1]} = Media.Getmidi({m_parameters[0]});";
                        break;

                    case "Goto":
                        o = $"goto {m_parameters[0]};";
                        break;

                    case "if":
                        o = $"if {m_parameters[0]}";
                        break;

                    case "If_above":
                        o = $"if ({m_parameters[0]} > {m_parameters[1]})";
                        break;

                    case "If_below":
                        o = $"if ({m_parameters[0]} < {m_parameters[1]})";
                        break;

                    case "If_equal":
                        o = $"if ({m_parameters[0]} == {m_parameters[1]})";
                        break;

                    case "If_nequal":
                        o = $"if ({m_parameters[0]} != {m_parameters[1]})";
                        break;

                    case "If_max":
                        o = $"if ({m_parameters[0]} >= {m_parameters[0]}.Max)";
                        break;

                    case "If_min":
                        o = $"if ({m_parameters[0]} <= {m_parameters[0]}.Min)";
                        break;

                    case "Inkey":
                        o = $"yield return Scheduler.Inkey(out {m_parameters[0]});";
                        break;

                    case "Inport":
                        o = $"{m_parameters[0]} = Environment.Inport({m_parameters[1]});";
                        break;

                    case "Lift":
                        o = $"{m_parameters[0]}.Lift({m_parameters[1]});";
                        break;

                    case "Load":
                        o = $"Environment.Load({m_parameters[0]}, {m_parameters[1]}, false);";
                        break;

                    case "Load_info":
                        o = $"Environment.Load({m_parameters[0]}, {m_parameters[1]}, true);";
                        break;

                    case "Locate":
                        o = $"{m_parameters[0]}.Locate();";
                        break;

                    case "Map":
                        o = $"Environment.Map({m_parameters[0]});";
                        break;

                    case "Midi_com":
                        o = $"Media.Midi_com({m_parameters[0]}, {m_parameters[1]}, {m_parameters[2]});";
                        break;

                    case "Next_my":
                        o = $"Globals.My = Globals.My.Next();";
                        break;

                    case "Next_my_there":
                        o = $"Globals.My = Globals.My.Next_there();";
                        break;

                    case "Next_there":
                        o = $"Globals.There = Globals.There.Next();";
                        break;

                    case "Level":
                        o = $"Environment.Level({m_parameters[0]});";
                        break;

                    case "outport":
                        o = $"Environment.Inport({m_parameters[0]} , {m_parameters[1]});";
                        break;

                    case "Place":
                        o = $"{m_parameters[0]}.Place();";
                        break;

                    case "Play_demo":
                        o = $"Environment.Play_demo({m_parameters[0]}, {m_parameters[1]}, true);";
                        break;

                    case "Play_flic":
                        o = $"Media.Play_flic({m_parameters[0]});";
                        break;

                    case "Play_song":
                        o = $"Media.Play_song({m_parameters[0]}, {m_parameters[1]}, true);";
                        break;

                    case "Play_song_once":
                        o = $"Media.Play_song({m_parameters[0]}, {m_parameters[1]}, false);";
                        break;

                    case "Play_sound":
                        if (m_parameters.Count > 2)
                        {
                            //TODO: check for specific properties containing an object, e.g. <object>.Genius - checking for dot may lead to false positives
                            if (Objects.Is("Thing", m_parameters[2]) || Objects.Is("Actor", m_parameters[2]) || Objects.Is("Wall", m_parameters[2]) ||
                                m_parameters[2].StartsWith("Globals.") || m_parameters[2].EndsWith(".Genius")
                                )
                            {
                                o = $"{m_parameters[2]}.Play_sound({m_parameters[0]}, {m_parameters[1]});";
                            }
                            else
                            {
                                o = $"Media.Play_sound({m_parameters[0]}, {m_parameters[1]}, {m_parameters[2]});";
                            }
                        }
                        else
                        {
                            o = $"Media.Play_sound({m_parameters[0]}, {m_parameters[1]});";
                        }

                        break;

                    case "Print":
                        o = $"Diag.Print({m_parameters[0]})";
                        break;

                    case "Printfile":
                        o = $"Diag.Print({m_parameters[0]}, {m_parameters[1]})";
                        break;

                    case "Push":
                        o = $"Player.Push({m_parameters[0]});";
                        break;

                    case "Randomize":
                        o = $"{m_parameters[0]}.Randomize({m_parameters[1]});";
                        break;

                    case "Rotate":
                        o = $"{m_parameters[0]}.Rotate({m_parameters[1]});";
                        break;

                    case "Rule":
                        o = $"{m_parameters[0]};";
                        break;

                    case "Save":
                        o = $"Environment.Save({m_parameters[0]}, {m_parameters[1]}, false);";
                        break;

                    case "Save_demo":
                        o = $"Environment.Save({m_parameters[0]}, {m_parameters[1]});";
                        break;

                    case "Save_info":
                        o = $"Environment.Save({m_parameters[0]}, {m_parameters[1]}, true);";
                        break;

                    case "Screenshot":
                        o = $"Environment.Screenshot({m_parameters[0]}, {m_parameters[1]});";
                        break;

                    case "Set":
                        o = $"{Formatter.FormatTargetSkill(m_parameters[0])} = {m_parameters[1]};";
                        break;

                    case "Set_all":
                        string[] all = m_parameters[0].Split('.');
                        o = $"foreach (var instance in {all[0]}) instance.{all[1]} = {m_parameters[1]};";
                        break;

                    case "Set_skill":
                        o = $"{m_parameters[0]} = Convert.ToDouble({m_parameters[1]});";
                        break;

                    case "Set_string":
                        o = $"{m_parameters[0]} = string.Copy({m_parameters[1]});";
                        break;

                    case "Setmidi":
                        o = $"Media.Setmidi({m_parameters[0]}, {m_parameters[1]});";
                        break;

                    case "Shake":
                        o = $"{m_parameters[0]}.Shake();";
                        break;

                    case "Shift":
                        o = $"{m_parameters[0]}.Shift({m_parameters[1]}, {m_parameters[2]});";
                        break;

                    case "Shoot":
                        if (m_parameters.Count > 0)
                            o = $"{m_parameters[0]}.Shoot();";
                        else
                            o = $"Player.Shoot();";
                        break;

                    case "Skip":
                        o = $"goto {m_parameters[1]};";
                        break;

                    case "Sin":
                        o = $"{m_parameters[0]} = {Formatter.FormatMath("Sin")}({m_parameters[1]});";
                        break;

                    case "Sqrt":
                        o = $"{m_parameters[0]} = {Formatter.FormatMath("Sqrt")}({m_parameters[1]});";
                        break;

                    case "Stop_demo":
                        o = $"Environment.Stop_demo();";
                        break;

                    case "Stop_flic":
                        o = $"Media.Stop_flic();";
                        break;

                    case "Stop_sound":
                        o = $"Media.Stop_sound();";
                        break;

                    case "Sub":
                        o = $"{m_parameters[0]} -= {m_parameters[1]};";
                        break;

                    case "Subt":
                        o = $"{m_parameters[0]} -= {m_parameters[1]} * {Formatter.FormatSkill("Time_corr")};";
                        break;

                    case "Tilt":
                        o = $"{m_parameters[0]}.Tilt({m_parameters[1]});";
                        break;

                    case "To_string":
                        o = $"{m_parameters[0]} = {m_parameters[1]}.ToString();";
                        break;

                    case "Wait":
                        o = $"yield return Scheduler.Wait({m_parameters[0]});";
                        break;

                    case "Waitt":
                        o = $"yield return Scheduler.Waitt({m_parameters[0]});";
                        break;

                    default:
                        if (m_parameters.Count == 0)
                        {
                            o = m_command;
                        }
                        else
                        {
                            string pars = string.Empty;
                            foreach (string p in m_parameters)
                            {
                                pars += p + " ";
                            }
                            o = "//TODO: " + m_command + " " + pars;
                        }
                        break;
                }
            }
            catch(Exception e)
            {
                Console.WriteLine("(E) INSTRUCTION: " + e.Message + " ("+e.Source+")");
            }

            return o;
        }

        public static List<Instruction> DeserializeList(string stream)
        {
            string[] fragments = stream.Split(new[] { s_sepObj }, StringSplitOptions.RemoveEmptyEntries);
            List<Instruction> instructions = new List<Instruction>();
            foreach (string fragment in fragments)
            {
                Instruction inst = new Instruction();
                inst.Deserialize(fragment);
                instructions.Add(inst);
            }
            return instructions;
        }

    }
}
