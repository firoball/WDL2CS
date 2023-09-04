using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;
using WDL2CS;
using System.Diagnostics;

namespace VCCCompiler
{
    /// <summary>
    /// Zusammenfassung für MyCompiler.
    /// </summary>
    class MyCompiler
    {
        readonly List<int> tList = new List<int>();
        readonly List<Regex> rList = new List<Regex>();
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
            tList.Add(t_flag);
            rList.Add(new Regex("\\G((?=[A-Z])(FLOOR_DESCEND|CEIL_DESCEND|FLOOR_ASCEND|FLOOR_LIFTED|CEIL_ASCEND|CEIL_LIFTED|TRANSPARENT|CANDELABER|DIAPHANOUS|IMMATERIAL|IMPASSABLE|PORTCULLIS|AUTORANGE|CAREFULLY|CONDENSED|INVISIBLE|SENSITIVE|BERKELEY|CENTER_X|CENTER_Y|LIGHTMAP|PASSABLE|SAVE_ALL|CLUSTER|CURTAIN|FRAGILE|NO_CLIP|ONESHOT|REFRESH|VISIBLE|ABSPOS|BEHIND|GROUND|LIFTED|MASTER|NARROW|RELPOS|SHADOW|STICKY|FENCE|GHOST|LIBER|MOVED|SLOOP|BASE|BLUR|CLIP|FLAT|HARD|PLAY|SEEN|WIRE|FAR|SKY))"));
            tList.Add(t_property);
            rList.Add(new Regex("\\G((?=[A-Z])(ALBEDO|ANGLE|ASPEED|ATTACH|BELOW|BMAPS|BUTTON|CEIL_(ANGLE|OFFS_[X-Y]|TEX)|CYCLES|CYCLE|DEFAULT|DELAY|DIGITS|DISTANCE|DIST|EACH_CYCLE_(C|F)|EACH_CYCLE|FLAGS|FLOOR_(ANGLE|OFFS_[X-Y]|TEX)|FOOT_HGT|FRAME|GENIUS|HBAR|HEIGHT|HSLIDER|IF_(ARISE|ARRIVED|DIVE|FAR|LEAVE|NEAR|RELEASE|TOUCH)|INDEX|LAYER|LEFT|LENGTH|MAP_COLOR|MASK|MAX|MIN|MIRROR|OFFSET_[X-Y]|OVLYS|PALFILE|PAN_MAP|PICTURE|POSITION|POS_[X-Y]|RADIANCE|RANGE|REL_(ANGLE|DIST)|RIGHT|SCALE_(XY|X|Y)|SCYCLES|SCYCLE|SCALE|SDIST|SIDES|SIDE|SIZE_[X-Y]|SKILL[1-8]|SPEED|STRINGS|SVDIST|SVOL|TARGET_(MAP|X|Y)|TARGET|TEXTURE[1-4]|THING_HGT|TITLE|TOP|TOUCH|TYPE|VAL|VBAR|VSLIDER|VSPEED|WAYPOINT|WINDOW|[X-Z][1-2]|[X-Z]))"));
            tList.Add(t_command);
            rList.Add(new Regex("\\G((?=[A-Z])(ACCEL|ADD_STRING|ADDT|ADD|AND|BEEP|BRANCH|BREAK|CALL|DROP|END|EXCLUSIVE|EXEC_RULES|EXIT|EXPLODE|FADE_PAL|FIND|FREEZE|GETMIDI|GOTO|IF_(ABOVE|BELOW|EQUAL|MAX|MIN|NEQUAL)|INKEY|INPORT|LEVEL|LIFT|LOAD_INFO|LOAD|LOCATE|MAP|MIDI_COM|NEXT_(MY_THERE|MY|THERE)|NOP|OUTPORT|PLACE|PLAY_(CD|DEMO|FLICFILE|FLIC|SONG_ONCE|SONG|SOUNDFILE|SOUND)|PRINTFILE|PRINT_(STRING|VALUE)|PUSH|RANDOMIZE|ROTATE|SAVE_(DEMO|INFO)|SCAN|SCREENSHOT|SETMIDI|SET_(ALL|INFO|SKILL|STRING)|SET|SHAKE|SHIFT|SHOOT|SKIP|STOP_(DEMO|FLIC|SOUND)|SUBT|SUB|TILT|TO_STRING|WAITT|WAIT_TICKS|WAIT))"));
            tList.Add(t_list);
            rList.Add(new Regex("\\G((?=[A-Z])((EACH_TICK|EACH_SEC|PANELS|LAYERS|MESSAGES)\\.(1[0-6]|[1-9])))"));
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
            tList.Add(t_ambigChar95commandChar95flag);
            rList.Add(new Regex("\\G(SAVE)"));
            tList.Add(t_ambigChar95globalChar95synonymChar95property);
            rList.Add(new Regex("\\G(MSPRITE)"));
            tList.Add(t_ambigChar95commandChar95property);
            rList.Add(new Regex("\\G(DO)"));
            tList.Add(t_ambigChar95skillChar95flag);
            rList.Add(new Regex("\\G(FLAG[1-8])"));
            tList.Add(t_integer);
            rList.Add(new Regex("\\G([0-9]+)"));
            tList.Add(t_fixed);
            rList.Add(new Regex("\\G(([0-9]*\\.[0-9]+)|([0-9]+\\.[0-9]*))"));
            tList.Add(t_identifier);
            rList.Add(new Regex("\\G([A-Za-z0-9_][A-Za-z0-9_\\?]*(\\.[1-9][0-9]?)?)"));
//            rList.Add(new Regex("\\G([a-z0-9_][a-z0-9_\\?]*(\\.[1-9][0-9]?)?)"));
            tList.Add(t_file);
            rList.Add(new Regex("\\G(<[\\s]?[^<;:\" ]+\\.[^<;:\" ]+[\\s]?>)"));
            tList.Add(t_string);
            rList.Add(new Regex("\\G(\"(\\\\\"|.|[\\r\\n])*?\")"));
            tList.Add(t_ignore);
            rList.Add(new Regex("\\G([\\r\\n\\t\\s\\x00,]|:=|(#.*(\\n|$))|(//.*(\\n|$))|(/\\*(.|[\\r\\n])*?\\*/))"));
            Regex.CacheSize += rList.Count;
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
                int t_flag = 297;
                int t_property = 298;
                int t_command = 299;
                int t_list = 300;
                int t_skill = 301;
                int t_synonym = 302;
                int t_ambigChar95globalChar95property = 303;
                int t_ambigChar95eventChar95property = 304;
                int t_ambigChar95objectChar95flag = 305;
                int t_ambigChar95mathChar95command = 306;
                int t_ambigChar95mathChar95skillChar95property = 307;
                int t_ambigChar95synonymChar95flag = 308;
                int t_ambigChar95skillChar95property = 309;
                int t_ambigChar95commandChar95flag = 310;
                int t_ambigChar95globalChar95synonymChar95property = 311;
                int t_ambigChar95commandChar95property = 312;
                int t_ambigChar95skillChar95flag = 313;
                int t_integer = 314;
                int t_fixed = 315;
                int t_identifier = 316;
                int t_file = 317;
                int t_string = 318;
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
            Stopwatch watch = new Stopwatch();
            watch.Start();
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
         yyval = Sections.AddActionSection(yyv[yysp-0]);
         
       break;
							case   10 : 
         yyval = Sections.AddObjectSection(yyv[yysp-0]);
         
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
         yyval = Formatter.FormatGlobal(yyv[yysp-0]);
         
       break;
							case   15 : 
         yyval = Formatter.FormatGlobal(yyv[yysp-0]);
         
       break;
							case   16 : 
         yyval = Formatter.FormatGlobal(yyv[yysp-0]);
         
       break;
							case   17 : 
         yyval = Formatter.FormatTargetSkill(yyv[yysp-0]);
         
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
         yyval = yyv[yysp-0];
         
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
         yyval = Actions.CreateMarker(yyv[yysp-2], yyv[yysp-0]);
         
       break;
							case   50 : 
         yyval = yyv[yysp-2] + yyv[yysp-0];
         
       break;
							case   51 : 
         yyval = Actions.CreateMarker(yyv[yysp-2], yyv[yysp-0]);
         
       break;
							case   52 : 
         yyval = yyv[yysp-1] + yyv[yysp-0];
         
       break;
							case   53 : 
         yyval = yyv[yysp-1] + yyv[yysp-0];
         
       break;
							case   54 : 
         yyval = "";
         
       break;
							case   55 : 
         yyval = Actions.CreateInstruction(yyv[yysp-2]);
         
       break;
							case   56 : 
         //Capture and discard bogus code
         yyval = Actions.CreateInvalidInstruction(yyv[yysp-2]);
         
       break;
							case   57 : 
         yyval = yyv[yysp-1];
         
       break;
							case   58 : 
         yyval = Actions.CreateInstruction(yyv[yysp-1]);
         
       break;
							case   59 : 
         yyval = "";
         
       break;
							case   60 : 
         yyval = "";
         Actions.AddInstructionParameter(yyv[yysp-0]);
         
       break;
							case   61 : 
         yyval = "";
         Actions.AddInstructionParameter(yyv[yysp-1]);
         
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
         yyval = yyv[yysp-0];
         
       break;
							case   67 : 
         yyval = yyv[yysp-0];
         
       break;
							case   68 : 
         yyval = yyv[yysp-2] + " || " + yyv[yysp-0];
         
       break;
							case   69 : 
         yyval = yyv[yysp-0];
         
       break;
							case   70 : 
         yyval = yyv[yysp-2] + " && " + yyv[yysp-0];
         
       break;
							case   71 : 
         yyval = yyv[yysp-0];
         
       break;
							case   72 : 
         yyval = yyv[yysp-2] + " | " + yyv[yysp-0];
         
       break;
							case   73 : 
         yyval = yyv[yysp-0];
         
       break;
							case   74 : 
         yyval = yyv[yysp-2] + " ^ " + yyv[yysp-0];
         
       break;
							case   75 : 
         yyval = yyv[yysp-0];
         
       break;
							case   76 : 
         yyval = yyv[yysp-2] + " & " + yyv[yysp-0];
         
       break;
							case   77 : 
         yyval = yyv[yysp-0];
         
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
         yyval = yyv[yysp-2] + yyv[yysp-1] + yyv[yysp-0];
         
       break;
							case   86 : 
         yyval = yyv[yysp-0];
         
       break;
							case   87 : 
         yyval = yyv[yysp-1] + yyv[yysp-0];
         
       break;
							case   88 : 
         //yyval = yyv[yysp-1] + "." + yyv[yysp-0]); //fixes things like "18,4"
         Console.WriteLine("(W) PARSER discarded superfluous token in expression: , " + yyv[yysp-0]);
         yyval = yyv[yysp-1]; //this is what supposedly happens in A3
         
       break;
							case   89 : 
         yyval = Formatter.FormatMath(yyv[yysp-3]) + "(" + yyv[yysp-1] + ")";
         
       break;
							case   90 : 
         yyval = "(" + yyv[yysp-1] + ")";
         
       break;
							case   91 : 
         //yyval = Formatter.FormatTargetSkill(yyv[yysp-1] + yyv[yysp-0]); //fixes things like "Skill 6"
         Console.WriteLine("(W) PARSER discarded superfluous token in expression: " + yyv[yysp-1]);
         yyval = yyv[yysp-0]; //this is what supposedly happens in A3
         
       break;
							case   92 : 
         yyval = yyv[yysp-0];
         
       break;
							case   93 : 
         yyval = Formatter.FormatTargetSkill(yyv[yysp-0]);
         
       break;
							case   94 : 
         yyval = yyv[yysp-0];
         
       break;
							case   95 : 
         yyval = " != ";
         
       break;
							case   96 : 
         yyval = " == ";
         
       break;
							case   97 : 
         yyval = " < ";
         
       break;
							case   98 : 
         yyval = " <= ";
         
       break;
							case   99 : 
         yyval = " > ";
         
       break;
							case  100 : 
         yyval = " >= ";
         
       break;
							case  101 : 
         yyval = " + ";
         
       break;
							case  102 : 
         yyval = " - ";
         
       break;
							case  103 : 
         yyval = " % ";
         
       break;
							case  104 : 
         yyval = " * ";
         
       break;
							case  105 : 
         yyval = " / ";
         
       break;
							case  106 : 
         yyval = "!";
         
       break;
							case  107 : 
         yyval = "+";
         
       break;
							case  108 : 
         yyval = "-";
         
       break;
							case  109 : 
         //yyval = yyv[yysp-1] + yyv[yysp-0];
         yyval = Actions.CreateExpression(yyv[yysp-0]);
         
       break;
							case  110 : 
         //yyval = yyv[yysp-3] + yyv[yysp-2] + yyv[yysp-1] + yyv[yysp-0];
         yyval = Actions.CreateExpression(Formatter.FormatExpression(yyv[yysp-2], yyv[yysp-1], yyv[yysp-0]));
         
       break;
							case  111 : 
         //yyval = yyv[yysp-2] + yyv[yysp-1] + yyv[yysp-0];
         yyval = Actions.CreateExpression(Formatter.FormatExpression(yyv[yysp-2], yyv[yysp-1], yyv[yysp-0]));
         
       break;
							case  112 : 
         yyval = " *= ";
         
       break;
							case  113 : 
         yyval = " += ";
         
       break;
							case  114 : 
         yyval = " -= ";
         
       break;
							case  115 : 
         yyval = " /= ";
         
       break;
							case  116 : 
         yyval = " = ";
         
       break;
							case  117 : 
         yyval = yyv[yysp-0];
         
       break;
							case  118 : 
         yyval = yyv[yysp-0];
         
       break;
							case  119 : 
         yyval = yyv[yysp-0];
         
       break;
							case  120 : 
         yyval = Actions.CreateElseCondition(yyv[yysp-1]);
         
       break;
							case  121 : 
         yyval = Actions.CreateIfCondition(yyv[yysp-3], yyv[yysp-1]);
         
       break;
							case  122 : 
         yyval = Actions.CreateWhileCondition(yyv[yysp-3], yyv[yysp-1]);
         
       break;
							case  123 : 
         yyval = yyv[yysp-2] + "." + yyv[yysp-0];
         
       break;
							case  124 : 
         yyval = yyv[yysp-2] + "." + yyv[yysp-0];
         
       break;
							case  125 : 
         yyval = yyv[yysp-0];
         
       break;
							case  126 : 
         yyval = yyv[yysp-0];
         
       break;
							case  127 : 
         yyval = yyv[yysp-1] + yyv[yysp-0];
         
       break;
							case  128 : 
         yyval = Formatter.FormatProperty(yyv[yysp-0]);
         
       break;
							case  129 : 
         yyval = Formatter.FormatProperty(yyv[yysp-0]);
         
       break;
							case  130 : 
         yyval = Formatter.FormatProperty(yyv[yysp-0]);
         
       break;
							case  131 : 
         yyval = Formatter.FormatProperty(yyv[yysp-0]);
         
       break;
							case  132 : 
         yyval = Formatter.FormatProperty(yyv[yysp-0]);
         
       break;
							case  133 : 
         yyval = Formatter.FormatProperty(yyv[yysp-0]);
         
       break;
							case  134 : 
         yyval = Formatter.FormatProperty(yyv[yysp-0]);
         
       break;
							case  135 : 
         yyval = Formatter.FormatProperty(yyv[yysp-0]);
         
       break;
							case  136 : 
         yyval = Formatter.FormatProperty(yyv[yysp-0]);
         
       break;
							case  137 : 
         yyval = Formatter.FormatProperty(yyv[yysp-0]);
         
       break;
							case  138 : 
         yyval = Formatter.FormatProperty(yyv[yysp-0]);
         
       break;
							case  139 : 
         yyval = Formatter.FormatProperty(yyv[yysp-0]);
         
       break;
							case  140 : 
         yyval = Formatter.FormatProperty(yyv[yysp-0]);
         
       break;
							case  141 : 
         yyval = Formatter.FormatProperty(yyv[yysp-0]);
         
       break;
							case  142 : 
         yyval = Formatter.FormatCommand(yyv[yysp-0]);
         
       break;
							case  143 : 
         yyval = Formatter.FormatCommand(yyv[yysp-0]);
         
       break;
							case  144 : 
         yyval = Formatter.FormatCommand(yyv[yysp-0]);
         
       break;
							case  145 : 
         yyval = Formatter.FormatCommand(yyv[yysp-0]);
         
       break;
							case  146 : 
         yyval = Formatter.FormatCommand(yyv[yysp-0]);
         
       break;
							case  147 : 
         yyval = Formatter.FormatNumber(yyv[yysp-0]);
         
       break;
							case  148 : 
         yyval = Formatter.FormatNumber(yyv[yysp-0]);
         
       break;
							case  149 : 
         yyval = yyv[yysp-0];
         
       break;
							case  150 : 
         yyval = yyv[yysp-0];
         
       break;
							case  151 : 
         yyval = yyv[yysp-1] + yyv[yysp-0];
         
       break;
							case  152 : 
         yyval = yyv[yysp-0];
         
       break;
							case  153 : 
         yyval = yyv[yysp-1] + yyv[yysp-0];
         
       break;
							case  154 : 
         yyval = yyv[yysp-0];
         
       break;
							case  155 : 
         yyval = Formatter.FormatIdentifier(yyv[yysp-0]);
         
       break;
							case  156 : 
         yyval = Formatter.FormatList(yyv[yysp-0]);
         
       break;
							case  157 : 
         yyval = Formatter.FormatSkill(yyv[yysp-0]);
         
       break;
							case  158 : 
         yyval = Formatter.FormatSkill(yyv[yysp-0]);
         
       break;
							case  159 : 
         yyval = Formatter.FormatSkill(yyv[yysp-0]);
         
       break;
							case  160 : 
         yyval = Formatter.FormatIdentifier(yyv[yysp-0]);
         
       break;
							case  161 : 
         yyval = Formatter.FormatIdentifier(yyv[yysp-0]);
         
       break;
							case  162 : 
         yyval = Formatter.FormatEvent(yyv[yysp-0]);
         
       break;
							case  163 : 
         yyval = Formatter.FormatGlobal(yyv[yysp-0]);
         
       break;
							case  164 : 
         yyval = Formatter.FormatIdentifier(yyv[yysp-0]);
         
       break;
							case  165 : 
         yyval = Formatter.FormatIdentifier(yyv[yysp-0]);
         
       break;
							case  166 : 
         yyval = Formatter.FormatIdentifier(yyv[yysp-0]);
         
       break;
							case  167 : 
         yyval = Formatter.FormatIdentifier(yyv[yysp-0]);
         
       break;
							case  168 : 
         yyval = Formatter.FormatEvent(yyv[yysp-0]);
         
       break;
							case  169 : 
         yyval = Formatter.FormatIdentifier(yyv[yysp-0]);
         
       break;
							case  170 : 
         yyval = Formatter.FormatIdentifier(yyv[yysp-0]);
         
       break;
							case  171 : 
         yyval = Formatter.FormatGlobal(yyv[yysp-0]);
         
       break;
							case  172 : 
         yyval = Formatter.FormatIdentifier(yyv[yysp-0]);
         
       break;
							case  173 : 
         yyval = Formatter.FormatIdentifier(yyv[yysp-0]);
         
       break;
							case  174 : 
         yyval = Formatter.FormatIdentifier(yyv[yysp-0]);
         
       break;
							case  175 : 
         yyval = Formatter.FormatGlobal(yyv[yysp-0]);
         
       break;
							case  176 : 
         yyval = Formatter.FormatSkill(yyv[yysp-0]);
         
       break;
							case  177 : 
         yyval = Formatter.FormatSkill(yyv[yysp-0]);
         
       break;
							case  178 : 
         yyval = Formatter.FormatSkill(yyv[yysp-0]);
         
       break;
							case  179 : 
         yyval = Formatter.FormatGlobal(yyv[yysp-0]);
         
       break;
							case  180 : 
         yyval = Formatter.FormatSkill(yyv[yysp-0]);
         
       break;
							case  181 : 
         yyval = Formatter.FormatGlobal(yyv[yysp-0]);
         
       break;
							case  182 : 
         yyval = Formatter.FormatNull();
         
       break;
							case  183 : 
         yyval = yyv[yysp-0];
         
       break;
							case  184 : 
         yyval = yyv[yysp-0];
         
       break;
							case  185 : 
         yyval = yyv[yysp-0];
         
       break;
							case  186 : 
         yyval = yyv[yysp-2] + yyv[yysp-0];
         
       break;
							case  187 : 
         yyval = yyv[yysp-2] + yyv[yysp-0];
         
       break;
							case  188 : 
         yyval = yyv[yysp-0];
         
       break;
							case  189 : 
         yyval = yyv[yysp-0];
         
       break;
							case  190 : 
         yyval = yyv[yysp-0];
         
       break;
							case  191 : 
         yyval = yyv[yysp-0];
         
       break;
							case  192 : 
         yyval = yyv[yysp-0];
         
       break;
							case  193 : 
         yyval = yyv[yysp-0]; //TODO: FormatIdentifier?
         
       break;
							case  194 : 
         yyval = Formatter.FormatFile(yyv[yysp-0]);
         
       break;
							case  195 : 
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

