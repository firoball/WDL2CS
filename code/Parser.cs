using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using System.Text.RegularExpressions;
using WDL2CS;

namespace VCCCompiler
{
    /// <summary>
    /// Zusammenfassung für MyCompiler.
    /// </summary>
    class MyCompiler
    {
        List<int> tList = new List<int>();
        List<Regex> rList = new List<Regex>();
        MyCompiler()
        {
            tList.Add(t_Char59);
            rList.Add(new Regex("\\G(;)"));
            tList.Add(t_Char123);
            rList.Add(new Regex("\\G(\\{)"));
            tList.Add(t_Char125);
            rList.Add(new Regex("\\G(\\})"));
            tList.Add(t_Char58);
            rList.Add(new Regex("\\G(:)"));
            tList.Add(t_Char124Char124);
            rList.Add(new Regex("\\G(\\|\\|)"));
            tList.Add(t_Char38Char38);
            rList.Add(new Regex("\\G(&&)"));
            tList.Add(t_Char124);
            rList.Add(new Regex("\\G(\\|)"));
            tList.Add(t_Char94);
            rList.Add(new Regex("\\G(\\^)"));
            tList.Add(t_Char38);
            rList.Add(new Regex("\\G(&)"));
            tList.Add(t_Char40);
            rList.Add(new Regex("\\G(\\()"));
            tList.Add(t_Char41);
            rList.Add(new Regex("\\G(\\))"));
            tList.Add(t_Char33Char61);
            rList.Add(new Regex("\\G(!=)"));
            tList.Add(t_Char61Char61);
            rList.Add(new Regex("\\G(==)"));
            tList.Add(t_Char60);
            rList.Add(new Regex("\\G(<)"));
            tList.Add(t_Char60Char61);
            rList.Add(new Regex("\\G(<=)"));
            tList.Add(t_Char62);
            rList.Add(new Regex("\\G(>)"));
            tList.Add(t_Char62Char61);
            rList.Add(new Regex("\\G(>=)"));
            tList.Add(t_Char43);
            rList.Add(new Regex("\\G(\\+)"));
            tList.Add(t_Char45);
            rList.Add(new Regex("\\G(\\-)"));
            tList.Add(t_Char37);
            rList.Add(new Regex("\\G(%)"));
            tList.Add(t_Char42);
            rList.Add(new Regex("\\G(\\*)"));
            tList.Add(t_Char47);
            rList.Add(new Regex("\\G(/)"));
            tList.Add(t_Char33);
            rList.Add(new Regex("\\G(!)"));
            tList.Add(t_RULE);
            rList.Add(new Regex("\\G(RULE)"));
            tList.Add(t_Char42Char61);
            rList.Add(new Regex("\\G(\\*[\\s\\t\\x00]*=)"));
            tList.Add(t_Char43Char61);
            rList.Add(new Regex("\\G(\\+[\\s\\t\\x00]*=)"));
            tList.Add(t_Char45Char61);
            rList.Add(new Regex("\\G(\\-[\\s\\t\\x00]*=)"));
            tList.Add(t_Char47Char61);
            rList.Add(new Regex("\\G(/[\\s\\t\\x00]*=)"));
            tList.Add(t_Char61);
            rList.Add(new Regex("\\G(=)"));
            tList.Add(t_ELSE);
            rList.Add(new Regex("\\G(ELSE)"));
            tList.Add(t_IF);
            rList.Add(new Regex("\\G(IF)"));
            tList.Add(t_WHILE);
            rList.Add(new Regex("\\G(WHILE)"));
            tList.Add(t_Char46);
            rList.Add(new Regex("\\G(\\.)"));
            tList.Add(t_NULL);
            rList.Add(new Regex("\\G(NULL)"));
            tList.Add(t_event);
            rList.Add(new Regex("\\G((?=[A-Z])(EACH_SEC|IF_(AE|ALT|ANYKEY|APO|BKSL|BKSP|BRACKL|BRACKR|CAL|CAR|COMMA|CTRL|CUD|CUL|CUR|CUU|DEL|END|EQUALS|ESC|HOME|INS|JOY4|LEFT|LOAD|MIDDLE|MINUS|MSTOP|OE|PAUSE|PERIOD|PGDN|PGUP|PLUS|RIGHT|SEMIC|SLASH|SPACE|START|SZ|TAB|TAST|UE|F(1[0-2]|[1-9])|[0-9A-Z])|LAYERS|MESSAGES|PANELS))"));
            tList.Add(t_global);
            rList.Add(new Regex("\\G((?=[A-Z])(VIDEO|NEXUS|LIGHT_ANGLE|IBANK|DRUMBANK|MIDI_PITCH|BIND|MAPFILE|SAVEDIR|PATH|DITHER|SAVE_KEYS|REMOTE_KEYS))"));
            tList.Add(t_asset);
            rList.Add(new Regex("\\G((?=[A-Z])(MODEL|SOUND|MUSIC|FLIC|BMAP|OVLY|FONT))"));
            tList.Add(t_object);
            rList.Add(new Regex("\\G((?=[A-Z])(OVERLAY|PANEL|PALETTE|REGION|SKILL|STRING|SYNONYM|TEXTURE|TEXT|VIEW|WALL|WAY))"));
            tList.Add(t_function);
            rList.Add(new Regex("\\G((?=[A-Z])(ACTION|RULES))"));
            tList.Add(t_math);
            rList.Add(new Regex("\\G((?=[A-Z])(ACOS|COS|ATAN|TAN|SIGN|INT|EXP|LOG10|LOG2|LOG))"));
            tList.Add(t_list);
            rList.Add(new Regex("\\G(((EACH_TICK|EACH_SEC|PANELS|LAYERS|MESSAGES)\\.(1[0-6]|[1-9])))"));
            tList.Add(t_skill);
            rList.Add(new Regex("\\G((?=[A-Z])(ACCELERATION|ACTIONS|ACTIVE_(NEXUS|OBJTICKS|TARGETS)|ACTOR_(CEIL_HGT|CLIMB|DIST|FLOOR_HGT|IMPACT_V[X-Z]|WIDTH)|APPEND_MODE|ASPECT|BLUR_MODE|BOUNCE_V[X-Y]|CDAUDIO_VOL|CD_TRACK|CHANNEL_[0-7]|CHANNEL|CLIPPED|CLIPPING|COLOR_(ACTORS|BORDER|PLAYER|THINGS|WALLS)|DARK_DIST|DEBUG_MODE|DELTA_ANGLE|ERROR|FLIC_FRAME|FLOOR_MODE|FORCE_(AHEAD|ROT|STRAFE|TILT|UP)|FRAME_COLOR|FRICTION|HALF_PI|HIT_(DIST|MINDIST|X|Y)|IMPACT_V(ROT|[X-Z])|INERTIA|INV_DIST|JOYSTICK_[X-Y]|JOY_(4|SENSE)|KEY_(ALT|ANY|APO|BKSL|BKSP|BRACKL|BRACKR|CAL|CAR|COMMA|CTRL|CUD|CUL|CUR|CUU|DEL|END|ENTER|EQUALS|ESC|HOME|INS|JOY4|MINUS|PAUSE|PERIOD|PGDN|PGUP|PLUS|SEMIC|SENSE|SHIFT|SLASH|SPACE|SZ|TAB|F(1[0-2]|[1-9])|[A-Z0-9])|LIGHT_DIST|LOAD_MODE|MAP_(CENTER[X-Y]|(EDGE_[X-Y][1-2])|LAYER|MAX[X-Y]|MIN[X-Y]|MODE|OFFS[X-Y]|ROT|SCALE)|MAX_DIST|MICKEY_[X-Y]|MINV_DIST|MOTION_BLUR|MOUSE_(ANGLE|CALM|LEFT|MIDDLE|MODE|MOVING|RIGHT|SENSE|TIME|X|Y)|MOVE_(ANGLE|MODE)|MUSIC_VOL|MY_(X[1-2]|Y[1-2]|Z[1-2]|X|Y)|PANEL_LAYER|PI|PLAYER_(ANGLE|ARC|CLIMB|COS|DEPTH|HGT|LAST_[X-Y]|LIGHT|MSIN|SIN|SIZE|SPEED|TILT|VROT|V[X-Z]|WIDTH|[X-Z])|PSOUND_(TONE|VOL)|REAL_SPEED|REMOTE_[0-1]|RENDER_MODE|SCREEN_(HGT|WIDTH|X|Y)|SECS|SHIFT_SENSE|SHOOT_(ANGLE|FAC|RANGE|SECTOR|X|Y)|SKIP_FRAMES|SKY_OFFS_[X-Y]|SLOPE_(AHEAD|SIDE|X|Y)|SOUND_VOL|SPANS|STEPS|STR_LEN|TEXT_LAYER|THING_(DIST|WIDTH)|TICKS|TIME_(ACTIONS|CLIPPING|CORR|DRAW|FAC|FBUFFER|SLICES|TARGETS|VERTICES)|TOUCH_(DIST|MODE|RANGE|STATE)|TWO_PI|WALK_(PERIOD|TIME)|WALK|WAVE_PERIOD|WAVE|PALANIM_DELAY))"));
            tList.Add(t_synonym);
            rList.Add(new Regex("\\G((?=[A-Z])(THERE|MY|HIT|TOUCH_TEXT|TOUCHED|TOUCH_TEX|TOUCH_REG|COMMAND_LINE|REGION[1-8]))"));
            tList.Add(t_ambigChar95globalChar95property);
            rList.Add(new Regex("\\G(CLIP_DIST)"));
            tList.Add(t_ambigChar95eventChar95property);
            rList.Add(new Regex("\\G((?=[A-Z])(EACH_TICK|IF_ENTER|IF_HIT|IF_KLICK))"));
            tList.Add(t_ambigChar95objectChar95flag);
            rList.Add(new Regex("\\G((?=[A-Z])(THING|ACTOR))"));
            tList.Add(t_ambigChar95mathChar95command);
            rList.Add(new Regex("\\G((?=[A-Z])(SIN|ASIN|SQRT|ABS))"));
            tList.Add(t_ambigChar95mathChar95skillChar95property);
            rList.Add(new Regex("\\G(RANDOM)"));
            tList.Add(t_ambigChar95synonymChar95flag);
            rList.Add(new Regex("\\G(HERE)"));
            tList.Add(t_ambigChar95skillChar95property);
            rList.Add(new Regex("\\G((?=[A-Z])(FLOOR_HGT|CEIL_HGT|AMBIENT|RESULT|NODE))"));
            tList.Add(t_ambigChar95globalChar95synonymChar95property);
            rList.Add(new Regex("\\G(MSPRITE)"));
            tList.Add(t_ambigChar95skillChar95flag);
            rList.Add(new Regex("\\G((FLAG[1-8]))"));
            tList.Add(t_integer);
            rList.Add(new Regex("\\G([0-9]+)"));
            tList.Add(t_fixed);
            rList.Add(new Regex("\\G(([0-9]*\\.[0-9]+)|([0-9]+\\.[0-9]*))"));
            tList.Add(t_identifier);
            rList.Add(new Regex("\\G([A-Za-z0-9_][A-Za-z0-9_\\?]*(\\.[1-9][0-9]?)?)"));
            tList.Add(t_file);
            rList.Add(new Regex("\\G(<[\\s]?[^<;:\" ]+\\.[^<;:\" ]+[\\s]?>)"));
            tList.Add(t_string);
            rList.Add(new Regex("\\G(\"(\\\\\"|.|[\\r\\n])*?\")"));
            tList.Add(t_ignore);
            rList.Add(new Regex("\\G([\\r\\n\\t\\s\\x00,]|:=|(#.*(\\n|$))|(//.*(\\n|$))|(/\\*(.|[\\r\\n])*?\\*/))")); Regex.CacheSize += rList.Count;
        }
        YYARec[] yya;
        YYARec[] yyg;
        YYRRec[] yyr;
        int[] yyd;
        int[] yyal;
        int[] yyah;
        int[] yygl;
        int[] yygh;

        int yyn = 0;
        int yystate = 0;
        int yychar = -1;
        int yynerrs = 0;
        int yyerrflag = 0;
        int yysp = 0;
        int yymaxdepth = 20000;
        int yyflag = 0;
        int yyfnone = 0;
        int[] yys = new int[20000];
        string[] yyv = new string[20000];

        string yyval = "";

        FileStream OutputStream;
        //StreamWriter Output;
        TextWriter Output;
        bool DeserializeOutput = false;
        string Scriptname = "Script";

        class YYARec
        {
            public int sym;
            public int act;
            public YYARec(int s, int a) { sym = s; act = a; }
        }

        class YYRRec
        {
            public int len;
            public int sym;
            public YYRRec(int l, int s) { sym = s; len = l; }
        }

        ////////////////////////////////////////////////////////////////
        /// Constant values / tokens
        ////////////////////////////////////////////////////////////////
                int t_Char59 = 257;
                int t_Char123 = 258;
                int t_Char125 = 259;
                int t_Char58 = 260;
                int t_Char124Char124 = 261;
                int t_Char38Char38 = 262;
                int t_Char124 = 263;
                int t_Char94 = 264;
                int t_Char38 = 265;
                int t_Char40 = 266;
                int t_Char41 = 267;
                int t_Char33Char61 = 268;
                int t_Char61Char61 = 269;
                int t_Char60 = 270;
                int t_Char60Char61 = 271;
                int t_Char62 = 272;
                int t_Char62Char61 = 273;
                int t_Char43 = 274;
                int t_Char45 = 275;
                int t_Char37 = 276;
                int t_Char42 = 277;
                int t_Char47 = 278;
                int t_Char33 = 279;
                int t_RULE = 280;
                int t_Char42Char61 = 281;
                int t_Char43Char61 = 282;
                int t_Char45Char61 = 283;
                int t_Char47Char61 = 284;
                int t_Char61 = 285;
                int t_ELSE = 286;
                int t_IF = 287;
                int t_WHILE = 288;
                int t_Char46 = 289;
                int t_NULL = 290;
                int t_event = 291;
                int t_global = 292;
                int t_asset = 293;
                int t_object = 294;
                int t_function = 295;
                int t_math = 296;
                int t_list = 297;
                int t_skill = 298;
                int t_synonym = 299;
                int t_ambigChar95globalChar95property = 300;
                int t_ambigChar95eventChar95property = 301;
                int t_ambigChar95objectChar95flag = 302;
                int t_ambigChar95mathChar95command = 303;
                int t_ambigChar95mathChar95skillChar95property = 304;
                int t_ambigChar95synonymChar95flag = 305;
                int t_ambigChar95skillChar95property = 306;
                int t_ambigChar95globalChar95synonymChar95property = 307;
                int t_ambigChar95skillChar95flag = 308;
                int t_integer = 309;
                int t_fixed = 310;
                int t_identifier = 311;
                int t_file = 312;
                int t_string = 313;
                int t_ignore = 256;
///////////////////////////////////////////////////////////
/// Global settings: 
///////////////////////////////////////////////////////////

///////////////////////////////////////////////////////////


        /// <summary>
        /// Der Haupteinstiegspunkt für die Anwendung.
        /// </summary>
        [STAThread]
        static int Main(string[] args)
        {

            bool ShowTokens = false;
            string InputFilename = "";
            string OutputFilename = "";
            string Scriptname = "";

            foreach (string s in args)
            {
                if (s.ToLower() == "-t")
                {
                    ShowTokens = true;
                }
                else
                {
                    if (InputFilename == "") InputFilename = s;
                    else
                    if (OutputFilename == "") OutputFilename = s;
                    else
                    if (Scriptname == "") Scriptname = s;
                    else
                    {
                        Console.WriteLine("Too many arguments!");
                        return 1;
                    }
                }
            }
            if (InputFilename == "")
            {
                System.Console.WriteLine("You need to specify input and (optional) outputfile: compiler.exe input.txt [output.txt] [C# Class Identifier]");
                return 1;
            }
            if (Scriptname == "")
            {
                Scriptname = Path.GetFileNameWithoutExtension(InputFilename);
            }

            string inputstream = File.ReadAllText(InputFilename, Encoding.ASCII);
            string path = Path.GetDirectoryName(InputFilename);

            Stopwatch watch = new Stopwatch();
            watch.Start();
            Preprocess p = new Preprocess(path);
            inputstream = p.Parse(ref inputstream);

            ////////////////////////////////////////////////////////////////
            /// Compiler Code:
            ////////////////////////////////////////////////////////////////
            MyCompiler compiler = new MyCompiler();
            compiler.Output = null;
            compiler.DeserializeOutput = true;
            compiler.Scriptname = Scriptname;
            if (OutputFilename != "")
            {
                File.Delete(OutputFilename);
                compiler.OutputStream = File.OpenWrite(OutputFilename);
                compiler.Output = new StreamWriter(compiler.OutputStream, new System.Text.UTF8Encoding(false));
            }
            else
            {
                compiler.Output = new StreamWriter(Console.OpenStandardOutput(), new System.Text.UTF8Encoding(false));

            }

            //                        if (!compiler.Scanner(inputstream)) return 1;
            if (!compiler.ScannerOpt(inputstream)) return 1;
            if (ShowTokens)
            {
                foreach (AToken t in compiler.TokenList)
                {
                    Console.WriteLine("TokenID: " + t.token + "  =  " + t.val);
                }
            }
            compiler.InitTables();
            if (!compiler.yyparse()) return 1;

            if (compiler.Output != null) compiler.Output.Close();
            Console.WriteLine("(I) PARSER compilation finished in " + watch.Elapsed);
            watch.Stop();
            return 0;
        }
        public void yyaction(int yyruleno)
        {
            switch (yyruleno)
            {
                ////////////////////////////////////////////////////////////////
                /// YYAction code:
                ////////////////////////////////////////////////////////////////
							case    1 : 
         yyval = yyv[yysp-0];
         if (DeserializeOutput)
         Output.WriteLine(Script.Format(Scriptname, yyval));
         else
         Output.Write(yyval);
         
       break;
							case    2 : 
         yyval = yyv[yysp-1] + yyv[yysp-0];
         
       break;
							case    3 : 
         yyval = ""; //bogus keyword at EOF - just discard
         
       break;
							case    4 : 
         yyval = "";
         
       break;
							case    5 : 
         yyval = "";
         
       break;
							case    6 : 
         yyval = Sections.AddDummySection(yyv[yysp-0]);
         
       break;
							case    7 : 
         yyval = Sections.AddDummySection(yyv[yysp-0]);
         
       break;
							case    8 : 
         yyval = Sections.AddGlobalSection(yyv[yysp-0]);
         
       break;
							case    9 : 
         yyval = Sections.AddObjectSection(yyv[yysp-0]);
         
       break;
							case   10 : 
         yyval = Sections.AddActionSection(yyv[yysp-0]);
         
       break;
							case   11 : 
         yyval = Sections.AddAssetSection(yyv[yysp-0]);
         
       break;
							case   12 : 
         yyval = Globals.AddEvent(yyv[yysp-2]);
         
       break;
							case   13 : 
         yyval = Globals.AddGlobal(yyv[yysp-2], yyv[yysp-1]);
         
       break;
							case   14 : 
         yyval = Formatter.FormatGlobal(yyv[yysp-0]); //TODO: remove after globals are refactored
         
       break;
							case   15 : 
         yyval = Formatter.FormatGlobal(yyv[yysp-0]); //TODO: remove after globals are refactored
         
       break;
							case   16 : 
         yyval = Formatter.FormatGlobal(yyv[yysp-0]); //TODO: remove after globals are refactored
         
       break;
							case   17 : 
         yyval = Formatter.FormatTargetSkill(Formatter.FormatSkill(yyv[yysp-0])); //TODO: temporary - remove after refactoring
         
       break;
							case   18 : 
         yyval = Formatter.FormatEvent(yyv[yysp-0]);
         
       break;
							case   19 : 
         yyval = Formatter.FormatEvent(yyv[yysp-0]);
         
       break;
							case   20 : 
         yyval = "";
         Globals.AddParameter(yyv[yysp-0]);
         
       break;
							case   21 : 
         yyval = "";
         Globals.AddParameter(yyv[yysp-1]);
         
       break;
							case   22 : 
         yyval = yyv[yysp-0];
         
       break;
							case   23 : 
         //yyval = yyv[yysp-0];
         yyval = Formatter.FormatIdentifier(yyv[yysp-0]); //temporary - remove after updating grammar for globals/events
         
       break;
							case   24 : 
         yyval = yyv[yysp-0];
         
       break;
							case   25 : 
         yyval = yyv[yysp-0];
         
       break;
							case   26 : 
         yyval = Assets.AddAsset(yyv[yysp-4], yyv[yysp-3], yyv[yysp-2]);
         
       break;
							case   27 : 
         yyval = "";
         Assets.AddParameter(yyv[yysp-1]);
         
       break;
							case   28 : 
         yyval = "";
         
       break;
							case   29 : 
         yyval = yyv[yysp-0];
         
       break;
							case   30 : 
         yyval = yyv[yysp-0];
         
       break;
							case   31 : 
         yyval = Objects.AddObject(yyv[yysp-4], yyv[yysp-3], yyv[yysp-1]);
         
       break;
							case   32 : 
         yyval = Objects.AddStringObject(yyv[yysp-3], yyv[yysp-2], yyv[yysp-1]);
         
       break;
							case   33 : 
         yyval = Objects.AddObject(yyv[yysp-2], yyv[yysp-1]);
         
       break;
							case   34 : 
         yyval = yyv[yysp-0];
         
       break;
							case   35 : 
         yyval = yyv[yysp-0];
         
       break;
							case   36 : 
         yyval = yyv[yysp-0];
         
       break;
							case   37 : 
         yyval = yyv[yysp-1] + yyv[yysp-0];
         
       break;
							case   38 : 
         yyval = "";
         
       break;
							case   39 : 
         yyval = Objects.CreateProperty(yyv[yysp-2]);
         
       break;
							case   40 : 
         yyval = "";
         
       break;
							case   41 : 
         Objects.AddPropertyValue(yyv[yysp-0]);
         yyval = "";
         
       break;
							case   42 : 
         Objects.AddPropertyValue(yyv[yysp-1]);
         yyval = "";
         
       break;
							case   43 : 
         yyval = yyv[yysp-0];
         
       break;
							case   44 : 
         yyval = yyv[yysp-0];
         
       break;
							case   45 : 
         yyval = yyv[yysp-0];
         
       break;
							case   46 : 
         yyval = yyv[yysp-0];
         
       break;
							case   47 : 
         yyval = Actions.AddAction(yyv[yysp-3], yyv[yysp-1]);
         
       break;
							case   48 : 
         yyval = yyv[yysp-0];
         
       break;
							case   49 : 
         yyval = yyv[yysp-2] + yyv[yysp-0];
         
       break;
							case   50 : 
         yyval = Actions.CreateMarker(yyv[yysp-2], yyv[yysp-0]);
         
       break;
							case   51 : 
         yyval = yyv[yysp-1] + yyv[yysp-0];
         
       break;
							case   52 : 
         yyval = yyv[yysp-1] + yyv[yysp-0];
         
       break;
							case   53 : 
         yyval = "";
         
       break;
							case   54 : 
         yyval = Actions.CreateInstruction(yyv[yysp-2]);
         
       break;
							case   55 : 
         yyval = yyv[yysp-1];
         
       break;
							case   56 : 
         yyval = Actions.CreateInstruction(yyv[yysp-1]);
         
       break;
							case   57 : 
         yyval = "";
         
       break;
							case   58 : 
         yyval = "";
         Actions.AddInstructionParameter(yyv[yysp-0]);
         
       break;
							case   59 : 
         yyval = "";
         Actions.AddInstructionParameter(yyv[yysp-1]);
         
       break;
							case   60 : 
         yyval = yyv[yysp-0];
         
       break;
							case   61 : 
         yyval = yyv[yysp-0];
         
       break;
							case   62 : 
         yyval = yyv[yysp-0];
         
       break;
							case   63 : 
         yyval = yyv[yysp-0];
         
       break;
							case   64 : 
         yyval = yyv[yysp-0];
         
       break;
							case   65 : 
         yyval = yyv[yysp-0];
         
       break;
							case   66 : 
         yyval = yyv[yysp-2] + " || " + yyv[yysp-0];
         
       break;
							case   67 : 
         yyval = yyv[yysp-0];
         
       break;
							case   68 : 
         yyval = yyv[yysp-2] + " && " + yyv[yysp-0];
         
       break;
							case   69 : 
         yyval = yyv[yysp-0];
         
       break;
							case   70 : 
         yyval = yyv[yysp-2] + " | " + yyv[yysp-0];
         
       break;
							case   71 : 
         yyval = yyv[yysp-0];
         
       break;
							case   72 : 
         yyval = yyv[yysp-2] + " ^ " + yyv[yysp-0];
         
       break;
							case   73 : 
         yyval = yyv[yysp-0];
         
       break;
							case   74 : 
         yyval = yyv[yysp-2] + " & " + yyv[yysp-0];
         
       break;
							case   75 : 
         yyval = yyv[yysp-0];
         
       break;
							case   76 : 
         yyval = yyv[yysp-0];
         
       break;
							case   77 : 
         yyval = yyv[yysp-2] + yyv[yysp-1] + yyv[yysp-0];
         
       break;
							case   78 : 
         yyval = yyv[yysp-0];
         
       break;
							case   79 : 
         yyval = yyv[yysp-2] + yyv[yysp-1] + yyv[yysp-0];
         
       break;
							case   80 : 
         yyval = yyv[yysp-0];
         
       break;
							case   81 : 
         yyval = yyv[yysp-2] + yyv[yysp-1] + yyv[yysp-0];
         
       break;
							case   82 : 
         yyval = yyv[yysp-0];
         
       break;
							case   83 : 
         yyval = yyv[yysp-2] + yyv[yysp-1] + yyv[yysp-0];
         
       break;
							case   84 : 
         yyval = yyv[yysp-0];
         
       break;
							case   85 : 
         yyval = yyv[yysp-1] + yyv[yysp-0];
         
       break;
							case   86 : 
         yyval = Formatter.FormatMath(yyv[yysp-3]) + "(" + yyv[yysp-1] + ")";
         
       break;
							case   87 : 
         yyval = "(" + yyv[yysp-1] + ")";
         
       break;
							case   88 : 
         //yyval = yyv[yysp-1] + "." + yyv[yysp-0]); //fixes things like "18,4"
         Console.WriteLine("(W) PARSER discarded superfluous token in expression: , " + yyv[yysp-0]);
         yyval = Formatter.FormatNumber(yyv[yysp-1]); //this is what supposedly happens in A3
         
       break;
							case   89 : 
         //yyval = Formatter.FormatTargetSkill(yyv[yysp-1] + yyv[yysp-0]); //fixes things like "Skill 6"
         Console.WriteLine("(W) PARSER discarded superfluous token in expression: " + yyv[yysp-1]);
         yyval = Formatter.FormatNumber(yyv[yysp-0]); //this is what supposedly happens in A3
         
       break;
							case   90 : 
         yyval = yyv[yysp-0];
         
       break;
							case   91 : 
         yyval = yyv[yysp-0];
         
       break;
							case   92 : 
         yyval = yyv[yysp-0];
         
       break;
							case   93 : 
         yyval = " != ";
         
       break;
							case   94 : 
         yyval = " == ";
         
       break;
							case   95 : 
         yyval = " < ";
         
       break;
							case   96 : 
         yyval = " <= ";
         
       break;
							case   97 : 
         yyval = " > ";
         
       break;
							case   98 : 
         yyval = " >= ";
         
       break;
							case   99 : 
         yyval = " + ";
         
       break;
							case  100 : 
         yyval = " - ";
         
       break;
							case  101 : 
         yyval = " % ";
         
       break;
							case  102 : 
         yyval = " * ";
         
       break;
							case  103 : 
         yyval = " / ";
         
       break;
							case  104 : 
         yyval = "!";
         
       break;
							case  105 : 
         yyval = "+";
         
       break;
							case  106 : 
         yyval = "-";
         
       break;
							case  107 : 
         yyval = Actions.CreateExpression(yyv[yysp-0]);
         
       break;
							case  108 : 
         yyval = Actions.CreateExpression(yyv[yysp-2], yyv[yysp-1], yyv[yysp-0]);
         
       break;
							case  109 : 
         yyval = Actions.CreateExpression(yyv[yysp-2], yyv[yysp-1], yyv[yysp-0]);
         
       break;
							case  110 : 
         yyval = " *= ";
         
       break;
							case  111 : 
         yyval = " += ";
         
       break;
							case  112 : 
         yyval = " -= ";
         
       break;
							case  113 : 
         yyval = " /= ";
         
       break;
							case  114 : 
         yyval = " = ";
         
       break;
							case  115 : 
         yyval = yyv[yysp-0];
         
       break;
							case  116 : 
         yyval = yyv[yysp-0];
         
       break;
							case  117 : 
         yyval = yyv[yysp-0];
         
       break;
							case  118 : 
         yyval = Actions.CreateElseCondition(yyv[yysp-1]);
         
       break;
							case  119 : 
         yyval = Actions.CreateIfCondition(yyv[yysp-3], yyv[yysp-1]);
         
       break;
							case  120 : 
         yyval = Actions.CreateWhileCondition(yyv[yysp-3], yyv[yysp-1]);
         
       break;
							case  121 : 
         yyval = Formatter.FormatKeyword(yyv[yysp-2] + "." + yyv[yysp-0]);
         
       break;
							case  122 : 
         yyval = Formatter.FormatKeyword(yyv[yysp-0]);
         
       break;
							case  123 : 
         yyval = yyv[yysp-0];
         
       break;
							case  124 : 
         yyval = yyv[yysp-1] + yyv[yysp-0];
         
       break;
							case  125 : 
         yyval = Formatter.FormatCommand(yyv[yysp-0]);
         
       break;
							case  126 : 
         yyval = Formatter.FormatCommand(yyv[yysp-0]);
         
       break;
							case  127 : 
         yyval = Formatter.FormatNumber(yyv[yysp-0]);
         
       break;
							case  128 : 
         yyval = Formatter.FormatNumber(yyv[yysp-0]);
         
       break;
							case  129 : 
         yyval = Formatter.FormatNumber(yyv[yysp-0]);
         
       break;
							case  130 : 
         yyval = Formatter.FormatNumber(yyv[yysp-0]);
         
       break;
							case  131 : 
         yyval = yyv[yysp-1] + yyv[yysp-0];
         
       break;
							case  132 : 
         yyval = yyv[yysp-0];
         
       break;
							case  133 : 
         yyval = yyv[yysp-1] + yyv[yysp-0];
         
       break;
							case  134 : 
         yyval = yyv[yysp-0];
         
       break;
							case  135 : 
         yyval = yyv[yysp-0];
         
       break;
							case  136 : 
         yyval = Formatter.FormatList(yyv[yysp-0]);
         
       break;
							case  137 : 
         yyval = yyv[yysp-0];
         
       break;
							case  138 : 
         yyval = yyv[yysp-0];
         
       break;
							case  139 : 
         yyval = yyv[yysp-0];
         
       break;
							case  140 : 
         yyval = yyv[yysp-0];
         
       break;
							case  141 : 
         yyval = yyv[yysp-0];
         
       break;
							case  142 : 
         yyval = yyv[yysp-0];
         
       break;
							case  143 : 
         yyval = yyv[yysp-0];
         
       break;
							case  144 : 
         yyval = yyv[yysp-0];
         
       break;
							case  145 : 
         yyval = yyv[yysp-0];
         
       break;
							case  146 : 
         yyval = yyv[yysp-0];
         
       break;
							case  147 : 
         yyval = yyv[yysp-0];
         
       break;
							case  148 : 
         yyval = yyv[yysp-0];
         
       break;
							case  149 : 
         yyval = yyv[yysp-0];
         
       break;
							case  150 : 
         yyval = yyv[yysp-0];
         
       break;
							case  151 : 
         yyval = yyv[yysp-0];
         
       break;
							case  152 : 
         yyval = yyv[yysp-0];
         
       break;
							case  153 : 
         yyval = yyv[yysp-0];
         
       break;
							case  154 : 
         yyval = yyv[yysp-0];
         
       break;
							case  155 : 
         yyval = yyv[yysp-0];
         
       break;
							case  156 : 
         yyval = yyv[yysp-0];
         
       break;
							case  157 : 
         yyval = Formatter.FormatNull();
         
       break;
							case  158 : 
         yyval = yyv[yysp-0];
         
       break;
							case  159 : 
         yyval = yyv[yysp-0];
         
       break;
							case  160 : 
         yyval = yyv[yysp-0];
         
       break;
							case  161 : 
         yyval = yyv[yysp-2] + yyv[yysp-0];
         
       break;
							case  162 : 
         yyval = yyv[yysp-2] + yyv[yysp-0];
         
       break;
							case  163 : 
         yyval = yyv[yysp-0];
         
       break;
							case  164 : 
         yyval = yyv[yysp-0];
         
       break;
							case  165 : 
         yyval = yyv[yysp-0];
         
       break;
							case  166 : 
         yyval = yyv[yysp-0];
         
       break;
							case  167 : 
         yyval = yyv[yysp-0];
         
       break;
							case  168 : 
         yyval = yyv[yysp-0]; //TODO: FormatIdentifier?
         
       break;
							case  169 : 
         yyval = Formatter.FormatFile(yyv[yysp-0]);
         
       break;
							case  170 : 
         yyval = Formatter.FormatString(yyv[yysp-0]);
         
       break;
               default: return;
            }
        }

