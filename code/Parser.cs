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
        List<int> tList = new List<int>();
        List<Regex> rList = new List<Regex>();
        MyCompiler()
        {
            tList.Add(t_IFDEF);
            rList.Add(new Regex("\\G((?i)IFDEF)"));
            tList.Add(t_Char59);
            rList.Add(new Regex("\\G(;)"));
            tList.Add(t_IFNDEF);
            rList.Add(new Regex("\\G((?i)IFNDEF)"));
            tList.Add(t_IFELSE);
            rList.Add(new Regex("\\G((?i)IFELSE)"));
            tList.Add(t_ENDIF);
            rList.Add(new Regex("\\G((?i)ENDIF)"));
            tList.Add(t_DEFINE);
            rList.Add(new Regex("\\G((?i)DEFINE)"));
            tList.Add(t_UNDEF);
            rList.Add(new Regex("\\G((?i)UNDEF)"));
            tList.Add(t_INCLUDE);
            rList.Add(new Regex("\\G((?i)INCLUDE)"));
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
            rList.Add(new Regex("\\G((?i)RULE)"));
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
            rList.Add(new Regex("\\G((?i)ELSE)"));
            tList.Add(t_IF);
            rList.Add(new Regex("\\G((?i)IF)"));
            tList.Add(t_WHILE);
            rList.Add(new Regex("\\G((?i)WHILE)"));
            tList.Add(t_Char46);
            rList.Add(new Regex("\\G(\\.)"));
            tList.Add(t_NULL);
            rList.Add(new Regex("\\G((?i)NULL)"));
            tList.Add(t_event);
            rList.Add(new Regex("\\G((?i)(EACH_SEC|IF_(AE|ALT|ANYKEY|APO|BKSL|BKSP|BRACKL|BRACKR|CAL|CAR|COMMA|CTRL|CUD|CUL|CUR|CUU|DEL|END|EQUALS|ESC|HOME|INS|JOY4|LEFT|LOAD|MIDDLE|MINUS|MSTOP|OE|PAUSE|PERIOD|PGDN|PGUP|PLUS|RIGHT|SEMIC|SLASH|SPACE|START|SZ|TAB|TAST|UE|F(1[0-2]|[1-9])|[0-9A-Z])|LAYERS|MESSAGES|PANELS))"));
            tList.Add(t_global);
            rList.Add(new Regex("\\G((?i)(VIDEO|NEXUS|LIGHT_ANGLE|IBANK|DRUMBANK|MIDI_PITCH|BIND|MAPFILE|SAVEDIR|PATH|DITHER|SAVE_KEYS|REMOTE_KEYS))"));
            tList.Add(t_asset);
            rList.Add(new Regex("\\G((?i)(MODEL|SOUND|MUSIC|FLIC|BMAP|OVLY|FONT))"));
            tList.Add(t_object);
            rList.Add(new Regex("\\G((?i)(OVERLAY|PANEL|PALETTE|REGION|SKILL|STRING|SYNONYM|TEXTURE|TEXT|VIEW|WALL|WAY))"));
            tList.Add(t_function);
            rList.Add(new Regex("\\G((?i)(ACTION|RULES))"));
            tList.Add(t_math);
            rList.Add(new Regex("\\G((?i)(ACOS|COS|ATAN|TAN|SIGN|INT|EXP|LOG10|LOG2|LOG))"));
            tList.Add(t_flag);
            rList.Add(new Regex("\\G((?i)(FLOOR_DESCEND|CEIL_DESCEND|FLOOR_ASCEND|FLOOR_LIFTED|CEIL_ASCEND|CEIL_LIFTED|TRANSPARENT|CANDELABER|DIAPHANOUS|IMMATERIAL|IMPASSABLE|PORTCULLIS|AUTORANGE|CAREFULLY|CONDENSED|INVISIBLE|SENSITIVE|BERKELEY|CENTER_X|CENTER_Y|LIGHTMAP|PASSABLE|SAVE_ALL|CLUSTER|CURTAIN|FRAGILE|NO_CLIP|ONESHOT|REFRESH|VISIBLE|ABSPOS|BEHIND|GROUND|LIFTED|MASTER|NARROW|RELPOS|SHADOW|STICKY|FENCE|GHOST|LIBER|MOVED|SLOOP|BASE|BLUR|CLIP|FLAT|HARD|PLAY|SEEN|WIRE|FAR|SKY))"));
            tList.Add(t_property);
            rList.Add(new Regex("\\G((?i)(ALBEDO|ANGLE|ASPEED|ATTACH|BELOW|BMAPS|BUTTON|CEIL_(ANGLE|OFFS_[X-Y]|TEX)|CYCLES|CYCLE|DEFAULT|DELAY|DIGITS|DISTANCE|DIST|EACH_CYCLE_(C|F)|EACH_CYCLE|FLAGS|FLOOR_(ANGLE|OFFS_[X-Y]|TEX)|FOOT_HGT|FRAME|GENIUS|HBAR|HEIGHT|HSLIDER|IF_(ARISE|ARRIVED|DIVE|FAR|LEAVE|NEAR|RELEASE|TOUCH)|INDEX|LAYER|LEFT|LENGTH|MAP_COLOR|MASK|MAX|MIN|MIRROR|OFFSET_[X-Y]|OVLYS|PALFILE|PAN_MAP|PICTURE|POSITION|POS_[X-Y]|RADIANCE|RANGE|REL_(ANGLE|DIST)|RIGHT|SCALE_(XY|X|Y)|SCYCLES|SCYCLE|SCALE|SDIST|SIDES|SIDE|SIZE_[X-Y]|SKILL[1-8]|SPEED|STRINGS|SVDIST|SVOL|TARGET_(MAP|X|Y)|TARGET|TEXTURE[1-4]|THING_HGT|TITLE|TOP|TOUCH|TYPE|VAL|VBAR|VSLIDER|VSPEED|WAYPOINT|WINDOW|[X-Z][1-2]|[X-Z]))"));
            tList.Add(t_command);
            rList.Add(new Regex("\\G((?i)(ACCEL|ADD_STRING|ADDT|ADD|AND|BEEP|BRANCH|BREAK|CALL|DROP|END|EXCLUSIVE|EXEC_RULES|EXIT|EXPLODE|FADE_PAL|FIND|FREEZE|GETMIDI|GOTO|IF_(ABOVE|BELOW|EQUAL|MAX|MIN|NEQUAL)|INKEY|INPORT|LEVEL|LIFT|LOAD_INFO|LOAD|LOCATE|MAP|MIDI_COM|NEXT_(MY_THERE|MY|THERE)|NOP|OUTPORT|PLACE|PLAY_(CD|DEMO|FLICFILE|FLIC|SONG_ONCE|SONG|SOUNDFILE|SOUND)|PRINTFILE|PRINT_(STRING|VALUE)|PUSH|RANDOMIZE|ROTATE|SAVE_(DEMO|INFO)|SCAN|SCREENSHOT|SETMIDI|SET_(ALL|INFO|SKILL|STRING)|SET|SHAKE|SHIFT|SHOOT|SKIP|STOP_(DEMO|FLIC|SOUND)|SUBT|SUB|TILT|TO_STRING|WAITT|WAIT_TICKS|WAIT))"));
            tList.Add(t_list);
            rList.Add(new Regex("\\G((?i)((EACH_TICK|EACH_SEC|PANELS|LAYERS|MESSAGES)\\.(1[0-6]|[1-9])))"));
            tList.Add(t_skill);
            rList.Add(new Regex("\\G((?i)(ACCELERATION|ACTIONS|ACTIVE_(NEXUS|OBJTICKS|TARGETS)|ACTOR_(CEIL_HGT|CLIMB|DIST|FLOOR_HGT|IMPACT_V[X-Z]|WIDTH)|APPEND_MODE|ASPECT|BLUR_MODE|BOUNCE_V[X-Y]|CDAUDIO_VOL|CD_TRACK|CHANNEL_[0-7]|CHANNEL|CLIPPED|CLIPPING|COLOR_(ACTORS|BORDER|PLAYER|THINGS|WALLS)|DARK_DIST|DEBUG_MODE|DELTA_ANGLE|ERROR|FLIC_FRAME|FLOOR_MODE|FORCE_(AHEAD|ROT|STRAFE|TILT|UP)|FRAME_COLOR|FRICTION|HALF_PI|HIT_(DIST|MINDIST|X|Y)|IMPACT_V(ROT|[X-Z])|INERTIA|INV_DIST|JOYSTICK_[X-Y]|JOY_(4|SENSE)|KEY_(ALT|ANY|APO|BKSL|BKSP|BRACKL|BRACKR|CAL|CAR|COMMA|CTRL|CUD|CUL|CUR|CUU|DEL|END|ENTER|EQUALS|ESC|HOME|INS|JOY4|MINUS|PAUSE|PERIOD|PGDN|PGUP|PLUS|SEMIC|SENSE|SHIFT|SLASH|SPACE|SZ|TAB|F(1[0-2]|[1-9])|[A-Z0-9])|LIGHT_DIST|LOAD_MODE|MAP_(CENTER[X-Y]|(EDGE_[X-Y][1-2])|LAYER|MAX[X-Y]|MIN[X-Y]|MODE|OFFS[X-Y]|ROT|SCALE)|MAX_DIST|MICKEY_[X-Y]|MINV_DIST|MOTION_BLUR|MOUSE_(ANGLE|CALM|LEFT|MIDDLE|MODE|MOVING|RIGHT|SENSE|TIME|X|Y)|MOVE_(ANGLE|MODE)|MUSIC_VOL|MY_(X[1-2]|Y[1-2]|Z[1-2]|X|Y)|PANEL_LAYER|PI|PLAYER_(ANGLE|ARC|CLIMB|COS|DEPTH|HGT|LAST_[X-Y]|LIGHT|MSIN|SIN|SIZE|SPEED|TILT|VROT|V[X-Z]|WIDTH|[X-Z])|PSOUND_(TONE|VOL)|REAL_SPEED|REMOTE_[0-1]|RENDER_MODE|SCREEN_(HGT|WIDTH|X|Y)|SECS|SHIFT_SENSE|SHOOT_(ANGLE|FAC|RANGE|SECTOR|X|Y)|SKIP_FRAMES|SKY_OFFS_[X-Y]|SLOPE_(AHEAD|SIDE|X|Y)|SOUND_VOL|SPANS|STEPS|STR_LEN|TEXT_LAYER|THING_(DIST|WIDTH)|TICKS|TIME_(ACTIONS|CLIPPING|CORR|DRAW|FAC|FBUFFER|SLICES|TARGETS|VERTICES)|TOUCH_(DIST|MODE|RANGE|STATE)|TWO_PI|WALK_(PERIOD|TIME)|WALK|WAVE_PERIOD|WAVE|PALANIM_DELAY))"));
            tList.Add(t_synonym);
            rList.Add(new Regex("\\G((?i)(THERE|MY|HIT|TOUCH_TEXT|TOUCHED|TOUCH_TEX|TOUCH_REG|COMMAND_LINE|REGION[1-8]))"));
            tList.Add(t_ambigChar95globalChar95property);
            rList.Add(new Regex("\\G((?i)CLIP_DIST)"));
            tList.Add(t_ambigChar95eventChar95property);
            rList.Add(new Regex("\\G((?i)(EACH_TICK|IF_ENTER|IF_HIT|IF_KLICK))"));
            tList.Add(t_ambigChar95objectChar95flag);
            rList.Add(new Regex("\\G((?i)(THING|ACTOR))"));
            tList.Add(t_ambigChar95mathChar95command);
            rList.Add(new Regex("\\G((?i)(SIN|ASIN|SQRT|ABS))"));
            tList.Add(t_ambigChar95mathChar95skillChar95property);
            rList.Add(new Regex("\\G((?i)RANDOM)"));
            tList.Add(t_ambigChar95synonymChar95flag);
            rList.Add(new Regex("\\G((?i)HERE)"));
            tList.Add(t_ambigChar95skillChar95property);
            rList.Add(new Regex("\\G((?i)(FLOOR_HGT|CEIL_HGT|AMBIENT|RESULT|NODE))"));
            tList.Add(t_ambigChar95commandChar95flag);
            rList.Add(new Regex("\\G((?i)SAVE)"));
            tList.Add(t_ambigChar95globalChar95synonymChar95property);
            rList.Add(new Regex("\\G((?i)MSPRITE)"));
            tList.Add(t_ambigChar95commandChar95property);
            rList.Add(new Regex("\\G((?i)DO)"));
            tList.Add(t_ambigChar95skillChar95flag);
            rList.Add(new Regex("\\G((?i)(FLAG[1-8]))"));
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
            rList.Add(new Regex("\\G([\\r\\n\\t\\s\\x00,]|:=|(#.*(\\n|$))|(//.*(\\n|$))|(/\\*(.|[\\r\\n])*?\\*/))"));
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
                int t_IFDEF = 257;
                int t_Char59 = 258;
                int t_IFNDEF = 259;
                int t_IFELSE = 260;
                int t_ENDIF = 261;
                int t_DEFINE = 262;
                int t_UNDEF = 263;
                int t_INCLUDE = 264;
                int t_Char123 = 265;
                int t_Char125 = 266;
                int t_Char58 = 267;
                int t_Char124Char124 = 268;
                int t_Char38Char38 = 269;
                int t_Char124 = 270;
                int t_Char94 = 271;
                int t_Char38 = 272;
                int t_Char40 = 273;
                int t_Char41 = 274;
                int t_Char33Char61 = 275;
                int t_Char61Char61 = 276;
                int t_Char60 = 277;
                int t_Char60Char61 = 278;
                int t_Char62 = 279;
                int t_Char62Char61 = 280;
                int t_Char43 = 281;
                int t_Char45 = 282;
                int t_Char37 = 283;
                int t_Char42 = 284;
                int t_Char47 = 285;
                int t_Char33 = 286;
                int t_RULE = 287;
                int t_Char42Char61 = 288;
                int t_Char43Char61 = 289;
                int t_Char45Char61 = 290;
                int t_Char47Char61 = 291;
                int t_Char61 = 292;
                int t_ELSE = 293;
                int t_IF = 294;
                int t_WHILE = 295;
                int t_Char46 = 296;
                int t_NULL = 297;
                int t_event = 298;
                int t_global = 299;
                int t_asset = 300;
                int t_object = 301;
                int t_function = 302;
                int t_math = 303;
                int t_flag = 304;
                int t_property = 305;
                int t_command = 306;
                int t_list = 307;
                int t_skill = 308;
                int t_synonym = 309;
                int t_ambigChar95globalChar95property = 310;
                int t_ambigChar95eventChar95property = 311;
                int t_ambigChar95objectChar95flag = 312;
                int t_ambigChar95mathChar95command = 313;
                int t_ambigChar95mathChar95skillChar95property = 314;
                int t_ambigChar95synonymChar95flag = 315;
                int t_ambigChar95skillChar95property = 316;
                int t_ambigChar95commandChar95flag = 317;
                int t_ambigChar95globalChar95synonymChar95property = 318;
                int t_ambigChar95commandChar95property = 319;
                int t_ambigChar95skillChar95flag = 320;
                int t_integer = 321;
                int t_fixed = 322;
                int t_identifier = 323;
                int t_file = 324;
                int t_string = 325;
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
            Console.WriteLine(inputstream);
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
         yyval = yyv[yysp-0];
         
       break;
							case   10 : 
         yyval = yyv[yysp-0];
         
       break;
							case   11 : 
         yyval = Sections.AddActionSection(yyv[yysp-0]);
         
       break;
							case   12 : 
         yyval = Sections.AddObjectSection(yyv[yysp-0]);
         
       break;
							case   13 : 
         yyval = Sections.AddDefineSection(yyv[yysp-0]);
         
       break;
							case   14 : 
         yyval = Sections.AddAssetSection(yyv[yysp-0]);
         
       break;
							case   15 : 
         yyval = Sections.CreatePreProcIfCondition(yyv[yysp-3], yyv[yysp-1]);
         
       break;
							case   16 : 
         yyval = Sections.CreatePreProcIfNotCondition(yyv[yysp-3], yyv[yysp-1]);
         
       break;
							case   17 : 
         yyval = Sections.CreatePreProcElseCondition(yyv[yysp-4], yyv[yysp-1]);
         
       break;
							case   18 : 
         yyval = Sections.CreatePreProcEndCondition(yyv[yysp-1]);
         
       break;
							case   19 : 
         yyval = Actions.CreatePreProcIfCondition(yyv[yysp-1], yyv[yysp-0]);
         
       break;
							case   20 : 
         yyval = Actions.CreatePreProcIfNotCondition(yyv[yysp-1], yyv[yysp-0]);
         
       break;
							case   21 : 
         yyval = Actions.CreatePreProcElseCondition(yyv[yysp-3], yyv[yysp-1]);
         
       break;
							case   22 : 
         yyval = Actions.CreatePreProcEndCondition(yyv[yysp-1]);
         
       break;
							case   23 : 
         yyval = Objects.CreatePreProcIfCondition(yyv[yysp-3], yyv[yysp-1]);
         
       break;
							case   24 : 
         yyval = Objects.CreatePreProcIfNotCondition(yyv[yysp-3], yyv[yysp-1]);
         
       break;
							case   25 : 
         yyval = Objects.CreatePreProcElseCondition(yyv[yysp-4], yyv[yysp-1]);
         
       break;
							case   26 : 
         yyval = Objects.CreatePreProcEndCondition(yyv[yysp-1]);
         
       break;
							case   27 : 
         yyval = Defines.AddTransform(yyv[yysp-2]);
         
       break;
							case   28 : 
         yyval = Defines.AddDefine(yyv[yysp-1]);
         
       break;
							case   29 : 
         yyval = Defines.RemoveDefine(yyv[yysp-1]);
         
       break;
							case   30 : 
         yyval = Formatter.FormatPreprocessor(yyv[yysp-0]);
         
       break;
							case   31 : 
         yyval = Formatter.FormatPreprocessor(yyv[yysp-0]);
         
       break;
							case   32 : 
         yyval = "";
         Defines.AddStringDefine(yyv[yysp-0]);
         
       break;
							case   33 : 
         yyval = "";
         Defines.AddListDefine(yyv[yysp-0]);
         
       break;
							case   34 : 
         yyval = "";
         Defines.AddFileDefine(yyv[yysp-0]);
         
       break;
							case   35 : 
         yyval = "";
         Defines.AddNumberDefine(yyv[yysp-0]);
         
       break;
							case   36 : 
         yyval = "";
         Defines.AddKeywordDefine(yyv[yysp-0]);
         
       break;
							case   37 : 
         yyval = Include.Process(yyv[yysp-1]);
         
       break;
							case   38 : 
         yyval = Globals.AddEvent(yyv[yysp-2]);
         
       break;
							case   39 : 
         yyval = Globals.AddGlobal(yyv[yysp-2], yyv[yysp-1]);
         
       break;
							case   40 : 
         yyval = Formatter.FormatGlobal(yyv[yysp-0]);
         
       break;
							case   41 : 
         yyval = Formatter.FormatGlobal(yyv[yysp-0]);
         
       break;
							case   42 : 
         yyval = Formatter.FormatGlobal(yyv[yysp-0]);
         
       break;
							case   43 : 
         yyval = Formatter.FormatTargetSkill(yyv[yysp-0]);
         
       break;
							case   44 : 
         yyval = Formatter.FormatEvent(yyv[yysp-0]);
         
       break;
							case   45 : 
         yyval = Formatter.FormatEvent(yyv[yysp-0]);
         
       break;
							case   46 : 
         yyval = "";
         Globals.AddParameter(yyv[yysp-0]);
         
       break;
							case   47 : 
         yyval = "";
         Globals.AddParameter(yyv[yysp-1]);
         
       break;
							case   48 : 
         yyval = yyv[yysp-0];
         
       break;
							case   49 : 
         yyval = yyv[yysp-0];
         
       break;
							case   50 : 
         yyval = yyv[yysp-0];
         
       break;
							case   51 : 
         yyval = yyv[yysp-0];
         
       break;
							case   52 : 
         yyval = Assets.AddAsset(yyv[yysp-4], yyv[yysp-3], yyv[yysp-2]);
         
       break;
							case   53 : 
         yyval = "";
         Assets.AddParameter(yyv[yysp-1]);
         
       break;
							case   54 : 
         yyval = "";
         
       break;
							case   55 : 
         yyval = yyv[yysp-0];
         
       break;
							case   56 : 
         yyval = yyv[yysp-0];
         
       break;
							case   57 : 
         yyval = yyv[yysp-0];
         
       break;
							case   58 : 
         yyval = Objects.AddObject(yyv[yysp-4], yyv[yysp-3], yyv[yysp-1]);
         
       break;
							case   59 : 
         yyval = Objects.AddStringObject(yyv[yysp-3], yyv[yysp-2], yyv[yysp-1]);
         
       break;
							case   60 : 
         yyval = Objects.AddObject(yyv[yysp-2], yyv[yysp-1]);
         
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
         yyval = yyv[yysp-1] + yyv[yysp-0];
         
       break;
							case   65 : 
         yyval = yyv[yysp-1] + yyv[yysp-0];
         
       break;
							case   66 : 
         yyval = "";
         
       break;
							case   67 : 
         yyval = Objects.CreateProperty(yyv[yysp-2]);
         
       break;
							case   68 : 
         yyval = "";
         
       break;
							case   69 : 
         Objects.AddPropertyValue(yyv[yysp-0]);
         yyval = "";
         
       break;
							case   70 : 
         Objects.AddPropertyValue(yyv[yysp-1]);
         yyval = "";
         
       break;
							case   71 : 
         yyval = yyv[yysp-0];
         
       break;
							case   72 : 
         yyval = yyv[yysp-0];
         
       break;
							case   73 : 
         yyval = yyv[yysp-0];
         
       break;
							case   74 : 
         yyval = yyv[yysp-0];
         
       break;
							case   75 : 
         yyval = Actions.AddAction(yyv[yysp-3], yyv[yysp-1]);
         
       break;
							case   76 : 
         yyval = yyv[yysp-0];
         
       break;
							case   77 : 
         yyval = Actions.CreateMarker(yyv[yysp-2], yyv[yysp-0]);
         
       break;
							case   78 : 
         yyval = yyv[yysp-2] + yyv[yysp-0];
         
       break;
							case   79 : 
         yyval = Actions.CreateMarker(yyv[yysp-2], yyv[yysp-0]);
         
       break;
							case   80 : 
         yyval = yyv[yysp-1] + yyv[yysp-0];
         
       break;
							case   81 : 
         yyval = yyv[yysp-1] + yyv[yysp-0];
         
       break;
							case   82 : 
         yyval = yyv[yysp-1] + yyv[yysp-0];
         
       break;
							case   83 : 
         yyval = "";
         
       break;
							case   84 : 
         yyval = Actions.CreateInstruction(yyv[yysp-2]);
         
       break;
							case   85 : 
         //Capture and discard bogus code
         yyval = Actions.CreateInvalidInstruction(yyv[yysp-2]);
         
       break;
							case   86 : 
         yyval = yyv[yysp-1];
         
       break;
							case   87 :
                    yyval = Actions.CreateInstruction(yyv[yysp-1]);
         
       break;
							case   88 : 
         yyval = "";
         
       break;
							case   89 :
                    yyval = "";// Sections.AddDefineSection(yyv[yysp-0]);
                    //this causes invalid nesting, breaking the formatter

                    break;
							case   90 : 
         yyval = "";
         Actions.AddInstructionParameter(yyv[yysp-0]);
         
       break;
							case   91 : 
         yyval = "";
         Actions.AddInstructionParameter(yyv[yysp-1]);
         
       break;
							case   92 : 
         yyval = yyv[yysp-0];
         
       break;
							case   93 : 
         yyval = yyv[yysp-0];
         
       break;
							case   94 : 
         yyval = yyv[yysp-0];
         
       break;
							case   95 : 
         yyval = yyv[yysp-0];
         
       break;
							case   96 : 
         yyval = yyv[yysp-0];
         
       break;
							case   97 : 
         yyval = yyv[yysp-0];
         
       break;
							case   98 : 
         yyval = yyv[yysp-2] + " || " + yyv[yysp-0];
         
       break;
							case   99 : 
         yyval = yyv[yysp-0];
         
       break;
							case  100 : 
         yyval = yyv[yysp-2] + " && " + yyv[yysp-0];
         
       break;
							case  101 : 
         yyval = yyv[yysp-0];
         
       break;
							case  102 : 
         yyval = yyv[yysp-2] + " | " + yyv[yysp-0];
         
       break;
							case  103 : 
         yyval = yyv[yysp-0];
         
       break;
							case  104 : 
         yyval = yyv[yysp-2] + " ^ " + yyv[yysp-0];
         
       break;
							case  105 : 
         yyval = yyv[yysp-0];
         
       break;
							case  106 : 
         yyval = yyv[yysp-2] + " & " + yyv[yysp-0];
         
       break;
							case  107 : 
         yyval = yyv[yysp-0];
         
       break;
							case  108 : 
         yyval = yyv[yysp-0];
         
       break;
							case  109 : 
         yyval = yyv[yysp-2] + yyv[yysp-1] + yyv[yysp-0];
         
       break;
							case  110 : 
         yyval = yyv[yysp-0];
         
       break;
							case  111 : 
         yyval = yyv[yysp-2] + yyv[yysp-1] + yyv[yysp-0];
         
       break;
							case  112 : 
         yyval = yyv[yysp-0];
         
       break;
							case  113 : 
         yyval = yyv[yysp-2] + yyv[yysp-1] + yyv[yysp-0];
         
       break;
							case  114 : 
         yyval = yyv[yysp-0];
         
       break;
							case  115 : 
         yyval = yyv[yysp-2] + yyv[yysp-1] + yyv[yysp-0];
         
       break;
							case  116 : 
         yyval = yyv[yysp-0];
         
       break;
							case  117 : 
         yyval = yyv[yysp-1] + yyv[yysp-0];
         
       break;
							case  118 : 
         //yyval = yyv[yysp-1] + "." + yyv[yysp-0]); //fixes things like "18,4"
         Console.WriteLine("(W) PARSER discarded superfluous token in expression: , " + yyv[yysp-0]);
         yyval = yyv[yysp-1]; //this is what supposedly happens in A3
         
       break;
							case  119 : 
         yyval = Formatter.FormatMath(yyv[yysp-3]) + "(" + yyv[yysp-1] + ")";
         
       break;
							case  120 : 
         yyval = "(" + yyv[yysp-1] + ")";
         
       break;
							case  121 : 
         //yyval = Formatter.FormatTargetSkill(yyv[yysp-1] + yyv[yysp-0]); //fixes things like "Skill 6"
         Console.WriteLine("(W) PARSER discarded superfluous token in expression: " + yyv[yysp-1]);
         yyval = yyv[yysp-0]; //this is what supposedly happens in A3
         
       break;
							case  122 : 
         yyval = yyv[yysp-0];
         
       break;
							case  123 : 
         yyval = Formatter.FormatTargetSkill(yyv[yysp-0]);
         
       break;
							case  124 : 
         yyval = yyv[yysp-0];
         
       break;
							case  125 : 
         yyval = " != ";
         
       break;
							case  126 : 
         yyval = " == ";
         
       break;
							case  127 : 
         yyval = " < ";
         
       break;
							case  128 : 
         yyval = " <= ";
         
       break;
							case  129 : 
         yyval = " > ";
         
       break;
							case  130 : 
         yyval = " >= ";
         
       break;
							case  131 : 
         yyval = " + ";
         
       break;
							case  132 : 
         yyval = " - ";
         
       break;
							case  133 : 
         yyval = " % ";
         
       break;
							case  134 : 
         yyval = " * ";
         
       break;
							case  135 : 
         yyval = " / ";
         
       break;
							case  136 : 
         yyval = "!";
         
       break;
							case  137 : 
         yyval = "+";
         
       break;
							case  138 : 
         yyval = "-";
         
       break;
							case  139 : 
         //yyval = yyv[yysp-1] + yyv[yysp-0];
         yyval = Actions.CreateExpression(yyv[yysp-0]);
         
       break;
							case  140 : 
         //yyval = yyv[yysp-3] + yyv[yysp-2] + yyv[yysp-1] + yyv[yysp-0];
         yyval = Actions.CreateExpression(Formatter.FormatExpression(yyv[yysp-2], yyv[yysp-1], yyv[yysp-0]));
         
       break;
							case  141 : 
         //yyval = yyv[yysp-2] + yyv[yysp-1] + yyv[yysp-0];
         yyval = Actions.CreateExpression(Formatter.FormatExpression(yyv[yysp-2], yyv[yysp-1], yyv[yysp-0]));
         
       break;
							case  142 : 
         yyval = " *= ";
         
       break;
							case  143 : 
         yyval = " += ";
         
       break;
							case  144 : 
         yyval = " -= ";
         
       break;
							case  145 : 
         yyval = " /= ";
         
       break;
							case  146 : 
         yyval = " = ";
         
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
         yyval = Actions.CreateElseCondition(yyv[yysp-1]);
         
       break;
							case  151 : 
         yyval = Actions.CreateIfCondition(yyv[yysp-3], yyv[yysp-1]);
         
       break;
							case  152 : 
         yyval = Actions.CreateWhileCondition(yyv[yysp-3], yyv[yysp-1]);
         
       break;
							case  153 : 
         yyval = yyv[yysp-2] + "." + yyv[yysp-0];
         
       break;
							case  154 : 
         yyval = yyv[yysp-2] + "." + yyv[yysp-0];
         
       break;
							case  155 : 
         yyval = yyv[yysp-0];
         
       break;
							case  156 : 
         yyval = yyv[yysp-0];
         
       break;
							case  157 : 
         yyval = yyv[yysp-1] + yyv[yysp-0];
         
       break;
							case  158 : 
         yyval = Formatter.FormatProperty(yyv[yysp-0]);
         
       break;
							case  159 : 
         yyval = Formatter.FormatProperty(yyv[yysp-0]);
         
       break;
							case  160 : 
         yyval = Formatter.FormatProperty(yyv[yysp-0]);
         
       break;
							case  161 : 
         yyval = Formatter.FormatProperty(yyv[yysp-0]);
         
       break;
							case  162 : 
         yyval = Formatter.FormatProperty(yyv[yysp-0]);
         
       break;
							case  163 : 
         yyval = Formatter.FormatProperty(yyv[yysp-0]);
         
       break;
							case  164 : 
         yyval = Formatter.FormatProperty(yyv[yysp-0]);
         
       break;
							case  165 : 
         yyval = Formatter.FormatProperty(yyv[yysp-0]);
         
       break;
							case  166 : 
         yyval = Formatter.FormatProperty(yyv[yysp-0]);
         
       break;
							case  167 : 
         yyval = Formatter.FormatProperty(yyv[yysp-0]);
         
       break;
							case  168 : 
         yyval = Formatter.FormatProperty(yyv[yysp-0]);
         
       break;
							case  169 : 
         yyval = Formatter.FormatProperty(yyv[yysp-0]);
         
       break;
							case  170 : 
         yyval = Formatter.FormatProperty(yyv[yysp-0]);
         
       break;
							case  171 : 
         yyval = Formatter.FormatProperty(yyv[yysp-0]);
         
       break;
							case  172 : 
         yyval = Formatter.FormatProperty(yyv[yysp-0]);
         
       break;
							case  173 : 
         yyval = Formatter.FormatCommand(yyv[yysp-0]);
         
       break;
							case  174 : 
         yyval = Formatter.FormatCommand(yyv[yysp-0]);
         
       break;
							case  175 : 
         yyval = Formatter.FormatCommand(yyv[yysp-0]);
         
       break;
							case  176 : 
         yyval = Formatter.FormatCommand(yyv[yysp-0]);
         
       break;
							case  177 : 
         yyval = Formatter.FormatCommand(yyv[yysp-0]);
         
       break;
							case  178 : 
         yyval = Formatter.FormatNumber(yyv[yysp-0]);
         
       break;
							case  179 : 
         yyval = Formatter.FormatNumber(yyv[yysp-0]);
         
       break;
							case  180 : 
         yyval = yyv[yysp-0];
         
       break;
							case  181 : 
         yyval = yyv[yysp-0];
         
       break;
							case  182 : 
         yyval = yyv[yysp-1] + yyv[yysp-0];
         
       break;
							case  183 : 
         yyval = yyv[yysp-0];
         
       break;
							case  184 : 
         yyval = yyv[yysp-1] + yyv[yysp-0];
         
       break;
							case  185 : 
         yyval = yyv[yysp-0];
         
       break;
							case  186 : 
         yyval = Formatter.FormatIdentifier(yyv[yysp-0]);
         
       break;
							case  187 : 
         yyval = Formatter.FormatList(yyv[yysp-0]);
         
       break;
							case  188 : 
         yyval = Formatter.FormatSkill(yyv[yysp-0]);
         
       break;
							case  189 : 
         yyval = Formatter.FormatSkill(yyv[yysp-0]);
         
       break;
							case  190 : 
         yyval = Formatter.FormatSkill(yyv[yysp-0]);
         
       break;
							case  191 : 
         yyval = Formatter.FormatIdentifier(yyv[yysp-0]);
         
       break;
							case  192 : 
         yyval = Formatter.FormatIdentifier(yyv[yysp-0]);
         
       break;
							case  193 : 
         yyval = Formatter.FormatEvent(yyv[yysp-0]);
         
       break;
							case  194 : 
         yyval = Formatter.FormatGlobal(yyv[yysp-0]);
         
       break;
							case  195 : 
         yyval = Formatter.FormatIdentifier(yyv[yysp-0]);
         
       break;
							case  196 : 
         yyval = Formatter.FormatIdentifier(yyv[yysp-0]);
         
       break;
							case  197 : 
         yyval = Formatter.FormatIdentifier(yyv[yysp-0]);
         
       break;
							case  198 : 
         yyval = Formatter.FormatIdentifier(yyv[yysp-0]);
         
       break;
							case  199 : 
         yyval = Formatter.FormatEvent(yyv[yysp-0]);
         
       break;
							case  200 : 
         yyval = Formatter.FormatIdentifier(yyv[yysp-0]);
         
       break;
							case  201 : 
         yyval = Formatter.FormatIdentifier(yyv[yysp-0]);
         
       break;
							case  202 : 
         yyval = Formatter.FormatGlobal(yyv[yysp-0]);
         
       break;
							case  203 : 
         yyval = Formatter.FormatIdentifier(yyv[yysp-0]);
         
       break;
							case  204 : 
         yyval = Formatter.FormatIdentifier(yyv[yysp-0]);
         
       break;
							case  205 : 
         yyval = Formatter.FormatIdentifier(yyv[yysp-0]);
         
       break;
							case  206 : 
         yyval = Formatter.FormatGlobal(yyv[yysp-0]);
         
       break;
							case  207 : 
         yyval = Formatter.FormatSkill(yyv[yysp-0]);
         
       break;
							case  208 : 
         yyval = Formatter.FormatSkill(yyv[yysp-0]);
         
       break;
							case  209 : 
         yyval = Formatter.FormatSkill(yyv[yysp-0]);
         
       break;
							case  210 : 
         yyval = Formatter.FormatGlobal(yyv[yysp-0]);
         
       break;
							case  211 : 
         yyval = Formatter.FormatSkill(yyv[yysp-0]);
         
       break;
							case  212 : 
         yyval = Formatter.FormatGlobal(yyv[yysp-0]);
         
       break;
							case  213 : 
         yyval = Formatter.FormatNull();
         
       break;
							case  214 : 
         yyval = yyv[yysp-0];
         
       break;
							case  215 : 
         yyval = yyv[yysp-0];
         
       break;
							case  216 : 
         yyval = yyv[yysp-0];
         
       break;
							case  217 : 
         yyval = yyv[yysp-2] + yyv[yysp-0];
         
       break;
							case  218 : 
         yyval = yyv[yysp-2] + yyv[yysp-0];
         
       break;
							case  219 : 
         yyval = yyv[yysp-0];
         
       break;
							case  220 : 
         yyval = yyv[yysp-0];
         
       break;
							case  221 : 
         yyval = yyv[yysp-0];
         
       break;
							case  222 : 
         yyval = yyv[yysp-0];
         
       break;
							case  223 : 
         yyval = yyv[yysp-0];
         
       break;
							case  224 : 
         yyval = yyv[yysp-0]; //TODO: FormatIdentifier?
         
       break;
							case  225 : 
         yyval = Formatter.FormatFile(yyv[yysp-0]);
         
       break;
							case  226 : 
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

					int yynacts   = 2652;
					int yyngotos  = 813;
					int yynstates = 341;
					int yynrules  = 226;
					yya = new YYARec[yynacts+1];  int yyac = 1;
					yyg = new YYARec[yyngotos+1]; int yygc = 1;
					yyr = new YYRRec[yynrules+1]; int yyrc = 1;

					yya[yyac] = new YYARec(257,23);yyac++; 
					yya[yyac] = new YYARec(258,24);yyac++; 
					yya[yyac] = new YYARec(259,25);yyac++; 
					yya[yyac] = new YYARec(262,26);yyac++; 
					yya[yyac] = new YYARec(263,27);yyac++; 
					yya[yyac] = new YYARec(264,28);yyac++; 
					yya[yyac] = new YYARec(265,29);yyac++; 
					yya[yyac] = new YYARec(266,30);yyac++; 
					yya[yyac] = new YYARec(297,31);yyac++; 
					yya[yyac] = new YYARec(298,32);yyac++; 
					yya[yyac] = new YYARec(299,33);yyac++; 
					yya[yyac] = new YYARec(300,34);yyac++; 
					yya[yyac] = new YYARec(301,35);yyac++; 
					yya[yyac] = new YYARec(302,36);yyac++; 
					yya[yyac] = new YYARec(303,37);yyac++; 
					yya[yyac] = new YYARec(304,38);yyac++; 
					yya[yyac] = new YYARec(305,39);yyac++; 
					yya[yyac] = new YYARec(306,40);yyac++; 
					yya[yyac] = new YYARec(308,41);yyac++; 
					yya[yyac] = new YYARec(309,42);yyac++; 
					yya[yyac] = new YYARec(310,43);yyac++; 
					yya[yyac] = new YYARec(311,44);yyac++; 
					yya[yyac] = new YYARec(312,45);yyac++; 
					yya[yyac] = new YYARec(313,46);yyac++; 
					yya[yyac] = new YYARec(314,47);yyac++; 
					yya[yyac] = new YYARec(315,48);yyac++; 
					yya[yyac] = new YYARec(316,49);yyac++; 
					yya[yyac] = new YYARec(317,50);yyac++; 
					yya[yyac] = new YYARec(318,51);yyac++; 
					yya[yyac] = new YYARec(319,52);yyac++; 
					yya[yyac] = new YYARec(320,53);yyac++; 
					yya[yyac] = new YYARec(323,54);yyac++; 
					yya[yyac] = new YYARec(0,-4 );yyac++; 
					yya[yyac] = new YYARec(296,55);yyac++; 
					yya[yyac] = new YYARec(0,-155 );yyac++; 
					yya[yyac] = new YYARec(258,-155 );yyac++; 
					yya[yyac] = new YYARec(260,-155 );yyac++; 
					yya[yyac] = new YYARec(261,-155 );yyac++; 
					yya[yyac] = new YYARec(265,-155 );yyac++; 
					yya[yyac] = new YYARec(268,-155 );yyac++; 
					yya[yyac] = new YYARec(269,-155 );yyac++; 
					yya[yyac] = new YYARec(270,-155 );yyac++; 
					yya[yyac] = new YYARec(271,-155 );yyac++; 
					yya[yyac] = new YYARec(272,-155 );yyac++; 
					yya[yyac] = new YYARec(274,-155 );yyac++; 
					yya[yyac] = new YYARec(275,-155 );yyac++; 
					yya[yyac] = new YYARec(276,-155 );yyac++; 
					yya[yyac] = new YYARec(277,-155 );yyac++; 
					yya[yyac] = new YYARec(278,-155 );yyac++; 
					yya[yyac] = new YYARec(279,-155 );yyac++; 
					yya[yyac] = new YYARec(280,-155 );yyac++; 
					yya[yyac] = new YYARec(281,-155 );yyac++; 
					yya[yyac] = new YYARec(282,-155 );yyac++; 
					yya[yyac] = new YYARec(283,-155 );yyac++; 
					yya[yyac] = new YYARec(284,-155 );yyac++; 
					yya[yyac] = new YYARec(285,-155 );yyac++; 
					yya[yyac] = new YYARec(286,-155 );yyac++; 
					yya[yyac] = new YYARec(288,-155 );yyac++; 
					yya[yyac] = new YYARec(289,-155 );yyac++; 
					yya[yyac] = new YYARec(290,-155 );yyac++; 
					yya[yyac] = new YYARec(291,-155 );yyac++; 
					yya[yyac] = new YYARec(292,-155 );yyac++; 
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
					yya[yyac] = new YYARec(319,-155 );yyac++; 
					yya[yyac] = new YYARec(320,-155 );yyac++; 
					yya[yyac] = new YYARec(321,-155 );yyac++; 
					yya[yyac] = new YYARec(322,-155 );yyac++; 
					yya[yyac] = new YYARec(323,-155 );yyac++; 
					yya[yyac] = new YYARec(324,-155 );yyac++; 
					yya[yyac] = new YYARec(325,-155 );yyac++; 
					yya[yyac] = new YYARec(297,31);yyac++; 
					yya[yyac] = new YYARec(298,59);yyac++; 
					yya[yyac] = new YYARec(299,60);yyac++; 
					yya[yyac] = new YYARec(300,61);yyac++; 
					yya[yyac] = new YYARec(301,62);yyac++; 
					yya[yyac] = new YYARec(302,63);yyac++; 
					yya[yyac] = new YYARec(303,37);yyac++; 
					yya[yyac] = new YYARec(304,38);yyac++; 
					yya[yyac] = new YYARec(305,39);yyac++; 
					yya[yyac] = new YYARec(306,40);yyac++; 
					yya[yyac] = new YYARec(308,64);yyac++; 
					yya[yyac] = new YYARec(309,42);yyac++; 
					yya[yyac] = new YYARec(310,65);yyac++; 
					yya[yyac] = new YYARec(311,66);yyac++; 
					yya[yyac] = new YYARec(312,67);yyac++; 
					yya[yyac] = new YYARec(313,46);yyac++; 
					yya[yyac] = new YYARec(314,68);yyac++; 
					yya[yyac] = new YYARec(315,48);yyac++; 
					yya[yyac] = new YYARec(316,69);yyac++; 
					yya[yyac] = new YYARec(317,50);yyac++; 
					yya[yyac] = new YYARec(318,70);yyac++; 
					yya[yyac] = new YYARec(319,52);yyac++; 
					yya[yyac] = new YYARec(320,53);yyac++; 
					yya[yyac] = new YYARec(323,71);yyac++; 
					yya[yyac] = new YYARec(297,31);yyac++; 
					yya[yyac] = new YYARec(298,59);yyac++; 
					yya[yyac] = new YYARec(299,60);yyac++; 
					yya[yyac] = new YYARec(300,61);yyac++; 
					yya[yyac] = new YYARec(301,62);yyac++; 
					yya[yyac] = new YYARec(302,63);yyac++; 
					yya[yyac] = new YYARec(303,37);yyac++; 
					yya[yyac] = new YYARec(304,38);yyac++; 
					yya[yyac] = new YYARec(305,39);yyac++; 
					yya[yyac] = new YYARec(306,40);yyac++; 
					yya[yyac] = new YYARec(308,64);yyac++; 
					yya[yyac] = new YYARec(309,42);yyac++; 
					yya[yyac] = new YYARec(310,65);yyac++; 
					yya[yyac] = new YYARec(311,66);yyac++; 
					yya[yyac] = new YYARec(312,67);yyac++; 
					yya[yyac] = new YYARec(313,46);yyac++; 
					yya[yyac] = new YYARec(314,68);yyac++; 
					yya[yyac] = new YYARec(315,48);yyac++; 
					yya[yyac] = new YYARec(316,69);yyac++; 
					yya[yyac] = new YYARec(317,50);yyac++; 
					yya[yyac] = new YYARec(318,70);yyac++; 
					yya[yyac] = new YYARec(319,52);yyac++; 
					yya[yyac] = new YYARec(320,53);yyac++; 
					yya[yyac] = new YYARec(323,71);yyac++; 
					yya[yyac] = new YYARec(297,31);yyac++; 
					yya[yyac] = new YYARec(298,59);yyac++; 
					yya[yyac] = new YYARec(299,60);yyac++; 
					yya[yyac] = new YYARec(300,61);yyac++; 
					yya[yyac] = new YYARec(301,62);yyac++; 
					yya[yyac] = new YYARec(302,63);yyac++; 
					yya[yyac] = new YYARec(303,37);yyac++; 
					yya[yyac] = new YYARec(304,38);yyac++; 
					yya[yyac] = new YYARec(305,39);yyac++; 
					yya[yyac] = new YYARec(306,40);yyac++; 
					yya[yyac] = new YYARec(308,64);yyac++; 
					yya[yyac] = new YYARec(309,42);yyac++; 
					yya[yyac] = new YYARec(310,65);yyac++; 
					yya[yyac] = new YYARec(311,66);yyac++; 
					yya[yyac] = new YYARec(312,67);yyac++; 
					yya[yyac] = new YYARec(313,46);yyac++; 
					yya[yyac] = new YYARec(314,68);yyac++; 
					yya[yyac] = new YYARec(315,48);yyac++; 
					yya[yyac] = new YYARec(316,69);yyac++; 
					yya[yyac] = new YYARec(317,50);yyac++; 
					yya[yyac] = new YYARec(318,70);yyac++; 
					yya[yyac] = new YYARec(319,52);yyac++; 
					yya[yyac] = new YYARec(320,53);yyac++; 
					yya[yyac] = new YYARec(321,76);yyac++; 
					yya[yyac] = new YYARec(323,71);yyac++; 
					yya[yyac] = new YYARec(281,85);yyac++; 
					yya[yyac] = new YYARec(282,86);yyac++; 
					yya[yyac] = new YYARec(286,87);yyac++; 
					yya[yyac] = new YYARec(321,88);yyac++; 
					yya[yyac] = new YYARec(322,89);yyac++; 
					yya[yyac] = new YYARec(323,54);yyac++; 
					yya[yyac] = new YYARec(324,90);yyac++; 
					yya[yyac] = new YYARec(325,91);yyac++; 
					yya[yyac] = new YYARec(297,31);yyac++; 
					yya[yyac] = new YYARec(298,59);yyac++; 
					yya[yyac] = new YYARec(299,60);yyac++; 
					yya[yyac] = new YYARec(300,61);yyac++; 
					yya[yyac] = new YYARec(301,62);yyac++; 
					yya[yyac] = new YYARec(302,63);yyac++; 
					yya[yyac] = new YYARec(303,37);yyac++; 
					yya[yyac] = new YYARec(304,38);yyac++; 
					yya[yyac] = new YYARec(305,39);yyac++; 
					yya[yyac] = new YYARec(306,40);yyac++; 
					yya[yyac] = new YYARec(308,64);yyac++; 
					yya[yyac] = new YYARec(309,42);yyac++; 
					yya[yyac] = new YYARec(310,65);yyac++; 
					yya[yyac] = new YYARec(311,66);yyac++; 
					yya[yyac] = new YYARec(312,67);yyac++; 
					yya[yyac] = new YYARec(313,46);yyac++; 
					yya[yyac] = new YYARec(314,68);yyac++; 
					yya[yyac] = new YYARec(315,48);yyac++; 
					yya[yyac] = new YYARec(316,69);yyac++; 
					yya[yyac] = new YYARec(317,50);yyac++; 
					yya[yyac] = new YYARec(318,70);yyac++; 
					yya[yyac] = new YYARec(319,52);yyac++; 
					yya[yyac] = new YYARec(320,53);yyac++; 
					yya[yyac] = new YYARec(323,71);yyac++; 
					yya[yyac] = new YYARec(257,23);yyac++; 
					yya[yyac] = new YYARec(258,24);yyac++; 
					yya[yyac] = new YYARec(259,25);yyac++; 
					yya[yyac] = new YYARec(262,26);yyac++; 
					yya[yyac] = new YYARec(263,27);yyac++; 
					yya[yyac] = new YYARec(264,28);yyac++; 
					yya[yyac] = new YYARec(265,29);yyac++; 
					yya[yyac] = new YYARec(266,30);yyac++; 
					yya[yyac] = new YYARec(297,31);yyac++; 
					yya[yyac] = new YYARec(298,32);yyac++; 
					yya[yyac] = new YYARec(299,33);yyac++; 
					yya[yyac] = new YYARec(300,34);yyac++; 
					yya[yyac] = new YYARec(301,35);yyac++; 
					yya[yyac] = new YYARec(302,36);yyac++; 
					yya[yyac] = new YYARec(303,37);yyac++; 
					yya[yyac] = new YYARec(304,38);yyac++; 
					yya[yyac] = new YYARec(305,39);yyac++; 
					yya[yyac] = new YYARec(306,40);yyac++; 
					yya[yyac] = new YYARec(308,41);yyac++; 
					yya[yyac] = new YYARec(309,42);yyac++; 
					yya[yyac] = new YYARec(310,43);yyac++; 
					yya[yyac] = new YYARec(311,44);yyac++; 
					yya[yyac] = new YYARec(312,45);yyac++; 
					yya[yyac] = new YYARec(313,46);yyac++; 
					yya[yyac] = new YYARec(314,47);yyac++; 
					yya[yyac] = new YYARec(315,48);yyac++; 
					yya[yyac] = new YYARec(316,49);yyac++; 
					yya[yyac] = new YYARec(317,50);yyac++; 
					yya[yyac] = new YYARec(318,51);yyac++; 
					yya[yyac] = new YYARec(319,52);yyac++; 
					yya[yyac] = new YYARec(320,53);yyac++; 
					yya[yyac] = new YYARec(323,54);yyac++; 
					yya[yyac] = new YYARec(0,-4 );yyac++; 
					yya[yyac] = new YYARec(260,-4 );yyac++; 
					yya[yyac] = new YYARec(261,-4 );yyac++; 
					yya[yyac] = new YYARec(0,0);yyac++; 
					yya[yyac] = new YYARec(308,98);yyac++; 
					yya[yyac] = new YYARec(314,99);yyac++; 
					yya[yyac] = new YYARec(316,100);yyac++; 
					yya[yyac] = new YYARec(323,54);yyac++; 
					yya[yyac] = new YYARec(308,98);yyac++; 
					yya[yyac] = new YYARec(314,99);yyac++; 
					yya[yyac] = new YYARec(316,100);yyac++; 
					yya[yyac] = new YYARec(323,54);yyac++; 
					yya[yyac] = new YYARec(308,98);yyac++; 
					yya[yyac] = new YYARec(314,99);yyac++; 
					yya[yyac] = new YYARec(316,100);yyac++; 
					yya[yyac] = new YYARec(323,54);yyac++; 
					yya[yyac] = new YYARec(308,98);yyac++; 
					yya[yyac] = new YYARec(314,99);yyac++; 
					yya[yyac] = new YYARec(316,100);yyac++; 
					yya[yyac] = new YYARec(323,54);yyac++; 
					yya[yyac] = new YYARec(324,90);yyac++; 
					yya[yyac] = new YYARec(297,-45 );yyac++; 
					yya[yyac] = new YYARec(298,-45 );yyac++; 
					yya[yyac] = new YYARec(299,-45 );yyac++; 
					yya[yyac] = new YYARec(300,-45 );yyac++; 
					yya[yyac] = new YYARec(301,-45 );yyac++; 
					yya[yyac] = new YYARec(302,-45 );yyac++; 
					yya[yyac] = new YYARec(303,-45 );yyac++; 
					yya[yyac] = new YYARec(304,-45 );yyac++; 
					yya[yyac] = new YYARec(305,-45 );yyac++; 
					yya[yyac] = new YYARec(306,-45 );yyac++; 
					yya[yyac] = new YYARec(308,-45 );yyac++; 
					yya[yyac] = new YYARec(309,-45 );yyac++; 
					yya[yyac] = new YYARec(310,-45 );yyac++; 
					yya[yyac] = new YYARec(311,-45 );yyac++; 
					yya[yyac] = new YYARec(312,-45 );yyac++; 
					yya[yyac] = new YYARec(313,-45 );yyac++; 
					yya[yyac] = new YYARec(314,-45 );yyac++; 
					yya[yyac] = new YYARec(315,-45 );yyac++; 
					yya[yyac] = new YYARec(316,-45 );yyac++; 
					yya[yyac] = new YYARec(317,-45 );yyac++; 
					yya[yyac] = new YYARec(318,-45 );yyac++; 
					yya[yyac] = new YYARec(319,-45 );yyac++; 
					yya[yyac] = new YYARec(320,-45 );yyac++; 
					yya[yyac] = new YYARec(323,-45 );yyac++; 
					yya[yyac] = new YYARec(0,-199 );yyac++; 
					yya[yyac] = new YYARec(260,-199 );yyac++; 
					yya[yyac] = new YYARec(261,-199 );yyac++; 
					yya[yyac] = new YYARec(296,-199 );yyac++; 
					yya[yyac] = new YYARec(281,-42 );yyac++; 
					yya[yyac] = new YYARec(282,-42 );yyac++; 
					yya[yyac] = new YYARec(286,-42 );yyac++; 
					yya[yyac] = new YYARec(321,-42 );yyac++; 
					yya[yyac] = new YYARec(322,-42 );yyac++; 
					yya[yyac] = new YYARec(323,-42 );yyac++; 
					yya[yyac] = new YYARec(324,-42 );yyac++; 
					yya[yyac] = new YYARec(325,-42 );yyac++; 
					yya[yyac] = new YYARec(0,-202 );yyac++; 
					yya[yyac] = new YYARec(260,-202 );yyac++; 
					yya[yyac] = new YYARec(261,-202 );yyac++; 
					yya[yyac] = new YYARec(296,-202 );yyac++; 
					yya[yyac] = new YYARec(297,-57 );yyac++; 
					yya[yyac] = new YYARec(298,-57 );yyac++; 
					yya[yyac] = new YYARec(299,-57 );yyac++; 
					yya[yyac] = new YYARec(300,-57 );yyac++; 
					yya[yyac] = new YYARec(301,-57 );yyac++; 
					yya[yyac] = new YYARec(302,-57 );yyac++; 
					yya[yyac] = new YYARec(303,-57 );yyac++; 
					yya[yyac] = new YYARec(304,-57 );yyac++; 
					yya[yyac] = new YYARec(305,-57 );yyac++; 
					yya[yyac] = new YYARec(306,-57 );yyac++; 
					yya[yyac] = new YYARec(308,-57 );yyac++; 
					yya[yyac] = new YYARec(309,-57 );yyac++; 
					yya[yyac] = new YYARec(310,-57 );yyac++; 
					yya[yyac] = new YYARec(311,-57 );yyac++; 
					yya[yyac] = new YYARec(312,-57 );yyac++; 
					yya[yyac] = new YYARec(313,-57 );yyac++; 
					yya[yyac] = new YYARec(314,-57 );yyac++; 
					yya[yyac] = new YYARec(315,-57 );yyac++; 
					yya[yyac] = new YYARec(316,-57 );yyac++; 
					yya[yyac] = new YYARec(317,-57 );yyac++; 
					yya[yyac] = new YYARec(318,-57 );yyac++; 
					yya[yyac] = new YYARec(319,-57 );yyac++; 
					yya[yyac] = new YYARec(320,-57 );yyac++; 
					yya[yyac] = new YYARec(321,-57 );yyac++; 
					yya[yyac] = new YYARec(323,-57 );yyac++; 
					yya[yyac] = new YYARec(0,-197 );yyac++; 
					yya[yyac] = new YYARec(260,-197 );yyac++; 
					yya[yyac] = new YYARec(261,-197 );yyac++; 
					yya[yyac] = new YYARec(296,-197 );yyac++; 
					yya[yyac] = new YYARec(297,-61 );yyac++; 
					yya[yyac] = new YYARec(298,-61 );yyac++; 
					yya[yyac] = new YYARec(299,-61 );yyac++; 
					yya[yyac] = new YYARec(300,-61 );yyac++; 
					yya[yyac] = new YYARec(301,-61 );yyac++; 
					yya[yyac] = new YYARec(302,-61 );yyac++; 
					yya[yyac] = new YYARec(303,-61 );yyac++; 
					yya[yyac] = new YYARec(304,-61 );yyac++; 
					yya[yyac] = new YYARec(305,-61 );yyac++; 
					yya[yyac] = new YYARec(306,-61 );yyac++; 
					yya[yyac] = new YYARec(308,-61 );yyac++; 
					yya[yyac] = new YYARec(309,-61 );yyac++; 
					yya[yyac] = new YYARec(310,-61 );yyac++; 
					yya[yyac] = new YYARec(311,-61 );yyac++; 
					yya[yyac] = new YYARec(312,-61 );yyac++; 
					yya[yyac] = new YYARec(313,-61 );yyac++; 
					yya[yyac] = new YYARec(314,-61 );yyac++; 
					yya[yyac] = new YYARec(315,-61 );yyac++; 
					yya[yyac] = new YYARec(316,-61 );yyac++; 
					yya[yyac] = new YYARec(317,-61 );yyac++; 
					yya[yyac] = new YYARec(318,-61 );yyac++; 
					yya[yyac] = new YYARec(319,-61 );yyac++; 
					yya[yyac] = new YYARec(320,-61 );yyac++; 
					yya[yyac] = new YYARec(323,-61 );yyac++; 
					yya[yyac] = new YYARec(0,-204 );yyac++; 
					yya[yyac] = new YYARec(260,-204 );yyac++; 
					yya[yyac] = new YYARec(261,-204 );yyac++; 
					yya[yyac] = new YYARec(296,-204 );yyac++; 
					yya[yyac] = new YYARec(297,-76 );yyac++; 
					yya[yyac] = new YYARec(298,-76 );yyac++; 
					yya[yyac] = new YYARec(299,-76 );yyac++; 
					yya[yyac] = new YYARec(300,-76 );yyac++; 
					yya[yyac] = new YYARec(301,-76 );yyac++; 
					yya[yyac] = new YYARec(302,-76 );yyac++; 
					yya[yyac] = new YYARec(303,-76 );yyac++; 
					yya[yyac] = new YYARec(304,-76 );yyac++; 
					yya[yyac] = new YYARec(305,-76 );yyac++; 
					yya[yyac] = new YYARec(306,-76 );yyac++; 
					yya[yyac] = new YYARec(308,-76 );yyac++; 
					yya[yyac] = new YYARec(309,-76 );yyac++; 
					yya[yyac] = new YYARec(310,-76 );yyac++; 
					yya[yyac] = new YYARec(311,-76 );yyac++; 
					yya[yyac] = new YYARec(312,-76 );yyac++; 
					yya[yyac] = new YYARec(313,-76 );yyac++; 
					yya[yyac] = new YYARec(314,-76 );yyac++; 
					yya[yyac] = new YYARec(315,-76 );yyac++; 
					yya[yyac] = new YYARec(316,-76 );yyac++; 
					yya[yyac] = new YYARec(317,-76 );yyac++; 
					yya[yyac] = new YYARec(318,-76 );yyac++; 
					yya[yyac] = new YYARec(319,-76 );yyac++; 
					yya[yyac] = new YYARec(320,-76 );yyac++; 
					yya[yyac] = new YYARec(323,-76 );yyac++; 
					yya[yyac] = new YYARec(0,-201 );yyac++; 
					yya[yyac] = new YYARec(260,-201 );yyac++; 
					yya[yyac] = new YYARec(261,-201 );yyac++; 
					yya[yyac] = new YYARec(296,-201 );yyac++; 
					yya[yyac] = new YYARec(281,-190 );yyac++; 
					yya[yyac] = new YYARec(282,-190 );yyac++; 
					yya[yyac] = new YYARec(286,-190 );yyac++; 
					yya[yyac] = new YYARec(321,-190 );yyac++; 
					yya[yyac] = new YYARec(322,-190 );yyac++; 
					yya[yyac] = new YYARec(323,-190 );yyac++; 
					yya[yyac] = new YYARec(324,-190 );yyac++; 
					yya[yyac] = new YYARec(325,-190 );yyac++; 
					yya[yyac] = new YYARec(0,-211 );yyac++; 
					yya[yyac] = new YYARec(260,-211 );yyac++; 
					yya[yyac] = new YYARec(261,-211 );yyac++; 
					yya[yyac] = new YYARec(296,-211 );yyac++; 
					yya[yyac] = new YYARec(281,-40 );yyac++; 
					yya[yyac] = new YYARec(282,-40 );yyac++; 
					yya[yyac] = new YYARec(286,-40 );yyac++; 
					yya[yyac] = new YYARec(321,-40 );yyac++; 
					yya[yyac] = new YYARec(322,-40 );yyac++; 
					yya[yyac] = new YYARec(323,-40 );yyac++; 
					yya[yyac] = new YYARec(324,-40 );yyac++; 
					yya[yyac] = new YYARec(325,-40 );yyac++; 
					yya[yyac] = new YYARec(0,-194 );yyac++; 
					yya[yyac] = new YYARec(260,-194 );yyac++; 
					yya[yyac] = new YYARec(261,-194 );yyac++; 
					yya[yyac] = new YYARec(296,-194 );yyac++; 
					yya[yyac] = new YYARec(297,-44 );yyac++; 
					yya[yyac] = new YYARec(298,-44 );yyac++; 
					yya[yyac] = new YYARec(299,-44 );yyac++; 
					yya[yyac] = new YYARec(300,-44 );yyac++; 
					yya[yyac] = new YYARec(301,-44 );yyac++; 
					yya[yyac] = new YYARec(302,-44 );yyac++; 
					yya[yyac] = new YYARec(303,-44 );yyac++; 
					yya[yyac] = new YYARec(304,-44 );yyac++; 
					yya[yyac] = new YYARec(305,-44 );yyac++; 
					yya[yyac] = new YYARec(306,-44 );yyac++; 
					yya[yyac] = new YYARec(308,-44 );yyac++; 
					yya[yyac] = new YYARec(309,-44 );yyac++; 
					yya[yyac] = new YYARec(310,-44 );yyac++; 
					yya[yyac] = new YYARec(311,-44 );yyac++; 
					yya[yyac] = new YYARec(312,-44 );yyac++; 
					yya[yyac] = new YYARec(313,-44 );yyac++; 
					yya[yyac] = new YYARec(314,-44 );yyac++; 
					yya[yyac] = new YYARec(315,-44 );yyac++; 
					yya[yyac] = new YYARec(316,-44 );yyac++; 
					yya[yyac] = new YYARec(317,-44 );yyac++; 
					yya[yyac] = new YYARec(318,-44 );yyac++; 
					yya[yyac] = new YYARec(319,-44 );yyac++; 
					yya[yyac] = new YYARec(320,-44 );yyac++; 
					yya[yyac] = new YYARec(323,-44 );yyac++; 
					yya[yyac] = new YYARec(0,-193 );yyac++; 
					yya[yyac] = new YYARec(260,-193 );yyac++; 
					yya[yyac] = new YYARec(261,-193 );yyac++; 
					yya[yyac] = new YYARec(296,-193 );yyac++; 
					yya[yyac] = new YYARec(297,-62 );yyac++; 
					yya[yyac] = new YYARec(298,-62 );yyac++; 
					yya[yyac] = new YYARec(299,-62 );yyac++; 
					yya[yyac] = new YYARec(300,-62 );yyac++; 
					yya[yyac] = new YYARec(301,-62 );yyac++; 
					yya[yyac] = new YYARec(302,-62 );yyac++; 
					yya[yyac] = new YYARec(303,-62 );yyac++; 
					yya[yyac] = new YYARec(304,-62 );yyac++; 
					yya[yyac] = new YYARec(305,-62 );yyac++; 
					yya[yyac] = new YYARec(306,-62 );yyac++; 
					yya[yyac] = new YYARec(308,-62 );yyac++; 
					yya[yyac] = new YYARec(309,-62 );yyac++; 
					yya[yyac] = new YYARec(310,-62 );yyac++; 
					yya[yyac] = new YYARec(311,-62 );yyac++; 
					yya[yyac] = new YYARec(312,-62 );yyac++; 
					yya[yyac] = new YYARec(313,-62 );yyac++; 
					yya[yyac] = new YYARec(314,-62 );yyac++; 
					yya[yyac] = new YYARec(315,-62 );yyac++; 
					yya[yyac] = new YYARec(316,-62 );yyac++; 
					yya[yyac] = new YYARec(317,-62 );yyac++; 
					yya[yyac] = new YYARec(318,-62 );yyac++; 
					yya[yyac] = new YYARec(319,-62 );yyac++; 
					yya[yyac] = new YYARec(320,-62 );yyac++; 
					yya[yyac] = new YYARec(323,-62 );yyac++; 
					yya[yyac] = new YYARec(0,-196 );yyac++; 
					yya[yyac] = new YYARec(260,-196 );yyac++; 
					yya[yyac] = new YYARec(261,-196 );yyac++; 
					yya[yyac] = new YYARec(296,-196 );yyac++; 
					yya[yyac] = new YYARec(281,-188 );yyac++; 
					yya[yyac] = new YYARec(282,-188 );yyac++; 
					yya[yyac] = new YYARec(286,-188 );yyac++; 
					yya[yyac] = new YYARec(321,-188 );yyac++; 
					yya[yyac] = new YYARec(322,-188 );yyac++; 
					yya[yyac] = new YYARec(323,-188 );yyac++; 
					yya[yyac] = new YYARec(324,-188 );yyac++; 
					yya[yyac] = new YYARec(325,-188 );yyac++; 
					yya[yyac] = new YYARec(0,-207 );yyac++; 
					yya[yyac] = new YYARec(260,-207 );yyac++; 
					yya[yyac] = new YYARec(261,-207 );yyac++; 
					yya[yyac] = new YYARec(296,-207 );yyac++; 
					yya[yyac] = new YYARec(281,-189 );yyac++; 
					yya[yyac] = new YYARec(282,-189 );yyac++; 
					yya[yyac] = new YYARec(286,-189 );yyac++; 
					yya[yyac] = new YYARec(321,-189 );yyac++; 
					yya[yyac] = new YYARec(322,-189 );yyac++; 
					yya[yyac] = new YYARec(323,-189 );yyac++; 
					yya[yyac] = new YYARec(324,-189 );yyac++; 
					yya[yyac] = new YYARec(325,-189 );yyac++; 
					yya[yyac] = new YYARec(0,-209 );yyac++; 
					yya[yyac] = new YYARec(260,-209 );yyac++; 
					yya[yyac] = new YYARec(261,-209 );yyac++; 
					yya[yyac] = new YYARec(296,-209 );yyac++; 
					yya[yyac] = new YYARec(281,-41 );yyac++; 
					yya[yyac] = new YYARec(282,-41 );yyac++; 
					yya[yyac] = new YYARec(286,-41 );yyac++; 
					yya[yyac] = new YYARec(321,-41 );yyac++; 
					yya[yyac] = new YYARec(322,-41 );yyac++; 
					yya[yyac] = new YYARec(323,-41 );yyac++; 
					yya[yyac] = new YYARec(324,-41 );yyac++; 
					yya[yyac] = new YYARec(325,-41 );yyac++; 
					yya[yyac] = new YYARec(0,-206 );yyac++; 
					yya[yyac] = new YYARec(260,-206 );yyac++; 
					yya[yyac] = new YYARec(261,-206 );yyac++; 
					yya[yyac] = new YYARec(296,-206 );yyac++; 
					yya[yyac] = new YYARec(300,110);yyac++; 
					yya[yyac] = new YYARec(301,111);yyac++; 
					yya[yyac] = new YYARec(304,112);yyac++; 
					yya[yyac] = new YYARec(305,113);yyac++; 
					yya[yyac] = new YYARec(310,114);yyac++; 
					yya[yyac] = new YYARec(311,115);yyac++; 
					yya[yyac] = new YYARec(312,116);yyac++; 
					yya[yyac] = new YYARec(314,117);yyac++; 
					yya[yyac] = new YYARec(315,118);yyac++; 
					yya[yyac] = new YYARec(316,119);yyac++; 
					yya[yyac] = new YYARec(317,120);yyac++; 
					yya[yyac] = new YYARec(318,121);yyac++; 
					yya[yyac] = new YYARec(319,122);yyac++; 
					yya[yyac] = new YYARec(320,123);yyac++; 
					yya[yyac] = new YYARec(323,54);yyac++; 
					yya[yyac] = new YYARec(265,124);yyac++; 
					yya[yyac] = new YYARec(282,125);yyac++; 
					yya[yyac] = new YYARec(258,-186 );yyac++; 
					yya[yyac] = new YYARec(265,-186 );yyac++; 
					yya[yyac] = new YYARec(297,-186 );yyac++; 
					yya[yyac] = new YYARec(298,-186 );yyac++; 
					yya[yyac] = new YYARec(299,-186 );yyac++; 
					yya[yyac] = new YYARec(300,-186 );yyac++; 
					yya[yyac] = new YYARec(301,-186 );yyac++; 
					yya[yyac] = new YYARec(302,-186 );yyac++; 
					yya[yyac] = new YYARec(303,-186 );yyac++; 
					yya[yyac] = new YYARec(304,-186 );yyac++; 
					yya[yyac] = new YYARec(305,-186 );yyac++; 
					yya[yyac] = new YYARec(306,-186 );yyac++; 
					yya[yyac] = new YYARec(308,-186 );yyac++; 
					yya[yyac] = new YYARec(309,-186 );yyac++; 
					yya[yyac] = new YYARec(310,-186 );yyac++; 
					yya[yyac] = new YYARec(311,-186 );yyac++; 
					yya[yyac] = new YYARec(312,-186 );yyac++; 
					yya[yyac] = new YYARec(313,-186 );yyac++; 
					yya[yyac] = new YYARec(314,-186 );yyac++; 
					yya[yyac] = new YYARec(315,-186 );yyac++; 
					yya[yyac] = new YYARec(316,-186 );yyac++; 
					yya[yyac] = new YYARec(317,-186 );yyac++; 
					yya[yyac] = new YYARec(318,-186 );yyac++; 
					yya[yyac] = new YYARec(319,-186 );yyac++; 
					yya[yyac] = new YYARec(320,-186 );yyac++; 
					yya[yyac] = new YYARec(323,-186 );yyac++; 
					yya[yyac] = new YYARec(324,-186 );yyac++; 
					yya[yyac] = new YYARec(325,-186 );yyac++; 
					yya[yyac] = new YYARec(258,127);yyac++; 
					yya[yyac] = new YYARec(265,128);yyac++; 
					yya[yyac] = new YYARec(325,91);yyac++; 
					yya[yyac] = new YYARec(324,90);yyac++; 
					yya[yyac] = new YYARec(321,130);yyac++; 
					yya[yyac] = new YYARec(322,131);yyac++; 
					yya[yyac] = new YYARec(258,132);yyac++; 
					yya[yyac] = new YYARec(297,31);yyac++; 
					yya[yyac] = new YYARec(298,59);yyac++; 
					yya[yyac] = new YYARec(299,60);yyac++; 
					yya[yyac] = new YYARec(300,61);yyac++; 
					yya[yyac] = new YYARec(301,62);yyac++; 
					yya[yyac] = new YYARec(302,63);yyac++; 
					yya[yyac] = new YYARec(303,37);yyac++; 
					yya[yyac] = new YYARec(304,38);yyac++; 
					yya[yyac] = new YYARec(305,39);yyac++; 
					yya[yyac] = new YYARec(306,40);yyac++; 
					yya[yyac] = new YYARec(308,64);yyac++; 
					yya[yyac] = new YYARec(309,42);yyac++; 
					yya[yyac] = new YYARec(310,65);yyac++; 
					yya[yyac] = new YYARec(311,66);yyac++; 
					yya[yyac] = new YYARec(312,67);yyac++; 
					yya[yyac] = new YYARec(313,46);yyac++; 
					yya[yyac] = new YYARec(314,68);yyac++; 
					yya[yyac] = new YYARec(315,48);yyac++; 
					yya[yyac] = new YYARec(316,69);yyac++; 
					yya[yyac] = new YYARec(317,50);yyac++; 
					yya[yyac] = new YYARec(318,70);yyac++; 
					yya[yyac] = new YYARec(319,52);yyac++; 
					yya[yyac] = new YYARec(320,53);yyac++; 
					yya[yyac] = new YYARec(323,71);yyac++; 
					yya[yyac] = new YYARec(258,-46 );yyac++; 
					yya[yyac] = new YYARec(258,134);yyac++; 
					yya[yyac] = new YYARec(258,135);yyac++; 
					yya[yyac] = new YYARec(258,136);yyac++; 
					yya[yyac] = new YYARec(258,145);yyac++; 
					yya[yyac] = new YYARec(281,85);yyac++; 
					yya[yyac] = new YYARec(282,86);yyac++; 
					yya[yyac] = new YYARec(286,87);yyac++; 
					yya[yyac] = new YYARec(297,31);yyac++; 
					yya[yyac] = new YYARec(298,59);yyac++; 
					yya[yyac] = new YYARec(299,60);yyac++; 
					yya[yyac] = new YYARec(300,61);yyac++; 
					yya[yyac] = new YYARec(301,62);yyac++; 
					yya[yyac] = new YYARec(302,63);yyac++; 
					yya[yyac] = new YYARec(303,37);yyac++; 
					yya[yyac] = new YYARec(304,38);yyac++; 
					yya[yyac] = new YYARec(305,39);yyac++; 
					yya[yyac] = new YYARec(306,40);yyac++; 
					yya[yyac] = new YYARec(307,146);yyac++; 
					yya[yyac] = new YYARec(308,64);yyac++; 
					yya[yyac] = new YYARec(309,42);yyac++; 
					yya[yyac] = new YYARec(310,65);yyac++; 
					yya[yyac] = new YYARec(311,66);yyac++; 
					yya[yyac] = new YYARec(312,67);yyac++; 
					yya[yyac] = new YYARec(313,46);yyac++; 
					yya[yyac] = new YYARec(314,68);yyac++; 
					yya[yyac] = new YYARec(315,48);yyac++; 
					yya[yyac] = new YYARec(316,69);yyac++; 
					yya[yyac] = new YYARec(317,50);yyac++; 
					yya[yyac] = new YYARec(318,70);yyac++; 
					yya[yyac] = new YYARec(319,52);yyac++; 
					yya[yyac] = new YYARec(320,53);yyac++; 
					yya[yyac] = new YYARec(321,88);yyac++; 
					yya[yyac] = new YYARec(322,89);yyac++; 
					yya[yyac] = new YYARec(323,54);yyac++; 
					yya[yyac] = new YYARec(324,90);yyac++; 
					yya[yyac] = new YYARec(325,91);yyac++; 
					yya[yyac] = new YYARec(258,147);yyac++; 
					yya[yyac] = new YYARec(258,148);yyac++; 
					yya[yyac] = new YYARec(257,160);yyac++; 
					yya[yyac] = new YYARec(258,161);yyac++; 
					yya[yyac] = new YYARec(259,162);yyac++; 
					yya[yyac] = new YYARec(262,26);yyac++; 
					yya[yyac] = new YYARec(263,27);yyac++; 
					yya[yyac] = new YYARec(265,163);yyac++; 
					yya[yyac] = new YYARec(287,164);yyac++; 
					yya[yyac] = new YYARec(293,165);yyac++; 
					yya[yyac] = new YYARec(294,166);yyac++; 
					yya[yyac] = new YYARec(295,167);yyac++; 
					yya[yyac] = new YYARec(297,31);yyac++; 
					yya[yyac] = new YYARec(298,59);yyac++; 
					yya[yyac] = new YYARec(299,60);yyac++; 
					yya[yyac] = new YYARec(300,61);yyac++; 
					yya[yyac] = new YYARec(301,62);yyac++; 
					yya[yyac] = new YYARec(302,63);yyac++; 
					yya[yyac] = new YYARec(303,37);yyac++; 
					yya[yyac] = new YYARec(304,38);yyac++; 
					yya[yyac] = new YYARec(305,39);yyac++; 
					yya[yyac] = new YYARec(306,168);yyac++; 
					yya[yyac] = new YYARec(308,64);yyac++; 
					yya[yyac] = new YYARec(309,42);yyac++; 
					yya[yyac] = new YYARec(310,65);yyac++; 
					yya[yyac] = new YYARec(311,66);yyac++; 
					yya[yyac] = new YYARec(312,67);yyac++; 
					yya[yyac] = new YYARec(313,169);yyac++; 
					yya[yyac] = new YYARec(314,68);yyac++; 
					yya[yyac] = new YYARec(315,48);yyac++; 
					yya[yyac] = new YYARec(316,69);yyac++; 
					yya[yyac] = new YYARec(317,170);yyac++; 
					yya[yyac] = new YYARec(318,70);yyac++; 
					yya[yyac] = new YYARec(319,171);yyac++; 
					yya[yyac] = new YYARec(320,53);yyac++; 
					yya[yyac] = new YYARec(323,172);yyac++; 
					yya[yyac] = new YYARec(266,-83 );yyac++; 
					yya[yyac] = new YYARec(321,173);yyac++; 
					yya[yyac] = new YYARec(323,174);yyac++; 
					yya[yyac] = new YYARec(258,175);yyac++; 
					yya[yyac] = new YYARec(257,180);yyac++; 
					yya[yyac] = new YYARec(258,181);yyac++; 
					yya[yyac] = new YYARec(259,182);yyac++; 
					yya[yyac] = new YYARec(300,110);yyac++; 
					yya[yyac] = new YYARec(301,111);yyac++; 
					yya[yyac] = new YYARec(305,113);yyac++; 
					yya[yyac] = new YYARec(310,114);yyac++; 
					yya[yyac] = new YYARec(311,115);yyac++; 
					yya[yyac] = new YYARec(314,117);yyac++; 
					yya[yyac] = new YYARec(316,119);yyac++; 
					yya[yyac] = new YYARec(318,121);yyac++; 
					yya[yyac] = new YYARec(319,122);yyac++; 
					yya[yyac] = new YYARec(323,54);yyac++; 
					yya[yyac] = new YYARec(266,-66 );yyac++; 
					yya[yyac] = new YYARec(281,85);yyac++; 
					yya[yyac] = new YYARec(282,86);yyac++; 
					yya[yyac] = new YYARec(286,87);yyac++; 
					yya[yyac] = new YYARec(321,88);yyac++; 
					yya[yyac] = new YYARec(323,54);yyac++; 
					yya[yyac] = new YYARec(258,-54 );yyac++; 
					yya[yyac] = new YYARec(257,23);yyac++; 
					yya[yyac] = new YYARec(258,24);yyac++; 
					yya[yyac] = new YYARec(259,25);yyac++; 
					yya[yyac] = new YYARec(262,26);yyac++; 
					yya[yyac] = new YYARec(263,27);yyac++; 
					yya[yyac] = new YYARec(264,28);yyac++; 
					yya[yyac] = new YYARec(265,29);yyac++; 
					yya[yyac] = new YYARec(266,30);yyac++; 
					yya[yyac] = new YYARec(297,31);yyac++; 
					yya[yyac] = new YYARec(298,32);yyac++; 
					yya[yyac] = new YYARec(299,33);yyac++; 
					yya[yyac] = new YYARec(300,34);yyac++; 
					yya[yyac] = new YYARec(301,35);yyac++; 
					yya[yyac] = new YYARec(302,36);yyac++; 
					yya[yyac] = new YYARec(303,37);yyac++; 
					yya[yyac] = new YYARec(304,38);yyac++; 
					yya[yyac] = new YYARec(305,39);yyac++; 
					yya[yyac] = new YYARec(306,40);yyac++; 
					yya[yyac] = new YYARec(308,41);yyac++; 
					yya[yyac] = new YYARec(309,42);yyac++; 
					yya[yyac] = new YYARec(310,43);yyac++; 
					yya[yyac] = new YYARec(311,44);yyac++; 
					yya[yyac] = new YYARec(312,45);yyac++; 
					yya[yyac] = new YYARec(313,46);yyac++; 
					yya[yyac] = new YYARec(314,47);yyac++; 
					yya[yyac] = new YYARec(315,48);yyac++; 
					yya[yyac] = new YYARec(316,49);yyac++; 
					yya[yyac] = new YYARec(317,50);yyac++; 
					yya[yyac] = new YYARec(318,51);yyac++; 
					yya[yyac] = new YYARec(319,52);yyac++; 
					yya[yyac] = new YYARec(320,53);yyac++; 
					yya[yyac] = new YYARec(323,54);yyac++; 
					yya[yyac] = new YYARec(260,-4 );yyac++; 
					yya[yyac] = new YYARec(261,-4 );yyac++; 
					yya[yyac] = new YYARec(257,23);yyac++; 
					yya[yyac] = new YYARec(258,24);yyac++; 
					yya[yyac] = new YYARec(259,25);yyac++; 
					yya[yyac] = new YYARec(262,26);yyac++; 
					yya[yyac] = new YYARec(263,27);yyac++; 
					yya[yyac] = new YYARec(264,28);yyac++; 
					yya[yyac] = new YYARec(265,29);yyac++; 
					yya[yyac] = new YYARec(266,30);yyac++; 
					yya[yyac] = new YYARec(297,31);yyac++; 
					yya[yyac] = new YYARec(298,32);yyac++; 
					yya[yyac] = new YYARec(299,33);yyac++; 
					yya[yyac] = new YYARec(300,34);yyac++; 
					yya[yyac] = new YYARec(301,35);yyac++; 
					yya[yyac] = new YYARec(302,36);yyac++; 
					yya[yyac] = new YYARec(303,37);yyac++; 
					yya[yyac] = new YYARec(304,38);yyac++; 
					yya[yyac] = new YYARec(305,39);yyac++; 
					yya[yyac] = new YYARec(306,40);yyac++; 
					yya[yyac] = new YYARec(308,41);yyac++; 
					yya[yyac] = new YYARec(309,42);yyac++; 
					yya[yyac] = new YYARec(310,43);yyac++; 
					yya[yyac] = new YYARec(311,44);yyac++; 
					yya[yyac] = new YYARec(312,45);yyac++; 
					yya[yyac] = new YYARec(313,46);yyac++; 
					yya[yyac] = new YYARec(314,47);yyac++; 
					yya[yyac] = new YYARec(315,48);yyac++; 
					yya[yyac] = new YYARec(316,49);yyac++; 
					yya[yyac] = new YYARec(317,50);yyac++; 
					yya[yyac] = new YYARec(318,51);yyac++; 
					yya[yyac] = new YYARec(319,52);yyac++; 
					yya[yyac] = new YYARec(320,53);yyac++; 
					yya[yyac] = new YYARec(323,54);yyac++; 
					yya[yyac] = new YYARec(260,-4 );yyac++; 
					yya[yyac] = new YYARec(261,-4 );yyac++; 
					yya[yyac] = new YYARec(297,31);yyac++; 
					yya[yyac] = new YYARec(298,59);yyac++; 
					yya[yyac] = new YYARec(299,60);yyac++; 
					yya[yyac] = new YYARec(300,61);yyac++; 
					yya[yyac] = new YYARec(301,62);yyac++; 
					yya[yyac] = new YYARec(302,63);yyac++; 
					yya[yyac] = new YYARec(303,37);yyac++; 
					yya[yyac] = new YYARec(304,38);yyac++; 
					yya[yyac] = new YYARec(305,39);yyac++; 
					yya[yyac] = new YYARec(306,40);yyac++; 
					yya[yyac] = new YYARec(308,64);yyac++; 
					yya[yyac] = new YYARec(309,42);yyac++; 
					yya[yyac] = new YYARec(310,65);yyac++; 
					yya[yyac] = new YYARec(311,66);yyac++; 
					yya[yyac] = new YYARec(312,67);yyac++; 
					yya[yyac] = new YYARec(313,46);yyac++; 
					yya[yyac] = new YYARec(314,68);yyac++; 
					yya[yyac] = new YYARec(315,48);yyac++; 
					yya[yyac] = new YYARec(316,69);yyac++; 
					yya[yyac] = new YYARec(317,50);yyac++; 
					yya[yyac] = new YYARec(318,70);yyac++; 
					yya[yyac] = new YYARec(319,52);yyac++; 
					yya[yyac] = new YYARec(320,53);yyac++; 
					yya[yyac] = new YYARec(321,130);yyac++; 
					yya[yyac] = new YYARec(322,131);yyac++; 
					yya[yyac] = new YYARec(323,54);yyac++; 
					yya[yyac] = new YYARec(258,192);yyac++; 
					yya[yyac] = new YYARec(296,55);yyac++; 
					yya[yyac] = new YYARec(288,-155 );yyac++; 
					yya[yyac] = new YYARec(289,-155 );yyac++; 
					yya[yyac] = new YYARec(290,-155 );yyac++; 
					yya[yyac] = new YYARec(291,-155 );yyac++; 
					yya[yyac] = new YYARec(292,-155 );yyac++; 
					yya[yyac] = new YYARec(267,-224 );yyac++; 
					yya[yyac] = new YYARec(258,193);yyac++; 
					yya[yyac] = new YYARec(258,201);yyac++; 
					yya[yyac] = new YYARec(281,85);yyac++; 
					yya[yyac] = new YYARec(282,86);yyac++; 
					yya[yyac] = new YYARec(286,87);yyac++; 
					yya[yyac] = new YYARec(297,31);yyac++; 
					yya[yyac] = new YYARec(298,59);yyac++; 
					yya[yyac] = new YYARec(299,60);yyac++; 
					yya[yyac] = new YYARec(300,61);yyac++; 
					yya[yyac] = new YYARec(301,62);yyac++; 
					yya[yyac] = new YYARec(302,63);yyac++; 
					yya[yyac] = new YYARec(303,37);yyac++; 
					yya[yyac] = new YYARec(304,38);yyac++; 
					yya[yyac] = new YYARec(305,39);yyac++; 
					yya[yyac] = new YYARec(306,40);yyac++; 
					yya[yyac] = new YYARec(307,146);yyac++; 
					yya[yyac] = new YYARec(308,64);yyac++; 
					yya[yyac] = new YYARec(309,42);yyac++; 
					yya[yyac] = new YYARec(310,65);yyac++; 
					yya[yyac] = new YYARec(311,66);yyac++; 
					yya[yyac] = new YYARec(312,67);yyac++; 
					yya[yyac] = new YYARec(313,46);yyac++; 
					yya[yyac] = new YYARec(314,68);yyac++; 
					yya[yyac] = new YYARec(315,48);yyac++; 
					yya[yyac] = new YYARec(316,69);yyac++; 
					yya[yyac] = new YYARec(317,50);yyac++; 
					yya[yyac] = new YYARec(318,70);yyac++; 
					yya[yyac] = new YYARec(319,52);yyac++; 
					yya[yyac] = new YYARec(320,53);yyac++; 
					yya[yyac] = new YYARec(321,88);yyac++; 
					yya[yyac] = new YYARec(322,89);yyac++; 
					yya[yyac] = new YYARec(323,54);yyac++; 
					yya[yyac] = new YYARec(324,90);yyac++; 
					yya[yyac] = new YYARec(325,91);yyac++; 
					yya[yyac] = new YYARec(257,160);yyac++; 
					yya[yyac] = new YYARec(258,161);yyac++; 
					yya[yyac] = new YYARec(259,162);yyac++; 
					yya[yyac] = new YYARec(262,26);yyac++; 
					yya[yyac] = new YYARec(263,27);yyac++; 
					yya[yyac] = new YYARec(265,163);yyac++; 
					yya[yyac] = new YYARec(287,164);yyac++; 
					yya[yyac] = new YYARec(293,165);yyac++; 
					yya[yyac] = new YYARec(294,166);yyac++; 
					yya[yyac] = new YYARec(295,167);yyac++; 
					yya[yyac] = new YYARec(297,31);yyac++; 
					yya[yyac] = new YYARec(298,59);yyac++; 
					yya[yyac] = new YYARec(299,60);yyac++; 
					yya[yyac] = new YYARec(300,61);yyac++; 
					yya[yyac] = new YYARec(301,62);yyac++; 
					yya[yyac] = new YYARec(302,63);yyac++; 
					yya[yyac] = new YYARec(303,37);yyac++; 
					yya[yyac] = new YYARec(304,38);yyac++; 
					yya[yyac] = new YYARec(305,39);yyac++; 
					yya[yyac] = new YYARec(306,168);yyac++; 
					yya[yyac] = new YYARec(308,64);yyac++; 
					yya[yyac] = new YYARec(309,42);yyac++; 
					yya[yyac] = new YYARec(310,65);yyac++; 
					yya[yyac] = new YYARec(311,66);yyac++; 
					yya[yyac] = new YYARec(312,67);yyac++; 
					yya[yyac] = new YYARec(313,169);yyac++; 
					yya[yyac] = new YYARec(314,68);yyac++; 
					yya[yyac] = new YYARec(315,48);yyac++; 
					yya[yyac] = new YYARec(316,69);yyac++; 
					yya[yyac] = new YYARec(317,170);yyac++; 
					yya[yyac] = new YYARec(318,70);yyac++; 
					yya[yyac] = new YYARec(319,171);yyac++; 
					yya[yyac] = new YYARec(320,53);yyac++; 
					yya[yyac] = new YYARec(323,172);yyac++; 
					yya[yyac] = new YYARec(260,-83 );yyac++; 
					yya[yyac] = new YYARec(261,-83 );yyac++; 
					yya[yyac] = new YYARec(266,-83 );yyac++; 
					yya[yyac] = new YYARec(257,160);yyac++; 
					yya[yyac] = new YYARec(258,161);yyac++; 
					yya[yyac] = new YYARec(259,162);yyac++; 
					yya[yyac] = new YYARec(262,26);yyac++; 
					yya[yyac] = new YYARec(263,27);yyac++; 
					yya[yyac] = new YYARec(265,163);yyac++; 
					yya[yyac] = new YYARec(287,164);yyac++; 
					yya[yyac] = new YYARec(293,165);yyac++; 
					yya[yyac] = new YYARec(294,166);yyac++; 
					yya[yyac] = new YYARec(295,167);yyac++; 
					yya[yyac] = new YYARec(297,31);yyac++; 
					yya[yyac] = new YYARec(298,59);yyac++; 
					yya[yyac] = new YYARec(299,60);yyac++; 
					yya[yyac] = new YYARec(300,61);yyac++; 
					yya[yyac] = new YYARec(301,62);yyac++; 
					yya[yyac] = new YYARec(302,63);yyac++; 
					yya[yyac] = new YYARec(303,37);yyac++; 
					yya[yyac] = new YYARec(304,38);yyac++; 
					yya[yyac] = new YYARec(305,39);yyac++; 
					yya[yyac] = new YYARec(306,168);yyac++; 
					yya[yyac] = new YYARec(308,64);yyac++; 
					yya[yyac] = new YYARec(309,42);yyac++; 
					yya[yyac] = new YYARec(310,65);yyac++; 
					yya[yyac] = new YYARec(311,66);yyac++; 
					yya[yyac] = new YYARec(312,67);yyac++; 
					yya[yyac] = new YYARec(313,169);yyac++; 
					yya[yyac] = new YYARec(314,68);yyac++; 
					yya[yyac] = new YYARec(315,48);yyac++; 
					yya[yyac] = new YYARec(316,69);yyac++; 
					yya[yyac] = new YYARec(317,170);yyac++; 
					yya[yyac] = new YYARec(318,70);yyac++; 
					yya[yyac] = new YYARec(319,171);yyac++; 
					yya[yyac] = new YYARec(320,53);yyac++; 
					yya[yyac] = new YYARec(323,172);yyac++; 
					yya[yyac] = new YYARec(260,-83 );yyac++; 
					yya[yyac] = new YYARec(261,-83 );yyac++; 
					yya[yyac] = new YYARec(266,-83 );yyac++; 
					yya[yyac] = new YYARec(267,204);yyac++; 
					yya[yyac] = new YYARec(281,85);yyac++; 
					yya[yyac] = new YYARec(282,86);yyac++; 
					yya[yyac] = new YYARec(286,87);yyac++; 
					yya[yyac] = new YYARec(297,31);yyac++; 
					yya[yyac] = new YYARec(298,59);yyac++; 
					yya[yyac] = new YYARec(299,60);yyac++; 
					yya[yyac] = new YYARec(300,61);yyac++; 
					yya[yyac] = new YYARec(301,62);yyac++; 
					yya[yyac] = new YYARec(302,63);yyac++; 
					yya[yyac] = new YYARec(303,37);yyac++; 
					yya[yyac] = new YYARec(304,38);yyac++; 
					yya[yyac] = new YYARec(305,39);yyac++; 
					yya[yyac] = new YYARec(306,40);yyac++; 
					yya[yyac] = new YYARec(307,146);yyac++; 
					yya[yyac] = new YYARec(308,64);yyac++; 
					yya[yyac] = new YYARec(309,42);yyac++; 
					yya[yyac] = new YYARec(310,65);yyac++; 
					yya[yyac] = new YYARec(311,66);yyac++; 
					yya[yyac] = new YYARec(312,67);yyac++; 
					yya[yyac] = new YYARec(313,46);yyac++; 
					yya[yyac] = new YYARec(314,68);yyac++; 
					yya[yyac] = new YYARec(315,48);yyac++; 
					yya[yyac] = new YYARec(316,69);yyac++; 
					yya[yyac] = new YYARec(317,50);yyac++; 
					yya[yyac] = new YYARec(318,70);yyac++; 
					yya[yyac] = new YYARec(319,52);yyac++; 
					yya[yyac] = new YYARec(320,53);yyac++; 
					yya[yyac] = new YYARec(321,88);yyac++; 
					yya[yyac] = new YYARec(322,89);yyac++; 
					yya[yyac] = new YYARec(323,54);yyac++; 
					yya[yyac] = new YYARec(324,90);yyac++; 
					yya[yyac] = new YYARec(325,91);yyac++; 
					yya[yyac] = new YYARec(267,-214 );yyac++; 
					yya[yyac] = new YYARec(288,-214 );yyac++; 
					yya[yyac] = new YYARec(289,-214 );yyac++; 
					yya[yyac] = new YYARec(290,-214 );yyac++; 
					yya[yyac] = new YYARec(291,-214 );yyac++; 
					yya[yyac] = new YYARec(292,-214 );yyac++; 
					yya[yyac] = new YYARec(296,-214 );yyac++; 
					yya[yyac] = new YYARec(266,206);yyac++; 
					yya[yyac] = new YYARec(257,160);yyac++; 
					yya[yyac] = new YYARec(258,161);yyac++; 
					yya[yyac] = new YYARec(259,162);yyac++; 
					yya[yyac] = new YYARec(262,26);yyac++; 
					yya[yyac] = new YYARec(263,27);yyac++; 
					yya[yyac] = new YYARec(265,163);yyac++; 
					yya[yyac] = new YYARec(287,164);yyac++; 
					yya[yyac] = new YYARec(293,165);yyac++; 
					yya[yyac] = new YYARec(294,166);yyac++; 
					yya[yyac] = new YYARec(295,167);yyac++; 
					yya[yyac] = new YYARec(297,31);yyac++; 
					yya[yyac] = new YYARec(298,59);yyac++; 
					yya[yyac] = new YYARec(299,60);yyac++; 
					yya[yyac] = new YYARec(300,61);yyac++; 
					yya[yyac] = new YYARec(301,62);yyac++; 
					yya[yyac] = new YYARec(302,63);yyac++; 
					yya[yyac] = new YYARec(303,37);yyac++; 
					yya[yyac] = new YYARec(304,38);yyac++; 
					yya[yyac] = new YYARec(305,39);yyac++; 
					yya[yyac] = new YYARec(306,168);yyac++; 
					yya[yyac] = new YYARec(308,64);yyac++; 
					yya[yyac] = new YYARec(309,42);yyac++; 
					yya[yyac] = new YYARec(310,65);yyac++; 
					yya[yyac] = new YYARec(311,66);yyac++; 
					yya[yyac] = new YYARec(312,67);yyac++; 
					yya[yyac] = new YYARec(313,169);yyac++; 
					yya[yyac] = new YYARec(314,68);yyac++; 
					yya[yyac] = new YYARec(315,48);yyac++; 
					yya[yyac] = new YYARec(316,69);yyac++; 
					yya[yyac] = new YYARec(317,170);yyac++; 
					yya[yyac] = new YYARec(318,70);yyac++; 
					yya[yyac] = new YYARec(319,171);yyac++; 
					yya[yyac] = new YYARec(320,53);yyac++; 
					yya[yyac] = new YYARec(323,172);yyac++; 
					yya[yyac] = new YYARec(260,-83 );yyac++; 
					yya[yyac] = new YYARec(261,-83 );yyac++; 
					yya[yyac] = new YYARec(266,-83 );yyac++; 
					yya[yyac] = new YYARec(288,209);yyac++; 
					yya[yyac] = new YYARec(289,210);yyac++; 
					yya[yyac] = new YYARec(290,211);yyac++; 
					yya[yyac] = new YYARec(291,212);yyac++; 
					yya[yyac] = new YYARec(292,213);yyac++; 
					yya[yyac] = new YYARec(308,98);yyac++; 
					yya[yyac] = new YYARec(314,99);yyac++; 
					yya[yyac] = new YYARec(316,100);yyac++; 
					yya[yyac] = new YYARec(323,54);yyac++; 
					yya[yyac] = new YYARec(308,98);yyac++; 
					yya[yyac] = new YYARec(314,99);yyac++; 
					yya[yyac] = new YYARec(316,100);yyac++; 
					yya[yyac] = new YYARec(323,54);yyac++; 
					yya[yyac] = new YYARec(257,160);yyac++; 
					yya[yyac] = new YYARec(258,161);yyac++; 
					yya[yyac] = new YYARec(259,162);yyac++; 
					yya[yyac] = new YYARec(262,26);yyac++; 
					yya[yyac] = new YYARec(263,27);yyac++; 
					yya[yyac] = new YYARec(265,163);yyac++; 
					yya[yyac] = new YYARec(287,164);yyac++; 
					yya[yyac] = new YYARec(293,165);yyac++; 
					yya[yyac] = new YYARec(294,166);yyac++; 
					yya[yyac] = new YYARec(295,167);yyac++; 
					yya[yyac] = new YYARec(297,31);yyac++; 
					yya[yyac] = new YYARec(298,59);yyac++; 
					yya[yyac] = new YYARec(299,60);yyac++; 
					yya[yyac] = new YYARec(300,61);yyac++; 
					yya[yyac] = new YYARec(301,62);yyac++; 
					yya[yyac] = new YYARec(302,63);yyac++; 
					yya[yyac] = new YYARec(303,37);yyac++; 
					yya[yyac] = new YYARec(304,38);yyac++; 
					yya[yyac] = new YYARec(305,39);yyac++; 
					yya[yyac] = new YYARec(306,168);yyac++; 
					yya[yyac] = new YYARec(308,64);yyac++; 
					yya[yyac] = new YYARec(309,42);yyac++; 
					yya[yyac] = new YYARec(310,65);yyac++; 
					yya[yyac] = new YYARec(311,66);yyac++; 
					yya[yyac] = new YYARec(312,67);yyac++; 
					yya[yyac] = new YYARec(313,169);yyac++; 
					yya[yyac] = new YYARec(314,68);yyac++; 
					yya[yyac] = new YYARec(315,48);yyac++; 
					yya[yyac] = new YYARec(316,69);yyac++; 
					yya[yyac] = new YYARec(317,170);yyac++; 
					yya[yyac] = new YYARec(318,70);yyac++; 
					yya[yyac] = new YYARec(319,171);yyac++; 
					yya[yyac] = new YYARec(320,53);yyac++; 
					yya[yyac] = new YYARec(323,172);yyac++; 
					yya[yyac] = new YYARec(266,-83 );yyac++; 
					yya[yyac] = new YYARec(273,234);yyac++; 
					yya[yyac] = new YYARec(281,85);yyac++; 
					yya[yyac] = new YYARec(282,86);yyac++; 
					yya[yyac] = new YYARec(286,87);yyac++; 
					yya[yyac] = new YYARec(297,31);yyac++; 
					yya[yyac] = new YYARec(298,59);yyac++; 
					yya[yyac] = new YYARec(299,60);yyac++; 
					yya[yyac] = new YYARec(300,61);yyac++; 
					yya[yyac] = new YYARec(301,62);yyac++; 
					yya[yyac] = new YYARec(302,63);yyac++; 
					yya[yyac] = new YYARec(303,235);yyac++; 
					yya[yyac] = new YYARec(304,38);yyac++; 
					yya[yyac] = new YYARec(305,39);yyac++; 
					yya[yyac] = new YYARec(306,40);yyac++; 
					yya[yyac] = new YYARec(307,146);yyac++; 
					yya[yyac] = new YYARec(308,64);yyac++; 
					yya[yyac] = new YYARec(309,42);yyac++; 
					yya[yyac] = new YYARec(310,65);yyac++; 
					yya[yyac] = new YYARec(311,66);yyac++; 
					yya[yyac] = new YYARec(312,67);yyac++; 
					yya[yyac] = new YYARec(313,236);yyac++; 
					yya[yyac] = new YYARec(314,237);yyac++; 
					yya[yyac] = new YYARec(315,48);yyac++; 
					yya[yyac] = new YYARec(316,69);yyac++; 
					yya[yyac] = new YYARec(317,50);yyac++; 
					yya[yyac] = new YYARec(318,70);yyac++; 
					yya[yyac] = new YYARec(319,52);yyac++; 
					yya[yyac] = new YYARec(320,53);yyac++; 
					yya[yyac] = new YYARec(321,238);yyac++; 
					yya[yyac] = new YYARec(322,239);yyac++; 
					yya[yyac] = new YYARec(323,54);yyac++; 
					yya[yyac] = new YYARec(265,240);yyac++; 
					yya[yyac] = new YYARec(258,-173 );yyac++; 
					yya[yyac] = new YYARec(281,-173 );yyac++; 
					yya[yyac] = new YYARec(282,-173 );yyac++; 
					yya[yyac] = new YYARec(286,-173 );yyac++; 
					yya[yyac] = new YYARec(297,-173 );yyac++; 
					yya[yyac] = new YYARec(298,-173 );yyac++; 
					yya[yyac] = new YYARec(299,-173 );yyac++; 
					yya[yyac] = new YYARec(300,-173 );yyac++; 
					yya[yyac] = new YYARec(301,-173 );yyac++; 
					yya[yyac] = new YYARec(302,-173 );yyac++; 
					yya[yyac] = new YYARec(303,-173 );yyac++; 
					yya[yyac] = new YYARec(304,-173 );yyac++; 
					yya[yyac] = new YYARec(305,-173 );yyac++; 
					yya[yyac] = new YYARec(306,-173 );yyac++; 
					yya[yyac] = new YYARec(307,-173 );yyac++; 
					yya[yyac] = new YYARec(308,-173 );yyac++; 
					yya[yyac] = new YYARec(309,-173 );yyac++; 
					yya[yyac] = new YYARec(310,-173 );yyac++; 
					yya[yyac] = new YYARec(311,-173 );yyac++; 
					yya[yyac] = new YYARec(312,-173 );yyac++; 
					yya[yyac] = new YYARec(313,-173 );yyac++; 
					yya[yyac] = new YYARec(314,-173 );yyac++; 
					yya[yyac] = new YYARec(315,-173 );yyac++; 
					yya[yyac] = new YYARec(316,-173 );yyac++; 
					yya[yyac] = new YYARec(317,-173 );yyac++; 
					yya[yyac] = new YYARec(318,-173 );yyac++; 
					yya[yyac] = new YYARec(319,-173 );yyac++; 
					yya[yyac] = new YYARec(320,-173 );yyac++; 
					yya[yyac] = new YYARec(321,-173 );yyac++; 
					yya[yyac] = new YYARec(322,-173 );yyac++; 
					yya[yyac] = new YYARec(323,-173 );yyac++; 
					yya[yyac] = new YYARec(324,-173 );yyac++; 
					yya[yyac] = new YYARec(325,-173 );yyac++; 
					yya[yyac] = new YYARec(273,234);yyac++; 
					yya[yyac] = new YYARec(281,85);yyac++; 
					yya[yyac] = new YYARec(282,86);yyac++; 
					yya[yyac] = new YYARec(286,87);yyac++; 
					yya[yyac] = new YYARec(297,31);yyac++; 
					yya[yyac] = new YYARec(298,59);yyac++; 
					yya[yyac] = new YYARec(299,60);yyac++; 
					yya[yyac] = new YYARec(300,61);yyac++; 
					yya[yyac] = new YYARec(301,62);yyac++; 
					yya[yyac] = new YYARec(302,63);yyac++; 
					yya[yyac] = new YYARec(303,235);yyac++; 
					yya[yyac] = new YYARec(304,38);yyac++; 
					yya[yyac] = new YYARec(305,39);yyac++; 
					yya[yyac] = new YYARec(306,40);yyac++; 
					yya[yyac] = new YYARec(307,146);yyac++; 
					yya[yyac] = new YYARec(308,64);yyac++; 
					yya[yyac] = new YYARec(309,42);yyac++; 
					yya[yyac] = new YYARec(310,65);yyac++; 
					yya[yyac] = new YYARec(311,66);yyac++; 
					yya[yyac] = new YYARec(312,67);yyac++; 
					yya[yyac] = new YYARec(313,236);yyac++; 
					yya[yyac] = new YYARec(314,237);yyac++; 
					yya[yyac] = new YYARec(315,48);yyac++; 
					yya[yyac] = new YYARec(316,69);yyac++; 
					yya[yyac] = new YYARec(317,50);yyac++; 
					yya[yyac] = new YYARec(318,70);yyac++; 
					yya[yyac] = new YYARec(319,52);yyac++; 
					yya[yyac] = new YYARec(320,53);yyac++; 
					yya[yyac] = new YYARec(321,238);yyac++; 
					yya[yyac] = new YYARec(322,239);yyac++; 
					yya[yyac] = new YYARec(323,54);yyac++; 
					yya[yyac] = new YYARec(273,234);yyac++; 
					yya[yyac] = new YYARec(281,85);yyac++; 
					yya[yyac] = new YYARec(282,86);yyac++; 
					yya[yyac] = new YYARec(286,87);yyac++; 
					yya[yyac] = new YYARec(297,31);yyac++; 
					yya[yyac] = new YYARec(298,59);yyac++; 
					yya[yyac] = new YYARec(299,60);yyac++; 
					yya[yyac] = new YYARec(300,61);yyac++; 
					yya[yyac] = new YYARec(301,62);yyac++; 
					yya[yyac] = new YYARec(302,63);yyac++; 
					yya[yyac] = new YYARec(303,235);yyac++; 
					yya[yyac] = new YYARec(304,38);yyac++; 
					yya[yyac] = new YYARec(305,39);yyac++; 
					yya[yyac] = new YYARec(306,40);yyac++; 
					yya[yyac] = new YYARec(307,146);yyac++; 
					yya[yyac] = new YYARec(308,64);yyac++; 
					yya[yyac] = new YYARec(309,42);yyac++; 
					yya[yyac] = new YYARec(310,65);yyac++; 
					yya[yyac] = new YYARec(311,66);yyac++; 
					yya[yyac] = new YYARec(312,67);yyac++; 
					yya[yyac] = new YYARec(313,236);yyac++; 
					yya[yyac] = new YYARec(314,237);yyac++; 
					yya[yyac] = new YYARec(315,48);yyac++; 
					yya[yyac] = new YYARec(316,69);yyac++; 
					yya[yyac] = new YYARec(317,50);yyac++; 
					yya[yyac] = new YYARec(318,70);yyac++; 
					yya[yyac] = new YYARec(319,52);yyac++; 
					yya[yyac] = new YYARec(320,53);yyac++; 
					yya[yyac] = new YYARec(321,238);yyac++; 
					yya[yyac] = new YYARec(322,239);yyac++; 
					yya[yyac] = new YYARec(323,54);yyac++; 
					yya[yyac] = new YYARec(258,-177 );yyac++; 
					yya[yyac] = new YYARec(281,-177 );yyac++; 
					yya[yyac] = new YYARec(282,-177 );yyac++; 
					yya[yyac] = new YYARec(286,-177 );yyac++; 
					yya[yyac] = new YYARec(297,-177 );yyac++; 
					yya[yyac] = new YYARec(298,-177 );yyac++; 
					yya[yyac] = new YYARec(299,-177 );yyac++; 
					yya[yyac] = new YYARec(300,-177 );yyac++; 
					yya[yyac] = new YYARec(301,-177 );yyac++; 
					yya[yyac] = new YYARec(302,-177 );yyac++; 
					yya[yyac] = new YYARec(303,-177 );yyac++; 
					yya[yyac] = new YYARec(304,-177 );yyac++; 
					yya[yyac] = new YYARec(305,-177 );yyac++; 
					yya[yyac] = new YYARec(306,-177 );yyac++; 
					yya[yyac] = new YYARec(307,-177 );yyac++; 
					yya[yyac] = new YYARec(308,-177 );yyac++; 
					yya[yyac] = new YYARec(309,-177 );yyac++; 
					yya[yyac] = new YYARec(310,-177 );yyac++; 
					yya[yyac] = new YYARec(311,-177 );yyac++; 
					yya[yyac] = new YYARec(312,-177 );yyac++; 
					yya[yyac] = new YYARec(313,-177 );yyac++; 
					yya[yyac] = new YYARec(314,-177 );yyac++; 
					yya[yyac] = new YYARec(315,-177 );yyac++; 
					yya[yyac] = new YYARec(316,-177 );yyac++; 
					yya[yyac] = new YYARec(317,-177 );yyac++; 
					yya[yyac] = new YYARec(318,-177 );yyac++; 
					yya[yyac] = new YYARec(319,-177 );yyac++; 
					yya[yyac] = new YYARec(320,-177 );yyac++; 
					yya[yyac] = new YYARec(321,-177 );yyac++; 
					yya[yyac] = new YYARec(322,-177 );yyac++; 
					yya[yyac] = new YYARec(323,-177 );yyac++; 
					yya[yyac] = new YYARec(324,-177 );yyac++; 
					yya[yyac] = new YYARec(325,-177 );yyac++; 
					yya[yyac] = new YYARec(267,-198 );yyac++; 
					yya[yyac] = new YYARec(288,-198 );yyac++; 
					yya[yyac] = new YYARec(289,-198 );yyac++; 
					yya[yyac] = new YYARec(290,-198 );yyac++; 
					yya[yyac] = new YYARec(291,-198 );yyac++; 
					yya[yyac] = new YYARec(292,-198 );yyac++; 
					yya[yyac] = new YYARec(296,-198 );yyac++; 
					yya[yyac] = new YYARec(258,-176 );yyac++; 
					yya[yyac] = new YYARec(281,-176 );yyac++; 
					yya[yyac] = new YYARec(282,-176 );yyac++; 
					yya[yyac] = new YYARec(286,-176 );yyac++; 
					yya[yyac] = new YYARec(297,-176 );yyac++; 
					yya[yyac] = new YYARec(298,-176 );yyac++; 
					yya[yyac] = new YYARec(299,-176 );yyac++; 
					yya[yyac] = new YYARec(300,-176 );yyac++; 
					yya[yyac] = new YYARec(301,-176 );yyac++; 
					yya[yyac] = new YYARec(302,-176 );yyac++; 
					yya[yyac] = new YYARec(303,-176 );yyac++; 
					yya[yyac] = new YYARec(304,-176 );yyac++; 
					yya[yyac] = new YYARec(305,-176 );yyac++; 
					yya[yyac] = new YYARec(306,-176 );yyac++; 
					yya[yyac] = new YYARec(307,-176 );yyac++; 
					yya[yyac] = new YYARec(308,-176 );yyac++; 
					yya[yyac] = new YYARec(309,-176 );yyac++; 
					yya[yyac] = new YYARec(310,-176 );yyac++; 
					yya[yyac] = new YYARec(311,-176 );yyac++; 
					yya[yyac] = new YYARec(312,-176 );yyac++; 
					yya[yyac] = new YYARec(313,-176 );yyac++; 
					yya[yyac] = new YYARec(314,-176 );yyac++; 
					yya[yyac] = new YYARec(315,-176 );yyac++; 
					yya[yyac] = new YYARec(316,-176 );yyac++; 
					yya[yyac] = new YYARec(317,-176 );yyac++; 
					yya[yyac] = new YYARec(318,-176 );yyac++; 
					yya[yyac] = new YYARec(319,-176 );yyac++; 
					yya[yyac] = new YYARec(320,-176 );yyac++; 
					yya[yyac] = new YYARec(321,-176 );yyac++; 
					yya[yyac] = new YYARec(322,-176 );yyac++; 
					yya[yyac] = new YYARec(323,-176 );yyac++; 
					yya[yyac] = new YYARec(324,-176 );yyac++; 
					yya[yyac] = new YYARec(325,-176 );yyac++; 
					yya[yyac] = new YYARec(267,-195 );yyac++; 
					yya[yyac] = new YYARec(288,-195 );yyac++; 
					yya[yyac] = new YYARec(289,-195 );yyac++; 
					yya[yyac] = new YYARec(290,-195 );yyac++; 
					yya[yyac] = new YYARec(291,-195 );yyac++; 
					yya[yyac] = new YYARec(292,-195 );yyac++; 
					yya[yyac] = new YYARec(296,-195 );yyac++; 
					yya[yyac] = new YYARec(258,-174 );yyac++; 
					yya[yyac] = new YYARec(281,-174 );yyac++; 
					yya[yyac] = new YYARec(282,-174 );yyac++; 
					yya[yyac] = new YYARec(286,-174 );yyac++; 
					yya[yyac] = new YYARec(297,-174 );yyac++; 
					yya[yyac] = new YYARec(298,-174 );yyac++; 
					yya[yyac] = new YYARec(299,-174 );yyac++; 
					yya[yyac] = new YYARec(300,-174 );yyac++; 
					yya[yyac] = new YYARec(301,-174 );yyac++; 
					yya[yyac] = new YYARec(302,-174 );yyac++; 
					yya[yyac] = new YYARec(303,-174 );yyac++; 
					yya[yyac] = new YYARec(304,-174 );yyac++; 
					yya[yyac] = new YYARec(305,-174 );yyac++; 
					yya[yyac] = new YYARec(306,-174 );yyac++; 
					yya[yyac] = new YYARec(307,-174 );yyac++; 
					yya[yyac] = new YYARec(308,-174 );yyac++; 
					yya[yyac] = new YYARec(309,-174 );yyac++; 
					yya[yyac] = new YYARec(310,-174 );yyac++; 
					yya[yyac] = new YYARec(311,-174 );yyac++; 
					yya[yyac] = new YYARec(312,-174 );yyac++; 
					yya[yyac] = new YYARec(313,-174 );yyac++; 
					yya[yyac] = new YYARec(314,-174 );yyac++; 
					yya[yyac] = new YYARec(315,-174 );yyac++; 
					yya[yyac] = new YYARec(316,-174 );yyac++; 
					yya[yyac] = new YYARec(317,-174 );yyac++; 
					yya[yyac] = new YYARec(318,-174 );yyac++; 
					yya[yyac] = new YYARec(319,-174 );yyac++; 
					yya[yyac] = new YYARec(320,-174 );yyac++; 
					yya[yyac] = new YYARec(321,-174 );yyac++; 
					yya[yyac] = new YYARec(322,-174 );yyac++; 
					yya[yyac] = new YYARec(323,-174 );yyac++; 
					yya[yyac] = new YYARec(324,-174 );yyac++; 
					yya[yyac] = new YYARec(325,-174 );yyac++; 
					yya[yyac] = new YYARec(267,-191 );yyac++; 
					yya[yyac] = new YYARec(288,-191 );yyac++; 
					yya[yyac] = new YYARec(289,-191 );yyac++; 
					yya[yyac] = new YYARec(290,-191 );yyac++; 
					yya[yyac] = new YYARec(291,-191 );yyac++; 
					yya[yyac] = new YYARec(292,-191 );yyac++; 
					yya[yyac] = new YYARec(296,-191 );yyac++; 
					yya[yyac] = new YYARec(258,-175 );yyac++; 
					yya[yyac] = new YYARec(281,-175 );yyac++; 
					yya[yyac] = new YYARec(282,-175 );yyac++; 
					yya[yyac] = new YYARec(286,-175 );yyac++; 
					yya[yyac] = new YYARec(297,-175 );yyac++; 
					yya[yyac] = new YYARec(298,-175 );yyac++; 
					yya[yyac] = new YYARec(299,-175 );yyac++; 
					yya[yyac] = new YYARec(300,-175 );yyac++; 
					yya[yyac] = new YYARec(301,-175 );yyac++; 
					yya[yyac] = new YYARec(302,-175 );yyac++; 
					yya[yyac] = new YYARec(303,-175 );yyac++; 
					yya[yyac] = new YYARec(304,-175 );yyac++; 
					yya[yyac] = new YYARec(305,-175 );yyac++; 
					yya[yyac] = new YYARec(306,-175 );yyac++; 
					yya[yyac] = new YYARec(307,-175 );yyac++; 
					yya[yyac] = new YYARec(308,-175 );yyac++; 
					yya[yyac] = new YYARec(309,-175 );yyac++; 
					yya[yyac] = new YYARec(310,-175 );yyac++; 
					yya[yyac] = new YYARec(311,-175 );yyac++; 
					yya[yyac] = new YYARec(312,-175 );yyac++; 
					yya[yyac] = new YYARec(313,-175 );yyac++; 
					yya[yyac] = new YYARec(314,-175 );yyac++; 
					yya[yyac] = new YYARec(315,-175 );yyac++; 
					yya[yyac] = new YYARec(316,-175 );yyac++; 
					yya[yyac] = new YYARec(317,-175 );yyac++; 
					yya[yyac] = new YYARec(318,-175 );yyac++; 
					yya[yyac] = new YYARec(319,-175 );yyac++; 
					yya[yyac] = new YYARec(320,-175 );yyac++; 
					yya[yyac] = new YYARec(321,-175 );yyac++; 
					yya[yyac] = new YYARec(322,-175 );yyac++; 
					yya[yyac] = new YYARec(323,-175 );yyac++; 
					yya[yyac] = new YYARec(324,-175 );yyac++; 
					yya[yyac] = new YYARec(325,-175 );yyac++; 
					yya[yyac] = new YYARec(267,-192 );yyac++; 
					yya[yyac] = new YYARec(288,-192 );yyac++; 
					yya[yyac] = new YYARec(289,-192 );yyac++; 
					yya[yyac] = new YYARec(290,-192 );yyac++; 
					yya[yyac] = new YYARec(291,-192 );yyac++; 
					yya[yyac] = new YYARec(292,-192 );yyac++; 
					yya[yyac] = new YYARec(296,-192 );yyac++; 
					yya[yyac] = new YYARec(258,244);yyac++; 
					yya[yyac] = new YYARec(267,-186 );yyac++; 
					yya[yyac] = new YYARec(281,-186 );yyac++; 
					yya[yyac] = new YYARec(282,-186 );yyac++; 
					yya[yyac] = new YYARec(286,-186 );yyac++; 
					yya[yyac] = new YYARec(288,-186 );yyac++; 
					yya[yyac] = new YYARec(289,-186 );yyac++; 
					yya[yyac] = new YYARec(290,-186 );yyac++; 
					yya[yyac] = new YYARec(291,-186 );yyac++; 
					yya[yyac] = new YYARec(292,-186 );yyac++; 
					yya[yyac] = new YYARec(296,-186 );yyac++; 
					yya[yyac] = new YYARec(297,-186 );yyac++; 
					yya[yyac] = new YYARec(298,-186 );yyac++; 
					yya[yyac] = new YYARec(299,-186 );yyac++; 
					yya[yyac] = new YYARec(300,-186 );yyac++; 
					yya[yyac] = new YYARec(301,-186 );yyac++; 
					yya[yyac] = new YYARec(302,-186 );yyac++; 
					yya[yyac] = new YYARec(303,-186 );yyac++; 
					yya[yyac] = new YYARec(304,-186 );yyac++; 
					yya[yyac] = new YYARec(305,-186 );yyac++; 
					yya[yyac] = new YYARec(306,-186 );yyac++; 
					yya[yyac] = new YYARec(307,-186 );yyac++; 
					yya[yyac] = new YYARec(308,-186 );yyac++; 
					yya[yyac] = new YYARec(309,-186 );yyac++; 
					yya[yyac] = new YYARec(310,-186 );yyac++; 
					yya[yyac] = new YYARec(311,-186 );yyac++; 
					yya[yyac] = new YYARec(312,-186 );yyac++; 
					yya[yyac] = new YYARec(313,-186 );yyac++; 
					yya[yyac] = new YYARec(314,-186 );yyac++; 
					yya[yyac] = new YYARec(315,-186 );yyac++; 
					yya[yyac] = new YYARec(316,-186 );yyac++; 
					yya[yyac] = new YYARec(317,-186 );yyac++; 
					yya[yyac] = new YYARec(318,-186 );yyac++; 
					yya[yyac] = new YYARec(319,-186 );yyac++; 
					yya[yyac] = new YYARec(320,-186 );yyac++; 
					yya[yyac] = new YYARec(321,-186 );yyac++; 
					yya[yyac] = new YYARec(322,-186 );yyac++; 
					yya[yyac] = new YYARec(323,-186 );yyac++; 
					yya[yyac] = new YYARec(324,-186 );yyac++; 
					yya[yyac] = new YYARec(325,-186 );yyac++; 
					yya[yyac] = new YYARec(281,85);yyac++; 
					yya[yyac] = new YYARec(282,86);yyac++; 
					yya[yyac] = new YYARec(286,87);yyac++; 
					yya[yyac] = new YYARec(297,31);yyac++; 
					yya[yyac] = new YYARec(298,59);yyac++; 
					yya[yyac] = new YYARec(299,60);yyac++; 
					yya[yyac] = new YYARec(300,61);yyac++; 
					yya[yyac] = new YYARec(301,62);yyac++; 
					yya[yyac] = new YYARec(302,63);yyac++; 
					yya[yyac] = new YYARec(303,37);yyac++; 
					yya[yyac] = new YYARec(304,38);yyac++; 
					yya[yyac] = new YYARec(305,39);yyac++; 
					yya[yyac] = new YYARec(306,40);yyac++; 
					yya[yyac] = new YYARec(308,64);yyac++; 
					yya[yyac] = new YYARec(309,42);yyac++; 
					yya[yyac] = new YYARec(310,65);yyac++; 
					yya[yyac] = new YYARec(311,66);yyac++; 
					yya[yyac] = new YYARec(312,67);yyac++; 
					yya[yyac] = new YYARec(313,46);yyac++; 
					yya[yyac] = new YYARec(314,68);yyac++; 
					yya[yyac] = new YYARec(315,48);yyac++; 
					yya[yyac] = new YYARec(316,69);yyac++; 
					yya[yyac] = new YYARec(317,50);yyac++; 
					yya[yyac] = new YYARec(318,70);yyac++; 
					yya[yyac] = new YYARec(319,52);yyac++; 
					yya[yyac] = new YYARec(320,53);yyac++; 
					yya[yyac] = new YYARec(321,88);yyac++; 
					yya[yyac] = new YYARec(322,89);yyac++; 
					yya[yyac] = new YYARec(323,54);yyac++; 
					yya[yyac] = new YYARec(324,90);yyac++; 
					yya[yyac] = new YYARec(325,91);yyac++; 
					yya[yyac] = new YYARec(257,180);yyac++; 
					yya[yyac] = new YYARec(258,181);yyac++; 
					yya[yyac] = new YYARec(259,182);yyac++; 
					yya[yyac] = new YYARec(300,110);yyac++; 
					yya[yyac] = new YYARec(301,111);yyac++; 
					yya[yyac] = new YYARec(305,113);yyac++; 
					yya[yyac] = new YYARec(310,114);yyac++; 
					yya[yyac] = new YYARec(311,115);yyac++; 
					yya[yyac] = new YYARec(314,117);yyac++; 
					yya[yyac] = new YYARec(316,119);yyac++; 
					yya[yyac] = new YYARec(318,121);yyac++; 
					yya[yyac] = new YYARec(319,122);yyac++; 
					yya[yyac] = new YYARec(323,54);yyac++; 
					yya[yyac] = new YYARec(260,-66 );yyac++; 
					yya[yyac] = new YYARec(261,-66 );yyac++; 
					yya[yyac] = new YYARec(266,-66 );yyac++; 
					yya[yyac] = new YYARec(266,252);yyac++; 
					yya[yyac] = new YYARec(257,180);yyac++; 
					yya[yyac] = new YYARec(258,181);yyac++; 
					yya[yyac] = new YYARec(259,182);yyac++; 
					yya[yyac] = new YYARec(300,110);yyac++; 
					yya[yyac] = new YYARec(301,111);yyac++; 
					yya[yyac] = new YYARec(305,113);yyac++; 
					yya[yyac] = new YYARec(310,114);yyac++; 
					yya[yyac] = new YYARec(311,115);yyac++; 
					yya[yyac] = new YYARec(314,117);yyac++; 
					yya[yyac] = new YYARec(316,119);yyac++; 
					yya[yyac] = new YYARec(318,121);yyac++; 
					yya[yyac] = new YYARec(319,122);yyac++; 
					yya[yyac] = new YYARec(323,54);yyac++; 
					yya[yyac] = new YYARec(260,-66 );yyac++; 
					yya[yyac] = new YYARec(261,-66 );yyac++; 
					yya[yyac] = new YYARec(266,-66 );yyac++; 
					yya[yyac] = new YYARec(308,98);yyac++; 
					yya[yyac] = new YYARec(314,99);yyac++; 
					yya[yyac] = new YYARec(316,100);yyac++; 
					yya[yyac] = new YYARec(323,54);yyac++; 
					yya[yyac] = new YYARec(308,98);yyac++; 
					yya[yyac] = new YYARec(314,99);yyac++; 
					yya[yyac] = new YYARec(316,100);yyac++; 
					yya[yyac] = new YYARec(323,54);yyac++; 
					yya[yyac] = new YYARec(321,130);yyac++; 
					yya[yyac] = new YYARec(281,85);yyac++; 
					yya[yyac] = new YYARec(282,86);yyac++; 
					yya[yyac] = new YYARec(286,87);yyac++; 
					yya[yyac] = new YYARec(321,88);yyac++; 
					yya[yyac] = new YYARec(323,54);yyac++; 
					yya[yyac] = new YYARec(258,-54 );yyac++; 
					yya[yyac] = new YYARec(258,257);yyac++; 
					yya[yyac] = new YYARec(258,258);yyac++; 
					yya[yyac] = new YYARec(260,259);yyac++; 
					yya[yyac] = new YYARec(261,260);yyac++; 
					yya[yyac] = new YYARec(258,261);yyac++; 
					yya[yyac] = new YYARec(281,85);yyac++; 
					yya[yyac] = new YYARec(282,86);yyac++; 
					yya[yyac] = new YYARec(286,87);yyac++; 
					yya[yyac] = new YYARec(297,31);yyac++; 
					yya[yyac] = new YYARec(298,59);yyac++; 
					yya[yyac] = new YYARec(299,60);yyac++; 
					yya[yyac] = new YYARec(300,61);yyac++; 
					yya[yyac] = new YYARec(301,62);yyac++; 
					yya[yyac] = new YYARec(302,63);yyac++; 
					yya[yyac] = new YYARec(303,37);yyac++; 
					yya[yyac] = new YYARec(304,38);yyac++; 
					yya[yyac] = new YYARec(305,39);yyac++; 
					yya[yyac] = new YYARec(306,40);yyac++; 
					yya[yyac] = new YYARec(307,146);yyac++; 
					yya[yyac] = new YYARec(308,64);yyac++; 
					yya[yyac] = new YYARec(309,42);yyac++; 
					yya[yyac] = new YYARec(310,65);yyac++; 
					yya[yyac] = new YYARec(311,66);yyac++; 
					yya[yyac] = new YYARec(312,67);yyac++; 
					yya[yyac] = new YYARec(313,46);yyac++; 
					yya[yyac] = new YYARec(314,68);yyac++; 
					yya[yyac] = new YYARec(315,48);yyac++; 
					yya[yyac] = new YYARec(316,69);yyac++; 
					yya[yyac] = new YYARec(317,50);yyac++; 
					yya[yyac] = new YYARec(318,70);yyac++; 
					yya[yyac] = new YYARec(319,52);yyac++; 
					yya[yyac] = new YYARec(320,53);yyac++; 
					yya[yyac] = new YYARec(321,88);yyac++; 
					yya[yyac] = new YYARec(322,89);yyac++; 
					yya[yyac] = new YYARec(323,54);yyac++; 
					yya[yyac] = new YYARec(324,90);yyac++; 
					yya[yyac] = new YYARec(325,91);yyac++; 
					yya[yyac] = new YYARec(258,-90 );yyac++; 
					yya[yyac] = new YYARec(258,263);yyac++; 
					yya[yyac] = new YYARec(257,160);yyac++; 
					yya[yyac] = new YYARec(258,161);yyac++; 
					yya[yyac] = new YYARec(259,162);yyac++; 
					yya[yyac] = new YYARec(262,26);yyac++; 
					yya[yyac] = new YYARec(263,27);yyac++; 
					yya[yyac] = new YYARec(265,163);yyac++; 
					yya[yyac] = new YYARec(287,164);yyac++; 
					yya[yyac] = new YYARec(293,165);yyac++; 
					yya[yyac] = new YYARec(294,166);yyac++; 
					yya[yyac] = new YYARec(295,167);yyac++; 
					yya[yyac] = new YYARec(297,31);yyac++; 
					yya[yyac] = new YYARec(298,59);yyac++; 
					yya[yyac] = new YYARec(299,60);yyac++; 
					yya[yyac] = new YYARec(300,61);yyac++; 
					yya[yyac] = new YYARec(301,62);yyac++; 
					yya[yyac] = new YYARec(302,63);yyac++; 
					yya[yyac] = new YYARec(303,37);yyac++; 
					yya[yyac] = new YYARec(304,38);yyac++; 
					yya[yyac] = new YYARec(305,39);yyac++; 
					yya[yyac] = new YYARec(306,168);yyac++; 
					yya[yyac] = new YYARec(308,64);yyac++; 
					yya[yyac] = new YYARec(309,42);yyac++; 
					yya[yyac] = new YYARec(310,65);yyac++; 
					yya[yyac] = new YYARec(311,66);yyac++; 
					yya[yyac] = new YYARec(312,67);yyac++; 
					yya[yyac] = new YYARec(313,169);yyac++; 
					yya[yyac] = new YYARec(314,68);yyac++; 
					yya[yyac] = new YYARec(315,48);yyac++; 
					yya[yyac] = new YYARec(316,69);yyac++; 
					yya[yyac] = new YYARec(317,170);yyac++; 
					yya[yyac] = new YYARec(318,70);yyac++; 
					yya[yyac] = new YYARec(319,171);yyac++; 
					yya[yyac] = new YYARec(320,53);yyac++; 
					yya[yyac] = new YYARec(323,172);yyac++; 
					yya[yyac] = new YYARec(260,-83 );yyac++; 
					yya[yyac] = new YYARec(261,-83 );yyac++; 
					yya[yyac] = new YYARec(266,-83 );yyac++; 
					yya[yyac] = new YYARec(258,265);yyac++; 
					yya[yyac] = new YYARec(273,234);yyac++; 
					yya[yyac] = new YYARec(281,85);yyac++; 
					yya[yyac] = new YYARec(282,86);yyac++; 
					yya[yyac] = new YYARec(286,87);yyac++; 
					yya[yyac] = new YYARec(297,31);yyac++; 
					yya[yyac] = new YYARec(298,59);yyac++; 
					yya[yyac] = new YYARec(299,60);yyac++; 
					yya[yyac] = new YYARec(300,61);yyac++; 
					yya[yyac] = new YYARec(301,62);yyac++; 
					yya[yyac] = new YYARec(302,63);yyac++; 
					yya[yyac] = new YYARec(303,235);yyac++; 
					yya[yyac] = new YYARec(304,38);yyac++; 
					yya[yyac] = new YYARec(305,39);yyac++; 
					yya[yyac] = new YYARec(306,40);yyac++; 
					yya[yyac] = new YYARec(307,146);yyac++; 
					yya[yyac] = new YYARec(308,64);yyac++; 
					yya[yyac] = new YYARec(309,42);yyac++; 
					yya[yyac] = new YYARec(310,65);yyac++; 
					yya[yyac] = new YYARec(311,66);yyac++; 
					yya[yyac] = new YYARec(312,67);yyac++; 
					yya[yyac] = new YYARec(313,236);yyac++; 
					yya[yyac] = new YYARec(314,237);yyac++; 
					yya[yyac] = new YYARec(315,48);yyac++; 
					yya[yyac] = new YYARec(316,69);yyac++; 
					yya[yyac] = new YYARec(317,50);yyac++; 
					yya[yyac] = new YYARec(318,70);yyac++; 
					yya[yyac] = new YYARec(319,52);yyac++; 
					yya[yyac] = new YYARec(320,53);yyac++; 
					yya[yyac] = new YYARec(321,238);yyac++; 
					yya[yyac] = new YYARec(322,239);yyac++; 
					yya[yyac] = new YYARec(323,54);yyac++; 
					yya[yyac] = new YYARec(257,160);yyac++; 
					yya[yyac] = new YYARec(258,161);yyac++; 
					yya[yyac] = new YYARec(259,162);yyac++; 
					yya[yyac] = new YYARec(262,26);yyac++; 
					yya[yyac] = new YYARec(263,27);yyac++; 
					yya[yyac] = new YYARec(265,163);yyac++; 
					yya[yyac] = new YYARec(287,164);yyac++; 
					yya[yyac] = new YYARec(293,165);yyac++; 
					yya[yyac] = new YYARec(294,166);yyac++; 
					yya[yyac] = new YYARec(295,167);yyac++; 
					yya[yyac] = new YYARec(297,31);yyac++; 
					yya[yyac] = new YYARec(298,59);yyac++; 
					yya[yyac] = new YYARec(299,60);yyac++; 
					yya[yyac] = new YYARec(300,61);yyac++; 
					yya[yyac] = new YYARec(301,62);yyac++; 
					yya[yyac] = new YYARec(302,63);yyac++; 
					yya[yyac] = new YYARec(303,37);yyac++; 
					yya[yyac] = new YYARec(304,38);yyac++; 
					yya[yyac] = new YYARec(305,39);yyac++; 
					yya[yyac] = new YYARec(306,168);yyac++; 
					yya[yyac] = new YYARec(308,64);yyac++; 
					yya[yyac] = new YYARec(309,42);yyac++; 
					yya[yyac] = new YYARec(310,65);yyac++; 
					yya[yyac] = new YYARec(311,66);yyac++; 
					yya[yyac] = new YYARec(312,67);yyac++; 
					yya[yyac] = new YYARec(313,169);yyac++; 
					yya[yyac] = new YYARec(314,68);yyac++; 
					yya[yyac] = new YYARec(315,48);yyac++; 
					yya[yyac] = new YYARec(316,69);yyac++; 
					yya[yyac] = new YYARec(317,170);yyac++; 
					yya[yyac] = new YYARec(318,70);yyac++; 
					yya[yyac] = new YYARec(319,171);yyac++; 
					yya[yyac] = new YYARec(320,53);yyac++; 
					yya[yyac] = new YYARec(323,172);yyac++; 
					yya[yyac] = new YYARec(260,-83 );yyac++; 
					yya[yyac] = new YYARec(261,-83 );yyac++; 
					yya[yyac] = new YYARec(257,160);yyac++; 
					yya[yyac] = new YYARec(258,161);yyac++; 
					yya[yyac] = new YYARec(259,162);yyac++; 
					yya[yyac] = new YYARec(262,26);yyac++; 
					yya[yyac] = new YYARec(263,27);yyac++; 
					yya[yyac] = new YYARec(265,163);yyac++; 
					yya[yyac] = new YYARec(287,164);yyac++; 
					yya[yyac] = new YYARec(293,165);yyac++; 
					yya[yyac] = new YYARec(294,166);yyac++; 
					yya[yyac] = new YYARec(295,167);yyac++; 
					yya[yyac] = new YYARec(297,31);yyac++; 
					yya[yyac] = new YYARec(298,59);yyac++; 
					yya[yyac] = new YYARec(299,60);yyac++; 
					yya[yyac] = new YYARec(300,61);yyac++; 
					yya[yyac] = new YYARec(301,62);yyac++; 
					yya[yyac] = new YYARec(302,63);yyac++; 
					yya[yyac] = new YYARec(303,37);yyac++; 
					yya[yyac] = new YYARec(304,38);yyac++; 
					yya[yyac] = new YYARec(305,39);yyac++; 
					yya[yyac] = new YYARec(306,168);yyac++; 
					yya[yyac] = new YYARec(308,64);yyac++; 
					yya[yyac] = new YYARec(309,42);yyac++; 
					yya[yyac] = new YYARec(310,65);yyac++; 
					yya[yyac] = new YYARec(311,66);yyac++; 
					yya[yyac] = new YYARec(312,67);yyac++; 
					yya[yyac] = new YYARec(313,169);yyac++; 
					yya[yyac] = new YYARec(314,68);yyac++; 
					yya[yyac] = new YYARec(315,48);yyac++; 
					yya[yyac] = new YYARec(316,69);yyac++; 
					yya[yyac] = new YYARec(317,170);yyac++; 
					yya[yyac] = new YYARec(318,70);yyac++; 
					yya[yyac] = new YYARec(319,171);yyac++; 
					yya[yyac] = new YYARec(320,53);yyac++; 
					yya[yyac] = new YYARec(323,172);yyac++; 
					yya[yyac] = new YYARec(260,-83 );yyac++; 
					yya[yyac] = new YYARec(261,-83 );yyac++; 
					yya[yyac] = new YYARec(266,270);yyac++; 
					yya[yyac] = new YYARec(273,271);yyac++; 
					yya[yyac] = new YYARec(273,234);yyac++; 
					yya[yyac] = new YYARec(281,85);yyac++; 
					yya[yyac] = new YYARec(282,86);yyac++; 
					yya[yyac] = new YYARec(286,87);yyac++; 
					yya[yyac] = new YYARec(297,31);yyac++; 
					yya[yyac] = new YYARec(298,59);yyac++; 
					yya[yyac] = new YYARec(299,60);yyac++; 
					yya[yyac] = new YYARec(300,61);yyac++; 
					yya[yyac] = new YYARec(301,62);yyac++; 
					yya[yyac] = new YYARec(302,63);yyac++; 
					yya[yyac] = new YYARec(303,235);yyac++; 
					yya[yyac] = new YYARec(304,38);yyac++; 
					yya[yyac] = new YYARec(305,39);yyac++; 
					yya[yyac] = new YYARec(306,40);yyac++; 
					yya[yyac] = new YYARec(307,146);yyac++; 
					yya[yyac] = new YYARec(308,64);yyac++; 
					yya[yyac] = new YYARec(309,42);yyac++; 
					yya[yyac] = new YYARec(310,65);yyac++; 
					yya[yyac] = new YYARec(311,66);yyac++; 
					yya[yyac] = new YYARec(312,67);yyac++; 
					yya[yyac] = new YYARec(313,236);yyac++; 
					yya[yyac] = new YYARec(314,237);yyac++; 
					yya[yyac] = new YYARec(315,48);yyac++; 
					yya[yyac] = new YYARec(316,69);yyac++; 
					yya[yyac] = new YYARec(317,50);yyac++; 
					yya[yyac] = new YYARec(318,70);yyac++; 
					yya[yyac] = new YYARec(319,52);yyac++; 
					yya[yyac] = new YYARec(320,53);yyac++; 
					yya[yyac] = new YYARec(321,238);yyac++; 
					yya[yyac] = new YYARec(322,239);yyac++; 
					yya[yyac] = new YYARec(323,54);yyac++; 
					yya[yyac] = new YYARec(283,274);yyac++; 
					yya[yyac] = new YYARec(284,275);yyac++; 
					yya[yyac] = new YYARec(285,276);yyac++; 
					yya[yyac] = new YYARec(258,-112 );yyac++; 
					yya[yyac] = new YYARec(265,-112 );yyac++; 
					yya[yyac] = new YYARec(268,-112 );yyac++; 
					yya[yyac] = new YYARec(269,-112 );yyac++; 
					yya[yyac] = new YYARec(270,-112 );yyac++; 
					yya[yyac] = new YYARec(271,-112 );yyac++; 
					yya[yyac] = new YYARec(272,-112 );yyac++; 
					yya[yyac] = new YYARec(274,-112 );yyac++; 
					yya[yyac] = new YYARec(275,-112 );yyac++; 
					yya[yyac] = new YYARec(276,-112 );yyac++; 
					yya[yyac] = new YYARec(277,-112 );yyac++; 
					yya[yyac] = new YYARec(278,-112 );yyac++; 
					yya[yyac] = new YYARec(279,-112 );yyac++; 
					yya[yyac] = new YYARec(280,-112 );yyac++; 
					yya[yyac] = new YYARec(281,-112 );yyac++; 
					yya[yyac] = new YYARec(282,-112 );yyac++; 
					yya[yyac] = new YYARec(281,278);yyac++; 
					yya[yyac] = new YYARec(282,279);yyac++; 
					yya[yyac] = new YYARec(258,-110 );yyac++; 
					yya[yyac] = new YYARec(265,-110 );yyac++; 
					yya[yyac] = new YYARec(268,-110 );yyac++; 
					yya[yyac] = new YYARec(269,-110 );yyac++; 
					yya[yyac] = new YYARec(270,-110 );yyac++; 
					yya[yyac] = new YYARec(271,-110 );yyac++; 
					yya[yyac] = new YYARec(272,-110 );yyac++; 
					yya[yyac] = new YYARec(274,-110 );yyac++; 
					yya[yyac] = new YYARec(275,-110 );yyac++; 
					yya[yyac] = new YYARec(276,-110 );yyac++; 
					yya[yyac] = new YYARec(277,-110 );yyac++; 
					yya[yyac] = new YYARec(278,-110 );yyac++; 
					yya[yyac] = new YYARec(279,-110 );yyac++; 
					yya[yyac] = new YYARec(280,-110 );yyac++; 
					yya[yyac] = new YYARec(277,281);yyac++; 
					yya[yyac] = new YYARec(278,282);yyac++; 
					yya[yyac] = new YYARec(279,283);yyac++; 
					yya[yyac] = new YYARec(280,284);yyac++; 
					yya[yyac] = new YYARec(258,-108 );yyac++; 
					yya[yyac] = new YYARec(265,-108 );yyac++; 
					yya[yyac] = new YYARec(268,-108 );yyac++; 
					yya[yyac] = new YYARec(269,-108 );yyac++; 
					yya[yyac] = new YYARec(270,-108 );yyac++; 
					yya[yyac] = new YYARec(271,-108 );yyac++; 
					yya[yyac] = new YYARec(272,-108 );yyac++; 
					yya[yyac] = new YYARec(274,-108 );yyac++; 
					yya[yyac] = new YYARec(275,-108 );yyac++; 
					yya[yyac] = new YYARec(276,-108 );yyac++; 
					yya[yyac] = new YYARec(275,286);yyac++; 
					yya[yyac] = new YYARec(276,287);yyac++; 
					yya[yyac] = new YYARec(258,-107 );yyac++; 
					yya[yyac] = new YYARec(265,-107 );yyac++; 
					yya[yyac] = new YYARec(268,-107 );yyac++; 
					yya[yyac] = new YYARec(269,-107 );yyac++; 
					yya[yyac] = new YYARec(270,-107 );yyac++; 
					yya[yyac] = new YYARec(271,-107 );yyac++; 
					yya[yyac] = new YYARec(272,-107 );yyac++; 
					yya[yyac] = new YYARec(274,-107 );yyac++; 
					yya[yyac] = new YYARec(272,288);yyac++; 
					yya[yyac] = new YYARec(258,-105 );yyac++; 
					yya[yyac] = new YYARec(265,-105 );yyac++; 
					yya[yyac] = new YYARec(268,-105 );yyac++; 
					yya[yyac] = new YYARec(269,-105 );yyac++; 
					yya[yyac] = new YYARec(270,-105 );yyac++; 
					yya[yyac] = new YYARec(271,-105 );yyac++; 
					yya[yyac] = new YYARec(274,-105 );yyac++; 
					yya[yyac] = new YYARec(271,289);yyac++; 
					yya[yyac] = new YYARec(258,-103 );yyac++; 
					yya[yyac] = new YYARec(265,-103 );yyac++; 
					yya[yyac] = new YYARec(268,-103 );yyac++; 
					yya[yyac] = new YYARec(269,-103 );yyac++; 
					yya[yyac] = new YYARec(270,-103 );yyac++; 
					yya[yyac] = new YYARec(274,-103 );yyac++; 
					yya[yyac] = new YYARec(270,290);yyac++; 
					yya[yyac] = new YYARec(258,-101 );yyac++; 
					yya[yyac] = new YYARec(265,-101 );yyac++; 
					yya[yyac] = new YYARec(268,-101 );yyac++; 
					yya[yyac] = new YYARec(269,-101 );yyac++; 
					yya[yyac] = new YYARec(274,-101 );yyac++; 
					yya[yyac] = new YYARec(269,291);yyac++; 
					yya[yyac] = new YYARec(258,-99 );yyac++; 
					yya[yyac] = new YYARec(265,-99 );yyac++; 
					yya[yyac] = new YYARec(268,-99 );yyac++; 
					yya[yyac] = new YYARec(274,-99 );yyac++; 
					yya[yyac] = new YYARec(268,292);yyac++; 
					yya[yyac] = new YYARec(258,-97 );yyac++; 
					yya[yyac] = new YYARec(265,-97 );yyac++; 
					yya[yyac] = new YYARec(274,-97 );yyac++; 
					yya[yyac] = new YYARec(288,209);yyac++; 
					yya[yyac] = new YYARec(289,210);yyac++; 
					yya[yyac] = new YYARec(290,211);yyac++; 
					yya[yyac] = new YYARec(291,212);yyac++; 
					yya[yyac] = new YYARec(292,213);yyac++; 
					yya[yyac] = new YYARec(321,294);yyac++; 
					yya[yyac] = new YYARec(258,-123 );yyac++; 
					yya[yyac] = new YYARec(268,-123 );yyac++; 
					yya[yyac] = new YYARec(269,-123 );yyac++; 
					yya[yyac] = new YYARec(270,-123 );yyac++; 
					yya[yyac] = new YYARec(271,-123 );yyac++; 
					yya[yyac] = new YYARec(272,-123 );yyac++; 
					yya[yyac] = new YYARec(275,-123 );yyac++; 
					yya[yyac] = new YYARec(276,-123 );yyac++; 
					yya[yyac] = new YYARec(277,-123 );yyac++; 
					yya[yyac] = new YYARec(278,-123 );yyac++; 
					yya[yyac] = new YYARec(279,-123 );yyac++; 
					yya[yyac] = new YYARec(280,-123 );yyac++; 
					yya[yyac] = new YYARec(281,-123 );yyac++; 
					yya[yyac] = new YYARec(282,-123 );yyac++; 
					yya[yyac] = new YYARec(283,-123 );yyac++; 
					yya[yyac] = new YYARec(284,-123 );yyac++; 
					yya[yyac] = new YYARec(285,-123 );yyac++; 
					yya[yyac] = new YYARec(273,234);yyac++; 
					yya[yyac] = new YYARec(281,85);yyac++; 
					yya[yyac] = new YYARec(282,86);yyac++; 
					yya[yyac] = new YYARec(286,87);yyac++; 
					yya[yyac] = new YYARec(297,31);yyac++; 
					yya[yyac] = new YYARec(298,59);yyac++; 
					yya[yyac] = new YYARec(299,60);yyac++; 
					yya[yyac] = new YYARec(300,61);yyac++; 
					yya[yyac] = new YYARec(301,62);yyac++; 
					yya[yyac] = new YYARec(302,63);yyac++; 
					yya[yyac] = new YYARec(303,235);yyac++; 
					yya[yyac] = new YYARec(304,38);yyac++; 
					yya[yyac] = new YYARec(305,39);yyac++; 
					yya[yyac] = new YYARec(306,40);yyac++; 
					yya[yyac] = new YYARec(307,146);yyac++; 
					yya[yyac] = new YYARec(308,64);yyac++; 
					yya[yyac] = new YYARec(309,42);yyac++; 
					yya[yyac] = new YYARec(310,65);yyac++; 
					yya[yyac] = new YYARec(311,66);yyac++; 
					yya[yyac] = new YYARec(312,67);yyac++; 
					yya[yyac] = new YYARec(313,236);yyac++; 
					yya[yyac] = new YYARec(314,237);yyac++; 
					yya[yyac] = new YYARec(315,48);yyac++; 
					yya[yyac] = new YYARec(316,69);yyac++; 
					yya[yyac] = new YYARec(317,50);yyac++; 
					yya[yyac] = new YYARec(318,70);yyac++; 
					yya[yyac] = new YYARec(319,52);yyac++; 
					yya[yyac] = new YYARec(320,53);yyac++; 
					yya[yyac] = new YYARec(321,238);yyac++; 
					yya[yyac] = new YYARec(322,239);yyac++; 
					yya[yyac] = new YYARec(323,54);yyac++; 
					yya[yyac] = new YYARec(273,-149 );yyac++; 
					yya[yyac] = new YYARec(258,-203 );yyac++; 
					yya[yyac] = new YYARec(265,-203 );yyac++; 
					yya[yyac] = new YYARec(268,-203 );yyac++; 
					yya[yyac] = new YYARec(269,-203 );yyac++; 
					yya[yyac] = new YYARec(270,-203 );yyac++; 
					yya[yyac] = new YYARec(271,-203 );yyac++; 
					yya[yyac] = new YYARec(272,-203 );yyac++; 
					yya[yyac] = new YYARec(274,-203 );yyac++; 
					yya[yyac] = new YYARec(275,-203 );yyac++; 
					yya[yyac] = new YYARec(276,-203 );yyac++; 
					yya[yyac] = new YYARec(277,-203 );yyac++; 
					yya[yyac] = new YYARec(278,-203 );yyac++; 
					yya[yyac] = new YYARec(279,-203 );yyac++; 
					yya[yyac] = new YYARec(280,-203 );yyac++; 
					yya[yyac] = new YYARec(281,-203 );yyac++; 
					yya[yyac] = new YYARec(282,-203 );yyac++; 
					yya[yyac] = new YYARec(283,-203 );yyac++; 
					yya[yyac] = new YYARec(284,-203 );yyac++; 
					yya[yyac] = new YYARec(285,-203 );yyac++; 
					yya[yyac] = new YYARec(288,-203 );yyac++; 
					yya[yyac] = new YYARec(289,-203 );yyac++; 
					yya[yyac] = new YYARec(290,-203 );yyac++; 
					yya[yyac] = new YYARec(291,-203 );yyac++; 
					yya[yyac] = new YYARec(292,-203 );yyac++; 
					yya[yyac] = new YYARec(296,-203 );yyac++; 
					yya[yyac] = new YYARec(321,-203 );yyac++; 
					yya[yyac] = new YYARec(273,-147 );yyac++; 
					yya[yyac] = new YYARec(258,-195 );yyac++; 
					yya[yyac] = new YYARec(265,-195 );yyac++; 
					yya[yyac] = new YYARec(268,-195 );yyac++; 
					yya[yyac] = new YYARec(269,-195 );yyac++; 
					yya[yyac] = new YYARec(270,-195 );yyac++; 
					yya[yyac] = new YYARec(271,-195 );yyac++; 
					yya[yyac] = new YYARec(272,-195 );yyac++; 
					yya[yyac] = new YYARec(274,-195 );yyac++; 
					yya[yyac] = new YYARec(275,-195 );yyac++; 
					yya[yyac] = new YYARec(276,-195 );yyac++; 
					yya[yyac] = new YYARec(277,-195 );yyac++; 
					yya[yyac] = new YYARec(278,-195 );yyac++; 
					yya[yyac] = new YYARec(279,-195 );yyac++; 
					yya[yyac] = new YYARec(280,-195 );yyac++; 
					yya[yyac] = new YYARec(281,-195 );yyac++; 
					yya[yyac] = new YYARec(282,-195 );yyac++; 
					yya[yyac] = new YYARec(283,-195 );yyac++; 
					yya[yyac] = new YYARec(284,-195 );yyac++; 
					yya[yyac] = new YYARec(285,-195 );yyac++; 
					yya[yyac] = new YYARec(288,-195 );yyac++; 
					yya[yyac] = new YYARec(289,-195 );yyac++; 
					yya[yyac] = new YYARec(290,-195 );yyac++; 
					yya[yyac] = new YYARec(291,-195 );yyac++; 
					yya[yyac] = new YYARec(292,-195 );yyac++; 
					yya[yyac] = new YYARec(296,-195 );yyac++; 
					yya[yyac] = new YYARec(321,-195 );yyac++; 
					yya[yyac] = new YYARec(273,-148 );yyac++; 
					yya[yyac] = new YYARec(258,-207 );yyac++; 
					yya[yyac] = new YYARec(265,-207 );yyac++; 
					yya[yyac] = new YYARec(268,-207 );yyac++; 
					yya[yyac] = new YYARec(269,-207 );yyac++; 
					yya[yyac] = new YYARec(270,-207 );yyac++; 
					yya[yyac] = new YYARec(271,-207 );yyac++; 
					yya[yyac] = new YYARec(272,-207 );yyac++; 
					yya[yyac] = new YYARec(274,-207 );yyac++; 
					yya[yyac] = new YYARec(275,-207 );yyac++; 
					yya[yyac] = new YYARec(276,-207 );yyac++; 
					yya[yyac] = new YYARec(277,-207 );yyac++; 
					yya[yyac] = new YYARec(278,-207 );yyac++; 
					yya[yyac] = new YYARec(279,-207 );yyac++; 
					yya[yyac] = new YYARec(280,-207 );yyac++; 
					yya[yyac] = new YYARec(281,-207 );yyac++; 
					yya[yyac] = new YYARec(282,-207 );yyac++; 
					yya[yyac] = new YYARec(283,-207 );yyac++; 
					yya[yyac] = new YYARec(284,-207 );yyac++; 
					yya[yyac] = new YYARec(285,-207 );yyac++; 
					yya[yyac] = new YYARec(288,-207 );yyac++; 
					yya[yyac] = new YYARec(289,-207 );yyac++; 
					yya[yyac] = new YYARec(290,-207 );yyac++; 
					yya[yyac] = new YYARec(291,-207 );yyac++; 
					yya[yyac] = new YYARec(292,-207 );yyac++; 
					yya[yyac] = new YYARec(296,-207 );yyac++; 
					yya[yyac] = new YYARec(321,-207 );yyac++; 
					yya[yyac] = new YYARec(321,296);yyac++; 
					yya[yyac] = new YYARec(258,-181 );yyac++; 
					yya[yyac] = new YYARec(265,-181 );yyac++; 
					yya[yyac] = new YYARec(268,-181 );yyac++; 
					yya[yyac] = new YYARec(269,-181 );yyac++; 
					yya[yyac] = new YYARec(270,-181 );yyac++; 
					yya[yyac] = new YYARec(271,-181 );yyac++; 
					yya[yyac] = new YYARec(272,-181 );yyac++; 
					yya[yyac] = new YYARec(274,-181 );yyac++; 
					yya[yyac] = new YYARec(275,-181 );yyac++; 
					yya[yyac] = new YYARec(276,-181 );yyac++; 
					yya[yyac] = new YYARec(277,-181 );yyac++; 
					yya[yyac] = new YYARec(278,-181 );yyac++; 
					yya[yyac] = new YYARec(279,-181 );yyac++; 
					yya[yyac] = new YYARec(280,-181 );yyac++; 
					yya[yyac] = new YYARec(281,-181 );yyac++; 
					yya[yyac] = new YYARec(282,-181 );yyac++; 
					yya[yyac] = new YYARec(283,-181 );yyac++; 
					yya[yyac] = new YYARec(284,-181 );yyac++; 
					yya[yyac] = new YYARec(285,-181 );yyac++; 
					yya[yyac] = new YYARec(257,160);yyac++; 
					yya[yyac] = new YYARec(258,161);yyac++; 
					yya[yyac] = new YYARec(259,162);yyac++; 
					yya[yyac] = new YYARec(262,26);yyac++; 
					yya[yyac] = new YYARec(263,27);yyac++; 
					yya[yyac] = new YYARec(265,163);yyac++; 
					yya[yyac] = new YYARec(287,164);yyac++; 
					yya[yyac] = new YYARec(293,165);yyac++; 
					yya[yyac] = new YYARec(294,166);yyac++; 
					yya[yyac] = new YYARec(295,167);yyac++; 
					yya[yyac] = new YYARec(297,31);yyac++; 
					yya[yyac] = new YYARec(298,59);yyac++; 
					yya[yyac] = new YYARec(299,60);yyac++; 
					yya[yyac] = new YYARec(300,61);yyac++; 
					yya[yyac] = new YYARec(301,62);yyac++; 
					yya[yyac] = new YYARec(302,63);yyac++; 
					yya[yyac] = new YYARec(303,37);yyac++; 
					yya[yyac] = new YYARec(304,38);yyac++; 
					yya[yyac] = new YYARec(305,39);yyac++; 
					yya[yyac] = new YYARec(306,168);yyac++; 
					yya[yyac] = new YYARec(308,64);yyac++; 
					yya[yyac] = new YYARec(309,42);yyac++; 
					yya[yyac] = new YYARec(310,65);yyac++; 
					yya[yyac] = new YYARec(311,66);yyac++; 
					yya[yyac] = new YYARec(312,67);yyac++; 
					yya[yyac] = new YYARec(313,169);yyac++; 
					yya[yyac] = new YYARec(314,68);yyac++; 
					yya[yyac] = new YYARec(315,48);yyac++; 
					yya[yyac] = new YYARec(316,69);yyac++; 
					yya[yyac] = new YYARec(317,170);yyac++; 
					yya[yyac] = new YYARec(318,70);yyac++; 
					yya[yyac] = new YYARec(319,171);yyac++; 
					yya[yyac] = new YYARec(320,53);yyac++; 
					yya[yyac] = new YYARec(323,172);yyac++; 
					yya[yyac] = new YYARec(266,-83 );yyac++; 
					yya[yyac] = new YYARec(265,298);yyac++; 
					yya[yyac] = new YYARec(321,294);yyac++; 
					yya[yyac] = new YYARec(258,-123 );yyac++; 
					yya[yyac] = new YYARec(265,-123 );yyac++; 
					yya[yyac] = new YYARec(268,-123 );yyac++; 
					yya[yyac] = new YYARec(269,-123 );yyac++; 
					yya[yyac] = new YYARec(270,-123 );yyac++; 
					yya[yyac] = new YYARec(271,-123 );yyac++; 
					yya[yyac] = new YYARec(272,-123 );yyac++; 
					yya[yyac] = new YYARec(274,-123 );yyac++; 
					yya[yyac] = new YYARec(275,-123 );yyac++; 
					yya[yyac] = new YYARec(276,-123 );yyac++; 
					yya[yyac] = new YYARec(277,-123 );yyac++; 
					yya[yyac] = new YYARec(278,-123 );yyac++; 
					yya[yyac] = new YYARec(279,-123 );yyac++; 
					yya[yyac] = new YYARec(280,-123 );yyac++; 
					yya[yyac] = new YYARec(281,-123 );yyac++; 
					yya[yyac] = new YYARec(282,-123 );yyac++; 
					yya[yyac] = new YYARec(283,-123 );yyac++; 
					yya[yyac] = new YYARec(284,-123 );yyac++; 
					yya[yyac] = new YYARec(285,-123 );yyac++; 
					yya[yyac] = new YYARec(265,299);yyac++; 
					yya[yyac] = new YYARec(257,160);yyac++; 
					yya[yyac] = new YYARec(258,161);yyac++; 
					yya[yyac] = new YYARec(259,162);yyac++; 
					yya[yyac] = new YYARec(262,26);yyac++; 
					yya[yyac] = new YYARec(263,27);yyac++; 
					yya[yyac] = new YYARec(265,163);yyac++; 
					yya[yyac] = new YYARec(287,164);yyac++; 
					yya[yyac] = new YYARec(293,165);yyac++; 
					yya[yyac] = new YYARec(294,166);yyac++; 
					yya[yyac] = new YYARec(295,167);yyac++; 
					yya[yyac] = new YYARec(297,31);yyac++; 
					yya[yyac] = new YYARec(298,59);yyac++; 
					yya[yyac] = new YYARec(299,60);yyac++; 
					yya[yyac] = new YYARec(300,61);yyac++; 
					yya[yyac] = new YYARec(301,62);yyac++; 
					yya[yyac] = new YYARec(302,63);yyac++; 
					yya[yyac] = new YYARec(303,37);yyac++; 
					yya[yyac] = new YYARec(304,38);yyac++; 
					yya[yyac] = new YYARec(305,39);yyac++; 
					yya[yyac] = new YYARec(306,168);yyac++; 
					yya[yyac] = new YYARec(308,64);yyac++; 
					yya[yyac] = new YYARec(309,42);yyac++; 
					yya[yyac] = new YYARec(310,65);yyac++; 
					yya[yyac] = new YYARec(311,66);yyac++; 
					yya[yyac] = new YYARec(312,67);yyac++; 
					yya[yyac] = new YYARec(313,169);yyac++; 
					yya[yyac] = new YYARec(314,68);yyac++; 
					yya[yyac] = new YYARec(315,48);yyac++; 
					yya[yyac] = new YYARec(316,69);yyac++; 
					yya[yyac] = new YYARec(317,170);yyac++; 
					yya[yyac] = new YYARec(318,70);yyac++; 
					yya[yyac] = new YYARec(319,171);yyac++; 
					yya[yyac] = new YYARec(320,53);yyac++; 
					yya[yyac] = new YYARec(323,172);yyac++; 
					yya[yyac] = new YYARec(260,-83 );yyac++; 
					yya[yyac] = new YYARec(261,-83 );yyac++; 
					yya[yyac] = new YYARec(266,-83 );yyac++; 
					yya[yyac] = new YYARec(281,85);yyac++; 
					yya[yyac] = new YYARec(282,86);yyac++; 
					yya[yyac] = new YYARec(286,87);yyac++; 
					yya[yyac] = new YYARec(297,31);yyac++; 
					yya[yyac] = new YYARec(298,59);yyac++; 
					yya[yyac] = new YYARec(299,60);yyac++; 
					yya[yyac] = new YYARec(300,61);yyac++; 
					yya[yyac] = new YYARec(301,62);yyac++; 
					yya[yyac] = new YYARec(302,63);yyac++; 
					yya[yyac] = new YYARec(303,37);yyac++; 
					yya[yyac] = new YYARec(304,38);yyac++; 
					yya[yyac] = new YYARec(305,39);yyac++; 
					yya[yyac] = new YYARec(306,40);yyac++; 
					yya[yyac] = new YYARec(308,64);yyac++; 
					yya[yyac] = new YYARec(309,42);yyac++; 
					yya[yyac] = new YYARec(310,65);yyac++; 
					yya[yyac] = new YYARec(311,66);yyac++; 
					yya[yyac] = new YYARec(312,67);yyac++; 
					yya[yyac] = new YYARec(313,46);yyac++; 
					yya[yyac] = new YYARec(314,68);yyac++; 
					yya[yyac] = new YYARec(315,48);yyac++; 
					yya[yyac] = new YYARec(316,69);yyac++; 
					yya[yyac] = new YYARec(317,50);yyac++; 
					yya[yyac] = new YYARec(318,70);yyac++; 
					yya[yyac] = new YYARec(319,52);yyac++; 
					yya[yyac] = new YYARec(320,53);yyac++; 
					yya[yyac] = new YYARec(321,88);yyac++; 
					yya[yyac] = new YYARec(322,89);yyac++; 
					yya[yyac] = new YYARec(323,54);yyac++; 
					yya[yyac] = new YYARec(324,90);yyac++; 
					yya[yyac] = new YYARec(325,91);yyac++; 
					yya[yyac] = new YYARec(258,-69 );yyac++; 
					yya[yyac] = new YYARec(258,302);yyac++; 
					yya[yyac] = new YYARec(258,303);yyac++; 
					yya[yyac] = new YYARec(258,304);yyac++; 
					yya[yyac] = new YYARec(258,305);yyac++; 
					yya[yyac] = new YYARec(260,306);yyac++; 
					yya[yyac] = new YYARec(261,307);yyac++; 
					yya[yyac] = new YYARec(257,160);yyac++; 
					yya[yyac] = new YYARec(258,161);yyac++; 
					yya[yyac] = new YYARec(259,162);yyac++; 
					yya[yyac] = new YYARec(262,26);yyac++; 
					yya[yyac] = new YYARec(263,27);yyac++; 
					yya[yyac] = new YYARec(265,163);yyac++; 
					yya[yyac] = new YYARec(287,164);yyac++; 
					yya[yyac] = new YYARec(293,165);yyac++; 
					yya[yyac] = new YYARec(294,166);yyac++; 
					yya[yyac] = new YYARec(295,167);yyac++; 
					yya[yyac] = new YYARec(297,31);yyac++; 
					yya[yyac] = new YYARec(298,59);yyac++; 
					yya[yyac] = new YYARec(299,60);yyac++; 
					yya[yyac] = new YYARec(300,61);yyac++; 
					yya[yyac] = new YYARec(301,62);yyac++; 
					yya[yyac] = new YYARec(302,63);yyac++; 
					yya[yyac] = new YYARec(303,37);yyac++; 
					yya[yyac] = new YYARec(304,38);yyac++; 
					yya[yyac] = new YYARec(305,39);yyac++; 
					yya[yyac] = new YYARec(306,168);yyac++; 
					yya[yyac] = new YYARec(308,64);yyac++; 
					yya[yyac] = new YYARec(309,42);yyac++; 
					yya[yyac] = new YYARec(310,65);yyac++; 
					yya[yyac] = new YYARec(311,66);yyac++; 
					yya[yyac] = new YYARec(312,67);yyac++; 
					yya[yyac] = new YYARec(313,169);yyac++; 
					yya[yyac] = new YYARec(314,68);yyac++; 
					yya[yyac] = new YYARec(315,48);yyac++; 
					yya[yyac] = new YYARec(316,69);yyac++; 
					yya[yyac] = new YYARec(317,170);yyac++; 
					yya[yyac] = new YYARec(318,70);yyac++; 
					yya[yyac] = new YYARec(319,171);yyac++; 
					yya[yyac] = new YYARec(320,53);yyac++; 
					yya[yyac] = new YYARec(323,172);yyac++; 
					yya[yyac] = new YYARec(260,-83 );yyac++; 
					yya[yyac] = new YYARec(261,-83 );yyac++; 
					yya[yyac] = new YYARec(266,-83 );yyac++; 
					yya[yyac] = new YYARec(273,234);yyac++; 
					yya[yyac] = new YYARec(281,85);yyac++; 
					yya[yyac] = new YYARec(282,86);yyac++; 
					yya[yyac] = new YYARec(286,87);yyac++; 
					yya[yyac] = new YYARec(297,31);yyac++; 
					yya[yyac] = new YYARec(298,59);yyac++; 
					yya[yyac] = new YYARec(299,60);yyac++; 
					yya[yyac] = new YYARec(300,61);yyac++; 
					yya[yyac] = new YYARec(301,62);yyac++; 
					yya[yyac] = new YYARec(302,63);yyac++; 
					yya[yyac] = new YYARec(303,235);yyac++; 
					yya[yyac] = new YYARec(304,38);yyac++; 
					yya[yyac] = new YYARec(305,39);yyac++; 
					yya[yyac] = new YYARec(306,40);yyac++; 
					yya[yyac] = new YYARec(307,146);yyac++; 
					yya[yyac] = new YYARec(308,64);yyac++; 
					yya[yyac] = new YYARec(309,42);yyac++; 
					yya[yyac] = new YYARec(310,65);yyac++; 
					yya[yyac] = new YYARec(311,66);yyac++; 
					yya[yyac] = new YYARec(312,67);yyac++; 
					yya[yyac] = new YYARec(313,236);yyac++; 
					yya[yyac] = new YYARec(314,237);yyac++; 
					yya[yyac] = new YYARec(315,48);yyac++; 
					yya[yyac] = new YYARec(316,69);yyac++; 
					yya[yyac] = new YYARec(317,50);yyac++; 
					yya[yyac] = new YYARec(318,70);yyac++; 
					yya[yyac] = new YYARec(319,52);yyac++; 
					yya[yyac] = new YYARec(320,53);yyac++; 
					yya[yyac] = new YYARec(321,238);yyac++; 
					yya[yyac] = new YYARec(322,239);yyac++; 
					yya[yyac] = new YYARec(323,54);yyac++; 
					yya[yyac] = new YYARec(273,234);yyac++; 
					yya[yyac] = new YYARec(281,85);yyac++; 
					yya[yyac] = new YYARec(282,86);yyac++; 
					yya[yyac] = new YYARec(286,87);yyac++; 
					yya[yyac] = new YYARec(297,31);yyac++; 
					yya[yyac] = new YYARec(298,59);yyac++; 
					yya[yyac] = new YYARec(299,60);yyac++; 
					yya[yyac] = new YYARec(300,61);yyac++; 
					yya[yyac] = new YYARec(301,62);yyac++; 
					yya[yyac] = new YYARec(302,63);yyac++; 
					yya[yyac] = new YYARec(303,235);yyac++; 
					yya[yyac] = new YYARec(304,38);yyac++; 
					yya[yyac] = new YYARec(305,39);yyac++; 
					yya[yyac] = new YYARec(306,40);yyac++; 
					yya[yyac] = new YYARec(307,146);yyac++; 
					yya[yyac] = new YYARec(308,64);yyac++; 
					yya[yyac] = new YYARec(309,42);yyac++; 
					yya[yyac] = new YYARec(310,65);yyac++; 
					yya[yyac] = new YYARec(311,66);yyac++; 
					yya[yyac] = new YYARec(312,67);yyac++; 
					yya[yyac] = new YYARec(313,236);yyac++; 
					yya[yyac] = new YYARec(314,237);yyac++; 
					yya[yyac] = new YYARec(315,48);yyac++; 
					yya[yyac] = new YYARec(316,69);yyac++; 
					yya[yyac] = new YYARec(317,50);yyac++; 
					yya[yyac] = new YYARec(318,70);yyac++; 
					yya[yyac] = new YYARec(319,52);yyac++; 
					yya[yyac] = new YYARec(320,53);yyac++; 
					yya[yyac] = new YYARec(321,238);yyac++; 
					yya[yyac] = new YYARec(322,239);yyac++; 
					yya[yyac] = new YYARec(323,54);yyac++; 
					yya[yyac] = new YYARec(273,234);yyac++; 
					yya[yyac] = new YYARec(281,85);yyac++; 
					yya[yyac] = new YYARec(282,86);yyac++; 
					yya[yyac] = new YYARec(286,87);yyac++; 
					yya[yyac] = new YYARec(297,31);yyac++; 
					yya[yyac] = new YYARec(298,59);yyac++; 
					yya[yyac] = new YYARec(299,60);yyac++; 
					yya[yyac] = new YYARec(300,61);yyac++; 
					yya[yyac] = new YYARec(301,62);yyac++; 
					yya[yyac] = new YYARec(302,63);yyac++; 
					yya[yyac] = new YYARec(303,235);yyac++; 
					yya[yyac] = new YYARec(304,38);yyac++; 
					yya[yyac] = new YYARec(305,39);yyac++; 
					yya[yyac] = new YYARec(306,40);yyac++; 
					yya[yyac] = new YYARec(307,146);yyac++; 
					yya[yyac] = new YYARec(308,64);yyac++; 
					yya[yyac] = new YYARec(309,42);yyac++; 
					yya[yyac] = new YYARec(310,65);yyac++; 
					yya[yyac] = new YYARec(311,66);yyac++; 
					yya[yyac] = new YYARec(312,67);yyac++; 
					yya[yyac] = new YYARec(313,236);yyac++; 
					yya[yyac] = new YYARec(314,237);yyac++; 
					yya[yyac] = new YYARec(315,48);yyac++; 
					yya[yyac] = new YYARec(316,69);yyac++; 
					yya[yyac] = new YYARec(317,50);yyac++; 
					yya[yyac] = new YYARec(318,70);yyac++; 
					yya[yyac] = new YYARec(319,52);yyac++; 
					yya[yyac] = new YYARec(320,53);yyac++; 
					yya[yyac] = new YYARec(321,238);yyac++; 
					yya[yyac] = new YYARec(322,239);yyac++; 
					yya[yyac] = new YYARec(323,54);yyac++; 
					yya[yyac] = new YYARec(273,234);yyac++; 
					yya[yyac] = new YYARec(281,85);yyac++; 
					yya[yyac] = new YYARec(282,86);yyac++; 
					yya[yyac] = new YYARec(286,87);yyac++; 
					yya[yyac] = new YYARec(297,31);yyac++; 
					yya[yyac] = new YYARec(298,59);yyac++; 
					yya[yyac] = new YYARec(299,60);yyac++; 
					yya[yyac] = new YYARec(300,61);yyac++; 
					yya[yyac] = new YYARec(301,62);yyac++; 
					yya[yyac] = new YYARec(302,63);yyac++; 
					yya[yyac] = new YYARec(303,235);yyac++; 
					yya[yyac] = new YYARec(304,38);yyac++; 
					yya[yyac] = new YYARec(305,39);yyac++; 
					yya[yyac] = new YYARec(306,40);yyac++; 
					yya[yyac] = new YYARec(307,146);yyac++; 
					yya[yyac] = new YYARec(308,64);yyac++; 
					yya[yyac] = new YYARec(309,42);yyac++; 
					yya[yyac] = new YYARec(310,65);yyac++; 
					yya[yyac] = new YYARec(311,66);yyac++; 
					yya[yyac] = new YYARec(312,67);yyac++; 
					yya[yyac] = new YYARec(313,236);yyac++; 
					yya[yyac] = new YYARec(314,237);yyac++; 
					yya[yyac] = new YYARec(315,48);yyac++; 
					yya[yyac] = new YYARec(316,69);yyac++; 
					yya[yyac] = new YYARec(317,50);yyac++; 
					yya[yyac] = new YYARec(318,70);yyac++; 
					yya[yyac] = new YYARec(319,52);yyac++; 
					yya[yyac] = new YYARec(320,53);yyac++; 
					yya[yyac] = new YYARec(321,238);yyac++; 
					yya[yyac] = new YYARec(322,239);yyac++; 
					yya[yyac] = new YYARec(323,54);yyac++; 
					yya[yyac] = new YYARec(273,234);yyac++; 
					yya[yyac] = new YYARec(281,85);yyac++; 
					yya[yyac] = new YYARec(282,86);yyac++; 
					yya[yyac] = new YYARec(286,87);yyac++; 
					yya[yyac] = new YYARec(297,31);yyac++; 
					yya[yyac] = new YYARec(298,59);yyac++; 
					yya[yyac] = new YYARec(299,60);yyac++; 
					yya[yyac] = new YYARec(300,61);yyac++; 
					yya[yyac] = new YYARec(301,62);yyac++; 
					yya[yyac] = new YYARec(302,63);yyac++; 
					yya[yyac] = new YYARec(303,235);yyac++; 
					yya[yyac] = new YYARec(304,38);yyac++; 
					yya[yyac] = new YYARec(305,39);yyac++; 
					yya[yyac] = new YYARec(306,40);yyac++; 
					yya[yyac] = new YYARec(307,146);yyac++; 
					yya[yyac] = new YYARec(308,64);yyac++; 
					yya[yyac] = new YYARec(309,42);yyac++; 
					yya[yyac] = new YYARec(310,65);yyac++; 
					yya[yyac] = new YYARec(311,66);yyac++; 
					yya[yyac] = new YYARec(312,67);yyac++; 
					yya[yyac] = new YYARec(313,236);yyac++; 
					yya[yyac] = new YYARec(314,237);yyac++; 
					yya[yyac] = new YYARec(315,48);yyac++; 
					yya[yyac] = new YYARec(316,69);yyac++; 
					yya[yyac] = new YYARec(317,50);yyac++; 
					yya[yyac] = new YYARec(318,70);yyac++; 
					yya[yyac] = new YYARec(319,52);yyac++; 
					yya[yyac] = new YYARec(320,53);yyac++; 
					yya[yyac] = new YYARec(321,238);yyac++; 
					yya[yyac] = new YYARec(322,239);yyac++; 
					yya[yyac] = new YYARec(323,54);yyac++; 
					yya[yyac] = new YYARec(273,234);yyac++; 
					yya[yyac] = new YYARec(281,85);yyac++; 
					yya[yyac] = new YYARec(282,86);yyac++; 
					yya[yyac] = new YYARec(286,87);yyac++; 
					yya[yyac] = new YYARec(297,31);yyac++; 
					yya[yyac] = new YYARec(298,59);yyac++; 
					yya[yyac] = new YYARec(299,60);yyac++; 
					yya[yyac] = new YYARec(300,61);yyac++; 
					yya[yyac] = new YYARec(301,62);yyac++; 
					yya[yyac] = new YYARec(302,63);yyac++; 
					yya[yyac] = new YYARec(303,235);yyac++; 
					yya[yyac] = new YYARec(304,38);yyac++; 
					yya[yyac] = new YYARec(305,39);yyac++; 
					yya[yyac] = new YYARec(306,40);yyac++; 
					yya[yyac] = new YYARec(307,146);yyac++; 
					yya[yyac] = new YYARec(308,64);yyac++; 
					yya[yyac] = new YYARec(309,42);yyac++; 
					yya[yyac] = new YYARec(310,65);yyac++; 
					yya[yyac] = new YYARec(311,66);yyac++; 
					yya[yyac] = new YYARec(312,67);yyac++; 
					yya[yyac] = new YYARec(313,236);yyac++; 
					yya[yyac] = new YYARec(314,237);yyac++; 
					yya[yyac] = new YYARec(315,48);yyac++; 
					yya[yyac] = new YYARec(316,69);yyac++; 
					yya[yyac] = new YYARec(317,50);yyac++; 
					yya[yyac] = new YYARec(318,70);yyac++; 
					yya[yyac] = new YYARec(319,52);yyac++; 
					yya[yyac] = new YYARec(320,53);yyac++; 
					yya[yyac] = new YYARec(321,238);yyac++; 
					yya[yyac] = new YYARec(322,239);yyac++; 
					yya[yyac] = new YYARec(323,54);yyac++; 
					yya[yyac] = new YYARec(273,234);yyac++; 
					yya[yyac] = new YYARec(281,85);yyac++; 
					yya[yyac] = new YYARec(282,86);yyac++; 
					yya[yyac] = new YYARec(286,87);yyac++; 
					yya[yyac] = new YYARec(297,31);yyac++; 
					yya[yyac] = new YYARec(298,59);yyac++; 
					yya[yyac] = new YYARec(299,60);yyac++; 
					yya[yyac] = new YYARec(300,61);yyac++; 
					yya[yyac] = new YYARec(301,62);yyac++; 
					yya[yyac] = new YYARec(302,63);yyac++; 
					yya[yyac] = new YYARec(303,235);yyac++; 
					yya[yyac] = new YYARec(304,38);yyac++; 
					yya[yyac] = new YYARec(305,39);yyac++; 
					yya[yyac] = new YYARec(306,40);yyac++; 
					yya[yyac] = new YYARec(307,146);yyac++; 
					yya[yyac] = new YYARec(308,64);yyac++; 
					yya[yyac] = new YYARec(309,42);yyac++; 
					yya[yyac] = new YYARec(310,65);yyac++; 
					yya[yyac] = new YYARec(311,66);yyac++; 
					yya[yyac] = new YYARec(312,67);yyac++; 
					yya[yyac] = new YYARec(313,236);yyac++; 
					yya[yyac] = new YYARec(314,237);yyac++; 
					yya[yyac] = new YYARec(315,48);yyac++; 
					yya[yyac] = new YYARec(316,69);yyac++; 
					yya[yyac] = new YYARec(317,50);yyac++; 
					yya[yyac] = new YYARec(318,70);yyac++; 
					yya[yyac] = new YYARec(319,52);yyac++; 
					yya[yyac] = new YYARec(320,53);yyac++; 
					yya[yyac] = new YYARec(321,238);yyac++; 
					yya[yyac] = new YYARec(322,239);yyac++; 
					yya[yyac] = new YYARec(323,54);yyac++; 
					yya[yyac] = new YYARec(273,234);yyac++; 
					yya[yyac] = new YYARec(281,85);yyac++; 
					yya[yyac] = new YYARec(282,86);yyac++; 
					yya[yyac] = new YYARec(286,87);yyac++; 
					yya[yyac] = new YYARec(297,31);yyac++; 
					yya[yyac] = new YYARec(298,59);yyac++; 
					yya[yyac] = new YYARec(299,60);yyac++; 
					yya[yyac] = new YYARec(300,61);yyac++; 
					yya[yyac] = new YYARec(301,62);yyac++; 
					yya[yyac] = new YYARec(302,63);yyac++; 
					yya[yyac] = new YYARec(303,235);yyac++; 
					yya[yyac] = new YYARec(304,38);yyac++; 
					yya[yyac] = new YYARec(305,39);yyac++; 
					yya[yyac] = new YYARec(306,40);yyac++; 
					yya[yyac] = new YYARec(307,146);yyac++; 
					yya[yyac] = new YYARec(308,64);yyac++; 
					yya[yyac] = new YYARec(309,42);yyac++; 
					yya[yyac] = new YYARec(310,65);yyac++; 
					yya[yyac] = new YYARec(311,66);yyac++; 
					yya[yyac] = new YYARec(312,67);yyac++; 
					yya[yyac] = new YYARec(313,236);yyac++; 
					yya[yyac] = new YYARec(314,237);yyac++; 
					yya[yyac] = new YYARec(315,48);yyac++; 
					yya[yyac] = new YYARec(316,69);yyac++; 
					yya[yyac] = new YYARec(317,50);yyac++; 
					yya[yyac] = new YYARec(318,70);yyac++; 
					yya[yyac] = new YYARec(319,52);yyac++; 
					yya[yyac] = new YYARec(320,53);yyac++; 
					yya[yyac] = new YYARec(321,238);yyac++; 
					yya[yyac] = new YYARec(322,239);yyac++; 
					yya[yyac] = new YYARec(323,54);yyac++; 
					yya[yyac] = new YYARec(273,234);yyac++; 
					yya[yyac] = new YYARec(281,85);yyac++; 
					yya[yyac] = new YYARec(282,86);yyac++; 
					yya[yyac] = new YYARec(286,87);yyac++; 
					yya[yyac] = new YYARec(297,31);yyac++; 
					yya[yyac] = new YYARec(298,59);yyac++; 
					yya[yyac] = new YYARec(299,60);yyac++; 
					yya[yyac] = new YYARec(300,61);yyac++; 
					yya[yyac] = new YYARec(301,62);yyac++; 
					yya[yyac] = new YYARec(302,63);yyac++; 
					yya[yyac] = new YYARec(303,235);yyac++; 
					yya[yyac] = new YYARec(304,38);yyac++; 
					yya[yyac] = new YYARec(305,39);yyac++; 
					yya[yyac] = new YYARec(306,40);yyac++; 
					yya[yyac] = new YYARec(307,146);yyac++; 
					yya[yyac] = new YYARec(308,64);yyac++; 
					yya[yyac] = new YYARec(309,42);yyac++; 
					yya[yyac] = new YYARec(310,65);yyac++; 
					yya[yyac] = new YYARec(311,66);yyac++; 
					yya[yyac] = new YYARec(312,67);yyac++; 
					yya[yyac] = new YYARec(313,236);yyac++; 
					yya[yyac] = new YYARec(314,237);yyac++; 
					yya[yyac] = new YYARec(315,48);yyac++; 
					yya[yyac] = new YYARec(316,69);yyac++; 
					yya[yyac] = new YYARec(317,50);yyac++; 
					yya[yyac] = new YYARec(318,70);yyac++; 
					yya[yyac] = new YYARec(319,52);yyac++; 
					yya[yyac] = new YYARec(320,53);yyac++; 
					yya[yyac] = new YYARec(321,238);yyac++; 
					yya[yyac] = new YYARec(322,239);yyac++; 
					yya[yyac] = new YYARec(323,54);yyac++; 
					yya[yyac] = new YYARec(273,234);yyac++; 
					yya[yyac] = new YYARec(281,85);yyac++; 
					yya[yyac] = new YYARec(282,86);yyac++; 
					yya[yyac] = new YYARec(286,87);yyac++; 
					yya[yyac] = new YYARec(297,31);yyac++; 
					yya[yyac] = new YYARec(298,59);yyac++; 
					yya[yyac] = new YYARec(299,60);yyac++; 
					yya[yyac] = new YYARec(300,61);yyac++; 
					yya[yyac] = new YYARec(301,62);yyac++; 
					yya[yyac] = new YYARec(302,63);yyac++; 
					yya[yyac] = new YYARec(303,235);yyac++; 
					yya[yyac] = new YYARec(304,38);yyac++; 
					yya[yyac] = new YYARec(305,39);yyac++; 
					yya[yyac] = new YYARec(306,40);yyac++; 
					yya[yyac] = new YYARec(307,146);yyac++; 
					yya[yyac] = new YYARec(308,64);yyac++; 
					yya[yyac] = new YYARec(309,42);yyac++; 
					yya[yyac] = new YYARec(310,65);yyac++; 
					yya[yyac] = new YYARec(311,66);yyac++; 
					yya[yyac] = new YYARec(312,67);yyac++; 
					yya[yyac] = new YYARec(313,236);yyac++; 
					yya[yyac] = new YYARec(314,237);yyac++; 
					yya[yyac] = new YYARec(315,48);yyac++; 
					yya[yyac] = new YYARec(316,69);yyac++; 
					yya[yyac] = new YYARec(317,50);yyac++; 
					yya[yyac] = new YYARec(318,70);yyac++; 
					yya[yyac] = new YYARec(319,52);yyac++; 
					yya[yyac] = new YYARec(320,53);yyac++; 
					yya[yyac] = new YYARec(321,238);yyac++; 
					yya[yyac] = new YYARec(322,239);yyac++; 
					yya[yyac] = new YYARec(323,54);yyac++; 
					yya[yyac] = new YYARec(273,234);yyac++; 
					yya[yyac] = new YYARec(281,85);yyac++; 
					yya[yyac] = new YYARec(282,86);yyac++; 
					yya[yyac] = new YYARec(286,87);yyac++; 
					yya[yyac] = new YYARec(297,31);yyac++; 
					yya[yyac] = new YYARec(298,59);yyac++; 
					yya[yyac] = new YYARec(299,60);yyac++; 
					yya[yyac] = new YYARec(300,61);yyac++; 
					yya[yyac] = new YYARec(301,62);yyac++; 
					yya[yyac] = new YYARec(302,63);yyac++; 
					yya[yyac] = new YYARec(303,235);yyac++; 
					yya[yyac] = new YYARec(304,38);yyac++; 
					yya[yyac] = new YYARec(305,39);yyac++; 
					yya[yyac] = new YYARec(306,40);yyac++; 
					yya[yyac] = new YYARec(307,146);yyac++; 
					yya[yyac] = new YYARec(308,64);yyac++; 
					yya[yyac] = new YYARec(309,42);yyac++; 
					yya[yyac] = new YYARec(310,65);yyac++; 
					yya[yyac] = new YYARec(311,66);yyac++; 
					yya[yyac] = new YYARec(312,67);yyac++; 
					yya[yyac] = new YYARec(313,236);yyac++; 
					yya[yyac] = new YYARec(314,237);yyac++; 
					yya[yyac] = new YYARec(315,48);yyac++; 
					yya[yyac] = new YYARec(316,69);yyac++; 
					yya[yyac] = new YYARec(317,50);yyac++; 
					yya[yyac] = new YYARec(318,70);yyac++; 
					yya[yyac] = new YYARec(319,52);yyac++; 
					yya[yyac] = new YYARec(320,53);yyac++; 
					yya[yyac] = new YYARec(321,238);yyac++; 
					yya[yyac] = new YYARec(322,239);yyac++; 
					yya[yyac] = new YYARec(323,54);yyac++; 
					yya[yyac] = new YYARec(274,320);yyac++; 
					yya[yyac] = new YYARec(266,321);yyac++; 
					yya[yyac] = new YYARec(257,160);yyac++; 
					yya[yyac] = new YYARec(258,161);yyac++; 
					yya[yyac] = new YYARec(259,162);yyac++; 
					yya[yyac] = new YYARec(262,26);yyac++; 
					yya[yyac] = new YYARec(263,27);yyac++; 
					yya[yyac] = new YYARec(265,163);yyac++; 
					yya[yyac] = new YYARec(287,164);yyac++; 
					yya[yyac] = new YYARec(293,165);yyac++; 
					yya[yyac] = new YYARec(294,166);yyac++; 
					yya[yyac] = new YYARec(295,167);yyac++; 
					yya[yyac] = new YYARec(297,31);yyac++; 
					yya[yyac] = new YYARec(298,59);yyac++; 
					yya[yyac] = new YYARec(299,60);yyac++; 
					yya[yyac] = new YYARec(300,61);yyac++; 
					yya[yyac] = new YYARec(301,62);yyac++; 
					yya[yyac] = new YYARec(302,63);yyac++; 
					yya[yyac] = new YYARec(303,37);yyac++; 
					yya[yyac] = new YYARec(304,38);yyac++; 
					yya[yyac] = new YYARec(305,39);yyac++; 
					yya[yyac] = new YYARec(306,168);yyac++; 
					yya[yyac] = new YYARec(308,64);yyac++; 
					yya[yyac] = new YYARec(309,42);yyac++; 
					yya[yyac] = new YYARec(310,65);yyac++; 
					yya[yyac] = new YYARec(311,66);yyac++; 
					yya[yyac] = new YYARec(312,67);yyac++; 
					yya[yyac] = new YYARec(313,169);yyac++; 
					yya[yyac] = new YYARec(314,68);yyac++; 
					yya[yyac] = new YYARec(315,48);yyac++; 
					yya[yyac] = new YYARec(316,69);yyac++; 
					yya[yyac] = new YYARec(317,170);yyac++; 
					yya[yyac] = new YYARec(318,70);yyac++; 
					yya[yyac] = new YYARec(319,171);yyac++; 
					yya[yyac] = new YYARec(320,53);yyac++; 
					yya[yyac] = new YYARec(323,172);yyac++; 
					yya[yyac] = new YYARec(266,-83 );yyac++; 
					yya[yyac] = new YYARec(257,160);yyac++; 
					yya[yyac] = new YYARec(258,161);yyac++; 
					yya[yyac] = new YYARec(259,162);yyac++; 
					yya[yyac] = new YYARec(262,26);yyac++; 
					yya[yyac] = new YYARec(263,27);yyac++; 
					yya[yyac] = new YYARec(265,163);yyac++; 
					yya[yyac] = new YYARec(287,164);yyac++; 
					yya[yyac] = new YYARec(293,165);yyac++; 
					yya[yyac] = new YYARec(294,166);yyac++; 
					yya[yyac] = new YYARec(295,167);yyac++; 
					yya[yyac] = new YYARec(297,31);yyac++; 
					yya[yyac] = new YYARec(298,59);yyac++; 
					yya[yyac] = new YYARec(299,60);yyac++; 
					yya[yyac] = new YYARec(300,61);yyac++; 
					yya[yyac] = new YYARec(301,62);yyac++; 
					yya[yyac] = new YYARec(302,63);yyac++; 
					yya[yyac] = new YYARec(303,37);yyac++; 
					yya[yyac] = new YYARec(304,38);yyac++; 
					yya[yyac] = new YYARec(305,39);yyac++; 
					yya[yyac] = new YYARec(306,168);yyac++; 
					yya[yyac] = new YYARec(308,64);yyac++; 
					yya[yyac] = new YYARec(309,42);yyac++; 
					yya[yyac] = new YYARec(310,65);yyac++; 
					yya[yyac] = new YYARec(311,66);yyac++; 
					yya[yyac] = new YYARec(312,67);yyac++; 
					yya[yyac] = new YYARec(313,169);yyac++; 
					yya[yyac] = new YYARec(314,68);yyac++; 
					yya[yyac] = new YYARec(315,48);yyac++; 
					yya[yyac] = new YYARec(316,69);yyac++; 
					yya[yyac] = new YYARec(317,170);yyac++; 
					yya[yyac] = new YYARec(318,70);yyac++; 
					yya[yyac] = new YYARec(319,171);yyac++; 
					yya[yyac] = new YYARec(320,53);yyac++; 
					yya[yyac] = new YYARec(323,172);yyac++; 
					yya[yyac] = new YYARec(266,-83 );yyac++; 
					yya[yyac] = new YYARec(257,180);yyac++; 
					yya[yyac] = new YYARec(258,181);yyac++; 
					yya[yyac] = new YYARec(259,182);yyac++; 
					yya[yyac] = new YYARec(300,110);yyac++; 
					yya[yyac] = new YYARec(301,111);yyac++; 
					yya[yyac] = new YYARec(305,113);yyac++; 
					yya[yyac] = new YYARec(310,114);yyac++; 
					yya[yyac] = new YYARec(311,115);yyac++; 
					yya[yyac] = new YYARec(314,117);yyac++; 
					yya[yyac] = new YYARec(316,119);yyac++; 
					yya[yyac] = new YYARec(318,121);yyac++; 
					yya[yyac] = new YYARec(319,122);yyac++; 
					yya[yyac] = new YYARec(323,54);yyac++; 
					yya[yyac] = new YYARec(260,-66 );yyac++; 
					yya[yyac] = new YYARec(261,-66 );yyac++; 
					yya[yyac] = new YYARec(257,180);yyac++; 
					yya[yyac] = new YYARec(258,181);yyac++; 
					yya[yyac] = new YYARec(259,182);yyac++; 
					yya[yyac] = new YYARec(300,110);yyac++; 
					yya[yyac] = new YYARec(301,111);yyac++; 
					yya[yyac] = new YYARec(305,113);yyac++; 
					yya[yyac] = new YYARec(310,114);yyac++; 
					yya[yyac] = new YYARec(311,115);yyac++; 
					yya[yyac] = new YYARec(314,117);yyac++; 
					yya[yyac] = new YYARec(316,119);yyac++; 
					yya[yyac] = new YYARec(318,121);yyac++; 
					yya[yyac] = new YYARec(319,122);yyac++; 
					yya[yyac] = new YYARec(323,54);yyac++; 
					yya[yyac] = new YYARec(260,-66 );yyac++; 
					yya[yyac] = new YYARec(261,-66 );yyac++; 
					yya[yyac] = new YYARec(257,23);yyac++; 
					yya[yyac] = new YYARec(258,24);yyac++; 
					yya[yyac] = new YYARec(259,25);yyac++; 
					yya[yyac] = new YYARec(262,26);yyac++; 
					yya[yyac] = new YYARec(263,27);yyac++; 
					yya[yyac] = new YYARec(264,28);yyac++; 
					yya[yyac] = new YYARec(265,29);yyac++; 
					yya[yyac] = new YYARec(266,30);yyac++; 
					yya[yyac] = new YYARec(297,31);yyac++; 
					yya[yyac] = new YYARec(298,32);yyac++; 
					yya[yyac] = new YYARec(299,33);yyac++; 
					yya[yyac] = new YYARec(300,34);yyac++; 
					yya[yyac] = new YYARec(301,35);yyac++; 
					yya[yyac] = new YYARec(302,36);yyac++; 
					yya[yyac] = new YYARec(303,37);yyac++; 
					yya[yyac] = new YYARec(304,38);yyac++; 
					yya[yyac] = new YYARec(305,39);yyac++; 
					yya[yyac] = new YYARec(306,40);yyac++; 
					yya[yyac] = new YYARec(308,41);yyac++; 
					yya[yyac] = new YYARec(309,42);yyac++; 
					yya[yyac] = new YYARec(310,43);yyac++; 
					yya[yyac] = new YYARec(311,44);yyac++; 
					yya[yyac] = new YYARec(312,45);yyac++; 
					yya[yyac] = new YYARec(313,46);yyac++; 
					yya[yyac] = new YYARec(314,47);yyac++; 
					yya[yyac] = new YYARec(315,48);yyac++; 
					yya[yyac] = new YYARec(316,49);yyac++; 
					yya[yyac] = new YYARec(317,50);yyac++; 
					yya[yyac] = new YYARec(318,51);yyac++; 
					yya[yyac] = new YYARec(319,52);yyac++; 
					yya[yyac] = new YYARec(320,53);yyac++; 
					yya[yyac] = new YYARec(323,54);yyac++; 
					yya[yyac] = new YYARec(261,-4 );yyac++; 
					yya[yyac] = new YYARec(257,160);yyac++; 
					yya[yyac] = new YYARec(258,161);yyac++; 
					yya[yyac] = new YYARec(259,162);yyac++; 
					yya[yyac] = new YYARec(262,26);yyac++; 
					yya[yyac] = new YYARec(263,27);yyac++; 
					yya[yyac] = new YYARec(265,163);yyac++; 
					yya[yyac] = new YYARec(287,164);yyac++; 
					yya[yyac] = new YYARec(293,165);yyac++; 
					yya[yyac] = new YYARec(294,166);yyac++; 
					yya[yyac] = new YYARec(295,167);yyac++; 
					yya[yyac] = new YYARec(297,31);yyac++; 
					yya[yyac] = new YYARec(298,59);yyac++; 
					yya[yyac] = new YYARec(299,60);yyac++; 
					yya[yyac] = new YYARec(300,61);yyac++; 
					yya[yyac] = new YYARec(301,62);yyac++; 
					yya[yyac] = new YYARec(302,63);yyac++; 
					yya[yyac] = new YYARec(303,37);yyac++; 
					yya[yyac] = new YYARec(304,38);yyac++; 
					yya[yyac] = new YYARec(305,39);yyac++; 
					yya[yyac] = new YYARec(306,168);yyac++; 
					yya[yyac] = new YYARec(308,64);yyac++; 
					yya[yyac] = new YYARec(309,42);yyac++; 
					yya[yyac] = new YYARec(310,65);yyac++; 
					yya[yyac] = new YYARec(311,66);yyac++; 
					yya[yyac] = new YYARec(312,67);yyac++; 
					yya[yyac] = new YYARec(313,169);yyac++; 
					yya[yyac] = new YYARec(314,68);yyac++; 
					yya[yyac] = new YYARec(315,48);yyac++; 
					yya[yyac] = new YYARec(316,69);yyac++; 
					yya[yyac] = new YYARec(317,170);yyac++; 
					yya[yyac] = new YYARec(318,70);yyac++; 
					yya[yyac] = new YYARec(319,171);yyac++; 
					yya[yyac] = new YYARec(320,53);yyac++; 
					yya[yyac] = new YYARec(323,172);yyac++; 
					yya[yyac] = new YYARec(261,-83 );yyac++; 
					yya[yyac] = new YYARec(274,329);yyac++; 
					yya[yyac] = new YYARec(283,274);yyac++; 
					yya[yyac] = new YYARec(284,275);yyac++; 
					yya[yyac] = new YYARec(285,276);yyac++; 
					yya[yyac] = new YYARec(258,-113 );yyac++; 
					yya[yyac] = new YYARec(265,-113 );yyac++; 
					yya[yyac] = new YYARec(268,-113 );yyac++; 
					yya[yyac] = new YYARec(269,-113 );yyac++; 
					yya[yyac] = new YYARec(270,-113 );yyac++; 
					yya[yyac] = new YYARec(271,-113 );yyac++; 
					yya[yyac] = new YYARec(272,-113 );yyac++; 
					yya[yyac] = new YYARec(274,-113 );yyac++; 
					yya[yyac] = new YYARec(275,-113 );yyac++; 
					yya[yyac] = new YYARec(276,-113 );yyac++; 
					yya[yyac] = new YYARec(277,-113 );yyac++; 
					yya[yyac] = new YYARec(278,-113 );yyac++; 
					yya[yyac] = new YYARec(279,-113 );yyac++; 
					yya[yyac] = new YYARec(280,-113 );yyac++; 
					yya[yyac] = new YYARec(281,-113 );yyac++; 
					yya[yyac] = new YYARec(282,-113 );yyac++; 
					yya[yyac] = new YYARec(281,278);yyac++; 
					yya[yyac] = new YYARec(282,279);yyac++; 
					yya[yyac] = new YYARec(258,-111 );yyac++; 
					yya[yyac] = new YYARec(265,-111 );yyac++; 
					yya[yyac] = new YYARec(268,-111 );yyac++; 
					yya[yyac] = new YYARec(269,-111 );yyac++; 
					yya[yyac] = new YYARec(270,-111 );yyac++; 
					yya[yyac] = new YYARec(271,-111 );yyac++; 
					yya[yyac] = new YYARec(272,-111 );yyac++; 
					yya[yyac] = new YYARec(274,-111 );yyac++; 
					yya[yyac] = new YYARec(275,-111 );yyac++; 
					yya[yyac] = new YYARec(276,-111 );yyac++; 
					yya[yyac] = new YYARec(277,-111 );yyac++; 
					yya[yyac] = new YYARec(278,-111 );yyac++; 
					yya[yyac] = new YYARec(279,-111 );yyac++; 
					yya[yyac] = new YYARec(280,-111 );yyac++; 
					yya[yyac] = new YYARec(277,281);yyac++; 
					yya[yyac] = new YYARec(278,282);yyac++; 
					yya[yyac] = new YYARec(279,283);yyac++; 
					yya[yyac] = new YYARec(280,284);yyac++; 
					yya[yyac] = new YYARec(258,-109 );yyac++; 
					yya[yyac] = new YYARec(265,-109 );yyac++; 
					yya[yyac] = new YYARec(268,-109 );yyac++; 
					yya[yyac] = new YYARec(269,-109 );yyac++; 
					yya[yyac] = new YYARec(270,-109 );yyac++; 
					yya[yyac] = new YYARec(271,-109 );yyac++; 
					yya[yyac] = new YYARec(272,-109 );yyac++; 
					yya[yyac] = new YYARec(274,-109 );yyac++; 
					yya[yyac] = new YYARec(275,-109 );yyac++; 
					yya[yyac] = new YYARec(276,-109 );yyac++; 
					yya[yyac] = new YYARec(275,286);yyac++; 
					yya[yyac] = new YYARec(276,287);yyac++; 
					yya[yyac] = new YYARec(258,-106 );yyac++; 
					yya[yyac] = new YYARec(265,-106 );yyac++; 
					yya[yyac] = new YYARec(268,-106 );yyac++; 
					yya[yyac] = new YYARec(269,-106 );yyac++; 
					yya[yyac] = new YYARec(270,-106 );yyac++; 
					yya[yyac] = new YYARec(271,-106 );yyac++; 
					yya[yyac] = new YYARec(272,-106 );yyac++; 
					yya[yyac] = new YYARec(274,-106 );yyac++; 
					yya[yyac] = new YYARec(272,288);yyac++; 
					yya[yyac] = new YYARec(258,-104 );yyac++; 
					yya[yyac] = new YYARec(265,-104 );yyac++; 
					yya[yyac] = new YYARec(268,-104 );yyac++; 
					yya[yyac] = new YYARec(269,-104 );yyac++; 
					yya[yyac] = new YYARec(270,-104 );yyac++; 
					yya[yyac] = new YYARec(271,-104 );yyac++; 
					yya[yyac] = new YYARec(274,-104 );yyac++; 
					yya[yyac] = new YYARec(271,289);yyac++; 
					yya[yyac] = new YYARec(258,-102 );yyac++; 
					yya[yyac] = new YYARec(265,-102 );yyac++; 
					yya[yyac] = new YYARec(268,-102 );yyac++; 
					yya[yyac] = new YYARec(269,-102 );yyac++; 
					yya[yyac] = new YYARec(270,-102 );yyac++; 
					yya[yyac] = new YYARec(274,-102 );yyac++; 
					yya[yyac] = new YYARec(270,290);yyac++; 
					yya[yyac] = new YYARec(258,-100 );yyac++; 
					yya[yyac] = new YYARec(265,-100 );yyac++; 
					yya[yyac] = new YYARec(268,-100 );yyac++; 
					yya[yyac] = new YYARec(269,-100 );yyac++; 
					yya[yyac] = new YYARec(274,-100 );yyac++; 
					yya[yyac] = new YYARec(269,291);yyac++; 
					yya[yyac] = new YYARec(258,-98 );yyac++; 
					yya[yyac] = new YYARec(265,-98 );yyac++; 
					yya[yyac] = new YYARec(268,-98 );yyac++; 
					yya[yyac] = new YYARec(274,-98 );yyac++; 
					yya[yyac] = new YYARec(266,330);yyac++; 
					yya[yyac] = new YYARec(266,331);yyac++; 
					yya[yyac] = new YYARec(260,332);yyac++; 
					yya[yyac] = new YYARec(261,333);yyac++; 
					yya[yyac] = new YYARec(258,334);yyac++; 
					yya[yyac] = new YYARec(258,335);yyac++; 
					yya[yyac] = new YYARec(261,336);yyac++; 
					yya[yyac] = new YYARec(261,337);yyac++; 
					yya[yyac] = new YYARec(258,338);yyac++; 
					yya[yyac] = new YYARec(257,180);yyac++; 
					yya[yyac] = new YYARec(258,181);yyac++; 
					yya[yyac] = new YYARec(259,182);yyac++; 
					yya[yyac] = new YYARec(300,110);yyac++; 
					yya[yyac] = new YYARec(301,111);yyac++; 
					yya[yyac] = new YYARec(305,113);yyac++; 
					yya[yyac] = new YYARec(310,114);yyac++; 
					yya[yyac] = new YYARec(311,115);yyac++; 
					yya[yyac] = new YYARec(314,117);yyac++; 
					yya[yyac] = new YYARec(316,119);yyac++; 
					yya[yyac] = new YYARec(318,121);yyac++; 
					yya[yyac] = new YYARec(319,122);yyac++; 
					yya[yyac] = new YYARec(323,54);yyac++; 
					yya[yyac] = new YYARec(261,-66 );yyac++; 
					yya[yyac] = new YYARec(261,340);yyac++;

					yyg[yygc] = new YYARec(-78,1);yygc++; 
					yyg[yygc] = new YYARec(-77,2);yygc++; 
					yyg[yygc] = new YYARec(-74,3);yygc++; 
					yyg[yygc] = new YYARec(-45,4);yygc++; 
					yyg[yygc] = new YYARec(-40,5);yygc++; 
					yyg[yygc] = new YYARec(-39,6);yygc++; 
					yyg[yygc] = new YYARec(-34,7);yygc++; 
					yyg[yygc] = new YYARec(-31,8);yygc++; 
					yyg[yygc] = new YYARec(-29,9);yygc++; 
					yyg[yygc] = new YYARec(-23,10);yygc++; 
					yyg[yygc] = new YYARec(-22,11);yygc++; 
					yyg[yygc] = new YYARec(-12,12);yygc++; 
					yyg[yygc] = new YYARec(-11,13);yygc++; 
					yyg[yygc] = new YYARec(-10,14);yygc++; 
					yyg[yygc] = new YYARec(-9,15);yygc++; 
					yyg[yygc] = new YYARec(-8,16);yygc++; 
					yyg[yygc] = new YYARec(-7,17);yygc++; 
					yyg[yygc] = new YYARec(-6,18);yygc++; 
					yyg[yygc] = new YYARec(-5,19);yygc++; 
					yyg[yygc] = new YYARec(-4,20);yygc++; 
					yyg[yygc] = new YYARec(-3,21);yygc++; 
					yyg[yygc] = new YYARec(-2,22);yygc++; 
					yyg[yygc] = new YYARec(-79,56);yygc++; 
					yyg[yygc] = new YYARec(-78,1);yygc++; 
					yyg[yygc] = new YYARec(-77,2);yygc++; 
					yyg[yygc] = new YYARec(-74,57);yygc++; 
					yyg[yygc] = new YYARec(-46,58);yygc++; 
					yyg[yygc] = new YYARec(-23,10);yygc++; 
					yyg[yygc] = new YYARec(-79,72);yygc++; 
					yyg[yygc] = new YYARec(-78,1);yygc++; 
					yyg[yygc] = new YYARec(-77,2);yygc++; 
					yyg[yygc] = new YYARec(-74,57);yygc++; 
					yyg[yygc] = new YYARec(-33,73);yygc++; 
					yyg[yygc] = new YYARec(-23,10);yygc++; 
					yyg[yygc] = new YYARec(-79,74);yygc++; 
					yyg[yygc] = new YYARec(-78,1);yygc++; 
					yyg[yygc] = new YYARec(-77,2);yygc++; 
					yyg[yygc] = new YYARec(-74,57);yygc++; 
					yyg[yygc] = new YYARec(-35,75);yygc++; 
					yyg[yygc] = new YYARec(-23,10);yygc++; 
					yyg[yygc] = new YYARec(-76,77);yygc++; 
					yyg[yygc] = new YYARec(-70,78);yygc++; 
					yyg[yygc] = new YYARec(-38,79);yygc++; 
					yyg[yygc] = new YYARec(-32,80);yygc++; 
					yyg[yygc] = new YYARec(-27,81);yygc++; 
					yyg[yygc] = new YYARec(-26,82);yygc++; 
					yyg[yygc] = new YYARec(-24,83);yygc++; 
					yyg[yygc] = new YYARec(-23,84);yygc++; 
					yyg[yygc] = new YYARec(-79,72);yygc++; 
					yyg[yygc] = new YYARec(-78,1);yygc++; 
					yyg[yygc] = new YYARec(-77,2);yygc++; 
					yyg[yygc] = new YYARec(-74,57);yygc++; 
					yyg[yygc] = new YYARec(-33,92);yygc++; 
					yyg[yygc] = new YYARec(-30,93);yygc++; 
					yyg[yygc] = new YYARec(-23,10);yygc++; 
					yyg[yygc] = new YYARec(-78,1);yygc++; 
					yyg[yygc] = new YYARec(-77,2);yygc++; 
					yyg[yygc] = new YYARec(-74,3);yygc++; 
					yyg[yygc] = new YYARec(-45,4);yygc++; 
					yyg[yygc] = new YYARec(-40,5);yygc++; 
					yyg[yygc] = new YYARec(-39,6);yygc++; 
					yyg[yygc] = new YYARec(-34,7);yygc++; 
					yyg[yygc] = new YYARec(-31,8);yygc++; 
					yyg[yygc] = new YYARec(-29,9);yygc++; 
					yyg[yygc] = new YYARec(-23,10);yygc++; 
					yyg[yygc] = new YYARec(-22,11);yygc++; 
					yyg[yygc] = new YYARec(-12,12);yygc++; 
					yyg[yygc] = new YYARec(-11,13);yygc++; 
					yyg[yygc] = new YYARec(-10,14);yygc++; 
					yyg[yygc] = new YYARec(-9,15);yygc++; 
					yyg[yygc] = new YYARec(-8,16);yygc++; 
					yyg[yygc] = new YYARec(-7,17);yygc++; 
					yyg[yygc] = new YYARec(-6,18);yygc++; 
					yyg[yygc] = new YYARec(-5,19);yygc++; 
					yyg[yygc] = new YYARec(-4,20);yygc++; 
					yyg[yygc] = new YYARec(-3,94);yygc++; 
					yyg[yygc] = new YYARec(-23,95);yygc++; 
					yyg[yygc] = new YYARec(-22,96);yygc++; 
					yyg[yygc] = new YYARec(-13,97);yygc++; 
					yyg[yygc] = new YYARec(-23,95);yygc++; 
					yyg[yygc] = new YYARec(-22,96);yygc++; 
					yyg[yygc] = new YYARec(-13,101);yygc++; 
					yyg[yygc] = new YYARec(-23,95);yygc++; 
					yyg[yygc] = new YYARec(-22,96);yygc++; 
					yyg[yygc] = new YYARec(-13,102);yygc++; 
					yyg[yygc] = new YYARec(-23,95);yygc++; 
					yyg[yygc] = new YYARec(-22,96);yygc++; 
					yyg[yygc] = new YYARec(-13,103);yygc++; 
					yyg[yygc] = new YYARec(-26,104);yygc++; 
					yyg[yygc] = new YYARec(-75,105);yygc++; 
					yyg[yygc] = new YYARec(-42,106);yygc++; 
					yyg[yygc] = new YYARec(-40,107);yygc++; 
					yyg[yygc] = new YYARec(-34,108);yygc++; 
					yyg[yygc] = new YYARec(-23,109);yygc++; 
					yyg[yygc] = new YYARec(-24,126);yygc++; 
					yyg[yygc] = new YYARec(-26,129);yygc++; 
					yyg[yygc] = new YYARec(-79,72);yygc++; 
					yyg[yygc] = new YYARec(-78,1);yygc++; 
					yyg[yygc] = new YYARec(-77,2);yygc++; 
					yyg[yygc] = new YYARec(-74,57);yygc++; 
					yyg[yygc] = new YYARec(-33,92);yygc++; 
					yyg[yygc] = new YYARec(-30,133);yygc++; 
					yyg[yygc] = new YYARec(-23,10);yygc++; 
					yyg[yygc] = new YYARec(-78,1);yygc++; 
					yyg[yygc] = new YYARec(-77,2);yygc++; 
					yyg[yygc] = new YYARec(-76,77);yygc++; 
					yyg[yygc] = new YYARec(-74,3);yygc++; 
					yyg[yygc] = new YYARec(-70,137);yygc++; 
					yyg[yygc] = new YYARec(-38,79);yygc++; 
					yyg[yygc] = new YYARec(-28,138);yygc++; 
					yyg[yygc] = new YYARec(-27,139);yygc++; 
					yyg[yygc] = new YYARec(-26,140);yygc++; 
					yyg[yygc] = new YYARec(-25,141);yygc++; 
					yyg[yygc] = new YYARec(-24,142);yygc++; 
					yyg[yygc] = new YYARec(-23,10);yygc++; 
					yyg[yygc] = new YYARec(-21,143);yygc++; 
					yyg[yygc] = new YYARec(-5,144);yygc++; 
					yyg[yygc] = new YYARec(-78,1);yygc++; 
					yyg[yygc] = new YYARec(-77,2);yygc++; 
					yyg[yygc] = new YYARec(-74,149);yygc++; 
					yyg[yygc] = new YYARec(-52,150);yygc++; 
					yyg[yygc] = new YYARec(-50,151);yygc++; 
					yyg[yygc] = new YYARec(-49,152);yygc++; 
					yyg[yygc] = new YYARec(-48,153);yygc++; 
					yyg[yygc] = new YYARec(-47,154);yygc++; 
					yyg[yygc] = new YYARec(-23,155);yygc++; 
					yyg[yygc] = new YYARec(-17,156);yygc++; 
					yyg[yygc] = new YYARec(-15,157);yygc++; 
					yyg[yygc] = new YYARec(-11,158);yygc++; 
					yyg[yygc] = new YYARec(-5,159);yygc++; 
					yyg[yygc] = new YYARec(-42,176);yygc++; 
					yyg[yygc] = new YYARec(-41,177);yygc++; 
					yyg[yygc] = new YYARec(-40,107);yygc++; 
					yyg[yygc] = new YYARec(-34,108);yygc++; 
					yyg[yygc] = new YYARec(-23,109);yygc++; 
					yyg[yygc] = new YYARec(-20,178);yygc++; 
					yyg[yygc] = new YYARec(-18,179);yygc++; 
					yyg[yygc] = new YYARec(-70,183);yygc++; 
					yyg[yygc] = new YYARec(-38,184);yygc++; 
					yyg[yygc] = new YYARec(-37,185);yygc++; 
					yyg[yygc] = new YYARec(-36,186);yygc++; 
					yyg[yygc] = new YYARec(-23,187);yygc++; 
					yyg[yygc] = new YYARec(-78,1);yygc++; 
					yyg[yygc] = new YYARec(-77,2);yygc++; 
					yyg[yygc] = new YYARec(-74,3);yygc++; 
					yyg[yygc] = new YYARec(-45,4);yygc++; 
					yyg[yygc] = new YYARec(-40,5);yygc++; 
					yyg[yygc] = new YYARec(-39,6);yygc++; 
					yyg[yygc] = new YYARec(-34,7);yygc++; 
					yyg[yygc] = new YYARec(-31,8);yygc++; 
					yyg[yygc] = new YYARec(-29,9);yygc++; 
					yyg[yygc] = new YYARec(-23,10);yygc++; 
					yyg[yygc] = new YYARec(-22,11);yygc++; 
					yyg[yygc] = new YYARec(-14,188);yygc++; 
					yyg[yygc] = new YYARec(-12,12);yygc++; 
					yyg[yygc] = new YYARec(-11,13);yygc++; 
					yyg[yygc] = new YYARec(-10,14);yygc++; 
					yyg[yygc] = new YYARec(-9,15);yygc++; 
					yyg[yygc] = new YYARec(-8,16);yygc++; 
					yyg[yygc] = new YYARec(-7,17);yygc++; 
					yyg[yygc] = new YYARec(-6,18);yygc++; 
					yyg[yygc] = new YYARec(-5,19);yygc++; 
					yyg[yygc] = new YYARec(-4,20);yygc++; 
					yyg[yygc] = new YYARec(-3,189);yygc++; 
					yyg[yygc] = new YYARec(-78,1);yygc++; 
					yyg[yygc] = new YYARec(-77,2);yygc++; 
					yyg[yygc] = new YYARec(-74,3);yygc++; 
					yyg[yygc] = new YYARec(-45,4);yygc++; 
					yyg[yygc] = new YYARec(-40,5);yygc++; 
					yyg[yygc] = new YYARec(-39,6);yygc++; 
					yyg[yygc] = new YYARec(-34,7);yygc++; 
					yyg[yygc] = new YYARec(-31,8);yygc++; 
					yyg[yygc] = new YYARec(-29,9);yygc++; 
					yyg[yygc] = new YYARec(-23,10);yygc++; 
					yyg[yygc] = new YYARec(-22,11);yygc++; 
					yyg[yygc] = new YYARec(-14,190);yygc++; 
					yyg[yygc] = new YYARec(-12,12);yygc++; 
					yyg[yygc] = new YYARec(-11,13);yygc++; 
					yyg[yygc] = new YYARec(-10,14);yygc++; 
					yyg[yygc] = new YYARec(-9,15);yygc++; 
					yyg[yygc] = new YYARec(-8,16);yygc++; 
					yyg[yygc] = new YYARec(-7,17);yygc++; 
					yyg[yygc] = new YYARec(-6,18);yygc++; 
					yyg[yygc] = new YYARec(-5,19);yygc++; 
					yyg[yygc] = new YYARec(-4,20);yygc++; 
					yyg[yygc] = new YYARec(-3,189);yygc++; 
					yyg[yygc] = new YYARec(-78,1);yygc++; 
					yyg[yygc] = new YYARec(-77,2);yygc++; 
					yyg[yygc] = new YYARec(-74,3);yygc++; 
					yyg[yygc] = new YYARec(-23,10);yygc++; 
					yyg[yygc] = new YYARec(-5,191);yygc++; 
					yyg[yygc] = new YYARec(-78,1);yygc++; 
					yyg[yygc] = new YYARec(-77,2);yygc++; 
					yyg[yygc] = new YYARec(-76,77);yygc++; 
					yyg[yygc] = new YYARec(-74,3);yygc++; 
					yyg[yygc] = new YYARec(-70,137);yygc++; 
					yyg[yygc] = new YYARec(-53,194);yygc++; 
					yyg[yygc] = new YYARec(-51,195);yygc++; 
					yyg[yygc] = new YYARec(-38,79);yygc++; 
					yyg[yygc] = new YYARec(-28,196);yygc++; 
					yyg[yygc] = new YYARec(-27,197);yygc++; 
					yyg[yygc] = new YYARec(-26,198);yygc++; 
					yyg[yygc] = new YYARec(-25,199);yygc++; 
					yyg[yygc] = new YYARec(-24,200);yygc++; 
					yyg[yygc] = new YYARec(-23,10);yygc++; 
					yyg[yygc] = new YYARec(-5,144);yygc++; 
					yyg[yygc] = new YYARec(-78,1);yygc++; 
					yyg[yygc] = new YYARec(-77,2);yygc++; 
					yyg[yygc] = new YYARec(-74,149);yygc++; 
					yyg[yygc] = new YYARec(-52,150);yygc++; 
					yyg[yygc] = new YYARec(-50,151);yygc++; 
					yyg[yygc] = new YYARec(-49,152);yygc++; 
					yyg[yygc] = new YYARec(-48,153);yygc++; 
					yyg[yygc] = new YYARec(-47,154);yygc++; 
					yyg[yygc] = new YYARec(-23,155);yygc++; 
					yyg[yygc] = new YYARec(-17,202);yygc++; 
					yyg[yygc] = new YYARec(-15,157);yygc++; 
					yyg[yygc] = new YYARec(-11,158);yygc++; 
					yyg[yygc] = new YYARec(-5,159);yygc++; 
					yyg[yygc] = new YYARec(-78,1);yygc++; 
					yyg[yygc] = new YYARec(-77,2);yygc++; 
					yyg[yygc] = new YYARec(-74,149);yygc++; 
					yyg[yygc] = new YYARec(-52,150);yygc++; 
					yyg[yygc] = new YYARec(-50,151);yygc++; 
					yyg[yygc] = new YYARec(-49,152);yygc++; 
					yyg[yygc] = new YYARec(-48,153);yygc++; 
					yyg[yygc] = new YYARec(-47,154);yygc++; 
					yyg[yygc] = new YYARec(-23,155);yygc++; 
					yyg[yygc] = new YYARec(-17,203);yygc++; 
					yyg[yygc] = new YYARec(-15,157);yygc++; 
					yyg[yygc] = new YYARec(-11,158);yygc++; 
					yyg[yygc] = new YYARec(-5,159);yygc++; 
					yyg[yygc] = new YYARec(-78,1);yygc++; 
					yyg[yygc] = new YYARec(-77,2);yygc++; 
					yyg[yygc] = new YYARec(-76,77);yygc++; 
					yyg[yygc] = new YYARec(-74,3);yygc++; 
					yyg[yygc] = new YYARec(-70,137);yygc++; 
					yyg[yygc] = new YYARec(-53,194);yygc++; 
					yyg[yygc] = new YYARec(-51,205);yygc++; 
					yyg[yygc] = new YYARec(-38,79);yygc++; 
					yyg[yygc] = new YYARec(-28,196);yygc++; 
					yyg[yygc] = new YYARec(-27,197);yygc++; 
					yyg[yygc] = new YYARec(-26,198);yygc++; 
					yyg[yygc] = new YYARec(-25,199);yygc++; 
					yyg[yygc] = new YYARec(-24,200);yygc++; 
					yyg[yygc] = new YYARec(-23,10);yygc++; 
					yyg[yygc] = new YYARec(-5,144);yygc++; 
					yyg[yygc] = new YYARec(-78,1);yygc++; 
					yyg[yygc] = new YYARec(-77,2);yygc++; 
					yyg[yygc] = new YYARec(-74,149);yygc++; 
					yyg[yygc] = new YYARec(-52,150);yygc++; 
					yyg[yygc] = new YYARec(-50,151);yygc++; 
					yyg[yygc] = new YYARec(-49,152);yygc++; 
					yyg[yygc] = new YYARec(-48,153);yygc++; 
					yyg[yygc] = new YYARec(-47,154);yygc++; 
					yyg[yygc] = new YYARec(-23,155);yygc++; 
					yyg[yygc] = new YYARec(-17,207);yygc++; 
					yyg[yygc] = new YYARec(-15,157);yygc++; 
					yyg[yygc] = new YYARec(-11,158);yygc++; 
					yyg[yygc] = new YYARec(-5,159);yygc++; 
					yyg[yygc] = new YYARec(-73,208);yygc++; 
					yyg[yygc] = new YYARec(-23,95);yygc++; 
					yyg[yygc] = new YYARec(-22,96);yygc++; 
					yyg[yygc] = new YYARec(-13,214);yygc++; 
					yyg[yygc] = new YYARec(-23,95);yygc++; 
					yyg[yygc] = new YYARec(-22,96);yygc++; 
					yyg[yygc] = new YYARec(-13,215);yygc++; 
					yyg[yygc] = new YYARec(-78,1);yygc++; 
					yyg[yygc] = new YYARec(-77,2);yygc++; 
					yyg[yygc] = new YYARec(-74,149);yygc++; 
					yyg[yygc] = new YYARec(-52,150);yygc++; 
					yyg[yygc] = new YYARec(-50,151);yygc++; 
					yyg[yygc] = new YYARec(-49,152);yygc++; 
					yyg[yygc] = new YYARec(-48,153);yygc++; 
					yyg[yygc] = new YYARec(-47,154);yygc++; 
					yyg[yygc] = new YYARec(-23,155);yygc++; 
					yyg[yygc] = new YYARec(-17,216);yygc++; 
					yyg[yygc] = new YYARec(-15,157);yygc++; 
					yyg[yygc] = new YYARec(-11,158);yygc++; 
					yyg[yygc] = new YYARec(-5,159);yygc++; 
					yyg[yygc] = new YYARec(-78,1);yygc++; 
					yyg[yygc] = new YYARec(-77,2);yygc++; 
					yyg[yygc] = new YYARec(-74,3);yygc++; 
					yyg[yygc] = new YYARec(-72,217);yygc++; 
					yyg[yygc] = new YYARec(-71,218);yygc++; 
					yyg[yygc] = new YYARec(-70,219);yygc++; 
					yyg[yygc] = new YYARec(-69,220);yygc++; 
					yyg[yygc] = new YYARec(-67,221);yygc++; 
					yyg[yygc] = new YYARec(-65,222);yygc++; 
					yyg[yygc] = new YYARec(-63,223);yygc++; 
					yyg[yygc] = new YYARec(-61,224);yygc++; 
					yyg[yygc] = new YYARec(-60,225);yygc++; 
					yyg[yygc] = new YYARec(-59,226);yygc++; 
					yyg[yygc] = new YYARec(-58,227);yygc++; 
					yyg[yygc] = new YYARec(-57,228);yygc++; 
					yyg[yygc] = new YYARec(-56,229);yygc++; 
					yyg[yygc] = new YYARec(-55,230);yygc++; 
					yyg[yygc] = new YYARec(-54,231);yygc++; 
					yyg[yygc] = new YYARec(-25,232);yygc++; 
					yyg[yygc] = new YYARec(-23,10);yygc++; 
					yyg[yygc] = new YYARec(-5,233);yygc++; 
					yyg[yygc] = new YYARec(-78,1);yygc++; 
					yyg[yygc] = new YYARec(-77,2);yygc++; 
					yyg[yygc] = new YYARec(-74,3);yygc++; 
					yyg[yygc] = new YYARec(-72,217);yygc++; 
					yyg[yygc] = new YYARec(-71,218);yygc++; 
					yyg[yygc] = new YYARec(-70,219);yygc++; 
					yyg[yygc] = new YYARec(-69,220);yygc++; 
					yyg[yygc] = new YYARec(-67,221);yygc++; 
					yyg[yygc] = new YYARec(-65,222);yygc++; 
					yyg[yygc] = new YYARec(-63,223);yygc++; 
					yyg[yygc] = new YYARec(-61,224);yygc++; 
					yyg[yygc] = new YYARec(-60,225);yygc++; 
					yyg[yygc] = new YYARec(-59,226);yygc++; 
					yyg[yygc] = new YYARec(-58,227);yygc++; 
					yyg[yygc] = new YYARec(-57,228);yygc++; 
					yyg[yygc] = new YYARec(-56,229);yygc++; 
					yyg[yygc] = new YYARec(-55,230);yygc++; 
					yyg[yygc] = new YYARec(-54,241);yygc++; 
					yyg[yygc] = new YYARec(-25,232);yygc++; 
					yyg[yygc] = new YYARec(-23,10);yygc++; 
					yyg[yygc] = new YYARec(-5,242);yygc++; 
					yyg[yygc] = new YYARec(-78,1);yygc++; 
					yyg[yygc] = new YYARec(-77,2);yygc++; 
					yyg[yygc] = new YYARec(-74,3);yygc++; 
					yyg[yygc] = new YYARec(-72,217);yygc++; 
					yyg[yygc] = new YYARec(-71,218);yygc++; 
					yyg[yygc] = new YYARec(-70,219);yygc++; 
					yyg[yygc] = new YYARec(-69,220);yygc++; 
					yyg[yygc] = new YYARec(-67,221);yygc++; 
					yyg[yygc] = new YYARec(-65,222);yygc++; 
					yyg[yygc] = new YYARec(-63,223);yygc++; 
					yyg[yygc] = new YYARec(-61,224);yygc++; 
					yyg[yygc] = new YYARec(-60,225);yygc++; 
					yyg[yygc] = new YYARec(-59,226);yygc++; 
					yyg[yygc] = new YYARec(-58,227);yygc++; 
					yyg[yygc] = new YYARec(-57,228);yygc++; 
					yyg[yygc] = new YYARec(-56,229);yygc++; 
					yyg[yygc] = new YYARec(-55,230);yygc++; 
					yyg[yygc] = new YYARec(-54,243);yygc++; 
					yyg[yygc] = new YYARec(-25,232);yygc++; 
					yyg[yygc] = new YYARec(-23,10);yygc++; 
					yyg[yygc] = new YYARec(-5,242);yygc++; 
					yyg[yygc] = new YYARec(-78,1);yygc++; 
					yyg[yygc] = new YYARec(-77,2);yygc++; 
					yyg[yygc] = new YYARec(-76,77);yygc++; 
					yyg[yygc] = new YYARec(-74,3);yygc++; 
					yyg[yygc] = new YYARec(-70,137);yygc++; 
					yyg[yygc] = new YYARec(-44,245);yygc++; 
					yyg[yygc] = new YYARec(-43,246);yygc++; 
					yyg[yygc] = new YYARec(-38,79);yygc++; 
					yyg[yygc] = new YYARec(-28,247);yygc++; 
					yyg[yygc] = new YYARec(-27,248);yygc++; 
					yyg[yygc] = new YYARec(-26,249);yygc++; 
					yyg[yygc] = new YYARec(-24,250);yygc++; 
					yyg[yygc] = new YYARec(-23,10);yygc++; 
					yyg[yygc] = new YYARec(-5,144);yygc++; 
					yyg[yygc] = new YYARec(-42,176);yygc++; 
					yyg[yygc] = new YYARec(-41,177);yygc++; 
					yyg[yygc] = new YYARec(-40,107);yygc++; 
					yyg[yygc] = new YYARec(-34,108);yygc++; 
					yyg[yygc] = new YYARec(-23,109);yygc++; 
					yyg[yygc] = new YYARec(-20,251);yygc++; 
					yyg[yygc] = new YYARec(-18,179);yygc++; 
					yyg[yygc] = new YYARec(-42,176);yygc++; 
					yyg[yygc] = new YYARec(-41,177);yygc++; 
					yyg[yygc] = new YYARec(-40,107);yygc++; 
					yyg[yygc] = new YYARec(-34,108);yygc++; 
					yyg[yygc] = new YYARec(-23,109);yygc++; 
					yyg[yygc] = new YYARec(-20,253);yygc++; 
					yyg[yygc] = new YYARec(-18,179);yygc++; 
					yyg[yygc] = new YYARec(-23,95);yygc++; 
					yyg[yygc] = new YYARec(-22,96);yygc++; 
					yyg[yygc] = new YYARec(-13,254);yygc++; 
					yyg[yygc] = new YYARec(-23,95);yygc++; 
					yyg[yygc] = new YYARec(-22,96);yygc++; 
					yyg[yygc] = new YYARec(-13,255);yygc++; 
					yyg[yygc] = new YYARec(-70,183);yygc++; 
					yyg[yygc] = new YYARec(-38,184);yygc++; 
					yyg[yygc] = new YYARec(-37,185);yygc++; 
					yyg[yygc] = new YYARec(-36,256);yygc++; 
					yyg[yygc] = new YYARec(-23,187);yygc++; 
					yyg[yygc] = new YYARec(-78,1);yygc++; 
					yyg[yygc] = new YYARec(-77,2);yygc++; 
					yyg[yygc] = new YYARec(-76,77);yygc++; 
					yyg[yygc] = new YYARec(-74,3);yygc++; 
					yyg[yygc] = new YYARec(-70,137);yygc++; 
					yyg[yygc] = new YYARec(-53,194);yygc++; 
					yyg[yygc] = new YYARec(-51,262);yygc++; 
					yyg[yygc] = new YYARec(-38,79);yygc++; 
					yyg[yygc] = new YYARec(-28,196);yygc++; 
					yyg[yygc] = new YYARec(-27,197);yygc++; 
					yyg[yygc] = new YYARec(-26,198);yygc++; 
					yyg[yygc] = new YYARec(-25,199);yygc++; 
					yyg[yygc] = new YYARec(-24,200);yygc++; 
					yyg[yygc] = new YYARec(-23,10);yygc++; 
					yyg[yygc] = new YYARec(-5,144);yygc++; 
					yyg[yygc] = new YYARec(-78,1);yygc++; 
					yyg[yygc] = new YYARec(-77,2);yygc++; 
					yyg[yygc] = new YYARec(-74,149);yygc++; 
					yyg[yygc] = new YYARec(-52,150);yygc++; 
					yyg[yygc] = new YYARec(-50,151);yygc++; 
					yyg[yygc] = new YYARec(-49,152);yygc++; 
					yyg[yygc] = new YYARec(-48,153);yygc++; 
					yyg[yygc] = new YYARec(-47,154);yygc++; 
					yyg[yygc] = new YYARec(-23,155);yygc++; 
					yyg[yygc] = new YYARec(-17,264);yygc++; 
					yyg[yygc] = new YYARec(-15,157);yygc++; 
					yyg[yygc] = new YYARec(-11,158);yygc++; 
					yyg[yygc] = new YYARec(-5,159);yygc++; 
					yyg[yygc] = new YYARec(-78,1);yygc++; 
					yyg[yygc] = new YYARec(-77,2);yygc++; 
					yyg[yygc] = new YYARec(-74,3);yygc++; 
					yyg[yygc] = new YYARec(-72,217);yygc++; 
					yyg[yygc] = new YYARec(-71,218);yygc++; 
					yyg[yygc] = new YYARec(-70,219);yygc++; 
					yyg[yygc] = new YYARec(-69,220);yygc++; 
					yyg[yygc] = new YYARec(-67,221);yygc++; 
					yyg[yygc] = new YYARec(-65,222);yygc++; 
					yyg[yygc] = new YYARec(-63,223);yygc++; 
					yyg[yygc] = new YYARec(-61,224);yygc++; 
					yyg[yygc] = new YYARec(-60,225);yygc++; 
					yyg[yygc] = new YYARec(-59,226);yygc++; 
					yyg[yygc] = new YYARec(-58,227);yygc++; 
					yyg[yygc] = new YYARec(-57,228);yygc++; 
					yyg[yygc] = new YYARec(-56,229);yygc++; 
					yyg[yygc] = new YYARec(-55,230);yygc++; 
					yyg[yygc] = new YYARec(-54,266);yygc++; 
					yyg[yygc] = new YYARec(-25,232);yygc++; 
					yyg[yygc] = new YYARec(-23,10);yygc++; 
					yyg[yygc] = new YYARec(-5,242);yygc++; 
					yyg[yygc] = new YYARec(-78,1);yygc++; 
					yyg[yygc] = new YYARec(-77,2);yygc++; 
					yyg[yygc] = new YYARec(-74,149);yygc++; 
					yyg[yygc] = new YYARec(-52,150);yygc++; 
					yyg[yygc] = new YYARec(-50,151);yygc++; 
					yyg[yygc] = new YYARec(-49,152);yygc++; 
					yyg[yygc] = new YYARec(-48,153);yygc++; 
					yyg[yygc] = new YYARec(-47,154);yygc++; 
					yyg[yygc] = new YYARec(-23,155);yygc++; 
					yyg[yygc] = new YYARec(-17,267);yygc++; 
					yyg[yygc] = new YYARec(-16,268);yygc++; 
					yyg[yygc] = new YYARec(-15,157);yygc++; 
					yyg[yygc] = new YYARec(-11,158);yygc++; 
					yyg[yygc] = new YYARec(-5,159);yygc++; 
					yyg[yygc] = new YYARec(-78,1);yygc++; 
					yyg[yygc] = new YYARec(-77,2);yygc++; 
					yyg[yygc] = new YYARec(-74,149);yygc++; 
					yyg[yygc] = new YYARec(-52,150);yygc++; 
					yyg[yygc] = new YYARec(-50,151);yygc++; 
					yyg[yygc] = new YYARec(-49,152);yygc++; 
					yyg[yygc] = new YYARec(-48,153);yygc++; 
					yyg[yygc] = new YYARec(-47,154);yygc++; 
					yyg[yygc] = new YYARec(-23,155);yygc++; 
					yyg[yygc] = new YYARec(-17,267);yygc++; 
					yyg[yygc] = new YYARec(-16,269);yygc++; 
					yyg[yygc] = new YYARec(-15,157);yygc++; 
					yyg[yygc] = new YYARec(-11,158);yygc++; 
					yyg[yygc] = new YYARec(-5,159);yygc++; 
					yyg[yygc] = new YYARec(-78,1);yygc++; 
					yyg[yygc] = new YYARec(-77,2);yygc++; 
					yyg[yygc] = new YYARec(-74,3);yygc++; 
					yyg[yygc] = new YYARec(-72,217);yygc++; 
					yyg[yygc] = new YYARec(-71,218);yygc++; 
					yyg[yygc] = new YYARec(-70,219);yygc++; 
					yyg[yygc] = new YYARec(-69,220);yygc++; 
					yyg[yygc] = new YYARec(-67,272);yygc++; 
					yyg[yygc] = new YYARec(-25,232);yygc++; 
					yyg[yygc] = new YYARec(-23,10);yygc++; 
					yyg[yygc] = new YYARec(-5,242);yygc++; 
					yyg[yygc] = new YYARec(-68,273);yygc++; 
					yyg[yygc] = new YYARec(-66,277);yygc++; 
					yyg[yygc] = new YYARec(-64,280);yygc++; 
					yyg[yygc] = new YYARec(-62,285);yygc++; 
					yyg[yygc] = new YYARec(-73,293);yygc++; 
					yyg[yygc] = new YYARec(-78,1);yygc++; 
					yyg[yygc] = new YYARec(-77,2);yygc++; 
					yyg[yygc] = new YYARec(-74,3);yygc++; 
					yyg[yygc] = new YYARec(-72,217);yygc++; 
					yyg[yygc] = new YYARec(-71,218);yygc++; 
					yyg[yygc] = new YYARec(-70,219);yygc++; 
					yyg[yygc] = new YYARec(-69,220);yygc++; 
					yyg[yygc] = new YYARec(-67,221);yygc++; 
					yyg[yygc] = new YYARec(-65,222);yygc++; 
					yyg[yygc] = new YYARec(-63,223);yygc++; 
					yyg[yygc] = new YYARec(-61,224);yygc++; 
					yyg[yygc] = new YYARec(-60,225);yygc++; 
					yyg[yygc] = new YYARec(-59,226);yygc++; 
					yyg[yygc] = new YYARec(-58,227);yygc++; 
					yyg[yygc] = new YYARec(-57,228);yygc++; 
					yyg[yygc] = new YYARec(-56,229);yygc++; 
					yyg[yygc] = new YYARec(-55,230);yygc++; 
					yyg[yygc] = new YYARec(-54,295);yygc++; 
					yyg[yygc] = new YYARec(-25,232);yygc++; 
					yyg[yygc] = new YYARec(-23,10);yygc++; 
					yyg[yygc] = new YYARec(-5,242);yygc++; 
					yyg[yygc] = new YYARec(-78,1);yygc++; 
					yyg[yygc] = new YYARec(-77,2);yygc++; 
					yyg[yygc] = new YYARec(-74,149);yygc++; 
					yyg[yygc] = new YYARec(-52,150);yygc++; 
					yyg[yygc] = new YYARec(-50,151);yygc++; 
					yyg[yygc] = new YYARec(-49,152);yygc++; 
					yyg[yygc] = new YYARec(-48,153);yygc++; 
					yyg[yygc] = new YYARec(-47,154);yygc++; 
					yyg[yygc] = new YYARec(-23,155);yygc++; 
					yyg[yygc] = new YYARec(-17,297);yygc++; 
					yyg[yygc] = new YYARec(-15,157);yygc++; 
					yyg[yygc] = new YYARec(-11,158);yygc++; 
					yyg[yygc] = new YYARec(-5,159);yygc++; 
					yyg[yygc] = new YYARec(-78,1);yygc++; 
					yyg[yygc] = new YYARec(-77,2);yygc++; 
					yyg[yygc] = new YYARec(-74,149);yygc++; 
					yyg[yygc] = new YYARec(-52,150);yygc++; 
					yyg[yygc] = new YYARec(-50,151);yygc++; 
					yyg[yygc] = new YYARec(-49,152);yygc++; 
					yyg[yygc] = new YYARec(-48,153);yygc++; 
					yyg[yygc] = new YYARec(-47,154);yygc++; 
					yyg[yygc] = new YYARec(-23,155);yygc++; 
					yyg[yygc] = new YYARec(-17,300);yygc++; 
					yyg[yygc] = new YYARec(-15,157);yygc++; 
					yyg[yygc] = new YYARec(-11,158);yygc++; 
					yyg[yygc] = new YYARec(-5,159);yygc++; 
					yyg[yygc] = new YYARec(-78,1);yygc++; 
					yyg[yygc] = new YYARec(-77,2);yygc++; 
					yyg[yygc] = new YYARec(-76,77);yygc++; 
					yyg[yygc] = new YYARec(-74,3);yygc++; 
					yyg[yygc] = new YYARec(-70,137);yygc++; 
					yyg[yygc] = new YYARec(-44,245);yygc++; 
					yyg[yygc] = new YYARec(-43,301);yygc++; 
					yyg[yygc] = new YYARec(-38,79);yygc++; 
					yyg[yygc] = new YYARec(-28,247);yygc++; 
					yyg[yygc] = new YYARec(-27,248);yygc++; 
					yyg[yygc] = new YYARec(-26,249);yygc++; 
					yyg[yygc] = new YYARec(-24,250);yygc++; 
					yyg[yygc] = new YYARec(-23,10);yygc++; 
					yyg[yygc] = new YYARec(-5,144);yygc++; 
					yyg[yygc] = new YYARec(-78,1);yygc++; 
					yyg[yygc] = new YYARec(-77,2);yygc++; 
					yyg[yygc] = new YYARec(-74,149);yygc++; 
					yyg[yygc] = new YYARec(-52,150);yygc++; 
					yyg[yygc] = new YYARec(-50,151);yygc++; 
					yyg[yygc] = new YYARec(-49,152);yygc++; 
					yyg[yygc] = new YYARec(-48,153);yygc++; 
					yyg[yygc] = new YYARec(-47,154);yygc++; 
					yyg[yygc] = new YYARec(-23,155);yygc++; 
					yyg[yygc] = new YYARec(-17,308);yygc++; 
					yyg[yygc] = new YYARec(-15,157);yygc++; 
					yyg[yygc] = new YYARec(-11,158);yygc++; 
					yyg[yygc] = new YYARec(-5,159);yygc++; 
					yyg[yygc] = new YYARec(-78,1);yygc++; 
					yyg[yygc] = new YYARec(-77,2);yygc++; 
					yyg[yygc] = new YYARec(-74,3);yygc++; 
					yyg[yygc] = new YYARec(-72,217);yygc++; 
					yyg[yygc] = new YYARec(-71,218);yygc++; 
					yyg[yygc] = new YYARec(-70,219);yygc++; 
					yyg[yygc] = new YYARec(-69,220);yygc++; 
					yyg[yygc] = new YYARec(-67,221);yygc++; 
					yyg[yygc] = new YYARec(-65,222);yygc++; 
					yyg[yygc] = new YYARec(-63,223);yygc++; 
					yyg[yygc] = new YYARec(-61,224);yygc++; 
					yyg[yygc] = new YYARec(-60,225);yygc++; 
					yyg[yygc] = new YYARec(-59,226);yygc++; 
					yyg[yygc] = new YYARec(-58,227);yygc++; 
					yyg[yygc] = new YYARec(-57,228);yygc++; 
					yyg[yygc] = new YYARec(-56,229);yygc++; 
					yyg[yygc] = new YYARec(-55,230);yygc++; 
					yyg[yygc] = new YYARec(-54,309);yygc++; 
					yyg[yygc] = new YYARec(-25,232);yygc++; 
					yyg[yygc] = new YYARec(-23,10);yygc++; 
					yyg[yygc] = new YYARec(-5,242);yygc++; 
					yyg[yygc] = new YYARec(-78,1);yygc++; 
					yyg[yygc] = new YYARec(-77,2);yygc++; 
					yyg[yygc] = new YYARec(-74,3);yygc++; 
					yyg[yygc] = new YYARec(-72,217);yygc++; 
					yyg[yygc] = new YYARec(-71,218);yygc++; 
					yyg[yygc] = new YYARec(-70,219);yygc++; 
					yyg[yygc] = new YYARec(-69,220);yygc++; 
					yyg[yygc] = new YYARec(-67,310);yygc++; 
					yyg[yygc] = new YYARec(-25,232);yygc++; 
					yyg[yygc] = new YYARec(-23,10);yygc++; 
					yyg[yygc] = new YYARec(-5,242);yygc++; 
					yyg[yygc] = new YYARec(-78,1);yygc++; 
					yyg[yygc] = new YYARec(-77,2);yygc++; 
					yyg[yygc] = new YYARec(-74,3);yygc++; 
					yyg[yygc] = new YYARec(-72,217);yygc++; 
					yyg[yygc] = new YYARec(-71,218);yygc++; 
					yyg[yygc] = new YYARec(-70,219);yygc++; 
					yyg[yygc] = new YYARec(-69,220);yygc++; 
					yyg[yygc] = new YYARec(-67,221);yygc++; 
					yyg[yygc] = new YYARec(-65,311);yygc++; 
					yyg[yygc] = new YYARec(-25,232);yygc++; 
					yyg[yygc] = new YYARec(-23,10);yygc++; 
					yyg[yygc] = new YYARec(-5,242);yygc++; 
					yyg[yygc] = new YYARec(-78,1);yygc++; 
					yyg[yygc] = new YYARec(-77,2);yygc++; 
					yyg[yygc] = new YYARec(-74,3);yygc++; 
					yyg[yygc] = new YYARec(-72,217);yygc++; 
					yyg[yygc] = new YYARec(-71,218);yygc++; 
					yyg[yygc] = new YYARec(-70,219);yygc++; 
					yyg[yygc] = new YYARec(-69,220);yygc++; 
					yyg[yygc] = new YYARec(-67,221);yygc++; 
					yyg[yygc] = new YYARec(-65,222);yygc++; 
					yyg[yygc] = new YYARec(-63,312);yygc++; 
					yyg[yygc] = new YYARec(-25,232);yygc++; 
					yyg[yygc] = new YYARec(-23,10);yygc++; 
					yyg[yygc] = new YYARec(-5,242);yygc++; 
					yyg[yygc] = new YYARec(-78,1);yygc++; 
					yyg[yygc] = new YYARec(-77,2);yygc++; 
					yyg[yygc] = new YYARec(-74,3);yygc++; 
					yyg[yygc] = new YYARec(-72,217);yygc++; 
					yyg[yygc] = new YYARec(-71,218);yygc++; 
					yyg[yygc] = new YYARec(-70,219);yygc++; 
					yyg[yygc] = new YYARec(-69,220);yygc++; 
					yyg[yygc] = new YYARec(-67,221);yygc++; 
					yyg[yygc] = new YYARec(-65,222);yygc++; 
					yyg[yygc] = new YYARec(-63,223);yygc++; 
					yyg[yygc] = new YYARec(-61,313);yygc++; 
					yyg[yygc] = new YYARec(-25,232);yygc++; 
					yyg[yygc] = new YYARec(-23,10);yygc++; 
					yyg[yygc] = new YYARec(-5,242);yygc++; 
					yyg[yygc] = new YYARec(-78,1);yygc++; 
					yyg[yygc] = new YYARec(-77,2);yygc++; 
					yyg[yygc] = new YYARec(-74,3);yygc++; 
					yyg[yygc] = new YYARec(-72,217);yygc++; 
					yyg[yygc] = new YYARec(-71,218);yygc++; 
					yyg[yygc] = new YYARec(-70,219);yygc++; 
					yyg[yygc] = new YYARec(-69,220);yygc++; 
					yyg[yygc] = new YYARec(-67,221);yygc++; 
					yyg[yygc] = new YYARec(-65,222);yygc++; 
					yyg[yygc] = new YYARec(-63,223);yygc++; 
					yyg[yygc] = new YYARec(-61,224);yygc++; 
					yyg[yygc] = new YYARec(-60,314);yygc++; 
					yyg[yygc] = new YYARec(-25,232);yygc++; 
					yyg[yygc] = new YYARec(-23,10);yygc++; 
					yyg[yygc] = new YYARec(-5,242);yygc++; 
					yyg[yygc] = new YYARec(-78,1);yygc++; 
					yyg[yygc] = new YYARec(-77,2);yygc++; 
					yyg[yygc] = new YYARec(-74,3);yygc++; 
					yyg[yygc] = new YYARec(-72,217);yygc++; 
					yyg[yygc] = new YYARec(-71,218);yygc++; 
					yyg[yygc] = new YYARec(-70,219);yygc++; 
					yyg[yygc] = new YYARec(-69,220);yygc++; 
					yyg[yygc] = new YYARec(-67,221);yygc++; 
					yyg[yygc] = new YYARec(-65,222);yygc++; 
					yyg[yygc] = new YYARec(-63,223);yygc++; 
					yyg[yygc] = new YYARec(-61,224);yygc++; 
					yyg[yygc] = new YYARec(-60,225);yygc++; 
					yyg[yygc] = new YYARec(-59,315);yygc++; 
					yyg[yygc] = new YYARec(-25,232);yygc++; 
					yyg[yygc] = new YYARec(-23,10);yygc++; 
					yyg[yygc] = new YYARec(-5,242);yygc++; 
					yyg[yygc] = new YYARec(-78,1);yygc++; 
					yyg[yygc] = new YYARec(-77,2);yygc++; 
					yyg[yygc] = new YYARec(-74,3);yygc++; 
					yyg[yygc] = new YYARec(-72,217);yygc++; 
					yyg[yygc] = new YYARec(-71,218);yygc++; 
					yyg[yygc] = new YYARec(-70,219);yygc++; 
					yyg[yygc] = new YYARec(-69,220);yygc++; 
					yyg[yygc] = new YYARec(-67,221);yygc++; 
					yyg[yygc] = new YYARec(-65,222);yygc++; 
					yyg[yygc] = new YYARec(-63,223);yygc++; 
					yyg[yygc] = new YYARec(-61,224);yygc++; 
					yyg[yygc] = new YYARec(-60,225);yygc++; 
					yyg[yygc] = new YYARec(-59,226);yygc++; 
					yyg[yygc] = new YYARec(-58,316);yygc++; 
					yyg[yygc] = new YYARec(-25,232);yygc++; 
					yyg[yygc] = new YYARec(-23,10);yygc++; 
					yyg[yygc] = new YYARec(-5,242);yygc++; 
					yyg[yygc] = new YYARec(-78,1);yygc++; 
					yyg[yygc] = new YYARec(-77,2);yygc++; 
					yyg[yygc] = new YYARec(-74,3);yygc++; 
					yyg[yygc] = new YYARec(-72,217);yygc++; 
					yyg[yygc] = new YYARec(-71,218);yygc++; 
					yyg[yygc] = new YYARec(-70,219);yygc++; 
					yyg[yygc] = new YYARec(-69,220);yygc++; 
					yyg[yygc] = new YYARec(-67,221);yygc++; 
					yyg[yygc] = new YYARec(-65,222);yygc++; 
					yyg[yygc] = new YYARec(-63,223);yygc++; 
					yyg[yygc] = new YYARec(-61,224);yygc++; 
					yyg[yygc] = new YYARec(-60,225);yygc++; 
					yyg[yygc] = new YYARec(-59,226);yygc++; 
					yyg[yygc] = new YYARec(-58,227);yygc++; 
					yyg[yygc] = new YYARec(-57,317);yygc++; 
					yyg[yygc] = new YYARec(-25,232);yygc++; 
					yyg[yygc] = new YYARec(-23,10);yygc++; 
					yyg[yygc] = new YYARec(-5,242);yygc++; 
					yyg[yygc] = new YYARec(-78,1);yygc++; 
					yyg[yygc] = new YYARec(-77,2);yygc++; 
					yyg[yygc] = new YYARec(-74,3);yygc++; 
					yyg[yygc] = new YYARec(-72,217);yygc++; 
					yyg[yygc] = new YYARec(-71,218);yygc++; 
					yyg[yygc] = new YYARec(-70,219);yygc++; 
					yyg[yygc] = new YYARec(-69,220);yygc++; 
					yyg[yygc] = new YYARec(-67,221);yygc++; 
					yyg[yygc] = new YYARec(-65,222);yygc++; 
					yyg[yygc] = new YYARec(-63,223);yygc++; 
					yyg[yygc] = new YYARec(-61,224);yygc++; 
					yyg[yygc] = new YYARec(-60,225);yygc++; 
					yyg[yygc] = new YYARec(-59,226);yygc++; 
					yyg[yygc] = new YYARec(-58,227);yygc++; 
					yyg[yygc] = new YYARec(-57,228);yygc++; 
					yyg[yygc] = new YYARec(-56,318);yygc++; 
					yyg[yygc] = new YYARec(-25,232);yygc++; 
					yyg[yygc] = new YYARec(-23,10);yygc++; 
					yyg[yygc] = new YYARec(-5,242);yygc++; 
					yyg[yygc] = new YYARec(-78,1);yygc++; 
					yyg[yygc] = new YYARec(-77,2);yygc++; 
					yyg[yygc] = new YYARec(-74,3);yygc++; 
					yyg[yygc] = new YYARec(-72,217);yygc++; 
					yyg[yygc] = new YYARec(-71,218);yygc++; 
					yyg[yygc] = new YYARec(-70,219);yygc++; 
					yyg[yygc] = new YYARec(-69,220);yygc++; 
					yyg[yygc] = new YYARec(-67,221);yygc++; 
					yyg[yygc] = new YYARec(-65,222);yygc++; 
					yyg[yygc] = new YYARec(-63,223);yygc++; 
					yyg[yygc] = new YYARec(-61,224);yygc++; 
					yyg[yygc] = new YYARec(-60,225);yygc++; 
					yyg[yygc] = new YYARec(-59,226);yygc++; 
					yyg[yygc] = new YYARec(-58,227);yygc++; 
					yyg[yygc] = new YYARec(-57,228);yygc++; 
					yyg[yygc] = new YYARec(-56,229);yygc++; 
					yyg[yygc] = new YYARec(-55,230);yygc++; 
					yyg[yygc] = new YYARec(-54,319);yygc++; 
					yyg[yygc] = new YYARec(-25,232);yygc++; 
					yyg[yygc] = new YYARec(-23,10);yygc++; 
					yyg[yygc] = new YYARec(-5,242);yygc++; 
					yyg[yygc] = new YYARec(-78,1);yygc++; 
					yyg[yygc] = new YYARec(-77,2);yygc++; 
					yyg[yygc] = new YYARec(-74,149);yygc++; 
					yyg[yygc] = new YYARec(-52,150);yygc++; 
					yyg[yygc] = new YYARec(-50,151);yygc++; 
					yyg[yygc] = new YYARec(-49,152);yygc++; 
					yyg[yygc] = new YYARec(-48,153);yygc++; 
					yyg[yygc] = new YYARec(-47,154);yygc++; 
					yyg[yygc] = new YYARec(-23,155);yygc++; 
					yyg[yygc] = new YYARec(-17,322);yygc++; 
					yyg[yygc] = new YYARec(-15,157);yygc++; 
					yyg[yygc] = new YYARec(-11,158);yygc++; 
					yyg[yygc] = new YYARec(-5,159);yygc++; 
					yyg[yygc] = new YYARec(-78,1);yygc++; 
					yyg[yygc] = new YYARec(-77,2);yygc++; 
					yyg[yygc] = new YYARec(-74,149);yygc++; 
					yyg[yygc] = new YYARec(-52,150);yygc++; 
					yyg[yygc] = new YYARec(-50,151);yygc++; 
					yyg[yygc] = new YYARec(-49,152);yygc++; 
					yyg[yygc] = new YYARec(-48,153);yygc++; 
					yyg[yygc] = new YYARec(-47,154);yygc++; 
					yyg[yygc] = new YYARec(-23,155);yygc++; 
					yyg[yygc] = new YYARec(-17,323);yygc++; 
					yyg[yygc] = new YYARec(-15,157);yygc++; 
					yyg[yygc] = new YYARec(-11,158);yygc++; 
					yyg[yygc] = new YYARec(-5,159);yygc++; 
					yyg[yygc] = new YYARec(-42,176);yygc++; 
					yyg[yygc] = new YYARec(-41,177);yygc++; 
					yyg[yygc] = new YYARec(-40,107);yygc++; 
					yyg[yygc] = new YYARec(-34,108);yygc++; 
					yyg[yygc] = new YYARec(-23,109);yygc++; 
					yyg[yygc] = new YYARec(-20,324);yygc++; 
					yyg[yygc] = new YYARec(-19,325);yygc++; 
					yyg[yygc] = new YYARec(-18,179);yygc++; 
					yyg[yygc] = new YYARec(-42,176);yygc++; 
					yyg[yygc] = new YYARec(-41,177);yygc++; 
					yyg[yygc] = new YYARec(-40,107);yygc++; 
					yyg[yygc] = new YYARec(-34,108);yygc++; 
					yyg[yygc] = new YYARec(-23,109);yygc++; 
					yyg[yygc] = new YYARec(-20,324);yygc++; 
					yyg[yygc] = new YYARec(-19,326);yygc++; 
					yyg[yygc] = new YYARec(-18,179);yygc++; 
					yyg[yygc] = new YYARec(-78,1);yygc++; 
					yyg[yygc] = new YYARec(-77,2);yygc++; 
					yyg[yygc] = new YYARec(-74,3);yygc++; 
					yyg[yygc] = new YYARec(-45,4);yygc++; 
					yyg[yygc] = new YYARec(-40,5);yygc++; 
					yyg[yygc] = new YYARec(-39,6);yygc++; 
					yyg[yygc] = new YYARec(-34,7);yygc++; 
					yyg[yygc] = new YYARec(-31,8);yygc++; 
					yyg[yygc] = new YYARec(-29,9);yygc++; 
					yyg[yygc] = new YYARec(-23,10);yygc++; 
					yyg[yygc] = new YYARec(-22,11);yygc++; 
					yyg[yygc] = new YYARec(-12,12);yygc++; 
					yyg[yygc] = new YYARec(-11,13);yygc++; 
					yyg[yygc] = new YYARec(-10,14);yygc++; 
					yyg[yygc] = new YYARec(-9,15);yygc++; 
					yyg[yygc] = new YYARec(-8,16);yygc++; 
					yyg[yygc] = new YYARec(-7,17);yygc++; 
					yyg[yygc] = new YYARec(-6,18);yygc++; 
					yyg[yygc] = new YYARec(-5,19);yygc++; 
					yyg[yygc] = new YYARec(-4,20);yygc++; 
					yyg[yygc] = new YYARec(-3,327);yygc++; 
					yyg[yygc] = new YYARec(-78,1);yygc++; 
					yyg[yygc] = new YYARec(-77,2);yygc++; 
					yyg[yygc] = new YYARec(-74,149);yygc++; 
					yyg[yygc] = new YYARec(-52,150);yygc++; 
					yyg[yygc] = new YYARec(-50,151);yygc++; 
					yyg[yygc] = new YYARec(-49,152);yygc++; 
					yyg[yygc] = new YYARec(-48,153);yygc++; 
					yyg[yygc] = new YYARec(-47,154);yygc++; 
					yyg[yygc] = new YYARec(-23,155);yygc++; 
					yyg[yygc] = new YYARec(-17,328);yygc++; 
					yyg[yygc] = new YYARec(-15,157);yygc++; 
					yyg[yygc] = new YYARec(-11,158);yygc++; 
					yyg[yygc] = new YYARec(-5,159);yygc++; 
					yyg[yygc] = new YYARec(-68,273);yygc++; 
					yyg[yygc] = new YYARec(-66,277);yygc++; 
					yyg[yygc] = new YYARec(-64,280);yygc++; 
					yyg[yygc] = new YYARec(-62,285);yygc++; 
					yyg[yygc] = new YYARec(-42,176);yygc++; 
					yyg[yygc] = new YYARec(-41,177);yygc++; 
					yyg[yygc] = new YYARec(-40,107);yygc++; 
					yyg[yygc] = new YYARec(-34,108);yygc++; 
					yyg[yygc] = new YYARec(-23,109);yygc++; 
					yyg[yygc] = new YYARec(-20,339);yygc++; 
					yyg[yygc] = new YYARec(-18,179);yygc++;

					yyd = new int[yynstates];
					yyd[0] = 0;  
					yyd[1] = -216;  
					yyd[2] = -215;  
					yyd[3] = 0;  
					yyd[4] = 0;  
					yyd[5] = -63;  
					yyd[6] = 0;  
					yyd[7] = 0;  
					yyd[8] = 0;  
					yyd[9] = 0;  
					yyd[10] = -214;  
					yyd[11] = -43;  
					yyd[12] = -14;  
					yyd[13] = -13;  
					yyd[14] = -12;  
					yyd[15] = -11;  
					yyd[16] = -10;  
					yyd[17] = -9;  
					yyd[18] = -8;  
					yyd[19] = -3;  
					yyd[20] = 0;  
					yyd[21] = -1;  
					yyd[22] = 0;  
					yyd[23] = 0;  
					yyd[24] = -5;  
					yyd[25] = 0;  
					yyd[26] = 0;  
					yyd[27] = 0;  
					yyd[28] = 0;  
					yyd[29] = -6;  
					yyd[30] = -7;  
					yyd[31] = -213;  
					yyd[32] = 0;  
					yyd[33] = 0;  
					yyd[34] = 0;  
					yyd[35] = 0;  
					yyd[36] = 0;  
					yyd[37] = -203;  
					yyd[38] = -200;  
					yyd[39] = -205;  
					yyd[40] = -198;  
					yyd[41] = 0;  
					yyd[42] = -212;  
					yyd[43] = 0;  
					yyd[44] = 0;  
					yyd[45] = 0;  
					yyd[46] = -195;  
					yyd[47] = 0;  
					yyd[48] = -210;  
					yyd[49] = 0;  
					yyd[50] = -191;  
					yyd[51] = 0;  
					yyd[52] = -192;  
					yyd[53] = -208;  
					yyd[54] = -186;  
					yyd[55] = 0;  
					yyd[56] = -221;  
					yyd[57] = -219;  
					yyd[58] = 0;  
					yyd[59] = -199;  
					yyd[60] = -202;  
					yyd[61] = -197;  
					yyd[62] = -204;  
					yyd[63] = -201;  
					yyd[64] = -211;  
					yyd[65] = -194;  
					yyd[66] = -193;  
					yyd[67] = -196;  
					yyd[68] = -207;  
					yyd[69] = -209;  
					yyd[70] = -206;  
					yyd[71] = 0;  
					yyd[72] = -220;  
					yyd[73] = 0;  
					yyd[74] = -223;  
					yyd[75] = 0;  
					yyd[76] = -222;  
					yyd[77] = -178;  
					yyd[78] = 0;  
					yyd[79] = -179;  
					yyd[80] = 0;  
					yyd[81] = -50;  
					yyd[82] = -51;  
					yyd[83] = -48;  
					yyd[84] = -49;  
					yyd[85] = -137;  
					yyd[86] = -138;  
					yyd[87] = -136;  
					yyd[88] = -183;  
					yyd[89] = -185;  
					yyd[90] = -225;  
					yyd[91] = -226;  
					yyd[92] = 0;  
					yyd[93] = 0;  
					yyd[94] = -2;  
					yyd[95] = -31;  
					yyd[96] = -30;  
					yyd[97] = 0;  
					yyd[98] = -190;  
					yyd[99] = -188;  
					yyd[100] = -189;  
					yyd[101] = 0;  
					yyd[102] = 0;  
					yyd[103] = 0;  
					yyd[104] = 0;  
					yyd[105] = -153;  
					yyd[106] = -154;  
					yyd[107] = -171;  
					yyd[108] = -172;  
					yyd[109] = -170;  
					yyd[110] = -57;  
					yyd[111] = -61;  
					yyd[112] = -162;  
					yyd[113] = -169;  
					yyd[114] = -165;  
					yyd[115] = -164;  
					yyd[116] = -159;  
					yyd[117] = -167;  
					yyd[118] = -161;  
					yyd[119] = -168;  
					yyd[120] = -158;  
					yyd[121] = -166;  
					yyd[122] = -163;  
					yyd[123] = -160;  
					yyd[124] = 0;  
					yyd[125] = 0;  
					yyd[126] = 0;  
					yyd[127] = -60;  
					yyd[128] = 0;  
					yyd[129] = 0;  
					yyd[130] = -182;  
					yyd[131] = -184;  
					yyd[132] = -39;  
					yyd[133] = -47;  
					yyd[134] = -38;  
					yyd[135] = 0;  
					yyd[136] = 0;  
					yyd[137] = 0;  
					yyd[138] = -36;  
					yyd[139] = -35;  
					yyd[140] = -34;  
					yyd[141] = -33;  
					yyd[142] = -32;  
					yyd[143] = 0;  
					yyd[144] = -156;  
					yyd[145] = -28;  
					yyd[146] = -187;  
					yyd[147] = -29;  
					yyd[148] = -37;  
					yyd[149] = 0;  
					yyd[150] = 0;  
					yyd[151] = 0;  
					yyd[152] = 0;  
					yyd[153] = 0;  
					yyd[154] = 0;  
					yyd[155] = 0;  
					yyd[156] = 0;  
					yyd[157] = 0;  
					yyd[158] = -89;  
					yyd[159] = 0;  
					yyd[160] = 0;  
					yyd[161] = -88;  
					yyd[162] = 0;  
					yyd[163] = 0;  
					yyd[164] = 0;  
					yyd[165] = 0;  
					yyd[166] = 0;  
					yyd[167] = 0;  
					yyd[168] = 0;  
					yyd[169] = 0;  
					yyd[170] = 0;  
					yyd[171] = 0;  
					yyd[172] = 0;  
					yyd[173] = -218;  
					yyd[174] = -217;  
					yyd[175] = -59;  
					yyd[176] = 0;  
					yyd[177] = 0;  
					yyd[178] = 0;  
					yyd[179] = 0;  
					yyd[180] = 0;  
					yyd[181] = -68;  
					yyd[182] = 0;  
					yyd[183] = 0;  
					yyd[184] = -56;  
					yyd[185] = 0;  
					yyd[186] = 0;  
					yyd[187] = -55;  
					yyd[188] = 0;  
					yyd[189] = 0;  
					yyd[190] = 0;  
					yyd[191] = -157;  
					yyd[192] = -27;  
					yyd[193] = -86;  
					yyd[194] = 0;  
					yyd[195] = 0;  
					yyd[196] = -92;  
					yyd[197] = -95;  
					yyd[198] = -96;  
					yyd[199] = -93;  
					yyd[200] = -94;  
					yyd[201] = -87;  
					yyd[202] = -82;  
					yyd[203] = -80;  
					yyd[204] = 0;  
					yyd[205] = 0;  
					yyd[206] = -75;  
					yyd[207] = -81;  
					yyd[208] = 0;  
					yyd[209] = -142;  
					yyd[210] = -143;  
					yyd[211] = -144;  
					yyd[212] = -145;  
					yyd[213] = -146;  
					yyd[214] = 0;  
					yyd[215] = 0;  
					yyd[216] = 0;  
					yyd[217] = -122;  
					yyd[218] = 0;  
					yyd[219] = 0;  
					yyd[220] = -116;  
					yyd[221] = -114;  
					yyd[222] = 0;  
					yyd[223] = 0;  
					yyd[224] = 0;  
					yyd[225] = 0;  
					yyd[226] = 0;  
					yyd[227] = 0;  
					yyd[228] = 0;  
					yyd[229] = 0;  
					yyd[230] = 0;  
					yyd[231] = -139;  
					yyd[232] = -124;  
					yyd[233] = 0;  
					yyd[234] = 0;  
					yyd[235] = 0;  
					yyd[236] = 0;  
					yyd[237] = 0;  
					yyd[238] = 0;  
					yyd[239] = -180;  
					yyd[240] = 0;  
					yyd[241] = 0;  
					yyd[242] = 0;  
					yyd[243] = 0;  
					yyd[244] = 0;  
					yyd[245] = 0;  
					yyd[246] = 0;  
					yyd[247] = -72;  
					yyd[248] = -73;  
					yyd[249] = -74;  
					yyd[250] = -71;  
					yyd[251] = -64;  
					yyd[252] = -58;  
					yyd[253] = -65;  
					yyd[254] = 0;  
					yyd[255] = 0;  
					yyd[256] = -53;  
					yyd[257] = -52;  
					yyd[258] = -15;  
					yyd[259] = 0;  
					yyd[260] = -18;  
					yyd[261] = -16;  
					yyd[262] = -91;  
					yyd[263] = -84;  
					yyd[264] = -79;  
					yyd[265] = -85;  
					yyd[266] = -141;  
					yyd[267] = 0;  
					yyd[268] = -19;  
					yyd[269] = -20;  
					yyd[270] = 0;  
					yyd[271] = 0;  
					yyd[272] = -117;  
					yyd[273] = 0;  
					yyd[274] = -133;  
					yyd[275] = -134;  
					yyd[276] = -135;  
					yyd[277] = 0;  
					yyd[278] = -131;  
					yyd[279] = -132;  
					yyd[280] = 0;  
					yyd[281] = -127;  
					yyd[282] = -128;  
					yyd[283] = -129;  
					yyd[284] = -130;  
					yyd[285] = 0;  
					yyd[286] = -125;  
					yyd[287] = -126;  
					yyd[288] = 0;  
					yyd[289] = 0;  
					yyd[290] = 0;  
					yyd[291] = 0;  
					yyd[292] = 0;  
					yyd[293] = 0;  
					yyd[294] = -121;  
					yyd[295] = 0;  
					yyd[296] = -118;  
					yyd[297] = 0;  
					yyd[298] = 0;  
					yyd[299] = 0;  
					yyd[300] = -77;  
					yyd[301] = -70;  
					yyd[302] = -67;  
					yyd[303] = 0;  
					yyd[304] = 0;  
					yyd[305] = 0;  
					yyd[306] = 0;  
					yyd[307] = -22;  
					yyd[308] = -78;  
					yyd[309] = 0;  
					yyd[310] = -115;  
					yyd[311] = 0;  
					yyd[312] = 0;  
					yyd[313] = 0;  
					yyd[314] = 0;  
					yyd[315] = 0;  
					yyd[316] = 0;  
					yyd[317] = 0;  
					yyd[318] = 0;  
					yyd[319] = -140;  
					yyd[320] = -120;  
					yyd[321] = -150;  
					yyd[322] = 0;  
					yyd[323] = 0;  
					yyd[324] = 0;  
					yyd[325] = 0;  
					yyd[326] = 0;  
					yyd[327] = 0;  
					yyd[328] = 0;  
					yyd[329] = -119;  
					yyd[330] = -151;  
					yyd[331] = -152;  
					yyd[332] = 0;  
					yyd[333] = -26;  
					yyd[334] = -23;  
					yyd[335] = -24;  
					yyd[336] = -17;  
					yyd[337] = -21;  
					yyd[338] = 0;  
					yyd[339] = 0;  
					yyd[340] = -25; 

					yyal = new int[yynstates];
					yyal[0] = 1;  
					yyal[1] = 34;  
					yyal[2] = 34;  
					yyal[3] = 34;  
					yyal[4] = 92;  
					yyal[5] = 116;  
					yyal[6] = 116;  
					yyal[7] = 140;  
					yyal[8] = 165;  
					yyal[9] = 173;  
					yyal[10] = 197;  
					yyal[11] = 197;  
					yyal[12] = 197;  
					yyal[13] = 197;  
					yyal[14] = 197;  
					yyal[15] = 197;  
					yyal[16] = 197;  
					yyal[17] = 197;  
					yyal[18] = 197;  
					yyal[19] = 197;  
					yyal[20] = 197;  
					yyal[21] = 232;  
					yyal[22] = 232;  
					yyal[23] = 233;  
					yyal[24] = 237;  
					yyal[25] = 237;  
					yyal[26] = 241;  
					yyal[27] = 245;  
					yyal[28] = 249;  
					yyal[29] = 250;  
					yyal[30] = 250;  
					yyal[31] = 250;  
					yyal[32] = 250;  
					yyal[33] = 278;  
					yyal[34] = 290;  
					yyal[35] = 319;  
					yyal[36] = 347;  
					yyal[37] = 375;  
					yyal[38] = 375;  
					yyal[39] = 375;  
					yyal[40] = 375;  
					yyal[41] = 375;  
					yyal[42] = 387;  
					yyal[43] = 387;  
					yyal[44] = 399;  
					yyal[45] = 427;  
					yyal[46] = 455;  
					yyal[47] = 455;  
					yyal[48] = 467;  
					yyal[49] = 467;  
					yyal[50] = 479;  
					yyal[51] = 479;  
					yyal[52] = 491;  
					yyal[53] = 491;  
					yyal[54] = 491;  
					yyal[55] = 491;  
					yyal[56] = 506;  
					yyal[57] = 506;  
					yyal[58] = 506;  
					yyal[59] = 507;  
					yyal[60] = 507;  
					yyal[61] = 507;  
					yyal[62] = 507;  
					yyal[63] = 507;  
					yyal[64] = 507;  
					yyal[65] = 507;  
					yyal[66] = 507;  
					yyal[67] = 507;  
					yyal[68] = 507;  
					yyal[69] = 507;  
					yyal[70] = 507;  
					yyal[71] = 507;  
					yyal[72] = 536;  
					yyal[73] = 536;  
					yyal[74] = 539;  
					yyal[75] = 539;  
					yyal[76] = 540;  
					yyal[77] = 540;  
					yyal[78] = 540;  
					yyal[79] = 542;  
					yyal[80] = 542;  
					yyal[81] = 543;  
					yyal[82] = 543;  
					yyal[83] = 543;  
					yyal[84] = 543;  
					yyal[85] = 543;  
					yyal[86] = 543;  
					yyal[87] = 543;  
					yyal[88] = 543;  
					yyal[89] = 543;  
					yyal[90] = 543;  
					yyal[91] = 543;  
					yyal[92] = 543;  
					yyal[93] = 568;  
					yyal[94] = 569;  
					yyal[95] = 569;  
					yyal[96] = 569;  
					yyal[97] = 569;  
					yyal[98] = 570;  
					yyal[99] = 570;  
					yyal[100] = 570;  
					yyal[101] = 570;  
					yyal[102] = 571;  
					yyal[103] = 604;  
					yyal[104] = 605;  
					yyal[105] = 606;  
					yyal[106] = 606;  
					yyal[107] = 606;  
					yyal[108] = 606;  
					yyal[109] = 606;  
					yyal[110] = 606;  
					yyal[111] = 606;  
					yyal[112] = 606;  
					yyal[113] = 606;  
					yyal[114] = 606;  
					yyal[115] = 606;  
					yyal[116] = 606;  
					yyal[117] = 606;  
					yyal[118] = 606;  
					yyal[119] = 606;  
					yyal[120] = 606;  
					yyal[121] = 606;  
					yyal[122] = 606;  
					yyal[123] = 606;  
					yyal[124] = 606;  
					yyal[125] = 641;  
					yyal[126] = 643;  
					yyal[127] = 644;  
					yyal[128] = 644;  
					yyal[129] = 658;  
					yyal[130] = 664;  
					yyal[131] = 664;  
					yyal[132] = 664;  
					yyal[133] = 664;  
					yyal[134] = 664;  
					yyal[135] = 664;  
					yyal[136] = 698;  
					yyal[137] = 732;  
					yyal[138] = 758;  
					yyal[139] = 758;  
					yyal[140] = 758;  
					yyal[141] = 758;  
					yyal[142] = 758;  
					yyal[143] = 758;  
					yyal[144] = 759;  
					yyal[145] = 759;  
					yyal[146] = 759;  
					yyal[147] = 759;  
					yyal[148] = 759;  
					yyal[149] = 759;  
					yyal[150] = 766;  
					yyal[151] = 767;  
					yyal[152] = 800;  
					yyal[153] = 837;  
					yyal[154] = 874;  
					yyal[155] = 875;  
					yyal[156] = 914;  
					yyal[157] = 915;  
					yyal[158] = 952;  
					yyal[159] = 952;  
					yyal[160] = 957;  
					yyal[161] = 961;  
					yyal[162] = 961;  
					yyal[163] = 965;  
					yyal[164] = 1000;  
					yyal[165] = 1031;  
					yyal[166] = 1065;  
					yyal[167] = 1096;  
					yyal[168] = 1127;  
					yyal[169] = 1167;  
					yyal[170] = 1207;  
					yyal[171] = 1247;  
					yyal[172] = 1287;  
					yyal[173] = 1327;  
					yyal[174] = 1327;  
					yyal[175] = 1327;  
					yyal[176] = 1327;  
					yyal[177] = 1358;  
					yyal[178] = 1374;  
					yyal[179] = 1375;  
					yyal[180] = 1391;  
					yyal[181] = 1395;  
					yyal[182] = 1395;  
					yyal[183] = 1399;  
					yyal[184] = 1400;  
					yyal[185] = 1400;  
					yyal[186] = 1406;  
					yyal[187] = 1407;  
					yyal[188] = 1407;  
					yyal[189] = 1408;  
					yyal[190] = 1410;  
					yyal[191] = 1411;  
					yyal[192] = 1411;  
					yyal[193] = 1411;  
					yyal[194] = 1411;  
					yyal[195] = 1444;  
					yyal[196] = 1445;  
					yyal[197] = 1445;  
					yyal[198] = 1445;  
					yyal[199] = 1445;  
					yyal[200] = 1445;  
					yyal[201] = 1445;  
					yyal[202] = 1445;  
					yyal[203] = 1445;  
					yyal[204] = 1445;  
					yyal[205] = 1482;  
					yyal[206] = 1483;  
					yyal[207] = 1483;  
					yyal[208] = 1483;  
					yyal[209] = 1514;  
					yyal[210] = 1514;  
					yyal[211] = 1514;  
					yyal[212] = 1514;  
					yyal[213] = 1514;  
					yyal[214] = 1514;  
					yyal[215] = 1550;  
					yyal[216] = 1586;  
					yyal[217] = 1587;  
					yyal[218] = 1587;  
					yyal[219] = 1588;  
					yyal[220] = 1619;  
					yyal[221] = 1619;  
					yyal[222] = 1619;  
					yyal[223] = 1638;  
					yyal[224] = 1654;  
					yyal[225] = 1668;  
					yyal[226] = 1678;  
					yyal[227] = 1686;  
					yyal[228] = 1693;  
					yyal[229] = 1699;  
					yyal[230] = 1704;  
					yyal[231] = 1708;  
					yyal[232] = 1708;  
					yyal[233] = 1708;  
					yyal[234] = 1731;  
					yyal[235] = 1762;  
					yyal[236] = 1789;  
					yyal[237] = 1816;  
					yyal[238] = 1843;  
					yyal[239] = 1863;  
					yyal[240] = 1863;  
					yyal[241] = 1898;  
					yyal[242] = 1899;  
					yyal[243] = 1919;  
					yyal[244] = 1920;  
					yyal[245] = 1957;  
					yyal[246] = 1989;  
					yyal[247] = 1990;  
					yyal[248] = 1990;  
					yyal[249] = 1990;  
					yyal[250] = 1990;  
					yyal[251] = 1990;  
					yyal[252] = 1990;  
					yyal[253] = 1990;  
					yyal[254] = 1990;  
					yyal[255] = 1991;  
					yyal[256] = 1992;  
					yyal[257] = 1992;  
					yyal[258] = 1992;  
					yyal[259] = 1992;  
					yyal[260] = 1993;  
					yyal[261] = 1993;  
					yyal[262] = 1993;  
					yyal[263] = 1993;  
					yyal[264] = 1993;  
					yyal[265] = 1993;  
					yyal[266] = 1993;  
					yyal[267] = 1993;  
					yyal[268] = 1995;  
					yyal[269] = 1995;  
					yyal[270] = 1995;  
					yyal[271] = 2032;  
					yyal[272] = 2063;  
					yyal[273] = 2063;  
					yyal[274] = 2094;  
					yyal[275] = 2094;  
					yyal[276] = 2094;  
					yyal[277] = 2094;  
					yyal[278] = 2125;  
					yyal[279] = 2125;  
					yyal[280] = 2125;  
					yyal[281] = 2156;  
					yyal[282] = 2156;  
					yyal[283] = 2156;  
					yyal[284] = 2156;  
					yyal[285] = 2156;  
					yyal[286] = 2187;  
					yyal[287] = 2187;  
					yyal[288] = 2187;  
					yyal[289] = 2218;  
					yyal[290] = 2249;  
					yyal[291] = 2280;  
					yyal[292] = 2311;  
					yyal[293] = 2342;  
					yyal[294] = 2373;  
					yyal[295] = 2373;  
					yyal[296] = 2374;  
					yyal[297] = 2374;  
					yyal[298] = 2375;  
					yyal[299] = 2410;  
					yyal[300] = 2445;  
					yyal[301] = 2445;  
					yyal[302] = 2445;  
					yyal[303] = 2445;  
					yyal[304] = 2460;  
					yyal[305] = 2475;  
					yyal[306] = 2508;  
					yyal[307] = 2543;  
					yyal[308] = 2543;  
					yyal[309] = 2543;  
					yyal[310] = 2544;  
					yyal[311] = 2544;  
					yyal[312] = 2563;  
					yyal[313] = 2579;  
					yyal[314] = 2593;  
					yyal[315] = 2603;  
					yyal[316] = 2611;  
					yyal[317] = 2618;  
					yyal[318] = 2624;  
					yyal[319] = 2629;  
					yyal[320] = 2629;  
					yyal[321] = 2629;  
					yyal[322] = 2629;  
					yyal[323] = 2630;  
					yyal[324] = 2631;  
					yyal[325] = 2633;  
					yyal[326] = 2634;  
					yyal[327] = 2635;  
					yyal[328] = 2636;  
					yyal[329] = 2637;  
					yyal[330] = 2637;  
					yyal[331] = 2637;  
					yyal[332] = 2637;  
					yyal[333] = 2638;  
					yyal[334] = 2638;  
					yyal[335] = 2638;  
					yyal[336] = 2638;  
					yyal[337] = 2638;  
					yyal[338] = 2638;  
					yyal[339] = 2652;  
					yyal[340] = 2653; 

					yyah = new int[yynstates];
					yyah[0] = 33;  
					yyah[1] = 33;  
					yyah[2] = 33;  
					yyah[3] = 91;  
					yyah[4] = 115;  
					yyah[5] = 115;  
					yyah[6] = 139;  
					yyah[7] = 164;  
					yyah[8] = 172;  
					yyah[9] = 196;  
					yyah[10] = 196;  
					yyah[11] = 196;  
					yyah[12] = 196;  
					yyah[13] = 196;  
					yyah[14] = 196;  
					yyah[15] = 196;  
					yyah[16] = 196;  
					yyah[17] = 196;  
					yyah[18] = 196;  
					yyah[19] = 196;  
					yyah[20] = 231;  
					yyah[21] = 231;  
					yyah[22] = 232;  
					yyah[23] = 236;  
					yyah[24] = 236;  
					yyah[25] = 240;  
					yyah[26] = 244;  
					yyah[27] = 248;  
					yyah[28] = 249;  
					yyah[29] = 249;  
					yyah[30] = 249;  
					yyah[31] = 249;  
					yyah[32] = 277;  
					yyah[33] = 289;  
					yyah[34] = 318;  
					yyah[35] = 346;  
					yyah[36] = 374;  
					yyah[37] = 374;  
					yyah[38] = 374;  
					yyah[39] = 374;  
					yyah[40] = 374;  
					yyah[41] = 386;  
					yyah[42] = 386;  
					yyah[43] = 398;  
					yyah[44] = 426;  
					yyah[45] = 454;  
					yyah[46] = 454;  
					yyah[47] = 466;  
					yyah[48] = 466;  
					yyah[49] = 478;  
					yyah[50] = 478;  
					yyah[51] = 490;  
					yyah[52] = 490;  
					yyah[53] = 490;  
					yyah[54] = 490;  
					yyah[55] = 505;  
					yyah[56] = 505;  
					yyah[57] = 505;  
					yyah[58] = 506;  
					yyah[59] = 506;  
					yyah[60] = 506;  
					yyah[61] = 506;  
					yyah[62] = 506;  
					yyah[63] = 506;  
					yyah[64] = 506;  
					yyah[65] = 506;  
					yyah[66] = 506;  
					yyah[67] = 506;  
					yyah[68] = 506;  
					yyah[69] = 506;  
					yyah[70] = 506;  
					yyah[71] = 535;  
					yyah[72] = 535;  
					yyah[73] = 538;  
					yyah[74] = 538;  
					yyah[75] = 539;  
					yyah[76] = 539;  
					yyah[77] = 539;  
					yyah[78] = 541;  
					yyah[79] = 541;  
					yyah[80] = 542;  
					yyah[81] = 542;  
					yyah[82] = 542;  
					yyah[83] = 542;  
					yyah[84] = 542;  
					yyah[85] = 542;  
					yyah[86] = 542;  
					yyah[87] = 542;  
					yyah[88] = 542;  
					yyah[89] = 542;  
					yyah[90] = 542;  
					yyah[91] = 542;  
					yyah[92] = 567;  
					yyah[93] = 568;  
					yyah[94] = 568;  
					yyah[95] = 568;  
					yyah[96] = 568;  
					yyah[97] = 569;  
					yyah[98] = 569;  
					yyah[99] = 569;  
					yyah[100] = 569;  
					yyah[101] = 570;  
					yyah[102] = 603;  
					yyah[103] = 604;  
					yyah[104] = 605;  
					yyah[105] = 605;  
					yyah[106] = 605;  
					yyah[107] = 605;  
					yyah[108] = 605;  
					yyah[109] = 605;  
					yyah[110] = 605;  
					yyah[111] = 605;  
					yyah[112] = 605;  
					yyah[113] = 605;  
					yyah[114] = 605;  
					yyah[115] = 605;  
					yyah[116] = 605;  
					yyah[117] = 605;  
					yyah[118] = 605;  
					yyah[119] = 605;  
					yyah[120] = 605;  
					yyah[121] = 605;  
					yyah[122] = 605;  
					yyah[123] = 605;  
					yyah[124] = 640;  
					yyah[125] = 642;  
					yyah[126] = 643;  
					yyah[127] = 643;  
					yyah[128] = 657;  
					yyah[129] = 663;  
					yyah[130] = 663;  
					yyah[131] = 663;  
					yyah[132] = 663;  
					yyah[133] = 663;  
					yyah[134] = 663;  
					yyah[135] = 697;  
					yyah[136] = 731;  
					yyah[137] = 757;  
					yyah[138] = 757;  
					yyah[139] = 757;  
					yyah[140] = 757;  
					yyah[141] = 757;  
					yyah[142] = 757;  
					yyah[143] = 758;  
					yyah[144] = 758;  
					yyah[145] = 758;  
					yyah[146] = 758;  
					yyah[147] = 758;  
					yyah[148] = 758;  
					yyah[149] = 765;  
					yyah[150] = 766;  
					yyah[151] = 799;  
					yyah[152] = 836;  
					yyah[153] = 873;  
					yyah[154] = 874;  
					yyah[155] = 913;  
					yyah[156] = 914;  
					yyah[157] = 951;  
					yyah[158] = 951;  
					yyah[159] = 956;  
					yyah[160] = 960;  
					yyah[161] = 960;  
					yyah[162] = 964;  
					yyah[163] = 999;  
					yyah[164] = 1030;  
					yyah[165] = 1064;  
					yyah[166] = 1095;  
					yyah[167] = 1126;  
					yyah[168] = 1166;  
					yyah[169] = 1206;  
					yyah[170] = 1246;  
					yyah[171] = 1286;  
					yyah[172] = 1326;  
					yyah[173] = 1326;  
					yyah[174] = 1326;  
					yyah[175] = 1326;  
					yyah[176] = 1357;  
					yyah[177] = 1373;  
					yyah[178] = 1374;  
					yyah[179] = 1390;  
					yyah[180] = 1394;  
					yyah[181] = 1394;  
					yyah[182] = 1398;  
					yyah[183] = 1399;  
					yyah[184] = 1399;  
					yyah[185] = 1405;  
					yyah[186] = 1406;  
					yyah[187] = 1406;  
					yyah[188] = 1407;  
					yyah[189] = 1409;  
					yyah[190] = 1410;  
					yyah[191] = 1410;  
					yyah[192] = 1410;  
					yyah[193] = 1410;  
					yyah[194] = 1443;  
					yyah[195] = 1444;  
					yyah[196] = 1444;  
					yyah[197] = 1444;  
					yyah[198] = 1444;  
					yyah[199] = 1444;  
					yyah[200] = 1444;  
					yyah[201] = 1444;  
					yyah[202] = 1444;  
					yyah[203] = 1444;  
					yyah[204] = 1481;  
					yyah[205] = 1482;  
					yyah[206] = 1482;  
					yyah[207] = 1482;  
					yyah[208] = 1513;  
					yyah[209] = 1513;  
					yyah[210] = 1513;  
					yyah[211] = 1513;  
					yyah[212] = 1513;  
					yyah[213] = 1513;  
					yyah[214] = 1549;  
					yyah[215] = 1585;  
					yyah[216] = 1586;  
					yyah[217] = 1586;  
					yyah[218] = 1587;  
					yyah[219] = 1618;  
					yyah[220] = 1618;  
					yyah[221] = 1618;  
					yyah[222] = 1637;  
					yyah[223] = 1653;  
					yyah[224] = 1667;  
					yyah[225] = 1677;  
					yyah[226] = 1685;  
					yyah[227] = 1692;  
					yyah[228] = 1698;  
					yyah[229] = 1703;  
					yyah[230] = 1707;  
					yyah[231] = 1707;  
					yyah[232] = 1707;  
					yyah[233] = 1730;  
					yyah[234] = 1761;  
					yyah[235] = 1788;  
					yyah[236] = 1815;  
					yyah[237] = 1842;  
					yyah[238] = 1862;  
					yyah[239] = 1862;  
					yyah[240] = 1897;  
					yyah[241] = 1898;  
					yyah[242] = 1918;  
					yyah[243] = 1919;  
					yyah[244] = 1956;  
					yyah[245] = 1988;  
					yyah[246] = 1989;  
					yyah[247] = 1989;  
					yyah[248] = 1989;  
					yyah[249] = 1989;  
					yyah[250] = 1989;  
					yyah[251] = 1989;  
					yyah[252] = 1989;  
					yyah[253] = 1989;  
					yyah[254] = 1990;  
					yyah[255] = 1991;  
					yyah[256] = 1991;  
					yyah[257] = 1991;  
					yyah[258] = 1991;  
					yyah[259] = 1992;  
					yyah[260] = 1992;  
					yyah[261] = 1992;  
					yyah[262] = 1992;  
					yyah[263] = 1992;  
					yyah[264] = 1992;  
					yyah[265] = 1992;  
					yyah[266] = 1992;  
					yyah[267] = 1994;  
					yyah[268] = 1994;  
					yyah[269] = 1994;  
					yyah[270] = 2031;  
					yyah[271] = 2062;  
					yyah[272] = 2062;  
					yyah[273] = 2093;  
					yyah[274] = 2093;  
					yyah[275] = 2093;  
					yyah[276] = 2093;  
					yyah[277] = 2124;  
					yyah[278] = 2124;  
					yyah[279] = 2124;  
					yyah[280] = 2155;  
					yyah[281] = 2155;  
					yyah[282] = 2155;  
					yyah[283] = 2155;  
					yyah[284] = 2155;  
					yyah[285] = 2186;  
					yyah[286] = 2186;  
					yyah[287] = 2186;  
					yyah[288] = 2217;  
					yyah[289] = 2248;  
					yyah[290] = 2279;  
					yyah[291] = 2310;  
					yyah[292] = 2341;  
					yyah[293] = 2372;  
					yyah[294] = 2372;  
					yyah[295] = 2373;  
					yyah[296] = 2373;  
					yyah[297] = 2374;  
					yyah[298] = 2409;  
					yyah[299] = 2444;  
					yyah[300] = 2444;  
					yyah[301] = 2444;  
					yyah[302] = 2444;  
					yyah[303] = 2459;  
					yyah[304] = 2474;  
					yyah[305] = 2507;  
					yyah[306] = 2542;  
					yyah[307] = 2542;  
					yyah[308] = 2542;  
					yyah[309] = 2543;  
					yyah[310] = 2543;  
					yyah[311] = 2562;  
					yyah[312] = 2578;  
					yyah[313] = 2592;  
					yyah[314] = 2602;  
					yyah[315] = 2610;  
					yyah[316] = 2617;  
					yyah[317] = 2623;  
					yyah[318] = 2628;  
					yyah[319] = 2628;  
					yyah[320] = 2628;  
					yyah[321] = 2628;  
					yyah[322] = 2629;  
					yyah[323] = 2630;  
					yyah[324] = 2632;  
					yyah[325] = 2633;  
					yyah[326] = 2634;  
					yyah[327] = 2635;  
					yyah[328] = 2636;  
					yyah[329] = 2636;  
					yyah[330] = 2636;  
					yyah[331] = 2636;  
					yyah[332] = 2637;  
					yyah[333] = 2637;  
					yyah[334] = 2637;  
					yyah[335] = 2637;  
					yyah[336] = 2637;  
					yyah[337] = 2637;  
					yyah[338] = 2651;  
					yyah[339] = 2652;  
					yyah[340] = 2652; 

					yygl = new int[yynstates];
					yygl[0] = 1;  
					yygl[1] = 23;  
					yygl[2] = 23;  
					yygl[3] = 23;  
					yygl[4] = 23;  
					yygl[5] = 29;  
					yygl[6] = 29;  
					yygl[7] = 35;  
					yygl[8] = 41;  
					yygl[9] = 49;  
					yygl[10] = 56;  
					yygl[11] = 56;  
					yygl[12] = 56;  
					yygl[13] = 56;  
					yygl[14] = 56;  
					yygl[15] = 56;  
					yygl[16] = 56;  
					yygl[17] = 56;  
					yygl[18] = 56;  
					yygl[19] = 56;  
					yygl[20] = 56;  
					yygl[21] = 77;  
					yygl[22] = 77;  
					yygl[23] = 77;  
					yygl[24] = 80;  
					yygl[25] = 80;  
					yygl[26] = 83;  
					yygl[27] = 86;  
					yygl[28] = 89;  
					yygl[29] = 90;  
					yygl[30] = 90;  
					yygl[31] = 90;  
					yygl[32] = 90;  
					yygl[33] = 90;  
					yygl[34] = 90;  
					yygl[35] = 90;  
					yygl[36] = 90;  
					yygl[37] = 90;  
					yygl[38] = 90;  
					yygl[39] = 90;  
					yygl[40] = 90;  
					yygl[41] = 90;  
					yygl[42] = 90;  
					yygl[43] = 90;  
					yygl[44] = 90;  
					yygl[45] = 90;  
					yygl[46] = 90;  
					yygl[47] = 90;  
					yygl[48] = 90;  
					yygl[49] = 90;  
					yygl[50] = 90;  
					yygl[51] = 90;  
					yygl[52] = 90;  
					yygl[53] = 90;  
					yygl[54] = 90;  
					yygl[55] = 90;  
					yygl[56] = 95;  
					yygl[57] = 95;  
					yygl[58] = 95;  
					yygl[59] = 95;  
					yygl[60] = 95;  
					yygl[61] = 95;  
					yygl[62] = 95;  
					yygl[63] = 95;  
					yygl[64] = 95;  
					yygl[65] = 95;  
					yygl[66] = 95;  
					yygl[67] = 95;  
					yygl[68] = 95;  
					yygl[69] = 95;  
					yygl[70] = 95;  
					yygl[71] = 95;  
					yygl[72] = 95;  
					yygl[73] = 95;  
					yygl[74] = 96;  
					yygl[75] = 96;  
					yygl[76] = 97;  
					yygl[77] = 97;  
					yygl[78] = 97;  
					yygl[79] = 97;  
					yygl[80] = 97;  
					yygl[81] = 97;  
					yygl[82] = 97;  
					yygl[83] = 97;  
					yygl[84] = 97;  
					yygl[85] = 97;  
					yygl[86] = 97;  
					yygl[87] = 97;  
					yygl[88] = 97;  
					yygl[89] = 97;  
					yygl[90] = 97;  
					yygl[91] = 97;  
					yygl[92] = 97;  
					yygl[93] = 104;  
					yygl[94] = 104;  
					yygl[95] = 104;  
					yygl[96] = 104;  
					yygl[97] = 104;  
					yygl[98] = 104;  
					yygl[99] = 104;  
					yygl[100] = 104;  
					yygl[101] = 104;  
					yygl[102] = 104;  
					yygl[103] = 118;  
					yygl[104] = 118;  
					yygl[105] = 118;  
					yygl[106] = 118;  
					yygl[107] = 118;  
					yygl[108] = 118;  
					yygl[109] = 118;  
					yygl[110] = 118;  
					yygl[111] = 118;  
					yygl[112] = 118;  
					yygl[113] = 118;  
					yygl[114] = 118;  
					yygl[115] = 118;  
					yygl[116] = 118;  
					yygl[117] = 118;  
					yygl[118] = 118;  
					yygl[119] = 118;  
					yygl[120] = 118;  
					yygl[121] = 118;  
					yygl[122] = 118;  
					yygl[123] = 118;  
					yygl[124] = 118;  
					yygl[125] = 131;  
					yygl[126] = 131;  
					yygl[127] = 131;  
					yygl[128] = 131;  
					yygl[129] = 138;  
					yygl[130] = 143;  
					yygl[131] = 143;  
					yygl[132] = 143;  
					yygl[133] = 143;  
					yygl[134] = 143;  
					yygl[135] = 143;  
					yygl[136] = 165;  
					yygl[137] = 187;  
					yygl[138] = 192;  
					yygl[139] = 192;  
					yygl[140] = 192;  
					yygl[141] = 192;  
					yygl[142] = 192;  
					yygl[143] = 192;  
					yygl[144] = 192;  
					yygl[145] = 192;  
					yygl[146] = 192;  
					yygl[147] = 192;  
					yygl[148] = 192;  
					yygl[149] = 192;  
					yygl[150] = 192;  
					yygl[151] = 192;  
					yygl[152] = 207;  
					yygl[153] = 220;  
					yygl[154] = 233;  
					yygl[155] = 233;  
					yygl[156] = 248;  
					yygl[157] = 248;  
					yygl[158] = 261;  
					yygl[159] = 261;  
					yygl[160] = 262;  
					yygl[161] = 265;  
					yygl[162] = 265;  
					yygl[163] = 268;  
					yygl[164] = 281;  
					yygl[165] = 302;  
					yygl[166] = 302;  
					yygl[167] = 323;  
					yygl[168] = 344;  
					yygl[169] = 344;  
					yygl[170] = 344;  
					yygl[171] = 344;  
					yygl[172] = 344;  
					yygl[173] = 344;  
					yygl[174] = 344;  
					yygl[175] = 344;  
					yygl[176] = 344;  
					yygl[177] = 358;  
					yygl[178] = 365;  
					yygl[179] = 365;  
					yygl[180] = 372;  
					yygl[181] = 375;  
					yygl[182] = 375;  
					yygl[183] = 378;  
					yygl[184] = 378;  
					yygl[185] = 378;  
					yygl[186] = 383;  
					yygl[187] = 383;  
					yygl[188] = 383;  
					yygl[189] = 383;  
					yygl[190] = 383;  
					yygl[191] = 383;  
					yygl[192] = 383;  
					yygl[193] = 383;  
					yygl[194] = 383;  
					yygl[195] = 398;  
					yygl[196] = 398;  
					yygl[197] = 398;  
					yygl[198] = 398;  
					yygl[199] = 398;  
					yygl[200] = 398;  
					yygl[201] = 398;  
					yygl[202] = 398;  
					yygl[203] = 398;  
					yygl[204] = 398;  
					yygl[205] = 411;  
					yygl[206] = 411;  
					yygl[207] = 411;  
					yygl[208] = 411;  
					yygl[209] = 432;  
					yygl[210] = 432;  
					yygl[211] = 432;  
					yygl[212] = 432;  
					yygl[213] = 432;  
					yygl[214] = 432;  
					yygl[215] = 446;  
					yygl[216] = 460;  
					yygl[217] = 460;  
					yygl[218] = 460;  
					yygl[219] = 460;  
					yygl[220] = 471;  
					yygl[221] = 471;  
					yygl[222] = 471;  
					yygl[223] = 472;  
					yygl[224] = 473;  
					yygl[225] = 474;  
					yygl[226] = 475;  
					yygl[227] = 475;  
					yygl[228] = 475;  
					yygl[229] = 475;  
					yygl[230] = 475;  
					yygl[231] = 475;  
					yygl[232] = 475;  
					yygl[233] = 475;  
					yygl[234] = 476;  
					yygl[235] = 497;  
					yygl[236] = 497;  
					yygl[237] = 497;  
					yygl[238] = 497;  
					yygl[239] = 497;  
					yygl[240] = 497;  
					yygl[241] = 510;  
					yygl[242] = 510;  
					yygl[243] = 510;  
					yygl[244] = 510;  
					yygl[245] = 523;  
					yygl[246] = 537;  
					yygl[247] = 537;  
					yygl[248] = 537;  
					yygl[249] = 537;  
					yygl[250] = 537;  
					yygl[251] = 537;  
					yygl[252] = 537;  
					yygl[253] = 537;  
					yygl[254] = 537;  
					yygl[255] = 537;  
					yygl[256] = 537;  
					yygl[257] = 537;  
					yygl[258] = 537;  
					yygl[259] = 537;  
					yygl[260] = 537;  
					yygl[261] = 537;  
					yygl[262] = 537;  
					yygl[263] = 537;  
					yygl[264] = 537;  
					yygl[265] = 537;  
					yygl[266] = 537;  
					yygl[267] = 537;  
					yygl[268] = 537;  
					yygl[269] = 537;  
					yygl[270] = 537;  
					yygl[271] = 550;  
					yygl[272] = 571;  
					yygl[273] = 571;  
					yygl[274] = 582;  
					yygl[275] = 582;  
					yygl[276] = 582;  
					yygl[277] = 582;  
					yygl[278] = 594;  
					yygl[279] = 594;  
					yygl[280] = 594;  
					yygl[281] = 607;  
					yygl[282] = 607;  
					yygl[283] = 607;  
					yygl[284] = 607;  
					yygl[285] = 607;  
					yygl[286] = 621;  
					yygl[287] = 621;  
					yygl[288] = 621;  
					yygl[289] = 636;  
					yygl[290] = 652;  
					yygl[291] = 669;  
					yygl[292] = 687;  
					yygl[293] = 706;  
					yygl[294] = 727;  
					yygl[295] = 727;  
					yygl[296] = 727;  
					yygl[297] = 727;  
					yygl[298] = 727;  
					yygl[299] = 740;  
					yygl[300] = 753;  
					yygl[301] = 753;  
					yygl[302] = 753;  
					yygl[303] = 753;  
					yygl[304] = 761;  
					yygl[305] = 769;  
					yygl[306] = 790;  
					yygl[307] = 803;  
					yygl[308] = 803;  
					yygl[309] = 803;  
					yygl[310] = 803;  
					yygl[311] = 803;  
					yygl[312] = 804;  
					yygl[313] = 805;  
					yygl[314] = 806;  
					yygl[315] = 807;  
					yygl[316] = 807;  
					yygl[317] = 807;  
					yygl[318] = 807;  
					yygl[319] = 807;  
					yygl[320] = 807;  
					yygl[321] = 807;  
					yygl[322] = 807;  
					yygl[323] = 807;  
					yygl[324] = 807;  
					yygl[325] = 807;  
					yygl[326] = 807;  
					yygl[327] = 807;  
					yygl[328] = 807;  
					yygl[329] = 807;  
					yygl[330] = 807;  
					yygl[331] = 807;  
					yygl[332] = 807;  
					yygl[333] = 807;  
					yygl[334] = 807;  
					yygl[335] = 807;  
					yygl[336] = 807;  
					yygl[337] = 807;  
					yygl[338] = 807;  
					yygl[339] = 814;  
					yygl[340] = 814; 

					yygh = new int[yynstates];
					yygh[0] = 22;  
					yygh[1] = 22;  
					yygh[2] = 22;  
					yygh[3] = 22;  
					yygh[4] = 28;  
					yygh[5] = 28;  
					yygh[6] = 34;  
					yygh[7] = 40;  
					yygh[8] = 48;  
					yygh[9] = 55;  
					yygh[10] = 55;  
					yygh[11] = 55;  
					yygh[12] = 55;  
					yygh[13] = 55;  
					yygh[14] = 55;  
					yygh[15] = 55;  
					yygh[16] = 55;  
					yygh[17] = 55;  
					yygh[18] = 55;  
					yygh[19] = 55;  
					yygh[20] = 76;  
					yygh[21] = 76;  
					yygh[22] = 76;  
					yygh[23] = 79;  
					yygh[24] = 79;  
					yygh[25] = 82;  
					yygh[26] = 85;  
					yygh[27] = 88;  
					yygh[28] = 89;  
					yygh[29] = 89;  
					yygh[30] = 89;  
					yygh[31] = 89;  
					yygh[32] = 89;  
					yygh[33] = 89;  
					yygh[34] = 89;  
					yygh[35] = 89;  
					yygh[36] = 89;  
					yygh[37] = 89;  
					yygh[38] = 89;  
					yygh[39] = 89;  
					yygh[40] = 89;  
					yygh[41] = 89;  
					yygh[42] = 89;  
					yygh[43] = 89;  
					yygh[44] = 89;  
					yygh[45] = 89;  
					yygh[46] = 89;  
					yygh[47] = 89;  
					yygh[48] = 89;  
					yygh[49] = 89;  
					yygh[50] = 89;  
					yygh[51] = 89;  
					yygh[52] = 89;  
					yygh[53] = 89;  
					yygh[54] = 89;  
					yygh[55] = 94;  
					yygh[56] = 94;  
					yygh[57] = 94;  
					yygh[58] = 94;  
					yygh[59] = 94;  
					yygh[60] = 94;  
					yygh[61] = 94;  
					yygh[62] = 94;  
					yygh[63] = 94;  
					yygh[64] = 94;  
					yygh[65] = 94;  
					yygh[66] = 94;  
					yygh[67] = 94;  
					yygh[68] = 94;  
					yygh[69] = 94;  
					yygh[70] = 94;  
					yygh[71] = 94;  
					yygh[72] = 94;  
					yygh[73] = 95;  
					yygh[74] = 95;  
					yygh[75] = 96;  
					yygh[76] = 96;  
					yygh[77] = 96;  
					yygh[78] = 96;  
					yygh[79] = 96;  
					yygh[80] = 96;  
					yygh[81] = 96;  
					yygh[82] = 96;  
					yygh[83] = 96;  
					yygh[84] = 96;  
					yygh[85] = 96;  
					yygh[86] = 96;  
					yygh[87] = 96;  
					yygh[88] = 96;  
					yygh[89] = 96;  
					yygh[90] = 96;  
					yygh[91] = 96;  
					yygh[92] = 103;  
					yygh[93] = 103;  
					yygh[94] = 103;  
					yygh[95] = 103;  
					yygh[96] = 103;  
					yygh[97] = 103;  
					yygh[98] = 103;  
					yygh[99] = 103;  
					yygh[100] = 103;  
					yygh[101] = 103;  
					yygh[102] = 117;  
					yygh[103] = 117;  
					yygh[104] = 117;  
					yygh[105] = 117;  
					yygh[106] = 117;  
					yygh[107] = 117;  
					yygh[108] = 117;  
					yygh[109] = 117;  
					yygh[110] = 117;  
					yygh[111] = 117;  
					yygh[112] = 117;  
					yygh[113] = 117;  
					yygh[114] = 117;  
					yygh[115] = 117;  
					yygh[116] = 117;  
					yygh[117] = 117;  
					yygh[118] = 117;  
					yygh[119] = 117;  
					yygh[120] = 117;  
					yygh[121] = 117;  
					yygh[122] = 117;  
					yygh[123] = 117;  
					yygh[124] = 130;  
					yygh[125] = 130;  
					yygh[126] = 130;  
					yygh[127] = 130;  
					yygh[128] = 137;  
					yygh[129] = 142;  
					yygh[130] = 142;  
					yygh[131] = 142;  
					yygh[132] = 142;  
					yygh[133] = 142;  
					yygh[134] = 142;  
					yygh[135] = 164;  
					yygh[136] = 186;  
					yygh[137] = 191;  
					yygh[138] = 191;  
					yygh[139] = 191;  
					yygh[140] = 191;  
					yygh[141] = 191;  
					yygh[142] = 191;  
					yygh[143] = 191;  
					yygh[144] = 191;  
					yygh[145] = 191;  
					yygh[146] = 191;  
					yygh[147] = 191;  
					yygh[148] = 191;  
					yygh[149] = 191;  
					yygh[150] = 191;  
					yygh[151] = 206;  
					yygh[152] = 219;  
					yygh[153] = 232;  
					yygh[154] = 232;  
					yygh[155] = 247;  
					yygh[156] = 247;  
					yygh[157] = 260;  
					yygh[158] = 260;  
					yygh[159] = 261;  
					yygh[160] = 264;  
					yygh[161] = 264;  
					yygh[162] = 267;  
					yygh[163] = 280;  
					yygh[164] = 301;  
					yygh[165] = 301;  
					yygh[166] = 322;  
					yygh[167] = 343;  
					yygh[168] = 343;  
					yygh[169] = 343;  
					yygh[170] = 343;  
					yygh[171] = 343;  
					yygh[172] = 343;  
					yygh[173] = 343;  
					yygh[174] = 343;  
					yygh[175] = 343;  
					yygh[176] = 357;  
					yygh[177] = 364;  
					yygh[178] = 364;  
					yygh[179] = 371;  
					yygh[180] = 374;  
					yygh[181] = 374;  
					yygh[182] = 377;  
					yygh[183] = 377;  
					yygh[184] = 377;  
					yygh[185] = 382;  
					yygh[186] = 382;  
					yygh[187] = 382;  
					yygh[188] = 382;  
					yygh[189] = 382;  
					yygh[190] = 382;  
					yygh[191] = 382;  
					yygh[192] = 382;  
					yygh[193] = 382;  
					yygh[194] = 397;  
					yygh[195] = 397;  
					yygh[196] = 397;  
					yygh[197] = 397;  
					yygh[198] = 397;  
					yygh[199] = 397;  
					yygh[200] = 397;  
					yygh[201] = 397;  
					yygh[202] = 397;  
					yygh[203] = 397;  
					yygh[204] = 410;  
					yygh[205] = 410;  
					yygh[206] = 410;  
					yygh[207] = 410;  
					yygh[208] = 431;  
					yygh[209] = 431;  
					yygh[210] = 431;  
					yygh[211] = 431;  
					yygh[212] = 431;  
					yygh[213] = 431;  
					yygh[214] = 445;  
					yygh[215] = 459;  
					yygh[216] = 459;  
					yygh[217] = 459;  
					yygh[218] = 459;  
					yygh[219] = 470;  
					yygh[220] = 470;  
					yygh[221] = 470;  
					yygh[222] = 471;  
					yygh[223] = 472;  
					yygh[224] = 473;  
					yygh[225] = 474;  
					yygh[226] = 474;  
					yygh[227] = 474;  
					yygh[228] = 474;  
					yygh[229] = 474;  
					yygh[230] = 474;  
					yygh[231] = 474;  
					yygh[232] = 474;  
					yygh[233] = 475;  
					yygh[234] = 496;  
					yygh[235] = 496;  
					yygh[236] = 496;  
					yygh[237] = 496;  
					yygh[238] = 496;  
					yygh[239] = 496;  
					yygh[240] = 509;  
					yygh[241] = 509;  
					yygh[242] = 509;  
					yygh[243] = 509;  
					yygh[244] = 522;  
					yygh[245] = 536;  
					yygh[246] = 536;  
					yygh[247] = 536;  
					yygh[248] = 536;  
					yygh[249] = 536;  
					yygh[250] = 536;  
					yygh[251] = 536;  
					yygh[252] = 536;  
					yygh[253] = 536;  
					yygh[254] = 536;  
					yygh[255] = 536;  
					yygh[256] = 536;  
					yygh[257] = 536;  
					yygh[258] = 536;  
					yygh[259] = 536;  
					yygh[260] = 536;  
					yygh[261] = 536;  
					yygh[262] = 536;  
					yygh[263] = 536;  
					yygh[264] = 536;  
					yygh[265] = 536;  
					yygh[266] = 536;  
					yygh[267] = 536;  
					yygh[268] = 536;  
					yygh[269] = 536;  
					yygh[270] = 549;  
					yygh[271] = 570;  
					yygh[272] = 570;  
					yygh[273] = 581;  
					yygh[274] = 581;  
					yygh[275] = 581;  
					yygh[276] = 581;  
					yygh[277] = 593;  
					yygh[278] = 593;  
					yygh[279] = 593;  
					yygh[280] = 606;  
					yygh[281] = 606;  
					yygh[282] = 606;  
					yygh[283] = 606;  
					yygh[284] = 606;  
					yygh[285] = 620;  
					yygh[286] = 620;  
					yygh[287] = 620;  
					yygh[288] = 635;  
					yygh[289] = 651;  
					yygh[290] = 668;  
					yygh[291] = 686;  
					yygh[292] = 705;  
					yygh[293] = 726;  
					yygh[294] = 726;  
					yygh[295] = 726;  
					yygh[296] = 726;  
					yygh[297] = 726;  
					yygh[298] = 739;  
					yygh[299] = 752;  
					yygh[300] = 752;  
					yygh[301] = 752;  
					yygh[302] = 752;  
					yygh[303] = 760;  
					yygh[304] = 768;  
					yygh[305] = 789;  
					yygh[306] = 802;  
					yygh[307] = 802;  
					yygh[308] = 802;  
					yygh[309] = 802;  
					yygh[310] = 802;  
					yygh[311] = 803;  
					yygh[312] = 804;  
					yygh[313] = 805;  
					yygh[314] = 806;  
					yygh[315] = 806;  
					yygh[316] = 806;  
					yygh[317] = 806;  
					yygh[318] = 806;  
					yygh[319] = 806;  
					yygh[320] = 806;  
					yygh[321] = 806;  
					yygh[322] = 806;  
					yygh[323] = 806;  
					yygh[324] = 806;  
					yygh[325] = 806;  
					yygh[326] = 806;  
					yygh[327] = 806;  
					yygh[328] = 806;  
					yygh[329] = 806;  
					yygh[330] = 806;  
					yygh[331] = 806;  
					yygh[332] = 806;  
					yygh[333] = 806;  
					yygh[334] = 806;  
					yygh[335] = 806;  
					yygh[336] = 806;  
					yygh[337] = 806;  
					yygh[338] = 813;  
					yygh[339] = 813;  
					yygh[340] = 813; 

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
					yyr[yyrc] = new YYRRec(1,-4);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-4);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-4);yyrc++; 
					yyr[yyrc] = new YYRRec(5,-7);yyrc++; 
					yyr[yyrc] = new YYRRec(5,-7);yyrc++; 
					yyr[yyrc] = new YYRRec(5,-14);yyrc++; 
					yyr[yyrc] = new YYRRec(2,-14);yyrc++; 
					yyr[yyrc] = new YYRRec(3,-15);yyrc++; 
					yyr[yyrc] = new YYRRec(3,-15);yyrc++; 
					yyr[yyrc] = new YYRRec(4,-16);yyrc++; 
					yyr[yyrc] = new YYRRec(2,-16);yyrc++; 
					yyr[yyrc] = new YYRRec(5,-18);yyrc++; 
					yyr[yyrc] = new YYRRec(5,-18);yyrc++; 
					yyr[yyrc] = new YYRRec(5,-19);yyrc++; 
					yyr[yyrc] = new YYRRec(2,-19);yyrc++; 
					yyr[yyrc] = new YYRRec(4,-11);yyrc++; 
					yyr[yyrc] = new YYRRec(3,-11);yyrc++; 
					yyr[yyrc] = new YYRRec(3,-11);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-13);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-13);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-21);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-21);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-21);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-21);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-21);yyrc++; 
					yyr[yyrc] = new YYRRec(3,-8);yyrc++; 
					yyr[yyrc] = new YYRRec(3,-6);yyrc++; 
					yyr[yyrc] = new YYRRec(3,-6);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-31);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-31);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-31);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-31);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-29);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-29);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-30);yyrc++; 
					yyr[yyrc] = new YYRRec(2,-30);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-32);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-32);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-32);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-32);yyrc++; 
					yyr[yyrc] = new YYRRec(5,-12);yyrc++; 
					yyr[yyrc] = new YYRRec(2,-36);yyrc++; 
					yyr[yyrc] = new YYRRec(0,-36);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-37);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-37);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-34);yyrc++; 
					yyr[yyrc] = new YYRRec(5,-10);yyrc++; 
					yyr[yyrc] = new YYRRec(4,-10);yyrc++; 
					yyr[yyrc] = new YYRRec(3,-10);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-40);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-39);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-39);yyrc++; 
					yyr[yyrc] = new YYRRec(2,-20);yyrc++; 
					yyr[yyrc] = new YYRRec(2,-20);yyrc++; 
					yyr[yyrc] = new YYRRec(0,-20);yyrc++; 
					yyr[yyrc] = new YYRRec(3,-41);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-41);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-43);yyrc++; 
					yyr[yyrc] = new YYRRec(2,-43);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-44);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-44);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-44);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-44);yyrc++; 
					yyr[yyrc] = new YYRRec(5,-9);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-45);yyrc++; 
					yyr[yyrc] = new YYRRec(3,-17);yyrc++; 
					yyr[yyrc] = new YYRRec(4,-17);yyrc++; 
					yyr[yyrc] = new YYRRec(3,-17);yyrc++; 
					yyr[yyrc] = new YYRRec(2,-17);yyrc++; 
					yyr[yyrc] = new YYRRec(2,-17);yyrc++; 
					yyr[yyrc] = new YYRRec(2,-17);yyrc++; 
					yyr[yyrc] = new YYRRec(0,-17);yyrc++; 
					yyr[yyrc] = new YYRRec(3,-49);yyrc++; 
					yyr[yyrc] = new YYRRec(3,-49);yyrc++; 
					yyr[yyrc] = new YYRRec(2,-49);yyrc++; 
					yyr[yyrc] = new YYRRec(2,-49);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-49);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-49);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-51);yyrc++; 
					yyr[yyrc] = new YYRRec(2,-51);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-53);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-53);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-53);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-53);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-53);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-54);yyrc++; 
					yyr[yyrc] = new YYRRec(3,-55);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-55);yyrc++; 
					yyr[yyrc] = new YYRRec(3,-56);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-56);yyrc++; 
					yyr[yyrc] = new YYRRec(3,-57);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-57);yyrc++; 
					yyr[yyrc] = new YYRRec(3,-58);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-58);yyrc++; 
					yyr[yyrc] = new YYRRec(3,-59);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-59);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-60);yyrc++; 
					yyr[yyrc] = new YYRRec(3,-60);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-61);yyrc++; 
					yyr[yyrc] = new YYRRec(3,-61);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-63);yyrc++; 
					yyr[yyrc] = new YYRRec(3,-63);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-65);yyrc++; 
					yyr[yyrc] = new YYRRec(3,-65);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-67);yyrc++; 
					yyr[yyrc] = new YYRRec(2,-67);yyrc++; 
					yyr[yyrc] = new YYRRec(2,-69);yyrc++; 
					yyr[yyrc] = new YYRRec(4,-69);yyrc++; 
					yyr[yyrc] = new YYRRec(3,-69);yyrc++; 
					yyr[yyrc] = new YYRRec(2,-69);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-69);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-69);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-69);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-62);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-62);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-64);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-64);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-64);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-64);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-66);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-66);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-68);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-68);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-68);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-70);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-70);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-70);yyrc++; 
					yyr[yyrc] = new YYRRec(2,-52);yyrc++; 
					yyr[yyrc] = new YYRRec(4,-52);yyrc++; 
					yyr[yyrc] = new YYRRec(3,-52);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-73);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-73);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-73);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-73);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-73);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-71);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-71);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-71);yyrc++; 
					yyr[yyrc] = new YYRRec(4,-48);yyrc++; 
					yyr[yyrc] = new YYRRec(5,-48);yyrc++; 
					yyr[yyrc] = new YYRRec(5,-48);yyrc++; 
					yyr[yyrc] = new YYRRec(3,-5);yyrc++; 
					yyr[yyrc] = new YYRRec(3,-5);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-5);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-28);yyrc++; 
					yyr[yyrc] = new YYRRec(2,-28);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-75);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-75);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-75);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-75);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-75);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-42);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-42);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-42);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-42);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-42);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-42);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-42);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-42);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-42);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-42);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-50);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-50);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-50);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-50);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-50);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-27);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-27);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-72);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-72);yyrc++; 
					yyr[yyrc] = new YYRRec(2,-38);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-38);yyrc++; 
					yyr[yyrc] = new YYRRec(2,-76);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-76);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-23);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-25);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-22);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-22);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-22);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-77);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-77);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-77);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-77);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-77);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-77);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-77);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-77);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-77);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-77);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-77);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-77);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-77);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-77);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-77);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-78);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-78);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-78);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-78);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-78);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-78);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-78);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-74);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-74);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-74);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-74);yyrc++; 
					yyr[yyrc] = new YYRRec(3,-79);yyrc++; 
					yyr[yyrc] = new YYRRec(3,-79);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-79);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-33);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-46);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-35);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-35);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-47);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-26);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-24);yyrc++;
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
                    return true;
            }
            System.Console.WriteLine(Input);
            System.Console.WriteLine();
            System.Console.WriteLine("No matching token found!");
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
        public static string SubScanner(string file)
        {
            string inputstream = File.ReadAllText(file, Encoding.ASCII);

            MyCompiler compiler = new MyCompiler();
            StringBuilder sb = new StringBuilder();
            compiler.Output = new StringWriter(sb);
            if (!compiler.ScannerOpt(inputstream))
                return string.Empty;
            compiler.InitTables();
            if (!compiler.yyparse())
                return string.Empty;

            if (compiler.Output != null)
                compiler.Output.Close();
            return sb.ToString();
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

			if (Regex.IsMatch(Rest,"^((?i)IFDEF)")){
				Results.Add (t_IFDEF);
				ResultsV.Add(Regex.Match(Rest,"^((?i)IFDEF)").Value);}

			if (Regex.IsMatch(Rest,"^(;)")){
				Results.Add (t_Char59);
				ResultsV.Add(Regex.Match(Rest,"^(;)").Value);}

			if (Regex.IsMatch(Rest,"^((?i)IFNDEF)")){
				Results.Add (t_IFNDEF);
				ResultsV.Add(Regex.Match(Rest,"^((?i)IFNDEF)").Value);}

			if (Regex.IsMatch(Rest,"^((?i)IFELSE)")){
				Results.Add (t_IFELSE);
				ResultsV.Add(Regex.Match(Rest,"^((?i)IFELSE)").Value);}

			if (Regex.IsMatch(Rest,"^((?i)ENDIF)")){
				Results.Add (t_ENDIF);
				ResultsV.Add(Regex.Match(Rest,"^((?i)ENDIF)").Value);}

			if (Regex.IsMatch(Rest,"^((?i)DEFINE)")){
				Results.Add (t_DEFINE);
				ResultsV.Add(Regex.Match(Rest,"^((?i)DEFINE)").Value);}

			if (Regex.IsMatch(Rest,"^((?i)UNDEF)")){
				Results.Add (t_UNDEF);
				ResultsV.Add(Regex.Match(Rest,"^((?i)UNDEF)").Value);}

			if (Regex.IsMatch(Rest,"^((?i)INCLUDE)")){
				Results.Add (t_INCLUDE);
				ResultsV.Add(Regex.Match(Rest,"^((?i)INCLUDE)").Value);}

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