					int yynacts   = 2160;
					int yyngotos  = 592;
					int yynstates = 267;
					int yynrules  = 195;
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
					yya[yyac] = new YYARec(297,30);yyac++; 
					yya[yyac] = new YYARec(298,31);yyac++; 
					yya[yyac] = new YYARec(299,32);yyac++; 
					yya[yyac] = new YYARec(301,33);yyac++; 
					yya[yyac] = new YYARec(302,34);yyac++; 
					yya[yyac] = new YYARec(303,35);yyac++; 
					yya[yyac] = new YYARec(304,36);yyac++; 
					yya[yyac] = new YYARec(305,37);yyac++; 
					yya[yyac] = new YYARec(306,38);yyac++; 
					yya[yyac] = new YYARec(307,39);yyac++; 
					yya[yyac] = new YYARec(308,40);yyac++; 
					yya[yyac] = new YYARec(309,41);yyac++; 
					yya[yyac] = new YYARec(310,42);yyac++; 
					yya[yyac] = new YYARec(311,43);yyac++; 
					yya[yyac] = new YYARec(312,44);yyac++; 
					yya[yyac] = new YYARec(313,45);yyac++; 
					yya[yyac] = new YYARec(316,46);yyac++; 
					yya[yyac] = new YYARec(0,-4 );yyac++; 
					yya[yyac] = new YYARec(289,47);yyac++; 
					yya[yyac] = new YYARec(0,-125 );yyac++; 
					yya[yyac] = new YYARec(257,-125 );yyac++; 
					yya[yyac] = new YYARec(258,-125 );yyac++; 
					yya[yyac] = new YYARec(261,-125 );yyac++; 
					yya[yyac] = new YYARec(262,-125 );yyac++; 
					yya[yyac] = new YYARec(263,-125 );yyac++; 
					yya[yyac] = new YYARec(264,-125 );yyac++; 
					yya[yyac] = new YYARec(265,-125 );yyac++; 
					yya[yyac] = new YYARec(267,-125 );yyac++; 
					yya[yyac] = new YYARec(268,-125 );yyac++; 
					yya[yyac] = new YYARec(269,-125 );yyac++; 
					yya[yyac] = new YYARec(270,-125 );yyac++; 
					yya[yyac] = new YYARec(271,-125 );yyac++; 
					yya[yyac] = new YYARec(272,-125 );yyac++; 
					yya[yyac] = new YYARec(273,-125 );yyac++; 
					yya[yyac] = new YYARec(274,-125 );yyac++; 
					yya[yyac] = new YYARec(275,-125 );yyac++; 
					yya[yyac] = new YYARec(276,-125 );yyac++; 
					yya[yyac] = new YYARec(277,-125 );yyac++; 
					yya[yyac] = new YYARec(278,-125 );yyac++; 
					yya[yyac] = new YYARec(279,-125 );yyac++; 
					yya[yyac] = new YYARec(281,-125 );yyac++; 
					yya[yyac] = new YYARec(282,-125 );yyac++; 
					yya[yyac] = new YYARec(283,-125 );yyac++; 
					yya[yyac] = new YYARec(284,-125 );yyac++; 
					yya[yyac] = new YYARec(285,-125 );yyac++; 
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
					yya[yyac] = new YYARec(314,-125 );yyac++; 
					yya[yyac] = new YYARec(315,-125 );yyac++; 
					yya[yyac] = new YYARec(316,-125 );yyac++; 
					yya[yyac] = new YYARec(317,-125 );yyac++; 
					yya[yyac] = new YYARec(318,-125 );yyac++; 
					yya[yyac] = new YYARec(290,23);yyac++; 
					yya[yyac] = new YYARec(291,51);yyac++; 
					yya[yyac] = new YYARec(292,52);yyac++; 
					yya[yyac] = new YYARec(293,53);yyac++; 
					yya[yyac] = new YYARec(294,54);yyac++; 
					yya[yyac] = new YYARec(295,55);yyac++; 
					yya[yyac] = new YYARec(296,29);yyac++; 
					yya[yyac] = new YYARec(297,30);yyac++; 
					yya[yyac] = new YYARec(298,31);yyac++; 
					yya[yyac] = new YYARec(299,32);yyac++; 
					yya[yyac] = new YYARec(301,56);yyac++; 
					yya[yyac] = new YYARec(302,34);yyac++; 
					yya[yyac] = new YYARec(303,57);yyac++; 
					yya[yyac] = new YYARec(304,58);yyac++; 
					yya[yyac] = new YYARec(305,59);yyac++; 
					yya[yyac] = new YYARec(306,38);yyac++; 
					yya[yyac] = new YYARec(307,60);yyac++; 
					yya[yyac] = new YYARec(308,40);yyac++; 
					yya[yyac] = new YYARec(309,61);yyac++; 
					yya[yyac] = new YYARec(310,42);yyac++; 
					yya[yyac] = new YYARec(311,62);yyac++; 
					yya[yyac] = new YYARec(312,44);yyac++; 
					yya[yyac] = new YYARec(313,45);yyac++; 
					yya[yyac] = new YYARec(316,63);yyac++; 
					yya[yyac] = new YYARec(290,23);yyac++; 
					yya[yyac] = new YYARec(291,51);yyac++; 
					yya[yyac] = new YYARec(292,52);yyac++; 
					yya[yyac] = new YYARec(293,53);yyac++; 
					yya[yyac] = new YYARec(294,54);yyac++; 
					yya[yyac] = new YYARec(295,55);yyac++; 
					yya[yyac] = new YYARec(296,29);yyac++; 
					yya[yyac] = new YYARec(297,30);yyac++; 
					yya[yyac] = new YYARec(298,31);yyac++; 
					yya[yyac] = new YYARec(299,32);yyac++; 
					yya[yyac] = new YYARec(301,56);yyac++; 
					yya[yyac] = new YYARec(302,34);yyac++; 
					yya[yyac] = new YYARec(303,57);yyac++; 
					yya[yyac] = new YYARec(304,58);yyac++; 
					yya[yyac] = new YYARec(305,59);yyac++; 
					yya[yyac] = new YYARec(306,38);yyac++; 
					yya[yyac] = new YYARec(307,60);yyac++; 
					yya[yyac] = new YYARec(308,40);yyac++; 
					yya[yyac] = new YYARec(309,61);yyac++; 
					yya[yyac] = new YYARec(310,42);yyac++; 
					yya[yyac] = new YYARec(311,62);yyac++; 
					yya[yyac] = new YYARec(312,44);yyac++; 
					yya[yyac] = new YYARec(313,45);yyac++; 
					yya[yyac] = new YYARec(316,63);yyac++; 
					yya[yyac] = new YYARec(290,23);yyac++; 
					yya[yyac] = new YYARec(291,51);yyac++; 
					yya[yyac] = new YYARec(292,52);yyac++; 
					yya[yyac] = new YYARec(293,53);yyac++; 
					yya[yyac] = new YYARec(294,54);yyac++; 
					yya[yyac] = new YYARec(295,55);yyac++; 
					yya[yyac] = new YYARec(296,29);yyac++; 
					yya[yyac] = new YYARec(297,30);yyac++; 
					yya[yyac] = new YYARec(298,31);yyac++; 
					yya[yyac] = new YYARec(299,32);yyac++; 
					yya[yyac] = new YYARec(301,56);yyac++; 
					yya[yyac] = new YYARec(302,34);yyac++; 
					yya[yyac] = new YYARec(303,57);yyac++; 
					yya[yyac] = new YYARec(304,58);yyac++; 
					yya[yyac] = new YYARec(305,59);yyac++; 
					yya[yyac] = new YYARec(306,38);yyac++; 
					yya[yyac] = new YYARec(307,60);yyac++; 
					yya[yyac] = new YYARec(308,40);yyac++; 
					yya[yyac] = new YYARec(309,61);yyac++; 
					yya[yyac] = new YYARec(310,42);yyac++; 
					yya[yyac] = new YYARec(311,62);yyac++; 
					yya[yyac] = new YYARec(312,44);yyac++; 
					yya[yyac] = new YYARec(313,45);yyac++; 
					yya[yyac] = new YYARec(314,68);yyac++; 
					yya[yyac] = new YYARec(316,63);yyac++; 
					yya[yyac] = new YYARec(274,77);yyac++; 
					yya[yyac] = new YYARec(275,78);yyac++; 
					yya[yyac] = new YYARec(279,79);yyac++; 
					yya[yyac] = new YYARec(314,80);yyac++; 
					yya[yyac] = new YYARec(315,81);yyac++; 
					yya[yyac] = new YYARec(316,46);yyac++; 
					yya[yyac] = new YYARec(317,82);yyac++; 
					yya[yyac] = new YYARec(318,83);yyac++; 
					yya[yyac] = new YYARec(290,23);yyac++; 
					yya[yyac] = new YYARec(291,51);yyac++; 
					yya[yyac] = new YYARec(292,52);yyac++; 
					yya[yyac] = new YYARec(293,53);yyac++; 
					yya[yyac] = new YYARec(294,54);yyac++; 
					yya[yyac] = new YYARec(295,55);yyac++; 
					yya[yyac] = new YYARec(296,29);yyac++; 
					yya[yyac] = new YYARec(297,30);yyac++; 
					yya[yyac] = new YYARec(298,31);yyac++; 
					yya[yyac] = new YYARec(299,32);yyac++; 
					yya[yyac] = new YYARec(301,56);yyac++; 
					yya[yyac] = new YYARec(302,34);yyac++; 
					yya[yyac] = new YYARec(303,57);yyac++; 
					yya[yyac] = new YYARec(304,58);yyac++; 
					yya[yyac] = new YYARec(305,59);yyac++; 
					yya[yyac] = new YYARec(306,38);yyac++; 
					yya[yyac] = new YYARec(307,60);yyac++; 
					yya[yyac] = new YYARec(308,40);yyac++; 
					yya[yyac] = new YYARec(309,61);yyac++; 
					yya[yyac] = new YYARec(310,42);yyac++; 
					yya[yyac] = new YYARec(311,62);yyac++; 
					yya[yyac] = new YYARec(312,44);yyac++; 
					yya[yyac] = new YYARec(313,45);yyac++; 
					yya[yyac] = new YYARec(316,63);yyac++; 
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
					yya[yyac] = new YYARec(297,30);yyac++; 
					yya[yyac] = new YYARec(298,31);yyac++; 
					yya[yyac] = new YYARec(299,32);yyac++; 
					yya[yyac] = new YYARec(301,33);yyac++; 
					yya[yyac] = new YYARec(302,34);yyac++; 
					yya[yyac] = new YYARec(303,35);yyac++; 
					yya[yyac] = new YYARec(304,36);yyac++; 
					yya[yyac] = new YYARec(305,37);yyac++; 
					yya[yyac] = new YYARec(306,38);yyac++; 
					yya[yyac] = new YYARec(307,39);yyac++; 
					yya[yyac] = new YYARec(308,40);yyac++; 
					yya[yyac] = new YYARec(309,41);yyac++; 
					yya[yyac] = new YYARec(310,42);yyac++; 
					yya[yyac] = new YYARec(311,43);yyac++; 
					yya[yyac] = new YYARec(312,44);yyac++; 
					yya[yyac] = new YYARec(313,45);yyac++; 
					yya[yyac] = new YYARec(316,46);yyac++; 
					yya[yyac] = new YYARec(0,-4 );yyac++; 
					yya[yyac] = new YYARec(0,0);yyac++; 
					yya[yyac] = new YYARec(290,-19 );yyac++; 
					yya[yyac] = new YYARec(291,-19 );yyac++; 
					yya[yyac] = new YYARec(292,-19 );yyac++; 
					yya[yyac] = new YYARec(293,-19 );yyac++; 
					yya[yyac] = new YYARec(294,-19 );yyac++; 
					yya[yyac] = new YYARec(295,-19 );yyac++; 
					yya[yyac] = new YYARec(296,-19 );yyac++; 
					yya[yyac] = new YYARec(297,-19 );yyac++; 
					yya[yyac] = new YYARec(298,-19 );yyac++; 
					yya[yyac] = new YYARec(299,-19 );yyac++; 
					yya[yyac] = new YYARec(301,-19 );yyac++; 
					yya[yyac] = new YYARec(302,-19 );yyac++; 
					yya[yyac] = new YYARec(303,-19 );yyac++; 
					yya[yyac] = new YYARec(304,-19 );yyac++; 
					yya[yyac] = new YYARec(305,-19 );yyac++; 
					yya[yyac] = new YYARec(306,-19 );yyac++; 
					yya[yyac] = new YYARec(307,-19 );yyac++; 
					yya[yyac] = new YYARec(308,-19 );yyac++; 
					yya[yyac] = new YYARec(309,-19 );yyac++; 
					yya[yyac] = new YYARec(310,-19 );yyac++; 
					yya[yyac] = new YYARec(311,-19 );yyac++; 
					yya[yyac] = new YYARec(312,-19 );yyac++; 
					yya[yyac] = new YYARec(313,-19 );yyac++; 
					yya[yyac] = new YYARec(316,-19 );yyac++; 
					yya[yyac] = new YYARec(0,-168 );yyac++; 
					yya[yyac] = new YYARec(289,-168 );yyac++; 
					yya[yyac] = new YYARec(274,-16 );yyac++; 
					yya[yyac] = new YYARec(275,-16 );yyac++; 
					yya[yyac] = new YYARec(279,-16 );yyac++; 
					yya[yyac] = new YYARec(314,-16 );yyac++; 
					yya[yyac] = new YYARec(315,-16 );yyac++; 
					yya[yyac] = new YYARec(316,-16 );yyac++; 
					yya[yyac] = new YYARec(317,-16 );yyac++; 
					yya[yyac] = new YYARec(318,-16 );yyac++; 
					yya[yyac] = new YYARec(0,-171 );yyac++; 
					yya[yyac] = new YYARec(289,-171 );yyac++; 
					yya[yyac] = new YYARec(290,-30 );yyac++; 
					yya[yyac] = new YYARec(291,-30 );yyac++; 
					yya[yyac] = new YYARec(292,-30 );yyac++; 
					yya[yyac] = new YYARec(293,-30 );yyac++; 
					yya[yyac] = new YYARec(294,-30 );yyac++; 
					yya[yyac] = new YYARec(295,-30 );yyac++; 
					yya[yyac] = new YYARec(296,-30 );yyac++; 
					yya[yyac] = new YYARec(297,-30 );yyac++; 
					yya[yyac] = new YYARec(298,-30 );yyac++; 
					yya[yyac] = new YYARec(299,-30 );yyac++; 
					yya[yyac] = new YYARec(301,-30 );yyac++; 
					yya[yyac] = new YYARec(302,-30 );yyac++; 
					yya[yyac] = new YYARec(303,-30 );yyac++; 
					yya[yyac] = new YYARec(304,-30 );yyac++; 
					yya[yyac] = new YYARec(305,-30 );yyac++; 
					yya[yyac] = new YYARec(306,-30 );yyac++; 
					yya[yyac] = new YYARec(307,-30 );yyac++; 
					yya[yyac] = new YYARec(308,-30 );yyac++; 
					yya[yyac] = new YYARec(309,-30 );yyac++; 
					yya[yyac] = new YYARec(310,-30 );yyac++; 
					yya[yyac] = new YYARec(311,-30 );yyac++; 
					yya[yyac] = new YYARec(312,-30 );yyac++; 
					yya[yyac] = new YYARec(313,-30 );yyac++; 
					yya[yyac] = new YYARec(314,-30 );yyac++; 
					yya[yyac] = new YYARec(316,-30 );yyac++; 
					yya[yyac] = new YYARec(0,-166 );yyac++; 
					yya[yyac] = new YYARec(289,-166 );yyac++; 
					yya[yyac] = new YYARec(290,-34 );yyac++; 
					yya[yyac] = new YYARec(291,-34 );yyac++; 
					yya[yyac] = new YYARec(292,-34 );yyac++; 
					yya[yyac] = new YYARec(293,-34 );yyac++; 
					yya[yyac] = new YYARec(294,-34 );yyac++; 
					yya[yyac] = new YYARec(295,-34 );yyac++; 
					yya[yyac] = new YYARec(296,-34 );yyac++; 
					yya[yyac] = new YYARec(297,-34 );yyac++; 
					yya[yyac] = new YYARec(298,-34 );yyac++; 
					yya[yyac] = new YYARec(299,-34 );yyac++; 
					yya[yyac] = new YYARec(301,-34 );yyac++; 
					yya[yyac] = new YYARec(302,-34 );yyac++; 
					yya[yyac] = new YYARec(303,-34 );yyac++; 
					yya[yyac] = new YYARec(304,-34 );yyac++; 
					yya[yyac] = new YYARec(305,-34 );yyac++; 
					yya[yyac] = new YYARec(306,-34 );yyac++; 
					yya[yyac] = new YYARec(307,-34 );yyac++; 
					yya[yyac] = new YYARec(308,-34 );yyac++; 
					yya[yyac] = new YYARec(309,-34 );yyac++; 
					yya[yyac] = new YYARec(310,-34 );yyac++; 
					yya[yyac] = new YYARec(311,-34 );yyac++; 
					yya[yyac] = new YYARec(312,-34 );yyac++; 
					yya[yyac] = new YYARec(313,-34 );yyac++; 
					yya[yyac] = new YYARec(316,-34 );yyac++; 
					yya[yyac] = new YYARec(0,-173 );yyac++; 
					yya[yyac] = new YYARec(289,-173 );yyac++; 
					yya[yyac] = new YYARec(290,-48 );yyac++; 
					yya[yyac] = new YYARec(291,-48 );yyac++; 
					yya[yyac] = new YYARec(292,-48 );yyac++; 
					yya[yyac] = new YYARec(293,-48 );yyac++; 
					yya[yyac] = new YYARec(294,-48 );yyac++; 
					yya[yyac] = new YYARec(295,-48 );yyac++; 
					yya[yyac] = new YYARec(296,-48 );yyac++; 
					yya[yyac] = new YYARec(297,-48 );yyac++; 
					yya[yyac] = new YYARec(298,-48 );yyac++; 
					yya[yyac] = new YYARec(299,-48 );yyac++; 
					yya[yyac] = new YYARec(301,-48 );yyac++; 
					yya[yyac] = new YYARec(302,-48 );yyac++; 
					yya[yyac] = new YYARec(303,-48 );yyac++; 
					yya[yyac] = new YYARec(304,-48 );yyac++; 
					yya[yyac] = new YYARec(305,-48 );yyac++; 
					yya[yyac] = new YYARec(306,-48 );yyac++; 
					yya[yyac] = new YYARec(307,-48 );yyac++; 
					yya[yyac] = new YYARec(308,-48 );yyac++; 
					yya[yyac] = new YYARec(309,-48 );yyac++; 
					yya[yyac] = new YYARec(310,-48 );yyac++; 
					yya[yyac] = new YYARec(311,-48 );yyac++; 
					yya[yyac] = new YYARec(312,-48 );yyac++; 
					yya[yyac] = new YYARec(313,-48 );yyac++; 
					yya[yyac] = new YYARec(316,-48 );yyac++; 
					yya[yyac] = new YYARec(0,-170 );yyac++; 
					yya[yyac] = new YYARec(289,-170 );yyac++; 
					yya[yyac] = new YYARec(274,-159 );yyac++; 
					yya[yyac] = new YYARec(275,-159 );yyac++; 
					yya[yyac] = new YYARec(279,-159 );yyac++; 
					yya[yyac] = new YYARec(314,-159 );yyac++; 
					yya[yyac] = new YYARec(315,-159 );yyac++; 
					yya[yyac] = new YYARec(316,-159 );yyac++; 
					yya[yyac] = new YYARec(317,-159 );yyac++; 
					yya[yyac] = new YYARec(318,-159 );yyac++; 
					yya[yyac] = new YYARec(0,-180 );yyac++; 
					yya[yyac] = new YYARec(289,-180 );yyac++; 
					yya[yyac] = new YYARec(274,-14 );yyac++; 
					yya[yyac] = new YYARec(275,-14 );yyac++; 
					yya[yyac] = new YYARec(279,-14 );yyac++; 
					yya[yyac] = new YYARec(314,-14 );yyac++; 
					yya[yyac] = new YYARec(315,-14 );yyac++; 
					yya[yyac] = new YYARec(316,-14 );yyac++; 
					yya[yyac] = new YYARec(317,-14 );yyac++; 
					yya[yyac] = new YYARec(318,-14 );yyac++; 
					yya[yyac] = new YYARec(0,-163 );yyac++; 
					yya[yyac] = new YYARec(289,-163 );yyac++; 
					yya[yyac] = new YYARec(290,-18 );yyac++; 
					yya[yyac] = new YYARec(291,-18 );yyac++; 
					yya[yyac] = new YYARec(292,-18 );yyac++; 
					yya[yyac] = new YYARec(293,-18 );yyac++; 
					yya[yyac] = new YYARec(294,-18 );yyac++; 
					yya[yyac] = new YYARec(295,-18 );yyac++; 
					yya[yyac] = new YYARec(296,-18 );yyac++; 
					yya[yyac] = new YYARec(297,-18 );yyac++; 
					yya[yyac] = new YYARec(298,-18 );yyac++; 
					yya[yyac] = new YYARec(299,-18 );yyac++; 
					yya[yyac] = new YYARec(301,-18 );yyac++; 
					yya[yyac] = new YYARec(302,-18 );yyac++; 
					yya[yyac] = new YYARec(303,-18 );yyac++; 
					yya[yyac] = new YYARec(304,-18 );yyac++; 
					yya[yyac] = new YYARec(305,-18 );yyac++; 
					yya[yyac] = new YYARec(306,-18 );yyac++; 
					yya[yyac] = new YYARec(307,-18 );yyac++; 
					yya[yyac] = new YYARec(308,-18 );yyac++; 
					yya[yyac] = new YYARec(309,-18 );yyac++; 
					yya[yyac] = new YYARec(310,-18 );yyac++; 
					yya[yyac] = new YYARec(311,-18 );yyac++; 
					yya[yyac] = new YYARec(312,-18 );yyac++; 
					yya[yyac] = new YYARec(313,-18 );yyac++; 
					yya[yyac] = new YYARec(316,-18 );yyac++; 
					yya[yyac] = new YYARec(0,-162 );yyac++; 
					yya[yyac] = new YYARec(289,-162 );yyac++; 
					yya[yyac] = new YYARec(290,-35 );yyac++; 
					yya[yyac] = new YYARec(291,-35 );yyac++; 
					yya[yyac] = new YYARec(292,-35 );yyac++; 
					yya[yyac] = new YYARec(293,-35 );yyac++; 
					yya[yyac] = new YYARec(294,-35 );yyac++; 
					yya[yyac] = new YYARec(295,-35 );yyac++; 
					yya[yyac] = new YYARec(296,-35 );yyac++; 
					yya[yyac] = new YYARec(297,-35 );yyac++; 
					yya[yyac] = new YYARec(298,-35 );yyac++; 
					yya[yyac] = new YYARec(299,-35 );yyac++; 
					yya[yyac] = new YYARec(301,-35 );yyac++; 
					yya[yyac] = new YYARec(302,-35 );yyac++; 
					yya[yyac] = new YYARec(303,-35 );yyac++; 
					yya[yyac] = new YYARec(304,-35 );yyac++; 
					yya[yyac] = new YYARec(305,-35 );yyac++; 
					yya[yyac] = new YYARec(306,-35 );yyac++; 
					yya[yyac] = new YYARec(307,-35 );yyac++; 
					yya[yyac] = new YYARec(308,-35 );yyac++; 
					yya[yyac] = new YYARec(309,-35 );yyac++; 
					yya[yyac] = new YYARec(310,-35 );yyac++; 
					yya[yyac] = new YYARec(311,-35 );yyac++; 
					yya[yyac] = new YYARec(312,-35 );yyac++; 
					yya[yyac] = new YYARec(313,-35 );yyac++; 
					yya[yyac] = new YYARec(316,-35 );yyac++; 
					yya[yyac] = new YYARec(0,-165 );yyac++; 
					yya[yyac] = new YYARec(289,-165 );yyac++; 
					yya[yyac] = new YYARec(274,-157 );yyac++; 
					yya[yyac] = new YYARec(275,-157 );yyac++; 
					yya[yyac] = new YYARec(279,-157 );yyac++; 
					yya[yyac] = new YYARec(314,-157 );yyac++; 
					yya[yyac] = new YYARec(315,-157 );yyac++; 
					yya[yyac] = new YYARec(316,-157 );yyac++; 
					yya[yyac] = new YYARec(317,-157 );yyac++; 
					yya[yyac] = new YYARec(318,-157 );yyac++; 
					yya[yyac] = new YYARec(0,-176 );yyac++; 
					yya[yyac] = new YYARec(289,-176 );yyac++; 
					yya[yyac] = new YYARec(274,-158 );yyac++; 
					yya[yyac] = new YYARec(275,-158 );yyac++; 
					yya[yyac] = new YYARec(279,-158 );yyac++; 
					yya[yyac] = new YYARec(314,-158 );yyac++; 
					yya[yyac] = new YYARec(315,-158 );yyac++; 
					yya[yyac] = new YYARec(316,-158 );yyac++; 
					yya[yyac] = new YYARec(317,-158 );yyac++; 
					yya[yyac] = new YYARec(318,-158 );yyac++; 
					yya[yyac] = new YYARec(0,-178 );yyac++; 
					yya[yyac] = new YYARec(289,-178 );yyac++; 
					yya[yyac] = new YYARec(274,-15 );yyac++; 
					yya[yyac] = new YYARec(275,-15 );yyac++; 
					yya[yyac] = new YYARec(279,-15 );yyac++; 
					yya[yyac] = new YYARec(314,-15 );yyac++; 
					yya[yyac] = new YYARec(315,-15 );yyac++; 
					yya[yyac] = new YYARec(316,-15 );yyac++; 
					yya[yyac] = new YYARec(317,-15 );yyac++; 
					yya[yyac] = new YYARec(318,-15 );yyac++; 
					yya[yyac] = new YYARec(0,-175 );yyac++; 
					yya[yyac] = new YYARec(289,-175 );yyac++; 
					yya[yyac] = new YYARec(293,91);yyac++; 
					yya[yyac] = new YYARec(294,92);yyac++; 
					yya[yyac] = new YYARec(297,93);yyac++; 
					yya[yyac] = new YYARec(298,94);yyac++; 
					yya[yyac] = new YYARec(303,95);yyac++; 
					yya[yyac] = new YYARec(304,96);yyac++; 
					yya[yyac] = new YYARec(305,97);yyac++; 
					yya[yyac] = new YYARec(307,98);yyac++; 
					yya[yyac] = new YYARec(308,99);yyac++; 
					yya[yyac] = new YYARec(309,100);yyac++; 
					yya[yyac] = new YYARec(310,101);yyac++; 
					yya[yyac] = new YYARec(311,102);yyac++; 
					yya[yyac] = new YYARec(312,103);yyac++; 
					yya[yyac] = new YYARec(313,104);yyac++; 
					yya[yyac] = new YYARec(258,105);yyac++; 
					yya[yyac] = new YYARec(275,106);yyac++; 
					yya[yyac] = new YYARec(257,-155 );yyac++; 
					yya[yyac] = new YYARec(258,-155 );yyac++; 
					yya[yyac] = new YYARec(290,-155 );yyac++; 
					yya[yyac] = new YYARec(291,-155 );yyac++; 
					yya[yyac] = new YYARec(292,-155 );yyac++; 
					yya[yyac] = new YYARec(293,-155 );yyac++; 
					yya[yyac] = new YYARec(294,-155 );yyac++; 
					yya[yyac] = new YYARec(295,-155 );yyac++; 
					yya[yyac] = new YYARec(296,-155 );yyac++; 
					yya[yyac] = new YYARec(297,-155 );yyac++; 
					yya[yyac] = new YYARec(298,-155 );yyac++; 
					yya[yyac] = new YYARec(299,-155 );yyac++; 
					yya[yyac] = new YYARec(301,-155 );yyac++; 
					yya[yyac] = new YYARec(302,-155 );yyac++; 
					yya[yyac] = new YYARec(303,-155 );yyac++; 
					yya[yyac] = new YYARec(304,-155 );yyac++; 
					yya[yyac] = new YYARec(305,-155 );yyac++; 
					yya[yyac] = new YYARec(306,-155 );yyac++; 
					yya[yyac] = new YYARec(307,-155 );yyac++; 
					yya[yyac] = new YYARec(308,-155 );yyac++; 
					yya[yyac] = new YYARec(309,-155 );yyac++; 
					yya[yyac] = new YYARec(310,-155 );yyac++; 
					yya[yyac] = new YYARec(311,-155 );yyac++; 
					yya[yyac] = new YYARec(312,-155 );yyac++; 
					yya[yyac] = new YYARec(313,-155 );yyac++; 
					yya[yyac] = new YYARec(316,-155 );yyac++; 
					yya[yyac] = new YYARec(317,-155 );yyac++; 
					yya[yyac] = new YYARec(318,-155 );yyac++; 
					yya[yyac] = new YYARec(257,108);yyac++; 
					yya[yyac] = new YYARec(258,109);yyac++; 
					yya[yyac] = new YYARec(318,83);yyac++; 
					yya[yyac] = new YYARec(317,82);yyac++; 
					yya[yyac] = new YYARec(314,111);yyac++; 
					yya[yyac] = new YYARec(315,112);yyac++; 
					yya[yyac] = new YYARec(257,113);yyac++; 
					yya[yyac] = new YYARec(290,23);yyac++; 
					yya[yyac] = new YYARec(291,51);yyac++; 
					yya[yyac] = new YYARec(292,52);yyac++; 
					yya[yyac] = new YYARec(293,53);yyac++; 
					yya[yyac] = new YYARec(294,54);yyac++; 
					yya[yyac] = new YYARec(295,55);yyac++; 
					yya[yyac] = new YYARec(296,29);yyac++; 
					yya[yyac] = new YYARec(297,30);yyac++; 
					yya[yyac] = new YYARec(298,31);yyac++; 
					yya[yyac] = new YYARec(299,32);yyac++; 
					yya[yyac] = new YYARec(301,56);yyac++; 
					yya[yyac] = new YYARec(302,34);yyac++; 
					yya[yyac] = new YYARec(303,57);yyac++; 
					yya[yyac] = new YYARec(304,58);yyac++; 
					yya[yyac] = new YYARec(305,59);yyac++; 
					yya[yyac] = new YYARec(306,38);yyac++; 
					yya[yyac] = new YYARec(307,60);yyac++; 
					yya[yyac] = new YYARec(308,40);yyac++; 
					yya[yyac] = new YYARec(309,61);yyac++; 
					yya[yyac] = new YYARec(310,42);yyac++; 
					yya[yyac] = new YYARec(311,62);yyac++; 
					yya[yyac] = new YYARec(312,44);yyac++; 
					yya[yyac] = new YYARec(313,45);yyac++; 
					yya[yyac] = new YYARec(316,63);yyac++; 
					yya[yyac] = new YYARec(257,-20 );yyac++; 
					yya[yyac] = new YYARec(257,115);yyac++; 
					yya[yyac] = new YYARec(257,125);yyac++; 
					yya[yyac] = new YYARec(258,126);yyac++; 
					yya[yyac] = new YYARec(280,127);yyac++; 
					yya[yyac] = new YYARec(286,128);yyac++; 
					yya[yyac] = new YYARec(287,129);yyac++; 
					yya[yyac] = new YYARec(288,130);yyac++; 
					yya[yyac] = new YYARec(290,23);yyac++; 
					yya[yyac] = new YYARec(291,51);yyac++; 
					yya[yyac] = new YYARec(292,52);yyac++; 
					yya[yyac] = new YYARec(293,53);yyac++; 
					yya[yyac] = new YYARec(294,54);yyac++; 
					yya[yyac] = new YYARec(295,55);yyac++; 
					yya[yyac] = new YYARec(296,29);yyac++; 
					yya[yyac] = new YYARec(297,30);yyac++; 
					yya[yyac] = new YYARec(298,31);yyac++; 
					yya[yyac] = new YYARec(299,131);yyac++; 
					yya[yyac] = new YYARec(301,56);yyac++; 
					yya[yyac] = new YYARec(302,34);yyac++; 
					yya[yyac] = new YYARec(303,57);yyac++; 
					yya[yyac] = new YYARec(304,58);yyac++; 
					yya[yyac] = new YYARec(305,59);yyac++; 
					yya[yyac] = new YYARec(306,132);yyac++; 
					yya[yyac] = new YYARec(307,60);yyac++; 
					yya[yyac] = new YYARec(308,40);yyac++; 
					yya[yyac] = new YYARec(309,61);yyac++; 
					yya[yyac] = new YYARec(310,133);yyac++; 
					yya[yyac] = new YYARec(311,62);yyac++; 
					yya[yyac] = new YYARec(312,134);yyac++; 
					yya[yyac] = new YYARec(313,45);yyac++; 
					yya[yyac] = new YYARec(316,135);yyac++; 
					yya[yyac] = new YYARec(259,-54 );yyac++; 
					yya[yyac] = new YYARec(314,136);yyac++; 
					yya[yyac] = new YYARec(316,137);yyac++; 
					yya[yyac] = new YYARec(257,138);yyac++; 
					yya[yyac] = new YYARec(257,142);yyac++; 
					yya[yyac] = new YYARec(293,91);yyac++; 
					yya[yyac] = new YYARec(294,92);yyac++; 
					yya[yyac] = new YYARec(298,94);yyac++; 
					yya[yyac] = new YYARec(303,95);yyac++; 
					yya[yyac] = new YYARec(304,96);yyac++; 
					yya[yyac] = new YYARec(307,98);yyac++; 
					yya[yyac] = new YYARec(309,100);yyac++; 
					yya[yyac] = new YYARec(311,102);yyac++; 
					yya[yyac] = new YYARec(312,103);yyac++; 
					yya[yyac] = new YYARec(259,-38 );yyac++; 
					yya[yyac] = new YYARec(274,77);yyac++; 
					yya[yyac] = new YYARec(275,78);yyac++; 
					yya[yyac] = new YYARec(279,79);yyac++; 
					yya[yyac] = new YYARec(314,80);yyac++; 
					yya[yyac] = new YYARec(257,-28 );yyac++; 
					yya[yyac] = new YYARec(289,47);yyac++; 
					yya[yyac] = new YYARec(281,-125 );yyac++; 
					yya[yyac] = new YYARec(282,-125 );yyac++; 
					yya[yyac] = new YYARec(283,-125 );yyac++; 
					yya[yyac] = new YYARec(284,-125 );yyac++; 
					yya[yyac] = new YYARec(285,-125 );yyac++; 
					yya[yyac] = new YYARec(260,-193 );yyac++; 
					yya[yyac] = new YYARec(257,147);yyac++; 
					yya[yyac] = new YYARec(257,157);yyac++; 
					yya[yyac] = new YYARec(274,77);yyac++; 
					yya[yyac] = new YYARec(275,78);yyac++; 
					yya[yyac] = new YYARec(279,79);yyac++; 
					yya[yyac] = new YYARec(290,23);yyac++; 
					yya[yyac] = new YYARec(291,51);yyac++; 
					yya[yyac] = new YYARec(292,52);yyac++; 
					yya[yyac] = new YYARec(293,53);yyac++; 
					yya[yyac] = new YYARec(294,54);yyac++; 
					yya[yyac] = new YYARec(295,55);yyac++; 
					yya[yyac] = new YYARec(296,29);yyac++; 
					yya[yyac] = new YYARec(297,30);yyac++; 
					yya[yyac] = new YYARec(298,31);yyac++; 
					yya[yyac] = new YYARec(299,32);yyac++; 
					yya[yyac] = new YYARec(300,158);yyac++; 
					yya[yyac] = new YYARec(301,56);yyac++; 
					yya[yyac] = new YYARec(302,34);yyac++; 
					yya[yyac] = new YYARec(303,57);yyac++; 
					yya[yyac] = new YYARec(304,58);yyac++; 
					yya[yyac] = new YYARec(305,59);yyac++; 
					yya[yyac] = new YYARec(306,38);yyac++; 
					yya[yyac] = new YYARec(307,60);yyac++; 
					yya[yyac] = new YYARec(308,40);yyac++; 
					yya[yyac] = new YYARec(309,61);yyac++; 
					yya[yyac] = new YYARec(310,42);yyac++; 
					yya[yyac] = new YYARec(311,62);yyac++; 
					yya[yyac] = new YYARec(312,44);yyac++; 
					yya[yyac] = new YYARec(313,45);yyac++; 
					yya[yyac] = new YYARec(314,80);yyac++; 
					yya[yyac] = new YYARec(315,81);yyac++; 
					yya[yyac] = new YYARec(316,46);yyac++; 
					yya[yyac] = new YYARec(317,82);yyac++; 
					yya[yyac] = new YYARec(318,83);yyac++; 
					yya[yyac] = new YYARec(257,125);yyac++; 
					yya[yyac] = new YYARec(258,126);yyac++; 
					yya[yyac] = new YYARec(280,127);yyac++; 
					yya[yyac] = new YYARec(286,128);yyac++; 
					yya[yyac] = new YYARec(287,129);yyac++; 
					yya[yyac] = new YYARec(288,130);yyac++; 
					yya[yyac] = new YYARec(290,23);yyac++; 
					yya[yyac] = new YYARec(291,51);yyac++; 
					yya[yyac] = new YYARec(292,52);yyac++; 
					yya[yyac] = new YYARec(293,53);yyac++; 
					yya[yyac] = new YYARec(294,54);yyac++; 
					yya[yyac] = new YYARec(295,55);yyac++; 
					yya[yyac] = new YYARec(296,29);yyac++; 
					yya[yyac] = new YYARec(297,30);yyac++; 
					yya[yyac] = new YYARec(298,31);yyac++; 
					yya[yyac] = new YYARec(299,131);yyac++; 
					yya[yyac] = new YYARec(301,56);yyac++; 
					yya[yyac] = new YYARec(302,34);yyac++; 
					yya[yyac] = new YYARec(303,57);yyac++; 
					yya[yyac] = new YYARec(304,58);yyac++; 
					yya[yyac] = new YYARec(305,59);yyac++; 
					yya[yyac] = new YYARec(306,132);yyac++; 
					yya[yyac] = new YYARec(307,60);yyac++; 
					yya[yyac] = new YYARec(308,40);yyac++; 
					yya[yyac] = new YYARec(309,61);yyac++; 
					yya[yyac] = new YYARec(310,133);yyac++; 
					yya[yyac] = new YYARec(311,62);yyac++; 
					yya[yyac] = new YYARec(312,134);yyac++; 
					yya[yyac] = new YYARec(313,45);yyac++; 
					yya[yyac] = new YYARec(316,135);yyac++; 
					yya[yyac] = new YYARec(259,-54 );yyac++; 
					yya[yyac] = new YYARec(257,125);yyac++; 
					yya[yyac] = new YYARec(258,126);yyac++; 
					yya[yyac] = new YYARec(280,127);yyac++; 
					yya[yyac] = new YYARec(286,128);yyac++; 
					yya[yyac] = new YYARec(287,129);yyac++; 
					yya[yyac] = new YYARec(288,130);yyac++; 
					yya[yyac] = new YYARec(290,23);yyac++; 
					yya[yyac] = new YYARec(291,51);yyac++; 
					yya[yyac] = new YYARec(292,52);yyac++; 
					yya[yyac] = new YYARec(293,53);yyac++; 
					yya[yyac] = new YYARec(294,54);yyac++; 
					yya[yyac] = new YYARec(295,55);yyac++; 
					yya[yyac] = new YYARec(296,29);yyac++; 
					yya[yyac] = new YYARec(297,30);yyac++; 
					yya[yyac] = new YYARec(298,31);yyac++; 
					yya[yyac] = new YYARec(299,131);yyac++; 
					yya[yyac] = new YYARec(301,56);yyac++; 
					yya[yyac] = new YYARec(302,34);yyac++; 
					yya[yyac] = new YYARec(303,57);yyac++; 
					yya[yyac] = new YYARec(304,58);yyac++; 
					yya[yyac] = new YYARec(305,59);yyac++; 
					yya[yyac] = new YYARec(306,132);yyac++; 
					yya[yyac] = new YYARec(307,60);yyac++; 
					yya[yyac] = new YYARec(308,40);yyac++; 
					yya[yyac] = new YYARec(309,61);yyac++; 
					yya[yyac] = new YYARec(310,133);yyac++; 
					yya[yyac] = new YYARec(311,62);yyac++; 
					yya[yyac] = new YYARec(312,134);yyac++; 
					yya[yyac] = new YYARec(313,45);yyac++; 
					yya[yyac] = new YYARec(316,135);yyac++; 
					yya[yyac] = new YYARec(259,-54 );yyac++; 
					yya[yyac] = new YYARec(260,161);yyac++; 
					yya[yyac] = new YYARec(259,162);yyac++; 
					yya[yyac] = new YYARec(274,77);yyac++; 
					yya[yyac] = new YYARec(275,78);yyac++; 
					yya[yyac] = new YYARec(279,79);yyac++; 
					yya[yyac] = new YYARec(290,23);yyac++; 
					yya[yyac] = new YYARec(291,51);yyac++; 
					yya[yyac] = new YYARec(292,52);yyac++; 
					yya[yyac] = new YYARec(293,53);yyac++; 
					yya[yyac] = new YYARec(294,54);yyac++; 
					yya[yyac] = new YYARec(295,55);yyac++; 
					yya[yyac] = new YYARec(296,29);yyac++; 
					yya[yyac] = new YYARec(297,30);yyac++; 
					yya[yyac] = new YYARec(298,31);yyac++; 
					yya[yyac] = new YYARec(299,32);yyac++; 
					yya[yyac] = new YYARec(300,158);yyac++; 
					yya[yyac] = new YYARec(301,56);yyac++; 
					yya[yyac] = new YYARec(302,34);yyac++; 
					yya[yyac] = new YYARec(303,57);yyac++; 
					yya[yyac] = new YYARec(304,58);yyac++; 
					yya[yyac] = new YYARec(305,59);yyac++; 
					yya[yyac] = new YYARec(306,38);yyac++; 
					yya[yyac] = new YYARec(307,60);yyac++; 
					yya[yyac] = new YYARec(308,40);yyac++; 
					yya[yyac] = new YYARec(309,61);yyac++; 
					yya[yyac] = new YYARec(310,42);yyac++; 
					yya[yyac] = new YYARec(311,62);yyac++; 
					yya[yyac] = new YYARec(312,44);yyac++; 
					yya[yyac] = new YYARec(313,45);yyac++; 
					yya[yyac] = new YYARec(314,80);yyac++; 
					yya[yyac] = new YYARec(315,81);yyac++; 
					yya[yyac] = new YYARec(316,46);yyac++; 
					yya[yyac] = new YYARec(317,82);yyac++; 
					yya[yyac] = new YYARec(318,83);yyac++; 
					yya[yyac] = new YYARec(260,-183 );yyac++; 
					yya[yyac] = new YYARec(281,-183 );yyac++; 
					yya[yyac] = new YYARec(282,-183 );yyac++; 
					yya[yyac] = new YYARec(283,-183 );yyac++; 
					yya[yyac] = new YYARec(284,-183 );yyac++; 
					yya[yyac] = new YYARec(285,-183 );yyac++; 
					yya[yyac] = new YYARec(289,-183 );yyac++; 
					yya[yyac] = new YYARec(281,165);yyac++; 
					yya[yyac] = new YYARec(282,166);yyac++; 
					yya[yyac] = new YYARec(283,167);yyac++; 
					yya[yyac] = new YYARec(284,168);yyac++; 
					yya[yyac] = new YYARec(285,169);yyac++; 
					yya[yyac] = new YYARec(257,125);yyac++; 
					yya[yyac] = new YYARec(258,126);yyac++; 
					yya[yyac] = new YYARec(280,127);yyac++; 
					yya[yyac] = new YYARec(286,128);yyac++; 
					yya[yyac] = new YYARec(287,129);yyac++; 
					yya[yyac] = new YYARec(288,130);yyac++; 
					yya[yyac] = new YYARec(290,23);yyac++; 
					yya[yyac] = new YYARec(291,51);yyac++; 
					yya[yyac] = new YYARec(292,52);yyac++; 
					yya[yyac] = new YYARec(293,53);yyac++; 
					yya[yyac] = new YYARec(294,54);yyac++; 
					yya[yyac] = new YYARec(295,55);yyac++; 
					yya[yyac] = new YYARec(296,29);yyac++; 
					yya[yyac] = new YYARec(297,30);yyac++; 
					yya[yyac] = new YYARec(298,31);yyac++; 
					yya[yyac] = new YYARec(299,131);yyac++; 
					yya[yyac] = new YYARec(301,56);yyac++; 
					yya[yyac] = new YYARec(302,34);yyac++; 
					yya[yyac] = new YYARec(303,57);yyac++; 
					yya[yyac] = new YYARec(304,58);yyac++; 
					yya[yyac] = new YYARec(305,59);yyac++; 
					yya[yyac] = new YYARec(306,132);yyac++; 
					yya[yyac] = new YYARec(307,60);yyac++; 
					yya[yyac] = new YYARec(308,40);yyac++; 
					yya[yyac] = new YYARec(309,61);yyac++; 
					yya[yyac] = new YYARec(310,133);yyac++; 
					yya[yyac] = new YYARec(311,62);yyac++; 
					yya[yyac] = new YYARec(312,134);yyac++; 
					yya[yyac] = new YYARec(313,45);yyac++; 
					yya[yyac] = new YYARec(316,135);yyac++; 
					yya[yyac] = new YYARec(259,-54 );yyac++; 
					yya[yyac] = new YYARec(266,188);yyac++; 
					yya[yyac] = new YYARec(274,77);yyac++; 
					yya[yyac] = new YYARec(275,78);yyac++; 
					yya[yyac] = new YYARec(279,79);yyac++; 
					yya[yyac] = new YYARec(290,23);yyac++; 
					yya[yyac] = new YYARec(291,51);yyac++; 
					yya[yyac] = new YYARec(292,52);yyac++; 
					yya[yyac] = new YYARec(293,53);yyac++; 
					yya[yyac] = new YYARec(294,54);yyac++; 
					yya[yyac] = new YYARec(295,55);yyac++; 
					yya[yyac] = new YYARec(296,189);yyac++; 
					yya[yyac] = new YYARec(297,30);yyac++; 
					yya[yyac] = new YYARec(298,31);yyac++; 
					yya[yyac] = new YYARec(299,32);yyac++; 
					yya[yyac] = new YYARec(300,158);yyac++; 
					yya[yyac] = new YYARec(301,56);yyac++; 
					yya[yyac] = new YYARec(302,34);yyac++; 
					yya[yyac] = new YYARec(303,57);yyac++; 
					yya[yyac] = new YYARec(304,58);yyac++; 
					yya[yyac] = new YYARec(305,59);yyac++; 
					yya[yyac] = new YYARec(306,190);yyac++; 
					yya[yyac] = new YYARec(307,191);yyac++; 
					yya[yyac] = new YYARec(308,40);yyac++; 
					yya[yyac] = new YYARec(309,61);yyac++; 
					yya[yyac] = new YYARec(310,42);yyac++; 
					yya[yyac] = new YYARec(311,62);yyac++; 
					yya[yyac] = new YYARec(312,44);yyac++; 
					yya[yyac] = new YYARec(313,45);yyac++; 
					yya[yyac] = new YYARec(314,192);yyac++; 
					yya[yyac] = new YYARec(315,193);yyac++; 
					yya[yyac] = new YYARec(316,46);yyac++; 
					yya[yyac] = new YYARec(258,194);yyac++; 
					yya[yyac] = new YYARec(257,-142 );yyac++; 
					yya[yyac] = new YYARec(274,-142 );yyac++; 
					yya[yyac] = new YYARec(275,-142 );yyac++; 
					yya[yyac] = new YYARec(279,-142 );yyac++; 
					yya[yyac] = new YYARec(290,-142 );yyac++; 
					yya[yyac] = new YYARec(291,-142 );yyac++; 
					yya[yyac] = new YYARec(292,-142 );yyac++; 
					yya[yyac] = new YYARec(293,-142 );yyac++; 
					yya[yyac] = new YYARec(294,-142 );yyac++; 
					yya[yyac] = new YYARec(295,-142 );yyac++; 
					yya[yyac] = new YYARec(296,-142 );yyac++; 
					yya[yyac] = new YYARec(297,-142 );yyac++; 
					yya[yyac] = new YYARec(298,-142 );yyac++; 
					yya[yyac] = new YYARec(299,-142 );yyac++; 
					yya[yyac] = new YYARec(300,-142 );yyac++; 
					yya[yyac] = new YYARec(301,-142 );yyac++; 
					yya[yyac] = new YYARec(302,-142 );yyac++; 
					yya[yyac] = new YYARec(303,-142 );yyac++; 
					yya[yyac] = new YYARec(304,-142 );yyac++; 
					yya[yyac] = new YYARec(305,-142 );yyac++; 
					yya[yyac] = new YYARec(306,-142 );yyac++; 
					yya[yyac] = new YYARec(307,-142 );yyac++; 
					yya[yyac] = new YYARec(308,-142 );yyac++; 
					yya[yyac] = new YYARec(309,-142 );yyac++; 
					yya[yyac] = new YYARec(310,-142 );yyac++; 
					yya[yyac] = new YYARec(311,-142 );yyac++; 
					yya[yyac] = new YYARec(312,-142 );yyac++; 
					yya[yyac] = new YYARec(313,-142 );yyac++; 
					yya[yyac] = new YYARec(314,-142 );yyac++; 
					yya[yyac] = new YYARec(315,-142 );yyac++; 
					yya[yyac] = new YYARec(316,-142 );yyac++; 
					yya[yyac] = new YYARec(317,-142 );yyac++; 
					yya[yyac] = new YYARec(318,-142 );yyac++; 
					yya[yyac] = new YYARec(266,188);yyac++; 
					yya[yyac] = new YYARec(274,77);yyac++; 
					yya[yyac] = new YYARec(275,78);yyac++; 
					yya[yyac] = new YYARec(279,79);yyac++; 
					yya[yyac] = new YYARec(290,23);yyac++; 
					yya[yyac] = new YYARec(291,51);yyac++; 
					yya[yyac] = new YYARec(292,52);yyac++; 
					yya[yyac] = new YYARec(293,53);yyac++; 
					yya[yyac] = new YYARec(294,54);yyac++; 
					yya[yyac] = new YYARec(295,55);yyac++; 
					yya[yyac] = new YYARec(296,189);yyac++; 
					yya[yyac] = new YYARec(297,30);yyac++; 
					yya[yyac] = new YYARec(298,31);yyac++; 
					yya[yyac] = new YYARec(299,32);yyac++; 
					yya[yyac] = new YYARec(300,158);yyac++; 
					yya[yyac] = new YYARec(301,56);yyac++; 
					yya[yyac] = new YYARec(302,34);yyac++; 
					yya[yyac] = new YYARec(303,57);yyac++; 
					yya[yyac] = new YYARec(304,58);yyac++; 
					yya[yyac] = new YYARec(305,59);yyac++; 
					yya[yyac] = new YYARec(306,190);yyac++; 
					yya[yyac] = new YYARec(307,191);yyac++; 
					yya[yyac] = new YYARec(308,40);yyac++; 
					yya[yyac] = new YYARec(309,61);yyac++; 
					yya[yyac] = new YYARec(310,42);yyac++; 
					yya[yyac] = new YYARec(311,62);yyac++; 
					yya[yyac] = new YYARec(312,44);yyac++; 
					yya[yyac] = new YYARec(313,45);yyac++; 
					yya[yyac] = new YYARec(314,192);yyac++; 
					yya[yyac] = new YYARec(315,193);yyac++; 
					yya[yyac] = new YYARec(316,46);yyac++; 
					yya[yyac] = new YYARec(266,188);yyac++; 
					yya[yyac] = new YYARec(274,77);yyac++; 
					yya[yyac] = new YYARec(275,78);yyac++; 
					yya[yyac] = new YYARec(279,79);yyac++; 
					yya[yyac] = new YYARec(290,23);yyac++; 
					yya[yyac] = new YYARec(291,51);yyac++; 
					yya[yyac] = new YYARec(292,52);yyac++; 
					yya[yyac] = new YYARec(293,53);yyac++; 
					yya[yyac] = new YYARec(294,54);yyac++; 
					yya[yyac] = new YYARec(295,55);yyac++; 
					yya[yyac] = new YYARec(296,189);yyac++; 
					yya[yyac] = new YYARec(297,30);yyac++; 
					yya[yyac] = new YYARec(298,31);yyac++; 
					yya[yyac] = new YYARec(299,32);yyac++; 
					yya[yyac] = new YYARec(300,158);yyac++; 
					yya[yyac] = new YYARec(301,56);yyac++; 
					yya[yyac] = new YYARec(302,34);yyac++; 
					yya[yyac] = new YYARec(303,57);yyac++; 
					yya[yyac] = new YYARec(304,58);yyac++; 
					yya[yyac] = new YYARec(305,59);yyac++; 
					yya[yyac] = new YYARec(306,190);yyac++; 
					yya[yyac] = new YYARec(307,191);yyac++; 
					yya[yyac] = new YYARec(308,40);yyac++; 
					yya[yyac] = new YYARec(309,61);yyac++; 
					yya[yyac] = new YYARec(310,42);yyac++; 
					yya[yyac] = new YYARec(311,62);yyac++; 
					yya[yyac] = new YYARec(312,44);yyac++; 
					yya[yyac] = new YYARec(313,45);yyac++; 
					yya[yyac] = new YYARec(314,192);yyac++; 
					yya[yyac] = new YYARec(315,193);yyac++; 
					yya[yyac] = new YYARec(316,46);yyac++; 
					yya[yyac] = new YYARec(257,-146 );yyac++; 
					yya[yyac] = new YYARec(274,-146 );yyac++; 
					yya[yyac] = new YYARec(275,-146 );yyac++; 
					yya[yyac] = new YYARec(279,-146 );yyac++; 
					yya[yyac] = new YYARec(290,-146 );yyac++; 
					yya[yyac] = new YYARec(291,-146 );yyac++; 
					yya[yyac] = new YYARec(292,-146 );yyac++; 
					yya[yyac] = new YYARec(293,-146 );yyac++; 
					yya[yyac] = new YYARec(294,-146 );yyac++; 
					yya[yyac] = new YYARec(295,-146 );yyac++; 
					yya[yyac] = new YYARec(296,-146 );yyac++; 
					yya[yyac] = new YYARec(297,-146 );yyac++; 
					yya[yyac] = new YYARec(298,-146 );yyac++; 
					yya[yyac] = new YYARec(299,-146 );yyac++; 
					yya[yyac] = new YYARec(300,-146 );yyac++; 
					yya[yyac] = new YYARec(301,-146 );yyac++; 
					yya[yyac] = new YYARec(302,-146 );yyac++; 
					yya[yyac] = new YYARec(303,-146 );yyac++; 
					yya[yyac] = new YYARec(304,-146 );yyac++; 
					yya[yyac] = new YYARec(305,-146 );yyac++; 
					yya[yyac] = new YYARec(306,-146 );yyac++; 
					yya[yyac] = new YYARec(307,-146 );yyac++; 
					yya[yyac] = new YYARec(308,-146 );yyac++; 
					yya[yyac] = new YYARec(309,-146 );yyac++; 
					yya[yyac] = new YYARec(310,-146 );yyac++; 
					yya[yyac] = new YYARec(311,-146 );yyac++; 
					yya[yyac] = new YYARec(312,-146 );yyac++; 
					yya[yyac] = new YYARec(313,-146 );yyac++; 
					yya[yyac] = new YYARec(314,-146 );yyac++; 
					yya[yyac] = new YYARec(315,-146 );yyac++; 
					yya[yyac] = new YYARec(316,-146 );yyac++; 
					yya[yyac] = new YYARec(317,-146 );yyac++; 
					yya[yyac] = new YYARec(318,-146 );yyac++; 
					yya[yyac] = new YYARec(260,-167 );yyac++; 
					yya[yyac] = new YYARec(281,-167 );yyac++; 
					yya[yyac] = new YYARec(282,-167 );yyac++; 
					yya[yyac] = new YYARec(283,-167 );yyac++; 
					yya[yyac] = new YYARec(284,-167 );yyac++; 
					yya[yyac] = new YYARec(285,-167 );yyac++; 
					yya[yyac] = new YYARec(289,-167 );yyac++; 
					yya[yyac] = new YYARec(257,-145 );yyac++; 
					yya[yyac] = new YYARec(274,-145 );yyac++; 
					yya[yyac] = new YYARec(275,-145 );yyac++; 
					yya[yyac] = new YYARec(279,-145 );yyac++; 
					yya[yyac] = new YYARec(290,-145 );yyac++; 
					yya[yyac] = new YYARec(291,-145 );yyac++; 
					yya[yyac] = new YYARec(292,-145 );yyac++; 
					yya[yyac] = new YYARec(293,-145 );yyac++; 
					yya[yyac] = new YYARec(294,-145 );yyac++; 
					yya[yyac] = new YYARec(295,-145 );yyac++; 
					yya[yyac] = new YYARec(296,-145 );yyac++; 
					yya[yyac] = new YYARec(297,-145 );yyac++; 
					yya[yyac] = new YYARec(298,-145 );yyac++; 
					yya[yyac] = new YYARec(299,-145 );yyac++; 
					yya[yyac] = new YYARec(300,-145 );yyac++; 
					yya[yyac] = new YYARec(301,-145 );yyac++; 
					yya[yyac] = new YYARec(302,-145 );yyac++; 
					yya[yyac] = new YYARec(303,-145 );yyac++; 
					yya[yyac] = new YYARec(304,-145 );yyac++; 
					yya[yyac] = new YYARec(305,-145 );yyac++; 
					yya[yyac] = new YYARec(306,-145 );yyac++; 
					yya[yyac] = new YYARec(307,-145 );yyac++; 
					yya[yyac] = new YYARec(308,-145 );yyac++; 
					yya[yyac] = new YYARec(309,-145 );yyac++; 
					yya[yyac] = new YYARec(310,-145 );yyac++; 
					yya[yyac] = new YYARec(311,-145 );yyac++; 
					yya[yyac] = new YYARec(312,-145 );yyac++; 
					yya[yyac] = new YYARec(313,-145 );yyac++; 
					yya[yyac] = new YYARec(314,-145 );yyac++; 
					yya[yyac] = new YYARec(315,-145 );yyac++; 
					yya[yyac] = new YYARec(316,-145 );yyac++; 
					yya[yyac] = new YYARec(317,-145 );yyac++; 
					yya[yyac] = new YYARec(318,-145 );yyac++; 
					yya[yyac] = new YYARec(260,-164 );yyac++; 
					yya[yyac] = new YYARec(281,-164 );yyac++; 
					yya[yyac] = new YYARec(282,-164 );yyac++; 
					yya[yyac] = new YYARec(283,-164 );yyac++; 
					yya[yyac] = new YYARec(284,-164 );yyac++; 
					yya[yyac] = new YYARec(285,-164 );yyac++; 
					yya[yyac] = new YYARec(289,-164 );yyac++; 
					yya[yyac] = new YYARec(257,-143 );yyac++; 
					yya[yyac] = new YYARec(274,-143 );yyac++; 
					yya[yyac] = new YYARec(275,-143 );yyac++; 
					yya[yyac] = new YYARec(279,-143 );yyac++; 
					yya[yyac] = new YYARec(290,-143 );yyac++; 
					yya[yyac] = new YYARec(291,-143 );yyac++; 
					yya[yyac] = new YYARec(292,-143 );yyac++; 
					yya[yyac] = new YYARec(293,-143 );yyac++; 
					yya[yyac] = new YYARec(294,-143 );yyac++; 
					yya[yyac] = new YYARec(295,-143 );yyac++; 
					yya[yyac] = new YYARec(296,-143 );yyac++; 
					yya[yyac] = new YYARec(297,-143 );yyac++; 
					yya[yyac] = new YYARec(298,-143 );yyac++; 
					yya[yyac] = new YYARec(299,-143 );yyac++; 
					yya[yyac] = new YYARec(300,-143 );yyac++; 
					yya[yyac] = new YYARec(301,-143 );yyac++; 
					yya[yyac] = new YYARec(302,-143 );yyac++; 
					yya[yyac] = new YYARec(303,-143 );yyac++; 
					yya[yyac] = new YYARec(304,-143 );yyac++; 
					yya[yyac] = new YYARec(305,-143 );yyac++; 
					yya[yyac] = new YYARec(306,-143 );yyac++; 
					yya[yyac] = new YYARec(307,-143 );yyac++; 
					yya[yyac] = new YYARec(308,-143 );yyac++; 
					yya[yyac] = new YYARec(309,-143 );yyac++; 
					yya[yyac] = new YYARec(310,-143 );yyac++; 
					yya[yyac] = new YYARec(311,-143 );yyac++; 
					yya[yyac] = new YYARec(312,-143 );yyac++; 
					yya[yyac] = new YYARec(313,-143 );yyac++; 
					yya[yyac] = new YYARec(314,-143 );yyac++; 
					yya[yyac] = new YYARec(315,-143 );yyac++; 
					yya[yyac] = new YYARec(316,-143 );yyac++; 
					yya[yyac] = new YYARec(317,-143 );yyac++; 
					yya[yyac] = new YYARec(318,-143 );yyac++; 
					yya[yyac] = new YYARec(260,-160 );yyac++; 
					yya[yyac] = new YYARec(281,-160 );yyac++; 
					yya[yyac] = new YYARec(282,-160 );yyac++; 
					yya[yyac] = new YYARec(283,-160 );yyac++; 
					yya[yyac] = new YYARec(284,-160 );yyac++; 
					yya[yyac] = new YYARec(285,-160 );yyac++; 
					yya[yyac] = new YYARec(289,-160 );yyac++; 
					yya[yyac] = new YYARec(257,-144 );yyac++; 
					yya[yyac] = new YYARec(274,-144 );yyac++; 
					yya[yyac] = new YYARec(275,-144 );yyac++; 
					yya[yyac] = new YYARec(279,-144 );yyac++; 
					yya[yyac] = new YYARec(290,-144 );yyac++; 
					yya[yyac] = new YYARec(291,-144 );yyac++; 
					yya[yyac] = new YYARec(292,-144 );yyac++; 
					yya[yyac] = new YYARec(293,-144 );yyac++; 
					yya[yyac] = new YYARec(294,-144 );yyac++; 
					yya[yyac] = new YYARec(295,-144 );yyac++; 
					yya[yyac] = new YYARec(296,-144 );yyac++; 
					yya[yyac] = new YYARec(297,-144 );yyac++; 
					yya[yyac] = new YYARec(298,-144 );yyac++; 
					yya[yyac] = new YYARec(299,-144 );yyac++; 
					yya[yyac] = new YYARec(300,-144 );yyac++; 
					yya[yyac] = new YYARec(301,-144 );yyac++; 
					yya[yyac] = new YYARec(302,-144 );yyac++; 
					yya[yyac] = new YYARec(303,-144 );yyac++; 
					yya[yyac] = new YYARec(304,-144 );yyac++; 
					yya[yyac] = new YYARec(305,-144 );yyac++; 
					yya[yyac] = new YYARec(306,-144 );yyac++; 
					yya[yyac] = new YYARec(307,-144 );yyac++; 
					yya[yyac] = new YYARec(308,-144 );yyac++; 
					yya[yyac] = new YYARec(309,-144 );yyac++; 
					yya[yyac] = new YYARec(310,-144 );yyac++; 
					yya[yyac] = new YYARec(311,-144 );yyac++; 
					yya[yyac] = new YYARec(312,-144 );yyac++; 
					yya[yyac] = new YYARec(313,-144 );yyac++; 
					yya[yyac] = new YYARec(314,-144 );yyac++; 
					yya[yyac] = new YYARec(315,-144 );yyac++; 
					yya[yyac] = new YYARec(316,-144 );yyac++; 
					yya[yyac] = new YYARec(317,-144 );yyac++; 
					yya[yyac] = new YYARec(318,-144 );yyac++; 
					yya[yyac] = new YYARec(260,-161 );yyac++; 
					yya[yyac] = new YYARec(281,-161 );yyac++; 
					yya[yyac] = new YYARec(282,-161 );yyac++; 
					yya[yyac] = new YYARec(283,-161 );yyac++; 
					yya[yyac] = new YYARec(284,-161 );yyac++; 
					yya[yyac] = new YYARec(285,-161 );yyac++; 
					yya[yyac] = new YYARec(289,-161 );yyac++; 
					yya[yyac] = new YYARec(257,198);yyac++; 
					yya[yyac] = new YYARec(260,-155 );yyac++; 
					yya[yyac] = new YYARec(274,-155 );yyac++; 
					yya[yyac] = new YYARec(275,-155 );yyac++; 
					yya[yyac] = new YYARec(279,-155 );yyac++; 
					yya[yyac] = new YYARec(281,-155 );yyac++; 
					yya[yyac] = new YYARec(282,-155 );yyac++; 
					yya[yyac] = new YYARec(283,-155 );yyac++; 
					yya[yyac] = new YYARec(284,-155 );yyac++; 
					yya[yyac] = new YYARec(285,-155 );yyac++; 
					yya[yyac] = new YYARec(289,-155 );yyac++; 
					yya[yyac] = new YYARec(290,-155 );yyac++; 
					yya[yyac] = new YYARec(291,-155 );yyac++; 
					yya[yyac] = new YYARec(292,-155 );yyac++; 
					yya[yyac] = new YYARec(293,-155 );yyac++; 
					yya[yyac] = new YYARec(294,-155 );yyac++; 
					yya[yyac] = new YYARec(295,-155 );yyac++; 
					yya[yyac] = new YYARec(296,-155 );yyac++; 
					yya[yyac] = new YYARec(297,-155 );yyac++; 
					yya[yyac] = new YYARec(298,-155 );yyac++; 
					yya[yyac] = new YYARec(299,-155 );yyac++; 
					yya[yyac] = new YYARec(300,-155 );yyac++; 
					yya[yyac] = new YYARec(301,-155 );yyac++; 
					yya[yyac] = new YYARec(302,-155 );yyac++; 
					yya[yyac] = new YYARec(303,-155 );yyac++; 
					yya[yyac] = new YYARec(304,-155 );yyac++; 
					yya[yyac] = new YYARec(305,-155 );yyac++; 
					yya[yyac] = new YYARec(306,-155 );yyac++; 
					yya[yyac] = new YYARec(307,-155 );yyac++; 
					yya[yyac] = new YYARec(308,-155 );yyac++; 
					yya[yyac] = new YYARec(309,-155 );yyac++; 
					yya[yyac] = new YYARec(310,-155 );yyac++; 
					yya[yyac] = new YYARec(311,-155 );yyac++; 
					yya[yyac] = new YYARec(312,-155 );yyac++; 
					yya[yyac] = new YYARec(313,-155 );yyac++; 
					yya[yyac] = new YYARec(314,-155 );yyac++; 
					yya[yyac] = new YYARec(315,-155 );yyac++; 
					yya[yyac] = new YYARec(316,-155 );yyac++; 
					yya[yyac] = new YYARec(317,-155 );yyac++; 
					yya[yyac] = new YYARec(318,-155 );yyac++; 
					yya[yyac] = new YYARec(274,77);yyac++; 
					yya[yyac] = new YYARec(275,78);yyac++; 
					yya[yyac] = new YYARec(279,79);yyac++; 
					yya[yyac] = new YYARec(290,23);yyac++; 
					yya[yyac] = new YYARec(291,51);yyac++; 
					yya[yyac] = new YYARec(292,52);yyac++; 
					yya[yyac] = new YYARec(293,53);yyac++; 
					yya[yyac] = new YYARec(294,54);yyac++; 
					yya[yyac] = new YYARec(295,55);yyac++; 
					yya[yyac] = new YYARec(296,29);yyac++; 
					yya[yyac] = new YYARec(297,30);yyac++; 
					yya[yyac] = new YYARec(298,31);yyac++; 
					yya[yyac] = new YYARec(299,32);yyac++; 
					yya[yyac] = new YYARec(301,56);yyac++; 
					yya[yyac] = new YYARec(302,34);yyac++; 
					yya[yyac] = new YYARec(303,57);yyac++; 
					yya[yyac] = new YYARec(304,58);yyac++; 
					yya[yyac] = new YYARec(305,59);yyac++; 
					yya[yyac] = new YYARec(306,38);yyac++; 
					yya[yyac] = new YYARec(307,60);yyac++; 
					yya[yyac] = new YYARec(308,40);yyac++; 
					yya[yyac] = new YYARec(309,61);yyac++; 
					yya[yyac] = new YYARec(310,42);yyac++; 
					yya[yyac] = new YYARec(311,62);yyac++; 
					yya[yyac] = new YYARec(312,44);yyac++; 
					yya[yyac] = new YYARec(313,45);yyac++; 
					yya[yyac] = new YYARec(314,80);yyac++; 
					yya[yyac] = new YYARec(315,81);yyac++; 
					yya[yyac] = new YYARec(316,46);yyac++; 
					yya[yyac] = new YYARec(317,82);yyac++; 
					yya[yyac] = new YYARec(318,83);yyac++; 
					yya[yyac] = new YYARec(257,142);yyac++; 
					yya[yyac] = new YYARec(293,91);yyac++; 
					yya[yyac] = new YYARec(294,92);yyac++; 
					yya[yyac] = new YYARec(298,94);yyac++; 
					yya[yyac] = new YYARec(303,95);yyac++; 
					yya[yyac] = new YYARec(304,96);yyac++; 
					yya[yyac] = new YYARec(307,98);yyac++; 
					yya[yyac] = new YYARec(309,100);yyac++; 
					yya[yyac] = new YYARec(311,102);yyac++; 
					yya[yyac] = new YYARec(312,103);yyac++; 
					yya[yyac] = new YYARec(259,-38 );yyac++; 
					yya[yyac] = new YYARec(259,206);yyac++; 
					yya[yyac] = new YYARec(314,111);yyac++; 
					yya[yyac] = new YYARec(274,77);yyac++; 
					yya[yyac] = new YYARec(275,78);yyac++; 
					yya[yyac] = new YYARec(279,79);yyac++; 
					yya[yyac] = new YYARec(314,80);yyac++; 
					yya[yyac] = new YYARec(257,-28 );yyac++; 
					yya[yyac] = new YYARec(257,208);yyac++; 
					yya[yyac] = new YYARec(290,23);yyac++; 
					yya[yyac] = new YYARec(291,51);yyac++; 
					yya[yyac] = new YYARec(292,52);yyac++; 
					yya[yyac] = new YYARec(293,53);yyac++; 
					yya[yyac] = new YYARec(294,54);yyac++; 
					yya[yyac] = new YYARec(295,55);yyac++; 
					yya[yyac] = new YYARec(296,29);yyac++; 
					yya[yyac] = new YYARec(297,30);yyac++; 
					yya[yyac] = new YYARec(298,31);yyac++; 
					yya[yyac] = new YYARec(299,32);yyac++; 
					yya[yyac] = new YYARec(301,56);yyac++; 
					yya[yyac] = new YYARec(302,34);yyac++; 
					yya[yyac] = new YYARec(303,57);yyac++; 
					yya[yyac] = new YYARec(304,58);yyac++; 
					yya[yyac] = new YYARec(305,59);yyac++; 
					yya[yyac] = new YYARec(306,38);yyac++; 
					yya[yyac] = new YYARec(307,60);yyac++; 
					yya[yyac] = new YYARec(308,40);yyac++; 
					yya[yyac] = new YYARec(309,61);yyac++; 
					yya[yyac] = new YYARec(310,42);yyac++; 
					yya[yyac] = new YYARec(311,62);yyac++; 
					yya[yyac] = new YYARec(312,44);yyac++; 
					yya[yyac] = new YYARec(313,45);yyac++; 
					yya[yyac] = new YYARec(314,111);yyac++; 
					yya[yyac] = new YYARec(315,112);yyac++; 
					yya[yyac] = new YYARec(316,46);yyac++; 
					yya[yyac] = new YYARec(274,77);yyac++; 
					yya[yyac] = new YYARec(275,78);yyac++; 
					yya[yyac] = new YYARec(279,79);yyac++; 
					yya[yyac] = new YYARec(290,23);yyac++; 
					yya[yyac] = new YYARec(291,51);yyac++; 
					yya[yyac] = new YYARec(292,52);yyac++; 
					yya[yyac] = new YYARec(293,53);yyac++; 
					yya[yyac] = new YYARec(294,54);yyac++; 
					yya[yyac] = new YYARec(295,55);yyac++; 
					yya[yyac] = new YYARec(296,29);yyac++; 
					yya[yyac] = new YYARec(297,30);yyac++; 
					yya[yyac] = new YYARec(298,31);yyac++; 
					yya[yyac] = new YYARec(299,32);yyac++; 
					yya[yyac] = new YYARec(300,158);yyac++; 
					yya[yyac] = new YYARec(301,56);yyac++; 
					yya[yyac] = new YYARec(302,34);yyac++; 
					yya[yyac] = new YYARec(303,57);yyac++; 
					yya[yyac] = new YYARec(304,58);yyac++; 
					yya[yyac] = new YYARec(305,59);yyac++; 
					yya[yyac] = new YYARec(306,38);yyac++; 
					yya[yyac] = new YYARec(307,60);yyac++; 
					yya[yyac] = new YYARec(308,40);yyac++; 
					yya[yyac] = new YYARec(309,61);yyac++; 
					yya[yyac] = new YYARec(310,42);yyac++; 
					yya[yyac] = new YYARec(311,62);yyac++; 
					yya[yyac] = new YYARec(312,44);yyac++; 
					yya[yyac] = new YYARec(313,45);yyac++; 
					yya[yyac] = new YYARec(314,80);yyac++; 
					yya[yyac] = new YYARec(315,81);yyac++; 
					yya[yyac] = new YYARec(316,46);yyac++; 
					yya[yyac] = new YYARec(317,82);yyac++; 
					yya[yyac] = new YYARec(318,83);yyac++; 
					yya[yyac] = new YYARec(257,-60 );yyac++; 
					yya[yyac] = new YYARec(257,211);yyac++; 
					yya[yyac] = new YYARec(257,125);yyac++; 
					yya[yyac] = new YYARec(258,126);yyac++; 
					yya[yyac] = new YYARec(280,127);yyac++; 
					yya[yyac] = new YYARec(286,128);yyac++; 
					yya[yyac] = new YYARec(287,129);yyac++; 
					yya[yyac] = new YYARec(288,130);yyac++; 
					yya[yyac] = new YYARec(290,23);yyac++; 
					yya[yyac] = new YYARec(291,51);yyac++; 
					yya[yyac] = new YYARec(292,52);yyac++; 
					yya[yyac] = new YYARec(293,53);yyac++; 
					yya[yyac] = new YYARec(294,54);yyac++; 
					yya[yyac] = new YYARec(295,55);yyac++; 
					yya[yyac] = new YYARec(296,29);yyac++; 
					yya[yyac] = new YYARec(297,30);yyac++; 
					yya[yyac] = new YYARec(298,31);yyac++; 
					yya[yyac] = new YYARec(299,131);yyac++; 
					yya[yyac] = new YYARec(301,56);yyac++; 
					yya[yyac] = new YYARec(302,34);yyac++; 
					yya[yyac] = new YYARec(303,57);yyac++; 
					yya[yyac] = new YYARec(304,58);yyac++; 
					yya[yyac] = new YYARec(305,59);yyac++; 
					yya[yyac] = new YYARec(306,132);yyac++; 
					yya[yyac] = new YYARec(307,60);yyac++; 
					yya[yyac] = new YYARec(308,40);yyac++; 
					yya[yyac] = new YYARec(309,61);yyac++; 
					yya[yyac] = new YYARec(310,133);yyac++; 
					yya[yyac] = new YYARec(311,62);yyac++; 
					yya[yyac] = new YYARec(312,134);yyac++; 
					yya[yyac] = new YYARec(313,45);yyac++; 
					yya[yyac] = new YYARec(316,135);yyac++; 
					yya[yyac] = new YYARec(259,-54 );yyac++; 
					yya[yyac] = new YYARec(257,213);yyac++; 
					yya[yyac] = new YYARec(266,188);yyac++; 
					yya[yyac] = new YYARec(274,77);yyac++; 
					yya[yyac] = new YYARec(275,78);yyac++; 
					yya[yyac] = new YYARec(279,79);yyac++; 
					yya[yyac] = new YYARec(290,23);yyac++; 
					yya[yyac] = new YYARec(291,51);yyac++; 
					yya[yyac] = new YYARec(292,52);yyac++; 
					yya[yyac] = new YYARec(293,53);yyac++; 
					yya[yyac] = new YYARec(294,54);yyac++; 
					yya[yyac] = new YYARec(295,55);yyac++; 
					yya[yyac] = new YYARec(296,189);yyac++; 
					yya[yyac] = new YYARec(297,30);yyac++; 
					yya[yyac] = new YYARec(298,31);yyac++; 
					yya[yyac] = new YYARec(299,32);yyac++; 
					yya[yyac] = new YYARec(300,158);yyac++; 
					yya[yyac] = new YYARec(301,56);yyac++; 
					yya[yyac] = new YYARec(302,34);yyac++; 
					yya[yyac] = new YYARec(303,57);yyac++; 
					yya[yyac] = new YYARec(304,58);yyac++; 
					yya[yyac] = new YYARec(305,59);yyac++; 
					yya[yyac] = new YYARec(306,190);yyac++; 
					yya[yyac] = new YYARec(307,191);yyac++; 
					yya[yyac] = new YYARec(308,40);yyac++; 
					yya[yyac] = new YYARec(309,61);yyac++; 
					yya[yyac] = new YYARec(310,42);yyac++; 
					yya[yyac] = new YYARec(311,62);yyac++; 
					yya[yyac] = new YYARec(312,44);yyac++; 
					yya[yyac] = new YYARec(313,45);yyac++; 
					yya[yyac] = new YYARec(314,192);yyac++; 
					yya[yyac] = new YYARec(315,193);yyac++; 
					yya[yyac] = new YYARec(316,46);yyac++; 
					yya[yyac] = new YYARec(259,215);yyac++; 
					yya[yyac] = new YYARec(266,216);yyac++; 
					yya[yyac] = new YYARec(266,188);yyac++; 
					yya[yyac] = new YYARec(274,77);yyac++; 
					yya[yyac] = new YYARec(275,78);yyac++; 
					yya[yyac] = new YYARec(279,79);yyac++; 
					yya[yyac] = new YYARec(290,23);yyac++; 
					yya[yyac] = new YYARec(291,51);yyac++; 
					yya[yyac] = new YYARec(292,52);yyac++; 
					yya[yyac] = new YYARec(293,53);yyac++; 
					yya[yyac] = new YYARec(294,54);yyac++; 
					yya[yyac] = new YYARec(295,55);yyac++; 
					yya[yyac] = new YYARec(296,189);yyac++; 
					yya[yyac] = new YYARec(297,30);yyac++; 
					yya[yyac] = new YYARec(298,31);yyac++; 
					yya[yyac] = new YYARec(299,32);yyac++; 
					yya[yyac] = new YYARec(300,158);yyac++; 
					yya[yyac] = new YYARec(301,56);yyac++; 
					yya[yyac] = new YYARec(302,34);yyac++; 
					yya[yyac] = new YYARec(303,57);yyac++; 
					yya[yyac] = new YYARec(304,58);yyac++; 
					yya[yyac] = new YYARec(305,59);yyac++; 
					yya[yyac] = new YYARec(306,190);yyac++; 
					yya[yyac] = new YYARec(307,191);yyac++; 
					yya[yyac] = new YYARec(308,40);yyac++; 
					yya[yyac] = new YYARec(309,61);yyac++; 
					yya[yyac] = new YYARec(310,42);yyac++; 
					yya[yyac] = new YYARec(311,62);yyac++; 
					yya[yyac] = new YYARec(312,44);yyac++; 
					yya[yyac] = new YYARec(313,45);yyac++; 
					yya[yyac] = new YYARec(314,192);yyac++; 
					yya[yyac] = new YYARec(315,193);yyac++; 
					yya[yyac] = new YYARec(316,46);yyac++; 
					yya[yyac] = new YYARec(276,219);yyac++; 
					yya[yyac] = new YYARec(277,220);yyac++; 
					yya[yyac] = new YYARec(278,221);yyac++; 
					yya[yyac] = new YYARec(257,-82 );yyac++; 
					yya[yyac] = new YYARec(258,-82 );yyac++; 
					yya[yyac] = new YYARec(261,-82 );yyac++; 
					yya[yyac] = new YYARec(262,-82 );yyac++; 
					yya[yyac] = new YYARec(263,-82 );yyac++; 
					yya[yyac] = new YYARec(264,-82 );yyac++; 
					yya[yyac] = new YYARec(265,-82 );yyac++; 
					yya[yyac] = new YYARec(267,-82 );yyac++; 
					yya[yyac] = new YYARec(268,-82 );yyac++; 
					yya[yyac] = new YYARec(269,-82 );yyac++; 
					yya[yyac] = new YYARec(270,-82 );yyac++; 
					yya[yyac] = new YYARec(271,-82 );yyac++; 
					yya[yyac] = new YYARec(272,-82 );yyac++; 
					yya[yyac] = new YYARec(273,-82 );yyac++; 
					yya[yyac] = new YYARec(274,-82 );yyac++; 
					yya[yyac] = new YYARec(275,-82 );yyac++; 
					yya[yyac] = new YYARec(274,223);yyac++; 
					yya[yyac] = new YYARec(275,224);yyac++; 
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
					yya[yyac] = new YYARec(270,226);yyac++; 
					yya[yyac] = new YYARec(271,227);yyac++; 
					yya[yyac] = new YYARec(272,228);yyac++; 
					yya[yyac] = new YYARec(273,229);yyac++; 
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
					yya[yyac] = new YYARec(268,231);yyac++; 
					yya[yyac] = new YYARec(269,232);yyac++; 
					yya[yyac] = new YYARec(257,-77 );yyac++; 
					yya[yyac] = new YYARec(258,-77 );yyac++; 
					yya[yyac] = new YYARec(261,-77 );yyac++; 
					yya[yyac] = new YYARec(262,-77 );yyac++; 
					yya[yyac] = new YYARec(263,-77 );yyac++; 
					yya[yyac] = new YYARec(264,-77 );yyac++; 
					yya[yyac] = new YYARec(265,-77 );yyac++; 
					yya[yyac] = new YYARec(267,-77 );yyac++; 
					yya[yyac] = new YYARec(265,233);yyac++; 
					yya[yyac] = new YYARec(257,-75 );yyac++; 
					yya[yyac] = new YYARec(258,-75 );yyac++; 
					yya[yyac] = new YYARec(261,-75 );yyac++; 
					yya[yyac] = new YYARec(262,-75 );yyac++; 
					yya[yyac] = new YYARec(263,-75 );yyac++; 
					yya[yyac] = new YYARec(264,-75 );yyac++; 
					yya[yyac] = new YYARec(267,-75 );yyac++; 
					yya[yyac] = new YYARec(264,234);yyac++; 
					yya[yyac] = new YYARec(257,-73 );yyac++; 
					yya[yyac] = new YYARec(258,-73 );yyac++; 
					yya[yyac] = new YYARec(261,-73 );yyac++; 
					yya[yyac] = new YYARec(262,-73 );yyac++; 
					yya[yyac] = new YYARec(263,-73 );yyac++; 
					yya[yyac] = new YYARec(267,-73 );yyac++; 
					yya[yyac] = new YYARec(263,235);yyac++; 
					yya[yyac] = new YYARec(257,-71 );yyac++; 
					yya[yyac] = new YYARec(258,-71 );yyac++; 
					yya[yyac] = new YYARec(261,-71 );yyac++; 
					yya[yyac] = new YYARec(262,-71 );yyac++; 
					yya[yyac] = new YYARec(267,-71 );yyac++; 
					yya[yyac] = new YYARec(262,236);yyac++; 
					yya[yyac] = new YYARec(257,-69 );yyac++; 
					yya[yyac] = new YYARec(258,-69 );yyac++; 
					yya[yyac] = new YYARec(261,-69 );yyac++; 
					yya[yyac] = new YYARec(267,-69 );yyac++; 
					yya[yyac] = new YYARec(261,237);yyac++; 
					yya[yyac] = new YYARec(257,-67 );yyac++; 
					yya[yyac] = new YYARec(258,-67 );yyac++; 
					yya[yyac] = new YYARec(267,-67 );yyac++; 
					yya[yyac] = new YYARec(281,165);yyac++; 
					yya[yyac] = new YYARec(282,166);yyac++; 
					yya[yyac] = new YYARec(283,167);yyac++; 
					yya[yyac] = new YYARec(284,168);yyac++; 
					yya[yyac] = new YYARec(285,169);yyac++; 
					yya[yyac] = new YYARec(314,239);yyac++; 
					yya[yyac] = new YYARec(257,-93 );yyac++; 
					yya[yyac] = new YYARec(261,-93 );yyac++; 
					yya[yyac] = new YYARec(262,-93 );yyac++; 
					yya[yyac] = new YYARec(263,-93 );yyac++; 
					yya[yyac] = new YYARec(264,-93 );yyac++; 
					yya[yyac] = new YYARec(265,-93 );yyac++; 
					yya[yyac] = new YYARec(268,-93 );yyac++; 
					yya[yyac] = new YYARec(269,-93 );yyac++; 
					yya[yyac] = new YYARec(270,-93 );yyac++; 
					yya[yyac] = new YYARec(271,-93 );yyac++; 
					yya[yyac] = new YYARec(272,-93 );yyac++; 
					yya[yyac] = new YYARec(273,-93 );yyac++; 
					yya[yyac] = new YYARec(274,-93 );yyac++; 
					yya[yyac] = new YYARec(275,-93 );yyac++; 
					yya[yyac] = new YYARec(276,-93 );yyac++; 
					yya[yyac] = new YYARec(277,-93 );yyac++; 
					yya[yyac] = new YYARec(278,-93 );yyac++; 
					yya[yyac] = new YYARec(266,188);yyac++; 
					yya[yyac] = new YYARec(274,77);yyac++; 
					yya[yyac] = new YYARec(275,78);yyac++; 
					yya[yyac] = new YYARec(279,79);yyac++; 
					yya[yyac] = new YYARec(290,23);yyac++; 
					yya[yyac] = new YYARec(291,51);yyac++; 
					yya[yyac] = new YYARec(292,52);yyac++; 
					yya[yyac] = new YYARec(293,53);yyac++; 
					yya[yyac] = new YYARec(294,54);yyac++; 
					yya[yyac] = new YYARec(295,55);yyac++; 
					yya[yyac] = new YYARec(296,189);yyac++; 
					yya[yyac] = new YYARec(297,30);yyac++; 
					yya[yyac] = new YYARec(298,31);yyac++; 
					yya[yyac] = new YYARec(299,32);yyac++; 
					yya[yyac] = new YYARec(300,158);yyac++; 
					yya[yyac] = new YYARec(301,56);yyac++; 
					yya[yyac] = new YYARec(302,34);yyac++; 
					yya[yyac] = new YYARec(303,57);yyac++; 
					yya[yyac] = new YYARec(304,58);yyac++; 
					yya[yyac] = new YYARec(305,59);yyac++; 
					yya[yyac] = new YYARec(306,190);yyac++; 
					yya[yyac] = new YYARec(307,191);yyac++; 
					yya[yyac] = new YYARec(308,40);yyac++; 
					yya[yyac] = new YYARec(309,61);yyac++; 
					yya[yyac] = new YYARec(310,42);yyac++; 
					yya[yyac] = new YYARec(311,62);yyac++; 
					yya[yyac] = new YYARec(312,44);yyac++; 
					yya[yyac] = new YYARec(313,45);yyac++; 
					yya[yyac] = new YYARec(314,192);yyac++; 
					yya[yyac] = new YYARec(315,193);yyac++; 
					yya[yyac] = new YYARec(316,46);yyac++; 
					yya[yyac] = new YYARec(266,-119 );yyac++; 
					yya[yyac] = new YYARec(257,-172 );yyac++; 
					yya[yyac] = new YYARec(258,-172 );yyac++; 
					yya[yyac] = new YYARec(261,-172 );yyac++; 
					yya[yyac] = new YYARec(262,-172 );yyac++; 
					yya[yyac] = new YYARec(263,-172 );yyac++; 
					yya[yyac] = new YYARec(264,-172 );yyac++; 
					yya[yyac] = new YYARec(265,-172 );yyac++; 
					yya[yyac] = new YYARec(267,-172 );yyac++; 
					yya[yyac] = new YYARec(268,-172 );yyac++; 
					yya[yyac] = new YYARec(269,-172 );yyac++; 
					yya[yyac] = new YYARec(270,-172 );yyac++; 
					yya[yyac] = new YYARec(271,-172 );yyac++; 
					yya[yyac] = new YYARec(272,-172 );yyac++; 
					yya[yyac] = new YYARec(273,-172 );yyac++; 
					yya[yyac] = new YYARec(274,-172 );yyac++; 
					yya[yyac] = new YYARec(275,-172 );yyac++; 
					yya[yyac] = new YYARec(276,-172 );yyac++; 
					yya[yyac] = new YYARec(277,-172 );yyac++; 
					yya[yyac] = new YYARec(278,-172 );yyac++; 
					yya[yyac] = new YYARec(281,-172 );yyac++; 
					yya[yyac] = new YYARec(282,-172 );yyac++; 
					yya[yyac] = new YYARec(283,-172 );yyac++; 
					yya[yyac] = new YYARec(284,-172 );yyac++; 
					yya[yyac] = new YYARec(285,-172 );yyac++; 
					yya[yyac] = new YYARec(289,-172 );yyac++; 
					yya[yyac] = new YYARec(314,-172 );yyac++; 
					yya[yyac] = new YYARec(266,-117 );yyac++; 
					yya[yyac] = new YYARec(257,-164 );yyac++; 
					yya[yyac] = new YYARec(258,-164 );yyac++; 
					yya[yyac] = new YYARec(261,-164 );yyac++; 
					yya[yyac] = new YYARec(262,-164 );yyac++; 
					yya[yyac] = new YYARec(263,-164 );yyac++; 
					yya[yyac] = new YYARec(264,-164 );yyac++; 
					yya[yyac] = new YYARec(265,-164 );yyac++; 
					yya[yyac] = new YYARec(267,-164 );yyac++; 
					yya[yyac] = new YYARec(268,-164 );yyac++; 
					yya[yyac] = new YYARec(269,-164 );yyac++; 
					yya[yyac] = new YYARec(270,-164 );yyac++; 
					yya[yyac] = new YYARec(271,-164 );yyac++; 
					yya[yyac] = new YYARec(272,-164 );yyac++; 
					yya[yyac] = new YYARec(273,-164 );yyac++; 
					yya[yyac] = new YYARec(274,-164 );yyac++; 
					yya[yyac] = new YYARec(275,-164 );yyac++; 
					yya[yyac] = new YYARec(276,-164 );yyac++; 
					yya[yyac] = new YYARec(277,-164 );yyac++; 
					yya[yyac] = new YYARec(278,-164 );yyac++; 
					yya[yyac] = new YYARec(281,-164 );yyac++; 
					yya[yyac] = new YYARec(282,-164 );yyac++; 
					yya[yyac] = new YYARec(283,-164 );yyac++; 
					yya[yyac] = new YYARec(284,-164 );yyac++; 
					yya[yyac] = new YYARec(285,-164 );yyac++; 
					yya[yyac] = new YYARec(289,-164 );yyac++; 
					yya[yyac] = new YYARec(314,-164 );yyac++; 
					yya[yyac] = new YYARec(266,-118 );yyac++; 
					yya[yyac] = new YYARec(257,-176 );yyac++; 
					yya[yyac] = new YYARec(258,-176 );yyac++; 
					yya[yyac] = new YYARec(261,-176 );yyac++; 
					yya[yyac] = new YYARec(262,-176 );yyac++; 
					yya[yyac] = new YYARec(263,-176 );yyac++; 
					yya[yyac] = new YYARec(264,-176 );yyac++; 
					yya[yyac] = new YYARec(265,-176 );yyac++; 
					yya[yyac] = new YYARec(267,-176 );yyac++; 
					yya[yyac] = new YYARec(268,-176 );yyac++; 
					yya[yyac] = new YYARec(269,-176 );yyac++; 
					yya[yyac] = new YYARec(270,-176 );yyac++; 
					yya[yyac] = new YYARec(271,-176 );yyac++; 
					yya[yyac] = new YYARec(272,-176 );yyac++; 
					yya[yyac] = new YYARec(273,-176 );yyac++; 
					yya[yyac] = new YYARec(274,-176 );yyac++; 
					yya[yyac] = new YYARec(275,-176 );yyac++; 
					yya[yyac] = new YYARec(276,-176 );yyac++; 
					yya[yyac] = new YYARec(277,-176 );yyac++; 
					yya[yyac] = new YYARec(278,-176 );yyac++; 
					yya[yyac] = new YYARec(281,-176 );yyac++; 
					yya[yyac] = new YYARec(282,-176 );yyac++; 
					yya[yyac] = new YYARec(283,-176 );yyac++; 
					yya[yyac] = new YYARec(284,-176 );yyac++; 
					yya[yyac] = new YYARec(285,-176 );yyac++; 
					yya[yyac] = new YYARec(289,-176 );yyac++; 
					yya[yyac] = new YYARec(314,-176 );yyac++; 
					yya[yyac] = new YYARec(314,241);yyac++; 
					yya[yyac] = new YYARec(257,-150 );yyac++; 
					yya[yyac] = new YYARec(258,-150 );yyac++; 
					yya[yyac] = new YYARec(261,-150 );yyac++; 
					yya[yyac] = new YYARec(262,-150 );yyac++; 
					yya[yyac] = new YYARec(263,-150 );yyac++; 
					yya[yyac] = new YYARec(264,-150 );yyac++; 
					yya[yyac] = new YYARec(265,-150 );yyac++; 
					yya[yyac] = new YYARec(267,-150 );yyac++; 
					yya[yyac] = new YYARec(268,-150 );yyac++; 
					yya[yyac] = new YYARec(269,-150 );yyac++; 
					yya[yyac] = new YYARec(270,-150 );yyac++; 
					yya[yyac] = new YYARec(271,-150 );yyac++; 
					yya[yyac] = new YYARec(272,-150 );yyac++; 
					yya[yyac] = new YYARec(273,-150 );yyac++; 
					yya[yyac] = new YYARec(274,-150 );yyac++; 
					yya[yyac] = new YYARec(275,-150 );yyac++; 
					yya[yyac] = new YYARec(276,-150 );yyac++; 
					yya[yyac] = new YYARec(277,-150 );yyac++; 
					yya[yyac] = new YYARec(278,-150 );yyac++; 
					yya[yyac] = new YYARec(257,125);yyac++; 
					yya[yyac] = new YYARec(258,126);yyac++; 
					yya[yyac] = new YYARec(280,127);yyac++; 
					yya[yyac] = new YYARec(286,128);yyac++; 
					yya[yyac] = new YYARec(287,129);yyac++; 
					yya[yyac] = new YYARec(288,130);yyac++; 
					yya[yyac] = new YYARec(290,23);yyac++; 
					yya[yyac] = new YYARec(291,51);yyac++; 
					yya[yyac] = new YYARec(292,52);yyac++; 
					yya[yyac] = new YYARec(293,53);yyac++; 
					yya[yyac] = new YYARec(294,54);yyac++; 
					yya[yyac] = new YYARec(295,55);yyac++; 
					yya[yyac] = new YYARec(296,29);yyac++; 
					yya[yyac] = new YYARec(297,30);yyac++; 
					yya[yyac] = new YYARec(298,31);yyac++; 
					yya[yyac] = new YYARec(299,131);yyac++; 
					yya[yyac] = new YYARec(301,56);yyac++; 
					yya[yyac] = new YYARec(302,34);yyac++; 
					yya[yyac] = new YYARec(303,57);yyac++; 
					yya[yyac] = new YYARec(304,58);yyac++; 
					yya[yyac] = new YYARec(305,59);yyac++; 
					yya[yyac] = new YYARec(306,132);yyac++; 
					yya[yyac] = new YYARec(307,60);yyac++; 
					yya[yyac] = new YYARec(308,40);yyac++; 
					yya[yyac] = new YYARec(309,61);yyac++; 
					yya[yyac] = new YYARec(310,133);yyac++; 
					yya[yyac] = new YYARec(311,62);yyac++; 
					yya[yyac] = new YYARec(312,134);yyac++; 
					yya[yyac] = new YYARec(313,45);yyac++; 
					yya[yyac] = new YYARec(316,135);yyac++; 
					yya[yyac] = new YYARec(259,-54 );yyac++; 
					yya[yyac] = new YYARec(258,243);yyac++; 
					yya[yyac] = new YYARec(314,239);yyac++; 
					yya[yyac] = new YYARec(257,-93 );yyac++; 
					yya[yyac] = new YYARec(258,-93 );yyac++; 
					yya[yyac] = new YYARec(261,-93 );yyac++; 
					yya[yyac] = new YYARec(262,-93 );yyac++; 
					yya[yyac] = new YYARec(263,-93 );yyac++; 
					yya[yyac] = new YYARec(264,-93 );yyac++; 
					yya[yyac] = new YYARec(265,-93 );yyac++; 
					yya[yyac] = new YYARec(267,-93 );yyac++; 
					yya[yyac] = new YYARec(268,-93 );yyac++; 
					yya[yyac] = new YYARec(269,-93 );yyac++; 
					yya[yyac] = new YYARec(270,-93 );yyac++; 
					yya[yyac] = new YYARec(271,-93 );yyac++; 
					yya[yyac] = new YYARec(272,-93 );yyac++; 
					yya[yyac] = new YYARec(273,-93 );yyac++; 
					yya[yyac] = new YYARec(274,-93 );yyac++; 
					yya[yyac] = new YYARec(275,-93 );yyac++; 
					yya[yyac] = new YYARec(276,-93 );yyac++; 
					yya[yyac] = new YYARec(277,-93 );yyac++; 
					yya[yyac] = new YYARec(278,-93 );yyac++; 
					yya[yyac] = new YYARec(258,244);yyac++; 
					yya[yyac] = new YYARec(257,125);yyac++; 
					yya[yyac] = new YYARec(258,126);yyac++; 
					yya[yyac] = new YYARec(280,127);yyac++; 
					yya[yyac] = new YYARec(286,128);yyac++; 
					yya[yyac] = new YYARec(287,129);yyac++; 
					yya[yyac] = new YYARec(288,130);yyac++; 
					yya[yyac] = new YYARec(290,23);yyac++; 
					yya[yyac] = new YYARec(291,51);yyac++; 
					yya[yyac] = new YYARec(292,52);yyac++; 
					yya[yyac] = new YYARec(293,53);yyac++; 
					yya[yyac] = new YYARec(294,54);yyac++; 
					yya[yyac] = new YYARec(295,55);yyac++; 
					yya[yyac] = new YYARec(296,29);yyac++; 
					yya[yyac] = new YYARec(297,30);yyac++; 
					yya[yyac] = new YYARec(298,31);yyac++; 
					yya[yyac] = new YYARec(299,131);yyac++; 
					yya[yyac] = new YYARec(301,56);yyac++; 
					yya[yyac] = new YYARec(302,34);yyac++; 
					yya[yyac] = new YYARec(303,57);yyac++; 
					yya[yyac] = new YYARec(304,58);yyac++; 
					yya[yyac] = new YYARec(305,59);yyac++; 
					yya[yyac] = new YYARec(306,132);yyac++; 
					yya[yyac] = new YYARec(307,60);yyac++; 
					yya[yyac] = new YYARec(308,40);yyac++; 
					yya[yyac] = new YYARec(309,61);yyac++; 
					yya[yyac] = new YYARec(310,133);yyac++; 
					yya[yyac] = new YYARec(311,62);yyac++; 
					yya[yyac] = new YYARec(312,134);yyac++; 
					yya[yyac] = new YYARec(313,45);yyac++; 
					yya[yyac] = new YYARec(316,135);yyac++; 
					yya[yyac] = new YYARec(259,-54 );yyac++; 
					yya[yyac] = new YYARec(274,77);yyac++; 
					yya[yyac] = new YYARec(275,78);yyac++; 
					yya[yyac] = new YYARec(279,79);yyac++; 
					yya[yyac] = new YYARec(290,23);yyac++; 
					yya[yyac] = new YYARec(291,51);yyac++; 
					yya[yyac] = new YYARec(292,52);yyac++; 
					yya[yyac] = new YYARec(293,53);yyac++; 
					yya[yyac] = new YYARec(294,54);yyac++; 
					yya[yyac] = new YYARec(295,55);yyac++; 
					yya[yyac] = new YYARec(296,29);yyac++; 
					yya[yyac] = new YYARec(297,30);yyac++; 
					yya[yyac] = new YYARec(298,31);yyac++; 
					yya[yyac] = new YYARec(299,32);yyac++; 
					yya[yyac] = new YYARec(301,56);yyac++; 
					yya[yyac] = new YYARec(302,34);yyac++; 
					yya[yyac] = new YYARec(303,57);yyac++; 
					yya[yyac] = new YYARec(304,58);yyac++; 
					yya[yyac] = new YYARec(305,59);yyac++; 
					yya[yyac] = new YYARec(306,38);yyac++; 
					yya[yyac] = new YYARec(307,60);yyac++; 
					yya[yyac] = new YYARec(308,40);yyac++; 
					yya[yyac] = new YYARec(309,61);yyac++; 
					yya[yyac] = new YYARec(310,42);yyac++; 
					yya[yyac] = new YYARec(311,62);yyac++; 
					yya[yyac] = new YYARec(312,44);yyac++; 
					yya[yyac] = new YYARec(313,45);yyac++; 
					yya[yyac] = new YYARec(314,80);yyac++; 
					yya[yyac] = new YYARec(315,81);yyac++; 
					yya[yyac] = new YYARec(316,46);yyac++; 
					yya[yyac] = new YYARec(317,82);yyac++; 
					yya[yyac] = new YYARec(318,83);yyac++; 
					yya[yyac] = new YYARec(257,-41 );yyac++; 
					yya[yyac] = new YYARec(257,247);yyac++; 
					yya[yyac] = new YYARec(257,125);yyac++; 
					yya[yyac] = new YYARec(258,126);yyac++; 
					yya[yyac] = new YYARec(280,127);yyac++; 
					yya[yyac] = new YYARec(286,128);yyac++; 
					yya[yyac] = new YYARec(287,129);yyac++; 
					yya[yyac] = new YYARec(288,130);yyac++; 
					yya[yyac] = new YYARec(290,23);yyac++; 
					yya[yyac] = new YYARec(291,51);yyac++; 
					yya[yyac] = new YYARec(292,52);yyac++; 
					yya[yyac] = new YYARec(293,53);yyac++; 
					yya[yyac] = new YYARec(294,54);yyac++; 
					yya[yyac] = new YYARec(295,55);yyac++; 
					yya[yyac] = new YYARec(296,29);yyac++; 
					yya[yyac] = new YYARec(297,30);yyac++; 
					yya[yyac] = new YYARec(298,31);yyac++; 
					yya[yyac] = new YYARec(299,131);yyac++; 
					yya[yyac] = new YYARec(301,56);yyac++; 
					yya[yyac] = new YYARec(302,34);yyac++; 
					yya[yyac] = new YYARec(303,57);yyac++; 
					yya[yyac] = new YYARec(304,58);yyac++; 
					yya[yyac] = new YYARec(305,59);yyac++; 
					yya[yyac] = new YYARec(306,132);yyac++; 
					yya[yyac] = new YYARec(307,60);yyac++; 
					yya[yyac] = new YYARec(308,40);yyac++; 
					yya[yyac] = new YYARec(309,61);yyac++; 
					yya[yyac] = new YYARec(310,133);yyac++; 
					yya[yyac] = new YYARec(311,62);yyac++; 
					yya[yyac] = new YYARec(312,134);yyac++; 
					yya[yyac] = new YYARec(313,45);yyac++; 
					yya[yyac] = new YYARec(316,135);yyac++; 
					yya[yyac] = new YYARec(259,-54 );yyac++; 
					yya[yyac] = new YYARec(266,188);yyac++; 
					yya[yyac] = new YYARec(274,77);yyac++; 
					yya[yyac] = new YYARec(275,78);yyac++; 
					yya[yyac] = new YYARec(279,79);yyac++; 
					yya[yyac] = new YYARec(290,23);yyac++; 
					yya[yyac] = new YYARec(291,51);yyac++; 
					yya[yyac] = new YYARec(292,52);yyac++; 
					yya[yyac] = new YYARec(293,53);yyac++; 
					yya[yyac] = new YYARec(294,54);yyac++; 
					yya[yyac] = new YYARec(295,55);yyac++; 
					yya[yyac] = new YYARec(296,189);yyac++; 
					yya[yyac] = new YYARec(297,30);yyac++; 
					yya[yyac] = new YYARec(298,31);yyac++; 
					yya[yyac] = new YYARec(299,32);yyac++; 
					yya[yyac] = new YYARec(300,158);yyac++; 
					yya[yyac] = new YYARec(301,56);yyac++; 
					yya[yyac] = new YYARec(302,34);yyac++; 
					yya[yyac] = new YYARec(303,57);yyac++; 
					yya[yyac] = new YYARec(304,58);yyac++; 
					yya[yyac] = new YYARec(305,59);yyac++; 
					yya[yyac] = new YYARec(306,190);yyac++; 
					yya[yyac] = new YYARec(307,191);yyac++; 
					yya[yyac] = new YYARec(308,40);yyac++; 
					yya[yyac] = new YYARec(309,61);yyac++; 
					yya[yyac] = new YYARec(310,42);yyac++; 
					yya[yyac] = new YYARec(311,62);yyac++; 
					yya[yyac] = new YYARec(312,44);yyac++; 
					yya[yyac] = new YYARec(313,45);yyac++; 
					yya[yyac] = new YYARec(314,192);yyac++; 
					yya[yyac] = new YYARec(315,193);yyac++; 
					yya[yyac] = new YYARec(316,46);yyac++; 
					yya[yyac] = new YYARec(266,188);yyac++; 
					yya[yyac] = new YYARec(274,77);yyac++; 
					yya[yyac] = new YYARec(275,78);yyac++; 
					yya[yyac] = new YYARec(279,79);yyac++; 
					yya[yyac] = new YYARec(290,23);yyac++; 
					yya[yyac] = new YYARec(291,51);yyac++; 
					yya[yyac] = new YYARec(292,52);yyac++; 
					yya[yyac] = new YYARec(293,53);yyac++; 
					yya[yyac] = new YYARec(294,54);yyac++; 
					yya[yyac] = new YYARec(295,55);yyac++; 
					yya[yyac] = new YYARec(296,189);yyac++; 
					yya[yyac] = new YYARec(297,30);yyac++; 
					yya[yyac] = new YYARec(298,31);yyac++; 
					yya[yyac] = new YYARec(299,32);yyac++; 
					yya[yyac] = new YYARec(300,158);yyac++; 
					yya[yyac] = new YYARec(301,56);yyac++; 
					yya[yyac] = new YYARec(302,34);yyac++; 
					yya[yyac] = new YYARec(303,57);yyac++; 
					yya[yyac] = new YYARec(304,58);yyac++; 
					yya[yyac] = new YYARec(305,59);yyac++; 
					yya[yyac] = new YYARec(306,190);yyac++; 
					yya[yyac] = new YYARec(307,191);yyac++; 
					yya[yyac] = new YYARec(308,40);yyac++; 
					yya[yyac] = new YYARec(309,61);yyac++; 
					yya[yyac] = new YYARec(310,42);yyac++; 
					yya[yyac] = new YYARec(311,62);yyac++; 
					yya[yyac] = new YYARec(312,44);yyac++; 
					yya[yyac] = new YYARec(313,45);yyac++; 
					yya[yyac] = new YYARec(314,192);yyac++; 
					yya[yyac] = new YYARec(315,193);yyac++; 
					yya[yyac] = new YYARec(316,46);yyac++; 
					yya[yyac] = new YYARec(266,188);yyac++; 
					yya[yyac] = new YYARec(274,77);yyac++; 
					yya[yyac] = new YYARec(275,78);yyac++; 
					yya[yyac] = new YYARec(279,79);yyac++; 
					yya[yyac] = new YYARec(290,23);yyac++; 
					yya[yyac] = new YYARec(291,51);yyac++; 
					yya[yyac] = new YYARec(292,52);yyac++; 
					yya[yyac] = new YYARec(293,53);yyac++; 
					yya[yyac] = new YYARec(294,54);yyac++; 
					yya[yyac] = new YYARec(295,55);yyac++; 
					yya[yyac] = new YYARec(296,189);yyac++; 
					yya[yyac] = new YYARec(297,30);yyac++; 
					yya[yyac] = new YYARec(298,31);yyac++; 
					yya[yyac] = new YYARec(299,32);yyac++; 
					yya[yyac] = new YYARec(300,158);yyac++; 
					yya[yyac] = new YYARec(301,56);yyac++; 
					yya[yyac] = new YYARec(302,34);yyac++; 
					yya[yyac] = new YYARec(303,57);yyac++; 
					yya[yyac] = new YYARec(304,58);yyac++; 
					yya[yyac] = new YYARec(305,59);yyac++; 
					yya[yyac] = new YYARec(306,190);yyac++; 
					yya[yyac] = new YYARec(307,191);yyac++; 
					yya[yyac] = new YYARec(308,40);yyac++; 
					yya[yyac] = new YYARec(309,61);yyac++; 
					yya[yyac] = new YYARec(310,42);yyac++; 
					yya[yyac] = new YYARec(311,62);yyac++; 
					yya[yyac] = new YYARec(312,44);yyac++; 
					yya[yyac] = new YYARec(313,45);yyac++; 
					yya[yyac] = new YYARec(314,192);yyac++; 
					yya[yyac] = new YYARec(315,193);yyac++; 
					yya[yyac] = new YYARec(316,46);yyac++; 
					yya[yyac] = new YYARec(266,188);yyac++; 
					yya[yyac] = new YYARec(274,77);yyac++; 
					yya[yyac] = new YYARec(275,78);yyac++; 
					yya[yyac] = new YYARec(279,79);yyac++; 
					yya[yyac] = new YYARec(290,23);yyac++; 
					yya[yyac] = new YYARec(291,51);yyac++; 
					yya[yyac] = new YYARec(292,52);yyac++; 
					yya[yyac] = new YYARec(293,53);yyac++; 
					yya[yyac] = new YYARec(294,54);yyac++; 
					yya[yyac] = new YYARec(295,55);yyac++; 
					yya[yyac] = new YYARec(296,189);yyac++; 
					yya[yyac] = new YYARec(297,30);yyac++; 
					yya[yyac] = new YYARec(298,31);yyac++; 
					yya[yyac] = new YYARec(299,32);yyac++; 
					yya[yyac] = new YYARec(300,158);yyac++; 
					yya[yyac] = new YYARec(301,56);yyac++; 
					yya[yyac] = new YYARec(302,34);yyac++; 
					yya[yyac] = new YYARec(303,57);yyac++; 
					yya[yyac] = new YYARec(304,58);yyac++; 
					yya[yyac] = new YYARec(305,59);yyac++; 
					yya[yyac] = new YYARec(306,190);yyac++; 
					yya[yyac] = new YYARec(307,191);yyac++; 
					yya[yyac] = new YYARec(308,40);yyac++; 
					yya[yyac] = new YYARec(309,61);yyac++; 
					yya[yyac] = new YYARec(310,42);yyac++; 
					yya[yyac] = new YYARec(311,62);yyac++; 
					yya[yyac] = new YYARec(312,44);yyac++; 
					yya[yyac] = new YYARec(313,45);yyac++; 
					yya[yyac] = new YYARec(314,192);yyac++; 
					yya[yyac] = new YYARec(315,193);yyac++; 
					yya[yyac] = new YYARec(316,46);yyac++; 
					yya[yyac] = new YYARec(266,188);yyac++; 
					yya[yyac] = new YYARec(274,77);yyac++; 
					yya[yyac] = new YYARec(275,78);yyac++; 
					yya[yyac] = new YYARec(279,79);yyac++; 
					yya[yyac] = new YYARec(290,23);yyac++; 
					yya[yyac] = new YYARec(291,51);yyac++; 
					yya[yyac] = new YYARec(292,52);yyac++; 
					yya[yyac] = new YYARec(293,53);yyac++; 
					yya[yyac] = new YYARec(294,54);yyac++; 
					yya[yyac] = new YYARec(295,55);yyac++; 
					yya[yyac] = new YYARec(296,189);yyac++; 
					yya[yyac] = new YYARec(297,30);yyac++; 
					yya[yyac] = new YYARec(298,31);yyac++; 
					yya[yyac] = new YYARec(299,32);yyac++; 
					yya[yyac] = new YYARec(300,158);yyac++; 
					yya[yyac] = new YYARec(301,56);yyac++; 
					yya[yyac] = new YYARec(302,34);yyac++; 
					yya[yyac] = new YYARec(303,57);yyac++; 
					yya[yyac] = new YYARec(304,58);yyac++; 
					yya[yyac] = new YYARec(305,59);yyac++; 
					yya[yyac] = new YYARec(306,190);yyac++; 
					yya[yyac] = new YYARec(307,191);yyac++; 
					yya[yyac] = new YYARec(308,40);yyac++; 
					yya[yyac] = new YYARec(309,61);yyac++; 
					yya[yyac] = new YYARec(310,42);yyac++; 
					yya[yyac] = new YYARec(311,62);yyac++; 
					yya[yyac] = new YYARec(312,44);yyac++; 
					yya[yyac] = new YYARec(313,45);yyac++; 
					yya[yyac] = new YYARec(314,192);yyac++; 
					yya[yyac] = new YYARec(315,193);yyac++; 
					yya[yyac] = new YYARec(316,46);yyac++; 
					yya[yyac] = new YYARec(266,188);yyac++; 
					yya[yyac] = new YYARec(274,77);yyac++; 
					yya[yyac] = new YYARec(275,78);yyac++; 
					yya[yyac] = new YYARec(279,79);yyac++; 
					yya[yyac] = new YYARec(290,23);yyac++; 
					yya[yyac] = new YYARec(291,51);yyac++; 
					yya[yyac] = new YYARec(292,52);yyac++; 
					yya[yyac] = new YYARec(293,53);yyac++; 
					yya[yyac] = new YYARec(294,54);yyac++; 
					yya[yyac] = new YYARec(295,55);yyac++; 
					yya[yyac] = new YYARec(296,189);yyac++; 
					yya[yyac] = new YYARec(297,30);yyac++; 
					yya[yyac] = new YYARec(298,31);yyac++; 
					yya[yyac] = new YYARec(299,32);yyac++; 
					yya[yyac] = new YYARec(300,158);yyac++; 
					yya[yyac] = new YYARec(301,56);yyac++; 
					yya[yyac] = new YYARec(302,34);yyac++; 
					yya[yyac] = new YYARec(303,57);yyac++; 
					yya[yyac] = new YYARec(304,58);yyac++; 
					yya[yyac] = new YYARec(305,59);yyac++; 
					yya[yyac] = new YYARec(306,190);yyac++; 
					yya[yyac] = new YYARec(307,191);yyac++; 
					yya[yyac] = new YYARec(308,40);yyac++; 
					yya[yyac] = new YYARec(309,61);yyac++; 
					yya[yyac] = new YYARec(310,42);yyac++; 
					yya[yyac] = new YYARec(311,62);yyac++; 
					yya[yyac] = new YYARec(312,44);yyac++; 
					yya[yyac] = new YYARec(313,45);yyac++; 
					yya[yyac] = new YYARec(314,192);yyac++; 
					yya[yyac] = new YYARec(315,193);yyac++; 
					yya[yyac] = new YYARec(316,46);yyac++; 
					yya[yyac] = new YYARec(266,188);yyac++; 
					yya[yyac] = new YYARec(274,77);yyac++; 
					yya[yyac] = new YYARec(275,78);yyac++; 
					yya[yyac] = new YYARec(279,79);yyac++; 
					yya[yyac] = new YYARec(290,23);yyac++; 
					yya[yyac] = new YYARec(291,51);yyac++; 
					yya[yyac] = new YYARec(292,52);yyac++; 
					yya[yyac] = new YYARec(293,53);yyac++; 
					yya[yyac] = new YYARec(294,54);yyac++; 
					yya[yyac] = new YYARec(295,55);yyac++; 
					yya[yyac] = new YYARec(296,189);yyac++; 
					yya[yyac] = new YYARec(297,30);yyac++; 
					yya[yyac] = new YYARec(298,31);yyac++; 
					yya[yyac] = new YYARec(299,32);yyac++; 
					yya[yyac] = new YYARec(300,158);yyac++; 
					yya[yyac] = new YYARec(301,56);yyac++; 
					yya[yyac] = new YYARec(302,34);yyac++; 
					yya[yyac] = new YYARec(303,57);yyac++; 
					yya[yyac] = new YYARec(304,58);yyac++; 
					yya[yyac] = new YYARec(305,59);yyac++; 
					yya[yyac] = new YYARec(306,190);yyac++; 
					yya[yyac] = new YYARec(307,191);yyac++; 
					yya[yyac] = new YYARec(308,40);yyac++; 
					yya[yyac] = new YYARec(309,61);yyac++; 
					yya[yyac] = new YYARec(310,42);yyac++; 
					yya[yyac] = new YYARec(311,62);yyac++; 
					yya[yyac] = new YYARec(312,44);yyac++; 
					yya[yyac] = new YYARec(313,45);yyac++; 
					yya[yyac] = new YYARec(314,192);yyac++; 
					yya[yyac] = new YYARec(315,193);yyac++; 
					yya[yyac] = new YYARec(316,46);yyac++; 
					yya[yyac] = new YYARec(266,188);yyac++; 
					yya[yyac] = new YYARec(274,77);yyac++; 
					yya[yyac] = new YYARec(275,78);yyac++; 
					yya[yyac] = new YYARec(279,79);yyac++; 
					yya[yyac] = new YYARec(290,23);yyac++; 
					yya[yyac] = new YYARec(291,51);yyac++; 
					yya[yyac] = new YYARec(292,52);yyac++; 
					yya[yyac] = new YYARec(293,53);yyac++; 
					yya[yyac] = new YYARec(294,54);yyac++; 
					yya[yyac] = new YYARec(295,55);yyac++; 
					yya[yyac] = new YYARec(296,189);yyac++; 
					yya[yyac] = new YYARec(297,30);yyac++; 
					yya[yyac] = new YYARec(298,31);yyac++; 
					yya[yyac] = new YYARec(299,32);yyac++; 
					yya[yyac] = new YYARec(300,158);yyac++; 
					yya[yyac] = new YYARec(301,56);yyac++; 
					yya[yyac] = new YYARec(302,34);yyac++; 
					yya[yyac] = new YYARec(303,57);yyac++; 
					yya[yyac] = new YYARec(304,58);yyac++; 
					yya[yyac] = new YYARec(305,59);yyac++; 
					yya[yyac] = new YYARec(306,190);yyac++; 
					yya[yyac] = new YYARec(307,191);yyac++; 
					yya[yyac] = new YYARec(308,40);yyac++; 
					yya[yyac] = new YYARec(309,61);yyac++; 
					yya[yyac] = new YYARec(310,42);yyac++; 
					yya[yyac] = new YYARec(311,62);yyac++; 
					yya[yyac] = new YYARec(312,44);yyac++; 
					yya[yyac] = new YYARec(313,45);yyac++; 
					yya[yyac] = new YYARec(314,192);yyac++; 
					yya[yyac] = new YYARec(315,193);yyac++; 
					yya[yyac] = new YYARec(316,46);yyac++; 
					yya[yyac] = new YYARec(266,188);yyac++; 
					yya[yyac] = new YYARec(274,77);yyac++; 
					yya[yyac] = new YYARec(275,78);yyac++; 
					yya[yyac] = new YYARec(279,79);yyac++; 
					yya[yyac] = new YYARec(290,23);yyac++; 
					yya[yyac] = new YYARec(291,51);yyac++; 
					yya[yyac] = new YYARec(292,52);yyac++; 
					yya[yyac] = new YYARec(293,53);yyac++; 
					yya[yyac] = new YYARec(294,54);yyac++; 
					yya[yyac] = new YYARec(295,55);yyac++; 
					yya[yyac] = new YYARec(296,189);yyac++; 
					yya[yyac] = new YYARec(297,30);yyac++; 
					yya[yyac] = new YYARec(298,31);yyac++; 
					yya[yyac] = new YYARec(299,32);yyac++; 
					yya[yyac] = new YYARec(300,158);yyac++; 
					yya[yyac] = new YYARec(301,56);yyac++; 
					yya[yyac] = new YYARec(302,34);yyac++; 
					yya[yyac] = new YYARec(303,57);yyac++; 
					yya[yyac] = new YYARec(304,58);yyac++; 
					yya[yyac] = new YYARec(305,59);yyac++; 
					yya[yyac] = new YYARec(306,190);yyac++; 
					yya[yyac] = new YYARec(307,191);yyac++; 
					yya[yyac] = new YYARec(308,40);yyac++; 
					yya[yyac] = new YYARec(309,61);yyac++; 
					yya[yyac] = new YYARec(310,42);yyac++; 
					yya[yyac] = new YYARec(311,62);yyac++; 
					yya[yyac] = new YYARec(312,44);yyac++; 
					yya[yyac] = new YYARec(313,45);yyac++; 
					yya[yyac] = new YYARec(314,192);yyac++; 
					yya[yyac] = new YYARec(315,193);yyac++; 
					yya[yyac] = new YYARec(316,46);yyac++; 
					yya[yyac] = new YYARec(266,188);yyac++; 
					yya[yyac] = new YYARec(274,77);yyac++; 
					yya[yyac] = new YYARec(275,78);yyac++; 
					yya[yyac] = new YYARec(279,79);yyac++; 
					yya[yyac] = new YYARec(290,23);yyac++; 
					yya[yyac] = new YYARec(291,51);yyac++; 
					yya[yyac] = new YYARec(292,52);yyac++; 
					yya[yyac] = new YYARec(293,53);yyac++; 
					yya[yyac] = new YYARec(294,54);yyac++; 
					yya[yyac] = new YYARec(295,55);yyac++; 
					yya[yyac] = new YYARec(296,189);yyac++; 
					yya[yyac] = new YYARec(297,30);yyac++; 
					yya[yyac] = new YYARec(298,31);yyac++; 
					yya[yyac] = new YYARec(299,32);yyac++; 
					yya[yyac] = new YYARec(300,158);yyac++; 
					yya[yyac] = new YYARec(301,56);yyac++; 
					yya[yyac] = new YYARec(302,34);yyac++; 
					yya[yyac] = new YYARec(303,57);yyac++; 
					yya[yyac] = new YYARec(304,58);yyac++; 
					yya[yyac] = new YYARec(305,59);yyac++; 
					yya[yyac] = new YYARec(306,190);yyac++; 
					yya[yyac] = new YYARec(307,191);yyac++; 
					yya[yyac] = new YYARec(308,40);yyac++; 
					yya[yyac] = new YYARec(309,61);yyac++; 
					yya[yyac] = new YYARec(310,42);yyac++; 
					yya[yyac] = new YYARec(311,62);yyac++; 
					yya[yyac] = new YYARec(312,44);yyac++; 
					yya[yyac] = new YYARec(313,45);yyac++; 
					yya[yyac] = new YYARec(314,192);yyac++; 
					yya[yyac] = new YYARec(315,193);yyac++; 
					yya[yyac] = new YYARec(316,46);yyac++; 
					yya[yyac] = new YYARec(266,188);yyac++; 
					yya[yyac] = new YYARec(274,77);yyac++; 
					yya[yyac] = new YYARec(275,78);yyac++; 
					yya[yyac] = new YYARec(279,79);yyac++; 
					yya[yyac] = new YYARec(290,23);yyac++; 
					yya[yyac] = new YYARec(291,51);yyac++; 
					yya[yyac] = new YYARec(292,52);yyac++; 
					yya[yyac] = new YYARec(293,53);yyac++; 
					yya[yyac] = new YYARec(294,54);yyac++; 
					yya[yyac] = new YYARec(295,55);yyac++; 
					yya[yyac] = new YYARec(296,189);yyac++; 
					yya[yyac] = new YYARec(297,30);yyac++; 
					yya[yyac] = new YYARec(298,31);yyac++; 
					yya[yyac] = new YYARec(299,32);yyac++; 
					yya[yyac] = new YYARec(300,158);yyac++; 
					yya[yyac] = new YYARec(301,56);yyac++; 
					yya[yyac] = new YYARec(302,34);yyac++; 
					yya[yyac] = new YYARec(303,57);yyac++; 
					yya[yyac] = new YYARec(304,58);yyac++; 
					yya[yyac] = new YYARec(305,59);yyac++; 
					yya[yyac] = new YYARec(306,190);yyac++; 
					yya[yyac] = new YYARec(307,191);yyac++; 
					yya[yyac] = new YYARec(308,40);yyac++; 
					yya[yyac] = new YYARec(309,61);yyac++; 
					yya[yyac] = new YYARec(310,42);yyac++; 
					yya[yyac] = new YYARec(311,62);yyac++; 
					yya[yyac] = new YYARec(312,44);yyac++; 
					yya[yyac] = new YYARec(313,45);yyac++; 
					yya[yyac] = new YYARec(314,192);yyac++; 
					yya[yyac] = new YYARec(315,193);yyac++; 
					yya[yyac] = new YYARec(316,46);yyac++; 
					yya[yyac] = new YYARec(267,260);yyac++; 
					yya[yyac] = new YYARec(259,261);yyac++; 
					yya[yyac] = new YYARec(257,125);yyac++; 
					yya[yyac] = new YYARec(258,126);yyac++; 
					yya[yyac] = new YYARec(280,127);yyac++; 
					yya[yyac] = new YYARec(286,128);yyac++; 
					yya[yyac] = new YYARec(287,129);yyac++; 
					yya[yyac] = new YYARec(288,130);yyac++; 
					yya[yyac] = new YYARec(290,23);yyac++; 
					yya[yyac] = new YYARec(291,51);yyac++; 
					yya[yyac] = new YYARec(292,52);yyac++; 
					yya[yyac] = new YYARec(293,53);yyac++; 
					yya[yyac] = new YYARec(294,54);yyac++; 
					yya[yyac] = new YYARec(295,55);yyac++; 
					yya[yyac] = new YYARec(296,29);yyac++; 
					yya[yyac] = new YYARec(297,30);yyac++; 
					yya[yyac] = new YYARec(298,31);yyac++; 
					yya[yyac] = new YYARec(299,131);yyac++; 
					yya[yyac] = new YYARec(301,56);yyac++; 
					yya[yyac] = new YYARec(302,34);yyac++; 
					yya[yyac] = new YYARec(303,57);yyac++; 
					yya[yyac] = new YYARec(304,58);yyac++; 
					yya[yyac] = new YYARec(305,59);yyac++; 
					yya[yyac] = new YYARec(306,132);yyac++; 
					yya[yyac] = new YYARec(307,60);yyac++; 
					yya[yyac] = new YYARec(308,40);yyac++; 
					yya[yyac] = new YYARec(309,61);yyac++; 
					yya[yyac] = new YYARec(310,133);yyac++; 
					yya[yyac] = new YYARec(311,62);yyac++; 
					yya[yyac] = new YYARec(312,134);yyac++; 
					yya[yyac] = new YYARec(313,45);yyac++; 
					yya[yyac] = new YYARec(316,135);yyac++; 
					yya[yyac] = new YYARec(259,-54 );yyac++; 
					yya[yyac] = new YYARec(257,125);yyac++; 
					yya[yyac] = new YYARec(258,126);yyac++; 
					yya[yyac] = new YYARec(280,127);yyac++; 
					yya[yyac] = new YYARec(286,128);yyac++; 
					yya[yyac] = new YYARec(287,129);yyac++; 
					yya[yyac] = new YYARec(288,130);yyac++; 
					yya[yyac] = new YYARec(290,23);yyac++; 
					yya[yyac] = new YYARec(291,51);yyac++; 
					yya[yyac] = new YYARec(292,52);yyac++; 
					yya[yyac] = new YYARec(293,53);yyac++; 
					yya[yyac] = new YYARec(294,54);yyac++; 
					yya[yyac] = new YYARec(295,55);yyac++; 
					yya[yyac] = new YYARec(296,29);yyac++; 
					yya[yyac] = new YYARec(297,30);yyac++; 
					yya[yyac] = new YYARec(298,31);yyac++; 
					yya[yyac] = new YYARec(299,131);yyac++; 
					yya[yyac] = new YYARec(301,56);yyac++; 
					yya[yyac] = new YYARec(302,34);yyac++; 
					yya[yyac] = new YYARec(303,57);yyac++; 
					yya[yyac] = new YYARec(304,58);yyac++; 
					yya[yyac] = new YYARec(305,59);yyac++; 
					yya[yyac] = new YYARec(306,132);yyac++; 
					yya[yyac] = new YYARec(307,60);yyac++; 
					yya[yyac] = new YYARec(308,40);yyac++; 
					yya[yyac] = new YYARec(309,61);yyac++; 
					yya[yyac] = new YYARec(310,133);yyac++; 
					yya[yyac] = new YYARec(311,62);yyac++; 
					yya[yyac] = new YYARec(312,134);yyac++; 
					yya[yyac] = new YYARec(313,45);yyac++; 
					yya[yyac] = new YYARec(316,135);yyac++; 
					yya[yyac] = new YYARec(259,-54 );yyac++; 
					yya[yyac] = new YYARec(267,264);yyac++; 
					yya[yyac] = new YYARec(276,219);yyac++; 
					yya[yyac] = new YYARec(277,220);yyac++; 
					yya[yyac] = new YYARec(278,221);yyac++; 
					yya[yyac] = new YYARec(257,-83 );yyac++; 
					yya[yyac] = new YYARec(258,-83 );yyac++; 
					yya[yyac] = new YYARec(261,-83 );yyac++; 
					yya[yyac] = new YYARec(262,-83 );yyac++; 
					yya[yyac] = new YYARec(263,-83 );yyac++; 
					yya[yyac] = new YYARec(264,-83 );yyac++; 
					yya[yyac] = new YYARec(265,-83 );yyac++; 
					yya[yyac] = new YYARec(267,-83 );yyac++; 
					yya[yyac] = new YYARec(268,-83 );yyac++; 
					yya[yyac] = new YYARec(269,-83 );yyac++; 
					yya[yyac] = new YYARec(270,-83 );yyac++; 
					yya[yyac] = new YYARec(271,-83 );yyac++; 
					yya[yyac] = new YYARec(272,-83 );yyac++; 
					yya[yyac] = new YYARec(273,-83 );yyac++; 
					yya[yyac] = new YYARec(274,-83 );yyac++; 
					yya[yyac] = new YYARec(275,-83 );yyac++; 
					yya[yyac] = new YYARec(274,223);yyac++; 
					yya[yyac] = new YYARec(275,224);yyac++; 
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
					yya[yyac] = new YYARec(270,226);yyac++; 
					yya[yyac] = new YYARec(271,227);yyac++; 
					yya[yyac] = new YYARec(272,228);yyac++; 
					yya[yyac] = new YYARec(273,229);yyac++; 
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
					yya[yyac] = new YYARec(268,231);yyac++; 
					yya[yyac] = new YYARec(269,232);yyac++; 
					yya[yyac] = new YYARec(257,-76 );yyac++; 
					yya[yyac] = new YYARec(258,-76 );yyac++; 
					yya[yyac] = new YYARec(261,-76 );yyac++; 
					yya[yyac] = new YYARec(262,-76 );yyac++; 
					yya[yyac] = new YYARec(263,-76 );yyac++; 
					yya[yyac] = new YYARec(264,-76 );yyac++; 
					yya[yyac] = new YYARec(265,-76 );yyac++; 
					yya[yyac] = new YYARec(267,-76 );yyac++; 
					yya[yyac] = new YYARec(265,233);yyac++; 
					yya[yyac] = new YYARec(257,-74 );yyac++; 
					yya[yyac] = new YYARec(258,-74 );yyac++; 
					yya[yyac] = new YYARec(261,-74 );yyac++; 
					yya[yyac] = new YYARec(262,-74 );yyac++; 
					yya[yyac] = new YYARec(263,-74 );yyac++; 
					yya[yyac] = new YYARec(264,-74 );yyac++; 
					yya[yyac] = new YYARec(267,-74 );yyac++; 
					yya[yyac] = new YYARec(264,234);yyac++; 
					yya[yyac] = new YYARec(257,-72 );yyac++; 
					yya[yyac] = new YYARec(258,-72 );yyac++; 
					yya[yyac] = new YYARec(261,-72 );yyac++; 
					yya[yyac] = new YYARec(262,-72 );yyac++; 
					yya[yyac] = new YYARec(263,-72 );yyac++; 
					yya[yyac] = new YYARec(267,-72 );yyac++; 
					yya[yyac] = new YYARec(263,235);yyac++; 
					yya[yyac] = new YYARec(257,-70 );yyac++; 
					yya[yyac] = new YYARec(258,-70 );yyac++; 
					yya[yyac] = new YYARec(261,-70 );yyac++; 
					yya[yyac] = new YYARec(262,-70 );yyac++; 
					yya[yyac] = new YYARec(267,-70 );yyac++; 
					yya[yyac] = new YYARec(262,236);yyac++; 
					yya[yyac] = new YYARec(257,-68 );yyac++; 
					yya[yyac] = new YYARec(258,-68 );yyac++; 
					yya[yyac] = new YYARec(261,-68 );yyac++; 
					yya[yyac] = new YYARec(267,-68 );yyac++; 
					yya[yyac] = new YYARec(259,265);yyac++; 
					yya[yyac] = new YYARec(259,266);yyac++;