        public void InitTables()
        {
            ////////////////////////////////////////////////////////////////
            /// Init Table code:
            ////////////////////////////////////////////////////////////////

					int yynacts   = 1707;
					int yyngotos  = 568;
					int yynstates = 235;
					int yynrules  = 170;
					yya = new YYARec[yynacts+1];  int yyac = 1;
					yyg = new YYARec[yyngotos+1]; int yygc = 1;
					yyr = new YYRRec[yynrules+1]; int yyrc = 1;

					yya[yyac] = new YYARec(257,20);yyac++; 
					yya[yyac] = new YYARec(258,21);yyac++; 
					yya[yyac] = new YYARec(259,22);yyac++; 
					yya[yyac] = new YYARec(290,23);yyac++; 
					yya[yyac] = new YYARec(291,24);yyac++; 
					yya[yyac] = new YYARec(292,25);yyac++; 
					yya[yyac] = new YYARec(293,26);yyac++; 
					yya[yyac] = new YYARec(294,27);yyac++; 
					yya[yyac] = new YYARec(295,28);yyac++; 
					yya[yyac] = new YYARec(296,29);yyac++; 
					yya[yyac] = new YYARec(298,30);yyac++; 
					yya[yyac] = new YYARec(299,31);yyac++; 
					yya[yyac] = new YYARec(300,32);yyac++; 
					yya[yyac] = new YYARec(301,33);yyac++; 
					yya[yyac] = new YYARec(302,34);yyac++; 
					yya[yyac] = new YYARec(303,35);yyac++; 
					yya[yyac] = new YYARec(304,36);yyac++; 
					yya[yyac] = new YYARec(305,37);yyac++; 
					yya[yyac] = new YYARec(306,38);yyac++; 
					yya[yyac] = new YYARec(307,39);yyac++; 
					yya[yyac] = new YYARec(308,40);yyac++; 
					yya[yyac] = new YYARec(311,41);yyac++; 
					yya[yyac] = new YYARec(0,-4 );yyac++; 
					yya[yyac] = new YYARec(290,23);yyac++; 
					yya[yyac] = new YYARec(291,45);yyac++; 
					yya[yyac] = new YYARec(292,46);yyac++; 
					yya[yyac] = new YYARec(293,47);yyac++; 
					yya[yyac] = new YYARec(294,48);yyac++; 
					yya[yyac] = new YYARec(295,49);yyac++; 
					yya[yyac] = new YYARec(296,29);yyac++; 
					yya[yyac] = new YYARec(298,50);yyac++; 
					yya[yyac] = new YYARec(299,31);yyac++; 
					yya[yyac] = new YYARec(300,51);yyac++; 
					yya[yyac] = new YYARec(301,52);yyac++; 
					yya[yyac] = new YYARec(302,53);yyac++; 
					yya[yyac] = new YYARec(303,35);yyac++; 
					yya[yyac] = new YYARec(304,54);yyac++; 
					yya[yyac] = new YYARec(305,37);yyac++; 
					yya[yyac] = new YYARec(306,55);yyac++; 
					yya[yyac] = new YYARec(307,56);yyac++; 
					yya[yyac] = new YYARec(308,40);yyac++; 
					yya[yyac] = new YYARec(311,57);yyac++; 
					yya[yyac] = new YYARec(289,58);yyac++; 
					yya[yyac] = new YYARec(0,-122 );yyac++; 
					yya[yyac] = new YYARec(257,-122 );yyac++; 
					yya[yyac] = new YYARec(258,-122 );yyac++; 
					yya[yyac] = new YYARec(261,-122 );yyac++; 
					yya[yyac] = new YYARec(262,-122 );yyac++; 
					yya[yyac] = new YYARec(263,-122 );yyac++; 
					yya[yyac] = new YYARec(264,-122 );yyac++; 
					yya[yyac] = new YYARec(265,-122 );yyac++; 
					yya[yyac] = new YYARec(267,-122 );yyac++; 
					yya[yyac] = new YYARec(268,-122 );yyac++; 
					yya[yyac] = new YYARec(269,-122 );yyac++; 
					yya[yyac] = new YYARec(270,-122 );yyac++; 
					yya[yyac] = new YYARec(271,-122 );yyac++; 
					yya[yyac] = new YYARec(272,-122 );yyac++; 
					yya[yyac] = new YYARec(273,-122 );yyac++; 
					yya[yyac] = new YYARec(274,-122 );yyac++; 
					yya[yyac] = new YYARec(275,-122 );yyac++; 
					yya[yyac] = new YYARec(276,-122 );yyac++; 
					yya[yyac] = new YYARec(277,-122 );yyac++; 
					yya[yyac] = new YYARec(278,-122 );yyac++; 
					yya[yyac] = new YYARec(279,-122 );yyac++; 
					yya[yyac] = new YYARec(281,-122 );yyac++; 
					yya[yyac] = new YYARec(282,-122 );yyac++; 
					yya[yyac] = new YYARec(283,-122 );yyac++; 
					yya[yyac] = new YYARec(284,-122 );yyac++; 
					yya[yyac] = new YYARec(285,-122 );yyac++; 
					yya[yyac] = new YYARec(290,-122 );yyac++; 
					yya[yyac] = new YYARec(291,-122 );yyac++; 
					yya[yyac] = new YYARec(292,-122 );yyac++; 
					yya[yyac] = new YYARec(293,-122 );yyac++; 
					yya[yyac] = new YYARec(294,-122 );yyac++; 
					yya[yyac] = new YYARec(295,-122 );yyac++; 
					yya[yyac] = new YYARec(296,-122 );yyac++; 
					yya[yyac] = new YYARec(297,-122 );yyac++; 
					yya[yyac] = new YYARec(298,-122 );yyac++; 
					yya[yyac] = new YYARec(299,-122 );yyac++; 
					yya[yyac] = new YYARec(300,-122 );yyac++; 
					yya[yyac] = new YYARec(301,-122 );yyac++; 
					yya[yyac] = new YYARec(302,-122 );yyac++; 
					yya[yyac] = new YYARec(303,-122 );yyac++; 
					yya[yyac] = new YYARec(304,-122 );yyac++; 
					yya[yyac] = new YYARec(305,-122 );yyac++; 
					yya[yyac] = new YYARec(306,-122 );yyac++; 
					yya[yyac] = new YYARec(307,-122 );yyac++; 
					yya[yyac] = new YYARec(308,-122 );yyac++; 
					yya[yyac] = new YYARec(309,-122 );yyac++; 
					yya[yyac] = new YYARec(310,-122 );yyac++; 
					yya[yyac] = new YYARec(311,-122 );yyac++; 
					yya[yyac] = new YYARec(312,-122 );yyac++; 
					yya[yyac] = new YYARec(313,-122 );yyac++; 
					yya[yyac] = new YYARec(290,23);yyac++; 
					yya[yyac] = new YYARec(291,45);yyac++; 
					yya[yyac] = new YYARec(292,46);yyac++; 
					yya[yyac] = new YYARec(293,47);yyac++; 
					yya[yyac] = new YYARec(294,48);yyac++; 
					yya[yyac] = new YYARec(295,49);yyac++; 
					yya[yyac] = new YYARec(296,29);yyac++; 
					yya[yyac] = new YYARec(298,50);yyac++; 
					yya[yyac] = new YYARec(299,31);yyac++; 
					yya[yyac] = new YYARec(300,51);yyac++; 
					yya[yyac] = new YYARec(301,52);yyac++; 
					yya[yyac] = new YYARec(302,53);yyac++; 
					yya[yyac] = new YYARec(303,35);yyac++; 
					yya[yyac] = new YYARec(304,54);yyac++; 
					yya[yyac] = new YYARec(305,37);yyac++; 
					yya[yyac] = new YYARec(306,55);yyac++; 
					yya[yyac] = new YYARec(307,56);yyac++; 
					yya[yyac] = new YYARec(308,40);yyac++; 
					yya[yyac] = new YYARec(311,57);yyac++; 
					yya[yyac] = new YYARec(290,23);yyac++; 
					yya[yyac] = new YYARec(291,45);yyac++; 
					yya[yyac] = new YYARec(292,46);yyac++; 
					yya[yyac] = new YYARec(293,47);yyac++; 
					yya[yyac] = new YYARec(294,48);yyac++; 
					yya[yyac] = new YYARec(295,49);yyac++; 
					yya[yyac] = new YYARec(296,29);yyac++; 
					yya[yyac] = new YYARec(298,50);yyac++; 
					yya[yyac] = new YYARec(299,31);yyac++; 
					yya[yyac] = new YYARec(300,51);yyac++; 
					yya[yyac] = new YYARec(301,52);yyac++; 
					yya[yyac] = new YYARec(302,53);yyac++; 
					yya[yyac] = new YYARec(303,35);yyac++; 
					yya[yyac] = new YYARec(304,54);yyac++; 
					yya[yyac] = new YYARec(305,37);yyac++; 
					yya[yyac] = new YYARec(306,55);yyac++; 
					yya[yyac] = new YYARec(307,56);yyac++; 
					yya[yyac] = new YYARec(308,40);yyac++; 
					yya[yyac] = new YYARec(309,63);yyac++; 
					yya[yyac] = new YYARec(311,57);yyac++; 
					yya[yyac] = new YYARec(274,72);yyac++; 
					yya[yyac] = new YYARec(275,73);yyac++; 
					yya[yyac] = new YYARec(279,74);yyac++; 
					yya[yyac] = new YYARec(309,75);yyac++; 
					yya[yyac] = new YYARec(310,76);yyac++; 
					yya[yyac] = new YYARec(311,41);yyac++; 
					yya[yyac] = new YYARec(312,77);yyac++; 
					yya[yyac] = new YYARec(313,78);yyac++; 
					yya[yyac] = new YYARec(290,23);yyac++; 
					yya[yyac] = new YYARec(291,45);yyac++; 
					yya[yyac] = new YYARec(292,46);yyac++; 
					yya[yyac] = new YYARec(293,47);yyac++; 
					yya[yyac] = new YYARec(294,48);yyac++; 
					yya[yyac] = new YYARec(295,49);yyac++; 
					yya[yyac] = new YYARec(296,29);yyac++; 
					yya[yyac] = new YYARec(298,50);yyac++; 
					yya[yyac] = new YYARec(299,31);yyac++; 
					yya[yyac] = new YYARec(300,51);yyac++; 
					yya[yyac] = new YYARec(301,52);yyac++; 
					yya[yyac] = new YYARec(302,53);yyac++; 
					yya[yyac] = new YYARec(303,35);yyac++; 
					yya[yyac] = new YYARec(304,54);yyac++; 
					yya[yyac] = new YYARec(305,37);yyac++; 
					yya[yyac] = new YYARec(306,55);yyac++; 
					yya[yyac] = new YYARec(307,56);yyac++; 
					yya[yyac] = new YYARec(308,40);yyac++; 
					yya[yyac] = new YYARec(311,57);yyac++; 
					yya[yyac] = new YYARec(257,20);yyac++; 
					yya[yyac] = new YYARec(258,21);yyac++; 
					yya[yyac] = new YYARec(259,22);yyac++; 
					yya[yyac] = new YYARec(290,23);yyac++; 
					yya[yyac] = new YYARec(291,24);yyac++; 
					yya[yyac] = new YYARec(292,25);yyac++; 
					yya[yyac] = new YYARec(293,26);yyac++; 
					yya[yyac] = new YYARec(294,27);yyac++; 
					yya[yyac] = new YYARec(295,28);yyac++; 
					yya[yyac] = new YYARec(296,29);yyac++; 
					yya[yyac] = new YYARec(298,30);yyac++; 
					yya[yyac] = new YYARec(299,31);yyac++; 
					yya[yyac] = new YYARec(300,32);yyac++; 
					yya[yyac] = new YYARec(301,33);yyac++; 
					yya[yyac] = new YYARec(302,34);yyac++; 
					yya[yyac] = new YYARec(303,35);yyac++; 
					yya[yyac] = new YYARec(304,36);yyac++; 
					yya[yyac] = new YYARec(305,37);yyac++; 
					yya[yyac] = new YYARec(306,38);yyac++; 
					yya[yyac] = new YYARec(307,39);yyac++; 
					yya[yyac] = new YYARec(308,40);yyac++; 
					yya[yyac] = new YYARec(311,41);yyac++; 
					yya[yyac] = new YYARec(0,-4 );yyac++; 
					yya[yyac] = new YYARec(0,0);yyac++; 
					yya[yyac] = new YYARec(290,-19 );yyac++; 
					yya[yyac] = new YYARec(291,-19 );yyac++; 
					yya[yyac] = new YYARec(292,-19 );yyac++; 
					yya[yyac] = new YYARec(293,-19 );yyac++; 
					yya[yyac] = new YYARec(294,-19 );yyac++; 
					yya[yyac] = new YYARec(295,-19 );yyac++; 
					yya[yyac] = new YYARec(296,-19 );yyac++; 
					yya[yyac] = new YYARec(298,-19 );yyac++; 
					yya[yyac] = new YYARec(299,-19 );yyac++; 
					yya[yyac] = new YYARec(300,-19 );yyac++; 
					yya[yyac] = new YYARec(301,-19 );yyac++; 
					yya[yyac] = new YYARec(302,-19 );yyac++; 
					yya[yyac] = new YYARec(303,-19 );yyac++; 
					yya[yyac] = new YYARec(304,-19 );yyac++; 
					yya[yyac] = new YYARec(305,-19 );yyac++; 
					yya[yyac] = new YYARec(306,-19 );yyac++; 
					yya[yyac] = new YYARec(307,-19 );yyac++; 
					yya[yyac] = new YYARec(308,-19 );yyac++; 
					yya[yyac] = new YYARec(311,-19 );yyac++; 
					yya[yyac] = new YYARec(0,-145 );yyac++; 
					yya[yyac] = new YYARec(289,-145 );yyac++; 
					yya[yyac] = new YYARec(274,-16 );yyac++; 
					yya[yyac] = new YYARec(275,-16 );yyac++; 
					yya[yyac] = new YYARec(279,-16 );yyac++; 
					yya[yyac] = new YYARec(309,-16 );yyac++; 
					yya[yyac] = new YYARec(310,-16 );yyac++; 
					yya[yyac] = new YYARec(311,-16 );yyac++; 
					yya[yyac] = new YYARec(312,-16 );yyac++; 
					yya[yyac] = new YYARec(313,-16 );yyac++; 
					yya[yyac] = new YYARec(0,-147 );yyac++; 
					yya[yyac] = new YYARec(289,-147 );yyac++; 
					yya[yyac] = new YYARec(290,-30 );yyac++; 
					yya[yyac] = new YYARec(291,-30 );yyac++; 
					yya[yyac] = new YYARec(292,-30 );yyac++; 
					yya[yyac] = new YYARec(293,-30 );yyac++; 
					yya[yyac] = new YYARec(294,-30 );yyac++; 
					yya[yyac] = new YYARec(295,-30 );yyac++; 
					yya[yyac] = new YYARec(296,-30 );yyac++; 
					yya[yyac] = new YYARec(298,-30 );yyac++; 
					yya[yyac] = new YYARec(299,-30 );yyac++; 
					yya[yyac] = new YYARec(300,-30 );yyac++; 
					yya[yyac] = new YYARec(301,-30 );yyac++; 
					yya[yyac] = new YYARec(302,-30 );yyac++; 
					yya[yyac] = new YYARec(303,-30 );yyac++; 
					yya[yyac] = new YYARec(304,-30 );yyac++; 
					yya[yyac] = new YYARec(305,-30 );yyac++; 
					yya[yyac] = new YYARec(306,-30 );yyac++; 
					yya[yyac] = new YYARec(307,-30 );yyac++; 
					yya[yyac] = new YYARec(308,-30 );yyac++; 
					yya[yyac] = new YYARec(309,-30 );yyac++; 
					yya[yyac] = new YYARec(311,-30 );yyac++; 
					yya[yyac] = new YYARec(0,-144 );yyac++; 
					yya[yyac] = new YYARec(289,-144 );yyac++; 
					yya[yyac] = new YYARec(290,-34 );yyac++; 
					yya[yyac] = new YYARec(291,-34 );yyac++; 
					yya[yyac] = new YYARec(292,-34 );yyac++; 
					yya[yyac] = new YYARec(293,-34 );yyac++; 
					yya[yyac] = new YYARec(294,-34 );yyac++; 
					yya[yyac] = new YYARec(295,-34 );yyac++; 
					yya[yyac] = new YYARec(296,-34 );yyac++; 
					yya[yyac] = new YYARec(298,-34 );yyac++; 
					yya[yyac] = new YYARec(299,-34 );yyac++; 
					yya[yyac] = new YYARec(300,-34 );yyac++; 
					yya[yyac] = new YYARec(301,-34 );yyac++; 
					yya[yyac] = new YYARec(302,-34 );yyac++; 
					yya[yyac] = new YYARec(303,-34 );yyac++; 
					yya[yyac] = new YYARec(304,-34 );yyac++; 
					yya[yyac] = new YYARec(305,-34 );yyac++; 
					yya[yyac] = new YYARec(306,-34 );yyac++; 
					yya[yyac] = new YYARec(307,-34 );yyac++; 
					yya[yyac] = new YYARec(308,-34 );yyac++; 
					yya[yyac] = new YYARec(311,-34 );yyac++; 
					yya[yyac] = new YYARec(0,-149 );yyac++; 
					yya[yyac] = new YYARec(289,-149 );yyac++; 
					yya[yyac] = new YYARec(290,-48 );yyac++; 
					yya[yyac] = new YYARec(291,-48 );yyac++; 
					yya[yyac] = new YYARec(292,-48 );yyac++; 
					yya[yyac] = new YYARec(293,-48 );yyac++; 
					yya[yyac] = new YYARec(294,-48 );yyac++; 
					yya[yyac] = new YYARec(295,-48 );yyac++; 
					yya[yyac] = new YYARec(296,-48 );yyac++; 
					yya[yyac] = new YYARec(298,-48 );yyac++; 
					yya[yyac] = new YYARec(299,-48 );yyac++; 
					yya[yyac] = new YYARec(300,-48 );yyac++; 
					yya[yyac] = new YYARec(301,-48 );yyac++; 
					yya[yyac] = new YYARec(302,-48 );yyac++; 
					yya[yyac] = new YYARec(303,-48 );yyac++; 
					yya[yyac] = new YYARec(304,-48 );yyac++; 
					yya[yyac] = new YYARec(305,-48 );yyac++; 
					yya[yyac] = new YYARec(306,-48 );yyac++; 
					yya[yyac] = new YYARec(307,-48 );yyac++; 
					yya[yyac] = new YYARec(308,-48 );yyac++; 
					yya[yyac] = new YYARec(311,-48 );yyac++; 
					yya[yyac] = new YYARec(0,-146 );yyac++; 
					yya[yyac] = new YYARec(289,-146 );yyac++; 
					yya[yyac] = new YYARec(274,-139 );yyac++; 
					yya[yyac] = new YYARec(275,-139 );yyac++; 
					yya[yyac] = new YYARec(279,-139 );yyac++; 
					yya[yyac] = new YYARec(309,-139 );yyac++; 
					yya[yyac] = new YYARec(310,-139 );yyac++; 
					yya[yyac] = new YYARec(311,-139 );yyac++; 
					yya[yyac] = new YYARec(312,-139 );yyac++; 
					yya[yyac] = new YYARec(313,-139 );yyac++; 
					yya[yyac] = new YYARec(0,-155 );yyac++; 
					yya[yyac] = new YYARec(289,-155 );yyac++; 
					yya[yyac] = new YYARec(274,-14 );yyac++; 
					yya[yyac] = new YYARec(275,-14 );yyac++; 
					yya[yyac] = new YYARec(279,-14 );yyac++; 
					yya[yyac] = new YYARec(309,-14 );yyac++; 
					yya[yyac] = new YYARec(310,-14 );yyac++; 
					yya[yyac] = new YYARec(311,-14 );yyac++; 
					yya[yyac] = new YYARec(312,-14 );yyac++; 
					yya[yyac] = new YYARec(313,-14 );yyac++; 
					yya[yyac] = new YYARec(0,-141 );yyac++; 
					yya[yyac] = new YYARec(289,-141 );yyac++; 
					yya[yyac] = new YYARec(290,-18 );yyac++; 
					yya[yyac] = new YYARec(291,-18 );yyac++; 
					yya[yyac] = new YYARec(292,-18 );yyac++; 
					yya[yyac] = new YYARec(293,-18 );yyac++; 
					yya[yyac] = new YYARec(294,-18 );yyac++; 
					yya[yyac] = new YYARec(295,-18 );yyac++; 
					yya[yyac] = new YYARec(296,-18 );yyac++; 
					yya[yyac] = new YYARec(298,-18 );yyac++; 
					yya[yyac] = new YYARec(299,-18 );yyac++; 
					yya[yyac] = new YYARec(300,-18 );yyac++; 
					yya[yyac] = new YYARec(301,-18 );yyac++; 
					yya[yyac] = new YYARec(302,-18 );yyac++; 
					yya[yyac] = new YYARec(303,-18 );yyac++; 
					yya[yyac] = new YYARec(304,-18 );yyac++; 
					yya[yyac] = new YYARec(305,-18 );yyac++; 
					yya[yyac] = new YYARec(306,-18 );yyac++; 
					yya[yyac] = new YYARec(307,-18 );yyac++; 
					yya[yyac] = new YYARec(308,-18 );yyac++; 
					yya[yyac] = new YYARec(311,-18 );yyac++; 
					yya[yyac] = new YYARec(0,-140 );yyac++; 
					yya[yyac] = new YYARec(289,-140 );yyac++; 
					yya[yyac] = new YYARec(290,-35 );yyac++; 
					yya[yyac] = new YYARec(291,-35 );yyac++; 
					yya[yyac] = new YYARec(292,-35 );yyac++; 
					yya[yyac] = new YYARec(293,-35 );yyac++; 
					yya[yyac] = new YYARec(294,-35 );yyac++; 
					yya[yyac] = new YYARec(295,-35 );yyac++; 
					yya[yyac] = new YYARec(296,-35 );yyac++; 
					yya[yyac] = new YYARec(298,-35 );yyac++; 
					yya[yyac] = new YYARec(299,-35 );yyac++; 
					yya[yyac] = new YYARec(300,-35 );yyac++; 
					yya[yyac] = new YYARec(301,-35 );yyac++; 
					yya[yyac] = new YYARec(302,-35 );yyac++; 
					yya[yyac] = new YYARec(303,-35 );yyac++; 
					yya[yyac] = new YYARec(304,-35 );yyac++; 
					yya[yyac] = new YYARec(305,-35 );yyac++; 
					yya[yyac] = new YYARec(306,-35 );yyac++; 
					yya[yyac] = new YYARec(307,-35 );yyac++; 
					yya[yyac] = new YYARec(308,-35 );yyac++; 
					yya[yyac] = new YYARec(311,-35 );yyac++; 
					yya[yyac] = new YYARec(0,-143 );yyac++; 
					yya[yyac] = new YYARec(289,-143 );yyac++; 
					yya[yyac] = new YYARec(274,-137 );yyac++; 
					yya[yyac] = new YYARec(275,-137 );yyac++; 
					yya[yyac] = new YYARec(279,-137 );yyac++; 
					yya[yyac] = new YYARec(309,-137 );yyac++; 
					yya[yyac] = new YYARec(310,-137 );yyac++; 
					yya[yyac] = new YYARec(311,-137 );yyac++; 
					yya[yyac] = new YYARec(312,-137 );yyac++; 
					yya[yyac] = new YYARec(313,-137 );yyac++; 
					yya[yyac] = new YYARec(0,-151 );yyac++; 
					yya[yyac] = new YYARec(289,-151 );yyac++; 
					yya[yyac] = new YYARec(274,-138 );yyac++; 
					yya[yyac] = new YYARec(275,-138 );yyac++; 
					yya[yyac] = new YYARec(279,-138 );yyac++; 
					yya[yyac] = new YYARec(309,-138 );yyac++; 
					yya[yyac] = new YYARec(310,-138 );yyac++; 
					yya[yyac] = new YYARec(311,-138 );yyac++; 
					yya[yyac] = new YYARec(312,-138 );yyac++; 
					yya[yyac] = new YYARec(313,-138 );yyac++; 
					yya[yyac] = new YYARec(0,-153 );yyac++; 
					yya[yyac] = new YYARec(289,-153 );yyac++; 
					yya[yyac] = new YYARec(274,-15 );yyac++; 
					yya[yyac] = new YYARec(275,-15 );yyac++; 
					yya[yyac] = new YYARec(279,-15 );yyac++; 
					yya[yyac] = new YYARec(309,-15 );yyac++; 
					yya[yyac] = new YYARec(310,-15 );yyac++; 
					yya[yyac] = new YYARec(311,-15 );yyac++; 
					yya[yyac] = new YYARec(312,-15 );yyac++; 
					yya[yyac] = new YYARec(313,-15 );yyac++; 
					yya[yyac] = new YYARec(0,-150 );yyac++; 
					yya[yyac] = new YYARec(289,-150 );yyac++; 
					yya[yyac] = new YYARec(258,82);yyac++; 
					yya[yyac] = new YYARec(275,83);yyac++; 
					yya[yyac] = new YYARec(257,-135 );yyac++; 
					yya[yyac] = new YYARec(258,-135 );yyac++; 
					yya[yyac] = new YYARec(290,-135 );yyac++; 
					yya[yyac] = new YYARec(291,-135 );yyac++; 
					yya[yyac] = new YYARec(292,-135 );yyac++; 
					yya[yyac] = new YYARec(293,-135 );yyac++; 
					yya[yyac] = new YYARec(294,-135 );yyac++; 
					yya[yyac] = new YYARec(295,-135 );yyac++; 
					yya[yyac] = new YYARec(296,-135 );yyac++; 
					yya[yyac] = new YYARec(298,-135 );yyac++; 
					yya[yyac] = new YYARec(299,-135 );yyac++; 
					yya[yyac] = new YYARec(300,-135 );yyac++; 
					yya[yyac] = new YYARec(301,-135 );yyac++; 
					yya[yyac] = new YYARec(302,-135 );yyac++; 
					yya[yyac] = new YYARec(303,-135 );yyac++; 
					yya[yyac] = new YYARec(304,-135 );yyac++; 
					yya[yyac] = new YYARec(305,-135 );yyac++; 
					yya[yyac] = new YYARec(306,-135 );yyac++; 
					yya[yyac] = new YYARec(307,-135 );yyac++; 
					yya[yyac] = new YYARec(308,-135 );yyac++; 
					yya[yyac] = new YYARec(311,-135 );yyac++; 
					yya[yyac] = new YYARec(312,-135 );yyac++; 
					yya[yyac] = new YYARec(313,-135 );yyac++; 
					yya[yyac] = new YYARec(290,23);yyac++; 
					yya[yyac] = new YYARec(291,45);yyac++; 
					yya[yyac] = new YYARec(292,46);yyac++; 
					yya[yyac] = new YYARec(293,47);yyac++; 
					yya[yyac] = new YYARec(294,48);yyac++; 
					yya[yyac] = new YYARec(295,49);yyac++; 
					yya[yyac] = new YYARec(296,29);yyac++; 
					yya[yyac] = new YYARec(298,50);yyac++; 
					yya[yyac] = new YYARec(299,31);yyac++; 
					yya[yyac] = new YYARec(300,51);yyac++; 
					yya[yyac] = new YYARec(301,52);yyac++; 
					yya[yyac] = new YYARec(302,53);yyac++; 
					yya[yyac] = new YYARec(303,35);yyac++; 
					yya[yyac] = new YYARec(304,54);yyac++; 
					yya[yyac] = new YYARec(305,37);yyac++; 
					yya[yyac] = new YYARec(306,55);yyac++; 
					yya[yyac] = new YYARec(307,56);yyac++; 
					yya[yyac] = new YYARec(308,40);yyac++; 
					yya[yyac] = new YYARec(311,41);yyac++; 
					yya[yyac] = new YYARec(257,86);yyac++; 
					yya[yyac] = new YYARec(258,87);yyac++; 
					yya[yyac] = new YYARec(313,78);yyac++; 
					yya[yyac] = new YYARec(312,77);yyac++; 
					yya[yyac] = new YYARec(309,89);yyac++; 
					yya[yyac] = new YYARec(310,90);yyac++; 
					yya[yyac] = new YYARec(257,91);yyac++; 
					yya[yyac] = new YYARec(290,23);yyac++; 
					yya[yyac] = new YYARec(291,45);yyac++; 
					yya[yyac] = new YYARec(292,46);yyac++; 
					yya[yyac] = new YYARec(293,47);yyac++; 
					yya[yyac] = new YYARec(294,48);yyac++; 
					yya[yyac] = new YYARec(295,49);yyac++; 
					yya[yyac] = new YYARec(296,29);yyac++; 
					yya[yyac] = new YYARec(298,50);yyac++; 
					yya[yyac] = new YYARec(299,31);yyac++; 
					yya[yyac] = new YYARec(300,51);yyac++; 
					yya[yyac] = new YYARec(301,52);yyac++; 
					yya[yyac] = new YYARec(302,53);yyac++; 
					yya[yyac] = new YYARec(303,35);yyac++; 
					yya[yyac] = new YYARec(304,54);yyac++; 
					yya[yyac] = new YYARec(305,37);yyac++; 
					yya[yyac] = new YYARec(306,55);yyac++; 
					yya[yyac] = new YYARec(307,56);yyac++; 
					yya[yyac] = new YYARec(308,40);yyac++; 
					yya[yyac] = new YYARec(311,57);yyac++; 
					yya[yyac] = new YYARec(257,-20 );yyac++; 
					yya[yyac] = new YYARec(257,93);yyac++; 
					yya[yyac] = new YYARec(257,102);yyac++; 
					yya[yyac] = new YYARec(258,103);yyac++; 
					yya[yyac] = new YYARec(280,104);yyac++; 
					yya[yyac] = new YYARec(286,105);yyac++; 
					yya[yyac] = new YYARec(287,106);yyac++; 
					yya[yyac] = new YYARec(288,107);yyac++; 
					yya[yyac] = new YYARec(290,23);yyac++; 
					yya[yyac] = new YYARec(291,45);yyac++; 
					yya[yyac] = new YYARec(292,46);yyac++; 
					yya[yyac] = new YYARec(293,47);yyac++; 
					yya[yyac] = new YYARec(294,48);yyac++; 
					yya[yyac] = new YYARec(295,49);yyac++; 
					yya[yyac] = new YYARec(296,29);yyac++; 
					yya[yyac] = new YYARec(298,50);yyac++; 
					yya[yyac] = new YYARec(299,31);yyac++; 
					yya[yyac] = new YYARec(300,51);yyac++; 
					yya[yyac] = new YYARec(301,52);yyac++; 
					yya[yyac] = new YYARec(302,53);yyac++; 
					yya[yyac] = new YYARec(303,35);yyac++; 
					yya[yyac] = new YYARec(304,54);yyac++; 
					yya[yyac] = new YYARec(305,37);yyac++; 
					yya[yyac] = new YYARec(306,55);yyac++; 
					yya[yyac] = new YYARec(307,56);yyac++; 
					yya[yyac] = new YYARec(308,40);yyac++; 
					yya[yyac] = new YYARec(311,41);yyac++; 
					yya[yyac] = new YYARec(259,-53 );yyac++; 
					yya[yyac] = new YYARec(309,108);yyac++; 
					yya[yyac] = new YYARec(311,109);yyac++; 
					yya[yyac] = new YYARec(257,110);yyac++; 
					yya[yyac] = new YYARec(257,114);yyac++; 
					yya[yyac] = new YYARec(290,23);yyac++; 
					yya[yyac] = new YYARec(291,45);yyac++; 
					yya[yyac] = new YYARec(292,46);yyac++; 
					yya[yyac] = new YYARec(293,47);yyac++; 
					yya[yyac] = new YYARec(294,48);yyac++; 
					yya[yyac] = new YYARec(295,49);yyac++; 
					yya[yyac] = new YYARec(296,29);yyac++; 
					yya[yyac] = new YYARec(298,50);yyac++; 
					yya[yyac] = new YYARec(299,31);yyac++; 
					yya[yyac] = new YYARec(300,51);yyac++; 
					yya[yyac] = new YYARec(301,52);yyac++; 
					yya[yyac] = new YYARec(302,53);yyac++; 
					yya[yyac] = new YYARec(303,35);yyac++; 
					yya[yyac] = new YYARec(304,54);yyac++; 
					yya[yyac] = new YYARec(305,37);yyac++; 
					yya[yyac] = new YYARec(306,55);yyac++; 
					yya[yyac] = new YYARec(307,56);yyac++; 
					yya[yyac] = new YYARec(308,40);yyac++; 
					yya[yyac] = new YYARec(311,41);yyac++; 
					yya[yyac] = new YYARec(259,-38 );yyac++; 
					yya[yyac] = new YYARec(274,72);yyac++; 
					yya[yyac] = new YYARec(275,73);yyac++; 
					yya[yyac] = new YYARec(279,74);yyac++; 
					yya[yyac] = new YYARec(309,75);yyac++; 
					yya[yyac] = new YYARec(257,-28 );yyac++; 
					yya[yyac] = new YYARec(257,119);yyac++; 
					yya[yyac] = new YYARec(257,129);yyac++; 
					yya[yyac] = new YYARec(274,72);yyac++; 
					yya[yyac] = new YYARec(275,73);yyac++; 
					yya[yyac] = new YYARec(279,74);yyac++; 
					yya[yyac] = new YYARec(290,23);yyac++; 
					yya[yyac] = new YYARec(291,45);yyac++; 
					yya[yyac] = new YYARec(292,46);yyac++; 
					yya[yyac] = new YYARec(293,47);yyac++; 
					yya[yyac] = new YYARec(294,48);yyac++; 
					yya[yyac] = new YYARec(295,49);yyac++; 
					yya[yyac] = new YYARec(296,29);yyac++; 
					yya[yyac] = new YYARec(297,130);yyac++; 
					yya[yyac] = new YYARec(298,50);yyac++; 
					yya[yyac] = new YYARec(299,31);yyac++; 
					yya[yyac] = new YYARec(300,51);yyac++; 
					yya[yyac] = new YYARec(301,52);yyac++; 
					yya[yyac] = new YYARec(302,53);yyac++; 
					yya[yyac] = new YYARec(303,35);yyac++; 
					yya[yyac] = new YYARec(304,54);yyac++; 
					yya[yyac] = new YYARec(305,37);yyac++; 
					yya[yyac] = new YYARec(306,55);yyac++; 
					yya[yyac] = new YYARec(307,56);yyac++; 
					yya[yyac] = new YYARec(308,40);yyac++; 
					yya[yyac] = new YYARec(309,75);yyac++; 
					yya[yyac] = new YYARec(310,76);yyac++; 
					yya[yyac] = new YYARec(311,41);yyac++; 
					yya[yyac] = new YYARec(312,77);yyac++; 
					yya[yyac] = new YYARec(313,78);yyac++; 
					yya[yyac] = new YYARec(257,102);yyac++; 
					yya[yyac] = new YYARec(258,103);yyac++; 
					yya[yyac] = new YYARec(280,104);yyac++; 
					yya[yyac] = new YYARec(286,105);yyac++; 
					yya[yyac] = new YYARec(287,106);yyac++; 
					yya[yyac] = new YYARec(288,107);yyac++; 
					yya[yyac] = new YYARec(290,23);yyac++; 
					yya[yyac] = new YYARec(291,45);yyac++; 
					yya[yyac] = new YYARec(292,46);yyac++; 
					yya[yyac] = new YYARec(293,47);yyac++; 
					yya[yyac] = new YYARec(294,48);yyac++; 
					yya[yyac] = new YYARec(295,49);yyac++; 
					yya[yyac] = new YYARec(296,29);yyac++; 
					yya[yyac] = new YYARec(298,50);yyac++; 
					yya[yyac] = new YYARec(299,31);yyac++; 
					yya[yyac] = new YYARec(300,51);yyac++; 
					yya[yyac] = new YYARec(301,52);yyac++; 
					yya[yyac] = new YYARec(302,53);yyac++; 
					yya[yyac] = new YYARec(303,35);yyac++; 
					yya[yyac] = new YYARec(304,54);yyac++; 
					yya[yyac] = new YYARec(305,37);yyac++; 
					yya[yyac] = new YYARec(306,55);yyac++; 
					yya[yyac] = new YYARec(307,56);yyac++; 
					yya[yyac] = new YYARec(308,40);yyac++; 
					yya[yyac] = new YYARec(311,41);yyac++; 
					yya[yyac] = new YYARec(259,-53 );yyac++; 
					yya[yyac] = new YYARec(257,102);yyac++; 
					yya[yyac] = new YYARec(258,103);yyac++; 
					yya[yyac] = new YYARec(280,104);yyac++; 
					yya[yyac] = new YYARec(286,105);yyac++; 
					yya[yyac] = new YYARec(287,106);yyac++; 
					yya[yyac] = new YYARec(288,107);yyac++; 
					yya[yyac] = new YYARec(290,23);yyac++; 
					yya[yyac] = new YYARec(291,45);yyac++; 
					yya[yyac] = new YYARec(292,46);yyac++; 
					yya[yyac] = new YYARec(293,47);yyac++; 
					yya[yyac] = new YYARec(294,48);yyac++; 
					yya[yyac] = new YYARec(295,49);yyac++; 
					yya[yyac] = new YYARec(296,29);yyac++; 
					yya[yyac] = new YYARec(298,50);yyac++; 
					yya[yyac] = new YYARec(299,31);yyac++; 
					yya[yyac] = new YYARec(300,51);yyac++; 
					yya[yyac] = new YYARec(301,52);yyac++; 
					yya[yyac] = new YYARec(302,53);yyac++; 
					yya[yyac] = new YYARec(303,35);yyac++; 
					yya[yyac] = new YYARec(304,54);yyac++; 
					yya[yyac] = new YYARec(305,37);yyac++; 
					yya[yyac] = new YYARec(306,55);yyac++; 
					yya[yyac] = new YYARec(307,56);yyac++; 
					yya[yyac] = new YYARec(308,40);yyac++; 
					yya[yyac] = new YYARec(311,41);yyac++; 
					yya[yyac] = new YYARec(259,-53 );yyac++; 
					yya[yyac] = new YYARec(260,133);yyac++; 
					yya[yyac] = new YYARec(259,134);yyac++; 
					yya[yyac] = new YYARec(289,58);yyac++; 
					yya[yyac] = new YYARec(281,-122 );yyac++; 
					yya[yyac] = new YYARec(282,-122 );yyac++; 
					yya[yyac] = new YYARec(283,-122 );yyac++; 
					yya[yyac] = new YYARec(284,-122 );yyac++; 
					yya[yyac] = new YYARec(285,-122 );yyac++; 
					yya[yyac] = new YYARec(257,-126 );yyac++; 
					yya[yyac] = new YYARec(274,-126 );yyac++; 
					yya[yyac] = new YYARec(275,-126 );yyac++; 
					yya[yyac] = new YYARec(279,-126 );yyac++; 
					yya[yyac] = new YYARec(290,-126 );yyac++; 
					yya[yyac] = new YYARec(291,-126 );yyac++; 
					yya[yyac] = new YYARec(292,-126 );yyac++; 
					yya[yyac] = new YYARec(293,-126 );yyac++; 
					yya[yyac] = new YYARec(294,-126 );yyac++; 
					yya[yyac] = new YYARec(295,-126 );yyac++; 
					yya[yyac] = new YYARec(296,-126 );yyac++; 
					yya[yyac] = new YYARec(297,-126 );yyac++; 
					yya[yyac] = new YYARec(298,-126 );yyac++; 
					yya[yyac] = new YYARec(299,-126 );yyac++; 
					yya[yyac] = new YYARec(300,-126 );yyac++; 
					yya[yyac] = new YYARec(301,-126 );yyac++; 
					yya[yyac] = new YYARec(302,-126 );yyac++; 
					yya[yyac] = new YYARec(303,-126 );yyac++; 
					yya[yyac] = new YYARec(304,-126 );yyac++; 
					yya[yyac] = new YYARec(305,-126 );yyac++; 
					yya[yyac] = new YYARec(306,-126 );yyac++; 
					yya[yyac] = new YYARec(307,-126 );yyac++; 
					yya[yyac] = new YYARec(308,-126 );yyac++; 
					yya[yyac] = new YYARec(309,-126 );yyac++; 
					yya[yyac] = new YYARec(310,-126 );yyac++; 
					yya[yyac] = new YYARec(311,-126 );yyac++; 
					yya[yyac] = new YYARec(312,-126 );yyac++; 
					yya[yyac] = new YYARec(313,-126 );yyac++; 
					yya[yyac] = new YYARec(260,-168 );yyac++; 
					yya[yyac] = new YYARec(281,136);yyac++; 
					yya[yyac] = new YYARec(282,137);yyac++; 
					yya[yyac] = new YYARec(283,138);yyac++; 
					yya[yyac] = new YYARec(284,139);yyac++; 
					yya[yyac] = new YYARec(285,140);yyac++; 
					yya[yyac] = new YYARec(257,102);yyac++; 
					yya[yyac] = new YYARec(258,103);yyac++; 
					yya[yyac] = new YYARec(280,104);yyac++; 
					yya[yyac] = new YYARec(286,105);yyac++; 
					yya[yyac] = new YYARec(287,106);yyac++; 
					yya[yyac] = new YYARec(288,107);yyac++; 
					yya[yyac] = new YYARec(290,23);yyac++; 
					yya[yyac] = new YYARec(291,45);yyac++; 
					yya[yyac] = new YYARec(292,46);yyac++; 
					yya[yyac] = new YYARec(293,47);yyac++; 
					yya[yyac] = new YYARec(294,48);yyac++; 
					yya[yyac] = new YYARec(295,49);yyac++; 
					yya[yyac] = new YYARec(296,29);yyac++; 
					yya[yyac] = new YYARec(298,50);yyac++; 
					yya[yyac] = new YYARec(299,31);yyac++; 
					yya[yyac] = new YYARec(300,51);yyac++; 
					yya[yyac] = new YYARec(301,52);yyac++; 
					yya[yyac] = new YYARec(302,53);yyac++; 
					yya[yyac] = new YYARec(303,35);yyac++; 
					yya[yyac] = new YYARec(304,54);yyac++; 
					yya[yyac] = new YYARec(305,37);yyac++; 
					yya[yyac] = new YYARec(306,55);yyac++; 
					yya[yyac] = new YYARec(307,56);yyac++; 
					yya[yyac] = new YYARec(308,40);yyac++; 
					yya[yyac] = new YYARec(311,41);yyac++; 
					yya[yyac] = new YYARec(259,-53 );yyac++; 
					yya[yyac] = new YYARec(266,159);yyac++; 
					yya[yyac] = new YYARec(274,72);yyac++; 
					yya[yyac] = new YYARec(275,73);yyac++; 
					yya[yyac] = new YYARec(279,74);yyac++; 
					yya[yyac] = new YYARec(290,23);yyac++; 
					yya[yyac] = new YYARec(291,45);yyac++; 
					yya[yyac] = new YYARec(292,46);yyac++; 
					yya[yyac] = new YYARec(293,47);yyac++; 
					yya[yyac] = new YYARec(294,48);yyac++; 
					yya[yyac] = new YYARec(295,49);yyac++; 
					yya[yyac] = new YYARec(296,160);yyac++; 
					yya[yyac] = new YYARec(297,130);yyac++; 
					yya[yyac] = new YYARec(298,50);yyac++; 
					yya[yyac] = new YYARec(299,31);yyac++; 
					yya[yyac] = new YYARec(300,51);yyac++; 
					yya[yyac] = new YYARec(301,52);yyac++; 
					yya[yyac] = new YYARec(302,53);yyac++; 
					yya[yyac] = new YYARec(303,161);yyac++; 
					yya[yyac] = new YYARec(304,162);yyac++; 
					yya[yyac] = new YYARec(305,37);yyac++; 
					yya[yyac] = new YYARec(306,55);yyac++; 
					yya[yyac] = new YYARec(307,56);yyac++; 
					yya[yyac] = new YYARec(308,40);yyac++; 
					yya[yyac] = new YYARec(309,163);yyac++; 
					yya[yyac] = new YYARec(310,164);yyac++; 
					yya[yyac] = new YYARec(311,41);yyac++; 
					yya[yyac] = new YYARec(258,165);yyac++; 
					yya[yyac] = new YYARec(257,-125 );yyac++; 
					yya[yyac] = new YYARec(274,-125 );yyac++; 
					yya[yyac] = new YYARec(275,-125 );yyac++; 
					yya[yyac] = new YYARec(279,-125 );yyac++; 
					yya[yyac] = new YYARec(290,-125 );yyac++; 
					yya[yyac] = new YYARec(291,-125 );yyac++; 
					yya[yyac] = new YYARec(292,-125 );yyac++; 
					yya[yyac] = new YYARec(293,-125 );yyac++; 
					yya[yyac] = new YYARec(294,-125 );yyac++; 
					yya[yyac] = new YYARec(295,-125 );yyac++; 
					yya[yyac] = new YYARec(296,-125 );yyac++; 
					yya[yyac] = new YYARec(297,-125 );yyac++; 
					yya[yyac] = new YYARec(298,-125 );yyac++; 
					yya[yyac] = new YYARec(299,-125 );yyac++; 
					yya[yyac] = new YYARec(300,-125 );yyac++; 
					yya[yyac] = new YYARec(301,-125 );yyac++; 
					yya[yyac] = new YYARec(302,-125 );yyac++; 
					yya[yyac] = new YYARec(303,-125 );yyac++; 
					yya[yyac] = new YYARec(304,-125 );yyac++; 
					yya[yyac] = new YYARec(305,-125 );yyac++; 
					yya[yyac] = new YYARec(306,-125 );yyac++; 
					yya[yyac] = new YYARec(307,-125 );yyac++; 
					yya[yyac] = new YYARec(308,-125 );yyac++; 
					yya[yyac] = new YYARec(309,-125 );yyac++; 
					yya[yyac] = new YYARec(310,-125 );yyac++; 
					yya[yyac] = new YYARec(311,-125 );yyac++; 
					yya[yyac] = new YYARec(312,-125 );yyac++; 
					yya[yyac] = new YYARec(313,-125 );yyac++; 
					yya[yyac] = new YYARec(266,159);yyac++; 
					yya[yyac] = new YYARec(274,72);yyac++; 
					yya[yyac] = new YYARec(275,73);yyac++; 
					yya[yyac] = new YYARec(279,74);yyac++; 
					yya[yyac] = new YYARec(290,23);yyac++; 
					yya[yyac] = new YYARec(291,45);yyac++; 
					yya[yyac] = new YYARec(292,46);yyac++; 
					yya[yyac] = new YYARec(293,47);yyac++; 
					yya[yyac] = new YYARec(294,48);yyac++; 
					yya[yyac] = new YYARec(295,49);yyac++; 
					yya[yyac] = new YYARec(296,160);yyac++; 
					yya[yyac] = new YYARec(297,130);yyac++; 
					yya[yyac] = new YYARec(298,50);yyac++; 
					yya[yyac] = new YYARec(299,31);yyac++; 
					yya[yyac] = new YYARec(300,51);yyac++; 
					yya[yyac] = new YYARec(301,52);yyac++; 
					yya[yyac] = new YYARec(302,53);yyac++; 
					yya[yyac] = new YYARec(303,161);yyac++; 
					yya[yyac] = new YYARec(304,162);yyac++; 
					yya[yyac] = new YYARec(305,37);yyac++; 
					yya[yyac] = new YYARec(306,55);yyac++; 
					yya[yyac] = new YYARec(307,56);yyac++; 
					yya[yyac] = new YYARec(308,40);yyac++; 
					yya[yyac] = new YYARec(309,163);yyac++; 
					yya[yyac] = new YYARec(310,164);yyac++; 
					yya[yyac] = new YYARec(311,41);yyac++; 
					yya[yyac] = new YYARec(266,159);yyac++; 
					yya[yyac] = new YYARec(274,72);yyac++; 
					yya[yyac] = new YYARec(275,73);yyac++; 
					yya[yyac] = new YYARec(279,74);yyac++; 
					yya[yyac] = new YYARec(290,23);yyac++; 
					yya[yyac] = new YYARec(291,45);yyac++; 
					yya[yyac] = new YYARec(292,46);yyac++; 
					yya[yyac] = new YYARec(293,47);yyac++; 
					yya[yyac] = new YYARec(294,48);yyac++; 
					yya[yyac] = new YYARec(295,49);yyac++; 
					yya[yyac] = new YYARec(296,160);yyac++; 
					yya[yyac] = new YYARec(297,130);yyac++; 
					yya[yyac] = new YYARec(298,50);yyac++; 
					yya[yyac] = new YYARec(299,31);yyac++; 
					yya[yyac] = new YYARec(300,51);yyac++; 
					yya[yyac] = new YYARec(301,52);yyac++; 
					yya[yyac] = new YYARec(302,53);yyac++; 
					yya[yyac] = new YYARec(303,161);yyac++; 
					yya[yyac] = new YYARec(304,162);yyac++; 
					yya[yyac] = new YYARec(305,37);yyac++; 
					yya[yyac] = new YYARec(306,55);yyac++; 
					yya[yyac] = new YYARec(307,56);yyac++; 
					yya[yyac] = new YYARec(308,40);yyac++; 
					yya[yyac] = new YYARec(309,163);yyac++; 
					yya[yyac] = new YYARec(310,164);yyac++; 
					yya[yyac] = new YYARec(311,41);yyac++; 
					yya[yyac] = new YYARec(274,72);yyac++; 
					yya[yyac] = new YYARec(275,73);yyac++; 
					yya[yyac] = new YYARec(279,74);yyac++; 
					yya[yyac] = new YYARec(290,23);yyac++; 
					yya[yyac] = new YYARec(291,45);yyac++; 
					yya[yyac] = new YYARec(292,46);yyac++; 
					yya[yyac] = new YYARec(293,47);yyac++; 
					yya[yyac] = new YYARec(294,48);yyac++; 
					yya[yyac] = new YYARec(295,49);yyac++; 
					yya[yyac] = new YYARec(296,29);yyac++; 
					yya[yyac] = new YYARec(298,50);yyac++; 
					yya[yyac] = new YYARec(299,31);yyac++; 
					yya[yyac] = new YYARec(300,51);yyac++; 
					yya[yyac] = new YYARec(301,52);yyac++; 
					yya[yyac] = new YYARec(302,53);yyac++; 
					yya[yyac] = new YYARec(303,35);yyac++; 
					yya[yyac] = new YYARec(304,54);yyac++; 
					yya[yyac] = new YYARec(305,37);yyac++; 
					yya[yyac] = new YYARec(306,55);yyac++; 
					yya[yyac] = new YYARec(307,56);yyac++; 
					yya[yyac] = new YYARec(308,40);yyac++; 
					yya[yyac] = new YYARec(309,75);yyac++; 
					yya[yyac] = new YYARec(310,76);yyac++; 
					yya[yyac] = new YYARec(311,41);yyac++; 
					yya[yyac] = new YYARec(312,77);yyac++; 
					yya[yyac] = new YYARec(313,78);yyac++; 
					yya[yyac] = new YYARec(257,114);yyac++; 
					yya[yyac] = new YYARec(290,23);yyac++; 
					yya[yyac] = new YYARec(291,45);yyac++; 
					yya[yyac] = new YYARec(292,46);yyac++; 
					yya[yyac] = new YYARec(293,47);yyac++; 
					yya[yyac] = new YYARec(294,48);yyac++; 
					yya[yyac] = new YYARec(295,49);yyac++; 
					yya[yyac] = new YYARec(296,29);yyac++; 
					yya[yyac] = new YYARec(298,50);yyac++; 
					yya[yyac] = new YYARec(299,31);yyac++; 
					yya[yyac] = new YYARec(300,51);yyac++; 
					yya[yyac] = new YYARec(301,52);yyac++; 
					yya[yyac] = new YYARec(302,53);yyac++; 
					yya[yyac] = new YYARec(303,35);yyac++; 
					yya[yyac] = new YYARec(304,54);yyac++; 
					yya[yyac] = new YYARec(305,37);yyac++; 
					yya[yyac] = new YYARec(306,55);yyac++; 
					yya[yyac] = new YYARec(307,56);yyac++; 
					yya[yyac] = new YYARec(308,40);yyac++; 
					yya[yyac] = new YYARec(311,41);yyac++; 
					yya[yyac] = new YYARec(259,-38 );yyac++; 
					yya[yyac] = new YYARec(259,176);yyac++; 
					yya[yyac] = new YYARec(309,89);yyac++; 
					yya[yyac] = new YYARec(274,72);yyac++; 
					yya[yyac] = new YYARec(275,73);yyac++; 
					yya[yyac] = new YYARec(279,74);yyac++; 
					yya[yyac] = new YYARec(309,75);yyac++; 
					yya[yyac] = new YYARec(257,-28 );yyac++; 
					yya[yyac] = new YYARec(257,178);yyac++; 
					yya[yyac] = new YYARec(290,23);yyac++; 
					yya[yyac] = new YYARec(291,45);yyac++; 
					yya[yyac] = new YYARec(292,46);yyac++; 
					yya[yyac] = new YYARec(293,47);yyac++; 
					yya[yyac] = new YYARec(294,48);yyac++; 
					yya[yyac] = new YYARec(295,49);yyac++; 
					yya[yyac] = new YYARec(296,29);yyac++; 
					yya[yyac] = new YYARec(298,50);yyac++; 
					yya[yyac] = new YYARec(299,31);yyac++; 
					yya[yyac] = new YYARec(300,51);yyac++; 
					yya[yyac] = new YYARec(301,52);yyac++; 
					yya[yyac] = new YYARec(302,53);yyac++; 
					yya[yyac] = new YYARec(303,35);yyac++; 
					yya[yyac] = new YYARec(304,54);yyac++; 
					yya[yyac] = new YYARec(305,37);yyac++; 
					yya[yyac] = new YYARec(306,55);yyac++; 
					yya[yyac] = new YYARec(307,56);yyac++; 
					yya[yyac] = new YYARec(308,40);yyac++; 
					yya[yyac] = new YYARec(309,89);yyac++; 
					yya[yyac] = new YYARec(310,90);yyac++; 
					yya[yyac] = new YYARec(311,41);yyac++; 
					yya[yyac] = new YYARec(274,72);yyac++; 
					yya[yyac] = new YYARec(275,73);yyac++; 
					yya[yyac] = new YYARec(279,74);yyac++; 
					yya[yyac] = new YYARec(290,23);yyac++; 
					yya[yyac] = new YYARec(291,45);yyac++; 
					yya[yyac] = new YYARec(292,46);yyac++; 
					yya[yyac] = new YYARec(293,47);yyac++; 
					yya[yyac] = new YYARec(294,48);yyac++; 
					yya[yyac] = new YYARec(295,49);yyac++; 
					yya[yyac] = new YYARec(296,29);yyac++; 
					yya[yyac] = new YYARec(297,130);yyac++; 
					yya[yyac] = new YYARec(298,50);yyac++; 
					yya[yyac] = new YYARec(299,31);yyac++; 
					yya[yyac] = new YYARec(300,51);yyac++; 
					yya[yyac] = new YYARec(301,52);yyac++; 
					yya[yyac] = new YYARec(302,53);yyac++; 
					yya[yyac] = new YYARec(303,35);yyac++; 
					yya[yyac] = new YYARec(304,54);yyac++; 
					yya[yyac] = new YYARec(305,37);yyac++; 
					yya[yyac] = new YYARec(306,55);yyac++; 
					yya[yyac] = new YYARec(307,56);yyac++; 
					yya[yyac] = new YYARec(308,40);yyac++; 
					yya[yyac] = new YYARec(309,75);yyac++; 
					yya[yyac] = new YYARec(310,76);yyac++; 
					yya[yyac] = new YYARec(311,41);yyac++; 
					yya[yyac] = new YYARec(312,77);yyac++; 
					yya[yyac] = new YYARec(313,78);yyac++; 
					yya[yyac] = new YYARec(257,-58 );yyac++; 
					yya[yyac] = new YYARec(257,181);yyac++; 
					yya[yyac] = new YYARec(257,102);yyac++; 
					yya[yyac] = new YYARec(258,103);yyac++; 
					yya[yyac] = new YYARec(280,104);yyac++; 
					yya[yyac] = new YYARec(286,105);yyac++; 
					yya[yyac] = new YYARec(287,106);yyac++; 
					yya[yyac] = new YYARec(288,107);yyac++; 
					yya[yyac] = new YYARec(290,23);yyac++; 
					yya[yyac] = new YYARec(291,45);yyac++; 
					yya[yyac] = new YYARec(292,46);yyac++; 
					yya[yyac] = new YYARec(293,47);yyac++; 
					yya[yyac] = new YYARec(294,48);yyac++; 
					yya[yyac] = new YYARec(295,49);yyac++; 
					yya[yyac] = new YYARec(296,29);yyac++; 
					yya[yyac] = new YYARec(298,50);yyac++; 
					yya[yyac] = new YYARec(299,31);yyac++; 
					yya[yyac] = new YYARec(300,51);yyac++; 
					yya[yyac] = new YYARec(301,52);yyac++; 
					yya[yyac] = new YYARec(302,53);yyac++; 
					yya[yyac] = new YYARec(303,35);yyac++; 
					yya[yyac] = new YYARec(304,54);yyac++; 
					yya[yyac] = new YYARec(305,37);yyac++; 
					yya[yyac] = new YYARec(306,55);yyac++; 
					yya[yyac] = new YYARec(307,56);yyac++; 
					yya[yyac] = new YYARec(308,40);yyac++; 
					yya[yyac] = new YYARec(311,41);yyac++; 
					yya[yyac] = new YYARec(259,-53 );yyac++; 
					yya[yyac] = new YYARec(266,159);yyac++; 
					yya[yyac] = new YYARec(274,72);yyac++; 
					yya[yyac] = new YYARec(275,73);yyac++; 
					yya[yyac] = new YYARec(279,74);yyac++; 
					yya[yyac] = new YYARec(290,23);yyac++; 
					yya[yyac] = new YYARec(291,45);yyac++; 
					yya[yyac] = new YYARec(292,46);yyac++; 
					yya[yyac] = new YYARec(293,47);yyac++; 
					yya[yyac] = new YYARec(294,48);yyac++; 
					yya[yyac] = new YYARec(295,49);yyac++; 
					yya[yyac] = new YYARec(296,160);yyac++; 
					yya[yyac] = new YYARec(297,130);yyac++; 
					yya[yyac] = new YYARec(298,50);yyac++; 
					yya[yyac] = new YYARec(299,31);yyac++; 
					yya[yyac] = new YYARec(300,51);yyac++; 
					yya[yyac] = new YYARec(301,52);yyac++; 
					yya[yyac] = new YYARec(302,53);yyac++; 
					yya[yyac] = new YYARec(303,161);yyac++; 
					yya[yyac] = new YYARec(304,162);yyac++; 
					yya[yyac] = new YYARec(305,37);yyac++; 
					yya[yyac] = new YYARec(306,55);yyac++; 
					yya[yyac] = new YYARec(307,56);yyac++; 
					yya[yyac] = new YYARec(308,40);yyac++; 
					yya[yyac] = new YYARec(309,163);yyac++; 
					yya[yyac] = new YYARec(310,164);yyac++; 
					yya[yyac] = new YYARec(311,41);yyac++; 
					yya[yyac] = new YYARec(259,184);yyac++; 
					yya[yyac] = new YYARec(266,185);yyac++; 
					yya[yyac] = new YYARec(266,159);yyac++; 
					yya[yyac] = new YYARec(274,72);yyac++; 
					yya[yyac] = new YYARec(275,73);yyac++; 
					yya[yyac] = new YYARec(279,74);yyac++; 
					yya[yyac] = new YYARec(290,23);yyac++; 
					yya[yyac] = new YYARec(291,45);yyac++; 
					yya[yyac] = new YYARec(292,46);yyac++; 
					yya[yyac] = new YYARec(293,47);yyac++; 
					yya[yyac] = new YYARec(294,48);yyac++; 
					yya[yyac] = new YYARec(295,49);yyac++; 
					yya[yyac] = new YYARec(296,160);yyac++; 
					yya[yyac] = new YYARec(297,130);yyac++; 
					yya[yyac] = new YYARec(298,50);yyac++; 
					yya[yyac] = new YYARec(299,31);yyac++; 
					yya[yyac] = new YYARec(300,51);yyac++; 
					yya[yyac] = new YYARec(301,52);yyac++; 
					yya[yyac] = new YYARec(302,53);yyac++; 
					yya[yyac] = new YYARec(303,161);yyac++; 
					yya[yyac] = new YYARec(304,162);yyac++; 
					yya[yyac] = new YYARec(305,37);yyac++; 
					yya[yyac] = new YYARec(306,55);yyac++; 
					yya[yyac] = new YYARec(307,56);yyac++; 
					yya[yyac] = new YYARec(308,40);yyac++; 
					yya[yyac] = new YYARec(309,163);yyac++; 
					yya[yyac] = new YYARec(310,164);yyac++; 
					yya[yyac] = new YYARec(311,41);yyac++; 
					yya[yyac] = new YYARec(276,188);yyac++; 
					yya[yyac] = new YYARec(277,189);yyac++; 
					yya[yyac] = new YYARec(278,190);yyac++; 
					yya[yyac] = new YYARec(257,-80 );yyac++; 
					yya[yyac] = new YYARec(258,-80 );yyac++; 
					yya[yyac] = new YYARec(261,-80 );yyac++; 
					yya[yyac] = new YYARec(262,-80 );yyac++; 
					yya[yyac] = new YYARec(263,-80 );yyac++; 
					yya[yyac] = new YYARec(264,-80 );yyac++; 
					yya[yyac] = new YYARec(265,-80 );yyac++; 
					yya[yyac] = new YYARec(267,-80 );yyac++; 
					yya[yyac] = new YYARec(268,-80 );yyac++; 
					yya[yyac] = new YYARec(269,-80 );yyac++; 
					yya[yyac] = new YYARec(270,-80 );yyac++; 
					yya[yyac] = new YYARec(271,-80 );yyac++; 
					yya[yyac] = new YYARec(272,-80 );yyac++; 
					yya[yyac] = new YYARec(273,-80 );yyac++; 
					yya[yyac] = new YYARec(274,-80 );yyac++; 
					yya[yyac] = new YYARec(275,-80 );yyac++; 
					yya[yyac] = new YYARec(274,192);yyac++; 
					yya[yyac] = new YYARec(275,193);yyac++; 
					yya[yyac] = new YYARec(257,-78 );yyac++; 
					yya[yyac] = new YYARec(258,-78 );yyac++; 
					yya[yyac] = new YYARec(261,-78 );yyac++; 
					yya[yyac] = new YYARec(262,-78 );yyac++; 
					yya[yyac] = new YYARec(263,-78 );yyac++; 
					yya[yyac] = new YYARec(264,-78 );yyac++; 
					yya[yyac] = new YYARec(265,-78 );yyac++; 
					yya[yyac] = new YYARec(267,-78 );yyac++; 
					yya[yyac] = new YYARec(268,-78 );yyac++; 
					yya[yyac] = new YYARec(269,-78 );yyac++; 
					yya[yyac] = new YYARec(270,-78 );yyac++; 
					yya[yyac] = new YYARec(271,-78 );yyac++; 
					yya[yyac] = new YYARec(272,-78 );yyac++; 
					yya[yyac] = new YYARec(273,-78 );yyac++; 
					yya[yyac] = new YYARec(270,195);yyac++; 
					yya[yyac] = new YYARec(271,196);yyac++; 
					yya[yyac] = new YYARec(272,197);yyac++; 
					yya[yyac] = new YYARec(273,198);yyac++; 
					yya[yyac] = new YYARec(257,-76 );yyac++; 
					yya[yyac] = new YYARec(258,-76 );yyac++; 
					yya[yyac] = new YYARec(261,-76 );yyac++; 
					yya[yyac] = new YYARec(262,-76 );yyac++; 
					yya[yyac] = new YYARec(263,-76 );yyac++; 
					yya[yyac] = new YYARec(264,-76 );yyac++; 
					yya[yyac] = new YYARec(265,-76 );yyac++; 
					yya[yyac] = new YYARec(267,-76 );yyac++; 
					yya[yyac] = new YYARec(268,-76 );yyac++; 
					yya[yyac] = new YYARec(269,-76 );yyac++; 
					yya[yyac] = new YYARec(268,200);yyac++; 
					yya[yyac] = new YYARec(269,201);yyac++; 
					yya[yyac] = new YYARec(257,-75 );yyac++; 
					yya[yyac] = new YYARec(258,-75 );yyac++; 
					yya[yyac] = new YYARec(261,-75 );yyac++; 
					yya[yyac] = new YYARec(262,-75 );yyac++; 
					yya[yyac] = new YYARec(263,-75 );yyac++; 
					yya[yyac] = new YYARec(264,-75 );yyac++; 
					yya[yyac] = new YYARec(265,-75 );yyac++; 
					yya[yyac] = new YYARec(267,-75 );yyac++; 
					yya[yyac] = new YYARec(265,202);yyac++; 
					yya[yyac] = new YYARec(257,-73 );yyac++; 
					yya[yyac] = new YYARec(258,-73 );yyac++; 
					yya[yyac] = new YYARec(261,-73 );yyac++; 
					yya[yyac] = new YYARec(262,-73 );yyac++; 
					yya[yyac] = new YYARec(263,-73 );yyac++; 
					yya[yyac] = new YYARec(264,-73 );yyac++; 
					yya[yyac] = new YYARec(267,-73 );yyac++; 
					yya[yyac] = new YYARec(264,203);yyac++; 
					yya[yyac] = new YYARec(257,-71 );yyac++; 
					yya[yyac] = new YYARec(258,-71 );yyac++; 
					yya[yyac] = new YYARec(261,-71 );yyac++; 
					yya[yyac] = new YYARec(262,-71 );yyac++; 
					yya[yyac] = new YYARec(263,-71 );yyac++; 
					yya[yyac] = new YYARec(267,-71 );yyac++; 
					yya[yyac] = new YYARec(263,204);yyac++; 
					yya[yyac] = new YYARec(257,-69 );yyac++; 
					yya[yyac] = new YYARec(258,-69 );yyac++; 
					yya[yyac] = new YYARec(261,-69 );yyac++; 
					yya[yyac] = new YYARec(262,-69 );yyac++; 
					yya[yyac] = new YYARec(267,-69 );yyac++; 
					yya[yyac] = new YYARec(262,205);yyac++; 
					yya[yyac] = new YYARec(257,-67 );yyac++; 
					yya[yyac] = new YYARec(258,-67 );yyac++; 
					yya[yyac] = new YYARec(261,-67 );yyac++; 
					yya[yyac] = new YYARec(267,-67 );yyac++; 
					yya[yyac] = new YYARec(261,206);yyac++; 
					yya[yyac] = new YYARec(257,-65 );yyac++; 
					yya[yyac] = new YYARec(258,-65 );yyac++; 
					yya[yyac] = new YYARec(267,-65 );yyac++; 
					yya[yyac] = new YYARec(281,136);yyac++; 
					yya[yyac] = new YYARec(282,137);yyac++; 
					yya[yyac] = new YYARec(283,138);yyac++; 
					yya[yyac] = new YYARec(284,139);yyac++; 
					yya[yyac] = new YYARec(285,140);yyac++; 
					yya[yyac] = new YYARec(309,208);yyac++; 
					yya[yyac] = new YYARec(257,-91 );yyac++; 
					yya[yyac] = new YYARec(261,-91 );yyac++; 
					yya[yyac] = new YYARec(262,-91 );yyac++; 
					yya[yyac] = new YYARec(263,-91 );yyac++; 
					yya[yyac] = new YYARec(264,-91 );yyac++; 
					yya[yyac] = new YYARec(265,-91 );yyac++; 
					yya[yyac] = new YYARec(268,-91 );yyac++; 
					yya[yyac] = new YYARec(269,-91 );yyac++; 
					yya[yyac] = new YYARec(270,-91 );yyac++; 
					yya[yyac] = new YYARec(271,-91 );yyac++; 
					yya[yyac] = new YYARec(272,-91 );yyac++; 
					yya[yyac] = new YYARec(273,-91 );yyac++; 
					yya[yyac] = new YYARec(274,-91 );yyac++; 
					yya[yyac] = new YYARec(275,-91 );yyac++; 
					yya[yyac] = new YYARec(276,-91 );yyac++; 
					yya[yyac] = new YYARec(277,-91 );yyac++; 
					yya[yyac] = new YYARec(278,-91 );yyac++; 
					yya[yyac] = new YYARec(266,159);yyac++; 
					yya[yyac] = new YYARec(274,72);yyac++; 
					yya[yyac] = new YYARec(275,73);yyac++; 
					yya[yyac] = new YYARec(279,74);yyac++; 
					yya[yyac] = new YYARec(290,23);yyac++; 
					yya[yyac] = new YYARec(291,45);yyac++; 
					yya[yyac] = new YYARec(292,46);yyac++; 
					yya[yyac] = new YYARec(293,47);yyac++; 
					yya[yyac] = new YYARec(294,48);yyac++; 
					yya[yyac] = new YYARec(295,49);yyac++; 
					yya[yyac] = new YYARec(296,160);yyac++; 
					yya[yyac] = new YYARec(297,130);yyac++; 
					yya[yyac] = new YYARec(298,50);yyac++; 
					yya[yyac] = new YYARec(299,31);yyac++; 
					yya[yyac] = new YYARec(300,51);yyac++; 
					yya[yyac] = new YYARec(301,52);yyac++; 
					yya[yyac] = new YYARec(302,53);yyac++; 
					yya[yyac] = new YYARec(303,161);yyac++; 
					yya[yyac] = new YYARec(304,162);yyac++; 
					yya[yyac] = new YYARec(305,37);yyac++; 
					yya[yyac] = new YYARec(306,55);yyac++; 
					yya[yyac] = new YYARec(307,56);yyac++; 
					yya[yyac] = new YYARec(308,40);yyac++; 
					yya[yyac] = new YYARec(309,163);yyac++; 
					yya[yyac] = new YYARec(310,164);yyac++; 
					yya[yyac] = new YYARec(311,41);yyac++; 
					yya[yyac] = new YYARec(266,-117 );yyac++; 
					yya[yyac] = new YYARec(257,-148 );yyac++; 
					yya[yyac] = new YYARec(258,-148 );yyac++; 
					yya[yyac] = new YYARec(261,-148 );yyac++; 
					yya[yyac] = new YYARec(262,-148 );yyac++; 
					yya[yyac] = new YYARec(263,-148 );yyac++; 
					yya[yyac] = new YYARec(264,-148 );yyac++; 
					yya[yyac] = new YYARec(265,-148 );yyac++; 
					yya[yyac] = new YYARec(267,-148 );yyac++; 
					yya[yyac] = new YYARec(268,-148 );yyac++; 
					yya[yyac] = new YYARec(269,-148 );yyac++; 
					yya[yyac] = new YYARec(270,-148 );yyac++; 
					yya[yyac] = new YYARec(271,-148 );yyac++; 
					yya[yyac] = new YYARec(272,-148 );yyac++; 
					yya[yyac] = new YYARec(273,-148 );yyac++; 
					yya[yyac] = new YYARec(274,-148 );yyac++; 
					yya[yyac] = new YYARec(275,-148 );yyac++; 
					yya[yyac] = new YYARec(276,-148 );yyac++; 
					yya[yyac] = new YYARec(277,-148 );yyac++; 
					yya[yyac] = new YYARec(278,-148 );yyac++; 
					yya[yyac] = new YYARec(281,-148 );yyac++; 
					yya[yyac] = new YYARec(282,-148 );yyac++; 
					yya[yyac] = new YYARec(283,-148 );yyac++; 
					yya[yyac] = new YYARec(284,-148 );yyac++; 
					yya[yyac] = new YYARec(285,-148 );yyac++; 
					yya[yyac] = new YYARec(289,-148 );yyac++; 
					yya[yyac] = new YYARec(309,-148 );yyac++; 
					yya[yyac] = new YYARec(266,-115 );yyac++; 
					yya[yyac] = new YYARec(257,-142 );yyac++; 
					yya[yyac] = new YYARec(258,-142 );yyac++; 
					yya[yyac] = new YYARec(261,-142 );yyac++; 
					yya[yyac] = new YYARec(262,-142 );yyac++; 
					yya[yyac] = new YYARec(263,-142 );yyac++; 
					yya[yyac] = new YYARec(264,-142 );yyac++; 
					yya[yyac] = new YYARec(265,-142 );yyac++; 
					yya[yyac] = new YYARec(267,-142 );yyac++; 
					yya[yyac] = new YYARec(268,-142 );yyac++; 
					yya[yyac] = new YYARec(269,-142 );yyac++; 
					yya[yyac] = new YYARec(270,-142 );yyac++; 
					yya[yyac] = new YYARec(271,-142 );yyac++; 
					yya[yyac] = new YYARec(272,-142 );yyac++; 
					yya[yyac] = new YYARec(273,-142 );yyac++; 
					yya[yyac] = new YYARec(274,-142 );yyac++; 
					yya[yyac] = new YYARec(275,-142 );yyac++; 
					yya[yyac] = new YYARec(276,-142 );yyac++; 
					yya[yyac] = new YYARec(277,-142 );yyac++; 
					yya[yyac] = new YYARec(278,-142 );yyac++; 
					yya[yyac] = new YYARec(281,-142 );yyac++; 
					yya[yyac] = new YYARec(282,-142 );yyac++; 
					yya[yyac] = new YYARec(283,-142 );yyac++; 
					yya[yyac] = new YYARec(284,-142 );yyac++; 
					yya[yyac] = new YYARec(285,-142 );yyac++; 
					yya[yyac] = new YYARec(289,-142 );yyac++; 
					yya[yyac] = new YYARec(309,-142 );yyac++; 
					yya[yyac] = new YYARec(266,-116 );yyac++; 
					yya[yyac] = new YYARec(257,-151 );yyac++; 
					yya[yyac] = new YYARec(258,-151 );yyac++; 
					yya[yyac] = new YYARec(261,-151 );yyac++; 
					yya[yyac] = new YYARec(262,-151 );yyac++; 
					yya[yyac] = new YYARec(263,-151 );yyac++; 
					yya[yyac] = new YYARec(264,-151 );yyac++; 
					yya[yyac] = new YYARec(265,-151 );yyac++; 
					yya[yyac] = new YYARec(267,-151 );yyac++; 
					yya[yyac] = new YYARec(268,-151 );yyac++; 
					yya[yyac] = new YYARec(269,-151 );yyac++; 
					yya[yyac] = new YYARec(270,-151 );yyac++; 
					yya[yyac] = new YYARec(271,-151 );yyac++; 
					yya[yyac] = new YYARec(272,-151 );yyac++; 
					yya[yyac] = new YYARec(273,-151 );yyac++; 
					yya[yyac] = new YYARec(274,-151 );yyac++; 
					yya[yyac] = new YYARec(275,-151 );yyac++; 
					yya[yyac] = new YYARec(276,-151 );yyac++; 
					yya[yyac] = new YYARec(277,-151 );yyac++; 
					yya[yyac] = new YYARec(278,-151 );yyac++; 
					yya[yyac] = new YYARec(281,-151 );yyac++; 
					yya[yyac] = new YYARec(282,-151 );yyac++; 
					yya[yyac] = new YYARec(283,-151 );yyac++; 
					yya[yyac] = new YYARec(284,-151 );yyac++; 
					yya[yyac] = new YYARec(285,-151 );yyac++; 
					yya[yyac] = new YYARec(289,-151 );yyac++; 
					yya[yyac] = new YYARec(309,-151 );yyac++; 
					yya[yyac] = new YYARec(309,210);yyac++; 
					yya[yyac] = new YYARec(257,-130 );yyac++; 
					yya[yyac] = new YYARec(258,-130 );yyac++; 
					yya[yyac] = new YYARec(261,-130 );yyac++; 
					yya[yyac] = new YYARec(262,-130 );yyac++; 
					yya[yyac] = new YYARec(263,-130 );yyac++; 
					yya[yyac] = new YYARec(264,-130 );yyac++; 
					yya[yyac] = new YYARec(265,-130 );yyac++; 
					yya[yyac] = new YYARec(267,-130 );yyac++; 
					yya[yyac] = new YYARec(268,-130 );yyac++; 
					yya[yyac] = new YYARec(269,-130 );yyac++; 
					yya[yyac] = new YYARec(270,-130 );yyac++; 
					yya[yyac] = new YYARec(271,-130 );yyac++; 
					yya[yyac] = new YYARec(272,-130 );yyac++; 
					yya[yyac] = new YYARec(273,-130 );yyac++; 
					yya[yyac] = new YYARec(274,-130 );yyac++; 
					yya[yyac] = new YYARec(275,-130 );yyac++; 
					yya[yyac] = new YYARec(276,-130 );yyac++; 
					yya[yyac] = new YYARec(277,-130 );yyac++; 
					yya[yyac] = new YYARec(278,-130 );yyac++; 
					yya[yyac] = new YYARec(257,102);yyac++; 
					yya[yyac] = new YYARec(258,103);yyac++; 
					yya[yyac] = new YYARec(280,104);yyac++; 
					yya[yyac] = new YYARec(286,105);yyac++; 
					yya[yyac] = new YYARec(287,106);yyac++; 
					yya[yyac] = new YYARec(288,107);yyac++; 
					yya[yyac] = new YYARec(290,23);yyac++; 
					yya[yyac] = new YYARec(291,45);yyac++; 
					yya[yyac] = new YYARec(292,46);yyac++; 
					yya[yyac] = new YYARec(293,47);yyac++; 
					yya[yyac] = new YYARec(294,48);yyac++; 
					yya[yyac] = new YYARec(295,49);yyac++; 
					yya[yyac] = new YYARec(296,29);yyac++; 
					yya[yyac] = new YYARec(298,50);yyac++; 
					yya[yyac] = new YYARec(299,31);yyac++; 
					yya[yyac] = new YYARec(300,51);yyac++; 
					yya[yyac] = new YYARec(301,52);yyac++; 
					yya[yyac] = new YYARec(302,53);yyac++; 
					yya[yyac] = new YYARec(303,35);yyac++; 
					yya[yyac] = new YYARec(304,54);yyac++; 
					yya[yyac] = new YYARec(305,37);yyac++; 
					yya[yyac] = new YYARec(306,55);yyac++; 
					yya[yyac] = new YYARec(307,56);yyac++; 
					yya[yyac] = new YYARec(308,40);yyac++; 
					yya[yyac] = new YYARec(311,41);yyac++; 
					yya[yyac] = new YYARec(259,-53 );yyac++; 
					yya[yyac] = new YYARec(258,212);yyac++; 
					yya[yyac] = new YYARec(309,208);yyac++; 
					yya[yyac] = new YYARec(257,-91 );yyac++; 
					yya[yyac] = new YYARec(258,-91 );yyac++; 
					yya[yyac] = new YYARec(261,-91 );yyac++; 
					yya[yyac] = new YYARec(262,-91 );yyac++; 
					yya[yyac] = new YYARec(263,-91 );yyac++; 
					yya[yyac] = new YYARec(264,-91 );yyac++; 
					yya[yyac] = new YYARec(265,-91 );yyac++; 
					yya[yyac] = new YYARec(267,-91 );yyac++; 
					yya[yyac] = new YYARec(268,-91 );yyac++; 
					yya[yyac] = new YYARec(269,-91 );yyac++; 
					yya[yyac] = new YYARec(270,-91 );yyac++; 
					yya[yyac] = new YYARec(271,-91 );yyac++; 
					yya[yyac] = new YYARec(272,-91 );yyac++; 
					yya[yyac] = new YYARec(273,-91 );yyac++; 
					yya[yyac] = new YYARec(274,-91 );yyac++; 
					yya[yyac] = new YYARec(275,-91 );yyac++; 
					yya[yyac] = new YYARec(276,-91 );yyac++; 
					yya[yyac] = new YYARec(277,-91 );yyac++; 
					yya[yyac] = new YYARec(278,-91 );yyac++; 
					yya[yyac] = new YYARec(258,213);yyac++; 
					yya[yyac] = new YYARec(274,72);yyac++; 
					yya[yyac] = new YYARec(275,73);yyac++; 
					yya[yyac] = new YYARec(279,74);yyac++; 
					yya[yyac] = new YYARec(290,23);yyac++; 
					yya[yyac] = new YYARec(291,45);yyac++; 
					yya[yyac] = new YYARec(292,46);yyac++; 
					yya[yyac] = new YYARec(293,47);yyac++; 
					yya[yyac] = new YYARec(294,48);yyac++; 
					yya[yyac] = new YYARec(295,49);yyac++; 
					yya[yyac] = new YYARec(296,29);yyac++; 
					yya[yyac] = new YYARec(298,50);yyac++; 
					yya[yyac] = new YYARec(299,31);yyac++; 
					yya[yyac] = new YYARec(300,51);yyac++; 
					yya[yyac] = new YYARec(301,52);yyac++; 
					yya[yyac] = new YYARec(302,53);yyac++; 
					yya[yyac] = new YYARec(303,35);yyac++; 
					yya[yyac] = new YYARec(304,54);yyac++; 
					yya[yyac] = new YYARec(305,37);yyac++; 
					yya[yyac] = new YYARec(306,55);yyac++; 
					yya[yyac] = new YYARec(307,56);yyac++; 
					yya[yyac] = new YYARec(308,40);yyac++; 
					yya[yyac] = new YYARec(309,75);yyac++; 
					yya[yyac] = new YYARec(310,76);yyac++; 
					yya[yyac] = new YYARec(311,41);yyac++; 
					yya[yyac] = new YYARec(312,77);yyac++; 
					yya[yyac] = new YYARec(313,78);yyac++; 
					yya[yyac] = new YYARec(257,-41 );yyac++; 
					yya[yyac] = new YYARec(257,215);yyac++; 
					yya[yyac] = new YYARec(257,102);yyac++; 
					yya[yyac] = new YYARec(258,103);yyac++; 
					yya[yyac] = new YYARec(280,104);yyac++; 
					yya[yyac] = new YYARec(286,105);yyac++; 
					yya[yyac] = new YYARec(287,106);yyac++; 
					yya[yyac] = new YYARec(288,107);yyac++; 
					yya[yyac] = new YYARec(290,23);yyac++; 
					yya[yyac] = new YYARec(291,45);yyac++; 
					yya[yyac] = new YYARec(292,46);yyac++; 
					yya[yyac] = new YYARec(293,47);yyac++; 
					yya[yyac] = new YYARec(294,48);yyac++; 
					yya[yyac] = new YYARec(295,49);yyac++; 
					yya[yyac] = new YYARec(296,29);yyac++; 
					yya[yyac] = new YYARec(298,50);yyac++; 
					yya[yyac] = new YYARec(299,31);yyac++; 
					yya[yyac] = new YYARec(300,51);yyac++; 
					yya[yyac] = new YYARec(301,52);yyac++; 
					yya[yyac] = new YYARec(302,53);yyac++; 
					yya[yyac] = new YYARec(303,35);yyac++; 
					yya[yyac] = new YYARec(304,54);yyac++; 
					yya[yyac] = new YYARec(305,37);yyac++; 
					yya[yyac] = new YYARec(306,55);yyac++; 
					yya[yyac] = new YYARec(307,56);yyac++; 
					yya[yyac] = new YYARec(308,40);yyac++; 
					yya[yyac] = new YYARec(311,41);yyac++; 
					yya[yyac] = new YYARec(259,-53 );yyac++; 
					yya[yyac] = new YYARec(266,159);yyac++; 
					yya[yyac] = new YYARec(274,72);yyac++; 
					yya[yyac] = new YYARec(275,73);yyac++; 
					yya[yyac] = new YYARec(279,74);yyac++; 
					yya[yyac] = new YYARec(290,23);yyac++; 
					yya[yyac] = new YYARec(291,45);yyac++; 
					yya[yyac] = new YYARec(292,46);yyac++; 
					yya[yyac] = new YYARec(293,47);yyac++; 
					yya[yyac] = new YYARec(294,48);yyac++; 
					yya[yyac] = new YYARec(295,49);yyac++; 
					yya[yyac] = new YYARec(296,160);yyac++; 
					yya[yyac] = new YYARec(297,130);yyac++; 
					yya[yyac] = new YYARec(298,50);yyac++; 
					yya[yyac] = new YYARec(299,31);yyac++; 
					yya[yyac] = new YYARec(300,51);yyac++; 
					yya[yyac] = new YYARec(301,52);yyac++; 
					yya[yyac] = new YYARec(302,53);yyac++; 
					yya[yyac] = new YYARec(303,161);yyac++; 
					yya[yyac] = new YYARec(304,162);yyac++; 
					yya[yyac] = new YYARec(305,37);yyac++; 
					yya[yyac] = new YYARec(306,55);yyac++; 
					yya[yyac] = new YYARec(307,56);yyac++; 
					yya[yyac] = new YYARec(308,40);yyac++; 
					yya[yyac] = new YYARec(309,163);yyac++; 
					yya[yyac] = new YYARec(310,164);yyac++; 
					yya[yyac] = new YYARec(311,41);yyac++; 
					yya[yyac] = new YYARec(266,159);yyac++; 
					yya[yyac] = new YYARec(274,72);yyac++; 
					yya[yyac] = new YYARec(275,73);yyac++; 
					yya[yyac] = new YYARec(279,74);yyac++; 
					yya[yyac] = new YYARec(290,23);yyac++; 
					yya[yyac] = new YYARec(291,45);yyac++; 
					yya[yyac] = new YYARec(292,46);yyac++; 
					yya[yyac] = new YYARec(293,47);yyac++; 
					yya[yyac] = new YYARec(294,48);yyac++; 
					yya[yyac] = new YYARec(295,49);yyac++; 
					yya[yyac] = new YYARec(296,160);yyac++; 
					yya[yyac] = new YYARec(297,130);yyac++; 
					yya[yyac] = new YYARec(298,50);yyac++; 
					yya[yyac] = new YYARec(299,31);yyac++; 
					yya[yyac] = new YYARec(300,51);yyac++; 
					yya[yyac] = new YYARec(301,52);yyac++; 
					yya[yyac] = new YYARec(302,53);yyac++; 
					yya[yyac] = new YYARec(303,161);yyac++; 
					yya[yyac] = new YYARec(304,162);yyac++; 
					yya[yyac] = new YYARec(305,37);yyac++; 
					yya[yyac] = new YYARec(306,55);yyac++; 
					yya[yyac] = new YYARec(307,56);yyac++; 
					yya[yyac] = new YYARec(308,40);yyac++; 
					yya[yyac] = new YYARec(309,163);yyac++; 
					yya[yyac] = new YYARec(310,164);yyac++; 
					yya[yyac] = new YYARec(311,41);yyac++; 
					yya[yyac] = new YYARec(266,159);yyac++; 
					yya[yyac] = new YYARec(274,72);yyac++; 
					yya[yyac] = new YYARec(275,73);yyac++; 
					yya[yyac] = new YYARec(279,74);yyac++; 
					yya[yyac] = new YYARec(290,23);yyac++; 
					yya[yyac] = new YYARec(291,45);yyac++; 
					yya[yyac] = new YYARec(292,46);yyac++; 
					yya[yyac] = new YYARec(293,47);yyac++; 
					yya[yyac] = new YYARec(294,48);yyac++; 
					yya[yyac] = new YYARec(295,49);yyac++; 
					yya[yyac] = new YYARec(296,160);yyac++; 
					yya[yyac] = new YYARec(297,130);yyac++; 
					yya[yyac] = new YYARec(298,50);yyac++; 
					yya[yyac] = new YYARec(299,31);yyac++; 
					yya[yyac] = new YYARec(300,51);yyac++; 
					yya[yyac] = new YYARec(301,52);yyac++; 
					yya[yyac] = new YYARec(302,53);yyac++; 
					yya[yyac] = new YYARec(303,161);yyac++; 
					yya[yyac] = new YYARec(304,162);yyac++; 
					yya[yyac] = new YYARec(305,37);yyac++; 
					yya[yyac] = new YYARec(306,55);yyac++; 
					yya[yyac] = new YYARec(307,56);yyac++; 
					yya[yyac] = new YYARec(308,40);yyac++; 
					yya[yyac] = new YYARec(309,163);yyac++; 
					yya[yyac] = new YYARec(310,164);yyac++; 
					yya[yyac] = new YYARec(311,41);yyac++; 
					yya[yyac] = new YYARec(266,159);yyac++; 
					yya[yyac] = new YYARec(274,72);yyac++; 
					yya[yyac] = new YYARec(275,73);yyac++; 
					yya[yyac] = new YYARec(279,74);yyac++; 
					yya[yyac] = new YYARec(290,23);yyac++; 
					yya[yyac] = new YYARec(291,45);yyac++; 
					yya[yyac] = new YYARec(292,46);yyac++; 
					yya[yyac] = new YYARec(293,47);yyac++; 
					yya[yyac] = new YYARec(294,48);yyac++; 
					yya[yyac] = new YYARec(295,49);yyac++; 
					yya[yyac] = new YYARec(296,160);yyac++; 
					yya[yyac] = new YYARec(297,130);yyac++; 
					yya[yyac] = new YYARec(298,50);yyac++; 
					yya[yyac] = new YYARec(299,31);yyac++; 
					yya[yyac] = new YYARec(300,51);yyac++; 
					yya[yyac] = new YYARec(301,52);yyac++; 
					yya[yyac] = new YYARec(302,53);yyac++; 
					yya[yyac] = new YYARec(303,161);yyac++; 
					yya[yyac] = new YYARec(304,162);yyac++; 
					yya[yyac] = new YYARec(305,37);yyac++; 
					yya[yyac] = new YYARec(306,55);yyac++; 
					yya[yyac] = new YYARec(307,56);yyac++; 
					yya[yyac] = new YYARec(308,40);yyac++; 
					yya[yyac] = new YYARec(309,163);yyac++; 
					yya[yyac] = new YYARec(310,164);yyac++; 
					yya[yyac] = new YYARec(311,41);yyac++; 
					yya[yyac] = new YYARec(266,159);yyac++; 
					yya[yyac] = new YYARec(274,72);yyac++; 
					yya[yyac] = new YYARec(275,73);yyac++; 
					yya[yyac] = new YYARec(279,74);yyac++; 
					yya[yyac] = new YYARec(290,23);yyac++; 
					yya[yyac] = new YYARec(291,45);yyac++; 
					yya[yyac] = new YYARec(292,46);yyac++; 
					yya[yyac] = new YYARec(293,47);yyac++; 
					yya[yyac] = new YYARec(294,48);yyac++; 
					yya[yyac] = new YYARec(295,49);yyac++; 
					yya[yyac] = new YYARec(296,160);yyac++; 
					yya[yyac] = new YYARec(297,130);yyac++; 
					yya[yyac] = new YYARec(298,50);yyac++; 
					yya[yyac] = new YYARec(299,31);yyac++; 
					yya[yyac] = new YYARec(300,51);yyac++; 
					yya[yyac] = new YYARec(301,52);yyac++; 
					yya[yyac] = new YYARec(302,53);yyac++; 
					yya[yyac] = new YYARec(303,161);yyac++; 
					yya[yyac] = new YYARec(304,162);yyac++; 
					yya[yyac] = new YYARec(305,37);yyac++; 
					yya[yyac] = new YYARec(306,55);yyac++; 
					yya[yyac] = new YYARec(307,56);yyac++; 
					yya[yyac] = new YYARec(308,40);yyac++; 
					yya[yyac] = new YYARec(309,163);yyac++; 
					yya[yyac] = new YYARec(310,164);yyac++; 
					yya[yyac] = new YYARec(311,41);yyac++; 
					yya[yyac] = new YYARec(266,159);yyac++; 
					yya[yyac] = new YYARec(274,72);yyac++; 
					yya[yyac] = new YYARec(275,73);yyac++; 
					yya[yyac] = new YYARec(279,74);yyac++; 
					yya[yyac] = new YYARec(290,23);yyac++; 
					yya[yyac] = new YYARec(291,45);yyac++; 
					yya[yyac] = new YYARec(292,46);yyac++; 
					yya[yyac] = new YYARec(293,47);yyac++; 
					yya[yyac] = new YYARec(294,48);yyac++; 
					yya[yyac] = new YYARec(295,49);yyac++; 
					yya[yyac] = new YYARec(296,160);yyac++; 
					yya[yyac] = new YYARec(297,130);yyac++; 
					yya[yyac] = new YYARec(298,50);yyac++; 
					yya[yyac] = new YYARec(299,31);yyac++; 
					yya[yyac] = new YYARec(300,51);yyac++; 
					yya[yyac] = new YYARec(301,52);yyac++; 
					yya[yyac] = new YYARec(302,53);yyac++; 
					yya[yyac] = new YYARec(303,161);yyac++; 
					yya[yyac] = new YYARec(304,162);yyac++; 
					yya[yyac] = new YYARec(305,37);yyac++; 
					yya[yyac] = new YYARec(306,55);yyac++; 
					yya[yyac] = new YYARec(307,56);yyac++; 
					yya[yyac] = new YYARec(308,40);yyac++; 
					yya[yyac] = new YYARec(309,163);yyac++; 
					yya[yyac] = new YYARec(310,164);yyac++; 
					yya[yyac] = new YYARec(311,41);yyac++; 
					yya[yyac] = new YYARec(266,159);yyac++; 
					yya[yyac] = new YYARec(274,72);yyac++; 
					yya[yyac] = new YYARec(275,73);yyac++; 
					yya[yyac] = new YYARec(279,74);yyac++; 
					yya[yyac] = new YYARec(290,23);yyac++; 
					yya[yyac] = new YYARec(291,45);yyac++; 
					yya[yyac] = new YYARec(292,46);yyac++; 
					yya[yyac] = new YYARec(293,47);yyac++; 
					yya[yyac] = new YYARec(294,48);yyac++; 
					yya[yyac] = new YYARec(295,49);yyac++; 
					yya[yyac] = new YYARec(296,160);yyac++; 
					yya[yyac] = new YYARec(297,130);yyac++; 
					yya[yyac] = new YYARec(298,50);yyac++; 
					yya[yyac] = new YYARec(299,31);yyac++; 
					yya[yyac] = new YYARec(300,51);yyac++; 
					yya[yyac] = new YYARec(301,52);yyac++; 
					yya[yyac] = new YYARec(302,53);yyac++; 
					yya[yyac] = new YYARec(303,161);yyac++; 
					yya[yyac] = new YYARec(304,162);yyac++; 
					yya[yyac] = new YYARec(305,37);yyac++; 
					yya[yyac] = new YYARec(306,55);yyac++; 
					yya[yyac] = new YYARec(307,56);yyac++; 
					yya[yyac] = new YYARec(308,40);yyac++; 
					yya[yyac] = new YYARec(309,163);yyac++; 
					yya[yyac] = new YYARec(310,164);yyac++; 
					yya[yyac] = new YYARec(311,41);yyac++; 
					yya[yyac] = new YYARec(266,159);yyac++; 
					yya[yyac] = new YYARec(274,72);yyac++; 
					yya[yyac] = new YYARec(275,73);yyac++; 
					yya[yyac] = new YYARec(279,74);yyac++; 
					yya[yyac] = new YYARec(290,23);yyac++; 
					yya[yyac] = new YYARec(291,45);yyac++; 
					yya[yyac] = new YYARec(292,46);yyac++; 
					yya[yyac] = new YYARec(293,47);yyac++; 
					yya[yyac] = new YYARec(294,48);yyac++; 
					yya[yyac] = new YYARec(295,49);yyac++; 
					yya[yyac] = new YYARec(296,160);yyac++; 
					yya[yyac] = new YYARec(297,130);yyac++; 
					yya[yyac] = new YYARec(298,50);yyac++; 
					yya[yyac] = new YYARec(299,31);yyac++; 
					yya[yyac] = new YYARec(300,51);yyac++; 
					yya[yyac] = new YYARec(301,52);yyac++; 
					yya[yyac] = new YYARec(302,53);yyac++; 
					yya[yyac] = new YYARec(303,161);yyac++; 
					yya[yyac] = new YYARec(304,162);yyac++; 
					yya[yyac] = new YYARec(305,37);yyac++; 
					yya[yyac] = new YYARec(306,55);yyac++; 
					yya[yyac] = new YYARec(307,56);yyac++; 
					yya[yyac] = new YYARec(308,40);yyac++; 
					yya[yyac] = new YYARec(309,163);yyac++; 
					yya[yyac] = new YYARec(310,164);yyac++; 
					yya[yyac] = new YYARec(311,41);yyac++; 
					yya[yyac] = new YYARec(266,159);yyac++; 
					yya[yyac] = new YYARec(274,72);yyac++; 
					yya[yyac] = new YYARec(275,73);yyac++; 
					yya[yyac] = new YYARec(279,74);yyac++; 
					yya[yyac] = new YYARec(290,23);yyac++; 
					yya[yyac] = new YYARec(291,45);yyac++; 
					yya[yyac] = new YYARec(292,46);yyac++; 
					yya[yyac] = new YYARec(293,47);yyac++; 
					yya[yyac] = new YYARec(294,48);yyac++; 
					yya[yyac] = new YYARec(295,49);yyac++; 
					yya[yyac] = new YYARec(296,160);yyac++; 
					yya[yyac] = new YYARec(297,130);yyac++; 
					yya[yyac] = new YYARec(298,50);yyac++; 
					yya[yyac] = new YYARec(299,31);yyac++; 
					yya[yyac] = new YYARec(300,51);yyac++; 
					yya[yyac] = new YYARec(301,52);yyac++; 
					yya[yyac] = new YYARec(302,53);yyac++; 
					yya[yyac] = new YYARec(303,161);yyac++; 
					yya[yyac] = new YYARec(304,162);yyac++; 
					yya[yyac] = new YYARec(305,37);yyac++; 
					yya[yyac] = new YYARec(306,55);yyac++; 
					yya[yyac] = new YYARec(307,56);yyac++; 
					yya[yyac] = new YYARec(308,40);yyac++; 
					yya[yyac] = new YYARec(309,163);yyac++; 
					yya[yyac] = new YYARec(310,164);yyac++; 
					yya[yyac] = new YYARec(311,41);yyac++; 
					yya[yyac] = new YYARec(266,159);yyac++; 
					yya[yyac] = new YYARec(274,72);yyac++; 
					yya[yyac] = new YYARec(275,73);yyac++; 
					yya[yyac] = new YYARec(279,74);yyac++; 
					yya[yyac] = new YYARec(290,23);yyac++; 
					yya[yyac] = new YYARec(291,45);yyac++; 
					yya[yyac] = new YYARec(292,46);yyac++; 
					yya[yyac] = new YYARec(293,47);yyac++; 
					yya[yyac] = new YYARec(294,48);yyac++; 
					yya[yyac] = new YYARec(295,49);yyac++; 
					yya[yyac] = new YYARec(296,160);yyac++; 
					yya[yyac] = new YYARec(297,130);yyac++; 
					yya[yyac] = new YYARec(298,50);yyac++; 
					yya[yyac] = new YYARec(299,31);yyac++; 
					yya[yyac] = new YYARec(300,51);yyac++; 
					yya[yyac] = new YYARec(301,52);yyac++; 
					yya[yyac] = new YYARec(302,53);yyac++; 
					yya[yyac] = new YYARec(303,161);yyac++; 
					yya[yyac] = new YYARec(304,162);yyac++; 
					yya[yyac] = new YYARec(305,37);yyac++; 
					yya[yyac] = new YYARec(306,55);yyac++; 
					yya[yyac] = new YYARec(307,56);yyac++; 
					yya[yyac] = new YYARec(308,40);yyac++; 
					yya[yyac] = new YYARec(309,163);yyac++; 
					yya[yyac] = new YYARec(310,164);yyac++; 
					yya[yyac] = new YYARec(311,41);yyac++; 
					yya[yyac] = new YYARec(266,159);yyac++; 
					yya[yyac] = new YYARec(274,72);yyac++; 
					yya[yyac] = new YYARec(275,73);yyac++; 
					yya[yyac] = new YYARec(279,74);yyac++; 
					yya[yyac] = new YYARec(290,23);yyac++; 
					yya[yyac] = new YYARec(291,45);yyac++; 
					yya[yyac] = new YYARec(292,46);yyac++; 
					yya[yyac] = new YYARec(293,47);yyac++; 
					yya[yyac] = new YYARec(294,48);yyac++; 
					yya[yyac] = new YYARec(295,49);yyac++; 
					yya[yyac] = new YYARec(296,160);yyac++; 
					yya[yyac] = new YYARec(297,130);yyac++; 
					yya[yyac] = new YYARec(298,50);yyac++; 
					yya[yyac] = new YYARec(299,31);yyac++; 
					yya[yyac] = new YYARec(300,51);yyac++; 
					yya[yyac] = new YYARec(301,52);yyac++; 
					yya[yyac] = new YYARec(302,53);yyac++; 
					yya[yyac] = new YYARec(303,161);yyac++; 
					yya[yyac] = new YYARec(304,162);yyac++; 
					yya[yyac] = new YYARec(305,37);yyac++; 
					yya[yyac] = new YYARec(306,55);yyac++; 
					yya[yyac] = new YYARec(307,56);yyac++; 
					yya[yyac] = new YYARec(308,40);yyac++; 
					yya[yyac] = new YYARec(309,163);yyac++; 
					yya[yyac] = new YYARec(310,164);yyac++; 
					yya[yyac] = new YYARec(311,41);yyac++; 
					yya[yyac] = new YYARec(267,228);yyac++; 
					yya[yyac] = new YYARec(259,229);yyac++; 
					yya[yyac] = new YYARec(257,102);yyac++; 
					yya[yyac] = new YYARec(258,103);yyac++; 
					yya[yyac] = new YYARec(280,104);yyac++; 
					yya[yyac] = new YYARec(286,105);yyac++; 
					yya[yyac] = new YYARec(287,106);yyac++; 
					yya[yyac] = new YYARec(288,107);yyac++; 
					yya[yyac] = new YYARec(290,23);yyac++; 
					yya[yyac] = new YYARec(291,45);yyac++; 
					yya[yyac] = new YYARec(292,46);yyac++; 
					yya[yyac] = new YYARec(293,47);yyac++; 
					yya[yyac] = new YYARec(294,48);yyac++; 
					yya[yyac] = new YYARec(295,49);yyac++; 
					yya[yyac] = new YYARec(296,29);yyac++; 
					yya[yyac] = new YYARec(298,50);yyac++; 
					yya[yyac] = new YYARec(299,31);yyac++; 
					yya[yyac] = new YYARec(300,51);yyac++; 
					yya[yyac] = new YYARec(301,52);yyac++; 
					yya[yyac] = new YYARec(302,53);yyac++; 
					yya[yyac] = new YYARec(303,35);yyac++; 
					yya[yyac] = new YYARec(304,54);yyac++; 
					yya[yyac] = new YYARec(305,37);yyac++; 
					yya[yyac] = new YYARec(306,55);yyac++; 
					yya[yyac] = new YYARec(307,56);yyac++; 
					yya[yyac] = new YYARec(308,40);yyac++; 
					yya[yyac] = new YYARec(311,41);yyac++; 
					yya[yyac] = new YYARec(259,-53 );yyac++; 
					yya[yyac] = new YYARec(257,102);yyac++; 
					yya[yyac] = new YYARec(258,103);yyac++; 
					yya[yyac] = new YYARec(280,104);yyac++; 
					yya[yyac] = new YYARec(286,105);yyac++; 
					yya[yyac] = new YYARec(287,106);yyac++; 
					yya[yyac] = new YYARec(288,107);yyac++; 
					yya[yyac] = new YYARec(290,23);yyac++; 
					yya[yyac] = new YYARec(291,45);yyac++; 
					yya[yyac] = new YYARec(292,46);yyac++; 
					yya[yyac] = new YYARec(293,47);yyac++; 
					yya[yyac] = new YYARec(294,48);yyac++; 
					yya[yyac] = new YYARec(295,49);yyac++; 
					yya[yyac] = new YYARec(296,29);yyac++; 
					yya[yyac] = new YYARec(298,50);yyac++; 
					yya[yyac] = new YYARec(299,31);yyac++; 
					yya[yyac] = new YYARec(300,51);yyac++; 
					yya[yyac] = new YYARec(301,52);yyac++; 
					yya[yyac] = new YYARec(302,53);yyac++; 
					yya[yyac] = new YYARec(303,35);yyac++; 
					yya[yyac] = new YYARec(304,54);yyac++; 
					yya[yyac] = new YYARec(305,37);yyac++; 
					yya[yyac] = new YYARec(306,55);yyac++; 
					yya[yyac] = new YYARec(307,56);yyac++; 
					yya[yyac] = new YYARec(308,40);yyac++; 
					yya[yyac] = new YYARec(311,41);yyac++; 
					yya[yyac] = new YYARec(259,-53 );yyac++; 
					yya[yyac] = new YYARec(267,232);yyac++; 
					yya[yyac] = new YYARec(276,188);yyac++; 
					yya[yyac] = new YYARec(277,189);yyac++; 
					yya[yyac] = new YYARec(278,190);yyac++; 
					yya[yyac] = new YYARec(257,-81 );yyac++; 
					yya[yyac] = new YYARec(258,-81 );yyac++; 
					yya[yyac] = new YYARec(261,-81 );yyac++; 
					yya[yyac] = new YYARec(262,-81 );yyac++; 
					yya[yyac] = new YYARec(263,-81 );yyac++; 
					yya[yyac] = new YYARec(264,-81 );yyac++; 
					yya[yyac] = new YYARec(265,-81 );yyac++; 
					yya[yyac] = new YYARec(267,-81 );yyac++; 
					yya[yyac] = new YYARec(268,-81 );yyac++; 
					yya[yyac] = new YYARec(269,-81 );yyac++; 
					yya[yyac] = new YYARec(270,-81 );yyac++; 
					yya[yyac] = new YYARec(271,-81 );yyac++; 
					yya[yyac] = new YYARec(272,-81 );yyac++; 
					yya[yyac] = new YYARec(273,-81 );yyac++; 
					yya[yyac] = new YYARec(274,-81 );yyac++; 
					yya[yyac] = new YYARec(275,-81 );yyac++; 
					yya[yyac] = new YYARec(274,192);yyac++; 
					yya[yyac] = new YYARec(275,193);yyac++; 
					yya[yyac] = new YYARec(257,-79 );yyac++; 
					yya[yyac] = new YYARec(258,-79 );yyac++; 
					yya[yyac] = new YYARec(261,-79 );yyac++; 
					yya[yyac] = new YYARec(262,-79 );yyac++; 
					yya[yyac] = new YYARec(263,-79 );yyac++; 
					yya[yyac] = new YYARec(264,-79 );yyac++; 
					yya[yyac] = new YYARec(265,-79 );yyac++; 
					yya[yyac] = new YYARec(267,-79 );yyac++; 
					yya[yyac] = new YYARec(268,-79 );yyac++; 
					yya[yyac] = new YYARec(269,-79 );yyac++; 
					yya[yyac] = new YYARec(270,-79 );yyac++; 
					yya[yyac] = new YYARec(271,-79 );yyac++; 
					yya[yyac] = new YYARec(272,-79 );yyac++; 
					yya[yyac] = new YYARec(273,-79 );yyac++; 
					yya[yyac] = new YYARec(270,195);yyac++; 
					yya[yyac] = new YYARec(271,196);yyac++; 
					yya[yyac] = new YYARec(272,197);yyac++; 
					yya[yyac] = new YYARec(273,198);yyac++; 
					yya[yyac] = new YYARec(257,-77 );yyac++; 
					yya[yyac] = new YYARec(258,-77 );yyac++; 
					yya[yyac] = new YYARec(261,-77 );yyac++; 
					yya[yyac] = new YYARec(262,-77 );yyac++; 
					yya[yyac] = new YYARec(263,-77 );yyac++; 
					yya[yyac] = new YYARec(264,-77 );yyac++; 
					yya[yyac] = new YYARec(265,-77 );yyac++; 
					yya[yyac] = new YYARec(267,-77 );yyac++; 
					yya[yyac] = new YYARec(268,-77 );yyac++; 
					yya[yyac] = new YYARec(269,-77 );yyac++; 
					yya[yyac] = new YYARec(268,200);yyac++; 
					yya[yyac] = new YYARec(269,201);yyac++; 
					yya[yyac] = new YYARec(257,-74 );yyac++; 
					yya[yyac] = new YYARec(258,-74 );yyac++; 
					yya[yyac] = new YYARec(261,-74 );yyac++; 
					yya[yyac] = new YYARec(262,-74 );yyac++; 
					yya[yyac] = new YYARec(263,-74 );yyac++; 
					yya[yyac] = new YYARec(264,-74 );yyac++; 
					yya[yyac] = new YYARec(265,-74 );yyac++; 
					yya[yyac] = new YYARec(267,-74 );yyac++; 
					yya[yyac] = new YYARec(265,202);yyac++; 
					yya[yyac] = new YYARec(257,-72 );yyac++; 
					yya[yyac] = new YYARec(258,-72 );yyac++; 
					yya[yyac] = new YYARec(261,-72 );yyac++; 
					yya[yyac] = new YYARec(262,-72 );yyac++; 
					yya[yyac] = new YYARec(263,-72 );yyac++; 
					yya[yyac] = new YYARec(264,-72 );yyac++; 
					yya[yyac] = new YYARec(267,-72 );yyac++; 
					yya[yyac] = new YYARec(264,203);yyac++; 
					yya[yyac] = new YYARec(257,-70 );yyac++; 
					yya[yyac] = new YYARec(258,-70 );yyac++; 
					yya[yyac] = new YYARec(261,-70 );yyac++; 
					yya[yyac] = new YYARec(262,-70 );yyac++; 
					yya[yyac] = new YYARec(263,-70 );yyac++; 
					yya[yyac] = new YYARec(267,-70 );yyac++; 
					yya[yyac] = new YYARec(263,204);yyac++; 
					yya[yyac] = new YYARec(257,-68 );yyac++; 
					yya[yyac] = new YYARec(258,-68 );yyac++; 
					yya[yyac] = new YYARec(261,-68 );yyac++; 
					yya[yyac] = new YYARec(262,-68 );yyac++; 
					yya[yyac] = new YYARec(267,-68 );yyac++; 
					yya[yyac] = new YYARec(262,205);yyac++; 
					yya[yyac] = new YYARec(257,-66 );yyac++; 
					yya[yyac] = new YYARec(258,-66 );yyac++; 
					yya[yyac] = new YYARec(261,-66 );yyac++; 
					yya[yyac] = new YYARec(267,-66 );yyac++; 
					yya[yyac] = new YYARec(259,233);yyac++; 
					yya[yyac] = new YYARec(259,234);yyac++;

					yyg[yygc] = new YYARec(-66,1);yygc++; 
					yyg[yygc] = new YYARec(-65,2);yygc++; 
					yyg[yygc] = new YYARec(-33,3);yygc++; 
					yyg[yygc] = new YYARec(-29,4);yygc++; 
					yyg[yygc] = new YYARec(-27,5);yygc++; 
					yyg[yygc] = new YYARec(-25,6);yygc++; 
					yyg[yygc] = new YYARec(-20,7);yygc++; 
					yyg[yygc] = new YYARec(-17,8);yygc++; 
					yyg[yygc] = new YYARec(-14,9);yygc++; 
					yyg[yygc] = new YYARec(-12,10);yygc++; 
					yyg[yygc] = new YYARec(-10,11);yygc++; 
					yyg[yygc] = new YYARec(-9,12);yygc++; 
					yyg[yygc] = new YYARec(-8,13);yygc++; 
					yyg[yygc] = new YYARec(-7,14);yygc++; 
					yyg[yygc] = new YYARec(-6,15);yygc++; 
					yyg[yygc] = new YYARec(-5,16);yygc++; 
					yyg[yygc] = new YYARec(-4,17);yygc++; 
					yyg[yygc] = new YYARec(-3,18);yygc++; 
					yyg[yygc] = new YYARec(-2,19);yygc++; 
					yyg[yygc] = new YYARec(-67,42);yygc++; 
					yyg[yygc] = new YYARec(-66,1);yygc++; 
					yyg[yygc] = new YYARec(-65,2);yygc++; 
					yyg[yygc] = new YYARec(-34,43);yygc++; 
					yyg[yygc] = new YYARec(-29,44);yygc++; 
					yyg[yygc] = new YYARec(-17,8);yygc++; 
					yyg[yygc] = new YYARec(-67,59);yygc++; 
					yyg[yygc] = new YYARec(-66,1);yygc++; 
					yyg[yygc] = new YYARec(-65,2);yygc++; 
					yyg[yygc] = new YYARec(-29,44);yygc++; 
					yyg[yygc] = new YYARec(-17,8);yygc++; 
					yyg[yygc] = new YYARec(-15,60);yygc++; 
					yyg[yygc] = new YYARec(-67,61);yygc++; 
					yyg[yygc] = new YYARec(-66,1);yygc++; 
					yyg[yygc] = new YYARec(-65,2);yygc++; 
					yyg[yygc] = new YYARec(-29,44);yygc++; 
					yyg[yygc] = new YYARec(-21,62);yygc++; 
					yyg[yygc] = new YYARec(-17,8);yygc++; 
					yyg[yygc] = new YYARec(-64,64);yygc++; 
					yyg[yygc] = new YYARec(-60,65);yygc++; 
					yyg[yygc] = new YYARec(-24,66);yygc++; 
					yyg[yygc] = new YYARec(-19,67);yygc++; 
					yyg[yygc] = new YYARec(-18,68);yygc++; 
					yyg[yygc] = new YYARec(-17,69);yygc++; 
					yyg[yygc] = new YYARec(-16,70);yygc++; 
					yyg[yygc] = new YYARec(-13,71);yygc++; 
					yyg[yygc] = new YYARec(-67,59);yygc++; 
					yyg[yygc] = new YYARec(-66,1);yygc++; 
					yyg[yygc] = new YYARec(-65,2);yygc++; 
					yyg[yygc] = new YYARec(-29,44);yygc++; 
					yyg[yygc] = new YYARec(-17,8);yygc++; 
					yyg[yygc] = new YYARec(-15,79);yygc++; 
					yyg[yygc] = new YYARec(-11,80);yygc++; 
					yyg[yygc] = new YYARec(-66,1);yygc++; 
					yyg[yygc] = new YYARec(-65,2);yygc++; 
					yyg[yygc] = new YYARec(-33,3);yygc++; 
					yyg[yygc] = new YYARec(-29,4);yygc++; 
					yyg[yygc] = new YYARec(-27,5);yygc++; 
					yyg[yygc] = new YYARec(-25,6);yygc++; 
					yyg[yygc] = new YYARec(-20,7);yygc++; 
					yyg[yygc] = new YYARec(-17,8);yygc++; 
					yyg[yygc] = new YYARec(-14,9);yygc++; 
					yyg[yygc] = new YYARec(-12,10);yygc++; 
					yyg[yygc] = new YYARec(-10,11);yygc++; 
					yyg[yygc] = new YYARec(-9,12);yygc++; 
					yyg[yygc] = new YYARec(-8,13);yygc++; 
					yyg[yygc] = new YYARec(-7,14);yygc++; 
					yyg[yygc] = new YYARec(-6,15);yygc++; 
					yyg[yygc] = new YYARec(-5,16);yygc++; 
					yyg[yygc] = new YYARec(-4,17);yygc++; 
					yyg[yygc] = new YYARec(-3,81);yygc++; 
					yyg[yygc] = new YYARec(-66,1);yygc++; 
					yyg[yygc] = new YYARec(-65,2);yygc++; 
					yyg[yygc] = new YYARec(-29,84);yygc++; 
					yyg[yygc] = new YYARec(-17,8);yygc++; 
					yyg[yygc] = new YYARec(-16,85);yygc++; 
					yyg[yygc] = new YYARec(-19,88);yygc++; 
					yyg[yygc] = new YYARec(-67,59);yygc++; 
					yyg[yygc] = new YYARec(-66,1);yygc++; 
					yyg[yygc] = new YYARec(-65,2);yygc++; 
					yyg[yygc] = new YYARec(-29,44);yygc++; 
					yyg[yygc] = new YYARec(-17,8);yygc++; 
					yyg[yygc] = new YYARec(-15,79);yygc++; 
					yyg[yygc] = new YYARec(-11,92);yygc++; 
					yyg[yygc] = new YYARec(-66,1);yygc++; 
					yyg[yygc] = new YYARec(-65,2);yygc++; 
					yyg[yygc] = new YYARec(-41,94);yygc++; 
					yyg[yygc] = new YYARec(-39,95);yygc++; 
					yyg[yygc] = new YYARec(-38,96);yygc++; 
					yyg[yygc] = new YYARec(-37,97);yygc++; 
					yyg[yygc] = new YYARec(-36,98);yygc++; 
					yyg[yygc] = new YYARec(-35,99);yygc++; 
					yyg[yygc] = new YYARec(-29,100);yygc++; 
					yyg[yygc] = new YYARec(-17,8);yygc++; 
					yyg[yygc] = new YYARec(-5,101);yygc++; 
					yyg[yygc] = new YYARec(-66,1);yygc++; 
					yyg[yygc] = new YYARec(-65,2);yygc++; 
					yyg[yygc] = new YYARec(-29,111);yygc++; 
					yyg[yygc] = new YYARec(-28,112);yygc++; 
					yyg[yygc] = new YYARec(-26,113);yygc++; 
					yyg[yygc] = new YYARec(-17,8);yygc++; 
					yyg[yygc] = new YYARec(-60,115);yygc++; 
					yyg[yygc] = new YYARec(-24,116);yygc++; 
					yyg[yygc] = new YYARec(-23,117);yygc++; 
					yyg[yygc] = new YYARec(-22,118);yygc++; 
					yyg[yygc] = new YYARec(-66,1);yygc++; 
					yyg[yygc] = new YYARec(-65,2);yygc++; 
					yyg[yygc] = new YYARec(-64,64);yygc++; 
					yyg[yygc] = new YYARec(-60,120);yygc++; 
					yyg[yygc] = new YYARec(-43,121);yygc++; 
					yyg[yygc] = new YYARec(-42,122);yygc++; 
					yyg[yygc] = new YYARec(-40,123);yygc++; 
					yyg[yygc] = new YYARec(-32,124);yygc++; 
					yyg[yygc] = new YYARec(-29,4);yygc++; 
					yyg[yygc] = new YYARec(-24,66);yygc++; 
					yyg[yygc] = new YYARec(-19,125);yygc++; 
					yyg[yygc] = new YYARec(-18,126);yygc++; 
					yyg[yygc] = new YYARec(-17,8);yygc++; 
					yyg[yygc] = new YYARec(-16,127);yygc++; 
					yyg[yygc] = new YYARec(-5,128);yygc++; 
					yyg[yygc] = new YYARec(-66,1);yygc++; 
					yyg[yygc] = new YYARec(-65,2);yygc++; 
					yyg[yygc] = new YYARec(-41,94);yygc++; 
					yyg[yygc] = new YYARec(-39,95);yygc++; 
					yyg[yygc] = new YYARec(-38,96);yygc++; 
					yyg[yygc] = new YYARec(-37,97);yygc++; 
					yyg[yygc] = new YYARec(-36,98);yygc++; 
					yyg[yygc] = new YYARec(-35,131);yygc++; 
					yyg[yygc] = new YYARec(-29,100);yygc++; 
					yyg[yygc] = new YYARec(-17,8);yygc++; 
					yyg[yygc] = new YYARec(-5,101);yygc++; 
					yyg[yygc] = new YYARec(-66,1);yygc++; 
					yyg[yygc] = new YYARec(-65,2);yygc++; 
					yyg[yygc] = new YYARec(-41,94);yygc++; 
					yyg[yygc] = new YYARec(-39,95);yygc++; 
					yyg[yygc] = new YYARec(-38,96);yygc++; 
					yyg[yygc] = new YYARec(-37,97);yygc++; 
					yyg[yygc] = new YYARec(-36,98);yygc++; 
					yyg[yygc] = new YYARec(-35,132);yygc++; 
					yyg[yygc] = new YYARec(-29,100);yygc++; 
					yyg[yygc] = new YYARec(-17,8);yygc++; 
					yyg[yygc] = new YYARec(-5,101);yygc++; 
					yyg[yygc] = new YYARec(-63,135);yygc++; 
					yyg[yygc] = new YYARec(-66,1);yygc++; 
					yyg[yygc] = new YYARec(-65,2);yygc++; 
					yyg[yygc] = new YYARec(-41,94);yygc++; 
					yyg[yygc] = new YYARec(-39,95);yygc++; 
					yyg[yygc] = new YYARec(-38,96);yygc++; 
					yyg[yygc] = new YYARec(-37,97);yygc++; 
					yyg[yygc] = new YYARec(-36,98);yygc++; 
					yyg[yygc] = new YYARec(-35,141);yygc++; 
					yyg[yygc] = new YYARec(-29,100);yygc++; 
					yyg[yygc] = new YYARec(-17,8);yygc++; 
					yyg[yygc] = new YYARec(-5,101);yygc++; 
					yyg[yygc] = new YYARec(-66,1);yygc++; 
					yyg[yygc] = new YYARec(-65,2);yygc++; 
					yyg[yygc] = new YYARec(-62,142);yygc++; 
					yyg[yygc] = new YYARec(-61,143);yygc++; 
					yyg[yygc] = new YYARec(-60,144);yygc++; 
					yyg[yygc] = new YYARec(-59,145);yygc++; 
					yyg[yygc] = new YYARec(-57,146);yygc++; 
					yyg[yygc] = new YYARec(-55,147);yygc++; 
					yyg[yygc] = new YYARec(-53,148);yygc++; 
					yyg[yygc] = new YYARec(-51,149);yygc++; 
					yyg[yygc] = new YYARec(-50,150);yygc++; 
					yyg[yygc] = new YYARec(-49,151);yygc++; 
					yyg[yygc] = new YYARec(-48,152);yygc++; 
					yyg[yygc] = new YYARec(-47,153);yygc++; 
					yyg[yygc] = new YYARec(-46,154);yygc++; 
					yyg[yygc] = new YYARec(-45,155);yygc++; 
					yyg[yygc] = new YYARec(-44,156);yygc++; 
					yyg[yygc] = new YYARec(-43,157);yygc++; 
					yyg[yygc] = new YYARec(-29,4);yygc++; 
					yyg[yygc] = new YYARec(-17,8);yygc++; 
					yyg[yygc] = new YYARec(-5,158);yygc++; 
					yyg[yygc] = new YYARec(-66,1);yygc++; 
					yyg[yygc] = new YYARec(-65,2);yygc++; 
					yyg[yygc] = new YYARec(-62,142);yygc++; 
					yyg[yygc] = new YYARec(-61,143);yygc++; 
					yyg[yygc] = new YYARec(-60,144);yygc++; 
					yyg[yygc] = new YYARec(-59,145);yygc++; 
					yyg[yygc] = new YYARec(-57,146);yygc++; 
					yyg[yygc] = new YYARec(-55,147);yygc++; 
					yyg[yygc] = new YYARec(-53,148);yygc++; 
					yyg[yygc] = new YYARec(-51,149);yygc++; 
					yyg[yygc] = new YYARec(-50,150);yygc++; 
					yyg[yygc] = new YYARec(-49,151);yygc++; 
					yyg[yygc] = new YYARec(-48,152);yygc++; 
					yyg[yygc] = new YYARec(-47,153);yygc++; 
					yyg[yygc] = new YYARec(-46,154);yygc++; 
					yyg[yygc] = new YYARec(-45,155);yygc++; 
					yyg[yygc] = new YYARec(-44,166);yygc++; 
					yyg[yygc] = new YYARec(-43,157);yygc++; 
					yyg[yygc] = new YYARec(-29,4);yygc++; 
					yyg[yygc] = new YYARec(-17,8);yygc++; 
					yyg[yygc] = new YYARec(-5,167);yygc++; 
					yyg[yygc] = new YYARec(-66,1);yygc++; 
					yyg[yygc] = new YYARec(-65,2);yygc++; 
					yyg[yygc] = new YYARec(-62,142);yygc++; 
					yyg[yygc] = new YYARec(-61,143);yygc++; 
					yyg[yygc] = new YYARec(-60,144);yygc++; 
					yyg[yygc] = new YYARec(-59,145);yygc++; 
					yyg[yygc] = new YYARec(-57,146);yygc++; 
					yyg[yygc] = new YYARec(-55,147);yygc++; 
					yyg[yygc] = new YYARec(-53,148);yygc++; 
					yyg[yygc] = new YYARec(-51,149);yygc++; 
					yyg[yygc] = new YYARec(-50,150);yygc++; 
					yyg[yygc] = new YYARec(-49,151);yygc++; 
					yyg[yygc] = new YYARec(-48,152);yygc++; 
					yyg[yygc] = new YYARec(-47,153);yygc++; 
					yyg[yygc] = new YYARec(-46,154);yygc++; 
					yyg[yygc] = new YYARec(-45,155);yygc++; 
					yyg[yygc] = new YYARec(-44,168);yygc++; 
					yyg[yygc] = new YYARec(-43,157);yygc++; 
					yyg[yygc] = new YYARec(-29,4);yygc++; 
					yyg[yygc] = new YYARec(-17,8);yygc++; 
					yyg[yygc] = new YYARec(-5,167);yygc++; 
					yyg[yygc] = new YYARec(-66,1);yygc++; 
					yyg[yygc] = new YYARec(-65,2);yygc++; 
					yyg[yygc] = new YYARec(-64,64);yygc++; 
					yyg[yygc] = new YYARec(-60,120);yygc++; 
					yyg[yygc] = new YYARec(-32,169);yygc++; 
					yyg[yygc] = new YYARec(-31,170);yygc++; 
					yyg[yygc] = new YYARec(-30,171);yygc++; 
					yyg[yygc] = new YYARec(-29,4);yygc++; 
					yyg[yygc] = new YYARec(-24,66);yygc++; 
					yyg[yygc] = new YYARec(-19,172);yygc++; 
					yyg[yygc] = new YYARec(-18,173);yygc++; 
					yyg[yygc] = new YYARec(-17,8);yygc++; 
					yyg[yygc] = new YYARec(-16,174);yygc++; 
					yyg[yygc] = new YYARec(-5,128);yygc++; 
					yyg[yygc] = new YYARec(-66,1);yygc++; 
					yyg[yygc] = new YYARec(-65,2);yygc++; 
					yyg[yygc] = new YYARec(-29,111);yygc++; 
					yyg[yygc] = new YYARec(-28,112);yygc++; 
					yyg[yygc] = new YYARec(-26,175);yygc++; 
					yyg[yygc] = new YYARec(-17,8);yygc++; 
					yyg[yygc] = new YYARec(-60,115);yygc++; 
					yyg[yygc] = new YYARec(-24,116);yygc++; 
					yyg[yygc] = new YYARec(-23,117);yygc++; 
					yyg[yygc] = new YYARec(-22,177);yygc++; 
					yyg[yygc] = new YYARec(-66,1);yygc++; 
					yyg[yygc] = new YYARec(-65,2);yygc++; 
					yyg[yygc] = new YYARec(-29,4);yygc++; 
					yyg[yygc] = new YYARec(-17,8);yygc++; 
					yyg[yygc] = new YYARec(-5,179);yygc++; 
					yyg[yygc] = new YYARec(-66,1);yygc++; 
					yyg[yygc] = new YYARec(-65,2);yygc++; 
					yyg[yygc] = new YYARec(-64,64);yygc++; 
					yyg[yygc] = new YYARec(-60,120);yygc++; 
					yyg[yygc] = new YYARec(-43,121);yygc++; 
					yyg[yygc] = new YYARec(-42,122);yygc++; 
					yyg[yygc] = new YYARec(-40,180);yygc++; 
					yyg[yygc] = new YYARec(-32,124);yygc++; 
					yyg[yygc] = new YYARec(-29,4);yygc++; 
					yyg[yygc] = new YYARec(-24,66);yygc++; 
					yyg[yygc] = new YYARec(-19,125);yygc++; 
					yyg[yygc] = new YYARec(-18,126);yygc++; 
					yyg[yygc] = new YYARec(-17,8);yygc++; 
					yyg[yygc] = new YYARec(-16,127);yygc++; 
					yyg[yygc] = new YYARec(-5,128);yygc++; 
					yyg[yygc] = new YYARec(-66,1);yygc++; 
					yyg[yygc] = new YYARec(-65,2);yygc++; 
					yyg[yygc] = new YYARec(-41,94);yygc++; 
					yyg[yygc] = new YYARec(-39,95);yygc++; 
					yyg[yygc] = new YYARec(-38,96);yygc++; 
					yyg[yygc] = new YYARec(-37,97);yygc++; 
					yyg[yygc] = new YYARec(-36,98);yygc++; 
					yyg[yygc] = new YYARec(-35,182);yygc++; 
					yyg[yygc] = new YYARec(-29,100);yygc++; 
					yyg[yygc] = new YYARec(-17,8);yygc++; 
					yyg[yygc] = new YYARec(-5,101);yygc++; 
					yyg[yygc] = new YYARec(-66,1);yygc++; 
					yyg[yygc] = new YYARec(-65,2);yygc++; 
					yyg[yygc] = new YYARec(-62,142);yygc++; 
					yyg[yygc] = new YYARec(-61,143);yygc++; 
					yyg[yygc] = new YYARec(-60,144);yygc++; 
					yyg[yygc] = new YYARec(-59,145);yygc++; 
					yyg[yygc] = new YYARec(-57,146);yygc++; 
					yyg[yygc] = new YYARec(-55,147);yygc++; 
					yyg[yygc] = new YYARec(-53,148);yygc++; 
					yyg[yygc] = new YYARec(-51,149);yygc++; 
					yyg[yygc] = new YYARec(-50,150);yygc++; 
					yyg[yygc] = new YYARec(-49,151);yygc++; 
					yyg[yygc] = new YYARec(-48,152);yygc++; 
					yyg[yygc] = new YYARec(-47,153);yygc++; 
					yyg[yygc] = new YYARec(-46,154);yygc++; 
					yyg[yygc] = new YYARec(-45,155);yygc++; 
					yyg[yygc] = new YYARec(-44,183);yygc++; 
					yyg[yygc] = new YYARec(-43,157);yygc++; 
					yyg[yygc] = new YYARec(-29,4);yygc++; 
					yyg[yygc] = new YYARec(-17,8);yygc++; 
					yyg[yygc] = new YYARec(-5,167);yygc++; 
					yyg[yygc] = new YYARec(-66,1);yygc++; 
					yyg[yygc] = new YYARec(-65,2);yygc++; 
					yyg[yygc] = new YYARec(-62,142);yygc++; 
					yyg[yygc] = new YYARec(-61,143);yygc++; 
					yyg[yygc] = new YYARec(-60,144);yygc++; 
					yyg[yygc] = new YYARec(-59,145);yygc++; 
					yyg[yygc] = new YYARec(-57,186);yygc++; 
					yyg[yygc] = new YYARec(-43,157);yygc++; 
					yyg[yygc] = new YYARec(-29,4);yygc++; 
					yyg[yygc] = new YYARec(-17,8);yygc++; 
					yyg[yygc] = new YYARec(-5,167);yygc++; 
					yyg[yygc] = new YYARec(-58,187);yygc++; 
					yyg[yygc] = new YYARec(-56,191);yygc++; 
					yyg[yygc] = new YYARec(-54,194);yygc++; 
					yyg[yygc] = new YYARec(-52,199);yygc++; 
					yyg[yygc] = new YYARec(-63,207);yygc++; 
					yyg[yygc] = new YYARec(-66,1);yygc++; 
					yyg[yygc] = new YYARec(-65,2);yygc++; 
					yyg[yygc] = new YYARec(-62,142);yygc++; 
					yyg[yygc] = new YYARec(-61,143);yygc++; 
					yyg[yygc] = new YYARec(-60,144);yygc++; 
					yyg[yygc] = new YYARec(-59,145);yygc++; 
					yyg[yygc] = new YYARec(-57,146);yygc++; 
					yyg[yygc] = new YYARec(-55,147);yygc++; 
					yyg[yygc] = new YYARec(-53,148);yygc++; 
					yyg[yygc] = new YYARec(-51,149);yygc++; 
					yyg[yygc] = new YYARec(-50,150);yygc++; 
					yyg[yygc] = new YYARec(-49,151);yygc++; 
					yyg[yygc] = new YYARec(-48,152);yygc++; 
					yyg[yygc] = new YYARec(-47,153);yygc++; 
					yyg[yygc] = new YYARec(-46,154);yygc++; 
					yyg[yygc] = new YYARec(-45,155);yygc++; 
					yyg[yygc] = new YYARec(-44,209);yygc++; 
					yyg[yygc] = new YYARec(-43,157);yygc++; 
					yyg[yygc] = new YYARec(-29,4);yygc++; 
					yyg[yygc] = new YYARec(-17,8);yygc++; 
					yyg[yygc] = new YYARec(-5,167);yygc++; 
					yyg[yygc] = new YYARec(-66,1);yygc++; 
					yyg[yygc] = new YYARec(-65,2);yygc++; 
					yyg[yygc] = new YYARec(-41,94);yygc++; 
					yyg[yygc] = new YYARec(-39,95);yygc++; 
					yyg[yygc] = new YYARec(-38,96);yygc++; 
					yyg[yygc] = new YYARec(-37,97);yygc++; 
					yyg[yygc] = new YYARec(-36,98);yygc++; 
					yyg[yygc] = new YYARec(-35,211);yygc++; 
					yyg[yygc] = new YYARec(-29,100);yygc++; 
					yyg[yygc] = new YYARec(-17,8);yygc++; 
					yyg[yygc] = new YYARec(-5,101);yygc++; 
					yyg[yygc] = new YYARec(-66,1);yygc++; 
					yyg[yygc] = new YYARec(-65,2);yygc++; 
					yyg[yygc] = new YYARec(-64,64);yygc++; 
					yyg[yygc] = new YYARec(-60,120);yygc++; 
					yyg[yygc] = new YYARec(-32,169);yygc++; 
					yyg[yygc] = new YYARec(-31,170);yygc++; 
					yyg[yygc] = new YYARec(-30,214);yygc++; 
					yyg[yygc] = new YYARec(-29,4);yygc++; 
					yyg[yygc] = new YYARec(-24,66);yygc++; 
					yyg[yygc] = new YYARec(-19,172);yygc++; 
					yyg[yygc] = new YYARec(-18,173);yygc++; 
					yyg[yygc] = new YYARec(-17,8);yygc++; 
					yyg[yygc] = new YYARec(-16,174);yygc++; 
					yyg[yygc] = new YYARec(-5,128);yygc++; 
					yyg[yygc] = new YYARec(-66,1);yygc++; 
					yyg[yygc] = new YYARec(-65,2);yygc++; 
					yyg[yygc] = new YYARec(-41,94);yygc++; 
					yyg[yygc] = new YYARec(-39,95);yygc++; 
					yyg[yygc] = new YYARec(-38,96);yygc++; 
					yyg[yygc] = new YYARec(-37,97);yygc++; 
					yyg[yygc] = new YYARec(-36,98);yygc++; 
					yyg[yygc] = new YYARec(-35,216);yygc++; 
					yyg[yygc] = new YYARec(-29,100);yygc++; 
					yyg[yygc] = new YYARec(-17,8);yygc++; 
					yyg[yygc] = new YYARec(-5,101);yygc++; 
					yyg[yygc] = new YYARec(-66,1);yygc++; 
					yyg[yygc] = new YYARec(-65,2);yygc++; 
					yyg[yygc] = new YYARec(-62,142);yygc++; 
					yyg[yygc] = new YYARec(-61,143);yygc++; 
					yyg[yygc] = new YYARec(-60,144);yygc++; 
					yyg[yygc] = new YYARec(-59,145);yygc++; 
					yyg[yygc] = new YYARec(-57,146);yygc++; 
					yyg[yygc] = new YYARec(-55,147);yygc++; 
					yyg[yygc] = new YYARec(-53,148);yygc++; 
					yyg[yygc] = new YYARec(-51,149);yygc++; 
					yyg[yygc] = new YYARec(-50,150);yygc++; 
					yyg[yygc] = new YYARec(-49,151);yygc++; 
					yyg[yygc] = new YYARec(-48,152);yygc++; 
					yyg[yygc] = new YYARec(-47,153);yygc++; 
					yyg[yygc] = new YYARec(-46,154);yygc++; 
					yyg[yygc] = new YYARec(-45,155);yygc++; 
					yyg[yygc] = new YYARec(-44,217);yygc++; 
					yyg[yygc] = new YYARec(-43,157);yygc++; 
					yyg[yygc] = new YYARec(-29,4);yygc++; 
					yyg[yygc] = new YYARec(-17,8);yygc++; 
					yyg[yygc] = new YYARec(-5,167);yygc++; 
					yyg[yygc] = new YYARec(-66,1);yygc++; 
					yyg[yygc] = new YYARec(-65,2);yygc++; 
					yyg[yygc] = new YYARec(-62,142);yygc++; 
					yyg[yygc] = new YYARec(-61,143);yygc++; 
					yyg[yygc] = new YYARec(-60,144);yygc++; 
					yyg[yygc] = new YYARec(-59,145);yygc++; 
					yyg[yygc] = new YYARec(-57,218);yygc++; 
					yyg[yygc] = new YYARec(-43,157);yygc++; 
					yyg[yygc] = new YYARec(-29,4);yygc++; 
					yyg[yygc] = new YYARec(-17,8);yygc++; 
					yyg[yygc] = new YYARec(-5,167);yygc++; 
					yyg[yygc] = new YYARec(-66,1);yygc++; 
					yyg[yygc] = new YYARec(-65,2);yygc++; 
					yyg[yygc] = new YYARec(-62,142);yygc++; 
					yyg[yygc] = new YYARec(-61,143);yygc++; 
					yyg[yygc] = new YYARec(-60,144);yygc++; 
					yyg[yygc] = new YYARec(-59,145);yygc++; 
					yyg[yygc] = new YYARec(-57,146);yygc++; 
					yyg[yygc] = new YYARec(-55,219);yygc++; 
					yyg[yygc] = new YYARec(-43,157);yygc++; 
					yyg[yygc] = new YYARec(-29,4);yygc++; 
					yyg[yygc] = new YYARec(-17,8);yygc++; 
					yyg[yygc] = new YYARec(-5,167);yygc++; 
					yyg[yygc] = new YYARec(-66,1);yygc++; 
					yyg[yygc] = new YYARec(-65,2);yygc++; 
					yyg[yygc] = new YYARec(-62,142);yygc++; 
					yyg[yygc] = new YYARec(-61,143);yygc++; 
					yyg[yygc] = new YYARec(-60,144);yygc++; 
					yyg[yygc] = new YYARec(-59,145);yygc++; 
					yyg[yygc] = new YYARec(-57,146);yygc++; 
					yyg[yygc] = new YYARec(-55,147);yygc++; 
					yyg[yygc] = new YYARec(-53,220);yygc++; 
					yyg[yygc] = new YYARec(-43,157);yygc++; 
					yyg[yygc] = new YYARec(-29,4);yygc++; 
					yyg[yygc] = new YYARec(-17,8);yygc++; 
					yyg[yygc] = new YYARec(-5,167);yygc++; 
					yyg[yygc] = new YYARec(-66,1);yygc++; 
					yyg[yygc] = new YYARec(-65,2);yygc++; 
					yyg[yygc] = new YYARec(-62,142);yygc++; 
					yyg[yygc] = new YYARec(-61,143);yygc++; 
					yyg[yygc] = new YYARec(-60,144);yygc++; 
					yyg[yygc] = new YYARec(-59,145);yygc++; 
					yyg[yygc] = new YYARec(-57,146);yygc++; 
					yyg[yygc] = new YYARec(-55,147);yygc++; 
					yyg[yygc] = new YYARec(-53,148);yygc++; 
					yyg[yygc] = new YYARec(-51,221);yygc++; 
					yyg[yygc] = new YYARec(-43,157);yygc++; 
					yyg[yygc] = new YYARec(-29,4);yygc++; 
					yyg[yygc] = new YYARec(-17,8);yygc++; 
					yyg[yygc] = new YYARec(-5,167);yygc++; 
					yyg[yygc] = new YYARec(-66,1);yygc++; 
					yyg[yygc] = new YYARec(-65,2);yygc++; 
					yyg[yygc] = new YYARec(-62,142);yygc++; 
					yyg[yygc] = new YYARec(-61,143);yygc++; 
					yyg[yygc] = new YYARec(-60,144);yygc++; 
					yyg[yygc] = new YYARec(-59,145);yygc++; 
					yyg[yygc] = new YYARec(-57,146);yygc++; 
					yyg[yygc] = new YYARec(-55,147);yygc++; 
					yyg[yygc] = new YYARec(-53,148);yygc++; 
					yyg[yygc] = new YYARec(-51,149);yygc++; 
					yyg[yygc] = new YYARec(-50,222);yygc++; 
					yyg[yygc] = new YYARec(-43,157);yygc++; 
					yyg[yygc] = new YYARec(-29,4);yygc++; 
					yyg[yygc] = new YYARec(-17,8);yygc++; 
					yyg[yygc] = new YYARec(-5,167);yygc++; 
					yyg[yygc] = new YYARec(-66,1);yygc++; 
					yyg[yygc] = new YYARec(-65,2);yygc++; 
					yyg[yygc] = new YYARec(-62,142);yygc++; 
					yyg[yygc] = new YYARec(-61,143);yygc++; 
					yyg[yygc] = new YYARec(-60,144);yygc++; 
					yyg[yygc] = new YYARec(-59,145);yygc++; 
					yyg[yygc] = new YYARec(-57,146);yygc++; 
					yyg[yygc] = new YYARec(-55,147);yygc++; 
					yyg[yygc] = new YYARec(-53,148);yygc++; 
					yyg[yygc] = new YYARec(-51,149);yygc++; 
					yyg[yygc] = new YYARec(-50,150);yygc++; 
					yyg[yygc] = new YYARec(-49,223);yygc++; 
					yyg[yygc] = new YYARec(-43,157);yygc++; 
					yyg[yygc] = new YYARec(-29,4);yygc++; 
					yyg[yygc] = new YYARec(-17,8);yygc++; 
					yyg[yygc] = new YYARec(-5,167);yygc++; 
					yyg[yygc] = new YYARec(-66,1);yygc++; 
					yyg[yygc] = new YYARec(-65,2);yygc++; 
					yyg[yygc] = new YYARec(-62,142);yygc++; 
					yyg[yygc] = new YYARec(-61,143);yygc++; 
					yyg[yygc] = new YYARec(-60,144);yygc++; 
					yyg[yygc] = new YYARec(-59,145);yygc++; 
					yyg[yygc] = new YYARec(-57,146);yygc++; 
					yyg[yygc] = new YYARec(-55,147);yygc++; 
					yyg[yygc] = new YYARec(-53,148);yygc++; 
					yyg[yygc] = new YYARec(-51,149);yygc++; 
					yyg[yygc] = new YYARec(-50,150);yygc++; 
					yyg[yygc] = new YYARec(-49,151);yygc++; 
					yyg[yygc] = new YYARec(-48,224);yygc++; 
					yyg[yygc] = new YYARec(-43,157);yygc++; 
					yyg[yygc] = new YYARec(-29,4);yygc++; 
					yyg[yygc] = new YYARec(-17,8);yygc++; 
					yyg[yygc] = new YYARec(-5,167);yygc++; 
					yyg[yygc] = new YYARec(-66,1);yygc++; 
					yyg[yygc] = new YYARec(-65,2);yygc++; 
					yyg[yygc] = new YYARec(-62,142);yygc++; 
					yyg[yygc] = new YYARec(-61,143);yygc++; 
					yyg[yygc] = new YYARec(-60,144);yygc++; 
					yyg[yygc] = new YYARec(-59,145);yygc++; 
					yyg[yygc] = new YYARec(-57,146);yygc++; 
					yyg[yygc] = new YYARec(-55,147);yygc++; 
					yyg[yygc] = new YYARec(-53,148);yygc++; 
					yyg[yygc] = new YYARec(-51,149);yygc++; 
					yyg[yygc] = new YYARec(-50,150);yygc++; 
					yyg[yygc] = new YYARec(-49,151);yygc++; 
					yyg[yygc] = new YYARec(-48,152);yygc++; 
					yyg[yygc] = new YYARec(-47,225);yygc++; 
					yyg[yygc] = new YYARec(-43,157);yygc++; 
					yyg[yygc] = new YYARec(-29,4);yygc++; 
					yyg[yygc] = new YYARec(-17,8);yygc++; 
					yyg[yygc] = new YYARec(-5,167);yygc++; 
					yyg[yygc] = new YYARec(-66,1);yygc++; 
					yyg[yygc] = new YYARec(-65,2);yygc++; 
					yyg[yygc] = new YYARec(-62,142);yygc++; 
					yyg[yygc] = new YYARec(-61,143);yygc++; 
					yyg[yygc] = new YYARec(-60,144);yygc++; 
					yyg[yygc] = new YYARec(-59,145);yygc++; 
					yyg[yygc] = new YYARec(-57,146);yygc++; 
					yyg[yygc] = new YYARec(-55,147);yygc++; 
					yyg[yygc] = new YYARec(-53,148);yygc++; 
					yyg[yygc] = new YYARec(-51,149);yygc++; 
					yyg[yygc] = new YYARec(-50,150);yygc++; 
					yyg[yygc] = new YYARec(-49,151);yygc++; 
					yyg[yygc] = new YYARec(-48,152);yygc++; 
					yyg[yygc] = new YYARec(-47,153);yygc++; 
					yyg[yygc] = new YYARec(-46,226);yygc++; 
					yyg[yygc] = new YYARec(-43,157);yygc++; 
					yyg[yygc] = new YYARec(-29,4);yygc++; 
					yyg[yygc] = new YYARec(-17,8);yygc++; 
					yyg[yygc] = new YYARec(-5,167);yygc++; 
					yyg[yygc] = new YYARec(-66,1);yygc++; 
					yyg[yygc] = new YYARec(-65,2);yygc++; 
					yyg[yygc] = new YYARec(-62,142);yygc++; 
					yyg[yygc] = new YYARec(-61,143);yygc++; 
					yyg[yygc] = new YYARec(-60,144);yygc++; 
					yyg[yygc] = new YYARec(-59,145);yygc++; 
					yyg[yygc] = new YYARec(-57,146);yygc++; 
					yyg[yygc] = new YYARec(-55,147);yygc++; 
					yyg[yygc] = new YYARec(-53,148);yygc++; 
					yyg[yygc] = new YYARec(-51,149);yygc++; 
					yyg[yygc] = new YYARec(-50,150);yygc++; 
					yyg[yygc] = new YYARec(-49,151);yygc++; 
					yyg[yygc] = new YYARec(-48,152);yygc++; 
					yyg[yygc] = new YYARec(-47,153);yygc++; 
					yyg[yygc] = new YYARec(-46,154);yygc++; 
					yyg[yygc] = new YYARec(-45,155);yygc++; 
					yyg[yygc] = new YYARec(-44,227);yygc++; 
					yyg[yygc] = new YYARec(-43,157);yygc++; 
					yyg[yygc] = new YYARec(-29,4);yygc++; 
					yyg[yygc] = new YYARec(-17,8);yygc++; 
					yyg[yygc] = new YYARec(-5,167);yygc++; 
					yyg[yygc] = new YYARec(-66,1);yygc++; 
					yyg[yygc] = new YYARec(-65,2);yygc++; 
					yyg[yygc] = new YYARec(-41,94);yygc++; 
					yyg[yygc] = new YYARec(-39,95);yygc++; 
					yyg[yygc] = new YYARec(-38,96);yygc++; 
					yyg[yygc] = new YYARec(-37,97);yygc++; 
					yyg[yygc] = new YYARec(-36,98);yygc++; 
					yyg[yygc] = new YYARec(-35,230);yygc++; 
					yyg[yygc] = new YYARec(-29,100);yygc++; 
					yyg[yygc] = new YYARec(-17,8);yygc++; 
					yyg[yygc] = new YYARec(-5,101);yygc++; 
					yyg[yygc] = new YYARec(-66,1);yygc++; 
					yyg[yygc] = new YYARec(-65,2);yygc++; 
					yyg[yygc] = new YYARec(-41,94);yygc++; 
					yyg[yygc] = new YYARec(-39,95);yygc++; 
					yyg[yygc] = new YYARec(-38,96);yygc++; 
					yyg[yygc] = new YYARec(-37,97);yygc++; 
					yyg[yygc] = new YYARec(-36,98);yygc++; 
					yyg[yygc] = new YYARec(-35,231);yygc++; 
					yyg[yygc] = new YYARec(-29,100);yygc++; 
					yyg[yygc] = new YYARec(-17,8);yygc++; 
					yyg[yygc] = new YYARec(-5,101);yygc++; 
					yyg[yygc] = new YYARec(-58,187);yygc++; 
					yyg[yygc] = new YYARec(-56,191);yygc++; 
					yyg[yygc] = new YYARec(-54,194);yygc++; 
					yyg[yygc] = new YYARec(-52,199);yygc++;

					yyd = new int[yynstates];
					yyd[0] = 0;  
					yyd[1] = -160;  
					yyd[2] = -159;  
					yyd[3] = 0;  
					yyd[4] = 0;  
					yyd[5] = -36;  
					yyd[6] = 0;  
					yyd[7] = 0;  
					yyd[8] = -158;  
					yyd[9] = -17;  
					yyd[10] = 0;  
					yyd[11] = 0;  
					yyd[12] = -11;  
					yyd[13] = -10;  
					yyd[14] = -9;  
					yyd[15] = -8;  
					yyd[16] = -3;  
					yyd[17] = 0;  
					yyd[18] = -1;  
					yyd[19] = 0;  
					yyd[20] = -5;  
					yyd[21] = -6;  
					yyd[22] = -7;  
					yyd[23] = -157;  
					yyd[24] = 0;  
					yyd[25] = 0;  
					yyd[26] = 0;  
					yyd[27] = 0;  
					yyd[28] = 0;  
					yyd[29] = -148;  
					yyd[30] = 0;  
					yyd[31] = -156;  
					yyd[32] = 0;  
					yyd[33] = 0;  
					yyd[34] = 0;  
					yyd[35] = -142;  
					yyd[36] = 0;  
					yyd[37] = -154;  
					yyd[38] = 0;  
					yyd[39] = 0;  
					yyd[40] = -152;  
					yyd[41] = -135;  
					yyd[42] = -165;  
					yyd[43] = 0;  
					yyd[44] = -163;  
					yyd[45] = -145;  
					yyd[46] = -147;  
					yyd[47] = -144;  
					yyd[48] = -149;  
					yyd[49] = -146;  
					yyd[50] = -155;  
					yyd[51] = -141;  
					yyd[52] = -140;  
					yyd[53] = -143;  
					yyd[54] = -151;  
					yyd[55] = -153;  
					yyd[56] = -150;  
					yyd[57] = 0;  
					yyd[58] = 0;  
					yyd[59] = -164;  
					yyd[60] = 0;  
					yyd[61] = -167;  
					yyd[62] = 0;  
					yyd[63] = -166;  
					yyd[64] = -127;  
					yyd[65] = 0;  
					yyd[66] = -128;  
					yyd[67] = -25;  
					yyd[68] = -24;  
					yyd[69] = -23;  
					yyd[70] = -22;  
					yyd[71] = 0;  
					yyd[72] = -105;  
					yyd[73] = -106;  
					yyd[74] = -104;  
					yyd[75] = -132;  
					yyd[76] = -134;  
					yyd[77] = -169;  
					yyd[78] = -170;  
					yyd[79] = 0;  
					yyd[80] = 0;  
					yyd[81] = -2;  
					yyd[82] = 0;  
					yyd[83] = 0;  
					yyd[84] = -121;  
					yyd[85] = 0;  
					yyd[86] = -33;  
					yyd[87] = 0;  
					yyd[88] = 0;  
					yyd[89] = -131;  
					yyd[90] = -133;  
					yyd[91] = -13;  
					yyd[92] = -21;  
					yyd[93] = -12;  
					yyd[94] = 0;  
					yyd[95] = 0;  
					yyd[96] = 0;  
					yyd[97] = 0;  
					yyd[98] = 0;  
					yyd[99] = 0;  
					yyd[100] = 0;  
					yyd[101] = 0;  
					yyd[102] = -57;  
					yyd[103] = 0;  
					yyd[104] = 0;  
					yyd[105] = 0;  
					yyd[106] = 0;  
					yyd[107] = 0;  
					yyd[108] = -162;  
					yyd[109] = -161;  
					yyd[110] = -32;  
					yyd[111] = 0;  
					yyd[112] = 0;  
					yyd[113] = 0;  
					yyd[114] = -40;  
					yyd[115] = 0;  
					yyd[116] = -29;  
					yyd[117] = 0;  
					yyd[118] = 0;  
					yyd[119] = -55;  
					yyd[120] = 0;  
					yyd[121] = -61;  
					yyd[122] = 0;  
					yyd[123] = 0;  
					yyd[124] = -60;  
					yyd[125] = -64;  
					yyd[126] = -63;  
					yyd[127] = -62;  
					yyd[128] = -123;  
					yyd[129] = -56;  
					yyd[130] = -136;  
					yyd[131] = -52;  
					yyd[132] = -51;  
					yyd[133] = 0;  
					yyd[134] = -47;  
					yyd[135] = 0;  
					yyd[136] = -110;  
					yyd[137] = -111;  
					yyd[138] = -112;  
					yyd[139] = -113;  
					yyd[140] = -114;  
					yyd[141] = 0;  
					yyd[142] = -92;  
					yyd[143] = 0;  
					yyd[144] = 0;  
					yyd[145] = -84;  
					yyd[146] = -82;  
					yyd[147] = 0;  
					yyd[148] = 0;  
					yyd[149] = 0;  
					yyd[150] = 0;  
					yyd[151] = 0;  
					yyd[152] = 0;  
					yyd[153] = 0;  
					yyd[154] = 0;  
					yyd[155] = 0;  
					yyd[156] = -107;  
					yyd[157] = -90;  
					yyd[158] = 0;  
					yyd[159] = 0;  
					yyd[160] = 0;  
					yyd[161] = 0;  
					yyd[162] = 0;  
					yyd[163] = 0;  
					yyd[164] = -129;  
					yyd[165] = 0;  
					yyd[166] = 0;  
					yyd[167] = 0;  
					yyd[168] = 0;  
					yyd[169] = -44;  
					yyd[170] = 0;  
					yyd[171] = 0;  
					yyd[172] = -46;  
					yyd[173] = -45;  
					yyd[174] = -43;  
					yyd[175] = -37;  
					yyd[176] = -31;  
					yyd[177] = -27;  
					yyd[178] = -26;  
					yyd[179] = -124;  
					yyd[180] = -59;  
					yyd[181] = -54;  
					yyd[182] = -50;  
					yyd[183] = -109;  
					yyd[184] = 0;  
					yyd[185] = 0;  
					yyd[186] = -85;  
					yyd[187] = 0;  
					yyd[188] = -101;  
					yyd[189] = -102;  
					yyd[190] = -103;  
					yyd[191] = 0;  
					yyd[192] = -99;  
					yyd[193] = -100;  
					yyd[194] = 0;  
					yyd[195] = -95;  
					yyd[196] = -96;  
					yyd[197] = -97;  
					yyd[198] = -98;  
					yyd[199] = 0;  
					yyd[200] = -93;  
					yyd[201] = -94;  
					yyd[202] = 0;  
					yyd[203] = 0;  
					yyd[204] = 0;  
					yyd[205] = 0;  
					yyd[206] = 0;  
					yyd[207] = 0;  
					yyd[208] = -89;  
					yyd[209] = 0;  
					yyd[210] = -88;  
					yyd[211] = 0;  
					yyd[212] = 0;  
					yyd[213] = 0;  
					yyd[214] = -42;  
					yyd[215] = -39;  
					yyd[216] = -49;  
					yyd[217] = 0;  
					yyd[218] = -83;  
					yyd[219] = 0;  
					yyd[220] = 0;  
					yyd[221] = 0;  
					yyd[222] = 0;  
					yyd[223] = 0;  
					yyd[224] = 0;  
					yyd[225] = 0;  
					yyd[226] = 0;  
					yyd[227] = -108;  
					yyd[228] = -87;  
					yyd[229] = -118;  
					yyd[230] = 0;  
					yyd[231] = 0;  
					yyd[232] = -86;  
					yyd[233] = -119;  
					yyd[234] = -120; 

					yyal = new int[yynstates];
					yyal[0] = 1;  
					yyal[1] = 24;  
					yyal[2] = 24;  
					yyal[3] = 24;  
					yyal[4] = 43;  
					yyal[5] = 94;  
					yyal[6] = 94;  
					yyal[7] = 113;  
					yyal[8] = 133;  
					yyal[9] = 133;  
					yyal[10] = 133;  
					yyal[11] = 141;  
					yyal[12] = 160;  
					yyal[13] = 160;  
					yyal[14] = 160;  
					yyal[15] = 160;  
					yyal[16] = 160;  
					yyal[17] = 160;  
					yyal[18] = 183;  
					yyal[19] = 183;  
					yyal[20] = 184;  
					yyal[21] = 184;  
					yyal[22] = 184;  
					yyal[23] = 184;  
					yyal[24] = 184;  
					yyal[25] = 205;  
					yyal[26] = 215;  
					yyal[27] = 237;  
					yyal[28] = 258;  
					yyal[29] = 279;  
					yyal[30] = 279;  
					yyal[31] = 289;  
					yyal[32] = 289;  
					yyal[33] = 299;  
					yyal[34] = 320;  
					yyal[35] = 341;  
					yyal[36] = 341;  
					yyal[37] = 351;  
					yyal[38] = 351;  
					yyal[39] = 361;  
					yyal[40] = 371;  
					yyal[41] = 371;  
					yyal[42] = 371;  
					yyal[43] = 371;  
					yyal[44] = 372;  
					yyal[45] = 372;  
					yyal[46] = 372;  
					yyal[47] = 372;  
					yyal[48] = 372;  
					yyal[49] = 372;  
					yyal[50] = 372;  
					yyal[51] = 372;  
					yyal[52] = 372;  
					yyal[53] = 372;  
					yyal[54] = 372;  
					yyal[55] = 372;  
					yyal[56] = 372;  
					yyal[57] = 372;  
					yyal[58] = 396;  
					yyal[59] = 415;  
					yyal[60] = 415;  
					yyal[61] = 418;  
					yyal[62] = 418;  
					yyal[63] = 419;  
					yyal[64] = 419;  
					yyal[65] = 419;  
					yyal[66] = 421;  
					yyal[67] = 421;  
					yyal[68] = 421;  
					yyal[69] = 421;  
					yyal[70] = 421;  
					yyal[71] = 421;  
					yyal[72] = 422;  
					yyal[73] = 422;  
					yyal[74] = 422;  
					yyal[75] = 422;  
					yyal[76] = 422;  
					yyal[77] = 422;  
					yyal[78] = 422;  
					yyal[79] = 422;  
					yyal[80] = 442;  
					yyal[81] = 443;  
					yyal[82] = 443;  
					yyal[83] = 469;  
					yyal[84] = 471;  
					yyal[85] = 471;  
					yyal[86] = 472;  
					yyal[87] = 472;  
					yyal[88] = 493;  
					yyal[89] = 498;  
					yyal[90] = 498;  
					yyal[91] = 498;  
					yyal[92] = 498;  
					yyal[93] = 498;  
					yyal[94] = 498;  
					yyal[95] = 499;  
					yyal[96] = 527;  
					yyal[97] = 553;  
					yyal[98] = 579;  
					yyal[99] = 580;  
					yyal[100] = 581;  
					yyal[101] = 616;  
					yyal[102] = 621;  
					yyal[103] = 621;  
					yyal[104] = 647;  
					yyal[105] = 673;  
					yyal[106] = 702;  
					yyal[107] = 728;  
					yyal[108] = 754;  
					yyal[109] = 754;  
					yyal[110] = 754;  
					yyal[111] = 754;  
					yyal[112] = 780;  
					yyal[113] = 801;  
					yyal[114] = 802;  
					yyal[115] = 802;  
					yyal[116] = 803;  
					yyal[117] = 803;  
					yyal[118] = 808;  
					yyal[119] = 809;  
					yyal[120] = 809;  
					yyal[121] = 830;  
					yyal[122] = 830;  
					yyal[123] = 858;  
					yyal[124] = 859;  
					yyal[125] = 859;  
					yyal[126] = 859;  
					yyal[127] = 859;  
					yyal[128] = 859;  
					yyal[129] = 859;  
					yyal[130] = 859;  
					yyal[131] = 859;  
					yyal[132] = 859;  
					yyal[133] = 859;  
					yyal[134] = 885;  
					yyal[135] = 885;  
					yyal[136] = 911;  
					yyal[137] = 911;  
					yyal[138] = 911;  
					yyal[139] = 911;  
					yyal[140] = 911;  
					yyal[141] = 911;  
					yyal[142] = 912;  
					yyal[143] = 912;  
					yyal[144] = 913;  
					yyal[145] = 939;  
					yyal[146] = 939;  
					yyal[147] = 939;  
					yyal[148] = 958;  
					yyal[149] = 974;  
					yyal[150] = 988;  
					yyal[151] = 998;  
					yyal[152] = 1006;  
					yyal[153] = 1013;  
					yyal[154] = 1019;  
					yyal[155] = 1024;  
					yyal[156] = 1028;  
					yyal[157] = 1028;  
					yyal[158] = 1028;  
					yyal[159] = 1051;  
					yyal[160] = 1077;  
					yyal[161] = 1104;  
					yyal[162] = 1131;  
					yyal[163] = 1158;  
					yyal[164] = 1178;  
					yyal[165] = 1178;  
					yyal[166] = 1204;  
					yyal[167] = 1205;  
					yyal[168] = 1225;  
					yyal[169] = 1226;  
					yyal[170] = 1226;  
					yyal[171] = 1253;  
					yyal[172] = 1254;  
					yyal[173] = 1254;  
					yyal[174] = 1254;  
					yyal[175] = 1254;  
					yyal[176] = 1254;  
					yyal[177] = 1254;  
					yyal[178] = 1254;  
					yyal[179] = 1254;  
					yyal[180] = 1254;  
					yyal[181] = 1254;  
					yyal[182] = 1254;  
					yyal[183] = 1254;  
					yyal[184] = 1254;  
					yyal[185] = 1280;  
					yyal[186] = 1306;  
					yyal[187] = 1306;  
					yyal[188] = 1332;  
					yyal[189] = 1332;  
					yyal[190] = 1332;  
					yyal[191] = 1332;  
					yyal[192] = 1358;  
					yyal[193] = 1358;  
					yyal[194] = 1358;  
					yyal[195] = 1384;  
					yyal[196] = 1384;  
					yyal[197] = 1384;  
					yyal[198] = 1384;  
					yyal[199] = 1384;  
					yyal[200] = 1410;  
					yyal[201] = 1410;  
					yyal[202] = 1410;  
					yyal[203] = 1436;  
					yyal[204] = 1462;  
					yyal[205] = 1488;  
					yyal[206] = 1514;  
					yyal[207] = 1540;  
					yyal[208] = 1566;  
					yyal[209] = 1566;  
					yyal[210] = 1567;  
					yyal[211] = 1567;  
					yyal[212] = 1568;  
					yyal[213] = 1594;  
					yyal[214] = 1620;  
					yyal[215] = 1620;  
					yyal[216] = 1620;  
					yyal[217] = 1620;  
					yyal[218] = 1621;  
					yyal[219] = 1621;  
					yyal[220] = 1640;  
					yyal[221] = 1656;  
					yyal[222] = 1670;  
					yyal[223] = 1680;  
					yyal[224] = 1688;  
					yyal[225] = 1695;  
					yyal[226] = 1701;  
					yyal[227] = 1706;  
					yyal[228] = 1706;  
					yyal[229] = 1706;  
					yyal[230] = 1706;  
					yyal[231] = 1707;  
					yyal[232] = 1708;  
					yyal[233] = 1708;  
					yyal[234] = 1708; 

					yyah = new int[yynstates];
					yyah[0] = 23;  
					yyah[1] = 23;  
					yyah[2] = 23;  
					yyah[3] = 42;  
					yyah[4] = 93;  
					yyah[5] = 93;  
					yyah[6] = 112;  
					yyah[7] = 132;  
					yyah[8] = 132;  
					yyah[9] = 132;  
					yyah[10] = 140;  
					yyah[11] = 159;  
					yyah[12] = 159;  
					yyah[13] = 159;  
					yyah[14] = 159;  
					yyah[15] = 159;  
					yyah[16] = 159;  
					yyah[17] = 182;  
					yyah[18] = 182;  
					yyah[19] = 183;  
					yyah[20] = 183;  
					yyah[21] = 183;  
					yyah[22] = 183;  
					yyah[23] = 183;  
					yyah[24] = 204;  
					yyah[25] = 214;  
					yyah[26] = 236;  
					yyah[27] = 257;  
					yyah[28] = 278;  
					yyah[29] = 278;  
					yyah[30] = 288;  
					yyah[31] = 288;  
					yyah[32] = 298;  
					yyah[33] = 319;  
					yyah[34] = 340;  
					yyah[35] = 340;  
					yyah[36] = 350;  
					yyah[37] = 350;  
					yyah[38] = 360;  
					yyah[39] = 370;  
					yyah[40] = 370;  
					yyah[41] = 370;  
					yyah[42] = 370;  
					yyah[43] = 371;  
					yyah[44] = 371;  
					yyah[45] = 371;  
					yyah[46] = 371;  
					yyah[47] = 371;  
					yyah[48] = 371;  
					yyah[49] = 371;  
					yyah[50] = 371;  
					yyah[51] = 371;  
					yyah[52] = 371;  
					yyah[53] = 371;  
					yyah[54] = 371;  
					yyah[55] = 371;  
					yyah[56] = 371;  
					yyah[57] = 395;  
					yyah[58] = 414;  
					yyah[59] = 414;  
					yyah[60] = 417;  
					yyah[61] = 417;  
					yyah[62] = 418;  
					yyah[63] = 418;  
					yyah[64] = 418;  
					yyah[65] = 420;  
					yyah[66] = 420;  
					yyah[67] = 420;  
					yyah[68] = 420;  
					yyah[69] = 420;  
					yyah[70] = 420;  
					yyah[71] = 421;  
					yyah[72] = 421;  
					yyah[73] = 421;  
					yyah[74] = 421;  
					yyah[75] = 421;  
					yyah[76] = 421;  
					yyah[77] = 421;  
					yyah[78] = 421;  
					yyah[79] = 441;  
					yyah[80] = 442;  
					yyah[81] = 442;  
					yyah[82] = 468;  
					yyah[83] = 470;  
					yyah[84] = 470;  
					yyah[85] = 471;  
					yyah[86] = 471;  
					yyah[87] = 492;  
					yyah[88] = 497;  
					yyah[89] = 497;  
					yyah[90] = 497;  
					yyah[91] = 497;  
					yyah[92] = 497;  
					yyah[93] = 497;  
					yyah[94] = 498;  
					yyah[95] = 526;  
					yyah[96] = 552;  
					yyah[97] = 578;  
					yyah[98] = 579;  
					yyah[99] = 580;  
					yyah[100] = 615;  
					yyah[101] = 620;  
					yyah[102] = 620;  
					yyah[103] = 646;  
					yyah[104] = 672;  
					yyah[105] = 701;  
					yyah[106] = 727;  
					yyah[107] = 753;  
					yyah[108] = 753;  
					yyah[109] = 753;  
					yyah[110] = 753;  
					yyah[111] = 779;  
					yyah[112] = 800;  
					yyah[113] = 801;  
					yyah[114] = 801;  
					yyah[115] = 802;  
					yyah[116] = 802;  
					yyah[117] = 807;  
					yyah[118] = 808;  
					yyah[119] = 808;  
					yyah[120] = 829;  
					yyah[121] = 829;  
					yyah[122] = 857;  
					yyah[123] = 858;  
					yyah[124] = 858;  
					yyah[125] = 858;  
					yyah[126] = 858;  
					yyah[127] = 858;  
					yyah[128] = 858;  
					yyah[129] = 858;  
					yyah[130] = 858;  
					yyah[131] = 858;  
					yyah[132] = 858;  
					yyah[133] = 884;  
					yyah[134] = 884;  
					yyah[135] = 910;  
					yyah[136] = 910;  
					yyah[137] = 910;  
					yyah[138] = 910;  
					yyah[139] = 910;  
					yyah[140] = 910;  
					yyah[141] = 911;  
					yyah[142] = 911;  
					yyah[143] = 912;  
					yyah[144] = 938;  
					yyah[145] = 938;  
					yyah[146] = 938;  
					yyah[147] = 957;  
					yyah[148] = 973;  
					yyah[149] = 987;  
					yyah[150] = 997;  
					yyah[151] = 1005;  
					yyah[152] = 1012;  
					yyah[153] = 1018;  
					yyah[154] = 1023;  
					yyah[155] = 1027;  
					yyah[156] = 1027;  
					yyah[157] = 1027;  
					yyah[158] = 1050;  
					yyah[159] = 1076;  
					yyah[160] = 1103;  
					yyah[161] = 1130;  
					yyah[162] = 1157;  
					yyah[163] = 1177;  
					yyah[164] = 1177;  
					yyah[165] = 1203;  
					yyah[166] = 1204;  
					yyah[167] = 1224;  
					yyah[168] = 1225;  
					yyah[169] = 1225;  
					yyah[170] = 1252;  
					yyah[171] = 1253;  
					yyah[172] = 1253;  
					yyah[173] = 1253;  
					yyah[174] = 1253;  
					yyah[175] = 1253;  
					yyah[176] = 1253;  
					yyah[177] = 1253;  
					yyah[178] = 1253;  
					yyah[179] = 1253;  
					yyah[180] = 1253;  
					yyah[181] = 1253;  
					yyah[182] = 1253;  
					yyah[183] = 1253;  
					yyah[184] = 1279;  
					yyah[185] = 1305;  
					yyah[186] = 1305;  
					yyah[187] = 1331;  
					yyah[188] = 1331;  
					yyah[189] = 1331;  
					yyah[190] = 1331;  
					yyah[191] = 1357;  
					yyah[192] = 1357;  
					yyah[193] = 1357;  
					yyah[194] = 1383;  
					yyah[195] = 1383;  
					yyah[196] = 1383;  
					yyah[197] = 1383;  
					yyah[198] = 1383;  
					yyah[199] = 1409;  
					yyah[200] = 1409;  
					yyah[201] = 1409;  
					yyah[202] = 1435;  
					yyah[203] = 1461;  
					yyah[204] = 1487;  
					yyah[205] = 1513;  
					yyah[206] = 1539;  
					yyah[207] = 1565;  
					yyah[208] = 1565;  
					yyah[209] = 1566;  
					yyah[210] = 1566;  
					yyah[211] = 1567;  
					yyah[212] = 1593;  
					yyah[213] = 1619;  
					yyah[214] = 1619;  
					yyah[215] = 1619;  
					yyah[216] = 1619;  
					yyah[217] = 1620;  
					yyah[218] = 1620;  
					yyah[219] = 1639;  
					yyah[220] = 1655;  
					yyah[221] = 1669;  
					yyah[222] = 1679;  
					yyah[223] = 1687;  
					yyah[224] = 1694;  
					yyah[225] = 1700;  
					yyah[226] = 1705;  
					yyah[227] = 1705;  
					yyah[228] = 1705;  
					yyah[229] = 1705;  
					yyah[230] = 1706;  
					yyah[231] = 1707;  
					yyah[232] = 1707;  
					yyah[233] = 1707;  
					yyah[234] = 1707; 

					yygl = new int[yynstates];
					yygl[0] = 1;  
					yygl[1] = 20;  
					yygl[2] = 20;  
					yygl[3] = 20;  
					yygl[4] = 26;  
					yygl[5] = 26;  
					yygl[6] = 26;  
					yygl[7] = 32;  
					yygl[8] = 38;  
					yygl[9] = 38;  
					yygl[10] = 38;  
					yygl[11] = 46;  
					yygl[12] = 53;  
					yygl[13] = 53;  
					yygl[14] = 53;  
					yygl[15] = 53;  
					yygl[16] = 53;  
					yygl[17] = 53;  
					yygl[18] = 71;  
					yygl[19] = 71;  
					yygl[20] = 71;  
					yygl[21] = 71;  
					yygl[22] = 71;  
					yygl[23] = 71;  
					yygl[24] = 71;  
					yygl[25] = 71;  
					yygl[26] = 71;  
					yygl[27] = 71;  
					yygl[28] = 71;  
					yygl[29] = 71;  
					yygl[30] = 71;  
					yygl[31] = 71;  
					yygl[32] = 71;  
					yygl[33] = 71;  
					yygl[34] = 71;  
					yygl[35] = 71;  
					yygl[36] = 71;  
					yygl[37] = 71;  
					yygl[38] = 71;  
					yygl[39] = 71;  
					yygl[40] = 71;  
					yygl[41] = 71;  
					yygl[42] = 71;  
					yygl[43] = 71;  
					yygl[44] = 71;  
					yygl[45] = 71;  
					yygl[46] = 71;  
					yygl[47] = 71;  
					yygl[48] = 71;  
					yygl[49] = 71;  
					yygl[50] = 71;  
					yygl[51] = 71;  
					yygl[52] = 71;  
					yygl[53] = 71;  
					yygl[54] = 71;  
					yygl[55] = 71;  
					yygl[56] = 71;  
					yygl[57] = 71;  
					yygl[58] = 71;  
					yygl[59] = 75;  
					yygl[60] = 75;  
					yygl[61] = 76;  
					yygl[62] = 76;  
					yygl[63] = 77;  
					yygl[64] = 77;  
					yygl[65] = 77;  
					yygl[66] = 77;  
					yygl[67] = 77;  
					yygl[68] = 77;  
					yygl[69] = 77;  
					yygl[70] = 77;  
					yygl[71] = 77;  
					yygl[72] = 77;  
					yygl[73] = 77;  
					yygl[74] = 77;  
					yygl[75] = 77;  
					yygl[76] = 77;  
					yygl[77] = 77;  
					yygl[78] = 77;  
					yygl[79] = 77;  
					yygl[80] = 84;  
					yygl[81] = 84;  
					yygl[82] = 84;  
					yygl[83] = 95;  
					yygl[84] = 95;  
					yygl[85] = 95;  
					yygl[86] = 95;  
					yygl[87] = 95;  
					yygl[88] = 101;  
					yygl[89] = 105;  
					yygl[90] = 105;  
					yygl[91] = 105;  
					yygl[92] = 105;  
					yygl[93] = 105;  
					yygl[94] = 105;  
					yygl[95] = 105;  
					yygl[96] = 120;  
					yygl[97] = 131;  
					yygl[98] = 142;  
					yygl[99] = 142;  
					yygl[100] = 142;  
					yygl[101] = 142;  
					yygl[102] = 143;  
					yygl[103] = 143;  
					yygl[104] = 154;  
					yygl[105] = 175;  
					yygl[106] = 175;  
					yygl[107] = 196;  
					yygl[108] = 217;  
					yygl[109] = 217;  
					yygl[110] = 217;  
					yygl[111] = 217;  
					yygl[112] = 231;  
					yygl[113] = 237;  
					yygl[114] = 237;  
					yygl[115] = 237;  
					yygl[116] = 237;  
					yygl[117] = 237;  
					yygl[118] = 241;  
					yygl[119] = 241;  
					yygl[120] = 241;  
					yygl[121] = 246;  
					yygl[122] = 246;  
					yygl[123] = 261;  
					yygl[124] = 261;  
					yygl[125] = 261;  
					yygl[126] = 261;  
					yygl[127] = 261;  
					yygl[128] = 261;  
					yygl[129] = 261;  
					yygl[130] = 261;  
					yygl[131] = 261;  
					yygl[132] = 261;  
					yygl[133] = 261;  
					yygl[134] = 272;  
					yygl[135] = 272;  
					yygl[136] = 293;  
					yygl[137] = 293;  
					yygl[138] = 293;  
					yygl[139] = 293;  
					yygl[140] = 293;  
					yygl[141] = 293;  
					yygl[142] = 293;  
					yygl[143] = 293;  
					yygl[144] = 293;  
					yygl[145] = 304;  
					yygl[146] = 304;  
					yygl[147] = 304;  
					yygl[148] = 305;  
					yygl[149] = 306;  
					yygl[150] = 307;  
					yygl[151] = 308;  
					yygl[152] = 308;  
					yygl[153] = 308;  
					yygl[154] = 308;  
					yygl[155] = 308;  
					yygl[156] = 308;  
					yygl[157] = 308;  
					yygl[158] = 308;  
					yygl[159] = 309;  
					yygl[160] = 330;  
					yygl[161] = 330;  
					yygl[162] = 330;  
					yygl[163] = 330;  
					yygl[164] = 330;  
					yygl[165] = 330;  
					yygl[166] = 341;  
					yygl[167] = 341;  
					yygl[168] = 341;  
					yygl[169] = 341;  
					yygl[170] = 341;  
					yygl[171] = 355;  
					yygl[172] = 355;  
					yygl[173] = 355;  
					yygl[174] = 355;  
					yygl[175] = 355;  
					yygl[176] = 355;  
					yygl[177] = 355;  
					yygl[178] = 355;  
					yygl[179] = 355;  
					yygl[180] = 355;  
					yygl[181] = 355;  
					yygl[182] = 355;  
					yygl[183] = 355;  
					yygl[184] = 355;  
					yygl[185] = 366;  
					yygl[186] = 387;  
					yygl[187] = 387;  
					yygl[188] = 398;  
					yygl[189] = 398;  
					yygl[190] = 398;  
					yygl[191] = 398;  
					yygl[192] = 410;  
					yygl[193] = 410;  
					yygl[194] = 410;  
					yygl[195] = 423;  
					yygl[196] = 423;  
					yygl[197] = 423;  
					yygl[198] = 423;  
					yygl[199] = 423;  
					yygl[200] = 437;  
					yygl[201] = 437;  
					yygl[202] = 437;  
					yygl[203] = 452;  
					yygl[204] = 468;  
					yygl[205] = 485;  
					yygl[206] = 503;  
					yygl[207] = 522;  
					yygl[208] = 543;  
					yygl[209] = 543;  
					yygl[210] = 543;  
					yygl[211] = 543;  
					yygl[212] = 543;  
					yygl[213] = 554;  
					yygl[214] = 565;  
					yygl[215] = 565;  
					yygl[216] = 565;  
					yygl[217] = 565;  
					yygl[218] = 565;  
					yygl[219] = 565;  
					yygl[220] = 566;  
					yygl[221] = 567;  
					yygl[222] = 568;  
					yygl[223] = 569;  
					yygl[224] = 569;  
					yygl[225] = 569;  
					yygl[226] = 569;  
					yygl[227] = 569;  
					yygl[228] = 569;  
					yygl[229] = 569;  
					yygl[230] = 569;  
					yygl[231] = 569;  
					yygl[232] = 569;  
					yygl[233] = 569;  
					yygl[234] = 569; 

					yygh = new int[yynstates];
					yygh[0] = 19;  
					yygh[1] = 19;  
					yygh[2] = 19;  
					yygh[3] = 25;  
					yygh[4] = 25;  
					yygh[5] = 25;  
					yygh[6] = 31;  
					yygh[7] = 37;  
					yygh[8] = 37;  
					yygh[9] = 37;  
					yygh[10] = 45;  
					yygh[11] = 52;  
					yygh[12] = 52;  
					yygh[13] = 52;  
					yygh[14] = 52;  
					yygh[15] = 52;  
					yygh[16] = 52;  
					yygh[17] = 70;  
					yygh[18] = 70;  
					yygh[19] = 70;  
					yygh[20] = 70;  
					yygh[21] = 70;  
					yygh[22] = 70;  
					yygh[23] = 70;  
					yygh[24] = 70;  
					yygh[25] = 70;  
					yygh[26] = 70;  
					yygh[27] = 70;  
					yygh[28] = 70;  
					yygh[29] = 70;  
					yygh[30] = 70;  
					yygh[31] = 70;  
					yygh[32] = 70;  
					yygh[33] = 70;  
					yygh[34] = 70;  
					yygh[35] = 70;  
					yygh[36] = 70;  
					yygh[37] = 70;  
					yygh[38] = 70;  
					yygh[39] = 70;  
					yygh[40] = 70;  
					yygh[41] = 70;  
					yygh[42] = 70;  
					yygh[43] = 70;  
					yygh[44] = 70;  
					yygh[45] = 70;  
					yygh[46] = 70;  
					yygh[47] = 70;  
					yygh[48] = 70;  
					yygh[49] = 70;  
					yygh[50] = 70;  
					yygh[51] = 70;  
					yygh[52] = 70;  
					yygh[53] = 70;  
					yygh[54] = 70;  
					yygh[55] = 70;  
					yygh[56] = 70;  
					yygh[57] = 70;  
					yygh[58] = 74;  
					yygh[59] = 74;  
					yygh[60] = 75;  
					yygh[61] = 75;  
					yygh[62] = 76;  
					yygh[63] = 76;  
					yygh[64] = 76;  
					yygh[65] = 76;  
					yygh[66] = 76;  
					yygh[67] = 76;  
					yygh[68] = 76;  
					yygh[69] = 76;  
					yygh[70] = 76;  
					yygh[71] = 76;  
					yygh[72] = 76;  
					yygh[73] = 76;  
					yygh[74] = 76;  
					yygh[75] = 76;  
					yygh[76] = 76;  
					yygh[77] = 76;  
					yygh[78] = 76;  
					yygh[79] = 83;  
					yygh[80] = 83;  
					yygh[81] = 83;  
					yygh[82] = 94;  
					yygh[83] = 94;  
					yygh[84] = 94;  
					yygh[85] = 94;  
					yygh[86] = 94;  
					yygh[87] = 100;  
					yygh[88] = 104;  
					yygh[89] = 104;  
					yygh[90] = 104;  
					yygh[91] = 104;  
					yygh[92] = 104;  
					yygh[93] = 104;  
					yygh[94] = 104;  
					yygh[95] = 119;  
					yygh[96] = 130;  
					yygh[97] = 141;  
					yygh[98] = 141;  
					yygh[99] = 141;  
					yygh[100] = 141;  
					yygh[101] = 142;  
					yygh[102] = 142;  
					yygh[103] = 153;  
					yygh[104] = 174;  
					yygh[105] = 174;  
					yygh[106] = 195;  
					yygh[107] = 216;  
					yygh[108] = 216;  
					yygh[109] = 216;  
					yygh[110] = 216;  
					yygh[111] = 230;  
					yygh[112] = 236;  
					yygh[113] = 236;  
					yygh[114] = 236;  
					yygh[115] = 236;  
					yygh[116] = 236;  
					yygh[117] = 240;  
					yygh[118] = 240;  
					yygh[119] = 240;  
					yygh[120] = 245;  
					yygh[121] = 245;  
					yygh[122] = 260;  
					yygh[123] = 260;  
					yygh[124] = 260;  
					yygh[125] = 260;  
					yygh[126] = 260;  
					yygh[127] = 260;  
					yygh[128] = 260;  
					yygh[129] = 260;  
					yygh[130] = 260;  
					yygh[131] = 260;  
					yygh[132] = 260;  
					yygh[133] = 271;  
					yygh[134] = 271;  
					yygh[135] = 292;  
					yygh[136] = 292;  
					yygh[137] = 292;  
					yygh[138] = 292;  
					yygh[139] = 292;  
					yygh[140] = 292;  
					yygh[141] = 292;  
					yygh[142] = 292;  
					yygh[143] = 292;  
					yygh[144] = 303;  
					yygh[145] = 303;  
					yygh[146] = 303;  
					yygh[147] = 304;  
					yygh[148] = 305;  
					yygh[149] = 306;  
					yygh[150] = 307;  
					yygh[151] = 307;  
					yygh[152] = 307;  
					yygh[153] = 307;  
					yygh[154] = 307;  
					yygh[155] = 307;  
					yygh[156] = 307;  
					yygh[157] = 307;  
					yygh[158] = 308;  
					yygh[159] = 329;  
					yygh[160] = 329;  
					yygh[161] = 329;  
					yygh[162] = 329;  
					yygh[163] = 329;  
					yygh[164] = 329;  
					yygh[165] = 340;  
					yygh[166] = 340;  
					yygh[167] = 340;  
					yygh[168] = 340;  
					yygh[169] = 340;  
					yygh[170] = 354;  
					yygh[171] = 354;  
					yygh[172] = 354;  
					yygh[173] = 354;  
					yygh[174] = 354;  
					yygh[175] = 354;  
					yygh[176] = 354;  
					yygh[177] = 354;  
					yygh[178] = 354;  
					yygh[179] = 354;  
					yygh[180] = 354;  
					yygh[181] = 354;  
					yygh[182] = 354;  
					yygh[183] = 354;  
					yygh[184] = 365;  
					yygh[185] = 386;  
					yygh[186] = 386;  
					yygh[187] = 397;  
					yygh[188] = 397;  
					yygh[189] = 397;  
					yygh[190] = 397;  
					yygh[191] = 409;  
					yygh[192] = 409;  
					yygh[193] = 409;  
					yygh[194] = 422;  
					yygh[195] = 422;  
					yygh[196] = 422;  
					yygh[197] = 422;  
					yygh[198] = 422;  
					yygh[199] = 436;  
					yygh[200] = 436;  
					yygh[201] = 436;  
					yygh[202] = 451;  
					yygh[203] = 467;  
					yygh[204] = 484;  
					yygh[205] = 502;  
					yygh[206] = 521;  
					yygh[207] = 542;  
					yygh[208] = 542;  
					yygh[209] = 542;  
					yygh[210] = 542;  
					yygh[211] = 542;  
					yygh[212] = 553;  
					yygh[213] = 564;  
					yygh[214] = 564;  
					yygh[215] = 564;  
					yygh[216] = 564;  
					yygh[217] = 564;  
					yygh[218] = 564;  
					yygh[219] = 565;  
					yygh[220] = 566;  
					yygh[221] = 567;  
					yygh[222] = 568;  
					yygh[223] = 568;  
					yygh[224] = 568;  
					yygh[225] = 568;  
					yygh[226] = 568;  
					yygh[227] = 568;  
					yygh[228] = 568;  
					yygh[229] = 568;  
					yygh[230] = 568;  
					yygh[231] = 568;  
					yygh[232] = 568;  
					yygh[233] = 568;  
					yygh[234] = 568; 

					yyr[yyrc] = new YYRRec(1,-2);yyrc++; 
					yyr[yyrc] = new YYRRec(2,-3);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-3);yyrc++; 
					yyr[yyrc] = new YYRRec(0,-3);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-4);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-4);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-4);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-4);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-4);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-4);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-4);yyrc++; 
					yyr[yyrc] = new YYRRec(3,-6);yyrc++; 
					yyr[yyrc] = new YYRRec(3,-6);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-12);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-12);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-12);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-12);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-10);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-10);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-11);yyrc++; 
					yyr[yyrc] = new YYRRec(2,-11);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-13);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-13);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-13);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-13);yyrc++; 
					yyr[yyrc] = new YYRRec(5,-9);yyrc++; 
					yyr[yyrc] = new YYRRec(2,-22);yyrc++; 
					yyr[yyrc] = new YYRRec(0,-22);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-23);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-20);yyrc++; 
					yyr[yyrc] = new YYRRec(5,-7);yyrc++; 
					yyr[yyrc] = new YYRRec(4,-7);yyrc++; 
					yyr[yyrc] = new YYRRec(3,-7);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-27);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-25);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-25);yyrc++; 
					yyr[yyrc] = new YYRRec(2,-26);yyrc++; 
					yyr[yyrc] = new YYRRec(0,-26);yyrc++; 
					yyr[yyrc] = new YYRRec(3,-28);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-28);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-30);yyrc++; 
					yyr[yyrc] = new YYRRec(2,-30);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-31);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-31);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-31);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-31);yyrc++; 
					yyr[yyrc] = new YYRRec(5,-8);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-33);yyrc++; 
					yyr[yyrc] = new YYRRec(4,-35);yyrc++; 
					yyr[yyrc] = new YYRRec(3,-35);yyrc++; 
					yyr[yyrc] = new YYRRec(2,-35);yyrc++; 
					yyr[yyrc] = new YYRRec(2,-35);yyrc++; 
					yyr[yyrc] = new YYRRec(0,-35);yyrc++; 
					yyr[yyrc] = new YYRRec(3,-38);yyrc++; 
					yyr[yyrc] = new YYRRec(2,-38);yyrc++; 
					yyr[yyrc] = new YYRRec(2,-38);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-38);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-40);yyrc++; 
					yyr[yyrc] = new YYRRec(2,-40);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-42);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-42);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-42);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-42);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-42);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-44);yyrc++; 
					yyr[yyrc] = new YYRRec(3,-45);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-45);yyrc++; 
					yyr[yyrc] = new YYRRec(3,-46);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-46);yyrc++; 
					yyr[yyrc] = new YYRRec(3,-47);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-47);yyrc++; 
					yyr[yyrc] = new YYRRec(3,-48);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-48);yyrc++; 
					yyr[yyrc] = new YYRRec(3,-49);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-49);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-50);yyrc++; 
					yyr[yyrc] = new YYRRec(3,-50);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-51);yyrc++; 
					yyr[yyrc] = new YYRRec(3,-51);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-53);yyrc++; 
					yyr[yyrc] = new YYRRec(3,-53);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-55);yyrc++; 
					yyr[yyrc] = new YYRRec(3,-55);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-57);yyrc++; 
					yyr[yyrc] = new YYRRec(2,-57);yyrc++; 
					yyr[yyrc] = new YYRRec(4,-59);yyrc++; 
					yyr[yyrc] = new YYRRec(3,-59);yyrc++; 
					yyr[yyrc] = new YYRRec(2,-59);yyrc++; 
					yyr[yyrc] = new YYRRec(2,-59);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-59);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-59);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-59);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-52);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-52);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-54);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-54);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-54);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-54);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-56);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-56);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-58);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-58);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-58);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-60);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-60);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-60);yyrc++; 
					yyr[yyrc] = new YYRRec(2,-41);yyrc++; 
					yyr[yyrc] = new YYRRec(4,-41);yyrc++; 
					yyr[yyrc] = new YYRRec(3,-41);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-63);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-63);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-63);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-63);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-63);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-61);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-61);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-61);yyrc++; 
					yyr[yyrc] = new YYRRec(4,-37);yyrc++; 
					yyr[yyrc] = new YYRRec(5,-37);yyrc++; 
					yyr[yyrc] = new YYRRec(5,-37);yyrc++; 
					yyr[yyrc] = new YYRRec(3,-5);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-5);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-32);yyrc++; 
					yyr[yyrc] = new YYRRec(2,-32);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-39);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-39);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-18);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-18);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-62);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-62);yyrc++; 
					yyr[yyrc] = new YYRRec(2,-24);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-24);yyrc++; 
					yyr[yyrc] = new YYRRec(2,-64);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-64);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-17);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-43);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-14);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-14);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-14);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-65);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-65);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-65);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-65);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-65);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-65);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-65);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-65);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-65);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-65);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-66);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-66);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-66);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-66);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-66);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-66);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-66);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-29);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-29);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-29);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-29);yyrc++; 
					yyr[yyrc] = new YYRRec(3,-67);yyrc++; 
					yyr[yyrc] = new YYRRec(3,-67);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-67);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-15);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-34);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-21);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-21);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-36);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-19);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-16);yyrc++;
        }

        public bool yyact(int state, int sym, ref int act)
        {
            int k = yyal[state];
            while (k <= yyah[state] && yya[k].sym != sym) k++;
            if (k > yyah[state]) return false;
            act = yya[k].act;
            return true;
        }
        public bool yygoto(int state, int sym, ref int nstate)
        {
            int k = yygl[state];
            while (k <= yygh[state] && yyg[k].sym != sym) k++;
            if (k > yygh[state]) return false;
            nstate = yyg[k].act;
            return true;
        }

        public void yyerror(string s)
        {
            System.Console.Write(s);
        }

        int yylexpos = -1;
        string yylval = "";

        public int yylex()
        {
            yylexpos++;
            if (yylexpos >= TokenList.Count)
            {
                yylval = "";
                return 0;
            }
            else
            {
                yylval = ((AToken)TokenList[yylexpos]).val;
                return ((AToken)TokenList[yylexpos]).token;
            }
        }

        public bool yyparse()
        {
            Stopwatch watch = new Stopwatch();
            watch.Start();

            parse:

            yysp++;
            if (yysp >= yymaxdepth)
            {
                yyerror("yyparse stack overflow");
                goto abort;
            }

            yys[yysp] = yystate;
            yyv[yysp] = yyval;

            next:

            if (yyd[yystate] == 0 && yychar == -1)
            {
                yychar = yylex();
                if (yychar < 0) yychar = 0;
            }

            yyn = yyd[yystate];
            if (yyn != 0) goto reduce;


            if (! yyact(yystate, yychar, ref yyn)) goto error;
            else if (yyn>0) goto shift;
            else if (yyn<0) goto reduce;
            else            goto accept;

            error:

            if (yyerrflag==0) yyerror("syntax error " + yylval);

            errlab:

            if (yyerrflag==0) yynerrs++;

            if (yyerrflag<=2)
            {
                yyerrflag = 3;
                while (yysp > 0 && !(yyact(yys[yysp], 255, ref yyn) && yyn > 0)) yysp--;

                if (yysp == 0) goto abort;
                yystate = yyn;
                goto parse;
            }
            else
            {
                if (yychar == 0) goto abort;
                yychar = -1; goto next;
            }

            shift:

            yystate = yyn;
            yychar = -1;
            yyval = yylval;
            if (yyerrflag > 0) yyerrflag--;
            goto parse;

            reduce:

            yyflag = yyfnone;
            yyaction(-yyn);
            int l = yyr[-yyn].len;
            for (int z = yysp; z > yysp - l; z--)
                yyv[z] = null;

            yysp -= l;
//            yysp -= yyr[-yyn].len;

            if (yygoto(yys[yysp], yyr[-yyn].sym, ref yyn)) yystate = yyn;

            switch (yyflag)
            {
                case 1: goto accept;
                case 2: goto abort;
                case 3: goto errlab;
            }

            goto parse;

            accept:

            Console.WriteLine("(I) PARSER parsing finished in " + watch.Elapsed);
            watch.Stop();
            return true;

            abort:

            Console.WriteLine("(E) PARSER parsing aborted.");
            watch.Stop();
            return false;
        }
        ////////////////////////////////////////////////////////////////
        /// Scanner - Optimized
        ////////////////////////////////////////////////////////////////

        public bool ScannerOpt(string Input)
        {
            Stopwatch watch = new Stopwatch();
            watch.Start();

            if (Input.Length == 0) return true;
            TokenList = new ArrayList();
            int pos = 0;
            while (1 == 1)
            {
                AToken lasttoken = FindTokenOpt(Input, pos);
                if (lasttoken.token == 0) break;
                if (lasttoken.token != t_ignore) TokenList.Add(lasttoken);
                pos += lasttoken.val.Length;
                if (Input.Length <= pos)
                {
                    Console.WriteLine("(I) PARSER scanning finished in " + watch.Elapsed);
                    return true;
                }
            }
            System.Console.WriteLine(Input);
            System.Console.WriteLine();
            System.Console.WriteLine("No matching token found near: " + Input.Substring(pos));
            return false;
        }
        public AToken FindTokenOpt(string Rest, int startpos)
        {
            Match m;
            int maxlength = 0;
            int besttoken = 0;
            AToken ret = new AToken();
            try
            {

                for (int idx = 0; idx < tList.Count; idx++)
                {
                    m = rList[idx].Match(Rest, startpos);
                    if (m.Success)
                    {
                        if (m.Value.Length > maxlength)
                        {
                            maxlength = m.Value.Length;
                            besttoken = tList[idx];
                            ret.token = besttoken;
                            if (besttoken != 0)
                                ret.val = m.Value;
                        }
                    }
                }

            }
            catch { }
            return ret;
        }
		////////////////////////////////////////////////////////////////
		/// Scanner
		////////////////////////////////////////////////////////////////

		public class AToken
		{
			public int token;
			public string val;
		}

		ArrayList TokenList = new ArrayList();

		public bool Scanner (string Input)
		{
		        if (Input.Length == 0) return true;
			TokenList = new ArrayList();
			while (1==1)
			{
				AToken lasttoken = FindToken(Input);
				if (lasttoken.token == 0) break;
				if (lasttoken.token != t_ignore) TokenList.Add(lasttoken);
				if (Input.Length > lasttoken.val.Length)
				Input = Input.Substring(lasttoken.val.Length); else return true;
			}
                      System.Console.WriteLine(Input);
			System.Console.WriteLine();
			System.Console.WriteLine("No matching token found!");
			return false;
		}
		public AToken FindToken (string Rest)
		{
			ArrayList Results  = new ArrayList();
			ArrayList ResultsV = new ArrayList();
                      try{

			if (Regex.IsMatch(Rest,"^(;)")){
				Results.Add (t_Char59);
				ResultsV.Add(Regex.Match(Rest,"^(;)").Value);}

			if (Regex.IsMatch(Rest,"^(\\{)")){
				Results.Add (t_Char123);
				ResultsV.Add(Regex.Match(Rest,"^(\\{)").Value);}

			if (Regex.IsMatch(Rest,"^(\\})")){
				Results.Add (t_Char125);
				ResultsV.Add(Regex.Match(Rest,"^(\\})").Value);}

			if (Regex.IsMatch(Rest,"^(:)")){
				Results.Add (t_Char58);
				ResultsV.Add(Regex.Match(Rest,"^(:)").Value);}

			if (Regex.IsMatch(Rest,"^(\\|\\|)")){
				Results.Add (t_Char124Char124);
				ResultsV.Add(Regex.Match(Rest,"^(\\|\\|)").Value);}

			if (Regex.IsMatch(Rest,"^(&&)")){
				Results.Add (t_Char38Char38);
				ResultsV.Add(Regex.Match(Rest,"^(&&)").Value);}

			if (Regex.IsMatch(Rest,"^(\\|)")){
				Results.Add (t_Char124);
				ResultsV.Add(Regex.Match(Rest,"^(\\|)").Value);}

			if (Regex.IsMatch(Rest,"^(\\^)")){
				Results.Add (t_Char94);
				ResultsV.Add(Regex.Match(Rest,"^(\\^)").Value);}

			if (Regex.IsMatch(Rest,"^(&)")){
				Results.Add (t_Char38);
				ResultsV.Add(Regex.Match(Rest,"^(&)").Value);}

			if (Regex.IsMatch(Rest,"^(\\()")){
				Results.Add (t_Char40);
				ResultsV.Add(Regex.Match(Rest,"^(\\()").Value);}

			if (Regex.IsMatch(Rest,"^(\\))")){
				Results.Add (t_Char41);
				ResultsV.Add(Regex.Match(Rest,"^(\\))").Value);}

			if (Regex.IsMatch(Rest,"^(!=)")){
				Results.Add (t_Char33Char61);
				ResultsV.Add(Regex.Match(Rest,"^(!=)").Value);}

			if (Regex.IsMatch(Rest,"^(==)")){
				Results.Add (t_Char61Char61);
				ResultsV.Add(Regex.Match(Rest,"^(==)").Value);}

			if (Regex.IsMatch(Rest,"^(<)")){
				Results.Add (t_Char60);
				ResultsV.Add(Regex.Match(Rest,"^(<)").Value);}

			if (Regex.IsMatch(Rest,"^(<=)")){
				Results.Add (t_Char60Char61);
				ResultsV.Add(Regex.Match(Rest,"^(<=)").Value);}

			if (Regex.IsMatch(Rest,"^(>)")){
				Results.Add (t_Char62);
				ResultsV.Add(Regex.Match(Rest,"^(>)").Value);}

			if (Regex.IsMatch(Rest,"^(>=)")){
				Results.Add (t_Char62Char61);
				ResultsV.Add(Regex.Match(Rest,"^(>=)").Value);}

			if (Regex.IsMatch(Rest,"^(\\+)")){
				Results.Add (t_Char43);
				ResultsV.Add(Regex.Match(Rest,"^(\\+)").Value);}

			if (Regex.IsMatch(Rest,"^(\\-)")){
				Results.Add (t_Char45);
				ResultsV.Add(Regex.Match(Rest,"^(\\-)").Value);}

			if (Regex.IsMatch(Rest,"^(%)")){
				Results.Add (t_Char37);
				ResultsV.Add(Regex.Match(Rest,"^(%)").Value);}

			if (Regex.IsMatch(Rest,"^(\\*)")){
				Results.Add (t_Char42);
				ResultsV.Add(Regex.Match(Rest,"^(\\*)").Value);}

			if (Regex.IsMatch(Rest,"^(/)")){
				Results.Add (t_Char47);
				ResultsV.Add(Regex.Match(Rest,"^(/)").Value);}

			if (Regex.IsMatch(Rest,"^(!)")){
				Results.Add (t_Char33);
				ResultsV.Add(Regex.Match(Rest,"^(!)").Value);}

			if (Regex.IsMatch(Rest,"^(RULE)")){
				Results.Add (t_RULE);
				ResultsV.Add(Regex.Match(Rest,"^(RULE)").Value);}

			if (Regex.IsMatch(Rest,"^(\\*[\\s\\t\\x00]*=)")){
				Results.Add (t_Char42Char61);
				ResultsV.Add(Regex.Match(Rest,"^(\\*[\\s\\t\\x00]*=)").Value);}

			if (Regex.IsMatch(Rest,"^(\\+[\\s\\t\\x00]*=)")){
				Results.Add (t_Char43Char61);
				ResultsV.Add(Regex.Match(Rest,"^(\\+[\\s\\t\\x00]*=)").Value);}

			if (Regex.IsMatch(Rest,"^(\\-[\\s\\t\\x00]*=)")){
				Results.Add (t_Char45Char61);
				ResultsV.Add(Regex.Match(Rest,"^(\\-[\\s\\t\\x00]*=)").Value);}

			if (Regex.IsMatch(Rest,"^(/[\\s\\t\\x00]*=)")){
				Results.Add (t_Char47Char61);
				ResultsV.Add(Regex.Match(Rest,"^(/[\\s\\t\\x00]*=)").Value);}

			if (Regex.IsMatch(Rest,"^(=)")){
				Results.Add (t_Char61);
				ResultsV.Add(Regex.Match(Rest,"^(=)").Value);}

			if (Regex.IsMatch(Rest,"^(ELSE)")){
				Results.Add (t_ELSE);
				ResultsV.Add(Regex.Match(Rest,"^(ELSE)").Value);}

			if (Regex.IsMatch(Rest,"^(IF)")){
				Results.Add (t_IF);
				ResultsV.Add(Regex.Match(Rest,"^(IF)").Value);}

			if (Regex.IsMatch(Rest,"^(WHILE)")){
				Results.Add (t_WHILE);
				ResultsV.Add(Regex.Match(Rest,"^(WHILE)").Value);}

			if (Regex.IsMatch(Rest,"^(\\.)")){
				Results.Add (t_Char46);
				ResultsV.Add(Regex.Match(Rest,"^(\\.)").Value);}

			if (Regex.IsMatch(Rest,"^(NULL)")){
				Results.Add (t_NULL);
				ResultsV.Add(Regex.Match(Rest,"^(NULL)").Value);}

			if (Regex.IsMatch(Rest,"^((?=[A-Z])(EACH_SEC|IF_(AE|ALT|ANYKEY|APO|BKSL|BKSP|BRACKL|BRACKR|CAL|CAR|COMMA|CTRL|CUD|CUL|CUR|CUU|DEL|END|EQUALS|ESC|HOME|INS|JOY4|LEFT|LOAD|MIDDLE|MINUS|MSTOP|OE|PAUSE|PERIOD|PGDN|PGUP|PLUS|RIGHT|SEMIC|SLASH|SPACE|START|SZ|TAB|TAST|UE|F(1[0-2]|[1-9])|[0-9A-Z])|LAYERS|MESSAGES|PANELS))")){
				Results.Add (t_event);
				ResultsV.Add(Regex.Match(Rest,"^((?=[A-Z])(EACH_SEC|IF_(AE|ALT|ANYKEY|APO|BKSL|BKSP|BRACKL|BRACKR|CAL|CAR|COMMA|CTRL|CUD|CUL|CUR|CUU|DEL|END|EQUALS|ESC|HOME|INS|JOY4|LEFT|LOAD|MIDDLE|MINUS|MSTOP|OE|PAUSE|PERIOD|PGDN|PGUP|PLUS|RIGHT|SEMIC|SLASH|SPACE|START|SZ|TAB|TAST|UE|F(1[0-2]|[1-9])|[0-9A-Z])|LAYERS|MESSAGES|PANELS))").Value);}

			if (Regex.IsMatch(Rest,"^((?=[A-Z])(VIDEO|NEXUS|LIGHT_ANGLE|IBANK|DRUMBANK|MIDI_PITCH|BIND|MAPFILE|SAVEDIR|PATH|DITHER|SAVE_KEYS|REMOTE_KEYS))")){
				Results.Add (t_global);
				ResultsV.Add(Regex.Match(Rest,"^((?=[A-Z])(VIDEO|NEXUS|LIGHT_ANGLE|IBANK|DRUMBANK|MIDI_PITCH|BIND|MAPFILE|SAVEDIR|PATH|DITHER|SAVE_KEYS|REMOTE_KEYS))").Value);}

			if (Regex.IsMatch(Rest,"^((?=[A-Z])(MODEL|SOUND|MUSIC|FLIC|BMAP|OVLY|FONT))")){
				Results.Add (t_asset);
				ResultsV.Add(Regex.Match(Rest,"^((?=[A-Z])(MODEL|SOUND|MUSIC|FLIC|BMAP|OVLY|FONT))").Value);}

			if (Regex.IsMatch(Rest,"^((?=[A-Z])(OVERLAY|PANEL|PALETTE|REGION|SKILL|STRING|SYNONYM|TEXTURE|TEXT|VIEW|WALL|WAY))")){
				Results.Add (t_object);
				ResultsV.Add(Regex.Match(Rest,"^((?=[A-Z])(OVERLAY|PANEL|PALETTE|REGION|SKILL|STRING|SYNONYM|TEXTURE|TEXT|VIEW|WALL|WAY))").Value);}

			if (Regex.IsMatch(Rest,"^((?=[A-Z])(ACTION|RULES))")){
				Results.Add (t_function);
				ResultsV.Add(Regex.Match(Rest,"^((?=[A-Z])(ACTION|RULES))").Value);}

			if (Regex.IsMatch(Rest,"^((?=[A-Z])(ACOS|COS|ATAN|TAN|SIGN|INT|EXP|LOG10|LOG2|LOG))")){
				Results.Add (t_math);
				ResultsV.Add(Regex.Match(Rest,"^((?=[A-Z])(ACOS|COS|ATAN|TAN|SIGN|INT|EXP|LOG10|LOG2|LOG))").Value);}

			if (Regex.IsMatch(Rest,"^(((EACH_TICK|EACH_SEC|PANELS|LAYERS|MESSAGES)\\.(1[0-6]|[1-9])))")){
				Results.Add (t_list);
				ResultsV.Add(Regex.Match(Rest,"^(((EACH_TICK|EACH_SEC|PANELS|LAYERS|MESSAGES)\\.(1[0-6]|[1-9])))").Value);}

			if (Regex.IsMatch(Rest,"^((?=[A-Z])(ACCELERATION|ACTIONS|ACTIVE_(NEXUS|OBJTICKS|TARGETS)|ACTOR_(CEIL_HGT|CLIMB|DIST|FLOOR_HGT|IMPACT_V[X-Z]|WIDTH)|APPEND_MODE|ASPECT|BLUR_MODE|BOUNCE_V[X-Y]|CDAUDIO_VOL|CD_TRACK|CHANNEL_[0-7]|CHANNEL|CLIPPED|CLIPPING|COLOR_(ACTORS|BORDER|PLAYER|THINGS|WALLS)|DARK_DIST|DEBUG_MODE|DELTA_ANGLE|ERROR|FLIC_FRAME|FLOOR_MODE|FORCE_(AHEAD|ROT|STRAFE|TILT|UP)|FRAME_COLOR|FRICTION|HALF_PI|HIT_(DIST|MINDIST|X|Y)|IMPACT_V(ROT|[X-Z])|INERTIA|INV_DIST|JOYSTICK_[X-Y]|JOY_(4|SENSE)|KEY_(ALT|ANY|APO|BKSL|BKSP|BRACKL|BRACKR|CAL|CAR|COMMA|CTRL|CUD|CUL|CUR|CUU|DEL|END|ENTER|EQUALS|ESC|HOME|INS|JOY4|MINUS|PAUSE|PERIOD|PGDN|PGUP|PLUS|SEMIC|SENSE|SHIFT|SLASH|SPACE|SZ|TAB|F(1[0-2]|[1-9])|[A-Z0-9])|LIGHT_DIST|LOAD_MODE|MAP_(CENTER[X-Y]|(EDGE_[X-Y][1-2])|LAYER|MAX[X-Y]|MIN[X-Y]|MODE|OFFS[X-Y]|ROT|SCALE)|MAX_DIST|MICKEY_[X-Y]|MINV_DIST|MOTION_BLUR|MOUSE_(ANGLE|CALM|LEFT|MIDDLE|MODE|MOVING|RIGHT|SENSE|TIME|X|Y)|MOVE_(ANGLE|MODE)|MUSIC_VOL|MY_(X[1-2]|Y[1-2]|Z[1-2]|X|Y)|PANEL_LAYER|PI|PLAYER_(ANGLE|ARC|CLIMB|COS|DEPTH|HGT|LAST_[X-Y]|LIGHT|MSIN|SIN|SIZE|SPEED|TILT|VROT|V[X-Z]|WIDTH|[X-Z])|PSOUND_(TONE|VOL)|REAL_SPEED|REMOTE_[0-1]|RENDER_MODE|SCREEN_(HGT|WIDTH|X|Y)|SECS|SHIFT_SENSE|SHOOT_(ANGLE|FAC|RANGE|SECTOR|X|Y)|SKIP_FRAMES|SKY_OFFS_[X-Y]|SLOPE_(AHEAD|SIDE|X|Y)|SOUND_VOL|SPANS|STEPS|STR_LEN|TEXT_LAYER|THING_(DIST|WIDTH)|TICKS|TIME_(ACTIONS|CLIPPING|CORR|DRAW|FAC|FBUFFER|SLICES|TARGETS|VERTICES)|TOUCH_(DIST|MODE|RANGE|STATE)|TWO_PI|WALK_(PERIOD|TIME)|WALK|WAVE_PERIOD|WAVE|PALANIM_DELAY))")){
				Results.Add (t_skill);
				ResultsV.Add(Regex.Match(Rest,"^((?=[A-Z])(ACCELERATION|ACTIONS|ACTIVE_(NEXUS|OBJTICKS|TARGETS)|ACTOR_(CEIL_HGT|CLIMB|DIST|FLOOR_HGT|IMPACT_V[X-Z]|WIDTH)|APPEND_MODE|ASPECT|BLUR_MODE|BOUNCE_V[X-Y]|CDAUDIO_VOL|CD_TRACK|CHANNEL_[0-7]|CHANNEL|CLIPPED|CLIPPING|COLOR_(ACTORS|BORDER|PLAYER|THINGS|WALLS)|DARK_DIST|DEBUG_MODE|DELTA_ANGLE|ERROR|FLIC_FRAME|FLOOR_MODE|FORCE_(AHEAD|ROT|STRAFE|TILT|UP)|FRAME_COLOR|FRICTION|HALF_PI|HIT_(DIST|MINDIST|X|Y)|IMPACT_V(ROT|[X-Z])|INERTIA|INV_DIST|JOYSTICK_[X-Y]|JOY_(4|SENSE)|KEY_(ALT|ANY|APO|BKSL|BKSP|BRACKL|BRACKR|CAL|CAR|COMMA|CTRL|CUD|CUL|CUR|CUU|DEL|END|ENTER|EQUALS|ESC|HOME|INS|JOY4|MINUS|PAUSE|PERIOD|PGDN|PGUP|PLUS|SEMIC|SENSE|SHIFT|SLASH|SPACE|SZ|TAB|F(1[0-2]|[1-9])|[A-Z0-9])|LIGHT_DIST|LOAD_MODE|MAP_(CENTER[X-Y]|(EDGE_[X-Y][1-2])|LAYER|MAX[X-Y]|MIN[X-Y]|MODE|OFFS[X-Y]|ROT|SCALE)|MAX_DIST|MICKEY_[X-Y]|MINV_DIST|MOTION_BLUR|MOUSE_(ANGLE|CALM|LEFT|MIDDLE|MODE|MOVING|RIGHT|SENSE|TIME|X|Y)|MOVE_(ANGLE|MODE)|MUSIC_VOL|MY_(X[1-2]|Y[1-2]|Z[1-2]|X|Y)|PANEL_LAYER|PI|PLAYER_(ANGLE|ARC|CLIMB|COS|DEPTH|HGT|LAST_[X-Y]|LIGHT|MSIN|SIN|SIZE|SPEED|TILT|VROT|V[X-Z]|WIDTH|[X-Z])|PSOUND_(TONE|VOL)|REAL_SPEED|REMOTE_[0-1]|RENDER_MODE|SCREEN_(HGT|WIDTH|X|Y)|SECS|SHIFT_SENSE|SHOOT_(ANGLE|FAC|RANGE|SECTOR|X|Y)|SKIP_FRAMES|SKY_OFFS_[X-Y]|SLOPE_(AHEAD|SIDE|X|Y)|SOUND_VOL|SPANS|STEPS|STR_LEN|TEXT_LAYER|THING_(DIST|WIDTH)|TICKS|TIME_(ACTIONS|CLIPPING|CORR|DRAW|FAC|FBUFFER|SLICES|TARGETS|VERTICES)|TOUCH_(DIST|MODE|RANGE|STATE)|TWO_PI|WALK_(PERIOD|TIME)|WALK|WAVE_PERIOD|WAVE|PALANIM_DELAY))").Value);}

			if (Regex.IsMatch(Rest,"^((?=[A-Z])(THERE|MY|HIT|TOUCH_TEXT|TOUCHED|TOUCH_TEX|TOUCH_REG|COMMAND_LINE|REGION[1-8]))")){
				Results.Add (t_synonym);
				ResultsV.Add(Regex.Match(Rest,"^((?=[A-Z])(THERE|MY|HIT|TOUCH_TEXT|TOUCHED|TOUCH_TEX|TOUCH_REG|COMMAND_LINE|REGION[1-8]))").Value);}

			if (Regex.IsMatch(Rest,"^(CLIP_DIST)")){
				Results.Add (t_ambigChar95globalChar95property);
				ResultsV.Add(Regex.Match(Rest,"^(CLIP_DIST)").Value);}

			if (Regex.IsMatch(Rest,"^((?=[A-Z])(EACH_TICK|IF_ENTER|IF_HIT|IF_KLICK))")){
				Results.Add (t_ambigChar95eventChar95property);
				ResultsV.Add(Regex.Match(Rest,"^((?=[A-Z])(EACH_TICK|IF_ENTER|IF_HIT|IF_KLICK))").Value);}

			if (Regex.IsMatch(Rest,"^((?=[A-Z])(THING|ACTOR))")){
				Results.Add (t_ambigChar95objectChar95flag);
				ResultsV.Add(Regex.Match(Rest,"^((?=[A-Z])(THING|ACTOR))").Value);}

			if (Regex.IsMatch(Rest,"^((?=[A-Z])(SIN|ASIN|SQRT|ABS))")){
				Results.Add (t_ambigChar95mathChar95command);
				ResultsV.Add(Regex.Match(Rest,"^((?=[A-Z])(SIN|ASIN|SQRT|ABS))").Value);}

			if (Regex.IsMatch(Rest,"^(RANDOM)")){
				Results.Add (t_ambigChar95mathChar95skillChar95property);
				ResultsV.Add(Regex.Match(Rest,"^(RANDOM)").Value);}

			if (Regex.IsMatch(Rest,"^(HERE)")){
				Results.Add (t_ambigChar95synonymChar95flag);
				ResultsV.Add(Regex.Match(Rest,"^(HERE)").Value);}

			if (Regex.IsMatch(Rest,"^((?=[A-Z])(FLOOR_HGT|CEIL_HGT|AMBIENT|RESULT|NODE))")){
				Results.Add (t_ambigChar95skillChar95property);
				ResultsV.Add(Regex.Match(Rest,"^((?=[A-Z])(FLOOR_HGT|CEIL_HGT|AMBIENT|RESULT|NODE))").Value);}

			if (Regex.IsMatch(Rest,"^(MSPRITE)")){
				Results.Add (t_ambigChar95globalChar95synonymChar95property);
				ResultsV.Add(Regex.Match(Rest,"^(MSPRITE)").Value);}

			if (Regex.IsMatch(Rest,"^((FLAG[1-8]))")){
				Results.Add (t_ambigChar95skillChar95flag);
				ResultsV.Add(Regex.Match(Rest,"^((FLAG[1-8]))").Value);}

			if (Regex.IsMatch(Rest,"^([0-9]+)")){
				Results.Add (t_integer);
				ResultsV.Add(Regex.Match(Rest,"^([0-9]+)").Value);}

			if (Regex.IsMatch(Rest,"^(([0-9]*\\.[0-9]+)|([0-9]+\\.[0-9]*))")){
				Results.Add (t_fixed);
				ResultsV.Add(Regex.Match(Rest,"^(([0-9]*\\.[0-9]+)|([0-9]+\\.[0-9]*))").Value);}

			if (Regex.IsMatch(Rest,"^([A-Za-z0-9_][A-Za-z0-9_\\?]*(\\.[1-9][0-9]?)?)")){
				Results.Add (t_identifier);
				ResultsV.Add(Regex.Match(Rest,"^([A-Za-z0-9_][A-Za-z0-9_\\?]*(\\.[1-9][0-9]?)?)").Value);}

			if (Regex.IsMatch(Rest,"^(<[\\s]?[^<;:\" ]+\\.[^<;:\" ]+[\\s]?>)")){
				Results.Add (t_file);
				ResultsV.Add(Regex.Match(Rest,"^(<[\\s]?[^<;:\" ]+\\.[^<;:\" ]+[\\s]?>)").Value);}

			if (Regex.IsMatch(Rest,"^(\"(\\\\\"|.|[\\r\\n])*?\")")){
				Results.Add (t_string);
				ResultsV.Add(Regex.Match(Rest,"^(\"(\\\\\"|.|[\\r\\n])*?\")").Value);}

			if (Regex.IsMatch(Rest,"^([\\r\\n\\t\\s\\x00,]|:=|(#.*(\\n|$))|(//.*(\\n|$))|(/\\*(.|[\\r\\n])*?\\*/))")){
				Results.Add (t_ignore);
				ResultsV.Add(Regex.Match(Rest,"^([\\r\\n\\t\\s\\x00,]|:=|(#.*(\\n|$))|(//.*(\\n|$))|(/\\*(.|[\\r\\n])*?\\*/))").Value);}

			}catch{}
			int maxlength = 0;
			int besttoken = 0;
			AToken ret = new AToken();
		        ret.token = besttoken;
			for (int i = 0; i < Results.Count; i++){
				if (ResultsV[i].ToString().Length > maxlength)
				{
					maxlength = ResultsV[i].ToString().Length;
					besttoken = (int)Results[i];
		         	        ret.token = besttoken;
		                  	if (besttoken != 0)
			                ret.val   = ResultsV[i].ToString();
				}
			}
			return ret;
		}


	}
}