					yyg[yygc] = new YYARec(-68,1);yygc++; 
					yyg[yygc] = new YYARec(-67,2);yygc++; 
					yyg[yygc] = new YYARec(-64,3);yygc++; 
					yyg[yygc] = new YYARec(-33,4);yygc++; 
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
					yyg[yygc] = new YYARec(-69,48);yygc++; 
					yyg[yygc] = new YYARec(-68,1);yygc++; 
					yyg[yygc] = new YYARec(-67,2);yygc++; 
					yyg[yygc] = new YYARec(-64,49);yygc++; 
					yyg[yygc] = new YYARec(-34,50);yygc++; 
					yyg[yygc] = new YYARec(-17,8);yygc++; 
					yyg[yygc] = new YYARec(-69,64);yygc++; 
					yyg[yygc] = new YYARec(-68,1);yygc++; 
					yyg[yygc] = new YYARec(-67,2);yygc++; 
					yyg[yygc] = new YYARec(-64,49);yygc++; 
					yyg[yygc] = new YYARec(-17,8);yygc++; 
					yyg[yygc] = new YYARec(-15,65);yygc++; 
					yyg[yygc] = new YYARec(-69,66);yygc++; 
					yyg[yygc] = new YYARec(-68,1);yygc++; 
					yyg[yygc] = new YYARec(-67,2);yygc++; 
					yyg[yygc] = new YYARec(-64,49);yygc++; 
					yyg[yygc] = new YYARec(-21,67);yygc++; 
					yyg[yygc] = new YYARec(-17,8);yygc++; 
					yyg[yygc] = new YYARec(-66,69);yygc++; 
					yyg[yygc] = new YYARec(-60,70);yygc++; 
					yyg[yygc] = new YYARec(-24,71);yygc++; 
					yyg[yygc] = new YYARec(-19,72);yygc++; 
					yyg[yygc] = new YYARec(-18,73);yygc++; 
					yyg[yygc] = new YYARec(-17,74);yygc++; 
					yyg[yygc] = new YYARec(-16,75);yygc++; 
					yyg[yygc] = new YYARec(-13,76);yygc++; 
					yyg[yygc] = new YYARec(-69,64);yygc++; 
					yyg[yygc] = new YYARec(-68,1);yygc++; 
					yyg[yygc] = new YYARec(-67,2);yygc++; 
					yyg[yygc] = new YYARec(-64,49);yygc++; 
					yyg[yygc] = new YYARec(-17,8);yygc++; 
					yyg[yygc] = new YYARec(-15,84);yygc++; 
					yyg[yygc] = new YYARec(-11,85);yygc++; 
					yyg[yygc] = new YYARec(-68,1);yygc++; 
					yyg[yygc] = new YYARec(-67,2);yygc++; 
					yyg[yygc] = new YYARec(-64,3);yygc++; 
					yyg[yygc] = new YYARec(-33,4);yygc++; 
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
					yyg[yygc] = new YYARec(-3,86);yygc++; 
					yyg[yygc] = new YYARec(-65,87);yygc++; 
					yyg[yygc] = new YYARec(-29,88);yygc++; 
					yyg[yygc] = new YYARec(-27,89);yygc++; 
					yyg[yygc] = new YYARec(-20,90);yygc++; 
					yyg[yygc] = new YYARec(-16,107);yygc++; 
					yyg[yygc] = new YYARec(-19,110);yygc++; 
					yyg[yygc] = new YYARec(-69,64);yygc++; 
					yyg[yygc] = new YYARec(-68,1);yygc++; 
					yyg[yygc] = new YYARec(-67,2);yygc++; 
					yyg[yygc] = new YYARec(-64,49);yygc++; 
					yyg[yygc] = new YYARec(-17,8);yygc++; 
					yyg[yygc] = new YYARec(-15,84);yygc++; 
					yyg[yygc] = new YYARec(-11,114);yygc++; 
					yyg[yygc] = new YYARec(-68,1);yygc++; 
					yyg[yygc] = new YYARec(-67,2);yygc++; 
					yyg[yygc] = new YYARec(-64,116);yygc++; 
					yyg[yygc] = new YYARec(-41,117);yygc++; 
					yyg[yygc] = new YYARec(-39,118);yygc++; 
					yyg[yygc] = new YYARec(-38,119);yygc++; 
					yyg[yygc] = new YYARec(-37,120);yygc++; 
					yyg[yygc] = new YYARec(-36,121);yygc++; 
					yyg[yygc] = new YYARec(-35,122);yygc++; 
					yyg[yygc] = new YYARec(-17,123);yygc++; 
					yyg[yygc] = new YYARec(-5,124);yygc++; 
					yyg[yygc] = new YYARec(-29,139);yygc++; 
					yyg[yygc] = new YYARec(-28,140);yygc++; 
					yyg[yygc] = new YYARec(-27,89);yygc++; 
					yyg[yygc] = new YYARec(-26,141);yygc++; 
					yyg[yygc] = new YYARec(-20,90);yygc++; 
					yyg[yygc] = new YYARec(-60,143);yygc++; 
					yyg[yygc] = new YYARec(-24,144);yygc++; 
					yyg[yygc] = new YYARec(-23,145);yygc++; 
					yyg[yygc] = new YYARec(-22,146);yygc++; 
					yyg[yygc] = new YYARec(-68,1);yygc++; 
					yyg[yygc] = new YYARec(-67,2);yygc++; 
					yyg[yygc] = new YYARec(-66,69);yygc++; 
					yyg[yygc] = new YYARec(-64,3);yygc++; 
					yyg[yygc] = new YYARec(-60,148);yygc++; 
					yyg[yygc] = new YYARec(-43,149);yygc++; 
					yyg[yygc] = new YYARec(-42,150);yygc++; 
					yyg[yygc] = new YYARec(-40,151);yygc++; 
					yyg[yygc] = new YYARec(-32,152);yygc++; 
					yyg[yygc] = new YYARec(-24,71);yygc++; 
					yyg[yygc] = new YYARec(-19,153);yygc++; 
					yyg[yygc] = new YYARec(-18,154);yygc++; 
					yyg[yygc] = new YYARec(-17,8);yygc++; 
					yyg[yygc] = new YYARec(-16,155);yygc++; 
					yyg[yygc] = new YYARec(-5,156);yygc++; 
					yyg[yygc] = new YYARec(-68,1);yygc++; 
					yyg[yygc] = new YYARec(-67,2);yygc++; 
					yyg[yygc] = new YYARec(-64,116);yygc++; 
					yyg[yygc] = new YYARec(-41,117);yygc++; 
					yyg[yygc] = new YYARec(-39,118);yygc++; 
					yyg[yygc] = new YYARec(-38,119);yygc++; 
					yyg[yygc] = new YYARec(-37,120);yygc++; 
					yyg[yygc] = new YYARec(-36,121);yygc++; 
					yyg[yygc] = new YYARec(-35,159);yygc++; 
					yyg[yygc] = new YYARec(-17,123);yygc++; 
					yyg[yygc] = new YYARec(-5,124);yygc++; 
					yyg[yygc] = new YYARec(-68,1);yygc++; 
					yyg[yygc] = new YYARec(-67,2);yygc++; 
					yyg[yygc] = new YYARec(-64,116);yygc++; 
					yyg[yygc] = new YYARec(-41,117);yygc++; 
					yyg[yygc] = new YYARec(-39,118);yygc++; 
					yyg[yygc] = new YYARec(-38,119);yygc++; 
					yyg[yygc] = new YYARec(-37,120);yygc++; 
					yyg[yygc] = new YYARec(-36,121);yygc++; 
					yyg[yygc] = new YYARec(-35,160);yygc++; 
					yyg[yygc] = new YYARec(-17,123);yygc++; 
					yyg[yygc] = new YYARec(-5,124);yygc++; 
					yyg[yygc] = new YYARec(-68,1);yygc++; 
					yyg[yygc] = new YYARec(-67,2);yygc++; 
					yyg[yygc] = new YYARec(-66,69);yygc++; 
					yyg[yygc] = new YYARec(-64,3);yygc++; 
					yyg[yygc] = new YYARec(-60,148);yygc++; 
					yyg[yygc] = new YYARec(-43,149);yygc++; 
					yyg[yygc] = new YYARec(-42,150);yygc++; 
					yyg[yygc] = new YYARec(-40,163);yygc++; 
					yyg[yygc] = new YYARec(-32,152);yygc++; 
					yyg[yygc] = new YYARec(-24,71);yygc++; 
					yyg[yygc] = new YYARec(-19,153);yygc++; 
					yyg[yygc] = new YYARec(-18,154);yygc++; 
					yyg[yygc] = new YYARec(-17,8);yygc++; 
					yyg[yygc] = new YYARec(-16,155);yygc++; 
					yyg[yygc] = new YYARec(-5,156);yygc++; 
					yyg[yygc] = new YYARec(-63,164);yygc++; 
					yyg[yygc] = new YYARec(-68,1);yygc++; 
					yyg[yygc] = new YYARec(-67,2);yygc++; 
					yyg[yygc] = new YYARec(-64,116);yygc++; 
					yyg[yygc] = new YYARec(-41,117);yygc++; 
					yyg[yygc] = new YYARec(-39,118);yygc++; 
					yyg[yygc] = new YYARec(-38,119);yygc++; 
					yyg[yygc] = new YYARec(-37,120);yygc++; 
					yyg[yygc] = new YYARec(-36,121);yygc++; 
					yyg[yygc] = new YYARec(-35,170);yygc++; 
					yyg[yygc] = new YYARec(-17,123);yygc++; 
					yyg[yygc] = new YYARec(-5,124);yygc++; 
					yyg[yygc] = new YYARec(-68,1);yygc++; 
					yyg[yygc] = new YYARec(-67,2);yygc++; 
					yyg[yygc] = new YYARec(-64,3);yygc++; 
					yyg[yygc] = new YYARec(-62,171);yygc++; 
					yyg[yygc] = new YYARec(-61,172);yygc++; 
					yyg[yygc] = new YYARec(-60,173);yygc++; 
					yyg[yygc] = new YYARec(-59,174);yygc++; 
					yyg[yygc] = new YYARec(-57,175);yygc++; 
					yyg[yygc] = new YYARec(-55,176);yygc++; 
					yyg[yygc] = new YYARec(-53,177);yygc++; 
					yyg[yygc] = new YYARec(-51,178);yygc++; 
					yyg[yygc] = new YYARec(-50,179);yygc++; 
					yyg[yygc] = new YYARec(-49,180);yygc++; 
					yyg[yygc] = new YYARec(-48,181);yygc++; 
					yyg[yygc] = new YYARec(-47,182);yygc++; 
					yyg[yygc] = new YYARec(-46,183);yygc++; 
					yyg[yygc] = new YYARec(-45,184);yygc++; 
					yyg[yygc] = new YYARec(-44,185);yygc++; 
					yyg[yygc] = new YYARec(-43,186);yygc++; 
					yyg[yygc] = new YYARec(-17,8);yygc++; 
					yyg[yygc] = new YYARec(-5,187);yygc++; 
					yyg[yygc] = new YYARec(-68,1);yygc++; 
					yyg[yygc] = new YYARec(-67,2);yygc++; 
					yyg[yygc] = new YYARec(-64,3);yygc++; 
					yyg[yygc] = new YYARec(-62,171);yygc++; 
					yyg[yygc] = new YYARec(-61,172);yygc++; 
					yyg[yygc] = new YYARec(-60,173);yygc++; 
					yyg[yygc] = new YYARec(-59,174);yygc++; 
					yyg[yygc] = new YYARec(-57,175);yygc++; 
					yyg[yygc] = new YYARec(-55,176);yygc++; 
					yyg[yygc] = new YYARec(-53,177);yygc++; 
					yyg[yygc] = new YYARec(-51,178);yygc++; 
					yyg[yygc] = new YYARec(-50,179);yygc++; 
					yyg[yygc] = new YYARec(-49,180);yygc++; 
					yyg[yygc] = new YYARec(-48,181);yygc++; 
					yyg[yygc] = new YYARec(-47,182);yygc++; 
					yyg[yygc] = new YYARec(-46,183);yygc++; 
					yyg[yygc] = new YYARec(-45,184);yygc++; 
					yyg[yygc] = new YYARec(-44,195);yygc++; 
					yyg[yygc] = new YYARec(-43,186);yygc++; 
					yyg[yygc] = new YYARec(-17,8);yygc++; 
					yyg[yygc] = new YYARec(-5,196);yygc++; 
					yyg[yygc] = new YYARec(-68,1);yygc++; 
					yyg[yygc] = new YYARec(-67,2);yygc++; 
					yyg[yygc] = new YYARec(-64,3);yygc++; 
					yyg[yygc] = new YYARec(-62,171);yygc++; 
					yyg[yygc] = new YYARec(-61,172);yygc++; 
					yyg[yygc] = new YYARec(-60,173);yygc++; 
					yyg[yygc] = new YYARec(-59,174);yygc++; 
					yyg[yygc] = new YYARec(-57,175);yygc++; 
					yyg[yygc] = new YYARec(-55,176);yygc++; 
					yyg[yygc] = new YYARec(-53,177);yygc++; 
					yyg[yygc] = new YYARec(-51,178);yygc++; 
					yyg[yygc] = new YYARec(-50,179);yygc++; 
					yyg[yygc] = new YYARec(-49,180);yygc++; 
					yyg[yygc] = new YYARec(-48,181);yygc++; 
					yyg[yygc] = new YYARec(-47,182);yygc++; 
					yyg[yygc] = new YYARec(-46,183);yygc++; 
					yyg[yygc] = new YYARec(-45,184);yygc++; 
					yyg[yygc] = new YYARec(-44,197);yygc++; 
					yyg[yygc] = new YYARec(-43,186);yygc++; 
					yyg[yygc] = new YYARec(-17,8);yygc++; 
					yyg[yygc] = new YYARec(-5,196);yygc++; 
					yyg[yygc] = new YYARec(-68,1);yygc++; 
					yyg[yygc] = new YYARec(-67,2);yygc++; 
					yyg[yygc] = new YYARec(-66,69);yygc++; 
					yyg[yygc] = new YYARec(-64,3);yygc++; 
					yyg[yygc] = new YYARec(-60,148);yygc++; 
					yyg[yygc] = new YYARec(-32,199);yygc++; 
					yyg[yygc] = new YYARec(-31,200);yygc++; 
					yyg[yygc] = new YYARec(-30,201);yygc++; 
					yyg[yygc] = new YYARec(-24,71);yygc++; 
					yyg[yygc] = new YYARec(-19,202);yygc++; 
					yyg[yygc] = new YYARec(-18,203);yygc++; 
					yyg[yygc] = new YYARec(-17,8);yygc++; 
					yyg[yygc] = new YYARec(-16,204);yygc++; 
					yyg[yygc] = new YYARec(-5,156);yygc++; 
					yyg[yygc] = new YYARec(-29,139);yygc++; 
					yyg[yygc] = new YYARec(-28,140);yygc++; 
					yyg[yygc] = new YYARec(-27,89);yygc++; 
					yyg[yygc] = new YYARec(-26,205);yygc++; 
					yyg[yygc] = new YYARec(-20,90);yygc++; 
					yyg[yygc] = new YYARec(-60,143);yygc++; 
					yyg[yygc] = new YYARec(-24,144);yygc++; 
					yyg[yygc] = new YYARec(-23,145);yygc++; 
					yyg[yygc] = new YYARec(-22,207);yygc++; 
					yyg[yygc] = new YYARec(-68,1);yygc++; 
					yyg[yygc] = new YYARec(-67,2);yygc++; 
					yyg[yygc] = new YYARec(-64,3);yygc++; 
					yyg[yygc] = new YYARec(-17,8);yygc++; 
					yyg[yygc] = new YYARec(-5,209);yygc++; 
					yyg[yygc] = new YYARec(-68,1);yygc++; 
					yyg[yygc] = new YYARec(-67,2);yygc++; 
					yyg[yygc] = new YYARec(-66,69);yygc++; 
					yyg[yygc] = new YYARec(-64,3);yygc++; 
					yyg[yygc] = new YYARec(-60,148);yygc++; 
					yyg[yygc] = new YYARec(-43,149);yygc++; 
					yyg[yygc] = new YYARec(-42,150);yygc++; 
					yyg[yygc] = new YYARec(-40,210);yygc++; 
					yyg[yygc] = new YYARec(-32,152);yygc++; 
					yyg[yygc] = new YYARec(-24,71);yygc++; 
					yyg[yygc] = new YYARec(-19,153);yygc++; 
					yyg[yygc] = new YYARec(-18,154);yygc++; 
					yyg[yygc] = new YYARec(-17,8);yygc++; 
					yyg[yygc] = new YYARec(-16,155);yygc++; 
					yyg[yygc] = new YYARec(-5,156);yygc++; 
					yyg[yygc] = new YYARec(-68,1);yygc++; 
					yyg[yygc] = new YYARec(-67,2);yygc++; 
					yyg[yygc] = new YYARec(-64,116);yygc++; 
					yyg[yygc] = new YYARec(-41,117);yygc++; 
					yyg[yygc] = new YYARec(-39,118);yygc++; 
					yyg[yygc] = new YYARec(-38,119);yygc++; 
					yyg[yygc] = new YYARec(-37,120);yygc++; 
					yyg[yygc] = new YYARec(-36,121);yygc++; 
					yyg[yygc] = new YYARec(-35,212);yygc++; 
					yyg[yygc] = new YYARec(-17,123);yygc++; 
					yyg[yygc] = new YYARec(-5,124);yygc++; 
					yyg[yygc] = new YYARec(-68,1);yygc++; 
					yyg[yygc] = new YYARec(-67,2);yygc++; 
					yyg[yygc] = new YYARec(-64,3);yygc++; 
					yyg[yygc] = new YYARec(-62,171);yygc++; 
					yyg[yygc] = new YYARec(-61,172);yygc++; 
					yyg[yygc] = new YYARec(-60,173);yygc++; 
					yyg[yygc] = new YYARec(-59,174);yygc++; 
					yyg[yygc] = new YYARec(-57,175);yygc++; 
					yyg[yygc] = new YYARec(-55,176);yygc++; 
					yyg[yygc] = new YYARec(-53,177);yygc++; 
					yyg[yygc] = new YYARec(-51,178);yygc++; 
					yyg[yygc] = new YYARec(-50,179);yygc++; 
					yyg[yygc] = new YYARec(-49,180);yygc++; 
					yyg[yygc] = new YYARec(-48,181);yygc++; 
					yyg[yygc] = new YYARec(-47,182);yygc++; 
					yyg[yygc] = new YYARec(-46,183);yygc++; 
					yyg[yygc] = new YYARec(-45,184);yygc++; 
					yyg[yygc] = new YYARec(-44,214);yygc++; 
					yyg[yygc] = new YYARec(-43,186);yygc++; 
					yyg[yygc] = new YYARec(-17,8);yygc++; 
					yyg[yygc] = new YYARec(-5,196);yygc++; 
					yyg[yygc] = new YYARec(-68,1);yygc++; 
					yyg[yygc] = new YYARec(-67,2);yygc++; 
					yyg[yygc] = new YYARec(-64,3);yygc++; 
					yyg[yygc] = new YYARec(-62,171);yygc++; 
					yyg[yygc] = new YYARec(-61,172);yygc++; 
					yyg[yygc] = new YYARec(-60,173);yygc++; 
					yyg[yygc] = new YYARec(-59,174);yygc++; 
					yyg[yygc] = new YYARec(-57,217);yygc++; 
					yyg[yygc] = new YYARec(-43,186);yygc++; 
					yyg[yygc] = new YYARec(-17,8);yygc++; 
					yyg[yygc] = new YYARec(-5,196);yygc++; 
					yyg[yygc] = new YYARec(-58,218);yygc++; 
					yyg[yygc] = new YYARec(-56,222);yygc++; 
					yyg[yygc] = new YYARec(-54,225);yygc++; 
					yyg[yygc] = new YYARec(-52,230);yygc++; 
					yyg[yygc] = new YYARec(-63,238);yygc++; 
					yyg[yygc] = new YYARec(-68,1);yygc++; 
					yyg[yygc] = new YYARec(-67,2);yygc++; 
					yyg[yygc] = new YYARec(-64,3);yygc++; 
					yyg[yygc] = new YYARec(-62,171);yygc++; 
					yyg[yygc] = new YYARec(-61,172);yygc++; 
					yyg[yygc] = new YYARec(-60,173);yygc++; 
					yyg[yygc] = new YYARec(-59,174);yygc++; 
					yyg[yygc] = new YYARec(-57,175);yygc++; 
					yyg[yygc] = new YYARec(-55,176);yygc++; 
					yyg[yygc] = new YYARec(-53,177);yygc++; 
					yyg[yygc] = new YYARec(-51,178);yygc++; 
					yyg[yygc] = new YYARec(-50,179);yygc++; 
					yyg[yygc] = new YYARec(-49,180);yygc++; 
					yyg[yygc] = new YYARec(-48,181);yygc++; 
					yyg[yygc] = new YYARec(-47,182);yygc++; 
					yyg[yygc] = new YYARec(-46,183);yygc++; 
					yyg[yygc] = new YYARec(-45,184);yygc++; 
					yyg[yygc] = new YYARec(-44,240);yygc++; 
					yyg[yygc] = new YYARec(-43,186);yygc++; 
					yyg[yygc] = new YYARec(-17,8);yygc++; 
					yyg[yygc] = new YYARec(-5,196);yygc++; 
					yyg[yygc] = new YYARec(-68,1);yygc++; 
					yyg[yygc] = new YYARec(-67,2);yygc++; 
					yyg[yygc] = new YYARec(-64,116);yygc++; 
					yyg[yygc] = new YYARec(-41,117);yygc++; 
					yyg[yygc] = new YYARec(-39,118);yygc++; 
					yyg[yygc] = new YYARec(-38,119);yygc++; 
					yyg[yygc] = new YYARec(-37,120);yygc++; 
					yyg[yygc] = new YYARec(-36,121);yygc++; 
					yyg[yygc] = new YYARec(-35,242);yygc++; 
					yyg[yygc] = new YYARec(-17,123);yygc++; 
					yyg[yygc] = new YYARec(-5,124);yygc++; 
					yyg[yygc] = new YYARec(-68,1);yygc++; 
					yyg[yygc] = new YYARec(-67,2);yygc++; 
					yyg[yygc] = new YYARec(-64,116);yygc++; 
					yyg[yygc] = new YYARec(-41,117);yygc++; 
					yyg[yygc] = new YYARec(-39,118);yygc++; 
					yyg[yygc] = new YYARec(-38,119);yygc++; 
					yyg[yygc] = new YYARec(-37,120);yygc++; 
					yyg[yygc] = new YYARec(-36,121);yygc++; 
					yyg[yygc] = new YYARec(-35,245);yygc++; 
					yyg[yygc] = new YYARec(-17,123);yygc++; 
					yyg[yygc] = new YYARec(-5,124);yygc++; 
					yyg[yygc] = new YYARec(-68,1);yygc++; 
					yyg[yygc] = new YYARec(-67,2);yygc++; 
					yyg[yygc] = new YYARec(-66,69);yygc++; 
					yyg[yygc] = new YYARec(-64,3);yygc++; 
					yyg[yygc] = new YYARec(-60,148);yygc++; 
					yyg[yygc] = new YYARec(-32,199);yygc++; 
					yyg[yygc] = new YYARec(-31,200);yygc++; 
					yyg[yygc] = new YYARec(-30,246);yygc++; 
					yyg[yygc] = new YYARec(-24,71);yygc++; 
					yyg[yygc] = new YYARec(-19,202);yygc++; 
					yyg[yygc] = new YYARec(-18,203);yygc++; 
					yyg[yygc] = new YYARec(-17,8);yygc++; 
					yyg[yygc] = new YYARec(-16,204);yygc++; 
					yyg[yygc] = new YYARec(-5,156);yygc++; 
					yyg[yygc] = new YYARec(-68,1);yygc++; 
					yyg[yygc] = new YYARec(-67,2);yygc++; 
					yyg[yygc] = new YYARec(-64,116);yygc++; 
					yyg[yygc] = new YYARec(-41,117);yygc++; 
					yyg[yygc] = new YYARec(-39,118);yygc++; 
					yyg[yygc] = new YYARec(-38,119);yygc++; 
					yyg[yygc] = new YYARec(-37,120);yygc++; 
					yyg[yygc] = new YYARec(-36,121);yygc++; 
					yyg[yygc] = new YYARec(-35,248);yygc++; 
					yyg[yygc] = new YYARec(-17,123);yygc++; 
					yyg[yygc] = new YYARec(-5,124);yygc++; 
					yyg[yygc] = new YYARec(-68,1);yygc++; 
					yyg[yygc] = new YYARec(-67,2);yygc++; 
					yyg[yygc] = new YYARec(-64,3);yygc++; 
					yyg[yygc] = new YYARec(-62,171);yygc++; 
					yyg[yygc] = new YYARec(-61,172);yygc++; 
					yyg[yygc] = new YYARec(-60,173);yygc++; 
					yyg[yygc] = new YYARec(-59,174);yygc++; 
					yyg[yygc] = new YYARec(-57,175);yygc++; 
					yyg[yygc] = new YYARec(-55,176);yygc++; 
					yyg[yygc] = new YYARec(-53,177);yygc++; 
					yyg[yygc] = new YYARec(-51,178);yygc++; 
					yyg[yygc] = new YYARec(-50,179);yygc++; 
					yyg[yygc] = new YYARec(-49,180);yygc++; 
					yyg[yygc] = new YYARec(-48,181);yygc++; 
					yyg[yygc] = new YYARec(-47,182);yygc++; 
					yyg[yygc] = new YYARec(-46,183);yygc++; 
					yyg[yygc] = new YYARec(-45,184);yygc++; 
					yyg[yygc] = new YYARec(-44,249);yygc++; 
					yyg[yygc] = new YYARec(-43,186);yygc++; 
					yyg[yygc] = new YYARec(-17,8);yygc++; 
					yyg[yygc] = new YYARec(-5,196);yygc++; 
					yyg[yygc] = new YYARec(-68,1);yygc++; 
					yyg[yygc] = new YYARec(-67,2);yygc++; 
					yyg[yygc] = new YYARec(-64,3);yygc++; 
					yyg[yygc] = new YYARec(-62,171);yygc++; 
					yyg[yygc] = new YYARec(-61,172);yygc++; 
					yyg[yygc] = new YYARec(-60,173);yygc++; 
					yyg[yygc] = new YYARec(-59,174);yygc++; 
					yyg[yygc] = new YYARec(-57,250);yygc++; 
					yyg[yygc] = new YYARec(-43,186);yygc++; 
					yyg[yygc] = new YYARec(-17,8);yygc++; 
					yyg[yygc] = new YYARec(-5,196);yygc++; 
					yyg[yygc] = new YYARec(-68,1);yygc++; 
					yyg[yygc] = new YYARec(-67,2);yygc++; 
					yyg[yygc] = new YYARec(-64,3);yygc++; 
					yyg[yygc] = new YYARec(-62,171);yygc++; 
					yyg[yygc] = new YYARec(-61,172);yygc++; 
					yyg[yygc] = new YYARec(-60,173);yygc++; 
					yyg[yygc] = new YYARec(-59,174);yygc++; 
					yyg[yygc] = new YYARec(-57,175);yygc++; 
					yyg[yygc] = new YYARec(-55,251);yygc++; 
					yyg[yygc] = new YYARec(-43,186);yygc++; 
					yyg[yygc] = new YYARec(-17,8);yygc++; 
					yyg[yygc] = new YYARec(-5,196);yygc++; 
					yyg[yygc] = new YYARec(-68,1);yygc++; 
					yyg[yygc] = new YYARec(-67,2);yygc++; 
					yyg[yygc] = new YYARec(-64,3);yygc++; 
					yyg[yygc] = new YYARec(-62,171);yygc++; 
					yyg[yygc] = new YYARec(-61,172);yygc++; 
					yyg[yygc] = new YYARec(-60,173);yygc++; 
					yyg[yygc] = new YYARec(-59,174);yygc++; 
					yyg[yygc] = new YYARec(-57,175);yygc++; 
					yyg[yygc] = new YYARec(-55,176);yygc++; 
					yyg[yygc] = new YYARec(-53,252);yygc++; 
					yyg[yygc] = new YYARec(-43,186);yygc++; 
					yyg[yygc] = new YYARec(-17,8);yygc++; 
					yyg[yygc] = new YYARec(-5,196);yygc++; 
					yyg[yygc] = new YYARec(-68,1);yygc++; 
					yyg[yygc] = new YYARec(-67,2);yygc++; 
					yyg[yygc] = new YYARec(-64,3);yygc++; 
					yyg[yygc] = new YYARec(-62,171);yygc++; 
					yyg[yygc] = new YYARec(-61,172);yygc++; 
					yyg[yygc] = new YYARec(-60,173);yygc++; 
					yyg[yygc] = new YYARec(-59,174);yygc++; 
					yyg[yygc] = new YYARec(-57,175);yygc++; 
					yyg[yygc] = new YYARec(-55,176);yygc++; 
					yyg[yygc] = new YYARec(-53,177);yygc++; 
					yyg[yygc] = new YYARec(-51,253);yygc++; 
					yyg[yygc] = new YYARec(-43,186);yygc++; 
					yyg[yygc] = new YYARec(-17,8);yygc++; 
					yyg[yygc] = new YYARec(-5,196);yygc++; 
					yyg[yygc] = new YYARec(-68,1);yygc++; 
					yyg[yygc] = new YYARec(-67,2);yygc++; 
					yyg[yygc] = new YYARec(-64,3);yygc++; 
					yyg[yygc] = new YYARec(-62,171);yygc++; 
					yyg[yygc] = new YYARec(-61,172);yygc++; 
					yyg[yygc] = new YYARec(-60,173);yygc++; 
					yyg[yygc] = new YYARec(-59,174);yygc++; 
					yyg[yygc] = new YYARec(-57,175);yygc++; 
					yyg[yygc] = new YYARec(-55,176);yygc++; 
					yyg[yygc] = new YYARec(-53,177);yygc++; 
					yyg[yygc] = new YYARec(-51,178);yygc++; 
					yyg[yygc] = new YYARec(-50,254);yygc++; 
					yyg[yygc] = new YYARec(-43,186);yygc++; 
					yyg[yygc] = new YYARec(-17,8);yygc++; 
					yyg[yygc] = new YYARec(-5,196);yygc++; 
					yyg[yygc] = new YYARec(-68,1);yygc++; 
					yyg[yygc] = new YYARec(-67,2);yygc++; 
					yyg[yygc] = new YYARec(-64,3);yygc++; 
					yyg[yygc] = new YYARec(-62,171);yygc++; 
					yyg[yygc] = new YYARec(-61,172);yygc++; 
					yyg[yygc] = new YYARec(-60,173);yygc++; 
					yyg[yygc] = new YYARec(-59,174);yygc++; 
					yyg[yygc] = new YYARec(-57,175);yygc++; 
					yyg[yygc] = new YYARec(-55,176);yygc++; 
					yyg[yygc] = new YYARec(-53,177);yygc++; 
					yyg[yygc] = new YYARec(-51,178);yygc++; 
					yyg[yygc] = new YYARec(-50,179);yygc++; 
					yyg[yygc] = new YYARec(-49,255);yygc++; 
					yyg[yygc] = new YYARec(-43,186);yygc++; 
					yyg[yygc] = new YYARec(-17,8);yygc++; 
					yyg[yygc] = new YYARec(-5,196);yygc++; 
					yyg[yygc] = new YYARec(-68,1);yygc++; 
					yyg[yygc] = new YYARec(-67,2);yygc++; 
					yyg[yygc] = new YYARec(-64,3);yygc++; 
					yyg[yygc] = new YYARec(-62,171);yygc++; 
					yyg[yygc] = new YYARec(-61,172);yygc++; 
					yyg[yygc] = new YYARec(-60,173);yygc++; 
					yyg[yygc] = new YYARec(-59,174);yygc++; 
					yyg[yygc] = new YYARec(-57,175);yygc++; 
					yyg[yygc] = new YYARec(-55,176);yygc++; 
					yyg[yygc] = new YYARec(-53,177);yygc++; 
					yyg[yygc] = new YYARec(-51,178);yygc++; 
					yyg[yygc] = new YYARec(-50,179);yygc++; 
					yyg[yygc] = new YYARec(-49,180);yygc++; 
					yyg[yygc] = new YYARec(-48,256);yygc++; 
					yyg[yygc] = new YYARec(-43,186);yygc++; 
					yyg[yygc] = new YYARec(-17,8);yygc++; 
					yyg[yygc] = new YYARec(-5,196);yygc++; 
					yyg[yygc] = new YYARec(-68,1);yygc++; 
					yyg[yygc] = new YYARec(-67,2);yygc++; 
					yyg[yygc] = new YYARec(-64,3);yygc++; 
					yyg[yygc] = new YYARec(-62,171);yygc++; 
					yyg[yygc] = new YYARec(-61,172);yygc++; 
					yyg[yygc] = new YYARec(-60,173);yygc++; 
					yyg[yygc] = new YYARec(-59,174);yygc++; 
					yyg[yygc] = new YYARec(-57,175);yygc++; 
					yyg[yygc] = new YYARec(-55,176);yygc++; 
					yyg[yygc] = new YYARec(-53,177);yygc++; 
					yyg[yygc] = new YYARec(-51,178);yygc++; 
					yyg[yygc] = new YYARec(-50,179);yygc++; 
					yyg[yygc] = new YYARec(-49,180);yygc++; 
					yyg[yygc] = new YYARec(-48,181);yygc++; 
					yyg[yygc] = new YYARec(-47,257);yygc++; 
					yyg[yygc] = new YYARec(-43,186);yygc++; 
					yyg[yygc] = new YYARec(-17,8);yygc++; 
					yyg[yygc] = new YYARec(-5,196);yygc++; 
					yyg[yygc] = new YYARec(-68,1);yygc++; 
					yyg[yygc] = new YYARec(-67,2);yygc++; 
					yyg[yygc] = new YYARec(-64,3);yygc++; 
					yyg[yygc] = new YYARec(-62,171);yygc++; 
					yyg[yygc] = new YYARec(-61,172);yygc++; 
					yyg[yygc] = new YYARec(-60,173);yygc++; 
					yyg[yygc] = new YYARec(-59,174);yygc++; 
					yyg[yygc] = new YYARec(-57,175);yygc++; 
					yyg[yygc] = new YYARec(-55,176);yygc++; 
					yyg[yygc] = new YYARec(-53,177);yygc++; 
					yyg[yygc] = new YYARec(-51,178);yygc++; 
					yyg[yygc] = new YYARec(-50,179);yygc++; 
					yyg[yygc] = new YYARec(-49,180);yygc++; 
					yyg[yygc] = new YYARec(-48,181);yygc++; 
					yyg[yygc] = new YYARec(-47,182);yygc++; 
					yyg[yygc] = new YYARec(-46,258);yygc++; 
					yyg[yygc] = new YYARec(-43,186);yygc++; 
					yyg[yygc] = new YYARec(-17,8);yygc++; 
					yyg[yygc] = new YYARec(-5,196);yygc++; 
					yyg[yygc] = new YYARec(-68,1);yygc++; 
					yyg[yygc] = new YYARec(-67,2);yygc++; 
					yyg[yygc] = new YYARec(-64,3);yygc++; 
					yyg[yygc] = new YYARec(-62,171);yygc++; 
					yyg[yygc] = new YYARec(-61,172);yygc++; 
					yyg[yygc] = new YYARec(-60,173);yygc++; 
					yyg[yygc] = new YYARec(-59,174);yygc++; 
					yyg[yygc] = new YYARec(-57,175);yygc++; 
					yyg[yygc] = new YYARec(-55,176);yygc++; 
					yyg[yygc] = new YYARec(-53,177);yygc++; 
					yyg[yygc] = new YYARec(-51,178);yygc++; 
					yyg[yygc] = new YYARec(-50,179);yygc++; 
					yyg[yygc] = new YYARec(-49,180);yygc++; 
					yyg[yygc] = new YYARec(-48,181);yygc++; 
					yyg[yygc] = new YYARec(-47,182);yygc++; 
					yyg[yygc] = new YYARec(-46,183);yygc++; 
					yyg[yygc] = new YYARec(-45,184);yygc++; 
					yyg[yygc] = new YYARec(-44,259);yygc++; 
					yyg[yygc] = new YYARec(-43,186);yygc++; 
					yyg[yygc] = new YYARec(-17,8);yygc++; 
					yyg[yygc] = new YYARec(-5,196);yygc++; 
					yyg[yygc] = new YYARec(-68,1);yygc++; 
					yyg[yygc] = new YYARec(-67,2);yygc++; 
					yyg[yygc] = new YYARec(-64,116);yygc++; 
					yyg[yygc] = new YYARec(-41,117);yygc++; 
					yyg[yygc] = new YYARec(-39,118);yygc++; 
					yyg[yygc] = new YYARec(-38,119);yygc++; 
					yyg[yygc] = new YYARec(-37,120);yygc++; 
					yyg[yygc] = new YYARec(-36,121);yygc++; 
					yyg[yygc] = new YYARec(-35,262);yygc++; 
					yyg[yygc] = new YYARec(-17,123);yygc++; 
					yyg[yygc] = new YYARec(-5,124);yygc++; 
					yyg[yygc] = new YYARec(-68,1);yygc++; 
					yyg[yygc] = new YYARec(-67,2);yygc++; 
					yyg[yygc] = new YYARec(-64,116);yygc++; 
					yyg[yygc] = new YYARec(-41,117);yygc++; 
					yyg[yygc] = new YYARec(-39,118);yygc++; 
					yyg[yygc] = new YYARec(-38,119);yygc++; 
					yyg[yygc] = new YYARec(-37,120);yygc++; 
					yyg[yygc] = new YYARec(-36,121);yygc++; 
					yyg[yygc] = new YYARec(-35,263);yygc++; 
					yyg[yygc] = new YYARec(-17,123);yygc++; 
					yyg[yygc] = new YYARec(-5,124);yygc++; 
					yyg[yygc] = new YYARec(-58,218);yygc++; 
					yyg[yygc] = new YYARec(-56,222);yygc++; 
					yyg[yygc] = new YYARec(-54,225);yygc++; 
					yyg[yygc] = new YYARec(-52,230);yygc++;

					yyd = new int[yynstates];
					yyd[0] = 0;  
					yyd[1] = -185;  
					yyd[2] = -184;  
					yyd[3] = 0;  
					yyd[4] = 0;  
					yyd[5] = -36;  
					yyd[6] = 0;  
					yyd[7] = 0;  
					yyd[8] = -183;  
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
					yyd[23] = -182;  
					yyd[24] = 0;  
					yyd[25] = 0;  
					yyd[26] = 0;  
					yyd[27] = 0;  
					yyd[28] = 0;  
					yyd[29] = -172;  
					yyd[30] = -169;  
					yyd[31] = -174;  
					yyd[32] = -167;  
					yyd[33] = 0;  
					yyd[34] = -181;  
					yyd[35] = 0;  
					yyd[36] = 0;  
					yyd[37] = 0;  
					yyd[38] = -164;  
					yyd[39] = 0;  
					yyd[40] = -179;  
					yyd[41] = 0;  
					yyd[42] = -160;  
					yyd[43] = 0;  
					yyd[44] = -161;  
					yyd[45] = -177;  
					yyd[46] = -155;  
					yyd[47] = 0;  
					yyd[48] = -190;  
					yyd[49] = -188;  
					yyd[50] = 0;  
					yyd[51] = -168;  
					yyd[52] = -171;  
					yyd[53] = -166;  
					yyd[54] = -173;  
					yyd[55] = -170;  
					yyd[56] = -180;  
					yyd[57] = -163;  
					yyd[58] = -162;  
					yyd[59] = -165;  
					yyd[60] = -176;  
					yyd[61] = -178;  
					yyd[62] = -175;  
					yyd[63] = 0;  
					yyd[64] = -189;  
					yyd[65] = 0;  
					yyd[66] = -192;  
					yyd[67] = 0;  
					yyd[68] = -191;  
					yyd[69] = -147;  
					yyd[70] = 0;  
					yyd[71] = -148;  
					yyd[72] = -25;  
					yyd[73] = -24;  
					yyd[74] = -23;  
					yyd[75] = -22;  
					yyd[76] = 0;  
					yyd[77] = -107;  
					yyd[78] = -108;  
					yyd[79] = -106;  
					yyd[80] = -152;  
					yyd[81] = -154;  
					yyd[82] = -194;  
					yyd[83] = -195;  
					yyd[84] = 0;  
					yyd[85] = 0;  
					yyd[86] = -2;  
					yyd[87] = -123;  
					yyd[88] = -124;  
					yyd[89] = -140;  
					yyd[90] = -141;  
					yyd[91] = -30;  
					yyd[92] = -34;  
					yyd[93] = -132;  
					yyd[94] = -139;  
					yyd[95] = -135;  
					yyd[96] = -134;  
					yyd[97] = -129;  
					yyd[98] = -137;  
					yyd[99] = -131;  
					yyd[100] = -138;  
					yyd[101] = -128;  
					yyd[102] = -136;  
					yyd[103] = -133;  
					yyd[104] = -130;  
					yyd[105] = 0;  
					yyd[106] = 0;  
					yyd[107] = 0;  
					yyd[108] = -33;  
					yyd[109] = 0;  
					yyd[110] = 0;  
					yyd[111] = -151;  
					yyd[112] = -153;  
					yyd[113] = -13;  
					yyd[114] = -21;  
					yyd[115] = -12;  
					yyd[116] = 0;  
					yyd[117] = 0;  
					yyd[118] = 0;  
					yyd[119] = 0;  
					yyd[120] = 0;  
					yyd[121] = 0;  
					yyd[122] = 0;  
					yyd[123] = 0;  
					yyd[124] = 0;  
					yyd[125] = -59;  
					yyd[126] = 0;  
					yyd[127] = 0;  
					yyd[128] = 0;  
					yyd[129] = 0;  
					yyd[130] = 0;  
					yyd[131] = 0;  
					yyd[132] = 0;  
					yyd[133] = 0;  
					yyd[134] = 0;  
					yyd[135] = 0;  
					yyd[136] = -187;  
					yyd[137] = -186;  
					yyd[138] = -32;  
					yyd[139] = 0;  
					yyd[140] = 0;  
					yyd[141] = 0;  
					yyd[142] = -40;  
					yyd[143] = 0;  
					yyd[144] = -29;  
					yyd[145] = 0;  
					yyd[146] = 0;  
					yyd[147] = -57;  
					yyd[148] = 0;  
					yyd[149] = -63;  
					yyd[150] = 0;  
					yyd[151] = 0;  
					yyd[152] = -62;  
					yyd[153] = -66;  
					yyd[154] = -65;  
					yyd[155] = -64;  
					yyd[156] = -126;  
					yyd[157] = -58;  
					yyd[158] = -156;  
					yyd[159] = -53;  
					yyd[160] = -52;  
					yyd[161] = 0;  
					yyd[162] = -47;  
					yyd[163] = 0;  
					yyd[164] = 0;  
					yyd[165] = -112;  
					yyd[166] = -113;  
					yyd[167] = -114;  
					yyd[168] = -115;  
					yyd[169] = -116;  
					yyd[170] = 0;  
					yyd[171] = -92;  
					yyd[172] = 0;  
					yyd[173] = 0;  
					yyd[174] = -86;  
					yyd[175] = -84;  
					yyd[176] = 0;  
					yyd[177] = 0;  
					yyd[178] = 0;  
					yyd[179] = 0;  
					yyd[180] = 0;  
					yyd[181] = 0;  
					yyd[182] = 0;  
					yyd[183] = 0;  
					yyd[184] = 0;  
					yyd[185] = -109;  
					yyd[186] = -94;  
					yyd[187] = 0;  
					yyd[188] = 0;  
					yyd[189] = 0;  
					yyd[190] = 0;  
					yyd[191] = 0;  
					yyd[192] = 0;  
					yyd[193] = -149;  
					yyd[194] = 0;  
					yyd[195] = 0;  
					yyd[196] = 0;  
					yyd[197] = 0;  
					yyd[198] = 0;  
					yyd[199] = -44;  
					yyd[200] = 0;  
					yyd[201] = 0;  
					yyd[202] = -46;  
					yyd[203] = -45;  
					yyd[204] = -43;  
					yyd[205] = -37;  
					yyd[206] = -31;  
					yyd[207] = -27;  
					yyd[208] = -26;  
					yyd[209] = -127;  
					yyd[210] = -61;  
					yyd[211] = -55;  
					yyd[212] = -51;  
					yyd[213] = -56;  
					yyd[214] = -111;  
					yyd[215] = 0;  
					yyd[216] = 0;  
					yyd[217] = -87;  
					yyd[218] = 0;  
					yyd[219] = -103;  
					yyd[220] = -104;  
					yyd[221] = -105;  
					yyd[222] = 0;  
					yyd[223] = -101;  
					yyd[224] = -102;  
					yyd[225] = 0;  
					yyd[226] = -97;  
					yyd[227] = -98;  
					yyd[228] = -99;  
					yyd[229] = -100;  
					yyd[230] = 0;  
					yyd[231] = -95;  
					yyd[232] = -96;  
					yyd[233] = 0;  
					yyd[234] = 0;  
					yyd[235] = 0;  
					yyd[236] = 0;  
					yyd[237] = 0;  
					yyd[238] = 0;  
					yyd[239] = -91;  
					yyd[240] = 0;  
					yyd[241] = -88;  
					yyd[242] = 0;  
					yyd[243] = 0;  
					yyd[244] = 0;  
					yyd[245] = -49;  
					yyd[246] = -42;  
					yyd[247] = -39;  
					yyd[248] = -50;  
					yyd[249] = 0;  
					yyd[250] = -85;  
					yyd[251] = 0;  
					yyd[252] = 0;  
					yyd[253] = 0;  
					yyd[254] = 0;  
					yyd[255] = 0;  
					yyd[256] = 0;  
					yyd[257] = 0;  
					yyd[258] = 0;  
					yyd[259] = -110;  
					yyd[260] = -90;  
					yyd[261] = -120;  
					yyd[262] = 0;  
					yyd[263] = 0;  
					yyd[264] = -89;  
					yyd[265] = -121;  
					yyd[266] = -122; 

					yyal = new int[yynstates];
					yyal[0] = 1;  
					yyal[1] = 29;  
					yyal[2] = 29;  
					yyal[3] = 29;  
					yyal[4] = 85;  
					yyal[5] = 109;  
					yyal[6] = 109;  
					yyal[7] = 133;  
					yyal[8] = 158;  
					yyal[9] = 158;  
					yyal[10] = 158;  
					yyal[11] = 166;  
					yyal[12] = 190;  
					yyal[13] = 190;  
					yyal[14] = 190;  
					yyal[15] = 190;  
					yyal[16] = 190;  
					yyal[17] = 190;  
					yyal[18] = 218;  
					yyal[19] = 218;  
					yyal[20] = 219;  
					yyal[21] = 219;  
					yyal[22] = 219;  
					yyal[23] = 219;  
					yyal[24] = 219;  
					yyal[25] = 245;  
					yyal[26] = 255;  
					yyal[27] = 282;  
					yyal[28] = 308;  
					yyal[29] = 334;  
					yyal[30] = 334;  
					yyal[31] = 334;  
					yyal[32] = 334;  
					yyal[33] = 334;  
					yyal[34] = 344;  
					yyal[35] = 344;  
					yyal[36] = 354;  
					yyal[37] = 380;  
					yyal[38] = 406;  
					yyal[39] = 406;  
					yyal[40] = 416;  
					yyal[41] = 416;  
					yyal[42] = 426;  
					yyal[43] = 426;  
					yyal[44] = 436;  
					yyal[45] = 436;  
					yyal[46] = 436;  
					yyal[47] = 436;  
					yyal[48] = 450;  
					yyal[49] = 450;  
					yyal[50] = 450;  
					yyal[51] = 451;  
					yyal[52] = 451;  
					yyal[53] = 451;  
					yyal[54] = 451;  
					yyal[55] = 451;  
					yyal[56] = 451;  
					yyal[57] = 451;  
					yyal[58] = 451;  
					yyal[59] = 451;  
					yyal[60] = 451;  
					yyal[61] = 451;  
					yyal[62] = 451;  
					yyal[63] = 451;  
					yyal[64] = 480;  
					yyal[65] = 480;  
					yyal[66] = 483;  
					yyal[67] = 483;  
					yyal[68] = 484;  
					yyal[69] = 484;  
					yyal[70] = 484;  
					yyal[71] = 486;  
					yyal[72] = 486;  
					yyal[73] = 486;  
					yyal[74] = 486;  
					yyal[75] = 486;  
					yyal[76] = 486;  
					yyal[77] = 487;  
					yyal[78] = 487;  
					yyal[79] = 487;  
					yyal[80] = 487;  
					yyal[81] = 487;  
					yyal[82] = 487;  
					yyal[83] = 487;  
					yyal[84] = 487;  
					yyal[85] = 512;  
					yyal[86] = 513;  
					yyal[87] = 513;  
					yyal[88] = 513;  
					yyal[89] = 513;  
					yyal[90] = 513;  
					yyal[91] = 513;  
					yyal[92] = 513;  
					yyal[93] = 513;  
					yyal[94] = 513;  
					yyal[95] = 513;  
					yyal[96] = 513;  
					yyal[97] = 513;  
					yyal[98] = 513;  
					yyal[99] = 513;  
					yyal[100] = 513;  
					yyal[101] = 513;  
					yyal[102] = 513;  
					yyal[103] = 513;  
					yyal[104] = 513;  
					yyal[105] = 513;  
					yyal[106] = 544;  
					yyal[107] = 546;  
					yyal[108] = 547;  
					yyal[109] = 547;  
					yyal[110] = 558;  
					yyal[111] = 563;  
					yyal[112] = 563;  
					yyal[113] = 563;  
					yyal[114] = 563;  
					yyal[115] = 563;  
					yyal[116] = 563;  
					yyal[117] = 570;  
					yyal[118] = 571;  
					yyal[119] = 604;  
					yyal[120] = 635;  
					yyal[121] = 666;  
					yyal[122] = 667;  
					yyal[123] = 668;  
					yyal[124] = 707;  
					yyal[125] = 712;  
					yyal[126] = 712;  
					yyal[127] = 743;  
					yyal[128] = 774;  
					yyal[129] = 808;  
					yyal[130] = 839;  
					yyal[131] = 870;  
					yyal[132] = 910;  
					yyal[133] = 950;  
					yyal[134] = 990;  
					yyal[135] = 1030;  
					yyal[136] = 1070;  
					yyal[137] = 1070;  
					yyal[138] = 1070;  
					yyal[139] = 1070;  
					yyal[140] = 1101;  
					yyal[141] = 1112;  
					yyal[142] = 1113;  
					yyal[143] = 1113;  
					yyal[144] = 1114;  
					yyal[145] = 1114;  
					yyal[146] = 1119;  
					yyal[147] = 1120;  
					yyal[148] = 1120;  
					yyal[149] = 1146;  
					yyal[150] = 1146;  
					yyal[151] = 1179;  
					yyal[152] = 1180;  
					yyal[153] = 1180;  
					yyal[154] = 1180;  
					yyal[155] = 1180;  
					yyal[156] = 1180;  
					yyal[157] = 1180;  
					yyal[158] = 1180;  
					yyal[159] = 1180;  
					yyal[160] = 1180;  
					yyal[161] = 1180;  
					yyal[162] = 1211;  
					yyal[163] = 1211;  
					yyal[164] = 1212;  
					yyal[165] = 1243;  
					yyal[166] = 1243;  
					yyal[167] = 1243;  
					yyal[168] = 1243;  
					yyal[169] = 1243;  
					yyal[170] = 1243;  
					yyal[171] = 1244;  
					yyal[172] = 1244;  
					yyal[173] = 1245;  
					yyal[174] = 1276;  
					yyal[175] = 1276;  
					yyal[176] = 1276;  
					yyal[177] = 1295;  
					yyal[178] = 1311;  
					yyal[179] = 1325;  
					yyal[180] = 1335;  
					yyal[181] = 1343;  
					yyal[182] = 1350;  
					yyal[183] = 1356;  
					yyal[184] = 1361;  
					yyal[185] = 1365;  
					yyal[186] = 1365;  
					yyal[187] = 1365;  
					yyal[188] = 1388;  
					yyal[189] = 1419;  
					yyal[190] = 1446;  
					yyal[191] = 1473;  
					yyal[192] = 1500;  
					yyal[193] = 1520;  
					yyal[194] = 1520;  
					yyal[195] = 1551;  
					yyal[196] = 1552;  
					yyal[197] = 1572;  
					yyal[198] = 1573;  
					yyal[199] = 1604;  
					yyal[200] = 1604;  
					yyal[201] = 1636;  
					yyal[202] = 1637;  
					yyal[203] = 1637;  
					yyal[204] = 1637;  
					yyal[205] = 1637;  
					yyal[206] = 1637;  
					yyal[207] = 1637;  
					yyal[208] = 1637;  
					yyal[209] = 1637;  
					yyal[210] = 1637;  
					yyal[211] = 1637;  
					yyal[212] = 1637;  
					yyal[213] = 1637;  
					yyal[214] = 1637;  
					yyal[215] = 1637;  
					yyal[216] = 1668;  
					yyal[217] = 1699;  
					yyal[218] = 1699;  
					yyal[219] = 1730;  
					yyal[220] = 1730;  
					yyal[221] = 1730;  
					yyal[222] = 1730;  
					yyal[223] = 1761;  
					yyal[224] = 1761;  
					yyal[225] = 1761;  
					yyal[226] = 1792;  
					yyal[227] = 1792;  
					yyal[228] = 1792;  
					yyal[229] = 1792;  
					yyal[230] = 1792;  
					yyal[231] = 1823;  
					yyal[232] = 1823;  
					yyal[233] = 1823;  
					yyal[234] = 1854;  
					yyal[235] = 1885;  
					yyal[236] = 1916;  
					yyal[237] = 1947;  
					yyal[238] = 1978;  
					yyal[239] = 2009;  
					yyal[240] = 2009;  
					yyal[241] = 2010;  
					yyal[242] = 2010;  
					yyal[243] = 2011;  
					yyal[244] = 2042;  
					yyal[245] = 2073;  
					yyal[246] = 2073;  
					yyal[247] = 2073;  
					yyal[248] = 2073;  
					yyal[249] = 2073;  
					yyal[250] = 2074;  
					yyal[251] = 2074;  
					yyal[252] = 2093;  
					yyal[253] = 2109;  
					yyal[254] = 2123;  
					yyal[255] = 2133;  
					yyal[256] = 2141;  
					yyal[257] = 2148;  
					yyal[258] = 2154;  
					yyal[259] = 2159;  
					yyal[260] = 2159;  
					yyal[261] = 2159;  
					yyal[262] = 2159;  
					yyal[263] = 2160;  
					yyal[264] = 2161;  
					yyal[265] = 2161;  
					yyal[266] = 2161; 

					yyah = new int[yynstates];
					yyah[0] = 28;  
					yyah[1] = 28;  
					yyah[2] = 28;  
					yyah[3] = 84;  
					yyah[4] = 108;  
					yyah[5] = 108;  
					yyah[6] = 132;  
					yyah[7] = 157;  
					yyah[8] = 157;  
					yyah[9] = 157;  
					yyah[10] = 165;  
					yyah[11] = 189;  
					yyah[12] = 189;  
					yyah[13] = 189;  
					yyah[14] = 189;  
					yyah[15] = 189;  
					yyah[16] = 189;  
					yyah[17] = 217;  
					yyah[18] = 217;  
					yyah[19] = 218;  
					yyah[20] = 218;  
					yyah[21] = 218;  
					yyah[22] = 218;  
					yyah[23] = 218;  
					yyah[24] = 244;  
					yyah[25] = 254;  
					yyah[26] = 281;  
					yyah[27] = 307;  
					yyah[28] = 333;  
					yyah[29] = 333;  
					yyah[30] = 333;  
					yyah[31] = 333;  
					yyah[32] = 333;  
					yyah[33] = 343;  
					yyah[34] = 343;  
					yyah[35] = 353;  
					yyah[36] = 379;  
					yyah[37] = 405;  
					yyah[38] = 405;  
					yyah[39] = 415;  
					yyah[40] = 415;  
					yyah[41] = 425;  
					yyah[42] = 425;  
					yyah[43] = 435;  
					yyah[44] = 435;  
					yyah[45] = 435;  
					yyah[46] = 435;  
					yyah[47] = 449;  
					yyah[48] = 449;  
					yyah[49] = 449;  
					yyah[50] = 450;  
					yyah[51] = 450;  
					yyah[52] = 450;  
					yyah[53] = 450;  
					yyah[54] = 450;  
					yyah[55] = 450;  
					yyah[56] = 450;  
					yyah[57] = 450;  
					yyah[58] = 450;  
					yyah[59] = 450;  
					yyah[60] = 450;  
					yyah[61] = 450;  
					yyah[62] = 450;  
					yyah[63] = 479;  
					yyah[64] = 479;  
					yyah[65] = 482;  
					yyah[66] = 482;  
					yyah[67] = 483;  
					yyah[68] = 483;  
					yyah[69] = 483;  
					yyah[70] = 485;  
					yyah[71] = 485;  
					yyah[72] = 485;  
					yyah[73] = 485;  
					yyah[74] = 485;  
					yyah[75] = 485;  
					yyah[76] = 486;  
					yyah[77] = 486;  
					yyah[78] = 486;  
					yyah[79] = 486;  
					yyah[80] = 486;  
					yyah[81] = 486;  
					yyah[82] = 486;  
					yyah[83] = 486;  
					yyah[84] = 511;  
					yyah[85] = 512;  
					yyah[86] = 512;  
					yyah[87] = 512;  
					yyah[88] = 512;  
					yyah[89] = 512;  
					yyah[90] = 512;  
					yyah[91] = 512;  
					yyah[92] = 512;  
					yyah[93] = 512;  
					yyah[94] = 512;  
					yyah[95] = 512;  
					yyah[96] = 512;  
					yyah[97] = 512;  
					yyah[98] = 512;  
					yyah[99] = 512;  
					yyah[100] = 512;  
					yyah[101] = 512;  
					yyah[102] = 512;  
					yyah[103] = 512;  
					yyah[104] = 512;  
					yyah[105] = 543;  
					yyah[106] = 545;  
					yyah[107] = 546;  
					yyah[108] = 546;  
					yyah[109] = 557;  
					yyah[110] = 562;  
					yyah[111] = 562;  
					yyah[112] = 562;  
					yyah[113] = 562;  
					yyah[114] = 562;  
					yyah[115] = 562;  
					yyah[116] = 569;  
					yyah[117] = 570;  
					yyah[118] = 603;  
					yyah[119] = 634;  
					yyah[120] = 665;  
					yyah[121] = 666;  
					yyah[122] = 667;  
					yyah[123] = 706;  
					yyah[124] = 711;  
					yyah[125] = 711;  
					yyah[126] = 742;  
					yyah[127] = 773;  
					yyah[128] = 807;  
					yyah[129] = 838;  
					yyah[130] = 869;  
					yyah[131] = 909;  
					yyah[132] = 949;  
					yyah[133] = 989;  
					yyah[134] = 1029;  
					yyah[135] = 1069;  
					yyah[136] = 1069;  
					yyah[137] = 1069;  
					yyah[138] = 1069;  
					yyah[139] = 1100;  
					yyah[140] = 1111;  
					yyah[141] = 1112;  
					yyah[142] = 1112;  
					yyah[143] = 1113;  
					yyah[144] = 1113;  
					yyah[145] = 1118;  
					yyah[146] = 1119;  
					yyah[147] = 1119;  
					yyah[148] = 1145;  
					yyah[149] = 1145;  
					yyah[150] = 1178;  
					yyah[151] = 1179;  
					yyah[152] = 1179;  
					yyah[153] = 1179;  
					yyah[154] = 1179;  
					yyah[155] = 1179;  
					yyah[156] = 1179;  
					yyah[157] = 1179;  
					yyah[158] = 1179;  
					yyah[159] = 1179;  
					yyah[160] = 1179;  
					yyah[161] = 1210;  
					yyah[162] = 1210;  
					yyah[163] = 1211;  
					yyah[164] = 1242;  
					yyah[165] = 1242;  
					yyah[166] = 1242;  
					yyah[167] = 1242;  
					yyah[168] = 1242;  
					yyah[169] = 1242;  
					yyah[170] = 1243;  
					yyah[171] = 1243;  
					yyah[172] = 1244;  
					yyah[173] = 1275;  
					yyah[174] = 1275;  
					yyah[175] = 1275;  
					yyah[176] = 1294;  
					yyah[177] = 1310;  
					yyah[178] = 1324;  
					yyah[179] = 1334;  
					yyah[180] = 1342;  
					yyah[181] = 1349;  
					yyah[182] = 1355;  
					yyah[183] = 1360;  
					yyah[184] = 1364;  
					yyah[185] = 1364;  
					yyah[186] = 1364;  
					yyah[187] = 1387;  
					yyah[188] = 1418;  
					yyah[189] = 1445;  
					yyah[190] = 1472;  
					yyah[191] = 1499;  
					yyah[192] = 1519;  
					yyah[193] = 1519;  
					yyah[194] = 1550;  
					yyah[195] = 1551;  
					yyah[196] = 1571;  
					yyah[197] = 1572;  
					yyah[198] = 1603;  
					yyah[199] = 1603;  
					yyah[200] = 1635;  
					yyah[201] = 1636;  
					yyah[202] = 1636;  
					yyah[203] = 1636;  
					yyah[204] = 1636;  
					yyah[205] = 1636;  
					yyah[206] = 1636;  
					yyah[207] = 1636;  
					yyah[208] = 1636;  
					yyah[209] = 1636;  
					yyah[210] = 1636;  
					yyah[211] = 1636;  
					yyah[212] = 1636;  
					yyah[213] = 1636;  
					yyah[214] = 1636;  
					yyah[215] = 1667;  
					yyah[216] = 1698;  
					yyah[217] = 1698;  
					yyah[218] = 1729;  
					yyah[219] = 1729;  
					yyah[220] = 1729;  
					yyah[221] = 1729;  
					yyah[222] = 1760;  
					yyah[223] = 1760;  
					yyah[224] = 1760;  
					yyah[225] = 1791;  
					yyah[226] = 1791;  
					yyah[227] = 1791;  
					yyah[228] = 1791;  
					yyah[229] = 1791;  
					yyah[230] = 1822;  
					yyah[231] = 1822;  
					yyah[232] = 1822;  
					yyah[233] = 1853;  
					yyah[234] = 1884;  
					yyah[235] = 1915;  
					yyah[236] = 1946;  
					yyah[237] = 1977;  
					yyah[238] = 2008;  
					yyah[239] = 2008;  
					yyah[240] = 2009;  
					yyah[241] = 2009;  
					yyah[242] = 2010;  
					yyah[243] = 2041;  
					yyah[244] = 2072;  
					yyah[245] = 2072;  
					yyah[246] = 2072;  
					yyah[247] = 2072;  
					yyah[248] = 2072;  
					yyah[249] = 2073;  
					yyah[250] = 2073;  
					yyah[251] = 2092;  
					yyah[252] = 2108;  
					yyah[253] = 2122;  
					yyah[254] = 2132;  
					yyah[255] = 2140;  
					yyah[256] = 2147;  
					yyah[257] = 2153;  
					yyah[258] = 2158;  
					yyah[259] = 2158;  
					yyah[260] = 2158;  
					yyah[261] = 2158;  
					yyah[262] = 2159;  
					yyah[263] = 2160;  
					yyah[264] = 2160;  
					yyah[265] = 2160;  
					yyah[266] = 2160; 

					yygl = new int[yynstates];
					yygl[0] = 1;  
					yygl[1] = 20;  
					yygl[2] = 20;  
					yygl[3] = 20;  
					yygl[4] = 20;  
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
					yygl[48] = 75;  
					yygl[49] = 75;  
					yygl[50] = 75;  
					yygl[51] = 75;  
					yygl[52] = 75;  
					yygl[53] = 75;  
					yygl[54] = 75;  
					yygl[55] = 75;  
					yygl[56] = 75;  
					yygl[57] = 75;  
					yygl[58] = 75;  
					yygl[59] = 75;  
					yygl[60] = 75;  
					yygl[61] = 75;  
					yygl[62] = 75;  
					yygl[63] = 75;  
					yygl[64] = 75;  
					yygl[65] = 75;  
					yygl[66] = 76;  
					yygl[67] = 76;  
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
					yygl[80] = 77;  
					yygl[81] = 77;  
					yygl[82] = 77;  
					yygl[83] = 77;  
					yygl[84] = 77;  
					yygl[85] = 84;  
					yygl[86] = 84;  
					yygl[87] = 84;  
					yygl[88] = 84;  
					yygl[89] = 84;  
					yygl[90] = 84;  
					yygl[91] = 84;  
					yygl[92] = 84;  
					yygl[93] = 84;  
					yygl[94] = 84;  
					yygl[95] = 84;  
					yygl[96] = 84;  
					yygl[97] = 84;  
					yygl[98] = 84;  
					yygl[99] = 84;  
					yygl[100] = 84;  
					yygl[101] = 84;  
					yygl[102] = 84;  
					yygl[103] = 84;  
					yygl[104] = 84;  
					yygl[105] = 84;  
					yygl[106] = 95;  
					yygl[107] = 95;  
					yygl[108] = 95;  
					yygl[109] = 95;  
					yygl[110] = 100;  
					yygl[111] = 104;  
					yygl[112] = 104;  
					yygl[113] = 104;  
					yygl[114] = 104;  
					yygl[115] = 104;  
					yygl[116] = 104;  
					yygl[117] = 104;  
					yygl[118] = 104;  
					yygl[119] = 119;  
					yygl[120] = 130;  
					yygl[121] = 141;  
					yygl[122] = 141;  
					yygl[123] = 141;  
					yygl[124] = 156;  
					yygl[125] = 157;  
					yygl[126] = 157;  
					yygl[127] = 168;  
					yygl[128] = 189;  
					yygl[129] = 189;  
					yygl[130] = 210;  
					yygl[131] = 231;  
					yygl[132] = 231;  
					yygl[133] = 231;  
					yygl[134] = 231;  
					yygl[135] = 231;  
					yygl[136] = 231;  
					yygl[137] = 231;  
					yygl[138] = 231;  
					yygl[139] = 231;  
					yygl[140] = 245;  
					yygl[141] = 250;  
					yygl[142] = 250;  
					yygl[143] = 250;  
					yygl[144] = 250;  
					yygl[145] = 250;  
					yygl[146] = 254;  
					yygl[147] = 254;  
					yygl[148] = 254;  
					yygl[149] = 259;  
					yygl[150] = 259;  
					yygl[151] = 274;  
					yygl[152] = 274;  
					yygl[153] = 274;  
					yygl[154] = 274;  
					yygl[155] = 274;  
					yygl[156] = 274;  
					yygl[157] = 274;  
					yygl[158] = 274;  
					yygl[159] = 274;  
					yygl[160] = 274;  
					yygl[161] = 274;  
					yygl[162] = 285;  
					yygl[163] = 285;  
					yygl[164] = 285;  
					yygl[165] = 306;  
					yygl[166] = 306;  
					yygl[167] = 306;  
					yygl[168] = 306;  
					yygl[169] = 306;  
					yygl[170] = 306;  
					yygl[171] = 306;  
					yygl[172] = 306;  
					yygl[173] = 306;  
					yygl[174] = 317;  
					yygl[175] = 317;  
					yygl[176] = 317;  
					yygl[177] = 318;  
					yygl[178] = 319;  
					yygl[179] = 320;  
					yygl[180] = 321;  
					yygl[181] = 321;  
					yygl[182] = 321;  
					yygl[183] = 321;  
					yygl[184] = 321;  
					yygl[185] = 321;  
					yygl[186] = 321;  
					yygl[187] = 321;  
					yygl[188] = 322;  
					yygl[189] = 343;  
					yygl[190] = 343;  
					yygl[191] = 343;  
					yygl[192] = 343;  
					yygl[193] = 343;  
					yygl[194] = 343;  
					yygl[195] = 354;  
					yygl[196] = 354;  
					yygl[197] = 354;  
					yygl[198] = 354;  
					yygl[199] = 365;  
					yygl[200] = 365;  
					yygl[201] = 379;  
					yygl[202] = 379;  
					yygl[203] = 379;  
					yygl[204] = 379;  
					yygl[205] = 379;  
					yygl[206] = 379;  
					yygl[207] = 379;  
					yygl[208] = 379;  
					yygl[209] = 379;  
					yygl[210] = 379;  
					yygl[211] = 379;  
					yygl[212] = 379;  
					yygl[213] = 379;  
					yygl[214] = 379;  
					yygl[215] = 379;  
					yygl[216] = 390;  
					yygl[217] = 411;  
					yygl[218] = 411;  
					yygl[219] = 422;  
					yygl[220] = 422;  
					yygl[221] = 422;  
					yygl[222] = 422;  
					yygl[223] = 434;  
					yygl[224] = 434;  
					yygl[225] = 434;  
					yygl[226] = 447;  
					yygl[227] = 447;  
					yygl[228] = 447;  
					yygl[229] = 447;  
					yygl[230] = 447;  
					yygl[231] = 461;  
					yygl[232] = 461;  
					yygl[233] = 461;  
					yygl[234] = 476;  
					yygl[235] = 492;  
					yygl[236] = 509;  
					yygl[237] = 527;  
					yygl[238] = 546;  
					yygl[239] = 567;  
					yygl[240] = 567;  
					yygl[241] = 567;  
					yygl[242] = 567;  
					yygl[243] = 567;  
					yygl[244] = 578;  
					yygl[245] = 589;  
					yygl[246] = 589;  
					yygl[247] = 589;  
					yygl[248] = 589;  
					yygl[249] = 589;  
					yygl[250] = 589;  
					yygl[251] = 589;  
					yygl[252] = 590;  
					yygl[253] = 591;  
					yygl[254] = 592;  
					yygl[255] = 593;  
					yygl[256] = 593;  
					yygl[257] = 593;  
					yygl[258] = 593;  
					yygl[259] = 593;  
					yygl[260] = 593;  
					yygl[261] = 593;  
					yygl[262] = 593;  
					yygl[263] = 593;  
					yygl[264] = 593;  
					yygl[265] = 593;  
					yygl[266] = 593; 

					yygh = new int[yynstates];
					yygh[0] = 19;  
					yygh[1] = 19;  
					yygh[2] = 19;  
					yygh[3] = 19;  
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
					yygh[47] = 74;  
					yygh[48] = 74;  
					yygh[49] = 74;  
					yygh[50] = 74;  
					yygh[51] = 74;  
					yygh[52] = 74;  
					yygh[53] = 74;  
					yygh[54] = 74;  
					yygh[55] = 74;  
					yygh[56] = 74;  
					yygh[57] = 74;  
					yygh[58] = 74;  
					yygh[59] = 74;  
					yygh[60] = 74;  
					yygh[61] = 74;  
					yygh[62] = 74;  
					yygh[63] = 74;  
					yygh[64] = 74;  
					yygh[65] = 75;  
					yygh[66] = 75;  
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
					yygh[79] = 76;  
					yygh[80] = 76;  
					yygh[81] = 76;  
					yygh[82] = 76;  
					yygh[83] = 76;  
					yygh[84] = 83;  
					yygh[85] = 83;  
					yygh[86] = 83;  
					yygh[87] = 83;  
					yygh[88] = 83;  
					yygh[89] = 83;  
					yygh[90] = 83;  
					yygh[91] = 83;  
					yygh[92] = 83;  
					yygh[93] = 83;  
					yygh[94] = 83;  
					yygh[95] = 83;  
					yygh[96] = 83;  
					yygh[97] = 83;  
					yygh[98] = 83;  
					yygh[99] = 83;  
					yygh[100] = 83;  
					yygh[101] = 83;  
					yygh[102] = 83;  
					yygh[103] = 83;  
					yygh[104] = 83;  
					yygh[105] = 94;  
					yygh[106] = 94;  
					yygh[107] = 94;  
					yygh[108] = 94;  
					yygh[109] = 99;  
					yygh[110] = 103;  
					yygh[111] = 103;  
					yygh[112] = 103;  
					yygh[113] = 103;  
					yygh[114] = 103;  
					yygh[115] = 103;  
					yygh[116] = 103;  
					yygh[117] = 103;  
					yygh[118] = 118;  
					yygh[119] = 129;  
					yygh[120] = 140;  
					yygh[121] = 140;  
					yygh[122] = 140;  
					yygh[123] = 155;  
					yygh[124] = 156;  
					yygh[125] = 156;  
					yygh[126] = 167;  
					yygh[127] = 188;  
					yygh[128] = 188;  
					yygh[129] = 209;  
					yygh[130] = 230;  
					yygh[131] = 230;  
					yygh[132] = 230;  
					yygh[133] = 230;  
					yygh[134] = 230;  
					yygh[135] = 230;  
					yygh[136] = 230;  
					yygh[137] = 230;  
					yygh[138] = 230;  
					yygh[139] = 244;  
					yygh[140] = 249;  
					yygh[141] = 249;  
					yygh[142] = 249;  
					yygh[143] = 249;  
					yygh[144] = 249;  
					yygh[145] = 253;  
					yygh[146] = 253;  
					yygh[147] = 253;  
					yygh[148] = 258;  
					yygh[149] = 258;  
					yygh[150] = 273;  
					yygh[151] = 273;  
					yygh[152] = 273;  
					yygh[153] = 273;  
					yygh[154] = 273;  
					yygh[155] = 273;  
					yygh[156] = 273;  
					yygh[157] = 273;  
					yygh[158] = 273;  
					yygh[159] = 273;  
					yygh[160] = 273;  
					yygh[161] = 284;  
					yygh[162] = 284;  
					yygh[163] = 284;  
					yygh[164] = 305;  
					yygh[165] = 305;  
					yygh[166] = 305;  
					yygh[167] = 305;  
					yygh[168] = 305;  
					yygh[169] = 305;  
					yygh[170] = 305;  
					yygh[171] = 305;  
					yygh[172] = 305;  
					yygh[173] = 316;  
					yygh[174] = 316;  
					yygh[175] = 316;  
					yygh[176] = 317;  
					yygh[177] = 318;  
					yygh[178] = 319;  
					yygh[179] = 320;  
					yygh[180] = 320;  
					yygh[181] = 320;  
					yygh[182] = 320;  
					yygh[183] = 320;  
					yygh[184] = 320;  
					yygh[185] = 320;  
					yygh[186] = 320;  
					yygh[187] = 321;  
					yygh[188] = 342;  
					yygh[189] = 342;  
					yygh[190] = 342;  
					yygh[191] = 342;  
					yygh[192] = 342;  
					yygh[193] = 342;  
					yygh[194] = 353;  
					yygh[195] = 353;  
					yygh[196] = 353;  
					yygh[197] = 353;  
					yygh[198] = 364;  
					yygh[199] = 364;  
					yygh[200] = 378;  
					yygh[201] = 378;  
					yygh[202] = 378;  
					yygh[203] = 378;  
					yygh[204] = 378;  
					yygh[205] = 378;  
					yygh[206] = 378;  
					yygh[207] = 378;  
					yygh[208] = 378;  
					yygh[209] = 378;  
					yygh[210] = 378;  
					yygh[211] = 378;  
					yygh[212] = 378;  
					yygh[213] = 378;  
					yygh[214] = 378;  
					yygh[215] = 389;  
					yygh[216] = 410;  
					yygh[217] = 410;  
					yygh[218] = 421;  
					yygh[219] = 421;  
					yygh[220] = 421;  
					yygh[221] = 421;  
					yygh[222] = 433;  
					yygh[223] = 433;  
					yygh[224] = 433;  
					yygh[225] = 446;  
					yygh[226] = 446;  
					yygh[227] = 446;  
					yygh[228] = 446;  
					yygh[229] = 446;  
					yygh[230] = 460;  
					yygh[231] = 460;  
					yygh[232] = 460;  
					yygh[233] = 475;  
					yygh[234] = 491;  
					yygh[235] = 508;  
					yygh[236] = 526;  
					yygh[237] = 545;  
					yygh[238] = 566;  
					yygh[239] = 566;  
					yygh[240] = 566;  
					yygh[241] = 566;  
					yygh[242] = 566;  
					yygh[243] = 577;  
					yygh[244] = 588;  
					yygh[245] = 588;  
					yygh[246] = 588;  
					yygh[247] = 588;  
					yygh[248] = 588;  
					yygh[249] = 588;  
					yygh[250] = 588;  
					yygh[251] = 589;  
					yygh[252] = 590;  
					yygh[253] = 591;  
					yygh[254] = 592;  
					yygh[255] = 592;  
					yygh[256] = 592;  
					yygh[257] = 592;  
					yygh[258] = 592;  
					yygh[259] = 592;  
					yygh[260] = 592;  
					yygh[261] = 592;  
					yygh[262] = 592;  
					yygh[263] = 592;  
					yygh[264] = 592;  
					yygh[265] = 592;  
					yygh[266] = 592; 

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
					yyr[yyrc] = new YYRRec(5,-8);yyrc++; 
					yyr[yyrc] = new YYRRec(4,-8);yyrc++; 
					yyr[yyrc] = new YYRRec(3,-8);yyrc++; 
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
					yyr[yyrc] = new YYRRec(5,-7);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-33);yyrc++; 
					yyr[yyrc] = new YYRRec(3,-35);yyrc++; 
					yyr[yyrc] = new YYRRec(4,-35);yyrc++; 
					yyr[yyrc] = new YYRRec(3,-35);yyrc++; 
					yyr[yyrc] = new YYRRec(2,-35);yyrc++; 
					yyr[yyrc] = new YYRRec(2,-35);yyrc++; 
					yyr[yyrc] = new YYRRec(0,-35);yyrc++; 
					yyr[yyrc] = new YYRRec(3,-38);yyrc++; 
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
					yyr[yyrc] = new YYRRec(2,-59);yyrc++; 
					yyr[yyrc] = new YYRRec(4,-59);yyrc++; 
					yyr[yyrc] = new YYRRec(3,-59);yyrc++; 
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
					yyr[yyrc] = new YYRRec(3,-5);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-5);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-32);yyrc++; 
					yyr[yyrc] = new YYRRec(2,-32);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-65);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-65);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-65);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-65);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-65);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-29);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-29);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-29);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-29);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-29);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-29);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-29);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-29);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-29);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-39);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-39);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-39);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-39);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-39);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-18);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-18);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-62);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-62);yyrc++; 
					yyr[yyrc] = new YYRRec(2,-24);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-24);yyrc++; 
					yyr[yyrc] = new YYRRec(2,-66);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-66);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-17);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-43);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-14);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-14);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-14);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-67);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-67);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-67);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-67);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-67);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-67);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-67);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-67);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-67);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-67);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-67);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-67);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-67);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-67);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-67);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-68);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-68);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-68);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-68);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-68);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-68);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-68);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-64);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-64);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-64);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-64);yyrc++; 
					yyr[yyrc] = new YYRRec(3,-69);yyrc++; 
					yyr[yyrc] = new YYRRec(3,-69);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-69);yyrc++; 
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

            if (yyerrflag==0) yyerror("syntax error "+yylval);

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

            return true;

            abort:

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
                AToken lasttoken = FindTokenOpt(ref Input, pos);
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
        public AToken FindTokenOpt(ref string Rest, int startpos)
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

			if (Regex.IsMatch(Rest,"^((?i)RULE)")){
				Results.Add (t_RULE);
				ResultsV.Add(Regex.Match(Rest,"^((?i)RULE)").Value);}

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

			if (Regex.IsMatch(Rest,"^((?i)ELSE)")){
				Results.Add (t_ELSE);
				ResultsV.Add(Regex.Match(Rest,"^((?i)ELSE)").Value);}

			if (Regex.IsMatch(Rest,"^((?i)IF)")){
				Results.Add (t_IF);
				ResultsV.Add(Regex.Match(Rest,"^((?i)IF)").Value);}

			if (Regex.IsMatch(Rest,"^((?i)WHILE)")){
				Results.Add (t_WHILE);
				ResultsV.Add(Regex.Match(Rest,"^((?i)WHILE)").Value);}

			if (Regex.IsMatch(Rest,"^(\\.)")){
				Results.Add (t_Char46);
				ResultsV.Add(Regex.Match(Rest,"^(\\.)").Value);}

			if (Regex.IsMatch(Rest,"^((?i)NULL)")){
				Results.Add (t_NULL);
				ResultsV.Add(Regex.Match(Rest,"^((?i)NULL)").Value);}

			if (Regex.IsMatch(Rest,"^((?i)(EACH_SEC|IF_(AE|ALT|ANYKEY|APO|BKSL|BKSP|BRACKL|BRACKR|CAL|CAR|COMMA|CTRL|CUD|CUL|CUR|CUU|DEL|END|EQUALS|ESC|HOME|INS|JOY4|LEFT|LOAD|MIDDLE|MINUS|MSTOP|OE|PAUSE|PERIOD|PGDN|PGUP|PLUS|RIGHT|SEMIC|SLASH|SPACE|START|SZ|TAB|TAST|UE|F(1[0-2]|[1-9])|[0-9A-Z])|LAYERS|MESSAGES|PANELS))")){
				Results.Add (t_event);
				ResultsV.Add(Regex.Match(Rest,"^((?i)(EACH_SEC|IF_(AE|ALT|ANYKEY|APO|BKSL|BKSP|BRACKL|BRACKR|CAL|CAR|COMMA|CTRL|CUD|CUL|CUR|CUU|DEL|END|EQUALS|ESC|HOME|INS|JOY4|LEFT|LOAD|MIDDLE|MINUS|MSTOP|OE|PAUSE|PERIOD|PGDN|PGUP|PLUS|RIGHT|SEMIC|SLASH|SPACE|START|SZ|TAB|TAST|UE|F(1[0-2]|[1-9])|[0-9A-Z])|LAYERS|MESSAGES|PANELS))").Value);}

			if (Regex.IsMatch(Rest,"^((?i)(VIDEO|NEXUS|LIGHT_ANGLE|IBANK|DRUMBANK|MIDI_PITCH|BIND|MAPFILE|SAVEDIR|PATH|DITHER|SAVE_KEYS|REMOTE_KEYS))")){
				Results.Add (t_global);
				ResultsV.Add(Regex.Match(Rest,"^((?i)(VIDEO|NEXUS|LIGHT_ANGLE|IBANK|DRUMBANK|MIDI_PITCH|BIND|MAPFILE|SAVEDIR|PATH|DITHER|SAVE_KEYS|REMOTE_KEYS))").Value);}

			if (Regex.IsMatch(Rest,"^((?i)(MODEL|SOUND|MUSIC|FLIC|BMAP|OVLY|FONT))")){
				Results.Add (t_asset);
				ResultsV.Add(Regex.Match(Rest,"^((?i)(MODEL|SOUND|MUSIC|FLIC|BMAP|OVLY|FONT))").Value);}

			if (Regex.IsMatch(Rest,"^((?i)(OVERLAY|PANEL|PALETTE|REGION|SKILL|STRING|SYNONYM|TEXTURE|TEXT|VIEW|WALL|WAY))")){
				Results.Add (t_object);
				ResultsV.Add(Regex.Match(Rest,"^((?i)(OVERLAY|PANEL|PALETTE|REGION|SKILL|STRING|SYNONYM|TEXTURE|TEXT|VIEW|WALL|WAY))").Value);}

			if (Regex.IsMatch(Rest,"^((?i)(ACTION|RULES))")){
				Results.Add (t_function);
				ResultsV.Add(Regex.Match(Rest,"^((?i)(ACTION|RULES))").Value);}

			if (Regex.IsMatch(Rest,"^((?i)(ACOS|COS|ATAN|TAN|SIGN|INT|EXP|LOG10|LOG2|LOG))")){
				Results.Add (t_math);
				ResultsV.Add(Regex.Match(Rest,"^((?i)(ACOS|COS|ATAN|TAN|SIGN|INT|EXP|LOG10|LOG2|LOG))").Value);}

			if (Regex.IsMatch(Rest,"^((?i)(FLOOR_DESCEND|CEIL_DESCEND|FLOOR_ASCEND|FLOOR_LIFTED|CEIL_ASCEND|CEIL_LIFTED|TRANSPARENT|CANDELABER|DIAPHANOUS|IMMATERIAL|IMPASSABLE|PORTCULLIS|AUTORANGE|CAREFULLY|CONDENSED|INVISIBLE|SENSITIVE|BERKELEY|CENTER_X|CENTER_Y|LIGHTMAP|PASSABLE|SAVE_ALL|CLUSTER|CURTAIN|FRAGILE|NO_CLIP|ONESHOT|REFRESH|VISIBLE|ABSPOS|BEHIND|GROUND|LIFTED|MASTER|NARROW|RELPOS|SHADOW|STICKY|FENCE|GHOST|LIBER|MOVED|SLOOP|BASE|BLUR|CLIP|FLAT|HARD|PLAY|SEEN|WIRE|FAR|SKY))")){
				Results.Add (t_flag);
				ResultsV.Add(Regex.Match(Rest,"^((?i)(FLOOR_DESCEND|CEIL_DESCEND|FLOOR_ASCEND|FLOOR_LIFTED|CEIL_ASCEND|CEIL_LIFTED|TRANSPARENT|CANDELABER|DIAPHANOUS|IMMATERIAL|IMPASSABLE|PORTCULLIS|AUTORANGE|CAREFULLY|CONDENSED|INVISIBLE|SENSITIVE|BERKELEY|CENTER_X|CENTER_Y|LIGHTMAP|PASSABLE|SAVE_ALL|CLUSTER|CURTAIN|FRAGILE|NO_CLIP|ONESHOT|REFRESH|VISIBLE|ABSPOS|BEHIND|GROUND|LIFTED|MASTER|NARROW|RELPOS|SHADOW|STICKY|FENCE|GHOST|LIBER|MOVED|SLOOP|BASE|BLUR|CLIP|FLAT|HARD|PLAY|SEEN|WIRE|FAR|SKY))").Value);}

			if (Regex.IsMatch(Rest,"^((?i)(ALBEDO|ANGLE|ASPEED|ATTACH|BELOW|BMAPS|BUTTON|CEIL_(ANGLE|OFFS_[X-Y]|TEX)|CYCLES|CYCLE|DEFAULT|DELAY|DIGITS|DISTANCE|DIST|EACH_CYCLE_(C|F)|EACH_CYCLE|FLAGS|FLOOR_(ANGLE|OFFS_[X-Y]|TEX)|FOOT_HGT|FRAME|GENIUS|HBAR|HEIGHT|HSLIDER|IF_(ARISE|ARRIVED|DIVE|FAR|LEAVE|NEAR|RELEASE|TOUCH)|INDEX|LAYER|LEFT|LENGTH|MAP_COLOR|MASK|MAX|MIN|MIRROR|OFFSET_[X-Y]|OVLYS|PALFILE|PAN_MAP|PICTURE|POSITION|POS_[X-Y]|RADIANCE|RANGE|REL_(ANGLE|DIST)|RIGHT|SCALE_(XY|X|Y)|SCYCLES|SCYCLE|SCALE|SDIST|SIDES|SIDE|SIZE_[X-Y]|SKILL[1-8]|SPEED|STRINGS|SVDIST|SVOL|TARGET_(MAP|X|Y)|TARGET|TEXTURE[1-4]|THING_HGT|TITLE|TOP|TOUCH|TYPE|VAL|VBAR|VSLIDER|VSPEED|WAYPOINT|WINDOW|[X-Z][1-2]|[X-Z]))")){
				Results.Add (t_property);
				ResultsV.Add(Regex.Match(Rest,"^((?i)(ALBEDO|ANGLE|ASPEED|ATTACH|BELOW|BMAPS|BUTTON|CEIL_(ANGLE|OFFS_[X-Y]|TEX)|CYCLES|CYCLE|DEFAULT|DELAY|DIGITS|DISTANCE|DIST|EACH_CYCLE_(C|F)|EACH_CYCLE|FLAGS|FLOOR_(ANGLE|OFFS_[X-Y]|TEX)|FOOT_HGT|FRAME|GENIUS|HBAR|HEIGHT|HSLIDER|IF_(ARISE|ARRIVED|DIVE|FAR|LEAVE|NEAR|RELEASE|TOUCH)|INDEX|LAYER|LEFT|LENGTH|MAP_COLOR|MASK|MAX|MIN|MIRROR|OFFSET_[X-Y]|OVLYS|PALFILE|PAN_MAP|PICTURE|POSITION|POS_[X-Y]|RADIANCE|RANGE|REL_(ANGLE|DIST)|RIGHT|SCALE_(XY|X|Y)|SCYCLES|SCYCLE|SCALE|SDIST|SIDES|SIDE|SIZE_[X-Y]|SKILL[1-8]|SPEED|STRINGS|SVDIST|SVOL|TARGET_(MAP|X|Y)|TARGET|TEXTURE[1-4]|THING_HGT|TITLE|TOP|TOUCH|TYPE|VAL|VBAR|VSLIDER|VSPEED|WAYPOINT|WINDOW|[X-Z][1-2]|[X-Z]))").Value);}

			if (Regex.IsMatch(Rest,"^((?i)(ACCEL|ADD_STRING|ADDT|ADD|AND|BEEP|BRANCH|BREAK|CALL|DROP|END|EXCLUSIVE|EXEC_RULES|EXIT|EXPLODE|FADE_PAL|FIND|FREEZE|GETMIDI|GOTO|IF_(ABOVE|BELOW|EQUAL|MAX|MIN|NEQUAL)|INKEY|INPORT|LEVEL|LIFT|LOAD_INFO|LOAD|LOCATE|MAP|MIDI_COM|NEXT_(MY_THERE|MY|THERE)|NOP|OUTPORT|PLACE|PLAY_(CD|DEMO|FLICFILE|FLIC|SONG_ONCE|SONG|SOUNDFILE|SOUND)|PRINTFILE|PRINT_(STRING|VALUE)|PUSH|RANDOMIZE|ROTATE|SAVE_(DEMO|INFO)|SCAN|SCREENSHOT|SETMIDI|SET_(ALL|INFO|SKILL|STRING)|SET|SHAKE|SHIFT|SHOOT|SKIP|STOP_(DEMO|FLIC|SOUND)|SUBT|SUB|TILT|TO_STRING|WAITT|WAIT_TICKS|WAIT))")){
				Results.Add (t_command);
				ResultsV.Add(Regex.Match(Rest,"^((?i)(ACCEL|ADD_STRING|ADDT|ADD|AND|BEEP|BRANCH|BREAK|CALL|DROP|END|EXCLUSIVE|EXEC_RULES|EXIT|EXPLODE|FADE_PAL|FIND|FREEZE|GETMIDI|GOTO|IF_(ABOVE|BELOW|EQUAL|MAX|MIN|NEQUAL)|INKEY|INPORT|LEVEL|LIFT|LOAD_INFO|LOAD|LOCATE|MAP|MIDI_COM|NEXT_(MY_THERE|MY|THERE)|NOP|OUTPORT|PLACE|PLAY_(CD|DEMO|FLICFILE|FLIC|SONG_ONCE|SONG|SOUNDFILE|SOUND)|PRINTFILE|PRINT_(STRING|VALUE)|PUSH|RANDOMIZE|ROTATE|SAVE_(DEMO|INFO)|SCAN|SCREENSHOT|SETMIDI|SET_(ALL|INFO|SKILL|STRING)|SET|SHAKE|SHIFT|SHOOT|SKIP|STOP_(DEMO|FLIC|SOUND)|SUBT|SUB|TILT|TO_STRING|WAITT|WAIT_TICKS|WAIT))").Value);}

			if (Regex.IsMatch(Rest,"^((?i)((EACH_TICK|EACH_SEC|PANELS|LAYERS|MESSAGES)\\.(1[0-6]|[1-9])))")){
				Results.Add (t_list);
				ResultsV.Add(Regex.Match(Rest,"^((?i)((EACH_TICK|EACH_SEC|PANELS|LAYERS|MESSAGES)\\.(1[0-6]|[1-9])))").Value);}

			if (Regex.IsMatch(Rest,"^((?i)(ACCELERATION|ACTIONS|ACTIVE_(NEXUS|OBJTICKS|TARGETS)|ACTOR_(CEIL_HGT|CLIMB|DIST|FLOOR_HGT|IMPACT_V[X-Z]|WIDTH)|APPEND_MODE|ASPECT|BLUR_MODE|BOUNCE_V[X-Y]|CDAUDIO_VOL|CD_TRACK|CHANNEL_[0-7]|CHANNEL|CLIPPED|CLIPPING|COLOR_(ACTORS|BORDER|PLAYER|THINGS|WALLS)|DARK_DIST|DEBUG_MODE|DELTA_ANGLE|ERROR|FLIC_FRAME|FLOOR_MODE|FORCE_(AHEAD|ROT|STRAFE|TILT|UP)|FRAME_COLOR|FRICTION|HALF_PI|HIT_(DIST|MINDIST|X|Y)|IMPACT_V(ROT|[X-Z])|INERTIA|INV_DIST|JOYSTICK_[X-Y]|JOY_(4|SENSE)|KEY_(ALT|ANY|APO|BKSL|BKSP|BRACKL|BRACKR|CAL|CAR|COMMA|CTRL|CUD|CUL|CUR|CUU|DEL|END|ENTER|EQUALS|ESC|HOME|INS|JOY4|MINUS|PAUSE|PERIOD|PGDN|PGUP|PLUS|SEMIC|SENSE|SHIFT|SLASH|SPACE|SZ|TAB|F(1[0-2]|[1-9])|[A-Z0-9])|LIGHT_DIST|LOAD_MODE|MAP_(CENTER[X-Y]|(EDGE_[X-Y][1-2])|LAYER|MAX[X-Y]|MIN[X-Y]|MODE|OFFS[X-Y]|ROT|SCALE)|MAX_DIST|MICKEY_[X-Y]|MINV_DIST|MOTION_BLUR|MOUSE_(ANGLE|CALM|LEFT|MIDDLE|MODE|MOVING|RIGHT|SENSE|TIME|X|Y)|MOVE_(ANGLE|MODE)|MUSIC_VOL|MY_(X[1-2]|Y[1-2]|Z[1-2]|X|Y)|PANEL_LAYER|PI|PLAYER_(ANGLE|ARC|CLIMB|COS|DEPTH|HGT|LAST_[X-Y]|LIGHT|MSIN|SIN|SIZE|SPEED|TILT|VROT|V[X-Z]|WIDTH|[X-Z])|PSOUND_(TONE|VOL)|REAL_SPEED|REMOTE_[0-1]|RENDER_MODE|SCREEN_(HGT|WIDTH|X|Y)|SECS|SHIFT_SENSE|SHOOT_(ANGLE|FAC|RANGE|SECTOR|X|Y)|SKIP_FRAMES|SKY_OFFS_[X-Y]|SLOPE_(AHEAD|SIDE|X|Y)|SOUND_VOL|SPANS|STEPS|STR_LEN|TEXT_LAYER|THING_(DIST|WIDTH)|TICKS|TIME_(ACTIONS|CLIPPING|CORR|DRAW|FAC|FBUFFER|SLICES|TARGETS|VERTICES)|TOUCH_(DIST|MODE|RANGE|STATE)|TWO_PI|WALK_(PERIOD|TIME)|WALK|WAVE_PERIOD|WAVE|PALANIM_DELAY))")){
				Results.Add (t_skill);
				ResultsV.Add(Regex.Match(Rest,"^((?i)(ACCELERATION|ACTIONS|ACTIVE_(NEXUS|OBJTICKS|TARGETS)|ACTOR_(CEIL_HGT|CLIMB|DIST|FLOOR_HGT|IMPACT_V[X-Z]|WIDTH)|APPEND_MODE|ASPECT|BLUR_MODE|BOUNCE_V[X-Y]|CDAUDIO_VOL|CD_TRACK|CHANNEL_[0-7]|CHANNEL|CLIPPED|CLIPPING|COLOR_(ACTORS|BORDER|PLAYER|THINGS|WALLS)|DARK_DIST|DEBUG_MODE|DELTA_ANGLE|ERROR|FLIC_FRAME|FLOOR_MODE|FORCE_(AHEAD|ROT|STRAFE|TILT|UP)|FRAME_COLOR|FRICTION|HALF_PI|HIT_(DIST|MINDIST|X|Y)|IMPACT_V(ROT|[X-Z])|INERTIA|INV_DIST|JOYSTICK_[X-Y]|JOY_(4|SENSE)|KEY_(ALT|ANY|APO|BKSL|BKSP|BRACKL|BRACKR|CAL|CAR|COMMA|CTRL|CUD|CUL|CUR|CUU|DEL|END|ENTER|EQUALS|ESC|HOME|INS|JOY4|MINUS|PAUSE|PERIOD|PGDN|PGUP|PLUS|SEMIC|SENSE|SHIFT|SLASH|SPACE|SZ|TAB|F(1[0-2]|[1-9])|[A-Z0-9])|LIGHT_DIST|LOAD_MODE|MAP_(CENTER[X-Y]|(EDGE_[X-Y][1-2])|LAYER|MAX[X-Y]|MIN[X-Y]|MODE|OFFS[X-Y]|ROT|SCALE)|MAX_DIST|MICKEY_[X-Y]|MINV_DIST|MOTION_BLUR|MOUSE_(ANGLE|CALM|LEFT|MIDDLE|MODE|MOVING|RIGHT|SENSE|TIME|X|Y)|MOVE_(ANGLE|MODE)|MUSIC_VOL|MY_(X[1-2]|Y[1-2]|Z[1-2]|X|Y)|PANEL_LAYER|PI|PLAYER_(ANGLE|ARC|CLIMB|COS|DEPTH|HGT|LAST_[X-Y]|LIGHT|MSIN|SIN|SIZE|SPEED|TILT|VROT|V[X-Z]|WIDTH|[X-Z])|PSOUND_(TONE|VOL)|REAL_SPEED|REMOTE_[0-1]|RENDER_MODE|SCREEN_(HGT|WIDTH|X|Y)|SECS|SHIFT_SENSE|SHOOT_(ANGLE|FAC|RANGE|SECTOR|X|Y)|SKIP_FRAMES|SKY_OFFS_[X-Y]|SLOPE_(AHEAD|SIDE|X|Y)|SOUND_VOL|SPANS|STEPS|STR_LEN|TEXT_LAYER|THING_(DIST|WIDTH)|TICKS|TIME_(ACTIONS|CLIPPING|CORR|DRAW|FAC|FBUFFER|SLICES|TARGETS|VERTICES)|TOUCH_(DIST|MODE|RANGE|STATE)|TWO_PI|WALK_(PERIOD|TIME)|WALK|WAVE_PERIOD|WAVE|PALANIM_DELAY))").Value);}

			if (Regex.IsMatch(Rest,"^((?i)(THERE|MY|HIT|TOUCH_TEXT|TOUCHED|TOUCH_TEX|TOUCH_REG|COMMAND_LINE|REGION[1-8]))")){
				Results.Add (t_synonym);
				ResultsV.Add(Regex.Match(Rest,"^((?i)(THERE|MY|HIT|TOUCH_TEXT|TOUCHED|TOUCH_TEX|TOUCH_REG|COMMAND_LINE|REGION[1-8]))").Value);}

			if (Regex.IsMatch(Rest,"^((?i)CLIP_DIST)")){
				Results.Add (t_ambigChar95globalChar95property);
				ResultsV.Add(Regex.Match(Rest,"^((?i)CLIP_DIST)").Value);}

			if (Regex.IsMatch(Rest,"^((?i)(EACH_TICK|IF_ENTER|IF_HIT|IF_KLICK))")){
				Results.Add (t_ambigChar95eventChar95property);
				ResultsV.Add(Regex.Match(Rest,"^((?i)(EACH_TICK|IF_ENTER|IF_HIT|IF_KLICK))").Value);}

			if (Regex.IsMatch(Rest,"^((?i)(THING|ACTOR))")){
				Results.Add (t_ambigChar95objectChar95flag);
				ResultsV.Add(Regex.Match(Rest,"^((?i)(THING|ACTOR))").Value);}

			if (Regex.IsMatch(Rest,"^((?i)(SIN|ASIN|SQRT|ABS))")){
				Results.Add (t_ambigChar95mathChar95command);
				ResultsV.Add(Regex.Match(Rest,"^((?i)(SIN|ASIN|SQRT|ABS))").Value);}

			if (Regex.IsMatch(Rest,"^((?i)RANDOM)")){
				Results.Add (t_ambigChar95mathChar95skillChar95property);
				ResultsV.Add(Regex.Match(Rest,"^((?i)RANDOM)").Value);}

			if (Regex.IsMatch(Rest,"^((?i)HERE)")){
				Results.Add (t_ambigChar95synonymChar95flag);
				ResultsV.Add(Regex.Match(Rest,"^((?i)HERE)").Value);}

			if (Regex.IsMatch(Rest,"^((?i)(FLOOR_HGT|CEIL_HGT|AMBIENT|RESULT|NODE))")){
				Results.Add (t_ambigChar95skillChar95property);
				ResultsV.Add(Regex.Match(Rest,"^((?i)(FLOOR_HGT|CEIL_HGT|AMBIENT|RESULT|NODE))").Value);}

			if (Regex.IsMatch(Rest,"^((?i)SAVE)")){
				Results.Add (t_ambigChar95commandChar95flag);
				ResultsV.Add(Regex.Match(Rest,"^((?i)SAVE)").Value);}

			if (Regex.IsMatch(Rest,"^((?i)MSPRITE)")){
				Results.Add (t_ambigChar95globalChar95synonymChar95property);
				ResultsV.Add(Regex.Match(Rest,"^((?i)MSPRITE)").Value);}

			if (Regex.IsMatch(Rest,"^((?i)DO)")){
				Results.Add (t_ambigChar95commandChar95property);
				ResultsV.Add(Regex.Match(Rest,"^((?i)DO)").Value);}

			if (Regex.IsMatch(Rest,"^((?i)(FLAG[1-8]))")){
				Results.Add (t_ambigChar95skillChar95flag);
				ResultsV.Add(Regex.Match(Rest,"^((?i)(FLAG[1-8]))").Value);}

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
