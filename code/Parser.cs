using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;
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
            tList.Add(t_IFDEF);
            rList.Add(new Regex("^((?i)IFDEF)"));
            tList.Add(t_Char59);
            rList.Add(new Regex("^((,[\\s\\t\\x00]*)?;+)"));
            tList.Add(t_IFNDEF);
            rList.Add(new Regex("^((?i)IFNDEF)"));
            tList.Add(t_IFELSE);
            rList.Add(new Regex("^((?i)IFELSE)"));
            tList.Add(t_ENDIF);
            rList.Add(new Regex("^((?i)ENDIF)"));
            tList.Add(t_DEFINE);
            rList.Add(new Regex("^((?i)DEFINE)"));
            tList.Add(t_Char44);
            rList.Add(new Regex("^(,|:=)"));
            tList.Add(t_UNDEF);
            rList.Add(new Regex("^((?i)UNDEF)"));
            tList.Add(t_INCLUDE);
            rList.Add(new Regex("^((?i)INCLUDE)"));
            tList.Add(t_Char123);
            rList.Add(new Regex("^(\\{)"));
            tList.Add(t_Char125);
            rList.Add(new Regex("^(\\}([\\t\\s\\x00]*;+)?)"));
            tList.Add(t_Char58);
            rList.Add(new Regex("^(:)"));
            tList.Add(t_Char124Char124);
            rList.Add(new Regex("^(\\|\\|)"));
            tList.Add(t_Char38Char38);
            rList.Add(new Regex("^(&&)"));
            tList.Add(t_Char124);
            rList.Add(new Regex("^(\\|)"));
            tList.Add(t_Char94);
            rList.Add(new Regex("^(\\^)"));
            tList.Add(t_Char38);
            rList.Add(new Regex("^(&)"));
            tList.Add(t_Char40);
            rList.Add(new Regex("^(\\()"));
            tList.Add(t_Char41);
            rList.Add(new Regex("^(\\))"));
            tList.Add(t_Char33Char61);
            rList.Add(new Regex("^(!=)"));
            tList.Add(t_Char61Char61);
            rList.Add(new Regex("^(==)"));
            tList.Add(t_Char60);
            rList.Add(new Regex("^(<)"));
            tList.Add(t_Char60Char61);
            rList.Add(new Regex("^(<=)"));
            tList.Add(t_Char62);
            rList.Add(new Regex("^(>)"));
            tList.Add(t_Char62Char61);
            rList.Add(new Regex("^(>=)"));
            tList.Add(t_Char43);
            rList.Add(new Regex("^(\\+)"));
            tList.Add(t_Char45);
            rList.Add(new Regex("^(\\-)"));
            tList.Add(t_Char37);
            rList.Add(new Regex("^(%)"));
            tList.Add(t_Char42);
            rList.Add(new Regex("^(\\*)"));
            tList.Add(t_Char47);
            rList.Add(new Regex("^(/)"));
            tList.Add(t_Char33);
            rList.Add(new Regex("^(!)"));
            tList.Add(t_RULE);
            rList.Add(new Regex("^((?i)RULE)"));
            tList.Add(t_Char42Char61);
            rList.Add(new Regex("^(\\*[\\s\\t\\x00]*=)"));
            tList.Add(t_Char43Char61);
            rList.Add(new Regex("^(\\+[\\s\\t\\x00]*=)"));
            tList.Add(t_Char45Char61);
            rList.Add(new Regex("^(\\-[\\s\\t\\x00]*=)"));
            tList.Add(t_Char47Char61);
            rList.Add(new Regex("^(/[\\s\\t\\x00]*=)"));
            tList.Add(t_Char61);
            rList.Add(new Regex("^(=)"));
            tList.Add(t_ELSE);
            rList.Add(new Regex("^((?i)ELSE)"));
            tList.Add(t_IF);
            rList.Add(new Regex("^((?i)IF)"));
            tList.Add(t_WHILE);
            rList.Add(new Regex("^((?i)WHILE)"));
            tList.Add(t_Char46);
            rList.Add(new Regex("^(\\.)"));
            tList.Add(t_NULL);
            rList.Add(new Regex("^((?i)NULL)"));
            tList.Add(t_event);
            rList.Add(new Regex("^((?i)(EACH_SEC|IF_(ALT|ANYKEY|APO|BKSL|BKSP|BRACKL|BRACKR|CAL|CAR|COMMA|CTRL|CUD|CUL|CUR|CUU|DEL|END|EQUALS|ESC|HOME|INS|JOY4|LEFT|LOAD|MIDDLE|MINUS|MSTOP|PAUSE|PERIOD|PGDN|PGUP|PLUS|RIGHT|SEMIC|SLASH|SPACE|START|SZ|TAB|TAST|F(1[0-2]|[1-9])|[0-9A-Z])|LAYERS|MESSAGES|PANELS))"));
            tList.Add(t_global);
            rList.Add(new Regex("^((?i)(VIDEO|NEXUS|LIGHT_ANGLE|IBANK|DRUMBANK|MIDI_PITCH|BIND|MAPFILE|SAVEDIR|PATH|DITHER|SAVE_KEYS|REMOTE_KEYS))"));
            tList.Add(t_asset);
            rList.Add(new Regex("^((?i)(MODEL|SOUND|MUSIC|FLIC|BMAP|OVLY|FONT))"));
            tList.Add(t_object);
            rList.Add(new Regex("^((?i)(OVERLAY|PANEL|PALETTE|REGION|SKILL|STRING|SYNONYM|TEXTURE|TEXT|VIEW|WALL|WAY))"));
            tList.Add(t_function);
            rList.Add(new Regex("^((?i)(ACTION|RULES))"));
            tList.Add(t_math);
            rList.Add(new Regex("^((?i)(ACOS|COS|TAN|SIGN|INT|EXP|LOG10|LOG2|LOG))"));
            tList.Add(t_flag);
            rList.Add(new Regex("^((?i)(FLOOR_DESCEND|CEIL_DESCEND|FLOOR_ASCEND|FLOOR_LIFTED|CEIL_ASCEND|CEIL_LIFTED|TRANSPARENT|CANDELABER|DIAPHANOUS|IMMATERIAL|IMPASSABLE|PORTCULLIS|AUTORANGE|CAREFULLY|CONDENSED|FLAG[1-8]|INVISIBLE|SENSITIVE|BERKELEY|CENTER_X|CENTER_Y|LIGHTMAP|PASSABLE|SAVE_ALL|CLUSTER|CURTAIN|FRAGILE|NO_CLIP|ONESHOT|REFRESH|VISIBLE|ABSPOS|BEHIND|GROUND|LIFTED|MASTER|NARROW|RELPOS|SHADOW|STICKY|FENCE|GHOST|LIBER|MOVED|SLOOP|BASE|BLUR|CLIP|FLAT|HARD|PLAY|SEEN|WIRE|FAR|SKY))"));
            tList.Add(t_property);
            rList.Add(new Regex("^((?i)(ALBEDO|ANGLE|ASPEED|ATTACH|BELOW|BMAPS|BUTTON|CEIL_(ANGLE|OFFS_[X-Y]|TEX)|CYCLES|CYCLE|DEFAULT|DELAY|DIGITS|DISTANCE|DIST|EACH_CYCLE_C|EACH_CYCLE|FLAGS|FLOOR_(ANGLE|OFFS_[X-Y]|TEX)|FRAME|GENIUS|HBAR|HEIGHT|HSLIDER|IF_(ARISE|ARRIVED|DIVE|FAR|LEAVE|NEAR|RELEASE|TOUCH)|INDEX|LAYER|LEFT|LENGTH|MAP_COLOR|MASK|MAX|MIN|MIRROR|OFFSET_[X-Y]|OVLYS|PALFILE|PAN_MAP|PICTURE|POSITION|POS_[X-Y]|RADIANCE|RANGE|REL_(ANGLE|DIST)|RIGHT|SCALE_(XY|X|Y)|SCYCLES|SCYCLE|SDIST|SIDES|SIDE|SIZE_[X-Y]|SKILL[1-8]|SPEED|STRINGS|SVDIST|SVOL|TARGET_(MAP|X|Y)|TARGET|TEXTURE[1-4]|THING_HGT|TITLE|TOP|TOUCH|TYPE|VAL|VBAR|VSLIDER|VSPEED|WAYPOINT|WINDOW|[X-Z][1-2]|[X-Z]))"));
            tList.Add(t_command);
            rList.Add(new Regex("^((?i)(ACCEL|ADD_STRING|ADDT|ADD|AND|BEEP|BRANCH|BREAK|CALL|DROP|END|EXCLUSIVE|EXEC_RULES|EXIT|EXPLODE|FADE_PAL|FIND|FREEZE|GETMIDI|GOTO|IF_(ABOVE|BELOW|EQUAL|MAX|MIN|NEQUAL)|INKEY|INPORT|LEVEL|LIFT|LOAD_INFO|LOAD|LOCATE|MAP|MIDI_COM|NEXT_(MY_THERE|MY|THERE)|NOP|OUTPORT|PLACE|PLAY_(CD|DEMO|FLICFILE|FLIC|SONG_ONCE|SONG|SOUNDFILE|SOUND)|PRINTFILE|PRINT_(STRING|VALUE)|PUSH|RANDOMIZE|ROTATE|SAVE_(DEMO|INFO)|SCAN|SCREENSHOT|SETMIDI|SET_(ALL|INFO|SKILL|STRING)|SET|SHAKE|SHIFT|SHOOT|SKIP|STOP_(DEMO|FLIC|SOUND)|SUBT|SUB|TILT|TO_STRING|WAITT|WAIT_TICKS|WAIT))"));
            tList.Add(t_list);
            rList.Add(new Regex("^((?i)((EACH_TICK|EACH_SEC|PANELS|LAYERS|MESSAGES)\\.(1[0-6]|[1-9])))"));
            tList.Add(t_skill);
            rList.Add(new Regex("^((?i)(ACCELERATION|ACTIONS|ACTIVE_(NEXUS|OBJTICKS|TARGETS)|ACTOR_(CEIL_HGT|CLIMB|DIST|FLOOR_HGT|IMPACT_V[X-Z]|WIDTH)|ASPECT|BLUR_MODE|BOUNCE_V[X-Y]|CDAUDIO_VOL|CD_TRACK|CHANNEL_[0-7]|CHANNEL|CLIPPED|CLIPPING|COLOR_(ACTORS|BORDER|PLAYER|THINGS|WALLS)|DARK_DIST|DEBUG_MODE|DELTA_ANGLE|ERROR|FLIC_FRAME|FLOOR_MODE|FORCE_(AHEAD|ROT|STRAFE|TILT|UP)|FRAME_COLOR|FRICTION|HALF_PI|HIT_(DIST|MINDIST|X|Y)|IMPACT_V(ROT|[X-Z])|INERTIA|INV_DIST|JOYSTICK_[X-Y]|JOY_(4|SENSE)|KEY_(ALT|ANY|APO|BKSL|BKSP|BRACKL|BRACKR|CAL|CAR|COMMA|CTRL|CUD|CUL|CUR|CUU|DEL|END|ENTER|EQUALS|ESC|HOME|INS|JOY4|MINUS|PAUSE|PERIOD|PGDN|PGUP|PLUS|SEMIC|SENSE|SHIFT|SLASH|SPACE|SZ|TAB|F(1[0-2]|[1-9])|[A-Z0-9])|LIGHT_DIST|LOAD_MODE|MAP_(CENTER[X-Y]|(EDGE_[X-Y][1-2])|LAYER|MAX[X-Y]|MIN[X-Y]|MODE|OFFS[X-Y]|ROT|SCALE)|MAX_DIST|MICKEY_[X-Y]|MINV_DIST|MOTION_BLUR|MOUSE_(ANGLE|CALM|LEFT|MIDDLE|MODE|MOVING|RIGHT|SENSE|TIME|X|Y)|MOVE_(ANGLE|MODE)|MUSIC_VOL|MY_(X[1-2]|Y[1-2]|Z[1-2]|X|Y)|PANEL_LAYER|PI|PLAYER_(ANGLE|ARC|CLIMB|COS|DEPTH|HGT|LAST_[X-Y]|LIGHT|MSIN|SIN|SIZE|SPEED|TILT|VROT|V[X-Z]|WIDTH|[X-Z])|PSOUND_(TONE|VOL)|REAL_SPEED|REMOTE_[0-1]|RENDER_MODE|SCREEN_(HGT|WIDTH|X|Y)|SECS|SHIFT_SENSE|SHOOT_(ANGLE|FAC|RANGE|SECTOR|X|Y)|SKIP_FRAMES|SKY_OFFS_[X-Y]|SLOPE_(AHEAD|SIDE|X|Y)|SOUND_VOL|SPANS|STEPS|STR_LEN|TEXT_LAYER|THING_(DIST|WIDTH)|TICKS|TIME_(ACTIONS|CLIPPING|CORR|DRAW|FAC|FBUFFER|SLICES|TARGETS|VERTICES)|TOUCH_(DIST|MODE|STATE)|TWO_PI|WALK_(PERIOD|TIME)|WALK|WAVE_PERIOD|WAVE|PALANIM_DELAY))"));
            tList.Add(t_synonym);
            rList.Add(new Regex("^((?i)(THERE|MY|HIT|TOUCH_TEXT|TOUCHED|TOUCH_TEX|TOUCH_REG|COMMAND_LINE))"));
            tList.Add(t_ambigChar95globalChar95property);
            rList.Add(new Regex("^((?i)CLIP_DIST)"));
            tList.Add(t_ambigChar95eventChar95property);
            rList.Add(new Regex("^((?i)(EACH_TICK|IF_ENTER|IF_HIT|IF_KLICK))"));
            tList.Add(t_ambigChar95objectChar95flag);
            rList.Add(new Regex("^((?i)(THING|ACTOR))"));
            tList.Add(t_ambigChar95mathChar95command);
            rList.Add(new Regex("^((?i)(SIN|ASIN|SQRT|ABS))"));
            tList.Add(t_ambigChar95mathChar95skillChar95property);
            rList.Add(new Regex("^((?i)RANDOM)"));
            tList.Add(t_ambigChar95synonymChar95flag);
            rList.Add(new Regex("^((?i)HERE)"));
            tList.Add(t_ambigChar95skillChar95property);
            rList.Add(new Regex("^((?i)(FLOOR_HGT|CEIL_HGT|AMBIENT|RESULT|NODE))"));
            tList.Add(t_ambigChar95commandChar95flag);
            rList.Add(new Regex("^((?i)SAVE)"));
            tList.Add(t_ambigChar95globalChar95synonymChar95property);
            rList.Add(new Regex("^((?i)MSPRITE)"));
            tList.Add(t_ambigChar95commandChar95property);
            rList.Add(new Regex("^((?i)DO)"));
            tList.Add(t_integer);
            rList.Add(new Regex("^([0-9]+)"));
            tList.Add(t_fixed);
            rList.Add(new Regex("^(([0-9]*\\.[0-9]+)|([0-9]+\\.[0-9]*))"));
            tList.Add(t_identifier);
            rList.Add(new Regex("^([A-Za-z0-9_][A-Za-z0-9_]*(\\.[1-9][0-9]?)?)"));
            tList.Add(t_file);
            rList.Add(new Regex("^(<[\\s]?[^<;:\" ]+\\.[^<;:\" ]+[\\s]?>)"));
            tList.Add(t_string);
            rList.Add(new Regex("^(\"(.|[\\r\\n])*?\")"));
            tList.Add(t_ignore);
            rList.Add(new Regex("^([\\r\\n\\t\\s\\x00]|(#.*(\\n|$))|(//.*(\\n|$))|(/\\*(.|[\\r\\n])*?\\*/))"));
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
        int yymaxdepth = 4096;
        int yyflag = 0;
        int yyfnone = 0;
        int[] yys = new int[4096];
        string[] yyv = new string[4096];

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
                int t_Char44 = 263;
                int t_UNDEF = 264;
                int t_INCLUDE = 265;
                int t_Char123 = 266;
                int t_Char125 = 267;
                int t_Char58 = 268;
                int t_Char124Char124 = 269;
                int t_Char38Char38 = 270;
                int t_Char124 = 271;
                int t_Char94 = 272;
                int t_Char38 = 273;
                int t_Char40 = 274;
                int t_Char41 = 275;
                int t_Char33Char61 = 276;
                int t_Char61Char61 = 277;
                int t_Char60 = 278;
                int t_Char60Char61 = 279;
                int t_Char62 = 280;
                int t_Char62Char61 = 281;
                int t_Char43 = 282;
                int t_Char45 = 283;
                int t_Char37 = 284;
                int t_Char42 = 285;
                int t_Char47 = 286;
                int t_Char33 = 287;
                int t_RULE = 288;
                int t_Char42Char61 = 289;
                int t_Char43Char61 = 290;
                int t_Char45Char61 = 291;
                int t_Char47Char61 = 292;
                int t_Char61 = 293;
                int t_ELSE = 294;
                int t_IF = 295;
                int t_WHILE = 296;
                int t_Char46 = 297;
                int t_NULL = 298;
                int t_event = 299;
                int t_global = 300;
                int t_asset = 301;
                int t_object = 302;
                int t_function = 303;
                int t_math = 304;
                int t_flag = 305;
                int t_property = 306;
                int t_command = 307;
                int t_list = 308;
                int t_skill = 309;
                int t_synonym = 310;
                int t_ambigChar95globalChar95property = 311;
                int t_ambigChar95eventChar95property = 312;
                int t_ambigChar95objectChar95flag = 313;
                int t_ambigChar95mathChar95command = 314;
                int t_ambigChar95mathChar95skillChar95property = 315;
                int t_ambigChar95synonymChar95flag = 316;
                int t_ambigChar95skillChar95property = 317;
                int t_ambigChar95commandChar95flag = 318;
                int t_ambigChar95globalChar95synonymChar95property = 319;
                int t_ambigChar95commandChar95property = 320;
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

            StreamReader in_s = File.OpenText(InputFilename);
            string inputstream = in_s.ReadToEnd();
            in_s.Close();

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
         yyval = "";
         
       break;
							case    4 : 
         yyval = Sections.AddDummySection(yyv[yysp-0]);
         
       break;
							case    5 : 
         yyval = yyv[yysp-0];
         
       break;
							case    6 : 
         yyval = Sections.AddGlobalSection(yyv[yysp-0]);
         
       break;
							case    7 : 
         yyval = yyv[yysp-0];
         
       break;
							case    8 : 
         yyval = Sections.AddDefineSection(yyv[yysp-0]);
         
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
         //yyval = yyv[yysp-4] + yyv[yysp-3] + yyv[yysp-2] + yyv[yysp-1] + yyv[yysp-0];
         yyval = Sections.CreatePreProcIfCondition(yyv[yysp-3], yyv[yysp-1]);
         
       break;
							case   13 : 
         //yyval = yyv[yysp-4] + yyv[yysp-3] + yyv[yysp-2] + yyv[yysp-1] + yyv[yysp-0];
         yyval = Sections.CreatePreProcIfNotCondition(yyv[yysp-3], yyv[yysp-1]);
         
       break;
							case   14 : 
         //yyval = yyv[yysp-4] + yyv[yysp-3] + yyv[yysp-2] + yyv[yysp-1] + yyv[yysp-0];
         yyval = Sections.CreatePreProcElseCondition(yyv[yysp-4], yyv[yysp-1]);
         
       break;
							case   15 : 
         //yyval = yyv[yysp-1] + yyv[yysp-0];
         yyval = Sections.CreatePreProcEndCondition(yyv[yysp-1]);
         
       break;
							case   16 : 
         //yyval = yyv[yysp-4] + yyv[yysp-3] + yyv[yysp-2] + yyv[yysp-1] + yyv[yysp-0];
         yyval = Actions.CreatePreProcIfCondition(yyv[yysp-3], yyv[yysp-1]);
         
       break;
							case   17 : 
         //yyval = yyv[yysp-4] + yyv[yysp-3] + yyv[yysp-2] + yyv[yysp-1] + yyv[yysp-0];
         yyval = Actions.CreatePreProcIfNotCondition(yyv[yysp-3], yyv[yysp-1]);
         
       break;
							case   18 : 
         //yyval = yyv[yysp-4] + yyv[yysp-3] + yyv[yysp-2] + yyv[yysp-1] + yyv[yysp-0];
         yyval = Actions.CreatePreProcElseCondition(yyv[yysp-4], yyv[yysp-1]);
         
       break;
							case   19 : 
         //yyval = yyv[yysp-1] + yyv[yysp-0];
         yyval = Actions.CreatePreProcEndCondition(yyv[yysp-1]);
         
       break;
							case   20 : 
         //yyval = yyv[yysp-4] + yyv[yysp-3] + yyv[yysp-2] + yyv[yysp-1] + yyv[yysp-0];
         yyval = Objects.CreatePreProcIfCondition(yyv[yysp-3], yyv[yysp-1]);
         
       break;
							case   21 : 
         //yyval = yyv[yysp-4] + yyv[yysp-3] + yyv[yysp-2] + yyv[yysp-1] + yyv[yysp-0];
         yyval = Objects.CreatePreProcIfNotCondition(yyv[yysp-3], yyv[yysp-1]);
         
       break;
							case   22 : 
         //yyval = yyv[yysp-4] + yyv[yysp-3] + yyv[yysp-2] + yyv[yysp-1] + yyv[yysp-0];
         yyval = Objects.CreatePreProcElseCondition(yyv[yysp-4], yyv[yysp-1]);
         
       break;
							case   23 : 
         //yyval = yyv[yysp-1] + yyv[yysp-0];
         yyval = Objects.CreatePreProcEndCondition(yyv[yysp-1]);
         
       break;
							case   24 : 
         //yyval = yyv[yysp-4] + yyv[yysp-3] + yyv[yysp-2] + yyv[yysp-1] + yyv[yysp-0];
         yyval = Defines.AddTransform(yyv[yysp-3]);
         
       break;
							case   25 : 
         //yyval = yyv[yysp-2] + yyv[yysp-1] + yyv[yysp-0];
         yyval = Defines.AddDefine(yyv[yysp-1]);
         
       break;
							case   26 : 
         //yyval = yyv[yysp-2] + yyv[yysp-1] + yyv[yysp-0];
         yyval = Defines.RemoveDefine(yyv[yysp-1]);
         
       break;
							case   27 : 
         yyval = Formatter.FormatPreprocessor(yyv[yysp-0]);
         
       break;
							case   28 : 
         yyval = Formatter.FormatPreprocessor(yyv[yysp-0]);
         
       break;
							case   29 : 
         //yyval = yyv[yysp-0];
         yyval = "";
         Defines.AddStringDefine(yyv[yysp-0]);
         
       break;
							case   30 : 
         //yyval = yyv[yysp-0];
         yyval = "";
         //remove from grammar?
         
       break;
							case   31 : 
         //yyval = yyv[yysp-0];
         Defines.AddFileDefine(yyv[yysp-0]);
         
       break;
							case   32 : 
         //yyval = yyv[yysp-0];
         Defines.AddNumberDefine(yyv[yysp-0]);
         
       break;
							case   33 : 
         //yyval = yyv[yysp-0];
         Defines.AddKeywordDefine(yyv[yysp-0]);
         
       break;
							case   34 : 
         //yyval = yyv[yysp-2] + yyv[yysp-1] + yyv[yysp-0];
         yyval = Include.Process(yyv[yysp-1]);
         
       break;
							case   35 : 
         //yyval = yyv[yysp-3] + yyv[yysp-2] + yyv[yysp-1] + yyv[yysp-0];
         yyval = Globals.AddEvent(yyv[yysp-3]);
         
       break;
							case   36 : 
         //yyval = yyv[yysp-3] + yyv[yysp-2] + yyv[yysp-1] + yyv[yysp-0];
         yyval = Globals.AddGlobal(yyv[yysp-3], yyv[yysp-1]);
         
       break;
							case   37 : 
         yyval = Formatter.FormatGlobal(yyv[yysp-0]);
         
       break;
							case   38 : 
         yyval = Formatter.FormatGlobal(yyv[yysp-0]);
         
       break;
							case   39 : 
         yyval = Formatter.FormatGlobal(yyv[yysp-0]);
         
       break;
							case   40 : 
         yyval = Formatter.FormatTargetSkill(yyv[yysp-0]);
         
       break;
							case   41 : 
         yyval = Formatter.FormatEvent(yyv[yysp-0]);
         
       break;
							case   42 : 
         yyval = Formatter.FormatEvent(yyv[yysp-0]);
         
       break;
							case   43 : 
         //yyval = yyv[yysp-1];
         yyval = "";
         Globals.AddParameter(yyv[yysp-1]);
         
       break;
							case   44 : 
         //yyval = yyv[yysp-2] + yyv[yysp-1] + yyv[yysp-0];
         yyval = "";
         Globals.AddParameter(yyv[yysp-2]);
         
       break;
							case   45 : 
         yyval = yyv[yysp-0];
         
       break;
							case   46 : 
         yyval = yyv[yysp-0];
         
       break;
							case   47 : 
         yyval = yyv[yysp-0];
         
       break;
							case   48 : 
         yyval = yyv[yysp-0];
         
       break;
							case   49 : 
         //yyval = yyv[yysp-6] + yyv[yysp-5] + yyv[yysp-4] + yyv[yysp-3] + yyv[yysp-2] + yyv[yysp-1] + yyv[yysp-0];
         yyval = Assets.AddAsset(yyv[yysp-6], yyv[yysp-5], yyv[yysp-3]);
         
       break;
							case   50 : 
         //yyval = yyv[yysp-2] + yyv[yysp-1] + yyv[yysp-0];
         yyval = "";
         Assets.AddParameter(yyv[yysp-2]);
         
       break;
							case   51 : 
         //yyval = yyv[yysp-0];
         yyval = "";
         Assets.AddParameter(yyv[yysp-0]);
         
       break;
							case   52 : 
         yyval = "";
         
       break;
							case   53 : 
         yyval = Formatter.FormatObject(yyv[yysp-0]);
         
       break;
							case   54 : 
         //yyval = yyv[yysp-5] + yyv[yysp-4] + yyv[yysp-3] + yyv[yysp-2] + yyv[yysp-1] + yyv[yysp-0];
         yyval = Objects.AddObject(yyv[yysp-5], yyv[yysp-4], yyv[yysp-1]);
         
       break;
							case   55 : 
         //yyval = yyv[yysp-4] + yyv[yysp-3] + yyv[yysp-2] + yyv[yysp-1] + yyv[yysp-0];
         yyval = Objects.AddStringObject(yyv[yysp-4], yyv[yysp-3], yyv[yysp-1]);
         
       break;
							case   56 : 
         //yyval = yyv[yysp-2] + yyv[yysp-1] + yyv[yysp-0];
         yyval = Objects.AddObject(yyv[yysp-2], yyv[yysp-1]);
         
       break;
							case   57 : 
         yyval = yyv[yysp-0];
         
       break;
							case   58 : 
         yyval = Formatter.FormatObject(yyv[yysp-0]);
         
       break;
							case   59 : 
         yyval = Formatter.FormatObject(yyv[yysp-0]);
         
       break;
							case   60 : 
         //yyval = yyv[yysp-2] + yyv[yysp-1] + yyv[yysp-0];
         yyval = yyv[yysp-2] + yyv[yysp-0];
         
       break;
							case   61 : 
         yyval = yyv[yysp-1] + yyv[yysp-0];
         
       break;
							case   62 : 
         yyval = "";
         
       break;
							case   63 : 
         //yyval = yyv[yysp-2] + yyv[yysp-1] + yyv[yysp-0];
         yyval = Objects.CreateProperty(yyv[yysp-2]);
         
       break;
							case   64 : 
         Objects.AddPropertyValue(yyv[yysp-1]);
         yyval = "";
         
       break;
							case   65 : 
         Objects.AddPropertyValue(yyv[yysp-2]);
         yyval = "";
         
       break;
							case   66 : 
         yyval = yyv[yysp-2] + "." + yyv[yysp-0];
         
       break;
							case   67 : 
         yyval = yyv[yysp-2] + "." + yyv[yysp-0];
         
       break;
							case   68 : 
         yyval = yyv[yysp-2] + "." + yyv[yysp-0];
         
       break;
							case   69 : 
         yyval = yyv[yysp-2] + "." + yyv[yysp-0];
         
       break;
							case   70 : 
         yyval = yyv[yysp-0];
         
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
         yyval = yyv[yysp-0];
         
       break;
							case   76 : 
         yyval = Formatter.FormatPropertyValue(yyv[yysp-0]);
         
       break;
							case   77 : 
         //yyval = yyv[yysp-5] + yyv[yysp-4] + yyv[yysp-3] + yyv[yysp-2] + yyv[yysp-1] + yyv[yysp-0];
         yyval = Actions.AddAction(yyv[yysp-4], yyv[yysp-1]);
         
       break;
							case   78 : 
         yyval = yyv[yysp-0];
         
       break;
							case   79 : 
         //yyval = yyv[yysp-2] + yyv[yysp-1] + yyv[yysp-0];
         yyval = Actions.CreateMarker(yyv[yysp-2], yyv[yysp-0]);
         
       break;
							case   80 : 
         //yyval = yyv[yysp-3] + yyv[yysp-2] + yyv[yysp-1] + yyv[yysp-0];
         yyval = yyv[yysp-2] + yyv[yysp-0];
         
       break;
							case   81 : 
         //yyval = yyv[yysp-2] + yyv[yysp-1] + yyv[yysp-0];
         yyval = Actions.CreateMarker(yyv[yysp-2], yyv[yysp-0]);
         
       break;
							case   82 : 
         //yyval = yyv[yysp-2] + yyv[yysp-1] + yyv[yysp-0];
         yyval = yyv[yysp-2] + yyv[yysp-0];
         
       break;
							case   83 : 
         yyval = yyv[yysp-1] + yyv[yysp-0];
         
       break;
							case   84 : 
         yyval = yyv[yysp-1] + yyv[yysp-0];
         
       break;
							case   85 : 
         yyval = "";
         
       break;
							case   86 : 
         yyval = yyv[yysp-0];
         
       break;
							case   87 : 
         //yyval = yyv[yysp-2] + yyv[yysp-1] + yyv[yysp-0];
         yyval = Actions.CreateInstruction(yyv[yysp-2]);
         
       break;
							case   88 : 
         //yyval = yyv[yysp-0];
         yyval = Actions.CreateInstruction(yyv[yysp-0]);
         
       break;
							case   89 : 
         //yyval = yyv[yysp-1] + yyv[yysp-0];
         yyval = "";
         Actions.AddInstructionParameter(yyv[yysp-1]);
         
       break;
							case   90 : 
         yyval = yyv[yysp-2] + yyv[yysp-1] + yyv[yysp-0];
         yyval = "";
         Actions.AddInstructionParameter(yyv[yysp-2]);
         
       break;
							case   91 : 
         yyval = yyv[yysp-0];
         
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
         yyval = "";
         
       break;
							case   98 : 
         yyval = yyv[yysp-0];
         
       break;
							case   99 : 
         yyval = yyv[yysp-2] + " || " + yyv[yysp-0];
         
       break;
							case  100 : 
         yyval = yyv[yysp-0];
         
       break;
							case  101 : 
         yyval = yyv[yysp-2] + " && " + yyv[yysp-0];
         
       break;
							case  102 : 
         yyval = yyv[yysp-0];
         
       break;
							case  103 : 
         yyval = yyv[yysp-2] + " | " + yyv[yysp-0];
         
       break;
							case  104 : 
         yyval = yyv[yysp-0];
         
       break;
							case  105 : 
         yyval = yyv[yysp-2] + " ^ " + yyv[yysp-0];
         
       break;
							case  106 : 
         yyval = yyv[yysp-0];
         
       break;
							case  107 : 
         yyval = yyv[yysp-2] + " & " + yyv[yysp-0];
         
       break;
							case  108 : 
         yyval = yyv[yysp-0];
         
       break;
							case  109 : 
         yyval = yyv[yysp-0];
         
       break;
							case  110 : 
         yyval = yyv[yysp-2] + yyv[yysp-1] + yyv[yysp-0];
         
       break;
							case  111 : 
         yyval = yyv[yysp-0];
         
       break;
							case  112 : 
         yyval = yyv[yysp-2] + yyv[yysp-1] + yyv[yysp-0];
         
       break;
							case  113 : 
         yyval = yyv[yysp-0];
         
       break;
							case  114 : 
         yyval = yyv[yysp-2] + yyv[yysp-1] + yyv[yysp-0];
         
       break;
							case  115 : 
         yyval = yyv[yysp-0];
         
       break;
							case  116 : 
         yyval = yyv[yysp-2] + yyv[yysp-1] + yyv[yysp-0];
         
       break;
							case  117 : 
         yyval = yyv[yysp-0];
         
       break;
							case  118 : 
         yyval = yyv[yysp-1] + yyv[yysp-0];
         
       break;
							case  119 : 
         yyval = Formatter.FormatMath(yyv[yysp-3]) + "(" + yyv[yysp-1] + ")";
         
       break;
							case  120 : 
         yyval = "(" + yyv[yysp-1] + ")";
         
       break;
							case  121 : 
         yyval = yyv[yysp-0];
         
       break;
							case  122 : 
         yyval = Formatter.FormatTargetSkill(yyv[yysp-0]);
         
       break;
							case  123 : 
         yyval = " != ";
         
       break;
							case  124 : 
         yyval = " == ";
         
       break;
							case  125 : 
         yyval = " < ";
         
       break;
							case  126 : 
         yyval = " <= ";
         
       break;
							case  127 : 
         yyval = " > ";
         
       break;
							case  128 : 
         yyval = " >= ";
         
       break;
							case  129 : 
         yyval = " + ";
         
       break;
							case  130 : 
         yyval = " - ";
         
       break;
							case  131 : 
         yyval = " % ";
         
       break;
							case  132 : 
         yyval = " * ";
         
       break;
							case  133 : 
         yyval = " / ";
         
       break;
							case  134 : 
         yyval = "!";
         
       break;
							case  135 : 
         yyval = "+";
         
       break;
							case  136 : 
         yyval = "-";
         
       break;
							case  137 : 
         //yyval = yyv[yysp-1] + yyv[yysp-0];
         yyval = Actions.CreateExpression(yyv[yysp-0]);
         
       break;
							case  138 : 
         //yyval = yyv[yysp-3] + yyv[yysp-2] + yyv[yysp-1] + yyv[yysp-0];
         yyval = Actions.CreateExpression(Formatter.FormatExpression(yyv[yysp-2], yyv[yysp-1], yyv[yysp-0]));
         
       break;
							case  139 : 
         //yyval = yyv[yysp-2] + yyv[yysp-1] + yyv[yysp-0];
         yyval = Actions.CreateExpression(Formatter.FormatExpression(yyv[yysp-2], yyv[yysp-1], yyv[yysp-0]));
         
       break;
							case  140 : 
         yyval = " *= ";
         
       break;
							case  141 : 
         yyval = " += ";
         
       break;
							case  142 : 
         yyval = " -= ";
         
       break;
							case  143 : 
         yyval = " /= ";
         
       break;
							case  144 : 
         yyval = " = ";
         
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
         //yyval = yyv[yysp-3] + yyv[yysp-2] + yyv[yysp-1] + yyv[yysp-0];
         yyval = Actions.CreateElseCondition(yyv[yysp-1]);
         
       break;
							case  149 : 
         //yyval = yyv[yysp-4] + yyv[yysp-3] + yyv[yysp-2] + yyv[yysp-1] + yyv[yysp-0];
         yyval = Actions.CreateIfCondition(yyv[yysp-3], yyv[yysp-1]);
         
       break;
							case  150 : 
         //yyval = yyv[yysp-4] + yyv[yysp-3] + yyv[yysp-2] + yyv[yysp-1] + yyv[yysp-0];
         yyval = Actions.CreateWhileCondition(yyv[yysp-3], yyv[yysp-1]);
         
       break;
							case  151 : 
         yyval = yyv[yysp-2] + "." + yyv[yysp-0];
         
       break;
							case  152 : 
         yyval = yyv[yysp-2] + "." + yyv[yysp-0];
         
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
         yyval = Formatter.FormatObjectId(Formatter.FormatIdentifier(yyv[yysp-0]));
         
       break;
							case  157 : 
         yyval = Formatter.FormatObjectId(yyv[yysp-0]);
         
       break;
							case  158 : 
         yyval = yyv[yysp-0];
         
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
         yyval = yyv[yysp-0];
         
       break;
							case  177 : 
         yyval = yyv[yysp-0];
         
       break;
							case  178 : 
         yyval = yyv[yysp-0];
         
       break;
							case  179 : 
         yyval = Formatter.FormatNumber(yyv[yysp-0]);
         
       break;
							case  180 : 
         yyval = Formatter.FormatNumber(yyv[yysp-0]);
         
       break;
							case  181 : 
         yyval = yyv[yysp-0];
         
       break;
							case  182 : 
         yyval = yyv[yysp-0];
         
       break;
							case  183 : 
         yyval = yyv[yysp-1] + yyv[yysp-0];
         
       break;
							case  184 : 
         yyval = yyv[yysp-0];
         
       break;
							case  185 : 
         yyval = yyv[yysp-1] + yyv[yysp-0];
         
       break;
							case  186 : 
         yyval = yyv[yysp-0];
         
       break;
							case  187 : 
         yyval = Formatter.FormatIdentifier(yyv[yysp-0]);
         
       break;
							case  188 : 
         yyval = Formatter.FormatList(yyv[yysp-0]);
         
       break;
							case  189 : 
         yyval = Formatter.FormatAssetId(yyv[yysp-0]);
         
       break;
							case  190 : 
         yyval = yyv[yysp-0];
         
       break;
							case  191 : 
         yyval = Formatter.FormatSkill(yyv[yysp-0]);
         
       break;
							case  192 : 
         yyval = Formatter.FormatSkill(yyv[yysp-0]);
         
       break;
							case  193 : 
         yyval = Formatter.FormatGlobal(yyv[yysp-0]);
         
       break;
							case  194 : 
         yyval = Formatter.FormatGlobal(yyv[yysp-0]);
         
       break;
							case  195 : 
         yyval = Formatter.FormatGlobal(yyv[yysp-0]);
         
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
         yyval = Formatter.FormatIdentifier(yyv[yysp-0]);
         
       break;
							case  200 : 
         yyval = Formatter.FormatIdentifier(yyv[yysp-0]);
         
       break;
							case  201 : 
         yyval = Formatter.FormatIdentifier(yyv[yysp-0]);
         
       break;
							case  202 : 
         yyval = Formatter.FormatIdentifier(yyv[yysp-0]);
         
       break;
							case  203 : 
         yyval = yyv[yysp-0];
         
       break;
							case  204 : 
         yyval = Formatter.FormatNull();
         
       break;
							case  205 : 
         //yyval = yyv[yysp-0];
         yyval = Formatter.FormatPatchFunction(yyv[yysp-0]);
         
       break;
							case  206 : 
         //yyval = yyv[yysp-0];
         yyval = Formatter.FormatFunction(yyv[yysp-0]);
         
       break;
							case  207 : 
         yyval = yyv[yysp-0];
         
       break;
							case  208 : 
         yyval = yyv[yysp-0];
         
       break;
							case  209 : 
         yyval = Formatter.FormatFile(yyv[yysp-0]);
         
       break;
							case  210 : 
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

					int yynacts   = 2124;
					int yyngotos  = 913;
					int yynstates = 332;
					int yynrules  = 210;
					yya = new YYARec[yynacts+1];  int yyac = 1;
					yyg = new YYARec[yyngotos+1]; int yygc = 1;
					yyr = new YYRRec[yynrules+1]; int yyrc = 1;

					yya[yyac] = new YYARec(257,18);yyac++; 
					yya[yyac] = new YYARec(259,19);yyac++; 
					yya[yyac] = new YYARec(262,20);yyac++; 
					yya[yyac] = new YYARec(264,21);yyac++; 
					yya[yyac] = new YYARec(265,22);yyac++; 
					yya[yyac] = new YYARec(267,23);yyac++; 
					yya[yyac] = new YYARec(299,24);yyac++; 
					yya[yyac] = new YYARec(300,25);yyac++; 
					yya[yyac] = new YYARec(301,26);yyac++; 
					yya[yyac] = new YYARec(302,27);yyac++; 
					yya[yyac] = new YYARec(303,28);yyac++; 
					yya[yyac] = new YYARec(309,29);yyac++; 
					yya[yyac] = new YYARec(311,30);yyac++; 
					yya[yyac] = new YYARec(312,31);yyac++; 
					yya[yyac] = new YYARec(313,32);yyac++; 
					yya[yyac] = new YYARec(317,33);yyac++; 
					yya[yyac] = new YYARec(319,34);yyac++; 
					yya[yyac] = new YYARec(0,-3 );yyac++; 
					yya[yyac] = new YYARec(298,42);yyac++; 
					yya[yyac] = new YYARec(300,43);yyac++; 
					yya[yyac] = new YYARec(301,26);yyac++; 
					yya[yyac] = new YYARec(302,27);yyac++; 
					yya[yyac] = new YYARec(304,44);yyac++; 
					yya[yyac] = new YYARec(305,45);yyac++; 
					yya[yyac] = new YYARec(306,46);yyac++; 
					yya[yyac] = new YYARec(307,47);yyac++; 
					yya[yyac] = new YYARec(309,29);yyac++; 
					yya[yyac] = new YYARec(314,48);yyac++; 
					yya[yyac] = new YYARec(315,49);yyac++; 
					yya[yyac] = new YYARec(317,33);yyac++; 
					yya[yyac] = new YYARec(318,50);yyac++; 
					yya[yyac] = new YYARec(320,51);yyac++; 
					yya[yyac] = new YYARec(323,52);yyac++; 
					yya[yyac] = new YYARec(298,42);yyac++; 
					yya[yyac] = new YYARec(300,43);yyac++; 
					yya[yyac] = new YYARec(301,26);yyac++; 
					yya[yyac] = new YYARec(304,44);yyac++; 
					yya[yyac] = new YYARec(305,45);yyac++; 
					yya[yyac] = new YYARec(306,46);yyac++; 
					yya[yyac] = new YYARec(307,47);yyac++; 
					yya[yyac] = new YYARec(309,29);yyac++; 
					yya[yyac] = new YYARec(314,48);yyac++; 
					yya[yyac] = new YYARec(315,49);yyac++; 
					yya[yyac] = new YYARec(317,33);yyac++; 
					yya[yyac] = new YYARec(318,50);yyac++; 
					yya[yyac] = new YYARec(320,51);yyac++; 
					yya[yyac] = new YYARec(323,52);yyac++; 
					yya[yyac] = new YYARec(298,42);yyac++; 
					yya[yyac] = new YYARec(300,43);yyac++; 
					yya[yyac] = new YYARec(301,26);yyac++; 
					yya[yyac] = new YYARec(304,44);yyac++; 
					yya[yyac] = new YYARec(305,45);yyac++; 
					yya[yyac] = new YYARec(306,46);yyac++; 
					yya[yyac] = new YYARec(307,47);yyac++; 
					yya[yyac] = new YYARec(309,29);yyac++; 
					yya[yyac] = new YYARec(314,48);yyac++; 
					yya[yyac] = new YYARec(315,49);yyac++; 
					yya[yyac] = new YYARec(317,33);yyac++; 
					yya[yyac] = new YYARec(318,50);yyac++; 
					yya[yyac] = new YYARec(320,51);yyac++; 
					yya[yyac] = new YYARec(321,56);yyac++; 
					yya[yyac] = new YYARec(323,52);yyac++; 
					yya[yyac] = new YYARec(263,58);yyac++; 
					yya[yyac] = new YYARec(282,-97 );yyac++; 
					yya[yyac] = new YYARec(283,-97 );yyac++; 
					yya[yyac] = new YYARec(287,-97 );yyac++; 
					yya[yyac] = new YYARec(321,-97 );yyac++; 
					yya[yyac] = new YYARec(322,-97 );yyac++; 
					yya[yyac] = new YYARec(323,-97 );yyac++; 
					yya[yyac] = new YYARec(324,-97 );yyac++; 
					yya[yyac] = new YYARec(325,-97 );yyac++; 
					yya[yyac] = new YYARec(263,58);yyac++; 
					yya[yyac] = new YYARec(298,-97 );yyac++; 
					yya[yyac] = new YYARec(300,-97 );yyac++; 
					yya[yyac] = new YYARec(301,-97 );yyac++; 
					yya[yyac] = new YYARec(304,-97 );yyac++; 
					yya[yyac] = new YYARec(305,-97 );yyac++; 
					yya[yyac] = new YYARec(306,-97 );yyac++; 
					yya[yyac] = new YYARec(307,-97 );yyac++; 
					yya[yyac] = new YYARec(309,-97 );yyac++; 
					yya[yyac] = new YYARec(314,-97 );yyac++; 
					yya[yyac] = new YYARec(315,-97 );yyac++; 
					yya[yyac] = new YYARec(317,-97 );yyac++; 
					yya[yyac] = new YYARec(318,-97 );yyac++; 
					yya[yyac] = new YYARec(320,-97 );yyac++; 
					yya[yyac] = new YYARec(323,-97 );yyac++; 
					yya[yyac] = new YYARec(257,18);yyac++; 
					yya[yyac] = new YYARec(259,19);yyac++; 
					yya[yyac] = new YYARec(262,20);yyac++; 
					yya[yyac] = new YYARec(264,21);yyac++; 
					yya[yyac] = new YYARec(265,22);yyac++; 
					yya[yyac] = new YYARec(267,23);yyac++; 
					yya[yyac] = new YYARec(299,24);yyac++; 
					yya[yyac] = new YYARec(300,25);yyac++; 
					yya[yyac] = new YYARec(301,26);yyac++; 
					yya[yyac] = new YYARec(302,27);yyac++; 
					yya[yyac] = new YYARec(303,28);yyac++; 
					yya[yyac] = new YYARec(309,29);yyac++; 
					yya[yyac] = new YYARec(311,30);yyac++; 
					yya[yyac] = new YYARec(312,31);yyac++; 
					yya[yyac] = new YYARec(313,32);yyac++; 
					yya[yyac] = new YYARec(317,33);yyac++; 
					yya[yyac] = new YYARec(319,34);yyac++; 
					yya[yyac] = new YYARec(0,-3 );yyac++; 
					yya[yyac] = new YYARec(260,-3 );yyac++; 
					yya[yyac] = new YYARec(261,-3 );yyac++; 
					yya[yyac] = new YYARec(0,0);yyac++; 
					yya[yyac] = new YYARec(309,29);yyac++; 
					yya[yyac] = new YYARec(317,33);yyac++; 
					yya[yyac] = new YYARec(323,64);yyac++; 
					yya[yyac] = new YYARec(309,29);yyac++; 
					yya[yyac] = new YYARec(317,33);yyac++; 
					yya[yyac] = new YYARec(323,64);yyac++; 
					yya[yyac] = new YYARec(309,29);yyac++; 
					yya[yyac] = new YYARec(317,33);yyac++; 
					yya[yyac] = new YYARec(323,64);yyac++; 
					yya[yyac] = new YYARec(309,29);yyac++; 
					yya[yyac] = new YYARec(317,33);yyac++; 
					yya[yyac] = new YYARec(323,64);yyac++; 
					yya[yyac] = new YYARec(324,69);yyac++; 
					yya[yyac] = new YYARec(263,58);yyac++; 
					yya[yyac] = new YYARec(266,-97 );yyac++; 
					yya[yyac] = new YYARec(258,72);yyac++; 
					yya[yyac] = new YYARec(263,58);yyac++; 
					yya[yyac] = new YYARec(266,-97 );yyac++; 
					yya[yyac] = new YYARec(325,-97 );yyac++; 
					yya[yyac] = new YYARec(263,58);yyac++; 
					yya[yyac] = new YYARec(324,-97 );yyac++; 
					yya[yyac] = new YYARec(282,82);yyac++; 
					yya[yyac] = new YYARec(283,83);yyac++; 
					yya[yyac] = new YYARec(287,84);yyac++; 
					yya[yyac] = new YYARec(321,85);yyac++; 
					yya[yyac] = new YYARec(322,86);yyac++; 
					yya[yyac] = new YYARec(323,64);yyac++; 
					yya[yyac] = new YYARec(324,69);yyac++; 
					yya[yyac] = new YYARec(325,87);yyac++; 
					yya[yyac] = new YYARec(298,42);yyac++; 
					yya[yyac] = new YYARec(300,43);yyac++; 
					yya[yyac] = new YYARec(301,26);yyac++; 
					yya[yyac] = new YYARec(304,44);yyac++; 
					yya[yyac] = new YYARec(305,45);yyac++; 
					yya[yyac] = new YYARec(306,46);yyac++; 
					yya[yyac] = new YYARec(307,47);yyac++; 
					yya[yyac] = new YYARec(309,29);yyac++; 
					yya[yyac] = new YYARec(314,48);yyac++; 
					yya[yyac] = new YYARec(315,49);yyac++; 
					yya[yyac] = new YYARec(317,33);yyac++; 
					yya[yyac] = new YYARec(318,50);yyac++; 
					yya[yyac] = new YYARec(320,51);yyac++; 
					yya[yyac] = new YYARec(323,52);yyac++; 
					yya[yyac] = new YYARec(258,90);yyac++; 
					yya[yyac] = new YYARec(258,91);yyac++; 
					yya[yyac] = new YYARec(258,92);yyac++; 
					yya[yyac] = new YYARec(263,93);yyac++; 
					yya[yyac] = new YYARec(258,94);yyac++; 
					yya[yyac] = new YYARec(258,95);yyac++; 
					yya[yyac] = new YYARec(266,96);yyac++; 
					yya[yyac] = new YYARec(266,98);yyac++; 
					yya[yyac] = new YYARec(325,87);yyac++; 
					yya[yyac] = new YYARec(324,69);yyac++; 
					yya[yyac] = new YYARec(321,100);yyac++; 
					yya[yyac] = new YYARec(322,101);yyac++; 
					yya[yyac] = new YYARec(258,102);yyac++; 
					yya[yyac] = new YYARec(263,58);yyac++; 
					yya[yyac] = new YYARec(258,-97 );yyac++; 
					yya[yyac] = new YYARec(298,-97 );yyac++; 
					yya[yyac] = new YYARec(300,-97 );yyac++; 
					yya[yyac] = new YYARec(301,-97 );yyac++; 
					yya[yyac] = new YYARec(304,-97 );yyac++; 
					yya[yyac] = new YYARec(305,-97 );yyac++; 
					yya[yyac] = new YYARec(306,-97 );yyac++; 
					yya[yyac] = new YYARec(307,-97 );yyac++; 
					yya[yyac] = new YYARec(309,-97 );yyac++; 
					yya[yyac] = new YYARec(314,-97 );yyac++; 
					yya[yyac] = new YYARec(315,-97 );yyac++; 
					yya[yyac] = new YYARec(317,-97 );yyac++; 
					yya[yyac] = new YYARec(318,-97 );yyac++; 
					yya[yyac] = new YYARec(320,-97 );yyac++; 
					yya[yyac] = new YYARec(323,-97 );yyac++; 
					yya[yyac] = new YYARec(258,104);yyac++; 
					yya[yyac] = new YYARec(257,18);yyac++; 
					yya[yyac] = new YYARec(259,19);yyac++; 
					yya[yyac] = new YYARec(262,20);yyac++; 
					yya[yyac] = new YYARec(264,21);yyac++; 
					yya[yyac] = new YYARec(265,22);yyac++; 
					yya[yyac] = new YYARec(267,23);yyac++; 
					yya[yyac] = new YYARec(299,24);yyac++; 
					yya[yyac] = new YYARec(300,25);yyac++; 
					yya[yyac] = new YYARec(301,26);yyac++; 
					yya[yyac] = new YYARec(302,27);yyac++; 
					yya[yyac] = new YYARec(303,28);yyac++; 
					yya[yyac] = new YYARec(309,29);yyac++; 
					yya[yyac] = new YYARec(311,30);yyac++; 
					yya[yyac] = new YYARec(312,31);yyac++; 
					yya[yyac] = new YYARec(313,32);yyac++; 
					yya[yyac] = new YYARec(317,33);yyac++; 
					yya[yyac] = new YYARec(319,34);yyac++; 
					yya[yyac] = new YYARec(260,-3 );yyac++; 
					yya[yyac] = new YYARec(261,-3 );yyac++; 
					yya[yyac] = new YYARec(257,18);yyac++; 
					yya[yyac] = new YYARec(259,19);yyac++; 
					yya[yyac] = new YYARec(262,20);yyac++; 
					yya[yyac] = new YYARec(264,21);yyac++; 
					yya[yyac] = new YYARec(265,22);yyac++; 
					yya[yyac] = new YYARec(267,23);yyac++; 
					yya[yyac] = new YYARec(299,24);yyac++; 
					yya[yyac] = new YYARec(300,25);yyac++; 
					yya[yyac] = new YYARec(301,26);yyac++; 
					yya[yyac] = new YYARec(302,27);yyac++; 
					yya[yyac] = new YYARec(303,28);yyac++; 
					yya[yyac] = new YYARec(309,29);yyac++; 
					yya[yyac] = new YYARec(311,30);yyac++; 
					yya[yyac] = new YYARec(312,31);yyac++; 
					yya[yyac] = new YYARec(313,32);yyac++; 
					yya[yyac] = new YYARec(317,33);yyac++; 
					yya[yyac] = new YYARec(319,34);yyac++; 
					yya[yyac] = new YYARec(260,-3 );yyac++; 
					yya[yyac] = new YYARec(261,-3 );yyac++; 
					yya[yyac] = new YYARec(282,82);yyac++; 
					yya[yyac] = new YYARec(283,83);yyac++; 
					yya[yyac] = new YYARec(287,84);yyac++; 
					yya[yyac] = new YYARec(298,42);yyac++; 
					yya[yyac] = new YYARec(299,24);yyac++; 
					yya[yyac] = new YYARec(300,43);yyac++; 
					yya[yyac] = new YYARec(301,26);yyac++; 
					yya[yyac] = new YYARec(302,27);yyac++; 
					yya[yyac] = new YYARec(304,44);yyac++; 
					yya[yyac] = new YYARec(305,45);yyac++; 
					yya[yyac] = new YYARec(306,46);yyac++; 
					yya[yyac] = new YYARec(307,47);yyac++; 
					yya[yyac] = new YYARec(308,118);yyac++; 
					yya[yyac] = new YYARec(309,29);yyac++; 
					yya[yyac] = new YYARec(310,119);yyac++; 
					yya[yyac] = new YYARec(312,31);yyac++; 
					yya[yyac] = new YYARec(313,32);yyac++; 
					yya[yyac] = new YYARec(314,48);yyac++; 
					yya[yyac] = new YYARec(315,49);yyac++; 
					yya[yyac] = new YYARec(316,120);yyac++; 
					yya[yyac] = new YYARec(317,33);yyac++; 
					yya[yyac] = new YYARec(318,50);yyac++; 
					yya[yyac] = new YYARec(319,121);yyac++; 
					yya[yyac] = new YYARec(320,51);yyac++; 
					yya[yyac] = new YYARec(321,85);yyac++; 
					yya[yyac] = new YYARec(322,86);yyac++; 
					yya[yyac] = new YYARec(323,52);yyac++; 
					yya[yyac] = new YYARec(324,69);yyac++; 
					yya[yyac] = new YYARec(325,87);yyac++; 
					yya[yyac] = new YYARec(257,132);yyac++; 
					yya[yyac] = new YYARec(259,133);yyac++; 
					yya[yyac] = new YYARec(266,134);yyac++; 
					yya[yyac] = new YYARec(288,135);yyac++; 
					yya[yyac] = new YYARec(294,136);yyac++; 
					yya[yyac] = new YYARec(295,137);yyac++; 
					yya[yyac] = new YYARec(296,138);yyac++; 
					yya[yyac] = new YYARec(298,42);yyac++; 
					yya[yyac] = new YYARec(299,24);yyac++; 
					yya[yyac] = new YYARec(300,43);yyac++; 
					yya[yyac] = new YYARec(301,26);yyac++; 
					yya[yyac] = new YYARec(302,27);yyac++; 
					yya[yyac] = new YYARec(304,44);yyac++; 
					yya[yyac] = new YYARec(305,45);yyac++; 
					yya[yyac] = new YYARec(306,46);yyac++; 
					yya[yyac] = new YYARec(307,47);yyac++; 
					yya[yyac] = new YYARec(309,29);yyac++; 
					yya[yyac] = new YYARec(310,139);yyac++; 
					yya[yyac] = new YYARec(312,31);yyac++; 
					yya[yyac] = new YYARec(313,32);yyac++; 
					yya[yyac] = new YYARec(314,140);yyac++; 
					yya[yyac] = new YYARec(315,49);yyac++; 
					yya[yyac] = new YYARec(316,120);yyac++; 
					yya[yyac] = new YYARec(317,33);yyac++; 
					yya[yyac] = new YYARec(318,50);yyac++; 
					yya[yyac] = new YYARec(319,121);yyac++; 
					yya[yyac] = new YYARec(320,51);yyac++; 
					yya[yyac] = new YYARec(323,141);yyac++; 
					yya[yyac] = new YYARec(267,-85 );yyac++; 
					yya[yyac] = new YYARec(258,142);yyac++; 
					yya[yyac] = new YYARec(257,150);yyac++; 
					yya[yyac] = new YYARec(259,151);yyac++; 
					yya[yyac] = new YYARec(301,26);yyac++; 
					yya[yyac] = new YYARec(302,27);yyac++; 
					yya[yyac] = new YYARec(306,152);yyac++; 
					yya[yyac] = new YYARec(311,153);yyac++; 
					yya[yyac] = new YYARec(312,154);yyac++; 
					yya[yyac] = new YYARec(315,155);yyac++; 
					yya[yyac] = new YYARec(317,156);yyac++; 
					yya[yyac] = new YYARec(319,157);yyac++; 
					yya[yyac] = new YYARec(320,158);yyac++; 
					yya[yyac] = new YYARec(323,64);yyac++; 
					yya[yyac] = new YYARec(267,-62 );yyac++; 
					yya[yyac] = new YYARec(263,58);yyac++; 
					yya[yyac] = new YYARec(258,-97 );yyac++; 
					yya[yyac] = new YYARec(282,-97 );yyac++; 
					yya[yyac] = new YYARec(283,-97 );yyac++; 
					yya[yyac] = new YYARec(287,-97 );yyac++; 
					yya[yyac] = new YYARec(321,-97 );yyac++; 
					yya[yyac] = new YYARec(298,42);yyac++; 
					yya[yyac] = new YYARec(300,43);yyac++; 
					yya[yyac] = new YYARec(301,26);yyac++; 
					yya[yyac] = new YYARec(304,44);yyac++; 
					yya[yyac] = new YYARec(305,45);yyac++; 
					yya[yyac] = new YYARec(306,46);yyac++; 
					yya[yyac] = new YYARec(307,47);yyac++; 
					yya[yyac] = new YYARec(309,29);yyac++; 
					yya[yyac] = new YYARec(314,48);yyac++; 
					yya[yyac] = new YYARec(315,49);yyac++; 
					yya[yyac] = new YYARec(317,33);yyac++; 
					yya[yyac] = new YYARec(318,50);yyac++; 
					yya[yyac] = new YYARec(320,51);yyac++; 
					yya[yyac] = new YYARec(323,52);yyac++; 
					yya[yyac] = new YYARec(258,-43 );yyac++; 
					yya[yyac] = new YYARec(258,161);yyac++; 
					yya[yyac] = new YYARec(260,162);yyac++; 
					yya[yyac] = new YYARec(261,163);yyac++; 
					yya[yyac] = new YYARec(258,164);yyac++; 
					yya[yyac] = new YYARec(297,165);yyac++; 
					yya[yyac] = new YYARec(258,-158 );yyac++; 
					yya[yyac] = new YYARec(263,-158 );yyac++; 
					yya[yyac] = new YYARec(266,-158 );yyac++; 
					yya[yyac] = new YYARec(269,-158 );yyac++; 
					yya[yyac] = new YYARec(270,-158 );yyac++; 
					yya[yyac] = new YYARec(271,-158 );yyac++; 
					yya[yyac] = new YYARec(272,-158 );yyac++; 
					yya[yyac] = new YYARec(273,-158 );yyac++; 
					yya[yyac] = new YYARec(275,-158 );yyac++; 
					yya[yyac] = new YYARec(276,-158 );yyac++; 
					yya[yyac] = new YYARec(277,-158 );yyac++; 
					yya[yyac] = new YYARec(278,-158 );yyac++; 
					yya[yyac] = new YYARec(279,-158 );yyac++; 
					yya[yyac] = new YYARec(280,-158 );yyac++; 
					yya[yyac] = new YYARec(281,-158 );yyac++; 
					yya[yyac] = new YYARec(282,-158 );yyac++; 
					yya[yyac] = new YYARec(283,-158 );yyac++; 
					yya[yyac] = new YYARec(284,-158 );yyac++; 
					yya[yyac] = new YYARec(285,-158 );yyac++; 
					yya[yyac] = new YYARec(286,-158 );yyac++; 
					yya[yyac] = new YYARec(287,-158 );yyac++; 
					yya[yyac] = new YYARec(289,-158 );yyac++; 
					yya[yyac] = new YYARec(290,-158 );yyac++; 
					yya[yyac] = new YYARec(291,-158 );yyac++; 
					yya[yyac] = new YYARec(292,-158 );yyac++; 
					yya[yyac] = new YYARec(293,-158 );yyac++; 
					yya[yyac] = new YYARec(298,-158 );yyac++; 
					yya[yyac] = new YYARec(299,-158 );yyac++; 
					yya[yyac] = new YYARec(300,-158 );yyac++; 
					yya[yyac] = new YYARec(301,-158 );yyac++; 
					yya[yyac] = new YYARec(302,-158 );yyac++; 
					yya[yyac] = new YYARec(304,-158 );yyac++; 
					yya[yyac] = new YYARec(305,-158 );yyac++; 
					yya[yyac] = new YYARec(306,-158 );yyac++; 
					yya[yyac] = new YYARec(307,-158 );yyac++; 
					yya[yyac] = new YYARec(308,-158 );yyac++; 
					yya[yyac] = new YYARec(309,-158 );yyac++; 
					yya[yyac] = new YYARec(310,-158 );yyac++; 
					yya[yyac] = new YYARec(312,-158 );yyac++; 
					yya[yyac] = new YYARec(313,-158 );yyac++; 
					yya[yyac] = new YYARec(314,-158 );yyac++; 
					yya[yyac] = new YYARec(315,-158 );yyac++; 
					yya[yyac] = new YYARec(316,-158 );yyac++; 
					yya[yyac] = new YYARec(317,-158 );yyac++; 
					yya[yyac] = new YYARec(318,-158 );yyac++; 
					yya[yyac] = new YYARec(319,-158 );yyac++; 
					yya[yyac] = new YYARec(320,-158 );yyac++; 
					yya[yyac] = new YYARec(321,-158 );yyac++; 
					yya[yyac] = new YYARec(322,-158 );yyac++; 
					yya[yyac] = new YYARec(323,-158 );yyac++; 
					yya[yyac] = new YYARec(324,-158 );yyac++; 
					yya[yyac] = new YYARec(325,-158 );yyac++; 
					yya[yyac] = new YYARec(297,166);yyac++; 
					yya[yyac] = new YYARec(258,-157 );yyac++; 
					yya[yyac] = new YYARec(263,-157 );yyac++; 
					yya[yyac] = new YYARec(266,-157 );yyac++; 
					yya[yyac] = new YYARec(269,-157 );yyac++; 
					yya[yyac] = new YYARec(270,-157 );yyac++; 
					yya[yyac] = new YYARec(271,-157 );yyac++; 
					yya[yyac] = new YYARec(272,-157 );yyac++; 
					yya[yyac] = new YYARec(273,-157 );yyac++; 
					yya[yyac] = new YYARec(275,-157 );yyac++; 
					yya[yyac] = new YYARec(276,-157 );yyac++; 
					yya[yyac] = new YYARec(277,-157 );yyac++; 
					yya[yyac] = new YYARec(278,-157 );yyac++; 
					yya[yyac] = new YYARec(279,-157 );yyac++; 
					yya[yyac] = new YYARec(280,-157 );yyac++; 
					yya[yyac] = new YYARec(281,-157 );yyac++; 
					yya[yyac] = new YYARec(282,-157 );yyac++; 
					yya[yyac] = new YYARec(283,-157 );yyac++; 
					yya[yyac] = new YYARec(284,-157 );yyac++; 
					yya[yyac] = new YYARec(285,-157 );yyac++; 
					yya[yyac] = new YYARec(286,-157 );yyac++; 
					yya[yyac] = new YYARec(287,-157 );yyac++; 
					yya[yyac] = new YYARec(289,-157 );yyac++; 
					yya[yyac] = new YYARec(290,-157 );yyac++; 
					yya[yyac] = new YYARec(291,-157 );yyac++; 
					yya[yyac] = new YYARec(292,-157 );yyac++; 
					yya[yyac] = new YYARec(293,-157 );yyac++; 
					yya[yyac] = new YYARec(298,-157 );yyac++; 
					yya[yyac] = new YYARec(299,-157 );yyac++; 
					yya[yyac] = new YYARec(300,-157 );yyac++; 
					yya[yyac] = new YYARec(301,-157 );yyac++; 
					yya[yyac] = new YYARec(302,-157 );yyac++; 
					yya[yyac] = new YYARec(304,-157 );yyac++; 
					yya[yyac] = new YYARec(305,-157 );yyac++; 
					yya[yyac] = new YYARec(306,-157 );yyac++; 
					yya[yyac] = new YYARec(307,-157 );yyac++; 
					yya[yyac] = new YYARec(308,-157 );yyac++; 
					yya[yyac] = new YYARec(309,-157 );yyac++; 
					yya[yyac] = new YYARec(310,-157 );yyac++; 
					yya[yyac] = new YYARec(312,-157 );yyac++; 
					yya[yyac] = new YYARec(313,-157 );yyac++; 
					yya[yyac] = new YYARec(314,-157 );yyac++; 
					yya[yyac] = new YYARec(315,-157 );yyac++; 
					yya[yyac] = new YYARec(316,-157 );yyac++; 
					yya[yyac] = new YYARec(317,-157 );yyac++; 
					yya[yyac] = new YYARec(318,-157 );yyac++; 
					yya[yyac] = new YYARec(319,-157 );yyac++; 
					yya[yyac] = new YYARec(320,-157 );yyac++; 
					yya[yyac] = new YYARec(321,-157 );yyac++; 
					yya[yyac] = new YYARec(322,-157 );yyac++; 
					yya[yyac] = new YYARec(323,-157 );yyac++; 
					yya[yyac] = new YYARec(324,-157 );yyac++; 
					yya[yyac] = new YYARec(325,-157 );yyac++; 
					yya[yyac] = new YYARec(258,167);yyac++; 
					yya[yyac] = new YYARec(258,-175 );yyac++; 
					yya[yyac] = new YYARec(263,-175 );yyac++; 
					yya[yyac] = new YYARec(282,-175 );yyac++; 
					yya[yyac] = new YYARec(283,-175 );yyac++; 
					yya[yyac] = new YYARec(287,-175 );yyac++; 
					yya[yyac] = new YYARec(298,-175 );yyac++; 
					yya[yyac] = new YYARec(299,-175 );yyac++; 
					yya[yyac] = new YYARec(300,-175 );yyac++; 
					yya[yyac] = new YYARec(301,-175 );yyac++; 
					yya[yyac] = new YYARec(302,-175 );yyac++; 
					yya[yyac] = new YYARec(304,-175 );yyac++; 
					yya[yyac] = new YYARec(305,-175 );yyac++; 
					yya[yyac] = new YYARec(306,-175 );yyac++; 
					yya[yyac] = new YYARec(307,-175 );yyac++; 
					yya[yyac] = new YYARec(308,-175 );yyac++; 
					yya[yyac] = new YYARec(309,-175 );yyac++; 
					yya[yyac] = new YYARec(310,-175 );yyac++; 
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
					yya[yyac] = new YYARec(268,-200 );yyac++; 
					yya[yyac] = new YYARec(289,-200 );yyac++; 
					yya[yyac] = new YYARec(290,-200 );yyac++; 
					yya[yyac] = new YYARec(291,-200 );yyac++; 
					yya[yyac] = new YYARec(292,-200 );yyac++; 
					yya[yyac] = new YYARec(293,-200 );yyac++; 
					yya[yyac] = new YYARec(297,-200 );yyac++; 
					yya[yyac] = new YYARec(263,58);yyac++; 
					yya[yyac] = new YYARec(258,-88 );yyac++; 
					yya[yyac] = new YYARec(282,-97 );yyac++; 
					yya[yyac] = new YYARec(283,-97 );yyac++; 
					yya[yyac] = new YYARec(287,-97 );yyac++; 
					yya[yyac] = new YYARec(298,-97 );yyac++; 
					yya[yyac] = new YYARec(299,-97 );yyac++; 
					yya[yyac] = new YYARec(300,-97 );yyac++; 
					yya[yyac] = new YYARec(301,-97 );yyac++; 
					yya[yyac] = new YYARec(302,-97 );yyac++; 
					yya[yyac] = new YYARec(304,-97 );yyac++; 
					yya[yyac] = new YYARec(305,-97 );yyac++; 
					yya[yyac] = new YYARec(306,-97 );yyac++; 
					yya[yyac] = new YYARec(307,-97 );yyac++; 
					yya[yyac] = new YYARec(308,-97 );yyac++; 
					yya[yyac] = new YYARec(309,-97 );yyac++; 
					yya[yyac] = new YYARec(310,-97 );yyac++; 
					yya[yyac] = new YYARec(312,-97 );yyac++; 
					yya[yyac] = new YYARec(313,-97 );yyac++; 
					yya[yyac] = new YYARec(314,-97 );yyac++; 
					yya[yyac] = new YYARec(315,-97 );yyac++; 
					yya[yyac] = new YYARec(316,-97 );yyac++; 
					yya[yyac] = new YYARec(317,-97 );yyac++; 
					yya[yyac] = new YYARec(318,-97 );yyac++; 
					yya[yyac] = new YYARec(319,-97 );yyac++; 
					yya[yyac] = new YYARec(320,-97 );yyac++; 
					yya[yyac] = new YYARec(321,-97 );yyac++; 
					yya[yyac] = new YYARec(322,-97 );yyac++; 
					yya[yyac] = new YYARec(323,-97 );yyac++; 
					yya[yyac] = new YYARec(324,-97 );yyac++; 
					yya[yyac] = new YYARec(325,-97 );yyac++; 
					yya[yyac] = new YYARec(257,132);yyac++; 
					yya[yyac] = new YYARec(259,133);yyac++; 
					yya[yyac] = new YYARec(266,134);yyac++; 
					yya[yyac] = new YYARec(288,135);yyac++; 
					yya[yyac] = new YYARec(294,136);yyac++; 
					yya[yyac] = new YYARec(295,137);yyac++; 
					yya[yyac] = new YYARec(296,138);yyac++; 
					yya[yyac] = new YYARec(298,42);yyac++; 
					yya[yyac] = new YYARec(299,24);yyac++; 
					yya[yyac] = new YYARec(300,43);yyac++; 
					yya[yyac] = new YYARec(301,26);yyac++; 
					yya[yyac] = new YYARec(302,27);yyac++; 
					yya[yyac] = new YYARec(304,44);yyac++; 
					yya[yyac] = new YYARec(305,45);yyac++; 
					yya[yyac] = new YYARec(306,46);yyac++; 
					yya[yyac] = new YYARec(307,47);yyac++; 
					yya[yyac] = new YYARec(309,29);yyac++; 
					yya[yyac] = new YYARec(310,139);yyac++; 
					yya[yyac] = new YYARec(312,31);yyac++; 
					yya[yyac] = new YYARec(313,32);yyac++; 
					yya[yyac] = new YYARec(314,140);yyac++; 
					yya[yyac] = new YYARec(315,49);yyac++; 
					yya[yyac] = new YYARec(316,120);yyac++; 
					yya[yyac] = new YYARec(317,33);yyac++; 
					yya[yyac] = new YYARec(318,50);yyac++; 
					yya[yyac] = new YYARec(319,121);yyac++; 
					yya[yyac] = new YYARec(320,51);yyac++; 
					yya[yyac] = new YYARec(323,141);yyac++; 
					yya[yyac] = new YYARec(260,-85 );yyac++; 
					yya[yyac] = new YYARec(261,-85 );yyac++; 
					yya[yyac] = new YYARec(267,-85 );yyac++; 
					yya[yyac] = new YYARec(258,170);yyac++; 
					yya[yyac] = new YYARec(268,171);yyac++; 
					yya[yyac] = new YYARec(297,166);yyac++; 
					yya[yyac] = new YYARec(289,-157 );yyac++; 
					yya[yyac] = new YYARec(290,-157 );yyac++; 
					yya[yyac] = new YYARec(291,-157 );yyac++; 
					yya[yyac] = new YYARec(292,-157 );yyac++; 
					yya[yyac] = new YYARec(293,-157 );yyac++; 
					yya[yyac] = new YYARec(268,-208 );yyac++; 
					yya[yyac] = new YYARec(289,173);yyac++; 
					yya[yyac] = new YYARec(290,174);yyac++; 
					yya[yyac] = new YYARec(291,175);yyac++; 
					yya[yyac] = new YYARec(292,176);yyac++; 
					yya[yyac] = new YYARec(293,177);yyac++; 
					yya[yyac] = new YYARec(267,178);yyac++; 
					yya[yyac] = new YYARec(257,132);yyac++; 
					yya[yyac] = new YYARec(259,133);yyac++; 
					yya[yyac] = new YYARec(266,134);yyac++; 
					yya[yyac] = new YYARec(288,135);yyac++; 
					yya[yyac] = new YYARec(294,136);yyac++; 
					yya[yyac] = new YYARec(295,137);yyac++; 
					yya[yyac] = new YYARec(296,138);yyac++; 
					yya[yyac] = new YYARec(298,42);yyac++; 
					yya[yyac] = new YYARec(299,24);yyac++; 
					yya[yyac] = new YYARec(300,43);yyac++; 
					yya[yyac] = new YYARec(301,26);yyac++; 
					yya[yyac] = new YYARec(302,27);yyac++; 
					yya[yyac] = new YYARec(304,44);yyac++; 
					yya[yyac] = new YYARec(305,45);yyac++; 
					yya[yyac] = new YYARec(306,46);yyac++; 
					yya[yyac] = new YYARec(307,47);yyac++; 
					yya[yyac] = new YYARec(309,29);yyac++; 
					yya[yyac] = new YYARec(310,139);yyac++; 
					yya[yyac] = new YYARec(312,31);yyac++; 
					yya[yyac] = new YYARec(313,32);yyac++; 
					yya[yyac] = new YYARec(314,140);yyac++; 
					yya[yyac] = new YYARec(315,49);yyac++; 
					yya[yyac] = new YYARec(316,120);yyac++; 
					yya[yyac] = new YYARec(317,33);yyac++; 
					yya[yyac] = new YYARec(318,50);yyac++; 
					yya[yyac] = new YYARec(319,121);yyac++; 
					yya[yyac] = new YYARec(320,51);yyac++; 
					yya[yyac] = new YYARec(323,141);yyac++; 
					yya[yyac] = new YYARec(260,-85 );yyac++; 
					yya[yyac] = new YYARec(261,-85 );yyac++; 
					yya[yyac] = new YYARec(267,-85 );yyac++; 
					yya[yyac] = new YYARec(309,29);yyac++; 
					yya[yyac] = new YYARec(317,33);yyac++; 
					yya[yyac] = new YYARec(323,64);yyac++; 
					yya[yyac] = new YYARec(309,29);yyac++; 
					yya[yyac] = new YYARec(317,33);yyac++; 
					yya[yyac] = new YYARec(323,64);yyac++; 
					yya[yyac] = new YYARec(257,132);yyac++; 
					yya[yyac] = new YYARec(259,133);yyac++; 
					yya[yyac] = new YYARec(266,134);yyac++; 
					yya[yyac] = new YYARec(288,135);yyac++; 
					yya[yyac] = new YYARec(294,136);yyac++; 
					yya[yyac] = new YYARec(295,137);yyac++; 
					yya[yyac] = new YYARec(296,138);yyac++; 
					yya[yyac] = new YYARec(298,42);yyac++; 
					yya[yyac] = new YYARec(299,24);yyac++; 
					yya[yyac] = new YYARec(300,43);yyac++; 
					yya[yyac] = new YYARec(301,26);yyac++; 
					yya[yyac] = new YYARec(302,27);yyac++; 
					yya[yyac] = new YYARec(304,44);yyac++; 
					yya[yyac] = new YYARec(305,45);yyac++; 
					yya[yyac] = new YYARec(306,46);yyac++; 
					yya[yyac] = new YYARec(307,47);yyac++; 
					yya[yyac] = new YYARec(309,29);yyac++; 
					yya[yyac] = new YYARec(310,139);yyac++; 
					yya[yyac] = new YYARec(312,31);yyac++; 
					yya[yyac] = new YYARec(313,32);yyac++; 
					yya[yyac] = new YYARec(314,140);yyac++; 
					yya[yyac] = new YYARec(315,49);yyac++; 
					yya[yyac] = new YYARec(316,120);yyac++; 
					yya[yyac] = new YYARec(317,33);yyac++; 
					yya[yyac] = new YYARec(318,50);yyac++; 
					yya[yyac] = new YYARec(319,121);yyac++; 
					yya[yyac] = new YYARec(320,51);yyac++; 
					yya[yyac] = new YYARec(323,141);yyac++; 
					yya[yyac] = new YYARec(267,-85 );yyac++; 
					yya[yyac] = new YYARec(274,199);yyac++; 
					yya[yyac] = new YYARec(282,82);yyac++; 
					yya[yyac] = new YYARec(283,83);yyac++; 
					yya[yyac] = new YYARec(287,84);yyac++; 
					yya[yyac] = new YYARec(298,42);yyac++; 
					yya[yyac] = new YYARec(299,24);yyac++; 
					yya[yyac] = new YYARec(300,43);yyac++; 
					yya[yyac] = new YYARec(301,26);yyac++; 
					yya[yyac] = new YYARec(302,27);yyac++; 
					yya[yyac] = new YYARec(304,44);yyac++; 
					yya[yyac] = new YYARec(305,45);yyac++; 
					yya[yyac] = new YYARec(306,46);yyac++; 
					yya[yyac] = new YYARec(307,47);yyac++; 
					yya[yyac] = new YYARec(309,29);yyac++; 
					yya[yyac] = new YYARec(310,119);yyac++; 
					yya[yyac] = new YYARec(312,31);yyac++; 
					yya[yyac] = new YYARec(313,32);yyac++; 
					yya[yyac] = new YYARec(314,48);yyac++; 
					yya[yyac] = new YYARec(315,49);yyac++; 
					yya[yyac] = new YYARec(316,120);yyac++; 
					yya[yyac] = new YYARec(317,33);yyac++; 
					yya[yyac] = new YYARec(318,50);yyac++; 
					yya[yyac] = new YYARec(319,121);yyac++; 
					yya[yyac] = new YYARec(320,51);yyac++; 
					yya[yyac] = new YYARec(321,200);yyac++; 
					yya[yyac] = new YYARec(322,201);yyac++; 
					yya[yyac] = new YYARec(323,52);yyac++; 
					yya[yyac] = new YYARec(266,202);yyac++; 
					yya[yyac] = new YYARec(258,-173 );yyac++; 
					yya[yyac] = new YYARec(263,-173 );yyac++; 
					yya[yyac] = new YYARec(282,-173 );yyac++; 
					yya[yyac] = new YYARec(283,-173 );yyac++; 
					yya[yyac] = new YYARec(287,-173 );yyac++; 
					yya[yyac] = new YYARec(298,-173 );yyac++; 
					yya[yyac] = new YYARec(299,-173 );yyac++; 
					yya[yyac] = new YYARec(300,-173 );yyac++; 
					yya[yyac] = new YYARec(301,-173 );yyac++; 
					yya[yyac] = new YYARec(302,-173 );yyac++; 
					yya[yyac] = new YYARec(304,-173 );yyac++; 
					yya[yyac] = new YYARec(305,-173 );yyac++; 
					yya[yyac] = new YYARec(306,-173 );yyac++; 
					yya[yyac] = new YYARec(307,-173 );yyac++; 
					yya[yyac] = new YYARec(308,-173 );yyac++; 
					yya[yyac] = new YYARec(309,-173 );yyac++; 
					yya[yyac] = new YYARec(310,-173 );yyac++; 
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
					yya[yyac] = new YYARec(274,199);yyac++; 
					yya[yyac] = new YYARec(282,82);yyac++; 
					yya[yyac] = new YYARec(283,83);yyac++; 
					yya[yyac] = new YYARec(287,84);yyac++; 
					yya[yyac] = new YYARec(298,42);yyac++; 
					yya[yyac] = new YYARec(299,24);yyac++; 
					yya[yyac] = new YYARec(300,43);yyac++; 
					yya[yyac] = new YYARec(301,26);yyac++; 
					yya[yyac] = new YYARec(302,27);yyac++; 
					yya[yyac] = new YYARec(304,44);yyac++; 
					yya[yyac] = new YYARec(305,45);yyac++; 
					yya[yyac] = new YYARec(306,46);yyac++; 
					yya[yyac] = new YYARec(307,47);yyac++; 
					yya[yyac] = new YYARec(309,29);yyac++; 
					yya[yyac] = new YYARec(310,119);yyac++; 
					yya[yyac] = new YYARec(312,31);yyac++; 
					yya[yyac] = new YYARec(313,32);yyac++; 
					yya[yyac] = new YYARec(314,48);yyac++; 
					yya[yyac] = new YYARec(315,49);yyac++; 
					yya[yyac] = new YYARec(316,120);yyac++; 
					yya[yyac] = new YYARec(317,33);yyac++; 
					yya[yyac] = new YYARec(318,50);yyac++; 
					yya[yyac] = new YYARec(319,121);yyac++; 
					yya[yyac] = new YYARec(320,51);yyac++; 
					yya[yyac] = new YYARec(321,200);yyac++; 
					yya[yyac] = new YYARec(322,201);yyac++; 
					yya[yyac] = new YYARec(323,52);yyac++; 
					yya[yyac] = new YYARec(274,199);yyac++; 
					yya[yyac] = new YYARec(282,82);yyac++; 
					yya[yyac] = new YYARec(283,83);yyac++; 
					yya[yyac] = new YYARec(287,84);yyac++; 
					yya[yyac] = new YYARec(298,42);yyac++; 
					yya[yyac] = new YYARec(299,24);yyac++; 
					yya[yyac] = new YYARec(300,43);yyac++; 
					yya[yyac] = new YYARec(301,26);yyac++; 
					yya[yyac] = new YYARec(302,27);yyac++; 
					yya[yyac] = new YYARec(304,44);yyac++; 
					yya[yyac] = new YYARec(305,45);yyac++; 
					yya[yyac] = new YYARec(306,46);yyac++; 
					yya[yyac] = new YYARec(307,47);yyac++; 
					yya[yyac] = new YYARec(309,29);yyac++; 
					yya[yyac] = new YYARec(310,119);yyac++; 
					yya[yyac] = new YYARec(312,31);yyac++; 
					yya[yyac] = new YYARec(313,32);yyac++; 
					yya[yyac] = new YYARec(314,48);yyac++; 
					yya[yyac] = new YYARec(315,49);yyac++; 
					yya[yyac] = new YYARec(316,120);yyac++; 
					yya[yyac] = new YYARec(317,33);yyac++; 
					yya[yyac] = new YYARec(318,50);yyac++; 
					yya[yyac] = new YYARec(319,121);yyac++; 
					yya[yyac] = new YYARec(320,51);yyac++; 
					yya[yyac] = new YYARec(321,200);yyac++; 
					yya[yyac] = new YYARec(322,201);yyac++; 
					yya[yyac] = new YYARec(323,52);yyac++; 
					yya[yyac] = new YYARec(289,-195 );yyac++; 
					yya[yyac] = new YYARec(290,-195 );yyac++; 
					yya[yyac] = new YYARec(291,-195 );yyac++; 
					yya[yyac] = new YYARec(292,-195 );yyac++; 
					yya[yyac] = new YYARec(293,-195 );yyac++; 
					yya[yyac] = new YYARec(297,-195 );yyac++; 
					yya[yyac] = new YYARec(268,-207 );yyac++; 
					yya[yyac] = new YYARec(268,-145 );yyac++; 
					yya[yyac] = new YYARec(289,-145 );yyac++; 
					yya[yyac] = new YYARec(290,-145 );yyac++; 
					yya[yyac] = new YYARec(291,-145 );yyac++; 
					yya[yyac] = new YYARec(292,-145 );yyac++; 
					yya[yyac] = new YYARec(293,-145 );yyac++; 
					yya[yyac] = new YYARec(297,-145 );yyac++; 
					yya[yyac] = new YYARec(258,-174 );yyac++; 
					yya[yyac] = new YYARec(263,-174 );yyac++; 
					yya[yyac] = new YYARec(282,-174 );yyac++; 
					yya[yyac] = new YYARec(283,-174 );yyac++; 
					yya[yyac] = new YYARec(287,-174 );yyac++; 
					yya[yyac] = new YYARec(298,-174 );yyac++; 
					yya[yyac] = new YYARec(299,-174 );yyac++; 
					yya[yyac] = new YYARec(300,-174 );yyac++; 
					yya[yyac] = new YYARec(301,-174 );yyac++; 
					yya[yyac] = new YYARec(302,-174 );yyac++; 
					yya[yyac] = new YYARec(304,-174 );yyac++; 
					yya[yyac] = new YYARec(305,-174 );yyac++; 
					yya[yyac] = new YYARec(306,-174 );yyac++; 
					yya[yyac] = new YYARec(307,-174 );yyac++; 
					yya[yyac] = new YYARec(308,-174 );yyac++; 
					yya[yyac] = new YYARec(309,-174 );yyac++; 
					yya[yyac] = new YYARec(310,-174 );yyac++; 
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
					yya[yyac] = new YYARec(258,206);yyac++; 
					yya[yyac] = new YYARec(268,-198 );yyac++; 
					yya[yyac] = new YYARec(289,-198 );yyac++; 
					yya[yyac] = new YYARec(290,-198 );yyac++; 
					yya[yyac] = new YYARec(291,-198 );yyac++; 
					yya[yyac] = new YYARec(292,-198 );yyac++; 
					yya[yyac] = new YYARec(293,-198 );yyac++; 
					yya[yyac] = new YYARec(297,-198 );yyac++; 
					yya[yyac] = new YYARec(263,58);yyac++; 
					yya[yyac] = new YYARec(282,-97 );yyac++; 
					yya[yyac] = new YYARec(283,-97 );yyac++; 
					yya[yyac] = new YYARec(287,-97 );yyac++; 
					yya[yyac] = new YYARec(298,-97 );yyac++; 
					yya[yyac] = new YYARec(300,-97 );yyac++; 
					yya[yyac] = new YYARec(301,-97 );yyac++; 
					yya[yyac] = new YYARec(302,-97 );yyac++; 
					yya[yyac] = new YYARec(303,-97 );yyac++; 
					yya[yyac] = new YYARec(304,-97 );yyac++; 
					yya[yyac] = new YYARec(305,-97 );yyac++; 
					yya[yyac] = new YYARec(306,-97 );yyac++; 
					yya[yyac] = new YYARec(307,-97 );yyac++; 
					yya[yyac] = new YYARec(309,-97 );yyac++; 
					yya[yyac] = new YYARec(310,-97 );yyac++; 
					yya[yyac] = new YYARec(313,-97 );yyac++; 
					yya[yyac] = new YYARec(314,-97 );yyac++; 
					yya[yyac] = new YYARec(315,-97 );yyac++; 
					yya[yyac] = new YYARec(316,-97 );yyac++; 
					yya[yyac] = new YYARec(317,-97 );yyac++; 
					yya[yyac] = new YYARec(318,-97 );yyac++; 
					yya[yyac] = new YYARec(319,-97 );yyac++; 
					yya[yyac] = new YYARec(320,-97 );yyac++; 
					yya[yyac] = new YYARec(321,-97 );yyac++; 
					yya[yyac] = new YYARec(322,-97 );yyac++; 
					yya[yyac] = new YYARec(323,-97 );yyac++; 
					yya[yyac] = new YYARec(324,-97 );yyac++; 
					yya[yyac] = new YYARec(325,-97 );yyac++; 
					yya[yyac] = new YYARec(258,208);yyac++; 
					yya[yyac] = new YYARec(267,209);yyac++; 
					yya[yyac] = new YYARec(257,150);yyac++; 
					yya[yyac] = new YYARec(259,151);yyac++; 
					yya[yyac] = new YYARec(301,26);yyac++; 
					yya[yyac] = new YYARec(302,27);yyac++; 
					yya[yyac] = new YYARec(306,152);yyac++; 
					yya[yyac] = new YYARec(311,153);yyac++; 
					yya[yyac] = new YYARec(312,154);yyac++; 
					yya[yyac] = new YYARec(315,155);yyac++; 
					yya[yyac] = new YYARec(317,156);yyac++; 
					yya[yyac] = new YYARec(319,157);yyac++; 
					yya[yyac] = new YYARec(320,158);yyac++; 
					yya[yyac] = new YYARec(323,64);yyac++; 
					yya[yyac] = new YYARec(260,-62 );yyac++; 
					yya[yyac] = new YYARec(261,-62 );yyac++; 
					yya[yyac] = new YYARec(267,-62 );yyac++; 
					yya[yyac] = new YYARec(309,29);yyac++; 
					yya[yyac] = new YYARec(317,33);yyac++; 
					yya[yyac] = new YYARec(323,64);yyac++; 
					yya[yyac] = new YYARec(309,29);yyac++; 
					yya[yyac] = new YYARec(317,33);yyac++; 
					yya[yyac] = new YYARec(323,64);yyac++; 
					yya[yyac] = new YYARec(282,82);yyac++; 
					yya[yyac] = new YYARec(283,83);yyac++; 
					yya[yyac] = new YYARec(287,84);yyac++; 
					yya[yyac] = new YYARec(321,85);yyac++; 
					yya[yyac] = new YYARec(258,-52 );yyac++; 
					yya[yyac] = new YYARec(258,216);yyac++; 
					yya[yyac] = new YYARec(301,26);yyac++; 
					yya[yyac] = new YYARec(302,27);yyac++; 
					yya[yyac] = new YYARec(305,219);yyac++; 
					yya[yyac] = new YYARec(306,152);yyac++; 
					yya[yyac] = new YYARec(311,153);yyac++; 
					yya[yyac] = new YYARec(312,154);yyac++; 
					yya[yyac] = new YYARec(313,220);yyac++; 
					yya[yyac] = new YYARec(315,155);yyac++; 
					yya[yyac] = new YYARec(316,221);yyac++; 
					yya[yyac] = new YYARec(317,156);yyac++; 
					yya[yyac] = new YYARec(318,222);yyac++; 
					yya[yyac] = new YYARec(319,157);yyac++; 
					yya[yyac] = new YYARec(320,158);yyac++; 
					yya[yyac] = new YYARec(323,64);yyac++; 
					yya[yyac] = new YYARec(301,26);yyac++; 
					yya[yyac] = new YYARec(302,27);yyac++; 
					yya[yyac] = new YYARec(305,219);yyac++; 
					yya[yyac] = new YYARec(306,152);yyac++; 
					yya[yyac] = new YYARec(311,153);yyac++; 
					yya[yyac] = new YYARec(312,154);yyac++; 
					yya[yyac] = new YYARec(313,220);yyac++; 
					yya[yyac] = new YYARec(315,155);yyac++; 
					yya[yyac] = new YYARec(316,221);yyac++; 
					yya[yyac] = new YYARec(317,156);yyac++; 
					yya[yyac] = new YYARec(318,222);yyac++; 
					yya[yyac] = new YYARec(319,157);yyac++; 
					yya[yyac] = new YYARec(320,158);yyac++; 
					yya[yyac] = new YYARec(323,64);yyac++; 
					yya[yyac] = new YYARec(282,82);yyac++; 
					yya[yyac] = new YYARec(283,83);yyac++; 
					yya[yyac] = new YYARec(287,84);yyac++; 
					yya[yyac] = new YYARec(298,42);yyac++; 
					yya[yyac] = new YYARec(299,24);yyac++; 
					yya[yyac] = new YYARec(300,43);yyac++; 
					yya[yyac] = new YYARec(301,26);yyac++; 
					yya[yyac] = new YYARec(302,27);yyac++; 
					yya[yyac] = new YYARec(304,44);yyac++; 
					yya[yyac] = new YYARec(305,45);yyac++; 
					yya[yyac] = new YYARec(306,46);yyac++; 
					yya[yyac] = new YYARec(307,47);yyac++; 
					yya[yyac] = new YYARec(308,118);yyac++; 
					yya[yyac] = new YYARec(309,29);yyac++; 
					yya[yyac] = new YYARec(310,119);yyac++; 
					yya[yyac] = new YYARec(312,31);yyac++; 
					yya[yyac] = new YYARec(313,32);yyac++; 
					yya[yyac] = new YYARec(314,48);yyac++; 
					yya[yyac] = new YYARec(315,49);yyac++; 
					yya[yyac] = new YYARec(316,120);yyac++; 
					yya[yyac] = new YYARec(317,33);yyac++; 
					yya[yyac] = new YYARec(318,50);yyac++; 
					yya[yyac] = new YYARec(319,121);yyac++; 
					yya[yyac] = new YYARec(320,51);yyac++; 
					yya[yyac] = new YYARec(321,85);yyac++; 
					yya[yyac] = new YYARec(322,86);yyac++; 
					yya[yyac] = new YYARec(323,52);yyac++; 
					yya[yyac] = new YYARec(324,69);yyac++; 
					yya[yyac] = new YYARec(325,87);yyac++; 
					yya[yyac] = new YYARec(257,132);yyac++; 
					yya[yyac] = new YYARec(259,133);yyac++; 
					yya[yyac] = new YYARec(266,134);yyac++; 
					yya[yyac] = new YYARec(288,135);yyac++; 
					yya[yyac] = new YYARec(294,136);yyac++; 
					yya[yyac] = new YYARec(295,137);yyac++; 
					yya[yyac] = new YYARec(296,138);yyac++; 
					yya[yyac] = new YYARec(298,42);yyac++; 
					yya[yyac] = new YYARec(299,24);yyac++; 
					yya[yyac] = new YYARec(300,43);yyac++; 
					yya[yyac] = new YYARec(301,26);yyac++; 
					yya[yyac] = new YYARec(302,27);yyac++; 
					yya[yyac] = new YYARec(304,44);yyac++; 
					yya[yyac] = new YYARec(305,45);yyac++; 
					yya[yyac] = new YYARec(306,46);yyac++; 
					yya[yyac] = new YYARec(307,47);yyac++; 
					yya[yyac] = new YYARec(309,29);yyac++; 
					yya[yyac] = new YYARec(310,139);yyac++; 
					yya[yyac] = new YYARec(312,31);yyac++; 
					yya[yyac] = new YYARec(313,32);yyac++; 
					yya[yyac] = new YYARec(314,140);yyac++; 
					yya[yyac] = new YYARec(315,49);yyac++; 
					yya[yyac] = new YYARec(316,120);yyac++; 
					yya[yyac] = new YYARec(317,33);yyac++; 
					yya[yyac] = new YYARec(318,50);yyac++; 
					yya[yyac] = new YYARec(319,121);yyac++; 
					yya[yyac] = new YYARec(320,51);yyac++; 
					yya[yyac] = new YYARec(323,141);yyac++; 
					yya[yyac] = new YYARec(260,-85 );yyac++; 
					yya[yyac] = new YYARec(261,-85 );yyac++; 
					yya[yyac] = new YYARec(267,-85 );yyac++; 
					yya[yyac] = new YYARec(257,132);yyac++; 
					yya[yyac] = new YYARec(259,133);yyac++; 
					yya[yyac] = new YYARec(266,134);yyac++; 
					yya[yyac] = new YYARec(288,135);yyac++; 
					yya[yyac] = new YYARec(294,136);yyac++; 
					yya[yyac] = new YYARec(295,137);yyac++; 
					yya[yyac] = new YYARec(296,138);yyac++; 
					yya[yyac] = new YYARec(298,42);yyac++; 
					yya[yyac] = new YYARec(299,24);yyac++; 
					yya[yyac] = new YYARec(300,43);yyac++; 
					yya[yyac] = new YYARec(301,26);yyac++; 
					yya[yyac] = new YYARec(302,27);yyac++; 
					yya[yyac] = new YYARec(304,44);yyac++; 
					yya[yyac] = new YYARec(305,45);yyac++; 
					yya[yyac] = new YYARec(306,46);yyac++; 
					yya[yyac] = new YYARec(307,47);yyac++; 
					yya[yyac] = new YYARec(309,29);yyac++; 
					yya[yyac] = new YYARec(310,139);yyac++; 
					yya[yyac] = new YYARec(312,31);yyac++; 
					yya[yyac] = new YYARec(313,32);yyac++; 
					yya[yyac] = new YYARec(314,140);yyac++; 
					yya[yyac] = new YYARec(315,49);yyac++; 
					yya[yyac] = new YYARec(316,120);yyac++; 
					yya[yyac] = new YYARec(317,33);yyac++; 
					yya[yyac] = new YYARec(318,50);yyac++; 
					yya[yyac] = new YYARec(319,121);yyac++; 
					yya[yyac] = new YYARec(320,51);yyac++; 
					yya[yyac] = new YYARec(323,141);yyac++; 
					yya[yyac] = new YYARec(260,-85 );yyac++; 
					yya[yyac] = new YYARec(261,-85 );yyac++; 
					yya[yyac] = new YYARec(267,-85 );yyac++; 
					yya[yyac] = new YYARec(274,199);yyac++; 
					yya[yyac] = new YYARec(282,82);yyac++; 
					yya[yyac] = new YYARec(283,83);yyac++; 
					yya[yyac] = new YYARec(287,84);yyac++; 
					yya[yyac] = new YYARec(298,42);yyac++; 
					yya[yyac] = new YYARec(299,24);yyac++; 
					yya[yyac] = new YYARec(300,43);yyac++; 
					yya[yyac] = new YYARec(301,26);yyac++; 
					yya[yyac] = new YYARec(302,27);yyac++; 
					yya[yyac] = new YYARec(304,44);yyac++; 
					yya[yyac] = new YYARec(305,45);yyac++; 
					yya[yyac] = new YYARec(306,46);yyac++; 
					yya[yyac] = new YYARec(307,47);yyac++; 
					yya[yyac] = new YYARec(309,29);yyac++; 
					yya[yyac] = new YYARec(310,119);yyac++; 
					yya[yyac] = new YYARec(312,31);yyac++; 
					yya[yyac] = new YYARec(313,32);yyac++; 
					yya[yyac] = new YYARec(314,48);yyac++; 
					yya[yyac] = new YYARec(315,49);yyac++; 
					yya[yyac] = new YYARec(316,120);yyac++; 
					yya[yyac] = new YYARec(317,33);yyac++; 
					yya[yyac] = new YYARec(318,50);yyac++; 
					yya[yyac] = new YYARec(319,121);yyac++; 
					yya[yyac] = new YYARec(320,51);yyac++; 
					yya[yyac] = new YYARec(321,200);yyac++; 
					yya[yyac] = new YYARec(322,201);yyac++; 
					yya[yyac] = new YYARec(323,52);yyac++; 
					yya[yyac] = new YYARec(258,235);yyac++; 
					yya[yyac] = new YYARec(258,236);yyac++; 
					yya[yyac] = new YYARec(267,237);yyac++; 
					yya[yyac] = new YYARec(274,238);yyac++; 
					yya[yyac] = new YYARec(258,-202 );yyac++; 
					yya[yyac] = new YYARec(266,-202 );yyac++; 
					yya[yyac] = new YYARec(269,-202 );yyac++; 
					yya[yyac] = new YYARec(270,-202 );yyac++; 
					yya[yyac] = new YYARec(271,-202 );yyac++; 
					yya[yyac] = new YYARec(272,-202 );yyac++; 
					yya[yyac] = new YYARec(273,-202 );yyac++; 
					yya[yyac] = new YYARec(275,-202 );yyac++; 
					yya[yyac] = new YYARec(276,-202 );yyac++; 
					yya[yyac] = new YYARec(277,-202 );yyac++; 
					yya[yyac] = new YYARec(278,-202 );yyac++; 
					yya[yyac] = new YYARec(279,-202 );yyac++; 
					yya[yyac] = new YYARec(280,-202 );yyac++; 
					yya[yyac] = new YYARec(281,-202 );yyac++; 
					yya[yyac] = new YYARec(282,-202 );yyac++; 
					yya[yyac] = new YYARec(283,-202 );yyac++; 
					yya[yyac] = new YYARec(284,-202 );yyac++; 
					yya[yyac] = new YYARec(285,-202 );yyac++; 
					yya[yyac] = new YYARec(286,-202 );yyac++; 
					yya[yyac] = new YYARec(289,-202 );yyac++; 
					yya[yyac] = new YYARec(290,-202 );yyac++; 
					yya[yyac] = new YYARec(291,-202 );yyac++; 
					yya[yyac] = new YYARec(292,-202 );yyac++; 
					yya[yyac] = new YYARec(293,-202 );yyac++; 
					yya[yyac] = new YYARec(297,-202 );yyac++; 
					yya[yyac] = new YYARec(274,199);yyac++; 
					yya[yyac] = new YYARec(282,82);yyac++; 
					yya[yyac] = new YYARec(283,83);yyac++; 
					yya[yyac] = new YYARec(287,84);yyac++; 
					yya[yyac] = new YYARec(298,42);yyac++; 
					yya[yyac] = new YYARec(299,24);yyac++; 
					yya[yyac] = new YYARec(300,43);yyac++; 
					yya[yyac] = new YYARec(301,26);yyac++; 
					yya[yyac] = new YYARec(302,27);yyac++; 
					yya[yyac] = new YYARec(304,44);yyac++; 
					yya[yyac] = new YYARec(305,45);yyac++; 
					yya[yyac] = new YYARec(306,46);yyac++; 
					yya[yyac] = new YYARec(307,47);yyac++; 
					yya[yyac] = new YYARec(309,29);yyac++; 
					yya[yyac] = new YYARec(310,119);yyac++; 
					yya[yyac] = new YYARec(312,31);yyac++; 
					yya[yyac] = new YYARec(313,32);yyac++; 
					yya[yyac] = new YYARec(314,48);yyac++; 
					yya[yyac] = new YYARec(315,49);yyac++; 
					yya[yyac] = new YYARec(316,120);yyac++; 
					yya[yyac] = new YYARec(317,33);yyac++; 
					yya[yyac] = new YYARec(318,50);yyac++; 
					yya[yyac] = new YYARec(319,121);yyac++; 
					yya[yyac] = new YYARec(320,51);yyac++; 
					yya[yyac] = new YYARec(321,200);yyac++; 
					yya[yyac] = new YYARec(322,201);yyac++; 
					yya[yyac] = new YYARec(323,52);yyac++; 
					yya[yyac] = new YYARec(284,241);yyac++; 
					yya[yyac] = new YYARec(285,242);yyac++; 
					yya[yyac] = new YYARec(286,243);yyac++; 
					yya[yyac] = new YYARec(258,-113 );yyac++; 
					yya[yyac] = new YYARec(266,-113 );yyac++; 
					yya[yyac] = new YYARec(269,-113 );yyac++; 
					yya[yyac] = new YYARec(270,-113 );yyac++; 
					yya[yyac] = new YYARec(271,-113 );yyac++; 
					yya[yyac] = new YYARec(272,-113 );yyac++; 
					yya[yyac] = new YYARec(273,-113 );yyac++; 
					yya[yyac] = new YYARec(275,-113 );yyac++; 
					yya[yyac] = new YYARec(276,-113 );yyac++; 
					yya[yyac] = new YYARec(277,-113 );yyac++; 
					yya[yyac] = new YYARec(278,-113 );yyac++; 
					yya[yyac] = new YYARec(279,-113 );yyac++; 
					yya[yyac] = new YYARec(280,-113 );yyac++; 
					yya[yyac] = new YYARec(281,-113 );yyac++; 
					yya[yyac] = new YYARec(282,-113 );yyac++; 
					yya[yyac] = new YYARec(283,-113 );yyac++; 
					yya[yyac] = new YYARec(282,245);yyac++; 
					yya[yyac] = new YYARec(283,246);yyac++; 
					yya[yyac] = new YYARec(258,-111 );yyac++; 
					yya[yyac] = new YYARec(266,-111 );yyac++; 
					yya[yyac] = new YYARec(269,-111 );yyac++; 
					yya[yyac] = new YYARec(270,-111 );yyac++; 
					yya[yyac] = new YYARec(271,-111 );yyac++; 
					yya[yyac] = new YYARec(272,-111 );yyac++; 
					yya[yyac] = new YYARec(273,-111 );yyac++; 
					yya[yyac] = new YYARec(275,-111 );yyac++; 
					yya[yyac] = new YYARec(276,-111 );yyac++; 
					yya[yyac] = new YYARec(277,-111 );yyac++; 
					yya[yyac] = new YYARec(278,-111 );yyac++; 
					yya[yyac] = new YYARec(279,-111 );yyac++; 
					yya[yyac] = new YYARec(280,-111 );yyac++; 
					yya[yyac] = new YYARec(281,-111 );yyac++; 
					yya[yyac] = new YYARec(278,248);yyac++; 
					yya[yyac] = new YYARec(279,249);yyac++; 
					yya[yyac] = new YYARec(280,250);yyac++; 
					yya[yyac] = new YYARec(281,251);yyac++; 
					yya[yyac] = new YYARec(258,-109 );yyac++; 
					yya[yyac] = new YYARec(266,-109 );yyac++; 
					yya[yyac] = new YYARec(269,-109 );yyac++; 
					yya[yyac] = new YYARec(270,-109 );yyac++; 
					yya[yyac] = new YYARec(271,-109 );yyac++; 
					yya[yyac] = new YYARec(272,-109 );yyac++; 
					yya[yyac] = new YYARec(273,-109 );yyac++; 
					yya[yyac] = new YYARec(275,-109 );yyac++; 
					yya[yyac] = new YYARec(276,-109 );yyac++; 
					yya[yyac] = new YYARec(277,-109 );yyac++; 
					yya[yyac] = new YYARec(276,253);yyac++; 
					yya[yyac] = new YYARec(277,254);yyac++; 
					yya[yyac] = new YYARec(258,-108 );yyac++; 
					yya[yyac] = new YYARec(266,-108 );yyac++; 
					yya[yyac] = new YYARec(269,-108 );yyac++; 
					yya[yyac] = new YYARec(270,-108 );yyac++; 
					yya[yyac] = new YYARec(271,-108 );yyac++; 
					yya[yyac] = new YYARec(272,-108 );yyac++; 
					yya[yyac] = new YYARec(273,-108 );yyac++; 
					yya[yyac] = new YYARec(275,-108 );yyac++; 
					yya[yyac] = new YYARec(273,255);yyac++; 
					yya[yyac] = new YYARec(258,-106 );yyac++; 
					yya[yyac] = new YYARec(266,-106 );yyac++; 
					yya[yyac] = new YYARec(269,-106 );yyac++; 
					yya[yyac] = new YYARec(270,-106 );yyac++; 
					yya[yyac] = new YYARec(271,-106 );yyac++; 
					yya[yyac] = new YYARec(272,-106 );yyac++; 
					yya[yyac] = new YYARec(275,-106 );yyac++; 
					yya[yyac] = new YYARec(272,256);yyac++; 
					yya[yyac] = new YYARec(258,-104 );yyac++; 
					yya[yyac] = new YYARec(266,-104 );yyac++; 
					yya[yyac] = new YYARec(269,-104 );yyac++; 
					yya[yyac] = new YYARec(270,-104 );yyac++; 
					yya[yyac] = new YYARec(271,-104 );yyac++; 
					yya[yyac] = new YYARec(275,-104 );yyac++; 
					yya[yyac] = new YYARec(271,257);yyac++; 
					yya[yyac] = new YYARec(258,-102 );yyac++; 
					yya[yyac] = new YYARec(266,-102 );yyac++; 
					yya[yyac] = new YYARec(269,-102 );yyac++; 
					yya[yyac] = new YYARec(270,-102 );yyac++; 
					yya[yyac] = new YYARec(275,-102 );yyac++; 
					yya[yyac] = new YYARec(270,258);yyac++; 
					yya[yyac] = new YYARec(258,-100 );yyac++; 
					yya[yyac] = new YYARec(266,-100 );yyac++; 
					yya[yyac] = new YYARec(269,-100 );yyac++; 
					yya[yyac] = new YYARec(275,-100 );yyac++; 
					yya[yyac] = new YYARec(269,259);yyac++; 
					yya[yyac] = new YYARec(258,-98 );yyac++; 
					yya[yyac] = new YYARec(266,-98 );yyac++; 
					yya[yyac] = new YYARec(275,-98 );yyac++; 
					yya[yyac] = new YYARec(289,173);yyac++; 
					yya[yyac] = new YYARec(290,174);yyac++; 
					yya[yyac] = new YYARec(291,175);yyac++; 
					yya[yyac] = new YYARec(292,176);yyac++; 
					yya[yyac] = new YYARec(293,177);yyac++; 
					yya[yyac] = new YYARec(258,-122 );yyac++; 
					yya[yyac] = new YYARec(269,-122 );yyac++; 
					yya[yyac] = new YYARec(270,-122 );yyac++; 
					yya[yyac] = new YYARec(271,-122 );yyac++; 
					yya[yyac] = new YYARec(272,-122 );yyac++; 
					yya[yyac] = new YYARec(273,-122 );yyac++; 
					yya[yyac] = new YYARec(276,-122 );yyac++; 
					yya[yyac] = new YYARec(277,-122 );yyac++; 
					yya[yyac] = new YYARec(278,-122 );yyac++; 
					yya[yyac] = new YYARec(279,-122 );yyac++; 
					yya[yyac] = new YYARec(280,-122 );yyac++; 
					yya[yyac] = new YYARec(281,-122 );yyac++; 
					yya[yyac] = new YYARec(282,-122 );yyac++; 
					yya[yyac] = new YYARec(283,-122 );yyac++; 
					yya[yyac] = new YYARec(284,-122 );yyac++; 
					yya[yyac] = new YYARec(285,-122 );yyac++; 
					yya[yyac] = new YYARec(286,-122 );yyac++; 
					yya[yyac] = new YYARec(274,199);yyac++; 
					yya[yyac] = new YYARec(282,82);yyac++; 
					yya[yyac] = new YYARec(283,83);yyac++; 
					yya[yyac] = new YYARec(287,84);yyac++; 
					yya[yyac] = new YYARec(298,42);yyac++; 
					yya[yyac] = new YYARec(299,24);yyac++; 
					yya[yyac] = new YYARec(300,43);yyac++; 
					yya[yyac] = new YYARec(301,26);yyac++; 
					yya[yyac] = new YYARec(302,27);yyac++; 
					yya[yyac] = new YYARec(304,44);yyac++; 
					yya[yyac] = new YYARec(305,45);yyac++; 
					yya[yyac] = new YYARec(306,46);yyac++; 
					yya[yyac] = new YYARec(307,47);yyac++; 
					yya[yyac] = new YYARec(309,29);yyac++; 
					yya[yyac] = new YYARec(310,119);yyac++; 
					yya[yyac] = new YYARec(312,31);yyac++; 
					yya[yyac] = new YYARec(313,32);yyac++; 
					yya[yyac] = new YYARec(314,48);yyac++; 
					yya[yyac] = new YYARec(315,49);yyac++; 
					yya[yyac] = new YYARec(316,120);yyac++; 
					yya[yyac] = new YYARec(317,33);yyac++; 
					yya[yyac] = new YYARec(318,50);yyac++; 
					yya[yyac] = new YYARec(319,121);yyac++; 
					yya[yyac] = new YYARec(320,51);yyac++; 
					yya[yyac] = new YYARec(321,200);yyac++; 
					yya[yyac] = new YYARec(322,201);yyac++; 
					yya[yyac] = new YYARec(323,52);yyac++; 
					yya[yyac] = new YYARec(257,132);yyac++; 
					yya[yyac] = new YYARec(259,133);yyac++; 
					yya[yyac] = new YYARec(266,134);yyac++; 
					yya[yyac] = new YYARec(288,135);yyac++; 
					yya[yyac] = new YYARec(294,136);yyac++; 
					yya[yyac] = new YYARec(295,137);yyac++; 
					yya[yyac] = new YYARec(296,138);yyac++; 
					yya[yyac] = new YYARec(298,42);yyac++; 
					yya[yyac] = new YYARec(299,24);yyac++; 
					yya[yyac] = new YYARec(300,43);yyac++; 
					yya[yyac] = new YYARec(301,26);yyac++; 
					yya[yyac] = new YYARec(302,27);yyac++; 
					yya[yyac] = new YYARec(304,44);yyac++; 
					yya[yyac] = new YYARec(305,45);yyac++; 
					yya[yyac] = new YYARec(306,46);yyac++; 
					yya[yyac] = new YYARec(307,47);yyac++; 
					yya[yyac] = new YYARec(309,29);yyac++; 
					yya[yyac] = new YYARec(310,139);yyac++; 
					yya[yyac] = new YYARec(312,31);yyac++; 
					yya[yyac] = new YYARec(313,32);yyac++; 
					yya[yyac] = new YYARec(314,140);yyac++; 
					yya[yyac] = new YYARec(315,49);yyac++; 
					yya[yyac] = new YYARec(316,120);yyac++; 
					yya[yyac] = new YYARec(317,33);yyac++; 
					yya[yyac] = new YYARec(318,50);yyac++; 
					yya[yyac] = new YYARec(319,121);yyac++; 
					yya[yyac] = new YYARec(320,51);yyac++; 
					yya[yyac] = new YYARec(323,141);yyac++; 
					yya[yyac] = new YYARec(267,-85 );yyac++; 
					yya[yyac] = new YYARec(266,263);yyac++; 
					yya[yyac] = new YYARec(266,264);yyac++; 
					yya[yyac] = new YYARec(257,132);yyac++; 
					yya[yyac] = new YYARec(259,133);yyac++; 
					yya[yyac] = new YYARec(266,134);yyac++; 
					yya[yyac] = new YYARec(288,135);yyac++; 
					yya[yyac] = new YYARec(294,136);yyac++; 
					yya[yyac] = new YYARec(295,137);yyac++; 
					yya[yyac] = new YYARec(296,138);yyac++; 
					yya[yyac] = new YYARec(298,42);yyac++; 
					yya[yyac] = new YYARec(299,24);yyac++; 
					yya[yyac] = new YYARec(300,43);yyac++; 
					yya[yyac] = new YYARec(301,26);yyac++; 
					yya[yyac] = new YYARec(302,27);yyac++; 
					yya[yyac] = new YYARec(304,44);yyac++; 
					yya[yyac] = new YYARec(305,45);yyac++; 
					yya[yyac] = new YYARec(306,46);yyac++; 
					yya[yyac] = new YYARec(307,47);yyac++; 
					yya[yyac] = new YYARec(309,29);yyac++; 
					yya[yyac] = new YYARec(310,139);yyac++; 
					yya[yyac] = new YYARec(312,31);yyac++; 
					yya[yyac] = new YYARec(313,32);yyac++; 
					yya[yyac] = new YYARec(314,140);yyac++; 
					yya[yyac] = new YYARec(315,49);yyac++; 
					yya[yyac] = new YYARec(316,120);yyac++; 
					yya[yyac] = new YYARec(317,33);yyac++; 
					yya[yyac] = new YYARec(318,50);yyac++; 
					yya[yyac] = new YYARec(319,121);yyac++; 
					yya[yyac] = new YYARec(320,51);yyac++; 
					yya[yyac] = new YYARec(323,141);yyac++; 
					yya[yyac] = new YYARec(260,-85 );yyac++; 
					yya[yyac] = new YYARec(261,-85 );yyac++; 
					yya[yyac] = new YYARec(267,-85 );yyac++; 
					yya[yyac] = new YYARec(282,82);yyac++; 
					yya[yyac] = new YYARec(283,83);yyac++; 
					yya[yyac] = new YYARec(287,84);yyac++; 
					yya[yyac] = new YYARec(298,42);yyac++; 
					yya[yyac] = new YYARec(300,43);yyac++; 
					yya[yyac] = new YYARec(301,26);yyac++; 
					yya[yyac] = new YYARec(302,27);yyac++; 
					yya[yyac] = new YYARec(303,28);yyac++; 
					yya[yyac] = new YYARec(304,44);yyac++; 
					yya[yyac] = new YYARec(305,45);yyac++; 
					yya[yyac] = new YYARec(306,46);yyac++; 
					yya[yyac] = new YYARec(307,47);yyac++; 
					yya[yyac] = new YYARec(309,29);yyac++; 
					yya[yyac] = new YYARec(310,119);yyac++; 
					yya[yyac] = new YYARec(313,32);yyac++; 
					yya[yyac] = new YYARec(314,48);yyac++; 
					yya[yyac] = new YYARec(315,49);yyac++; 
					yya[yyac] = new YYARec(316,120);yyac++; 
					yya[yyac] = new YYARec(317,33);yyac++; 
					yya[yyac] = new YYARec(318,50);yyac++; 
					yya[yyac] = new YYARec(319,121);yyac++; 
					yya[yyac] = new YYARec(320,51);yyac++; 
					yya[yyac] = new YYARec(321,85);yyac++; 
					yya[yyac] = new YYARec(322,86);yyac++; 
					yya[yyac] = new YYARec(323,52);yyac++; 
					yya[yyac] = new YYARec(324,69);yyac++; 
					yya[yyac] = new YYARec(325,87);yyac++; 
					yya[yyac] = new YYARec(257,150);yyac++; 
					yya[yyac] = new YYARec(259,151);yyac++; 
					yya[yyac] = new YYARec(301,26);yyac++; 
					yya[yyac] = new YYARec(302,27);yyac++; 
					yya[yyac] = new YYARec(306,152);yyac++; 
					yya[yyac] = new YYARec(311,153);yyac++; 
					yya[yyac] = new YYARec(312,154);yyac++; 
					yya[yyac] = new YYARec(315,155);yyac++; 
					yya[yyac] = new YYARec(317,156);yyac++; 
					yya[yyac] = new YYARec(319,157);yyac++; 
					yya[yyac] = new YYARec(320,158);yyac++; 
					yya[yyac] = new YYARec(323,64);yyac++; 
					yya[yyac] = new YYARec(260,-62 );yyac++; 
					yya[yyac] = new YYARec(261,-62 );yyac++; 
					yya[yyac] = new YYARec(267,-62 );yyac++; 
					yya[yyac] = new YYARec(258,276);yyac++; 
					yya[yyac] = new YYARec(258,277);yyac++; 
					yya[yyac] = new YYARec(321,100);yyac++; 
					yya[yyac] = new YYARec(263,278);yyac++; 
					yya[yyac] = new YYARec(258,-51 );yyac++; 
					yya[yyac] = new YYARec(258,279);yyac++; 
					yya[yyac] = new YYARec(257,18);yyac++; 
					yya[yyac] = new YYARec(259,19);yyac++; 
					yya[yyac] = new YYARec(262,20);yyac++; 
					yya[yyac] = new YYARec(264,21);yyac++; 
					yya[yyac] = new YYARec(265,22);yyac++; 
					yya[yyac] = new YYARec(267,23);yyac++; 
					yya[yyac] = new YYARec(299,24);yyac++; 
					yya[yyac] = new YYARec(300,25);yyac++; 
					yya[yyac] = new YYARec(301,26);yyac++; 
					yya[yyac] = new YYARec(302,27);yyac++; 
					yya[yyac] = new YYARec(303,28);yyac++; 
					yya[yyac] = new YYARec(309,29);yyac++; 
					yya[yyac] = new YYARec(311,30);yyac++; 
					yya[yyac] = new YYARec(312,31);yyac++; 
					yya[yyac] = new YYARec(313,32);yyac++; 
					yya[yyac] = new YYARec(317,33);yyac++; 
					yya[yyac] = new YYARec(319,34);yyac++; 
					yya[yyac] = new YYARec(261,-3 );yyac++; 
					yya[yyac] = new YYARec(263,58);yyac++; 
					yya[yyac] = new YYARec(258,-97 );yyac++; 
					yya[yyac] = new YYARec(282,-97 );yyac++; 
					yya[yyac] = new YYARec(283,-97 );yyac++; 
					yya[yyac] = new YYARec(287,-97 );yyac++; 
					yya[yyac] = new YYARec(298,-97 );yyac++; 
					yya[yyac] = new YYARec(299,-97 );yyac++; 
					yya[yyac] = new YYARec(300,-97 );yyac++; 
					yya[yyac] = new YYARec(301,-97 );yyac++; 
					yya[yyac] = new YYARec(302,-97 );yyac++; 
					yya[yyac] = new YYARec(304,-97 );yyac++; 
					yya[yyac] = new YYARec(305,-97 );yyac++; 
					yya[yyac] = new YYARec(306,-97 );yyac++; 
					yya[yyac] = new YYARec(307,-97 );yyac++; 
					yya[yyac] = new YYARec(308,-97 );yyac++; 
					yya[yyac] = new YYARec(309,-97 );yyac++; 
					yya[yyac] = new YYARec(310,-97 );yyac++; 
					yya[yyac] = new YYARec(312,-97 );yyac++; 
					yya[yyac] = new YYARec(313,-97 );yyac++; 
					yya[yyac] = new YYARec(314,-97 );yyac++; 
					yya[yyac] = new YYARec(315,-97 );yyac++; 
					yya[yyac] = new YYARec(316,-97 );yyac++; 
					yya[yyac] = new YYARec(317,-97 );yyac++; 
					yya[yyac] = new YYARec(318,-97 );yyac++; 
					yya[yyac] = new YYARec(319,-97 );yyac++; 
					yya[yyac] = new YYARec(320,-97 );yyac++; 
					yya[yyac] = new YYARec(321,-97 );yyac++; 
					yya[yyac] = new YYARec(322,-97 );yyac++; 
					yya[yyac] = new YYARec(323,-97 );yyac++; 
					yya[yyac] = new YYARec(324,-97 );yyac++; 
					yya[yyac] = new YYARec(325,-97 );yyac++; 
					yya[yyac] = new YYARec(257,132);yyac++; 
					yya[yyac] = new YYARec(259,133);yyac++; 
					yya[yyac] = new YYARec(266,134);yyac++; 
					yya[yyac] = new YYARec(288,135);yyac++; 
					yya[yyac] = new YYARec(294,136);yyac++; 
					yya[yyac] = new YYARec(295,137);yyac++; 
					yya[yyac] = new YYARec(296,138);yyac++; 
					yya[yyac] = new YYARec(298,42);yyac++; 
					yya[yyac] = new YYARec(299,24);yyac++; 
					yya[yyac] = new YYARec(300,43);yyac++; 
					yya[yyac] = new YYARec(301,26);yyac++; 
					yya[yyac] = new YYARec(302,27);yyac++; 
					yya[yyac] = new YYARec(304,44);yyac++; 
					yya[yyac] = new YYARec(305,45);yyac++; 
					yya[yyac] = new YYARec(306,46);yyac++; 
					yya[yyac] = new YYARec(307,47);yyac++; 
					yya[yyac] = new YYARec(309,29);yyac++; 
					yya[yyac] = new YYARec(310,139);yyac++; 
					yya[yyac] = new YYARec(312,31);yyac++; 
					yya[yyac] = new YYARec(313,32);yyac++; 
					yya[yyac] = new YYARec(314,140);yyac++; 
					yya[yyac] = new YYARec(315,49);yyac++; 
					yya[yyac] = new YYARec(316,120);yyac++; 
					yya[yyac] = new YYARec(317,33);yyac++; 
					yya[yyac] = new YYARec(318,50);yyac++; 
					yya[yyac] = new YYARec(319,121);yyac++; 
					yya[yyac] = new YYARec(320,51);yyac++; 
					yya[yyac] = new YYARec(323,141);yyac++; 
					yya[yyac] = new YYARec(260,-85 );yyac++; 
					yya[yyac] = new YYARec(261,-85 );yyac++; 
					yya[yyac] = new YYARec(257,132);yyac++; 
					yya[yyac] = new YYARec(259,133);yyac++; 
					yya[yyac] = new YYARec(266,134);yyac++; 
					yya[yyac] = new YYARec(288,135);yyac++; 
					yya[yyac] = new YYARec(294,136);yyac++; 
					yya[yyac] = new YYARec(295,137);yyac++; 
					yya[yyac] = new YYARec(296,138);yyac++; 
					yya[yyac] = new YYARec(298,42);yyac++; 
					yya[yyac] = new YYARec(299,24);yyac++; 
					yya[yyac] = new YYARec(300,43);yyac++; 
					yya[yyac] = new YYARec(301,26);yyac++; 
					yya[yyac] = new YYARec(302,27);yyac++; 
					yya[yyac] = new YYARec(304,44);yyac++; 
					yya[yyac] = new YYARec(305,45);yyac++; 
					yya[yyac] = new YYARec(306,46);yyac++; 
					yya[yyac] = new YYARec(307,47);yyac++; 
					yya[yyac] = new YYARec(309,29);yyac++; 
					yya[yyac] = new YYARec(310,139);yyac++; 
					yya[yyac] = new YYARec(312,31);yyac++; 
					yya[yyac] = new YYARec(313,32);yyac++; 
					yya[yyac] = new YYARec(314,140);yyac++; 
					yya[yyac] = new YYARec(315,49);yyac++; 
					yya[yyac] = new YYARec(316,120);yyac++; 
					yya[yyac] = new YYARec(317,33);yyac++; 
					yya[yyac] = new YYARec(318,50);yyac++; 
					yya[yyac] = new YYARec(319,121);yyac++; 
					yya[yyac] = new YYARec(320,51);yyac++; 
					yya[yyac] = new YYARec(323,141);yyac++; 
					yya[yyac] = new YYARec(260,-85 );yyac++; 
					yya[yyac] = new YYARec(261,-85 );yyac++; 
					yya[yyac] = new YYARec(257,132);yyac++; 
					yya[yyac] = new YYARec(259,133);yyac++; 
					yya[yyac] = new YYARec(266,134);yyac++; 
					yya[yyac] = new YYARec(288,135);yyac++; 
					yya[yyac] = new YYARec(294,136);yyac++; 
					yya[yyac] = new YYARec(295,137);yyac++; 
					yya[yyac] = new YYARec(296,138);yyac++; 
					yya[yyac] = new YYARec(298,42);yyac++; 
					yya[yyac] = new YYARec(299,24);yyac++; 
					yya[yyac] = new YYARec(300,43);yyac++; 
					yya[yyac] = new YYARec(301,26);yyac++; 
					yya[yyac] = new YYARec(302,27);yyac++; 
					yya[yyac] = new YYARec(304,44);yyac++; 
					yya[yyac] = new YYARec(305,45);yyac++; 
					yya[yyac] = new YYARec(306,46);yyac++; 
					yya[yyac] = new YYARec(307,47);yyac++; 
					yya[yyac] = new YYARec(309,29);yyac++; 
					yya[yyac] = new YYARec(310,139);yyac++; 
					yya[yyac] = new YYARec(312,31);yyac++; 
					yya[yyac] = new YYARec(313,32);yyac++; 
					yya[yyac] = new YYARec(314,140);yyac++; 
					yya[yyac] = new YYARec(315,49);yyac++; 
					yya[yyac] = new YYARec(316,120);yyac++; 
					yya[yyac] = new YYARec(317,33);yyac++; 
					yya[yyac] = new YYARec(318,50);yyac++; 
					yya[yyac] = new YYARec(319,121);yyac++; 
					yya[yyac] = new YYARec(320,51);yyac++; 
					yya[yyac] = new YYARec(323,141);yyac++; 
					yya[yyac] = new YYARec(260,-85 );yyac++; 
					yya[yyac] = new YYARec(261,-85 );yyac++; 
					yya[yyac] = new YYARec(267,-85 );yyac++; 
					yya[yyac] = new YYARec(274,199);yyac++; 
					yya[yyac] = new YYARec(282,82);yyac++; 
					yya[yyac] = new YYARec(283,83);yyac++; 
					yya[yyac] = new YYARec(287,84);yyac++; 
					yya[yyac] = new YYARec(298,42);yyac++; 
					yya[yyac] = new YYARec(299,24);yyac++; 
					yya[yyac] = new YYARec(300,43);yyac++; 
					yya[yyac] = new YYARec(301,26);yyac++; 
					yya[yyac] = new YYARec(302,27);yyac++; 
					yya[yyac] = new YYARec(304,44);yyac++; 
					yya[yyac] = new YYARec(305,45);yyac++; 
					yya[yyac] = new YYARec(306,46);yyac++; 
					yya[yyac] = new YYARec(307,47);yyac++; 
					yya[yyac] = new YYARec(309,29);yyac++; 
					yya[yyac] = new YYARec(310,119);yyac++; 
					yya[yyac] = new YYARec(312,31);yyac++; 
					yya[yyac] = new YYARec(313,32);yyac++; 
					yya[yyac] = new YYARec(314,48);yyac++; 
					yya[yyac] = new YYARec(315,49);yyac++; 
					yya[yyac] = new YYARec(316,120);yyac++; 
					yya[yyac] = new YYARec(317,33);yyac++; 
					yya[yyac] = new YYARec(318,50);yyac++; 
					yya[yyac] = new YYARec(319,121);yyac++; 
					yya[yyac] = new YYARec(320,51);yyac++; 
					yya[yyac] = new YYARec(321,200);yyac++; 
					yya[yyac] = new YYARec(322,201);yyac++; 
					yya[yyac] = new YYARec(323,52);yyac++; 
					yya[yyac] = new YYARec(274,199);yyac++; 
					yya[yyac] = new YYARec(282,82);yyac++; 
					yya[yyac] = new YYARec(283,83);yyac++; 
					yya[yyac] = new YYARec(287,84);yyac++; 
					yya[yyac] = new YYARec(298,42);yyac++; 
					yya[yyac] = new YYARec(299,24);yyac++; 
					yya[yyac] = new YYARec(300,43);yyac++; 
					yya[yyac] = new YYARec(301,26);yyac++; 
					yya[yyac] = new YYARec(302,27);yyac++; 
					yya[yyac] = new YYARec(304,44);yyac++; 
					yya[yyac] = new YYARec(305,45);yyac++; 
					yya[yyac] = new YYARec(306,46);yyac++; 
					yya[yyac] = new YYARec(307,47);yyac++; 
					yya[yyac] = new YYARec(309,29);yyac++; 
					yya[yyac] = new YYARec(310,119);yyac++; 
					yya[yyac] = new YYARec(312,31);yyac++; 
					yya[yyac] = new YYARec(313,32);yyac++; 
					yya[yyac] = new YYARec(314,48);yyac++; 
					yya[yyac] = new YYARec(315,49);yyac++; 
					yya[yyac] = new YYARec(316,120);yyac++; 
					yya[yyac] = new YYARec(317,33);yyac++; 
					yya[yyac] = new YYARec(318,50);yyac++; 
					yya[yyac] = new YYARec(319,121);yyac++; 
					yya[yyac] = new YYARec(320,51);yyac++; 
					yya[yyac] = new YYARec(321,200);yyac++; 
					yya[yyac] = new YYARec(322,201);yyac++; 
					yya[yyac] = new YYARec(323,52);yyac++; 
					yya[yyac] = new YYARec(274,199);yyac++; 
					yya[yyac] = new YYARec(282,82);yyac++; 
					yya[yyac] = new YYARec(283,83);yyac++; 
					yya[yyac] = new YYARec(287,84);yyac++; 
					yya[yyac] = new YYARec(298,42);yyac++; 
					yya[yyac] = new YYARec(299,24);yyac++; 
					yya[yyac] = new YYARec(300,43);yyac++; 
					yya[yyac] = new YYARec(301,26);yyac++; 
					yya[yyac] = new YYARec(302,27);yyac++; 
					yya[yyac] = new YYARec(304,44);yyac++; 
					yya[yyac] = new YYARec(305,45);yyac++; 
					yya[yyac] = new YYARec(306,46);yyac++; 
					yya[yyac] = new YYARec(307,47);yyac++; 
					yya[yyac] = new YYARec(309,29);yyac++; 
					yya[yyac] = new YYARec(310,119);yyac++; 
					yya[yyac] = new YYARec(312,31);yyac++; 
					yya[yyac] = new YYARec(313,32);yyac++; 
					yya[yyac] = new YYARec(314,48);yyac++; 
					yya[yyac] = new YYARec(315,49);yyac++; 
					yya[yyac] = new YYARec(316,120);yyac++; 
					yya[yyac] = new YYARec(317,33);yyac++; 
					yya[yyac] = new YYARec(318,50);yyac++; 
					yya[yyac] = new YYARec(319,121);yyac++; 
					yya[yyac] = new YYARec(320,51);yyac++; 
					yya[yyac] = new YYARec(321,200);yyac++; 
					yya[yyac] = new YYARec(322,201);yyac++; 
					yya[yyac] = new YYARec(323,52);yyac++; 
					yya[yyac] = new YYARec(274,199);yyac++; 
					yya[yyac] = new YYARec(282,82);yyac++; 
					yya[yyac] = new YYARec(283,83);yyac++; 
					yya[yyac] = new YYARec(287,84);yyac++; 
					yya[yyac] = new YYARec(298,42);yyac++; 
					yya[yyac] = new YYARec(299,24);yyac++; 
					yya[yyac] = new YYARec(300,43);yyac++; 
					yya[yyac] = new YYARec(301,26);yyac++; 
					yya[yyac] = new YYARec(302,27);yyac++; 
					yya[yyac] = new YYARec(304,44);yyac++; 
					yya[yyac] = new YYARec(305,45);yyac++; 
					yya[yyac] = new YYARec(306,46);yyac++; 
					yya[yyac] = new YYARec(307,47);yyac++; 
					yya[yyac] = new YYARec(309,29);yyac++; 
					yya[yyac] = new YYARec(310,119);yyac++; 
					yya[yyac] = new YYARec(312,31);yyac++; 
					yya[yyac] = new YYARec(313,32);yyac++; 
					yya[yyac] = new YYARec(314,48);yyac++; 
					yya[yyac] = new YYARec(315,49);yyac++; 
					yya[yyac] = new YYARec(316,120);yyac++; 
					yya[yyac] = new YYARec(317,33);yyac++; 
					yya[yyac] = new YYARec(318,50);yyac++; 
					yya[yyac] = new YYARec(319,121);yyac++; 
					yya[yyac] = new YYARec(320,51);yyac++; 
					yya[yyac] = new YYARec(321,200);yyac++; 
					yya[yyac] = new YYARec(322,201);yyac++; 
					yya[yyac] = new YYARec(323,52);yyac++; 
					yya[yyac] = new YYARec(274,199);yyac++; 
					yya[yyac] = new YYARec(282,82);yyac++; 
					yya[yyac] = new YYARec(283,83);yyac++; 
					yya[yyac] = new YYARec(287,84);yyac++; 
					yya[yyac] = new YYARec(298,42);yyac++; 
					yya[yyac] = new YYARec(299,24);yyac++; 
					yya[yyac] = new YYARec(300,43);yyac++; 
					yya[yyac] = new YYARec(301,26);yyac++; 
					yya[yyac] = new YYARec(302,27);yyac++; 
					yya[yyac] = new YYARec(304,44);yyac++; 
					yya[yyac] = new YYARec(305,45);yyac++; 
					yya[yyac] = new YYARec(306,46);yyac++; 
					yya[yyac] = new YYARec(307,47);yyac++; 
					yya[yyac] = new YYARec(309,29);yyac++; 
					yya[yyac] = new YYARec(310,119);yyac++; 
					yya[yyac] = new YYARec(312,31);yyac++; 
					yya[yyac] = new YYARec(313,32);yyac++; 
					yya[yyac] = new YYARec(314,48);yyac++; 
					yya[yyac] = new YYARec(315,49);yyac++; 
					yya[yyac] = new YYARec(316,120);yyac++; 
					yya[yyac] = new YYARec(317,33);yyac++; 
					yya[yyac] = new YYARec(318,50);yyac++; 
					yya[yyac] = new YYARec(319,121);yyac++; 
					yya[yyac] = new YYARec(320,51);yyac++; 
					yya[yyac] = new YYARec(321,200);yyac++; 
					yya[yyac] = new YYARec(322,201);yyac++; 
					yya[yyac] = new YYARec(323,52);yyac++; 
					yya[yyac] = new YYARec(274,199);yyac++; 
					yya[yyac] = new YYARec(282,82);yyac++; 
					yya[yyac] = new YYARec(283,83);yyac++; 
					yya[yyac] = new YYARec(287,84);yyac++; 
					yya[yyac] = new YYARec(298,42);yyac++; 
					yya[yyac] = new YYARec(299,24);yyac++; 
					yya[yyac] = new YYARec(300,43);yyac++; 
					yya[yyac] = new YYARec(301,26);yyac++; 
					yya[yyac] = new YYARec(302,27);yyac++; 
					yya[yyac] = new YYARec(304,44);yyac++; 
					yya[yyac] = new YYARec(305,45);yyac++; 
					yya[yyac] = new YYARec(306,46);yyac++; 
					yya[yyac] = new YYARec(307,47);yyac++; 
					yya[yyac] = new YYARec(309,29);yyac++; 
					yya[yyac] = new YYARec(310,119);yyac++; 
					yya[yyac] = new YYARec(312,31);yyac++; 
					yya[yyac] = new YYARec(313,32);yyac++; 
					yya[yyac] = new YYARec(314,48);yyac++; 
					yya[yyac] = new YYARec(315,49);yyac++; 
					yya[yyac] = new YYARec(316,120);yyac++; 
					yya[yyac] = new YYARec(317,33);yyac++; 
					yya[yyac] = new YYARec(318,50);yyac++; 
					yya[yyac] = new YYARec(319,121);yyac++; 
					yya[yyac] = new YYARec(320,51);yyac++; 
					yya[yyac] = new YYARec(321,200);yyac++; 
					yya[yyac] = new YYARec(322,201);yyac++; 
					yya[yyac] = new YYARec(323,52);yyac++; 
					yya[yyac] = new YYARec(274,199);yyac++; 
					yya[yyac] = new YYARec(282,82);yyac++; 
					yya[yyac] = new YYARec(283,83);yyac++; 
					yya[yyac] = new YYARec(287,84);yyac++; 
					yya[yyac] = new YYARec(298,42);yyac++; 
					yya[yyac] = new YYARec(299,24);yyac++; 
					yya[yyac] = new YYARec(300,43);yyac++; 
					yya[yyac] = new YYARec(301,26);yyac++; 
					yya[yyac] = new YYARec(302,27);yyac++; 
					yya[yyac] = new YYARec(304,44);yyac++; 
					yya[yyac] = new YYARec(305,45);yyac++; 
					yya[yyac] = new YYARec(306,46);yyac++; 
					yya[yyac] = new YYARec(307,47);yyac++; 
					yya[yyac] = new YYARec(309,29);yyac++; 
					yya[yyac] = new YYARec(310,119);yyac++; 
					yya[yyac] = new YYARec(312,31);yyac++; 
					yya[yyac] = new YYARec(313,32);yyac++; 
					yya[yyac] = new YYARec(314,48);yyac++; 
					yya[yyac] = new YYARec(315,49);yyac++; 
					yya[yyac] = new YYARec(316,120);yyac++; 
					yya[yyac] = new YYARec(317,33);yyac++; 
					yya[yyac] = new YYARec(318,50);yyac++; 
					yya[yyac] = new YYARec(319,121);yyac++; 
					yya[yyac] = new YYARec(320,51);yyac++; 
					yya[yyac] = new YYARec(321,200);yyac++; 
					yya[yyac] = new YYARec(322,201);yyac++; 
					yya[yyac] = new YYARec(323,52);yyac++; 
					yya[yyac] = new YYARec(274,199);yyac++; 
					yya[yyac] = new YYARec(282,82);yyac++; 
					yya[yyac] = new YYARec(283,83);yyac++; 
					yya[yyac] = new YYARec(287,84);yyac++; 
					yya[yyac] = new YYARec(298,42);yyac++; 
					yya[yyac] = new YYARec(299,24);yyac++; 
					yya[yyac] = new YYARec(300,43);yyac++; 
					yya[yyac] = new YYARec(301,26);yyac++; 
					yya[yyac] = new YYARec(302,27);yyac++; 
					yya[yyac] = new YYARec(304,44);yyac++; 
					yya[yyac] = new YYARec(305,45);yyac++; 
					yya[yyac] = new YYARec(306,46);yyac++; 
					yya[yyac] = new YYARec(307,47);yyac++; 
					yya[yyac] = new YYARec(309,29);yyac++; 
					yya[yyac] = new YYARec(310,119);yyac++; 
					yya[yyac] = new YYARec(312,31);yyac++; 
					yya[yyac] = new YYARec(313,32);yyac++; 
					yya[yyac] = new YYARec(314,48);yyac++; 
					yya[yyac] = new YYARec(315,49);yyac++; 
					yya[yyac] = new YYARec(316,120);yyac++; 
					yya[yyac] = new YYARec(317,33);yyac++; 
					yya[yyac] = new YYARec(318,50);yyac++; 
					yya[yyac] = new YYARec(319,121);yyac++; 
					yya[yyac] = new YYARec(320,51);yyac++; 
					yya[yyac] = new YYARec(321,200);yyac++; 
					yya[yyac] = new YYARec(322,201);yyac++; 
					yya[yyac] = new YYARec(323,52);yyac++; 
					yya[yyac] = new YYARec(274,199);yyac++; 
					yya[yyac] = new YYARec(282,82);yyac++; 
					yya[yyac] = new YYARec(283,83);yyac++; 
					yya[yyac] = new YYARec(287,84);yyac++; 
					yya[yyac] = new YYARec(298,42);yyac++; 
					yya[yyac] = new YYARec(299,24);yyac++; 
					yya[yyac] = new YYARec(300,43);yyac++; 
					yya[yyac] = new YYARec(301,26);yyac++; 
					yya[yyac] = new YYARec(302,27);yyac++; 
					yya[yyac] = new YYARec(304,44);yyac++; 
					yya[yyac] = new YYARec(305,45);yyac++; 
					yya[yyac] = new YYARec(306,46);yyac++; 
					yya[yyac] = new YYARec(307,47);yyac++; 
					yya[yyac] = new YYARec(309,29);yyac++; 
					yya[yyac] = new YYARec(310,119);yyac++; 
					yya[yyac] = new YYARec(312,31);yyac++; 
					yya[yyac] = new YYARec(313,32);yyac++; 
					yya[yyac] = new YYARec(314,48);yyac++; 
					yya[yyac] = new YYARec(315,49);yyac++; 
					yya[yyac] = new YYARec(316,120);yyac++; 
					yya[yyac] = new YYARec(317,33);yyac++; 
					yya[yyac] = new YYARec(318,50);yyac++; 
					yya[yyac] = new YYARec(319,121);yyac++; 
					yya[yyac] = new YYARec(320,51);yyac++; 
					yya[yyac] = new YYARec(321,200);yyac++; 
					yya[yyac] = new YYARec(322,201);yyac++; 
					yya[yyac] = new YYARec(323,52);yyac++; 
					yya[yyac] = new YYARec(274,199);yyac++; 
					yya[yyac] = new YYARec(282,82);yyac++; 
					yya[yyac] = new YYARec(283,83);yyac++; 
					yya[yyac] = new YYARec(287,84);yyac++; 
					yya[yyac] = new YYARec(298,42);yyac++; 
					yya[yyac] = new YYARec(299,24);yyac++; 
					yya[yyac] = new YYARec(300,43);yyac++; 
					yya[yyac] = new YYARec(301,26);yyac++; 
					yya[yyac] = new YYARec(302,27);yyac++; 
					yya[yyac] = new YYARec(304,44);yyac++; 
					yya[yyac] = new YYARec(305,45);yyac++; 
					yya[yyac] = new YYARec(306,46);yyac++; 
					yya[yyac] = new YYARec(307,47);yyac++; 
					yya[yyac] = new YYARec(309,29);yyac++; 
					yya[yyac] = new YYARec(310,119);yyac++; 
					yya[yyac] = new YYARec(312,31);yyac++; 
					yya[yyac] = new YYARec(313,32);yyac++; 
					yya[yyac] = new YYARec(314,48);yyac++; 
					yya[yyac] = new YYARec(315,49);yyac++; 
					yya[yyac] = new YYARec(316,120);yyac++; 
					yya[yyac] = new YYARec(317,33);yyac++; 
					yya[yyac] = new YYARec(318,50);yyac++; 
					yya[yyac] = new YYARec(319,121);yyac++; 
					yya[yyac] = new YYARec(320,51);yyac++; 
					yya[yyac] = new YYARec(321,200);yyac++; 
					yya[yyac] = new YYARec(322,201);yyac++; 
					yya[yyac] = new YYARec(323,52);yyac++; 
					yya[yyac] = new YYARec(274,199);yyac++; 
					yya[yyac] = new YYARec(282,82);yyac++; 
					yya[yyac] = new YYARec(283,83);yyac++; 
					yya[yyac] = new YYARec(287,84);yyac++; 
					yya[yyac] = new YYARec(298,42);yyac++; 
					yya[yyac] = new YYARec(299,24);yyac++; 
					yya[yyac] = new YYARec(300,43);yyac++; 
					yya[yyac] = new YYARec(301,26);yyac++; 
					yya[yyac] = new YYARec(302,27);yyac++; 
					yya[yyac] = new YYARec(304,44);yyac++; 
					yya[yyac] = new YYARec(305,45);yyac++; 
					yya[yyac] = new YYARec(306,46);yyac++; 
					yya[yyac] = new YYARec(307,47);yyac++; 
					yya[yyac] = new YYARec(309,29);yyac++; 
					yya[yyac] = new YYARec(310,119);yyac++; 
					yya[yyac] = new YYARec(312,31);yyac++; 
					yya[yyac] = new YYARec(313,32);yyac++; 
					yya[yyac] = new YYARec(314,48);yyac++; 
					yya[yyac] = new YYARec(315,49);yyac++; 
					yya[yyac] = new YYARec(316,120);yyac++; 
					yya[yyac] = new YYARec(317,33);yyac++; 
					yya[yyac] = new YYARec(318,50);yyac++; 
					yya[yyac] = new YYARec(319,121);yyac++; 
					yya[yyac] = new YYARec(320,51);yyac++; 
					yya[yyac] = new YYARec(321,200);yyac++; 
					yya[yyac] = new YYARec(322,201);yyac++; 
					yya[yyac] = new YYARec(323,52);yyac++; 
					yya[yyac] = new YYARec(275,297);yyac++; 
					yya[yyac] = new YYARec(267,298);yyac++; 
					yya[yyac] = new YYARec(257,132);yyac++; 
					yya[yyac] = new YYARec(259,133);yyac++; 
					yya[yyac] = new YYARec(266,134);yyac++; 
					yya[yyac] = new YYARec(288,135);yyac++; 
					yya[yyac] = new YYARec(294,136);yyac++; 
					yya[yyac] = new YYARec(295,137);yyac++; 
					yya[yyac] = new YYARec(296,138);yyac++; 
					yya[yyac] = new YYARec(298,42);yyac++; 
					yya[yyac] = new YYARec(299,24);yyac++; 
					yya[yyac] = new YYARec(300,43);yyac++; 
					yya[yyac] = new YYARec(301,26);yyac++; 
					yya[yyac] = new YYARec(302,27);yyac++; 
					yya[yyac] = new YYARec(304,44);yyac++; 
					yya[yyac] = new YYARec(305,45);yyac++; 
					yya[yyac] = new YYARec(306,46);yyac++; 
					yya[yyac] = new YYARec(307,47);yyac++; 
					yya[yyac] = new YYARec(309,29);yyac++; 
					yya[yyac] = new YYARec(310,139);yyac++; 
					yya[yyac] = new YYARec(312,31);yyac++; 
					yya[yyac] = new YYARec(313,32);yyac++; 
					yya[yyac] = new YYARec(314,140);yyac++; 
					yya[yyac] = new YYARec(315,49);yyac++; 
					yya[yyac] = new YYARec(316,120);yyac++; 
					yya[yyac] = new YYARec(317,33);yyac++; 
					yya[yyac] = new YYARec(318,50);yyac++; 
					yya[yyac] = new YYARec(319,121);yyac++; 
					yya[yyac] = new YYARec(320,51);yyac++; 
					yya[yyac] = new YYARec(323,141);yyac++; 
					yya[yyac] = new YYARec(267,-85 );yyac++; 
					yya[yyac] = new YYARec(257,132);yyac++; 
					yya[yyac] = new YYARec(259,133);yyac++; 
					yya[yyac] = new YYARec(266,134);yyac++; 
					yya[yyac] = new YYARec(288,135);yyac++; 
					yya[yyac] = new YYARec(294,136);yyac++; 
					yya[yyac] = new YYARec(295,137);yyac++; 
					yya[yyac] = new YYARec(296,138);yyac++; 
					yya[yyac] = new YYARec(298,42);yyac++; 
					yya[yyac] = new YYARec(299,24);yyac++; 
					yya[yyac] = new YYARec(300,43);yyac++; 
					yya[yyac] = new YYARec(301,26);yyac++; 
					yya[yyac] = new YYARec(302,27);yyac++; 
					yya[yyac] = new YYARec(304,44);yyac++; 
					yya[yyac] = new YYARec(305,45);yyac++; 
					yya[yyac] = new YYARec(306,46);yyac++; 
					yya[yyac] = new YYARec(307,47);yyac++; 
					yya[yyac] = new YYARec(309,29);yyac++; 
					yya[yyac] = new YYARec(310,139);yyac++; 
					yya[yyac] = new YYARec(312,31);yyac++; 
					yya[yyac] = new YYARec(313,32);yyac++; 
					yya[yyac] = new YYARec(314,140);yyac++; 
					yya[yyac] = new YYARec(315,49);yyac++; 
					yya[yyac] = new YYARec(316,120);yyac++; 
					yya[yyac] = new YYARec(317,33);yyac++; 
					yya[yyac] = new YYARec(318,50);yyac++; 
					yya[yyac] = new YYARec(319,121);yyac++; 
					yya[yyac] = new YYARec(320,51);yyac++; 
					yya[yyac] = new YYARec(323,141);yyac++; 
					yya[yyac] = new YYARec(267,-85 );yyac++; 
					yya[yyac] = new YYARec(297,301);yyac++; 
					yya[yyac] = new YYARec(258,-75 );yyac++; 
					yya[yyac] = new YYARec(263,-75 );yyac++; 
					yya[yyac] = new YYARec(282,-75 );yyac++; 
					yya[yyac] = new YYARec(283,-75 );yyac++; 
					yya[yyac] = new YYARec(287,-75 );yyac++; 
					yya[yyac] = new YYARec(298,-75 );yyac++; 
					yya[yyac] = new YYARec(300,-75 );yyac++; 
					yya[yyac] = new YYARec(301,-75 );yyac++; 
					yya[yyac] = new YYARec(302,-75 );yyac++; 
					yya[yyac] = new YYARec(303,-75 );yyac++; 
					yya[yyac] = new YYARec(304,-75 );yyac++; 
					yya[yyac] = new YYARec(305,-75 );yyac++; 
					yya[yyac] = new YYARec(306,-75 );yyac++; 
					yya[yyac] = new YYARec(307,-75 );yyac++; 
					yya[yyac] = new YYARec(309,-75 );yyac++; 
					yya[yyac] = new YYARec(310,-75 );yyac++; 
					yya[yyac] = new YYARec(313,-75 );yyac++; 
					yya[yyac] = new YYARec(314,-75 );yyac++; 
					yya[yyac] = new YYARec(315,-75 );yyac++; 
					yya[yyac] = new YYARec(316,-75 );yyac++; 
					yya[yyac] = new YYARec(317,-75 );yyac++; 
					yya[yyac] = new YYARec(318,-75 );yyac++; 
					yya[yyac] = new YYARec(319,-75 );yyac++; 
					yya[yyac] = new YYARec(320,-75 );yyac++; 
					yya[yyac] = new YYARec(321,-75 );yyac++; 
					yya[yyac] = new YYARec(322,-75 );yyac++; 
					yya[yyac] = new YYARec(323,-75 );yyac++; 
					yya[yyac] = new YYARec(324,-75 );yyac++; 
					yya[yyac] = new YYARec(325,-75 );yyac++; 
					yya[yyac] = new YYARec(263,58);yyac++; 
					yya[yyac] = new YYARec(258,-97 );yyac++; 
					yya[yyac] = new YYARec(282,-97 );yyac++; 
					yya[yyac] = new YYARec(283,-97 );yyac++; 
					yya[yyac] = new YYARec(287,-97 );yyac++; 
					yya[yyac] = new YYARec(298,-97 );yyac++; 
					yya[yyac] = new YYARec(300,-97 );yyac++; 
					yya[yyac] = new YYARec(301,-97 );yyac++; 
					yya[yyac] = new YYARec(302,-97 );yyac++; 
					yya[yyac] = new YYARec(303,-97 );yyac++; 
					yya[yyac] = new YYARec(304,-97 );yyac++; 
					yya[yyac] = new YYARec(305,-97 );yyac++; 
					yya[yyac] = new YYARec(306,-97 );yyac++; 
					yya[yyac] = new YYARec(307,-97 );yyac++; 
					yya[yyac] = new YYARec(309,-97 );yyac++; 
					yya[yyac] = new YYARec(310,-97 );yyac++; 
					yya[yyac] = new YYARec(313,-97 );yyac++; 
					yya[yyac] = new YYARec(314,-97 );yyac++; 
					yya[yyac] = new YYARec(315,-97 );yyac++; 
					yya[yyac] = new YYARec(316,-97 );yyac++; 
					yya[yyac] = new YYARec(317,-97 );yyac++; 
					yya[yyac] = new YYARec(318,-97 );yyac++; 
					yya[yyac] = new YYARec(319,-97 );yyac++; 
					yya[yyac] = new YYARec(320,-97 );yyac++; 
					yya[yyac] = new YYARec(321,-97 );yyac++; 
					yya[yyac] = new YYARec(322,-97 );yyac++; 
					yya[yyac] = new YYARec(323,-97 );yyac++; 
					yya[yyac] = new YYARec(324,-97 );yyac++; 
					yya[yyac] = new YYARec(325,-97 );yyac++; 
					yya[yyac] = new YYARec(297,303);yyac++; 
					yya[yyac] = new YYARec(258,-76 );yyac++; 
					yya[yyac] = new YYARec(263,-76 );yyac++; 
					yya[yyac] = new YYARec(282,-76 );yyac++; 
					yya[yyac] = new YYARec(283,-76 );yyac++; 
					yya[yyac] = new YYARec(287,-76 );yyac++; 
					yya[yyac] = new YYARec(298,-76 );yyac++; 
					yya[yyac] = new YYARec(300,-76 );yyac++; 
					yya[yyac] = new YYARec(301,-76 );yyac++; 
					yya[yyac] = new YYARec(302,-76 );yyac++; 
					yya[yyac] = new YYARec(303,-76 );yyac++; 
					yya[yyac] = new YYARec(304,-76 );yyac++; 
					yya[yyac] = new YYARec(305,-76 );yyac++; 
					yya[yyac] = new YYARec(306,-76 );yyac++; 
					yya[yyac] = new YYARec(307,-76 );yyac++; 
					yya[yyac] = new YYARec(309,-76 );yyac++; 
					yya[yyac] = new YYARec(310,-76 );yyac++; 
					yya[yyac] = new YYARec(313,-76 );yyac++; 
					yya[yyac] = new YYARec(314,-76 );yyac++; 
					yya[yyac] = new YYARec(315,-76 );yyac++; 
					yya[yyac] = new YYARec(316,-76 );yyac++; 
					yya[yyac] = new YYARec(317,-76 );yyac++; 
					yya[yyac] = new YYARec(318,-76 );yyac++; 
					yya[yyac] = new YYARec(319,-76 );yyac++; 
					yya[yyac] = new YYARec(320,-76 );yyac++; 
					yya[yyac] = new YYARec(321,-76 );yyac++; 
					yya[yyac] = new YYARec(322,-76 );yyac++; 
					yya[yyac] = new YYARec(323,-76 );yyac++; 
					yya[yyac] = new YYARec(324,-76 );yyac++; 
					yya[yyac] = new YYARec(325,-76 );yyac++; 
					yya[yyac] = new YYARec(257,150);yyac++; 
					yya[yyac] = new YYARec(259,151);yyac++; 
					yya[yyac] = new YYARec(301,26);yyac++; 
					yya[yyac] = new YYARec(302,27);yyac++; 
					yya[yyac] = new YYARec(306,152);yyac++; 
					yya[yyac] = new YYARec(311,153);yyac++; 
					yya[yyac] = new YYARec(312,154);yyac++; 
					yya[yyac] = new YYARec(315,155);yyac++; 
					yya[yyac] = new YYARec(317,156);yyac++; 
					yya[yyac] = new YYARec(319,157);yyac++; 
					yya[yyac] = new YYARec(320,158);yyac++; 
					yya[yyac] = new YYARec(323,64);yyac++; 
					yya[yyac] = new YYARec(260,-62 );yyac++; 
					yya[yyac] = new YYARec(261,-62 );yyac++; 
					yya[yyac] = new YYARec(257,150);yyac++; 
					yya[yyac] = new YYARec(259,151);yyac++; 
					yya[yyac] = new YYARec(301,26);yyac++; 
					yya[yyac] = new YYARec(302,27);yyac++; 
					yya[yyac] = new YYARec(306,152);yyac++; 
					yya[yyac] = new YYARec(311,153);yyac++; 
					yya[yyac] = new YYARec(312,154);yyac++; 
					yya[yyac] = new YYARec(315,155);yyac++; 
					yya[yyac] = new YYARec(317,156);yyac++; 
					yya[yyac] = new YYARec(319,157);yyac++; 
					yya[yyac] = new YYARec(320,158);yyac++; 
					yya[yyac] = new YYARec(323,64);yyac++; 
					yya[yyac] = new YYARec(260,-62 );yyac++; 
					yya[yyac] = new YYARec(261,-62 );yyac++; 
					yya[yyac] = new YYARec(282,82);yyac++; 
					yya[yyac] = new YYARec(283,83);yyac++; 
					yya[yyac] = new YYARec(287,84);yyac++; 
					yya[yyac] = new YYARec(321,85);yyac++; 
					yya[yyac] = new YYARec(258,-52 );yyac++; 
					yya[yyac] = new YYARec(261,308);yyac++; 
					yya[yyac] = new YYARec(282,82);yyac++; 
					yya[yyac] = new YYARec(283,83);yyac++; 
					yya[yyac] = new YYARec(287,84);yyac++; 
					yya[yyac] = new YYARec(298,42);yyac++; 
					yya[yyac] = new YYARec(299,24);yyac++; 
					yya[yyac] = new YYARec(300,43);yyac++; 
					yya[yyac] = new YYARec(301,26);yyac++; 
					yya[yyac] = new YYARec(302,27);yyac++; 
					yya[yyac] = new YYARec(304,44);yyac++; 
					yya[yyac] = new YYARec(305,45);yyac++; 
					yya[yyac] = new YYARec(306,46);yyac++; 
					yya[yyac] = new YYARec(307,47);yyac++; 
					yya[yyac] = new YYARec(308,118);yyac++; 
					yya[yyac] = new YYARec(309,29);yyac++; 
					yya[yyac] = new YYARec(310,119);yyac++; 
					yya[yyac] = new YYARec(312,31);yyac++; 
					yya[yyac] = new YYARec(313,32);yyac++; 
					yya[yyac] = new YYARec(314,48);yyac++; 
					yya[yyac] = new YYARec(315,49);yyac++; 
					yya[yyac] = new YYARec(316,120);yyac++; 
					yya[yyac] = new YYARec(317,33);yyac++; 
					yya[yyac] = new YYARec(318,50);yyac++; 
					yya[yyac] = new YYARec(319,121);yyac++; 
					yya[yyac] = new YYARec(320,51);yyac++; 
					yya[yyac] = new YYARec(321,85);yyac++; 
					yya[yyac] = new YYARec(322,86);yyac++; 
					yya[yyac] = new YYARec(323,52);yyac++; 
					yya[yyac] = new YYARec(324,69);yyac++; 
					yya[yyac] = new YYARec(325,87);yyac++; 
					yya[yyac] = new YYARec(258,-89 );yyac++; 
					yya[yyac] = new YYARec(260,310);yyac++; 
					yya[yyac] = new YYARec(261,311);yyac++; 
					yya[yyac] = new YYARec(258,312);yyac++; 
					yya[yyac] = new YYARec(258,313);yyac++; 
					yya[yyac] = new YYARec(275,314);yyac++; 
					yya[yyac] = new YYARec(284,241);yyac++; 
					yya[yyac] = new YYARec(285,242);yyac++; 
					yya[yyac] = new YYARec(286,243);yyac++; 
					yya[yyac] = new YYARec(258,-114 );yyac++; 
					yya[yyac] = new YYARec(266,-114 );yyac++; 
					yya[yyac] = new YYARec(269,-114 );yyac++; 
					yya[yyac] = new YYARec(270,-114 );yyac++; 
					yya[yyac] = new YYARec(271,-114 );yyac++; 
					yya[yyac] = new YYARec(272,-114 );yyac++; 
					yya[yyac] = new YYARec(273,-114 );yyac++; 
					yya[yyac] = new YYARec(275,-114 );yyac++; 
					yya[yyac] = new YYARec(276,-114 );yyac++; 
					yya[yyac] = new YYARec(277,-114 );yyac++; 
					yya[yyac] = new YYARec(278,-114 );yyac++; 
					yya[yyac] = new YYARec(279,-114 );yyac++; 
					yya[yyac] = new YYARec(280,-114 );yyac++; 
					yya[yyac] = new YYARec(281,-114 );yyac++; 
					yya[yyac] = new YYARec(282,-114 );yyac++; 
					yya[yyac] = new YYARec(283,-114 );yyac++; 
					yya[yyac] = new YYARec(282,245);yyac++; 
					yya[yyac] = new YYARec(283,246);yyac++; 
					yya[yyac] = new YYARec(258,-112 );yyac++; 
					yya[yyac] = new YYARec(266,-112 );yyac++; 
					yya[yyac] = new YYARec(269,-112 );yyac++; 
					yya[yyac] = new YYARec(270,-112 );yyac++; 
					yya[yyac] = new YYARec(271,-112 );yyac++; 
					yya[yyac] = new YYARec(272,-112 );yyac++; 
					yya[yyac] = new YYARec(273,-112 );yyac++; 
					yya[yyac] = new YYARec(275,-112 );yyac++; 
					yya[yyac] = new YYARec(276,-112 );yyac++; 
					yya[yyac] = new YYARec(277,-112 );yyac++; 
					yya[yyac] = new YYARec(278,-112 );yyac++; 
					yya[yyac] = new YYARec(279,-112 );yyac++; 
					yya[yyac] = new YYARec(280,-112 );yyac++; 
					yya[yyac] = new YYARec(281,-112 );yyac++; 
					yya[yyac] = new YYARec(278,248);yyac++; 
					yya[yyac] = new YYARec(279,249);yyac++; 
					yya[yyac] = new YYARec(280,250);yyac++; 
					yya[yyac] = new YYARec(281,251);yyac++; 
					yya[yyac] = new YYARec(258,-110 );yyac++; 
					yya[yyac] = new YYARec(266,-110 );yyac++; 
					yya[yyac] = new YYARec(269,-110 );yyac++; 
					yya[yyac] = new YYARec(270,-110 );yyac++; 
					yya[yyac] = new YYARec(271,-110 );yyac++; 
					yya[yyac] = new YYARec(272,-110 );yyac++; 
					yya[yyac] = new YYARec(273,-110 );yyac++; 
					yya[yyac] = new YYARec(275,-110 );yyac++; 
					yya[yyac] = new YYARec(276,-110 );yyac++; 
					yya[yyac] = new YYARec(277,-110 );yyac++; 
					yya[yyac] = new YYARec(276,253);yyac++; 
					yya[yyac] = new YYARec(277,254);yyac++; 
					yya[yyac] = new YYARec(258,-107 );yyac++; 
					yya[yyac] = new YYARec(266,-107 );yyac++; 
					yya[yyac] = new YYARec(269,-107 );yyac++; 
					yya[yyac] = new YYARec(270,-107 );yyac++; 
					yya[yyac] = new YYARec(271,-107 );yyac++; 
					yya[yyac] = new YYARec(272,-107 );yyac++; 
					yya[yyac] = new YYARec(273,-107 );yyac++; 
					yya[yyac] = new YYARec(275,-107 );yyac++; 
					yya[yyac] = new YYARec(273,255);yyac++; 
					yya[yyac] = new YYARec(258,-105 );yyac++; 
					yya[yyac] = new YYARec(266,-105 );yyac++; 
					yya[yyac] = new YYARec(269,-105 );yyac++; 
					yya[yyac] = new YYARec(270,-105 );yyac++; 
					yya[yyac] = new YYARec(271,-105 );yyac++; 
					yya[yyac] = new YYARec(272,-105 );yyac++; 
					yya[yyac] = new YYARec(275,-105 );yyac++; 
					yya[yyac] = new YYARec(272,256);yyac++; 
					yya[yyac] = new YYARec(258,-103 );yyac++; 
					yya[yyac] = new YYARec(266,-103 );yyac++; 
					yya[yyac] = new YYARec(269,-103 );yyac++; 
					yya[yyac] = new YYARec(270,-103 );yyac++; 
					yya[yyac] = new YYARec(271,-103 );yyac++; 
					yya[yyac] = new YYARec(275,-103 );yyac++; 
					yya[yyac] = new YYARec(271,257);yyac++; 
					yya[yyac] = new YYARec(258,-101 );yyac++; 
					yya[yyac] = new YYARec(266,-101 );yyac++; 
					yya[yyac] = new YYARec(269,-101 );yyac++; 
					yya[yyac] = new YYARec(270,-101 );yyac++; 
					yya[yyac] = new YYARec(275,-101 );yyac++; 
					yya[yyac] = new YYARec(270,258);yyac++; 
					yya[yyac] = new YYARec(258,-99 );yyac++; 
					yya[yyac] = new YYARec(266,-99 );yyac++; 
					yya[yyac] = new YYARec(269,-99 );yyac++; 
					yya[yyac] = new YYARec(275,-99 );yyac++; 
					yya[yyac] = new YYARec(267,315);yyac++; 
					yya[yyac] = new YYARec(267,316);yyac++; 
					yya[yyac] = new YYARec(301,26);yyac++; 
					yya[yyac] = new YYARec(302,27);yyac++; 
					yya[yyac] = new YYARec(305,219);yyac++; 
					yya[yyac] = new YYARec(306,152);yyac++; 
					yya[yyac] = new YYARec(311,153);yyac++; 
					yya[yyac] = new YYARec(312,154);yyac++; 
					yya[yyac] = new YYARec(313,220);yyac++; 
					yya[yyac] = new YYARec(315,155);yyac++; 
					yya[yyac] = new YYARec(316,221);yyac++; 
					yya[yyac] = new YYARec(317,156);yyac++; 
					yya[yyac] = new YYARec(318,222);yyac++; 
					yya[yyac] = new YYARec(319,157);yyac++; 
					yya[yyac] = new YYARec(320,158);yyac++; 
					yya[yyac] = new YYARec(323,64);yyac++; 
					yya[yyac] = new YYARec(282,82);yyac++; 
					yya[yyac] = new YYARec(283,83);yyac++; 
					yya[yyac] = new YYARec(287,84);yyac++; 
					yya[yyac] = new YYARec(298,42);yyac++; 
					yya[yyac] = new YYARec(300,43);yyac++; 
					yya[yyac] = new YYARec(301,26);yyac++; 
					yya[yyac] = new YYARec(302,27);yyac++; 
					yya[yyac] = new YYARec(303,28);yyac++; 
					yya[yyac] = new YYARec(304,44);yyac++; 
					yya[yyac] = new YYARec(305,45);yyac++; 
					yya[yyac] = new YYARec(306,46);yyac++; 
					yya[yyac] = new YYARec(307,47);yyac++; 
					yya[yyac] = new YYARec(309,29);yyac++; 
					yya[yyac] = new YYARec(310,119);yyac++; 
					yya[yyac] = new YYARec(313,32);yyac++; 
					yya[yyac] = new YYARec(314,48);yyac++; 
					yya[yyac] = new YYARec(315,49);yyac++; 
					yya[yyac] = new YYARec(316,120);yyac++; 
					yya[yyac] = new YYARec(317,33);yyac++; 
					yya[yyac] = new YYARec(318,50);yyac++; 
					yya[yyac] = new YYARec(319,121);yyac++; 
					yya[yyac] = new YYARec(320,51);yyac++; 
					yya[yyac] = new YYARec(321,85);yyac++; 
					yya[yyac] = new YYARec(322,86);yyac++; 
					yya[yyac] = new YYARec(323,52);yyac++; 
					yya[yyac] = new YYARec(324,69);yyac++; 
					yya[yyac] = new YYARec(325,87);yyac++; 
					yya[yyac] = new YYARec(258,-64 );yyac++; 
					yya[yyac] = new YYARec(301,26);yyac++; 
					yya[yyac] = new YYARec(302,27);yyac++; 
					yya[yyac] = new YYARec(305,219);yyac++; 
					yya[yyac] = new YYARec(306,152);yyac++; 
					yya[yyac] = new YYARec(311,153);yyac++; 
					yya[yyac] = new YYARec(312,154);yyac++; 
					yya[yyac] = new YYARec(313,220);yyac++; 
					yya[yyac] = new YYARec(315,155);yyac++; 
					yya[yyac] = new YYARec(316,221);yyac++; 
					yya[yyac] = new YYARec(317,156);yyac++; 
					yya[yyac] = new YYARec(318,222);yyac++; 
					yya[yyac] = new YYARec(319,157);yyac++; 
					yya[yyac] = new YYARec(320,158);yyac++; 
					yya[yyac] = new YYARec(323,64);yyac++; 
					yya[yyac] = new YYARec(260,322);yyac++; 
					yya[yyac] = new YYARec(261,323);yyac++; 
					yya[yyac] = new YYARec(258,324);yyac++; 
					yya[yyac] = new YYARec(258,325);yyac++; 
					yya[yyac] = new YYARec(258,326);yyac++; 
					yya[yyac] = new YYARec(258,327);yyac++; 
					yya[yyac] = new YYARec(257,132);yyac++; 
					yya[yyac] = new YYARec(259,133);yyac++; 
					yya[yyac] = new YYARec(266,134);yyac++; 
					yya[yyac] = new YYARec(288,135);yyac++; 
					yya[yyac] = new YYARec(294,136);yyac++; 
					yya[yyac] = new YYARec(295,137);yyac++; 
					yya[yyac] = new YYARec(296,138);yyac++; 
					yya[yyac] = new YYARec(298,42);yyac++; 
					yya[yyac] = new YYARec(299,24);yyac++; 
					yya[yyac] = new YYARec(300,43);yyac++; 
					yya[yyac] = new YYARec(301,26);yyac++; 
					yya[yyac] = new YYARec(302,27);yyac++; 
					yya[yyac] = new YYARec(304,44);yyac++; 
					yya[yyac] = new YYARec(305,45);yyac++; 
					yya[yyac] = new YYARec(306,46);yyac++; 
					yya[yyac] = new YYARec(307,47);yyac++; 
					yya[yyac] = new YYARec(309,29);yyac++; 
					yya[yyac] = new YYARec(310,139);yyac++; 
					yya[yyac] = new YYARec(312,31);yyac++; 
					yya[yyac] = new YYARec(313,32);yyac++; 
					yya[yyac] = new YYARec(314,140);yyac++; 
					yya[yyac] = new YYARec(315,49);yyac++; 
					yya[yyac] = new YYARec(316,120);yyac++; 
					yya[yyac] = new YYARec(317,33);yyac++; 
					yya[yyac] = new YYARec(318,50);yyac++; 
					yya[yyac] = new YYARec(319,121);yyac++; 
					yya[yyac] = new YYARec(320,51);yyac++; 
					yya[yyac] = new YYARec(323,141);yyac++; 
					yya[yyac] = new YYARec(261,-85 );yyac++; 
					yya[yyac] = new YYARec(257,150);yyac++; 
					yya[yyac] = new YYARec(259,151);yyac++; 
					yya[yyac] = new YYARec(301,26);yyac++; 
					yya[yyac] = new YYARec(302,27);yyac++; 
					yya[yyac] = new YYARec(306,152);yyac++; 
					yya[yyac] = new YYARec(311,153);yyac++; 
					yya[yyac] = new YYARec(312,154);yyac++; 
					yya[yyac] = new YYARec(315,155);yyac++; 
					yya[yyac] = new YYARec(317,156);yyac++; 
					yya[yyac] = new YYARec(319,157);yyac++; 
					yya[yyac] = new YYARec(320,158);yyac++; 
					yya[yyac] = new YYARec(323,64);yyac++; 
					yya[yyac] = new YYARec(261,-62 );yyac++; 
					yya[yyac] = new YYARec(261,330);yyac++; 
					yya[yyac] = new YYARec(261,331);yyac++;

					yyg[yygc] = new YYARec(-46,1);yygc++; 
					yyg[yygc] = new YYARec(-39,2);yygc++; 
					yyg[yygc] = new YYARec(-38,3);yygc++; 
					yyg[yygc] = new YYARec(-34,4);yygc++; 
					yyg[yygc] = new YYARec(-31,5);yygc++; 
					yyg[yygc] = new YYARec(-28,6);yygc++; 
					yyg[yygc] = new YYARec(-21,7);yygc++; 
					yyg[yygc] = new YYARec(-11,8);yygc++; 
					yyg[yygc] = new YYARec(-10,9);yygc++; 
					yyg[yygc] = new YYARec(-9,10);yygc++; 
					yyg[yygc] = new YYARec(-8,11);yygc++; 
					yyg[yygc] = new YYARec(-7,12);yygc++; 
					yyg[yygc] = new YYARec(-6,13);yygc++; 
					yyg[yygc] = new YYARec(-5,14);yygc++; 
					yyg[yygc] = new YYARec(-4,15);yygc++; 
					yyg[yygc] = new YYARec(-3,16);yygc++; 
					yyg[yygc] = new YYARec(-2,17);yygc++; 
					yyg[yygc] = new YYARec(-75,35);yygc++; 
					yyg[yygc] = new YYARec(-72,36);yygc++; 
					yyg[yygc] = new YYARec(-47,37);yygc++; 
					yyg[yygc] = new YYARec(-39,38);yygc++; 
					yyg[yygc] = new YYARec(-34,39);yygc++; 
					yyg[yygc] = new YYARec(-33,40);yygc++; 
					yyg[yygc] = new YYARec(-21,41);yygc++; 
					yyg[yygc] = new YYARec(-75,35);yygc++; 
					yyg[yygc] = new YYARec(-72,36);yygc++; 
					yyg[yygc] = new YYARec(-34,39);yygc++; 
					yyg[yygc] = new YYARec(-33,53);yygc++; 
					yyg[yygc] = new YYARec(-21,41);yygc++; 
					yyg[yygc] = new YYARec(-75,35);yygc++; 
					yyg[yygc] = new YYARec(-72,36);yygc++; 
					yyg[yygc] = new YYARec(-35,54);yygc++; 
					yyg[yygc] = new YYARec(-34,39);yygc++; 
					yyg[yygc] = new YYARec(-33,55);yygc++; 
					yyg[yygc] = new YYARec(-21,41);yygc++; 
					yyg[yygc] = new YYARec(-29,57);yygc++; 
					yyg[yygc] = new YYARec(-29,59);yygc++; 
					yyg[yygc] = new YYARec(-46,1);yygc++; 
					yyg[yygc] = new YYARec(-39,2);yygc++; 
					yyg[yygc] = new YYARec(-38,3);yygc++; 
					yyg[yygc] = new YYARec(-34,4);yygc++; 
					yyg[yygc] = new YYARec(-31,5);yygc++; 
					yyg[yygc] = new YYARec(-28,6);yygc++; 
					yyg[yygc] = new YYARec(-21,7);yygc++; 
					yyg[yygc] = new YYARec(-11,8);yygc++; 
					yyg[yygc] = new YYARec(-10,9);yygc++; 
					yyg[yygc] = new YYARec(-9,10);yygc++; 
					yyg[yygc] = new YYARec(-8,11);yygc++; 
					yyg[yygc] = new YYARec(-7,12);yygc++; 
					yyg[yygc] = new YYARec(-6,13);yygc++; 
					yyg[yygc] = new YYARec(-5,14);yygc++; 
					yyg[yygc] = new YYARec(-4,15);yygc++; 
					yyg[yygc] = new YYARec(-3,60);yygc++; 
					yyg[yygc] = new YYARec(-22,61);yygc++; 
					yyg[yygc] = new YYARec(-21,62);yygc++; 
					yyg[yygc] = new YYARec(-12,63);yygc++; 
					yyg[yygc] = new YYARec(-22,61);yygc++; 
					yyg[yygc] = new YYARec(-21,62);yygc++; 
					yyg[yygc] = new YYARec(-12,65);yygc++; 
					yyg[yygc] = new YYARec(-22,61);yygc++; 
					yyg[yygc] = new YYARec(-21,62);yygc++; 
					yyg[yygc] = new YYARec(-12,66);yygc++; 
					yyg[yygc] = new YYARec(-22,61);yygc++; 
					yyg[yygc] = new YYARec(-21,62);yygc++; 
					yyg[yygc] = new YYARec(-12,67);yygc++; 
					yyg[yygc] = new YYARec(-25,68);yygc++; 
					yyg[yygc] = new YYARec(-29,70);yygc++; 
					yyg[yygc] = new YYARec(-29,71);yygc++; 
					yyg[yygc] = new YYARec(-29,73);yygc++; 
					yyg[yygc] = new YYARec(-76,74);yygc++; 
					yyg[yygc] = new YYARec(-71,75);yygc++; 
					yyg[yygc] = new YYARec(-37,76);yygc++; 
					yyg[yygc] = new YYARec(-32,77);yygc++; 
					yyg[yygc] = new YYARec(-26,78);yygc++; 
					yyg[yygc] = new YYARec(-25,79);yygc++; 
					yyg[yygc] = new YYARec(-23,80);yygc++; 
					yyg[yygc] = new YYARec(-22,81);yygc++; 
					yyg[yygc] = new YYARec(-75,35);yygc++; 
					yyg[yygc] = new YYARec(-72,36);yygc++; 
					yyg[yygc] = new YYARec(-34,39);yygc++; 
					yyg[yygc] = new YYARec(-33,88);yygc++; 
					yyg[yygc] = new YYARec(-30,89);yygc++; 
					yyg[yygc] = new YYARec(-21,41);yygc++; 
					yyg[yygc] = new YYARec(-23,97);yygc++; 
					yyg[yygc] = new YYARec(-25,99);yygc++; 
					yyg[yygc] = new YYARec(-29,103);yygc++; 
					yyg[yygc] = new YYARec(-46,1);yygc++; 
					yyg[yygc] = new YYARec(-39,2);yygc++; 
					yyg[yygc] = new YYARec(-38,3);yygc++; 
					yyg[yygc] = new YYARec(-34,4);yygc++; 
					yyg[yygc] = new YYARec(-31,5);yygc++; 
					yyg[yygc] = new YYARec(-28,6);yygc++; 
					yyg[yygc] = new YYARec(-21,7);yygc++; 
					yyg[yygc] = new YYARec(-13,105);yygc++; 
					yyg[yygc] = new YYARec(-11,8);yygc++; 
					yyg[yygc] = new YYARec(-10,9);yygc++; 
					yyg[yygc] = new YYARec(-9,10);yygc++; 
					yyg[yygc] = new YYARec(-8,11);yygc++; 
					yyg[yygc] = new YYARec(-7,12);yygc++; 
					yyg[yygc] = new YYARec(-6,13);yygc++; 
					yyg[yygc] = new YYARec(-5,14);yygc++; 
					yyg[yygc] = new YYARec(-4,15);yygc++; 
					yyg[yygc] = new YYARec(-3,106);yygc++; 
					yyg[yygc] = new YYARec(-46,1);yygc++; 
					yyg[yygc] = new YYARec(-39,2);yygc++; 
					yyg[yygc] = new YYARec(-38,3);yygc++; 
					yyg[yygc] = new YYARec(-34,4);yygc++; 
					yyg[yygc] = new YYARec(-31,5);yygc++; 
					yyg[yygc] = new YYARec(-28,6);yygc++; 
					yyg[yygc] = new YYARec(-21,7);yygc++; 
					yyg[yygc] = new YYARec(-13,107);yygc++; 
					yyg[yygc] = new YYARec(-11,8);yygc++; 
					yyg[yygc] = new YYARec(-10,9);yygc++; 
					yyg[yygc] = new YYARec(-9,10);yygc++; 
					yyg[yygc] = new YYARec(-8,11);yygc++; 
					yyg[yygc] = new YYARec(-7,12);yygc++; 
					yyg[yygc] = new YYARec(-6,13);yygc++; 
					yyg[yygc] = new YYARec(-5,14);yygc++; 
					yyg[yygc] = new YYARec(-4,15);yygc++; 
					yyg[yygc] = new YYARec(-3,106);yygc++; 
					yyg[yygc] = new YYARec(-76,74);yygc++; 
					yyg[yygc] = new YYARec(-75,35);yygc++; 
					yyg[yygc] = new YYARec(-72,36);yygc++; 
					yyg[yygc] = new YYARec(-71,75);yygc++; 
					yyg[yygc] = new YYARec(-45,108);yygc++; 
					yyg[yygc] = new YYARec(-39,2);yygc++; 
					yyg[yygc] = new YYARec(-38,109);yygc++; 
					yyg[yygc] = new YYARec(-37,76);yygc++; 
					yyg[yygc] = new YYARec(-34,39);yygc++; 
					yyg[yygc] = new YYARec(-33,110);yygc++; 
					yyg[yygc] = new YYARec(-28,111);yygc++; 
					yyg[yygc] = new YYARec(-27,112);yygc++; 
					yyg[yygc] = new YYARec(-26,113);yygc++; 
					yyg[yygc] = new YYARec(-25,114);yygc++; 
					yyg[yygc] = new YYARec(-24,115);yygc++; 
					yyg[yygc] = new YYARec(-23,116);yygc++; 
					yyg[yygc] = new YYARec(-21,41);yygc++; 
					yyg[yygc] = new YYARec(-20,117);yygc++; 
					yyg[yygc] = new YYARec(-75,122);yygc++; 
					yyg[yygc] = new YYARec(-72,36);yygc++; 
					yyg[yygc] = new YYARec(-52,123);yygc++; 
					yyg[yygc] = new YYARec(-51,124);yygc++; 
					yyg[yygc] = new YYARec(-50,125);yygc++; 
					yyg[yygc] = new YYARec(-49,126);yygc++; 
					yyg[yygc] = new YYARec(-48,127);yygc++; 
					yyg[yygc] = new YYARec(-45,108);yygc++; 
					yyg[yygc] = new YYARec(-39,2);yygc++; 
					yyg[yygc] = new YYARec(-38,109);yygc++; 
					yyg[yygc] = new YYARec(-34,39);yygc++; 
					yyg[yygc] = new YYARec(-33,128);yygc++; 
					yyg[yygc] = new YYARec(-28,111);yygc++; 
					yyg[yygc] = new YYARec(-27,129);yygc++; 
					yyg[yygc] = new YYARec(-21,41);yygc++; 
					yyg[yygc] = new YYARec(-16,130);yygc++; 
					yyg[yygc] = new YYARec(-14,131);yygc++; 
					yyg[yygc] = new YYARec(-41,143);yygc++; 
					yyg[yygc] = new YYARec(-40,144);yygc++; 
					yyg[yygc] = new YYARec(-39,145);yygc++; 
					yyg[yygc] = new YYARec(-34,146);yygc++; 
					yyg[yygc] = new YYARec(-22,147);yygc++; 
					yyg[yygc] = new YYARec(-19,148);yygc++; 
					yyg[yygc] = new YYARec(-17,149);yygc++; 
					yyg[yygc] = new YYARec(-29,159);yygc++; 
					yyg[yygc] = new YYARec(-75,35);yygc++; 
					yyg[yygc] = new YYARec(-72,36);yygc++; 
					yyg[yygc] = new YYARec(-34,39);yygc++; 
					yyg[yygc] = new YYARec(-33,88);yygc++; 
					yyg[yygc] = new YYARec(-30,160);yygc++; 
					yyg[yygc] = new YYARec(-21,41);yygc++; 
					yyg[yygc] = new YYARec(-29,168);yygc++; 
					yyg[yygc] = new YYARec(-75,122);yygc++; 
					yyg[yygc] = new YYARec(-72,36);yygc++; 
					yyg[yygc] = new YYARec(-52,123);yygc++; 
					yyg[yygc] = new YYARec(-51,124);yygc++; 
					yyg[yygc] = new YYARec(-50,125);yygc++; 
					yyg[yygc] = new YYARec(-49,126);yygc++; 
					yyg[yygc] = new YYARec(-48,127);yygc++; 
					yyg[yygc] = new YYARec(-45,108);yygc++; 
					yyg[yygc] = new YYARec(-39,2);yygc++; 
					yyg[yygc] = new YYARec(-38,109);yygc++; 
					yyg[yygc] = new YYARec(-34,39);yygc++; 
					yyg[yygc] = new YYARec(-33,128);yygc++; 
					yyg[yygc] = new YYARec(-28,111);yygc++; 
					yyg[yygc] = new YYARec(-27,129);yygc++; 
					yyg[yygc] = new YYARec(-21,41);yygc++; 
					yyg[yygc] = new YYARec(-16,169);yygc++; 
					yyg[yygc] = new YYARec(-14,131);yygc++; 
					yyg[yygc] = new YYARec(-74,172);yygc++; 
					yyg[yygc] = new YYARec(-75,122);yygc++; 
					yyg[yygc] = new YYARec(-72,36);yygc++; 
					yyg[yygc] = new YYARec(-52,123);yygc++; 
					yyg[yygc] = new YYARec(-51,124);yygc++; 
					yyg[yygc] = new YYARec(-50,125);yygc++; 
					yyg[yygc] = new YYARec(-49,126);yygc++; 
					yyg[yygc] = new YYARec(-48,127);yygc++; 
					yyg[yygc] = new YYARec(-45,108);yygc++; 
					yyg[yygc] = new YYARec(-39,2);yygc++; 
					yyg[yygc] = new YYARec(-38,109);yygc++; 
					yyg[yygc] = new YYARec(-34,39);yygc++; 
					yyg[yygc] = new YYARec(-33,128);yygc++; 
					yyg[yygc] = new YYARec(-28,111);yygc++; 
					yyg[yygc] = new YYARec(-27,129);yygc++; 
					yyg[yygc] = new YYARec(-21,41);yygc++; 
					yyg[yygc] = new YYARec(-16,179);yygc++; 
					yyg[yygc] = new YYARec(-14,131);yygc++; 
					yyg[yygc] = new YYARec(-22,61);yygc++; 
					yyg[yygc] = new YYARec(-21,62);yygc++; 
					yyg[yygc] = new YYARec(-12,180);yygc++; 
					yyg[yygc] = new YYARec(-22,61);yygc++; 
					yyg[yygc] = new YYARec(-21,62);yygc++; 
					yyg[yygc] = new YYARec(-12,181);yygc++; 
					yyg[yygc] = new YYARec(-75,122);yygc++; 
					yyg[yygc] = new YYARec(-72,36);yygc++; 
					yyg[yygc] = new YYARec(-52,123);yygc++; 
					yyg[yygc] = new YYARec(-51,124);yygc++; 
					yyg[yygc] = new YYARec(-50,125);yygc++; 
					yyg[yygc] = new YYARec(-49,126);yygc++; 
					yyg[yygc] = new YYARec(-48,127);yygc++; 
					yyg[yygc] = new YYARec(-45,108);yygc++; 
					yyg[yygc] = new YYARec(-39,2);yygc++; 
					yyg[yygc] = new YYARec(-38,109);yygc++; 
					yyg[yygc] = new YYARec(-34,39);yygc++; 
					yyg[yygc] = new YYARec(-33,128);yygc++; 
					yyg[yygc] = new YYARec(-28,111);yygc++; 
					yyg[yygc] = new YYARec(-27,129);yygc++; 
					yyg[yygc] = new YYARec(-21,41);yygc++; 
					yyg[yygc] = new YYARec(-16,182);yygc++; 
					yyg[yygc] = new YYARec(-14,131);yygc++; 
					yyg[yygc] = new YYARec(-75,35);yygc++; 
					yyg[yygc] = new YYARec(-73,183);yygc++; 
					yyg[yygc] = new YYARec(-72,184);yygc++; 
					yyg[yygc] = new YYARec(-71,185);yygc++; 
					yyg[yygc] = new YYARec(-70,186);yygc++; 
					yyg[yygc] = new YYARec(-68,187);yygc++; 
					yyg[yygc] = new YYARec(-66,188);yygc++; 
					yyg[yygc] = new YYARec(-64,189);yygc++; 
					yyg[yygc] = new YYARec(-62,190);yygc++; 
					yyg[yygc] = new YYARec(-61,191);yygc++; 
					yyg[yygc] = new YYARec(-60,192);yygc++; 
					yyg[yygc] = new YYARec(-59,193);yygc++; 
					yyg[yygc] = new YYARec(-58,194);yygc++; 
					yyg[yygc] = new YYARec(-57,195);yygc++; 
					yyg[yygc] = new YYARec(-56,196);yygc++; 
					yyg[yygc] = new YYARec(-55,197);yygc++; 
					yyg[yygc] = new YYARec(-45,108);yygc++; 
					yyg[yygc] = new YYARec(-39,2);yygc++; 
					yyg[yygc] = new YYARec(-38,109);yygc++; 
					yyg[yygc] = new YYARec(-34,39);yygc++; 
					yyg[yygc] = new YYARec(-33,110);yygc++; 
					yyg[yygc] = new YYARec(-28,111);yygc++; 
					yyg[yygc] = new YYARec(-27,198);yygc++; 
					yyg[yygc] = new YYARec(-21,41);yygc++; 
					yyg[yygc] = new YYARec(-75,35);yygc++; 
					yyg[yygc] = new YYARec(-73,183);yygc++; 
					yyg[yygc] = new YYARec(-72,184);yygc++; 
					yyg[yygc] = new YYARec(-71,185);yygc++; 
					yyg[yygc] = new YYARec(-70,186);yygc++; 
					yyg[yygc] = new YYARec(-68,187);yygc++; 
					yyg[yygc] = new YYARec(-66,188);yygc++; 
					yyg[yygc] = new YYARec(-64,189);yygc++; 
					yyg[yygc] = new YYARec(-62,190);yygc++; 
					yyg[yygc] = new YYARec(-61,191);yygc++; 
					yyg[yygc] = new YYARec(-60,192);yygc++; 
					yyg[yygc] = new YYARec(-59,193);yygc++; 
					yyg[yygc] = new YYARec(-58,194);yygc++; 
					yyg[yygc] = new YYARec(-57,195);yygc++; 
					yyg[yygc] = new YYARec(-56,196);yygc++; 
					yyg[yygc] = new YYARec(-55,203);yygc++; 
					yyg[yygc] = new YYARec(-45,108);yygc++; 
					yyg[yygc] = new YYARec(-39,2);yygc++; 
					yyg[yygc] = new YYARec(-38,109);yygc++; 
					yyg[yygc] = new YYARec(-34,39);yygc++; 
					yyg[yygc] = new YYARec(-33,110);yygc++; 
					yyg[yygc] = new YYARec(-28,111);yygc++; 
					yyg[yygc] = new YYARec(-27,204);yygc++; 
					yyg[yygc] = new YYARec(-21,41);yygc++; 
					yyg[yygc] = new YYARec(-75,35);yygc++; 
					yyg[yygc] = new YYARec(-73,183);yygc++; 
					yyg[yygc] = new YYARec(-72,184);yygc++; 
					yyg[yygc] = new YYARec(-71,185);yygc++; 
					yyg[yygc] = new YYARec(-70,186);yygc++; 
					yyg[yygc] = new YYARec(-68,187);yygc++; 
					yyg[yygc] = new YYARec(-66,188);yygc++; 
					yyg[yygc] = new YYARec(-64,189);yygc++; 
					yyg[yygc] = new YYARec(-62,190);yygc++; 
					yyg[yygc] = new YYARec(-61,191);yygc++; 
					yyg[yygc] = new YYARec(-60,192);yygc++; 
					yyg[yygc] = new YYARec(-59,193);yygc++; 
					yyg[yygc] = new YYARec(-58,194);yygc++; 
					yyg[yygc] = new YYARec(-57,195);yygc++; 
					yyg[yygc] = new YYARec(-56,196);yygc++; 
					yyg[yygc] = new YYARec(-55,205);yygc++; 
					yyg[yygc] = new YYARec(-45,108);yygc++; 
					yyg[yygc] = new YYARec(-39,2);yygc++; 
					yyg[yygc] = new YYARec(-38,109);yygc++; 
					yyg[yygc] = new YYARec(-34,39);yygc++; 
					yyg[yygc] = new YYARec(-33,110);yygc++; 
					yyg[yygc] = new YYARec(-28,111);yygc++; 
					yyg[yygc] = new YYARec(-27,204);yygc++; 
					yyg[yygc] = new YYARec(-21,41);yygc++; 
					yyg[yygc] = new YYARec(-29,207);yygc++; 
					yyg[yygc] = new YYARec(-41,143);yygc++; 
					yyg[yygc] = new YYARec(-40,144);yygc++; 
					yyg[yygc] = new YYARec(-39,145);yygc++; 
					yyg[yygc] = new YYARec(-34,146);yygc++; 
					yyg[yygc] = new YYARec(-22,147);yygc++; 
					yyg[yygc] = new YYARec(-19,210);yygc++; 
					yyg[yygc] = new YYARec(-17,149);yygc++; 
					yyg[yygc] = new YYARec(-22,61);yygc++; 
					yyg[yygc] = new YYARec(-21,62);yygc++; 
					yyg[yygc] = new YYARec(-12,211);yygc++; 
					yyg[yygc] = new YYARec(-22,61);yygc++; 
					yyg[yygc] = new YYARec(-21,62);yygc++; 
					yyg[yygc] = new YYARec(-12,212);yygc++; 
					yyg[yygc] = new YYARec(-71,213);yygc++; 
					yyg[yygc] = new YYARec(-37,214);yygc++; 
					yyg[yygc] = new YYARec(-36,215);yygc++; 
					yyg[yygc] = new YYARec(-44,217);yygc++; 
					yyg[yygc] = new YYARec(-41,218);yygc++; 
					yyg[yygc] = new YYARec(-39,145);yygc++; 
					yyg[yygc] = new YYARec(-34,146);yygc++; 
					yyg[yygc] = new YYARec(-22,147);yygc++; 
					yyg[yygc] = new YYARec(-44,223);yygc++; 
					yyg[yygc] = new YYARec(-41,224);yygc++; 
					yyg[yygc] = new YYARec(-39,145);yygc++; 
					yyg[yygc] = new YYARec(-34,146);yygc++; 
					yyg[yygc] = new YYARec(-22,147);yygc++; 
					yyg[yygc] = new YYARec(-76,74);yygc++; 
					yyg[yygc] = new YYARec(-75,35);yygc++; 
					yyg[yygc] = new YYARec(-72,36);yygc++; 
					yyg[yygc] = new YYARec(-71,75);yygc++; 
					yyg[yygc] = new YYARec(-54,225);yygc++; 
					yyg[yygc] = new YYARec(-53,226);yygc++; 
					yyg[yygc] = new YYARec(-45,108);yygc++; 
					yyg[yygc] = new YYARec(-39,2);yygc++; 
					yyg[yygc] = new YYARec(-38,109);yygc++; 
					yyg[yygc] = new YYARec(-37,76);yygc++; 
					yyg[yygc] = new YYARec(-34,39);yygc++; 
					yyg[yygc] = new YYARec(-33,110);yygc++; 
					yyg[yygc] = new YYARec(-28,111);yygc++; 
					yyg[yygc] = new YYARec(-27,227);yygc++; 
					yyg[yygc] = new YYARec(-26,228);yygc++; 
					yyg[yygc] = new YYARec(-25,229);yygc++; 
					yyg[yygc] = new YYARec(-24,230);yygc++; 
					yyg[yygc] = new YYARec(-23,231);yygc++; 
					yyg[yygc] = new YYARec(-21,41);yygc++; 
					yyg[yygc] = new YYARec(-75,122);yygc++; 
					yyg[yygc] = new YYARec(-72,36);yygc++; 
					yyg[yygc] = new YYARec(-52,123);yygc++; 
					yyg[yygc] = new YYARec(-51,124);yygc++; 
					yyg[yygc] = new YYARec(-50,125);yygc++; 
					yyg[yygc] = new YYARec(-49,126);yygc++; 
					yyg[yygc] = new YYARec(-48,127);yygc++; 
					yyg[yygc] = new YYARec(-45,108);yygc++; 
					yyg[yygc] = new YYARec(-39,2);yygc++; 
					yyg[yygc] = new YYARec(-38,109);yygc++; 
					yyg[yygc] = new YYARec(-34,39);yygc++; 
					yyg[yygc] = new YYARec(-33,128);yygc++; 
					yyg[yygc] = new YYARec(-28,111);yygc++; 
					yyg[yygc] = new YYARec(-27,129);yygc++; 
					yyg[yygc] = new YYARec(-21,41);yygc++; 
					yyg[yygc] = new YYARec(-16,232);yygc++; 
					yyg[yygc] = new YYARec(-14,131);yygc++; 
					yyg[yygc] = new YYARec(-75,122);yygc++; 
					yyg[yygc] = new YYARec(-72,36);yygc++; 
					yyg[yygc] = new YYARec(-52,123);yygc++; 
					yyg[yygc] = new YYARec(-51,124);yygc++; 
					yyg[yygc] = new YYARec(-50,125);yygc++; 
					yyg[yygc] = new YYARec(-49,126);yygc++; 
					yyg[yygc] = new YYARec(-48,127);yygc++; 
					yyg[yygc] = new YYARec(-45,108);yygc++; 
					yyg[yygc] = new YYARec(-39,2);yygc++; 
					yyg[yygc] = new YYARec(-38,109);yygc++; 
					yyg[yygc] = new YYARec(-34,39);yygc++; 
					yyg[yygc] = new YYARec(-33,128);yygc++; 
					yyg[yygc] = new YYARec(-28,111);yygc++; 
					yyg[yygc] = new YYARec(-27,129);yygc++; 
					yyg[yygc] = new YYARec(-21,41);yygc++; 
					yyg[yygc] = new YYARec(-16,233);yygc++; 
					yyg[yygc] = new YYARec(-14,131);yygc++; 
					yyg[yygc] = new YYARec(-75,35);yygc++; 
					yyg[yygc] = new YYARec(-73,183);yygc++; 
					yyg[yygc] = new YYARec(-72,184);yygc++; 
					yyg[yygc] = new YYARec(-71,185);yygc++; 
					yyg[yygc] = new YYARec(-70,186);yygc++; 
					yyg[yygc] = new YYARec(-68,187);yygc++; 
					yyg[yygc] = new YYARec(-66,188);yygc++; 
					yyg[yygc] = new YYARec(-64,189);yygc++; 
					yyg[yygc] = new YYARec(-62,190);yygc++; 
					yyg[yygc] = new YYARec(-61,191);yygc++; 
					yyg[yygc] = new YYARec(-60,192);yygc++; 
					yyg[yygc] = new YYARec(-59,193);yygc++; 
					yyg[yygc] = new YYARec(-58,194);yygc++; 
					yyg[yygc] = new YYARec(-57,195);yygc++; 
					yyg[yygc] = new YYARec(-56,196);yygc++; 
					yyg[yygc] = new YYARec(-55,234);yygc++; 
					yyg[yygc] = new YYARec(-45,108);yygc++; 
					yyg[yygc] = new YYARec(-39,2);yygc++; 
					yyg[yygc] = new YYARec(-38,109);yygc++; 
					yyg[yygc] = new YYARec(-34,39);yygc++; 
					yyg[yygc] = new YYARec(-33,110);yygc++; 
					yyg[yygc] = new YYARec(-28,111);yygc++; 
					yyg[yygc] = new YYARec(-27,204);yygc++; 
					yyg[yygc] = new YYARec(-21,41);yygc++; 
					yyg[yygc] = new YYARec(-75,35);yygc++; 
					yyg[yygc] = new YYARec(-73,183);yygc++; 
					yyg[yygc] = new YYARec(-72,184);yygc++; 
					yyg[yygc] = new YYARec(-71,185);yygc++; 
					yyg[yygc] = new YYARec(-70,186);yygc++; 
					yyg[yygc] = new YYARec(-68,239);yygc++; 
					yyg[yygc] = new YYARec(-45,108);yygc++; 
					yyg[yygc] = new YYARec(-39,2);yygc++; 
					yyg[yygc] = new YYARec(-38,109);yygc++; 
					yyg[yygc] = new YYARec(-34,39);yygc++; 
					yyg[yygc] = new YYARec(-33,110);yygc++; 
					yyg[yygc] = new YYARec(-28,111);yygc++; 
					yyg[yygc] = new YYARec(-27,204);yygc++; 
					yyg[yygc] = new YYARec(-21,41);yygc++; 
					yyg[yygc] = new YYARec(-69,240);yygc++; 
					yyg[yygc] = new YYARec(-67,244);yygc++; 
					yyg[yygc] = new YYARec(-65,247);yygc++; 
					yyg[yygc] = new YYARec(-63,252);yygc++; 
					yyg[yygc] = new YYARec(-74,260);yygc++; 
					yyg[yygc] = new YYARec(-75,35);yygc++; 
					yyg[yygc] = new YYARec(-73,183);yygc++; 
					yyg[yygc] = new YYARec(-72,184);yygc++; 
					yyg[yygc] = new YYARec(-71,185);yygc++; 
					yyg[yygc] = new YYARec(-70,186);yygc++; 
					yyg[yygc] = new YYARec(-68,187);yygc++; 
					yyg[yygc] = new YYARec(-66,188);yygc++; 
					yyg[yygc] = new YYARec(-64,189);yygc++; 
					yyg[yygc] = new YYARec(-62,190);yygc++; 
					yyg[yygc] = new YYARec(-61,191);yygc++; 
					yyg[yygc] = new YYARec(-60,192);yygc++; 
					yyg[yygc] = new YYARec(-59,193);yygc++; 
					yyg[yygc] = new YYARec(-58,194);yygc++; 
					yyg[yygc] = new YYARec(-57,195);yygc++; 
					yyg[yygc] = new YYARec(-56,196);yygc++; 
					yyg[yygc] = new YYARec(-55,261);yygc++; 
					yyg[yygc] = new YYARec(-45,108);yygc++; 
					yyg[yygc] = new YYARec(-39,2);yygc++; 
					yyg[yygc] = new YYARec(-38,109);yygc++; 
					yyg[yygc] = new YYARec(-34,39);yygc++; 
					yyg[yygc] = new YYARec(-33,110);yygc++; 
					yyg[yygc] = new YYARec(-28,111);yygc++; 
					yyg[yygc] = new YYARec(-27,204);yygc++; 
					yyg[yygc] = new YYARec(-21,41);yygc++; 
					yyg[yygc] = new YYARec(-75,122);yygc++; 
					yyg[yygc] = new YYARec(-72,36);yygc++; 
					yyg[yygc] = new YYARec(-52,123);yygc++; 
					yyg[yygc] = new YYARec(-51,124);yygc++; 
					yyg[yygc] = new YYARec(-50,125);yygc++; 
					yyg[yygc] = new YYARec(-49,126);yygc++; 
					yyg[yygc] = new YYARec(-48,127);yygc++; 
					yyg[yygc] = new YYARec(-45,108);yygc++; 
					yyg[yygc] = new YYARec(-39,2);yygc++; 
					yyg[yygc] = new YYARec(-38,109);yygc++; 
					yyg[yygc] = new YYARec(-34,39);yygc++; 
					yyg[yygc] = new YYARec(-33,128);yygc++; 
					yyg[yygc] = new YYARec(-28,111);yygc++; 
					yyg[yygc] = new YYARec(-27,129);yygc++; 
					yyg[yygc] = new YYARec(-21,41);yygc++; 
					yyg[yygc] = new YYARec(-16,262);yygc++; 
					yyg[yygc] = new YYARec(-14,131);yygc++; 
					yyg[yygc] = new YYARec(-75,122);yygc++; 
					yyg[yygc] = new YYARec(-72,36);yygc++; 
					yyg[yygc] = new YYARec(-52,123);yygc++; 
					yyg[yygc] = new YYARec(-51,124);yygc++; 
					yyg[yygc] = new YYARec(-50,125);yygc++; 
					yyg[yygc] = new YYARec(-49,126);yygc++; 
					yyg[yygc] = new YYARec(-48,127);yygc++; 
					yyg[yygc] = new YYARec(-45,108);yygc++; 
					yyg[yygc] = new YYARec(-39,2);yygc++; 
					yyg[yygc] = new YYARec(-38,109);yygc++; 
					yyg[yygc] = new YYARec(-34,39);yygc++; 
					yyg[yygc] = new YYARec(-33,128);yygc++; 
					yyg[yygc] = new YYARec(-28,111);yygc++; 
					yyg[yygc] = new YYARec(-27,129);yygc++; 
					yyg[yygc] = new YYARec(-21,41);yygc++; 
					yyg[yygc] = new YYARec(-16,265);yygc++; 
					yyg[yygc] = new YYARec(-14,131);yygc++; 
					yyg[yygc] = new YYARec(-76,74);yygc++; 
					yyg[yygc] = new YYARec(-75,35);yygc++; 
					yyg[yygc] = new YYARec(-72,36);yygc++; 
					yyg[yygc] = new YYARec(-71,75);yygc++; 
					yyg[yygc] = new YYARec(-46,266);yygc++; 
					yyg[yygc] = new YYARec(-45,267);yygc++; 
					yyg[yygc] = new YYARec(-43,268);yygc++; 
					yyg[yygc] = new YYARec(-42,269);yygc++; 
					yyg[yygc] = new YYARec(-39,2);yygc++; 
					yyg[yygc] = new YYARec(-38,270);yygc++; 
					yyg[yygc] = new YYARec(-37,76);yygc++; 
					yyg[yygc] = new YYARec(-34,39);yygc++; 
					yyg[yygc] = new YYARec(-33,271);yygc++; 
					yyg[yygc] = new YYARec(-26,272);yygc++; 
					yyg[yygc] = new YYARec(-25,273);yygc++; 
					yyg[yygc] = new YYARec(-23,274);yygc++; 
					yyg[yygc] = new YYARec(-21,41);yygc++; 
					yyg[yygc] = new YYARec(-41,143);yygc++; 
					yyg[yygc] = new YYARec(-40,144);yygc++; 
					yyg[yygc] = new YYARec(-39,145);yygc++; 
					yyg[yygc] = new YYARec(-34,146);yygc++; 
					yyg[yygc] = new YYARec(-22,147);yygc++; 
					yyg[yygc] = new YYARec(-19,275);yygc++; 
					yyg[yygc] = new YYARec(-17,149);yygc++; 
					yyg[yygc] = new YYARec(-46,1);yygc++; 
					yyg[yygc] = new YYARec(-39,2);yygc++; 
					yyg[yygc] = new YYARec(-38,3);yygc++; 
					yyg[yygc] = new YYARec(-34,4);yygc++; 
					yyg[yygc] = new YYARec(-31,5);yygc++; 
					yyg[yygc] = new YYARec(-28,6);yygc++; 
					yyg[yygc] = new YYARec(-21,7);yygc++; 
					yyg[yygc] = new YYARec(-11,8);yygc++; 
					yyg[yygc] = new YYARec(-10,9);yygc++; 
					yyg[yygc] = new YYARec(-9,10);yygc++; 
					yyg[yygc] = new YYARec(-8,11);yygc++; 
					yyg[yygc] = new YYARec(-7,12);yygc++; 
					yyg[yygc] = new YYARec(-6,13);yygc++; 
					yyg[yygc] = new YYARec(-5,14);yygc++; 
					yyg[yygc] = new YYARec(-4,15);yygc++; 
					yyg[yygc] = new YYARec(-3,280);yygc++; 
					yyg[yygc] = new YYARec(-29,281);yygc++; 
					yyg[yygc] = new YYARec(-75,122);yygc++; 
					yyg[yygc] = new YYARec(-72,36);yygc++; 
					yyg[yygc] = new YYARec(-52,123);yygc++; 
					yyg[yygc] = new YYARec(-51,124);yygc++; 
					yyg[yygc] = new YYARec(-50,125);yygc++; 
					yyg[yygc] = new YYARec(-49,126);yygc++; 
					yyg[yygc] = new YYARec(-48,127);yygc++; 
					yyg[yygc] = new YYARec(-45,108);yygc++; 
					yyg[yygc] = new YYARec(-39,2);yygc++; 
					yyg[yygc] = new YYARec(-38,109);yygc++; 
					yyg[yygc] = new YYARec(-34,39);yygc++; 
					yyg[yygc] = new YYARec(-33,128);yygc++; 
					yyg[yygc] = new YYARec(-28,111);yygc++; 
					yyg[yygc] = new YYARec(-27,129);yygc++; 
					yyg[yygc] = new YYARec(-21,41);yygc++; 
					yyg[yygc] = new YYARec(-16,282);yygc++; 
					yyg[yygc] = new YYARec(-15,283);yygc++; 
					yyg[yygc] = new YYARec(-14,131);yygc++; 
					yyg[yygc] = new YYARec(-75,122);yygc++; 
					yyg[yygc] = new YYARec(-72,36);yygc++; 
					yyg[yygc] = new YYARec(-52,123);yygc++; 
					yyg[yygc] = new YYARec(-51,124);yygc++; 
					yyg[yygc] = new YYARec(-50,125);yygc++; 
					yyg[yygc] = new YYARec(-49,126);yygc++; 
					yyg[yygc] = new YYARec(-48,127);yygc++; 
					yyg[yygc] = new YYARec(-45,108);yygc++; 
					yyg[yygc] = new YYARec(-39,2);yygc++; 
					yyg[yygc] = new YYARec(-38,109);yygc++; 
					yyg[yygc] = new YYARec(-34,39);yygc++; 
					yyg[yygc] = new YYARec(-33,128);yygc++; 
					yyg[yygc] = new YYARec(-28,111);yygc++; 
					yyg[yygc] = new YYARec(-27,129);yygc++; 
					yyg[yygc] = new YYARec(-21,41);yygc++; 
					yyg[yygc] = new YYARec(-16,282);yygc++; 
					yyg[yygc] = new YYARec(-15,284);yygc++; 
					yyg[yygc] = new YYARec(-14,131);yygc++; 
					yyg[yygc] = new YYARec(-75,122);yygc++; 
					yyg[yygc] = new YYARec(-72,36);yygc++; 
					yyg[yygc] = new YYARec(-52,123);yygc++; 
					yyg[yygc] = new YYARec(-51,124);yygc++; 
					yyg[yygc] = new YYARec(-50,125);yygc++; 
					yyg[yygc] = new YYARec(-49,126);yygc++; 
					yyg[yygc] = new YYARec(-48,127);yygc++; 
					yyg[yygc] = new YYARec(-45,108);yygc++; 
					yyg[yygc] = new YYARec(-39,2);yygc++; 
					yyg[yygc] = new YYARec(-38,109);yygc++; 
					yyg[yygc] = new YYARec(-34,39);yygc++; 
					yyg[yygc] = new YYARec(-33,128);yygc++; 
					yyg[yygc] = new YYARec(-28,111);yygc++; 
					yyg[yygc] = new YYARec(-27,129);yygc++; 
					yyg[yygc] = new YYARec(-21,41);yygc++; 
					yyg[yygc] = new YYARec(-16,285);yygc++; 
					yyg[yygc] = new YYARec(-14,131);yygc++; 
					yyg[yygc] = new YYARec(-75,35);yygc++; 
					yyg[yygc] = new YYARec(-73,183);yygc++; 
					yyg[yygc] = new YYARec(-72,184);yygc++; 
					yyg[yygc] = new YYARec(-71,185);yygc++; 
					yyg[yygc] = new YYARec(-70,186);yygc++; 
					yyg[yygc] = new YYARec(-68,187);yygc++; 
					yyg[yygc] = new YYARec(-66,188);yygc++; 
					yyg[yygc] = new YYARec(-64,189);yygc++; 
					yyg[yygc] = new YYARec(-62,190);yygc++; 
					yyg[yygc] = new YYARec(-61,191);yygc++; 
					yyg[yygc] = new YYARec(-60,192);yygc++; 
					yyg[yygc] = new YYARec(-59,193);yygc++; 
					yyg[yygc] = new YYARec(-58,194);yygc++; 
					yyg[yygc] = new YYARec(-57,195);yygc++; 
					yyg[yygc] = new YYARec(-56,196);yygc++; 
					yyg[yygc] = new YYARec(-55,286);yygc++; 
					yyg[yygc] = new YYARec(-45,108);yygc++; 
					yyg[yygc] = new YYARec(-39,2);yygc++; 
					yyg[yygc] = new YYARec(-38,109);yygc++; 
					yyg[yygc] = new YYARec(-34,39);yygc++; 
					yyg[yygc] = new YYARec(-33,110);yygc++; 
					yyg[yygc] = new YYARec(-28,111);yygc++; 
					yyg[yygc] = new YYARec(-27,204);yygc++; 
					yyg[yygc] = new YYARec(-21,41);yygc++; 
					yyg[yygc] = new YYARec(-75,35);yygc++; 
					yyg[yygc] = new YYARec(-73,183);yygc++; 
					yyg[yygc] = new YYARec(-72,184);yygc++; 
					yyg[yygc] = new YYARec(-71,185);yygc++; 
					yyg[yygc] = new YYARec(-70,186);yygc++; 
					yyg[yygc] = new YYARec(-68,287);yygc++; 
					yyg[yygc] = new YYARec(-45,108);yygc++; 
					yyg[yygc] = new YYARec(-39,2);yygc++; 
					yyg[yygc] = new YYARec(-38,109);yygc++; 
					yyg[yygc] = new YYARec(-34,39);yygc++; 
					yyg[yygc] = new YYARec(-33,110);yygc++; 
					yyg[yygc] = new YYARec(-28,111);yygc++; 
					yyg[yygc] = new YYARec(-27,204);yygc++; 
					yyg[yygc] = new YYARec(-21,41);yygc++; 
					yyg[yygc] = new YYARec(-75,35);yygc++; 
					yyg[yygc] = new YYARec(-73,183);yygc++; 
					yyg[yygc] = new YYARec(-72,184);yygc++; 
					yyg[yygc] = new YYARec(-71,185);yygc++; 
					yyg[yygc] = new YYARec(-70,186);yygc++; 
					yyg[yygc] = new YYARec(-68,187);yygc++; 
					yyg[yygc] = new YYARec(-66,288);yygc++; 
					yyg[yygc] = new YYARec(-45,108);yygc++; 
					yyg[yygc] = new YYARec(-39,2);yygc++; 
					yyg[yygc] = new YYARec(-38,109);yygc++; 
					yyg[yygc] = new YYARec(-34,39);yygc++; 
					yyg[yygc] = new YYARec(-33,110);yygc++; 
					yyg[yygc] = new YYARec(-28,111);yygc++; 
					yyg[yygc] = new YYARec(-27,204);yygc++; 
					yyg[yygc] = new YYARec(-21,41);yygc++; 
					yyg[yygc] = new YYARec(-75,35);yygc++; 
					yyg[yygc] = new YYARec(-73,183);yygc++; 
					yyg[yygc] = new YYARec(-72,184);yygc++; 
					yyg[yygc] = new YYARec(-71,185);yygc++; 
					yyg[yygc] = new YYARec(-70,186);yygc++; 
					yyg[yygc] = new YYARec(-68,187);yygc++; 
					yyg[yygc] = new YYARec(-66,188);yygc++; 
					yyg[yygc] = new YYARec(-64,289);yygc++; 
					yyg[yygc] = new YYARec(-45,108);yygc++; 
					yyg[yygc] = new YYARec(-39,2);yygc++; 
					yyg[yygc] = new YYARec(-38,109);yygc++; 
					yyg[yygc] = new YYARec(-34,39);yygc++; 
					yyg[yygc] = new YYARec(-33,110);yygc++; 
					yyg[yygc] = new YYARec(-28,111);yygc++; 
					yyg[yygc] = new YYARec(-27,204);yygc++; 
					yyg[yygc] = new YYARec(-21,41);yygc++; 
					yyg[yygc] = new YYARec(-75,35);yygc++; 
					yyg[yygc] = new YYARec(-73,183);yygc++; 
					yyg[yygc] = new YYARec(-72,184);yygc++; 
					yyg[yygc] = new YYARec(-71,185);yygc++; 
					yyg[yygc] = new YYARec(-70,186);yygc++; 
					yyg[yygc] = new YYARec(-68,187);yygc++; 
					yyg[yygc] = new YYARec(-66,188);yygc++; 
					yyg[yygc] = new YYARec(-64,189);yygc++; 
					yyg[yygc] = new YYARec(-62,290);yygc++; 
					yyg[yygc] = new YYARec(-45,108);yygc++; 
					yyg[yygc] = new YYARec(-39,2);yygc++; 
					yyg[yygc] = new YYARec(-38,109);yygc++; 
					yyg[yygc] = new YYARec(-34,39);yygc++; 
					yyg[yygc] = new YYARec(-33,110);yygc++; 
					yyg[yygc] = new YYARec(-28,111);yygc++; 
					yyg[yygc] = new YYARec(-27,204);yygc++; 
					yyg[yygc] = new YYARec(-21,41);yygc++; 
					yyg[yygc] = new YYARec(-75,35);yygc++; 
					yyg[yygc] = new YYARec(-73,183);yygc++; 
					yyg[yygc] = new YYARec(-72,184);yygc++; 
					yyg[yygc] = new YYARec(-71,185);yygc++; 
					yyg[yygc] = new YYARec(-70,186);yygc++; 
					yyg[yygc] = new YYARec(-68,187);yygc++; 
					yyg[yygc] = new YYARec(-66,188);yygc++; 
					yyg[yygc] = new YYARec(-64,189);yygc++; 
					yyg[yygc] = new YYARec(-62,190);yygc++; 
					yyg[yygc] = new YYARec(-61,291);yygc++; 
					yyg[yygc] = new YYARec(-45,108);yygc++; 
					yyg[yygc] = new YYARec(-39,2);yygc++; 
					yyg[yygc] = new YYARec(-38,109);yygc++; 
					yyg[yygc] = new YYARec(-34,39);yygc++; 
					yyg[yygc] = new YYARec(-33,110);yygc++; 
					yyg[yygc] = new YYARec(-28,111);yygc++; 
					yyg[yygc] = new YYARec(-27,204);yygc++; 
					yyg[yygc] = new YYARec(-21,41);yygc++; 
					yyg[yygc] = new YYARec(-75,35);yygc++; 
					yyg[yygc] = new YYARec(-73,183);yygc++; 
					yyg[yygc] = new YYARec(-72,184);yygc++; 
					yyg[yygc] = new YYARec(-71,185);yygc++; 
					yyg[yygc] = new YYARec(-70,186);yygc++; 
					yyg[yygc] = new YYARec(-68,187);yygc++; 
					yyg[yygc] = new YYARec(-66,188);yygc++; 
					yyg[yygc] = new YYARec(-64,189);yygc++; 
					yyg[yygc] = new YYARec(-62,190);yygc++; 
					yyg[yygc] = new YYARec(-61,191);yygc++; 
					yyg[yygc] = new YYARec(-60,292);yygc++; 
					yyg[yygc] = new YYARec(-45,108);yygc++; 
					yyg[yygc] = new YYARec(-39,2);yygc++; 
					yyg[yygc] = new YYARec(-38,109);yygc++; 
					yyg[yygc] = new YYARec(-34,39);yygc++; 
					yyg[yygc] = new YYARec(-33,110);yygc++; 
					yyg[yygc] = new YYARec(-28,111);yygc++; 
					yyg[yygc] = new YYARec(-27,204);yygc++; 
					yyg[yygc] = new YYARec(-21,41);yygc++; 
					yyg[yygc] = new YYARec(-75,35);yygc++; 
					yyg[yygc] = new YYARec(-73,183);yygc++; 
					yyg[yygc] = new YYARec(-72,184);yygc++; 
					yyg[yygc] = new YYARec(-71,185);yygc++; 
					yyg[yygc] = new YYARec(-70,186);yygc++; 
					yyg[yygc] = new YYARec(-68,187);yygc++; 
					yyg[yygc] = new YYARec(-66,188);yygc++; 
					yyg[yygc] = new YYARec(-64,189);yygc++; 
					yyg[yygc] = new YYARec(-62,190);yygc++; 
					yyg[yygc] = new YYARec(-61,191);yygc++; 
					yyg[yygc] = new YYARec(-60,192);yygc++; 
					yyg[yygc] = new YYARec(-59,293);yygc++; 
					yyg[yygc] = new YYARec(-45,108);yygc++; 
					yyg[yygc] = new YYARec(-39,2);yygc++; 
					yyg[yygc] = new YYARec(-38,109);yygc++; 
					yyg[yygc] = new YYARec(-34,39);yygc++; 
					yyg[yygc] = new YYARec(-33,110);yygc++; 
					yyg[yygc] = new YYARec(-28,111);yygc++; 
					yyg[yygc] = new YYARec(-27,204);yygc++; 
					yyg[yygc] = new YYARec(-21,41);yygc++; 
					yyg[yygc] = new YYARec(-75,35);yygc++; 
					yyg[yygc] = new YYARec(-73,183);yygc++; 
					yyg[yygc] = new YYARec(-72,184);yygc++; 
					yyg[yygc] = new YYARec(-71,185);yygc++; 
					yyg[yygc] = new YYARec(-70,186);yygc++; 
					yyg[yygc] = new YYARec(-68,187);yygc++; 
					yyg[yygc] = new YYARec(-66,188);yygc++; 
					yyg[yygc] = new YYARec(-64,189);yygc++; 
					yyg[yygc] = new YYARec(-62,190);yygc++; 
					yyg[yygc] = new YYARec(-61,191);yygc++; 
					yyg[yygc] = new YYARec(-60,192);yygc++; 
					yyg[yygc] = new YYARec(-59,193);yygc++; 
					yyg[yygc] = new YYARec(-58,294);yygc++; 
					yyg[yygc] = new YYARec(-45,108);yygc++; 
					yyg[yygc] = new YYARec(-39,2);yygc++; 
					yyg[yygc] = new YYARec(-38,109);yygc++; 
					yyg[yygc] = new YYARec(-34,39);yygc++; 
					yyg[yygc] = new YYARec(-33,110);yygc++; 
					yyg[yygc] = new YYARec(-28,111);yygc++; 
					yyg[yygc] = new YYARec(-27,204);yygc++; 
					yyg[yygc] = new YYARec(-21,41);yygc++; 
					yyg[yygc] = new YYARec(-75,35);yygc++; 
					yyg[yygc] = new YYARec(-73,183);yygc++; 
					yyg[yygc] = new YYARec(-72,184);yygc++; 
					yyg[yygc] = new YYARec(-71,185);yygc++; 
					yyg[yygc] = new YYARec(-70,186);yygc++; 
					yyg[yygc] = new YYARec(-68,187);yygc++; 
					yyg[yygc] = new YYARec(-66,188);yygc++; 
					yyg[yygc] = new YYARec(-64,189);yygc++; 
					yyg[yygc] = new YYARec(-62,190);yygc++; 
					yyg[yygc] = new YYARec(-61,191);yygc++; 
					yyg[yygc] = new YYARec(-60,192);yygc++; 
					yyg[yygc] = new YYARec(-59,193);yygc++; 
					yyg[yygc] = new YYARec(-58,194);yygc++; 
					yyg[yygc] = new YYARec(-57,295);yygc++; 
					yyg[yygc] = new YYARec(-45,108);yygc++; 
					yyg[yygc] = new YYARec(-39,2);yygc++; 
					yyg[yygc] = new YYARec(-38,109);yygc++; 
					yyg[yygc] = new YYARec(-34,39);yygc++; 
					yyg[yygc] = new YYARec(-33,110);yygc++; 
					yyg[yygc] = new YYARec(-28,111);yygc++; 
					yyg[yygc] = new YYARec(-27,204);yygc++; 
					yyg[yygc] = new YYARec(-21,41);yygc++; 
					yyg[yygc] = new YYARec(-75,35);yygc++; 
					yyg[yygc] = new YYARec(-73,183);yygc++; 
					yyg[yygc] = new YYARec(-72,184);yygc++; 
					yyg[yygc] = new YYARec(-71,185);yygc++; 
					yyg[yygc] = new YYARec(-70,186);yygc++; 
					yyg[yygc] = new YYARec(-68,187);yygc++; 
					yyg[yygc] = new YYARec(-66,188);yygc++; 
					yyg[yygc] = new YYARec(-64,189);yygc++; 
					yyg[yygc] = new YYARec(-62,190);yygc++; 
					yyg[yygc] = new YYARec(-61,191);yygc++; 
					yyg[yygc] = new YYARec(-60,192);yygc++; 
					yyg[yygc] = new YYARec(-59,193);yygc++; 
					yyg[yygc] = new YYARec(-58,194);yygc++; 
					yyg[yygc] = new YYARec(-57,195);yygc++; 
					yyg[yygc] = new YYARec(-56,196);yygc++; 
					yyg[yygc] = new YYARec(-55,296);yygc++; 
					yyg[yygc] = new YYARec(-45,108);yygc++; 
					yyg[yygc] = new YYARec(-39,2);yygc++; 
					yyg[yygc] = new YYARec(-38,109);yygc++; 
					yyg[yygc] = new YYARec(-34,39);yygc++; 
					yyg[yygc] = new YYARec(-33,110);yygc++; 
					yyg[yygc] = new YYARec(-28,111);yygc++; 
					yyg[yygc] = new YYARec(-27,204);yygc++; 
					yyg[yygc] = new YYARec(-21,41);yygc++; 
					yyg[yygc] = new YYARec(-75,122);yygc++; 
					yyg[yygc] = new YYARec(-72,36);yygc++; 
					yyg[yygc] = new YYARec(-52,123);yygc++; 
					yyg[yygc] = new YYARec(-51,124);yygc++; 
					yyg[yygc] = new YYARec(-50,125);yygc++; 
					yyg[yygc] = new YYARec(-49,126);yygc++; 
					yyg[yygc] = new YYARec(-48,127);yygc++; 
					yyg[yygc] = new YYARec(-45,108);yygc++; 
					yyg[yygc] = new YYARec(-39,2);yygc++; 
					yyg[yygc] = new YYARec(-38,109);yygc++; 
					yyg[yygc] = new YYARec(-34,39);yygc++; 
					yyg[yygc] = new YYARec(-33,128);yygc++; 
					yyg[yygc] = new YYARec(-28,111);yygc++; 
					yyg[yygc] = new YYARec(-27,129);yygc++; 
					yyg[yygc] = new YYARec(-21,41);yygc++; 
					yyg[yygc] = new YYARec(-16,299);yygc++; 
					yyg[yygc] = new YYARec(-14,131);yygc++; 
					yyg[yygc] = new YYARec(-75,122);yygc++; 
					yyg[yygc] = new YYARec(-72,36);yygc++; 
					yyg[yygc] = new YYARec(-52,123);yygc++; 
					yyg[yygc] = new YYARec(-51,124);yygc++; 
					yyg[yygc] = new YYARec(-50,125);yygc++; 
					yyg[yygc] = new YYARec(-49,126);yygc++; 
					yyg[yygc] = new YYARec(-48,127);yygc++; 
					yyg[yygc] = new YYARec(-45,108);yygc++; 
					yyg[yygc] = new YYARec(-39,2);yygc++; 
					yyg[yygc] = new YYARec(-38,109);yygc++; 
					yyg[yygc] = new YYARec(-34,39);yygc++; 
					yyg[yygc] = new YYARec(-33,128);yygc++; 
					yyg[yygc] = new YYARec(-28,111);yygc++; 
					yyg[yygc] = new YYARec(-27,129);yygc++; 
					yyg[yygc] = new YYARec(-21,41);yygc++; 
					yyg[yygc] = new YYARec(-16,300);yygc++; 
					yyg[yygc] = new YYARec(-14,131);yygc++; 
					yyg[yygc] = new YYARec(-29,302);yygc++; 
					yyg[yygc] = new YYARec(-41,143);yygc++; 
					yyg[yygc] = new YYARec(-40,144);yygc++; 
					yyg[yygc] = new YYARec(-39,145);yygc++; 
					yyg[yygc] = new YYARec(-34,146);yygc++; 
					yyg[yygc] = new YYARec(-22,147);yygc++; 
					yyg[yygc] = new YYARec(-19,304);yygc++; 
					yyg[yygc] = new YYARec(-18,305);yygc++; 
					yyg[yygc] = new YYARec(-17,149);yygc++; 
					yyg[yygc] = new YYARec(-41,143);yygc++; 
					yyg[yygc] = new YYARec(-40,144);yygc++; 
					yyg[yygc] = new YYARec(-39,145);yygc++; 
					yyg[yygc] = new YYARec(-34,146);yygc++; 
					yyg[yygc] = new YYARec(-22,147);yygc++; 
					yyg[yygc] = new YYARec(-19,304);yygc++; 
					yyg[yygc] = new YYARec(-18,306);yygc++; 
					yyg[yygc] = new YYARec(-17,149);yygc++; 
					yyg[yygc] = new YYARec(-71,213);yygc++; 
					yyg[yygc] = new YYARec(-37,214);yygc++; 
					yyg[yygc] = new YYARec(-36,307);yygc++; 
					yyg[yygc] = new YYARec(-76,74);yygc++; 
					yyg[yygc] = new YYARec(-75,35);yygc++; 
					yyg[yygc] = new YYARec(-72,36);yygc++; 
					yyg[yygc] = new YYARec(-71,75);yygc++; 
					yyg[yygc] = new YYARec(-54,225);yygc++; 
					yyg[yygc] = new YYARec(-53,309);yygc++; 
					yyg[yygc] = new YYARec(-45,108);yygc++; 
					yyg[yygc] = new YYARec(-39,2);yygc++; 
					yyg[yygc] = new YYARec(-38,109);yygc++; 
					yyg[yygc] = new YYARec(-37,76);yygc++; 
					yyg[yygc] = new YYARec(-34,39);yygc++; 
					yyg[yygc] = new YYARec(-33,110);yygc++; 
					yyg[yygc] = new YYARec(-28,111);yygc++; 
					yyg[yygc] = new YYARec(-27,227);yygc++; 
					yyg[yygc] = new YYARec(-26,228);yygc++; 
					yyg[yygc] = new YYARec(-25,229);yygc++; 
					yyg[yygc] = new YYARec(-24,230);yygc++; 
					yyg[yygc] = new YYARec(-23,231);yygc++; 
					yyg[yygc] = new YYARec(-21,41);yygc++; 
					yyg[yygc] = new YYARec(-69,240);yygc++; 
					yyg[yygc] = new YYARec(-67,244);yygc++; 
					yyg[yygc] = new YYARec(-65,247);yygc++; 
					yyg[yygc] = new YYARec(-63,252);yygc++; 
					yyg[yygc] = new YYARec(-44,317);yygc++; 
					yyg[yygc] = new YYARec(-41,318);yygc++; 
					yyg[yygc] = new YYARec(-39,145);yygc++; 
					yyg[yygc] = new YYARec(-34,146);yygc++; 
					yyg[yygc] = new YYARec(-22,147);yygc++; 
					yyg[yygc] = new YYARec(-76,74);yygc++; 
					yyg[yygc] = new YYARec(-75,35);yygc++; 
					yyg[yygc] = new YYARec(-72,36);yygc++; 
					yyg[yygc] = new YYARec(-71,75);yygc++; 
					yyg[yygc] = new YYARec(-46,266);yygc++; 
					yyg[yygc] = new YYARec(-45,267);yygc++; 
					yyg[yygc] = new YYARec(-43,268);yygc++; 
					yyg[yygc] = new YYARec(-42,319);yygc++; 
					yyg[yygc] = new YYARec(-39,2);yygc++; 
					yyg[yygc] = new YYARec(-38,270);yygc++; 
					yyg[yygc] = new YYARec(-37,76);yygc++; 
					yyg[yygc] = new YYARec(-34,39);yygc++; 
					yyg[yygc] = new YYARec(-33,271);yygc++; 
					yyg[yygc] = new YYARec(-26,272);yygc++; 
					yyg[yygc] = new YYARec(-25,273);yygc++; 
					yyg[yygc] = new YYARec(-23,274);yygc++; 
					yyg[yygc] = new YYARec(-21,41);yygc++; 
					yyg[yygc] = new YYARec(-44,320);yygc++; 
					yyg[yygc] = new YYARec(-41,321);yygc++; 
					yyg[yygc] = new YYARec(-39,145);yygc++; 
					yyg[yygc] = new YYARec(-34,146);yygc++; 
					yyg[yygc] = new YYARec(-22,147);yygc++; 
					yyg[yygc] = new YYARec(-75,122);yygc++; 
					yyg[yygc] = new YYARec(-72,36);yygc++; 
					yyg[yygc] = new YYARec(-52,123);yygc++; 
					yyg[yygc] = new YYARec(-51,124);yygc++; 
					yyg[yygc] = new YYARec(-50,125);yygc++; 
					yyg[yygc] = new YYARec(-49,126);yygc++; 
					yyg[yygc] = new YYARec(-48,127);yygc++; 
					yyg[yygc] = new YYARec(-45,108);yygc++; 
					yyg[yygc] = new YYARec(-39,2);yygc++; 
					yyg[yygc] = new YYARec(-38,109);yygc++; 
					yyg[yygc] = new YYARec(-34,39);yygc++; 
					yyg[yygc] = new YYARec(-33,128);yygc++; 
					yyg[yygc] = new YYARec(-28,111);yygc++; 
					yyg[yygc] = new YYARec(-27,129);yygc++; 
					yyg[yygc] = new YYARec(-21,41);yygc++; 
					yyg[yygc] = new YYARec(-16,328);yygc++; 
					yyg[yygc] = new YYARec(-14,131);yygc++; 
					yyg[yygc] = new YYARec(-41,143);yygc++; 
					yyg[yygc] = new YYARec(-40,144);yygc++; 
					yyg[yygc] = new YYARec(-39,145);yygc++; 
					yyg[yygc] = new YYARec(-34,146);yygc++; 
					yyg[yygc] = new YYARec(-22,147);yygc++; 
					yyg[yygc] = new YYARec(-19,329);yygc++; 
					yyg[yygc] = new YYARec(-17,149);yygc++;

					yyd = new int[yynstates];
					yyd[0] = 0;  
					yyd[1] = 0;  
					yyd[2] = -59;  
					yyd[3] = 0;  
					yyd[4] = 0;  
					yyd[5] = 0;  
					yyd[6] = 0;  
					yyd[7] = -40;  
					yyd[8] = -11;  
					yyd[9] = -10;  
					yyd[10] = -9;  
					yyd[11] = -8;  
					yyd[12] = -7;  
					yyd[13] = -6;  
					yyd[14] = -5;  
					yyd[15] = 0;  
					yyd[16] = -1;  
					yyd[17] = 0;  
					yyd[18] = 0;  
					yyd[19] = 0;  
					yyd[20] = 0;  
					yyd[21] = 0;  
					yyd[22] = 0;  
					yyd[23] = -4;  
					yyd[24] = -42;  
					yyd[25] = -39;  
					yyd[26] = -53;  
					yyd[27] = -57;  
					yyd[28] = -78;  
					yyd[29] = -192;  
					yyd[30] = -37;  
					yyd[31] = -41;  
					yyd[32] = -58;  
					yyd[33] = -191;  
					yyd[34] = -38;  
					yyd[35] = -200;  
					yyd[36] = -202;  
					yyd[37] = 0;  
					yyd[38] = -205;  
					yyd[39] = -201;  
					yyd[40] = -206;  
					yyd[41] = -203;  
					yyd[42] = -204;  
					yyd[43] = -197;  
					yyd[44] = -147;  
					yyd[45] = -196;  
					yyd[46] = -199;  
					yyd[47] = -178;  
					yyd[48] = -145;  
					yyd[49] = -146;  
					yyd[50] = -176;  
					yyd[51] = -177;  
					yyd[52] = -198;  
					yyd[53] = 0;  
					yyd[54] = 0;  
					yyd[55] = -190;  
					yyd[56] = -189;  
					yyd[57] = 0;  
					yyd[58] = -96;  
					yyd[59] = 0;  
					yyd[60] = -2;  
					yyd[61] = -28;  
					yyd[62] = -27;  
					yyd[63] = 0;  
					yyd[64] = -187;  
					yyd[65] = 0;  
					yyd[66] = 0;  
					yyd[67] = 0;  
					yyd[68] = 0;  
					yyd[69] = -209;  
					yyd[70] = 0;  
					yyd[71] = 0;  
					yyd[72] = -56;  
					yyd[73] = 0;  
					yyd[74] = -179;  
					yyd[75] = 0;  
					yyd[76] = -180;  
					yyd[77] = 0;  
					yyd[78] = -47;  
					yyd[79] = -48;  
					yyd[80] = -45;  
					yyd[81] = -46;  
					yyd[82] = -135;  
					yyd[83] = -136;  
					yyd[84] = -134;  
					yyd[85] = -184;  
					yyd[86] = -186;  
					yyd[87] = -210;  
					yyd[88] = 0;  
					yyd[89] = 0;  
					yyd[90] = 0;  
					yyd[91] = 0;  
					yyd[92] = -25;  
					yyd[93] = 0;  
					yyd[94] = -26;  
					yyd[95] = -34;  
					yyd[96] = 0;  
					yyd[97] = 0;  
					yyd[98] = 0;  
					yyd[99] = 0;  
					yyd[100] = -183;  
					yyd[101] = -185;  
					yyd[102] = -36;  
					yyd[103] = 0;  
					yyd[104] = -35;  
					yyd[105] = 0;  
					yyd[106] = 0;  
					yyd[107] = 0;  
					yyd[108] = 0;  
					yyd[109] = -156;  
					yyd[110] = 0;  
					yyd[111] = -155;  
					yyd[112] = -33;  
					yyd[113] = -32;  
					yyd[114] = -31;  
					yyd[115] = -30;  
					yyd[116] = -29;  
					yyd[117] = 0;  
					yyd[118] = -188;  
					yyd[119] = -195;  
					yyd[120] = -194;  
					yyd[121] = -193;  
					yyd[122] = 0;  
					yyd[123] = 0;  
					yyd[124] = -86;  
					yyd[125] = 0;  
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
					yyd[136] = 0;  
					yyd[137] = 0;  
					yyd[138] = 0;  
					yyd[139] = 0;  
					yyd[140] = 0;  
					yyd[141] = 0;  
					yyd[142] = -55;  
					yyd[143] = 0;  
					yyd[144] = 0;  
					yyd[145] = -171;  
					yyd[146] = -172;  
					yyd[147] = -170;  
					yyd[148] = 0;  
					yyd[149] = 0;  
					yyd[150] = 0;  
					yyd[151] = 0;  
					yyd[152] = -169;  
					yyd[153] = -165;  
					yyd[154] = -164;  
					yyd[155] = -167;  
					yyd[156] = -168;  
					yyd[157] = -166;  
					yyd[158] = -163;  
					yyd[159] = 0;  
					yyd[160] = -44;  
					yyd[161] = -12;  
					yyd[162] = 0;  
					yyd[163] = -15;  
					yyd[164] = -13;  
					yyd[165] = 0;  
					yyd[166] = 0;  
					yyd[167] = -24;  
					yyd[168] = 0;  
					yyd[169] = -84;  
					yyd[170] = 0;  
					yyd[171] = 0;  
					yyd[172] = 0;  
					yyd[173] = -140;  
					yyd[174] = -141;  
					yyd[175] = -142;  
					yyd[176] = -143;  
					yyd[177] = -144;  
					yyd[178] = -77;  
					yyd[179] = -83;  
					yyd[180] = 0;  
					yyd[181] = 0;  
					yyd[182] = 0;  
					yyd[183] = -121;  
					yyd[184] = 0;  
					yyd[185] = 0;  
					yyd[186] = -117;  
					yyd[187] = -115;  
					yyd[188] = 0;  
					yyd[189] = 0;  
					yyd[190] = 0;  
					yyd[191] = 0;  
					yyd[192] = 0;  
					yyd[193] = 0;  
					yyd[194] = 0;  
					yyd[195] = 0;  
					yyd[196] = 0;  
					yyd[197] = -137;  
					yyd[198] = 0;  
					yyd[199] = 0;  
					yyd[200] = -182;  
					yyd[201] = -181;  
					yyd[202] = 0;  
					yyd[203] = 0;  
					yyd[204] = -122;  
					yyd[205] = 0;  
					yyd[206] = 0;  
					yyd[207] = 0;  
					yyd[208] = 0;  
					yyd[209] = -54;  
					yyd[210] = -61;  
					yyd[211] = 0;  
					yyd[212] = 0;  
					yyd[213] = 0;  
					yyd[214] = 0;  
					yyd[215] = 0;  
					yyd[216] = 0;  
					yyd[217] = -152;  
					yyd[218] = -153;  
					yyd[219] = -162;  
					yyd[220] = -160;  
					yyd[221] = -161;  
					yyd[222] = -159;  
					yyd[223] = -151;  
					yyd[224] = -154;  
					yyd[225] = 0;  
					yyd[226] = -87;  
					yyd[227] = -95;  
					yyd[228] = -94;  
					yyd[229] = -93;  
					yyd[230] = -92;  
					yyd[231] = -91;  
					yyd[232] = -82;  
					yyd[233] = -81;  
					yyd[234] = -139;  
					yyd[235] = 0;  
					yyd[236] = 0;  
					yyd[237] = 0;  
					yyd[238] = 0;  
					yyd[239] = -118;  
					yyd[240] = 0;  
					yyd[241] = -131;  
					yyd[242] = -132;  
					yyd[243] = -133;  
					yyd[244] = 0;  
					yyd[245] = -129;  
					yyd[246] = -130;  
					yyd[247] = 0;  
					yyd[248] = -125;  
					yyd[249] = -126;  
					yyd[250] = -127;  
					yyd[251] = -128;  
					yyd[252] = 0;  
					yyd[253] = -123;  
					yyd[254] = -124;  
					yyd[255] = 0;  
					yyd[256] = 0;  
					yyd[257] = 0;  
					yyd[258] = 0;  
					yyd[259] = 0;  
					yyd[260] = 0;  
					yyd[261] = 0;  
					yyd[262] = 0;  
					yyd[263] = 0;  
					yyd[264] = 0;  
					yyd[265] = -79;  
					yyd[266] = -72;  
					yyd[267] = 0;  
					yyd[268] = 0;  
					yyd[269] = -63;  
					yyd[270] = -73;  
					yyd[271] = 0;  
					yyd[272] = -74;  
					yyd[273] = -71;  
					yyd[274] = -70;  
					yyd[275] = -60;  
					yyd[276] = 0;  
					yyd[277] = 0;  
					yyd[278] = 0;  
					yyd[279] = -49;  
					yyd[280] = 0;  
					yyd[281] = 0;  
					yyd[282] = 0;  
					yyd[283] = 0;  
					yyd[284] = 0;  
					yyd[285] = -80;  
					yyd[286] = 0;  
					yyd[287] = -116;  
					yyd[288] = 0;  
					yyd[289] = 0;  
					yyd[290] = 0;  
					yyd[291] = 0;  
					yyd[292] = 0;  
					yyd[293] = 0;  
					yyd[294] = 0;  
					yyd[295] = 0;  
					yyd[296] = -138;  
					yyd[297] = -120;  
					yyd[298] = -148;  
					yyd[299] = 0;  
					yyd[300] = 0;  
					yyd[301] = 0;  
					yyd[302] = 0;  
					yyd[303] = 0;  
					yyd[304] = 0;  
					yyd[305] = 0;  
					yyd[306] = 0;  
					yyd[307] = -50;  
					yyd[308] = -14;  
					yyd[309] = -90;  
					yyd[310] = 0;  
					yyd[311] = -19;  
					yyd[312] = -16;  
					yyd[313] = -17;  
					yyd[314] = -119;  
					yyd[315] = -149;  
					yyd[316] = -150;  
					yyd[317] = -68;  
					yyd[318] = -67;  
					yyd[319] = -65;  
					yyd[320] = -66;  
					yyd[321] = -69;  
					yyd[322] = 0;  
					yyd[323] = -23;  
					yyd[324] = -20;  
					yyd[325] = -21;  
					yyd[326] = 0;  
					yyd[327] = 0;  
					yyd[328] = 0;  
					yyd[329] = 0;  
					yyd[330] = -18;  
					yyd[331] = -22; 

					yyal = new int[yynstates];
					yyal[0] = 1;  
					yyal[1] = 19;  
					yyal[2] = 34;  
					yyal[3] = 34;  
					yyal[4] = 48;  
					yyal[5] = 63;  
					yyal[6] = 72;  
					yyal[7] = 87;  
					yyal[8] = 87;  
					yyal[9] = 87;  
					yyal[10] = 87;  
					yyal[11] = 87;  
					yyal[12] = 87;  
					yyal[13] = 87;  
					yyal[14] = 87;  
					yyal[15] = 87;  
					yyal[16] = 107;  
					yyal[17] = 107;  
					yyal[18] = 108;  
					yyal[19] = 111;  
					yyal[20] = 114;  
					yyal[21] = 117;  
					yyal[22] = 120;  
					yyal[23] = 121;  
					yyal[24] = 121;  
					yyal[25] = 121;  
					yyal[26] = 121;  
					yyal[27] = 121;  
					yyal[28] = 121;  
					yyal[29] = 121;  
					yyal[30] = 121;  
					yyal[31] = 121;  
					yyal[32] = 121;  
					yyal[33] = 121;  
					yyal[34] = 121;  
					yyal[35] = 121;  
					yyal[36] = 121;  
					yyal[37] = 121;  
					yyal[38] = 123;  
					yyal[39] = 123;  
					yyal[40] = 123;  
					yyal[41] = 123;  
					yyal[42] = 123;  
					yyal[43] = 123;  
					yyal[44] = 123;  
					yyal[45] = 123;  
					yyal[46] = 123;  
					yyal[47] = 123;  
					yyal[48] = 123;  
					yyal[49] = 123;  
					yyal[50] = 123;  
					yyal[51] = 123;  
					yyal[52] = 123;  
					yyal[53] = 123;  
					yyal[54] = 127;  
					yyal[55] = 129;  
					yyal[56] = 129;  
					yyal[57] = 129;  
					yyal[58] = 137;  
					yyal[59] = 137;  
					yyal[60] = 151;  
					yyal[61] = 151;  
					yyal[62] = 151;  
					yyal[63] = 151;  
					yyal[64] = 152;  
					yyal[65] = 152;  
					yyal[66] = 153;  
					yyal[67] = 155;  
					yyal[68] = 156;  
					yyal[69] = 157;  
					yyal[70] = 157;  
					yyal[71] = 158;  
					yyal[72] = 160;  
					yyal[73] = 160;  
					yyal[74] = 161;  
					yyal[75] = 161;  
					yyal[76] = 163;  
					yyal[77] = 163;  
					yyal[78] = 164;  
					yyal[79] = 164;  
					yyal[80] = 164;  
					yyal[81] = 164;  
					yyal[82] = 164;  
					yyal[83] = 164;  
					yyal[84] = 164;  
					yyal[85] = 164;  
					yyal[86] = 164;  
					yyal[87] = 164;  
					yyal[88] = 164;  
					yyal[89] = 180;  
					yyal[90] = 181;  
					yyal[91] = 200;  
					yyal[92] = 219;  
					yyal[93] = 219;  
					yyal[94] = 248;  
					yyal[95] = 248;  
					yyal[96] = 248;  
					yyal[97] = 277;  
					yyal[98] = 278;  
					yyal[99] = 291;  
					yyal[100] = 297;  
					yyal[101] = 297;  
					yyal[102] = 297;  
					yyal[103] = 297;  
					yyal[104] = 312;  
					yyal[105] = 312;  
					yyal[106] = 313;  
					yyal[107] = 315;  
					yyal[108] = 316;  
					yyal[109] = 369;  
					yyal[110] = 369;  
					yyal[111] = 422;  
					yyal[112] = 422;  
					yyal[113] = 422;  
					yyal[114] = 422;  
					yyal[115] = 422;  
					yyal[116] = 422;  
					yyal[117] = 422;  
					yyal[118] = 423;  
					yyal[119] = 423;  
					yyal[120] = 423;  
					yyal[121] = 423;  
					yyal[122] = 423;  
					yyal[123] = 461;  
					yyal[124] = 492;  
					yyal[125] = 492;  
					yyal[126] = 523;  
					yyal[127] = 524;  
					yyal[128] = 525;  
					yyal[129] = 532;  
					yyal[130] = 537;  
					yyal[131] = 538;  
					yyal[132] = 569;  
					yyal[133] = 572;  
					yyal[134] = 575;  
					yyal[135] = 604;  
					yyal[136] = 631;  
					yyal[137] = 663;  
					yyal[138] = 690;  
					yyal[139] = 717;  
					yyal[140] = 724;  
					yyal[141] = 762;  
					yyal[142] = 770;  
					yyal[143] = 770;  
					yyal[144] = 798;  
					yyal[145] = 799;  
					yyal[146] = 799;  
					yyal[147] = 799;  
					yyal[148] = 799;  
					yyal[149] = 800;  
					yyal[150] = 815;  
					yyal[151] = 818;  
					yyal[152] = 821;  
					yyal[153] = 821;  
					yyal[154] = 821;  
					yyal[155] = 821;  
					yyal[156] = 821;  
					yyal[157] = 821;  
					yyal[158] = 821;  
					yyal[159] = 821;  
					yyal[160] = 826;  
					yyal[161] = 826;  
					yyal[162] = 826;  
					yyal[163] = 827;  
					yyal[164] = 827;  
					yyal[165] = 827;  
					yyal[166] = 841;  
					yyal[167] = 855;  
					yyal[168] = 855;  
					yyal[169] = 884;  
					yyal[170] = 884;  
					yyal[171] = 915;  
					yyal[172] = 946;  
					yyal[173] = 973;  
					yyal[174] = 973;  
					yyal[175] = 973;  
					yyal[176] = 973;  
					yyal[177] = 973;  
					yyal[178] = 973;  
					yyal[179] = 973;  
					yyal[180] = 973;  
					yyal[181] = 974;  
					yyal[182] = 975;  
					yyal[183] = 976;  
					yyal[184] = 976;  
					yyal[185] = 1002;  
					yyal[186] = 1029;  
					yyal[187] = 1029;  
					yyal[188] = 1029;  
					yyal[189] = 1048;  
					yyal[190] = 1064;  
					yyal[191] = 1078;  
					yyal[192] = 1088;  
					yyal[193] = 1096;  
					yyal[194] = 1103;  
					yyal[195] = 1109;  
					yyal[196] = 1114;  
					yyal[197] = 1118;  
					yyal[198] = 1118;  
					yyal[199] = 1140;  
					yyal[200] = 1167;  
					yyal[201] = 1167;  
					yyal[202] = 1167;  
					yyal[203] = 1196;  
					yyal[204] = 1197;  
					yyal[205] = 1197;  
					yyal[206] = 1198;  
					yyal[207] = 1229;  
					yyal[208] = 1256;  
					yyal[209] = 1271;  
					yyal[210] = 1271;  
					yyal[211] = 1271;  
					yyal[212] = 1272;  
					yyal[213] = 1273;  
					yyal[214] = 1274;  
					yyal[215] = 1276;  
					yyal[216] = 1277;  
					yyal[217] = 1295;  
					yyal[218] = 1295;  
					yyal[219] = 1295;  
					yyal[220] = 1295;  
					yyal[221] = 1295;  
					yyal[222] = 1295;  
					yyal[223] = 1295;  
					yyal[224] = 1295;  
					yyal[225] = 1295;  
					yyal[226] = 1326;  
					yyal[227] = 1326;  
					yyal[228] = 1326;  
					yyal[229] = 1326;  
					yyal[230] = 1326;  
					yyal[231] = 1326;  
					yyal[232] = 1326;  
					yyal[233] = 1326;  
					yyal[234] = 1326;  
					yyal[235] = 1326;  
					yyal[236] = 1356;  
					yyal[237] = 1386;  
					yyal[238] = 1417;  
					yyal[239] = 1444;  
					yyal[240] = 1444;  
					yyal[241] = 1471;  
					yyal[242] = 1471;  
					yyal[243] = 1471;  
					yyal[244] = 1471;  
					yyal[245] = 1498;  
					yyal[246] = 1498;  
					yyal[247] = 1498;  
					yyal[248] = 1525;  
					yyal[249] = 1525;  
					yyal[250] = 1525;  
					yyal[251] = 1525;  
					yyal[252] = 1525;  
					yyal[253] = 1552;  
					yyal[254] = 1552;  
					yyal[255] = 1552;  
					yyal[256] = 1579;  
					yyal[257] = 1606;  
					yyal[258] = 1633;  
					yyal[259] = 1660;  
					yyal[260] = 1687;  
					yyal[261] = 1714;  
					yyal[262] = 1715;  
					yyal[263] = 1716;  
					yyal[264] = 1745;  
					yyal[265] = 1774;  
					yyal[266] = 1774;  
					yyal[267] = 1774;  
					yyal[268] = 1804;  
					yyal[269] = 1833;  
					yyal[270] = 1833;  
					yyal[271] = 1833;  
					yyal[272] = 1863;  
					yyal[273] = 1863;  
					yyal[274] = 1863;  
					yyal[275] = 1863;  
					yyal[276] = 1863;  
					yyal[277] = 1877;  
					yyal[278] = 1891;  
					yyal[279] = 1896;  
					yyal[280] = 1896;  
					yyal[281] = 1897;  
					yyal[282] = 1927;  
					yyal[283] = 1929;  
					yyal[284] = 1930;  
					yyal[285] = 1931;  
					yyal[286] = 1931;  
					yyal[287] = 1932;  
					yyal[288] = 1932;  
					yyal[289] = 1951;  
					yyal[290] = 1967;  
					yyal[291] = 1981;  
					yyal[292] = 1991;  
					yyal[293] = 1999;  
					yyal[294] = 2006;  
					yyal[295] = 2012;  
					yyal[296] = 2017;  
					yyal[297] = 2017;  
					yyal[298] = 2017;  
					yyal[299] = 2017;  
					yyal[300] = 2018;  
					yyal[301] = 2019;  
					yyal[302] = 2033;  
					yyal[303] = 2061;  
					yyal[304] = 2075;  
					yyal[305] = 2077;  
					yyal[306] = 2078;  
					yyal[307] = 2079;  
					yyal[308] = 2079;  
					yyal[309] = 2079;  
					yyal[310] = 2079;  
					yyal[311] = 2080;  
					yyal[312] = 2080;  
					yyal[313] = 2080;  
					yyal[314] = 2080;  
					yyal[315] = 2080;  
					yyal[316] = 2080;  
					yyal[317] = 2080;  
					yyal[318] = 2080;  
					yyal[319] = 2080;  
					yyal[320] = 2080;  
					yyal[321] = 2080;  
					yyal[322] = 2080;  
					yyal[323] = 2081;  
					yyal[324] = 2081;  
					yyal[325] = 2081;  
					yyal[326] = 2081;  
					yyal[327] = 2110;  
					yyal[328] = 2123;  
					yyal[329] = 2124;  
					yyal[330] = 2125;  
					yyal[331] = 2125; 

					yyah = new int[yynstates];
					yyah[0] = 18;  
					yyah[1] = 33;  
					yyah[2] = 33;  
					yyah[3] = 47;  
					yyah[4] = 62;  
					yyah[5] = 71;  
					yyah[6] = 86;  
					yyah[7] = 86;  
					yyah[8] = 86;  
					yyah[9] = 86;  
					yyah[10] = 86;  
					yyah[11] = 86;  
					yyah[12] = 86;  
					yyah[13] = 86;  
					yyah[14] = 86;  
					yyah[15] = 106;  
					yyah[16] = 106;  
					yyah[17] = 107;  
					yyah[18] = 110;  
					yyah[19] = 113;  
					yyah[20] = 116;  
					yyah[21] = 119;  
					yyah[22] = 120;  
					yyah[23] = 120;  
					yyah[24] = 120;  
					yyah[25] = 120;  
					yyah[26] = 120;  
					yyah[27] = 120;  
					yyah[28] = 120;  
					yyah[29] = 120;  
					yyah[30] = 120;  
					yyah[31] = 120;  
					yyah[32] = 120;  
					yyah[33] = 120;  
					yyah[34] = 120;  
					yyah[35] = 120;  
					yyah[36] = 120;  
					yyah[37] = 122;  
					yyah[38] = 122;  
					yyah[39] = 122;  
					yyah[40] = 122;  
					yyah[41] = 122;  
					yyah[42] = 122;  
					yyah[43] = 122;  
					yyah[44] = 122;  
					yyah[45] = 122;  
					yyah[46] = 122;  
					yyah[47] = 122;  
					yyah[48] = 122;  
					yyah[49] = 122;  
					yyah[50] = 122;  
					yyah[51] = 122;  
					yyah[52] = 122;  
					yyah[53] = 126;  
					yyah[54] = 128;  
					yyah[55] = 128;  
					yyah[56] = 128;  
					yyah[57] = 136;  
					yyah[58] = 136;  
					yyah[59] = 150;  
					yyah[60] = 150;  
					yyah[61] = 150;  
					yyah[62] = 150;  
					yyah[63] = 151;  
					yyah[64] = 151;  
					yyah[65] = 152;  
					yyah[66] = 154;  
					yyah[67] = 155;  
					yyah[68] = 156;  
					yyah[69] = 156;  
					yyah[70] = 157;  
					yyah[71] = 159;  
					yyah[72] = 159;  
					yyah[73] = 160;  
					yyah[74] = 160;  
					yyah[75] = 162;  
					yyah[76] = 162;  
					yyah[77] = 163;  
					yyah[78] = 163;  
					yyah[79] = 163;  
					yyah[80] = 163;  
					yyah[81] = 163;  
					yyah[82] = 163;  
					yyah[83] = 163;  
					yyah[84] = 163;  
					yyah[85] = 163;  
					yyah[86] = 163;  
					yyah[87] = 163;  
					yyah[88] = 179;  
					yyah[89] = 180;  
					yyah[90] = 199;  
					yyah[91] = 218;  
					yyah[92] = 218;  
					yyah[93] = 247;  
					yyah[94] = 247;  
					yyah[95] = 247;  
					yyah[96] = 276;  
					yyah[97] = 277;  
					yyah[98] = 290;  
					yyah[99] = 296;  
					yyah[100] = 296;  
					yyah[101] = 296;  
					yyah[102] = 296;  
					yyah[103] = 311;  
					yyah[104] = 311;  
					yyah[105] = 312;  
					yyah[106] = 314;  
					yyah[107] = 315;  
					yyah[108] = 368;  
					yyah[109] = 368;  
					yyah[110] = 421;  
					yyah[111] = 421;  
					yyah[112] = 421;  
					yyah[113] = 421;  
					yyah[114] = 421;  
					yyah[115] = 421;  
					yyah[116] = 421;  
					yyah[117] = 422;  
					yyah[118] = 422;  
					yyah[119] = 422;  
					yyah[120] = 422;  
					yyah[121] = 422;  
					yyah[122] = 460;  
					yyah[123] = 491;  
					yyah[124] = 491;  
					yyah[125] = 522;  
					yyah[126] = 523;  
					yyah[127] = 524;  
					yyah[128] = 531;  
					yyah[129] = 536;  
					yyah[130] = 537;  
					yyah[131] = 568;  
					yyah[132] = 571;  
					yyah[133] = 574;  
					yyah[134] = 603;  
					yyah[135] = 630;  
					yyah[136] = 662;  
					yyah[137] = 689;  
					yyah[138] = 716;  
					yyah[139] = 723;  
					yyah[140] = 761;  
					yyah[141] = 769;  
					yyah[142] = 769;  
					yyah[143] = 797;  
					yyah[144] = 798;  
					yyah[145] = 798;  
					yyah[146] = 798;  
					yyah[147] = 798;  
					yyah[148] = 799;  
					yyah[149] = 814;  
					yyah[150] = 817;  
					yyah[151] = 820;  
					yyah[152] = 820;  
					yyah[153] = 820;  
					yyah[154] = 820;  
					yyah[155] = 820;  
					yyah[156] = 820;  
					yyah[157] = 820;  
					yyah[158] = 820;  
					yyah[159] = 825;  
					yyah[160] = 825;  
					yyah[161] = 825;  
					yyah[162] = 826;  
					yyah[163] = 826;  
					yyah[164] = 826;  
					yyah[165] = 840;  
					yyah[166] = 854;  
					yyah[167] = 854;  
					yyah[168] = 883;  
					yyah[169] = 883;  
					yyah[170] = 914;  
					yyah[171] = 945;  
					yyah[172] = 972;  
					yyah[173] = 972;  
					yyah[174] = 972;  
					yyah[175] = 972;  
					yyah[176] = 972;  
					yyah[177] = 972;  
					yyah[178] = 972;  
					yyah[179] = 972;  
					yyah[180] = 973;  
					yyah[181] = 974;  
					yyah[182] = 975;  
					yyah[183] = 975;  
					yyah[184] = 1001;  
					yyah[185] = 1028;  
					yyah[186] = 1028;  
					yyah[187] = 1028;  
					yyah[188] = 1047;  
					yyah[189] = 1063;  
					yyah[190] = 1077;  
					yyah[191] = 1087;  
					yyah[192] = 1095;  
					yyah[193] = 1102;  
					yyah[194] = 1108;  
					yyah[195] = 1113;  
					yyah[196] = 1117;  
					yyah[197] = 1117;  
					yyah[198] = 1139;  
					yyah[199] = 1166;  
					yyah[200] = 1166;  
					yyah[201] = 1166;  
					yyah[202] = 1195;  
					yyah[203] = 1196;  
					yyah[204] = 1196;  
					yyah[205] = 1197;  
					yyah[206] = 1228;  
					yyah[207] = 1255;  
					yyah[208] = 1270;  
					yyah[209] = 1270;  
					yyah[210] = 1270;  
					yyah[211] = 1271;  
					yyah[212] = 1272;  
					yyah[213] = 1273;  
					yyah[214] = 1275;  
					yyah[215] = 1276;  
					yyah[216] = 1294;  
					yyah[217] = 1294;  
					yyah[218] = 1294;  
					yyah[219] = 1294;  
					yyah[220] = 1294;  
					yyah[221] = 1294;  
					yyah[222] = 1294;  
					yyah[223] = 1294;  
					yyah[224] = 1294;  
					yyah[225] = 1325;  
					yyah[226] = 1325;  
					yyah[227] = 1325;  
					yyah[228] = 1325;  
					yyah[229] = 1325;  
					yyah[230] = 1325;  
					yyah[231] = 1325;  
					yyah[232] = 1325;  
					yyah[233] = 1325;  
					yyah[234] = 1325;  
					yyah[235] = 1355;  
					yyah[236] = 1385;  
					yyah[237] = 1416;  
					yyah[238] = 1443;  
					yyah[239] = 1443;  
					yyah[240] = 1470;  
					yyah[241] = 1470;  
					yyah[242] = 1470;  
					yyah[243] = 1470;  
					yyah[244] = 1497;  
					yyah[245] = 1497;  
					yyah[246] = 1497;  
					yyah[247] = 1524;  
					yyah[248] = 1524;  
					yyah[249] = 1524;  
					yyah[250] = 1524;  
					yyah[251] = 1524;  
					yyah[252] = 1551;  
					yyah[253] = 1551;  
					yyah[254] = 1551;  
					yyah[255] = 1578;  
					yyah[256] = 1605;  
					yyah[257] = 1632;  
					yyah[258] = 1659;  
					yyah[259] = 1686;  
					yyah[260] = 1713;  
					yyah[261] = 1714;  
					yyah[262] = 1715;  
					yyah[263] = 1744;  
					yyah[264] = 1773;  
					yyah[265] = 1773;  
					yyah[266] = 1773;  
					yyah[267] = 1803;  
					yyah[268] = 1832;  
					yyah[269] = 1832;  
					yyah[270] = 1832;  
					yyah[271] = 1862;  
					yyah[272] = 1862;  
					yyah[273] = 1862;  
					yyah[274] = 1862;  
					yyah[275] = 1862;  
					yyah[276] = 1876;  
					yyah[277] = 1890;  
					yyah[278] = 1895;  
					yyah[279] = 1895;  
					yyah[280] = 1896;  
					yyah[281] = 1926;  
					yyah[282] = 1928;  
					yyah[283] = 1929;  
					yyah[284] = 1930;  
					yyah[285] = 1930;  
					yyah[286] = 1931;  
					yyah[287] = 1931;  
					yyah[288] = 1950;  
					yyah[289] = 1966;  
					yyah[290] = 1980;  
					yyah[291] = 1990;  
					yyah[292] = 1998;  
					yyah[293] = 2005;  
					yyah[294] = 2011;  
					yyah[295] = 2016;  
					yyah[296] = 2016;  
					yyah[297] = 2016;  
					yyah[298] = 2016;  
					yyah[299] = 2017;  
					yyah[300] = 2018;  
					yyah[301] = 2032;  
					yyah[302] = 2060;  
					yyah[303] = 2074;  
					yyah[304] = 2076;  
					yyah[305] = 2077;  
					yyah[306] = 2078;  
					yyah[307] = 2078;  
					yyah[308] = 2078;  
					yyah[309] = 2078;  
					yyah[310] = 2079;  
					yyah[311] = 2079;  
					yyah[312] = 2079;  
					yyah[313] = 2079;  
					yyah[314] = 2079;  
					yyah[315] = 2079;  
					yyah[316] = 2079;  
					yyah[317] = 2079;  
					yyah[318] = 2079;  
					yyah[319] = 2079;  
					yyah[320] = 2079;  
					yyah[321] = 2079;  
					yyah[322] = 2080;  
					yyah[323] = 2080;  
					yyah[324] = 2080;  
					yyah[325] = 2080;  
					yyah[326] = 2109;  
					yyah[327] = 2122;  
					yyah[328] = 2123;  
					yyah[329] = 2124;  
					yyah[330] = 2124;  
					yyah[331] = 2124; 

					yygl = new int[yynstates];
					yygl[0] = 1;  
					yygl[1] = 18;  
					yygl[2] = 25;  
					yygl[3] = 25;  
					yygl[4] = 30;  
					yygl[5] = 36;  
					yygl[6] = 37;  
					yygl[7] = 38;  
					yygl[8] = 38;  
					yygl[9] = 38;  
					yygl[10] = 38;  
					yygl[11] = 38;  
					yygl[12] = 38;  
					yygl[13] = 38;  
					yygl[14] = 38;  
					yygl[15] = 38;  
					yygl[16] = 54;  
					yygl[17] = 54;  
					yygl[18] = 54;  
					yygl[19] = 57;  
					yygl[20] = 60;  
					yygl[21] = 63;  
					yygl[22] = 66;  
					yygl[23] = 67;  
					yygl[24] = 67;  
					yygl[25] = 67;  
					yygl[26] = 67;  
					yygl[27] = 67;  
					yygl[28] = 67;  
					yygl[29] = 67;  
					yygl[30] = 67;  
					yygl[31] = 67;  
					yygl[32] = 67;  
					yygl[33] = 67;  
					yygl[34] = 67;  
					yygl[35] = 67;  
					yygl[36] = 67;  
					yygl[37] = 67;  
					yygl[38] = 68;  
					yygl[39] = 68;  
					yygl[40] = 68;  
					yygl[41] = 68;  
					yygl[42] = 68;  
					yygl[43] = 68;  
					yygl[44] = 68;  
					yygl[45] = 68;  
					yygl[46] = 68;  
					yygl[47] = 68;  
					yygl[48] = 68;  
					yygl[49] = 68;  
					yygl[50] = 68;  
					yygl[51] = 68;  
					yygl[52] = 68;  
					yygl[53] = 68;  
					yygl[54] = 69;  
					yygl[55] = 70;  
					yygl[56] = 70;  
					yygl[57] = 70;  
					yygl[58] = 78;  
					yygl[59] = 78;  
					yygl[60] = 84;  
					yygl[61] = 84;  
					yygl[62] = 84;  
					yygl[63] = 84;  
					yygl[64] = 84;  
					yygl[65] = 84;  
					yygl[66] = 84;  
					yygl[67] = 84;  
					yygl[68] = 84;  
					yygl[69] = 84;  
					yygl[70] = 84;  
					yygl[71] = 84;  
					yygl[72] = 85;  
					yygl[73] = 85;  
					yygl[74] = 86;  
					yygl[75] = 86;  
					yygl[76] = 86;  
					yygl[77] = 86;  
					yygl[78] = 86;  
					yygl[79] = 86;  
					yygl[80] = 86;  
					yygl[81] = 86;  
					yygl[82] = 86;  
					yygl[83] = 86;  
					yygl[84] = 86;  
					yygl[85] = 86;  
					yygl[86] = 86;  
					yygl[87] = 86;  
					yygl[88] = 86;  
					yygl[89] = 87;  
					yygl[90] = 87;  
					yygl[91] = 104;  
					yygl[92] = 121;  
					yygl[93] = 121;  
					yygl[94] = 139;  
					yygl[95] = 139;  
					yygl[96] = 139;  
					yygl[97] = 156;  
					yygl[98] = 156;  
					yygl[99] = 163;  
					yygl[100] = 164;  
					yygl[101] = 164;  
					yygl[102] = 164;  
					yygl[103] = 164;  
					yygl[104] = 170;  
					yygl[105] = 170;  
					yygl[106] = 170;  
					yygl[107] = 170;  
					yygl[108] = 170;  
					yygl[109] = 170;  
					yygl[110] = 170;  
					yygl[111] = 170;  
					yygl[112] = 170;  
					yygl[113] = 170;  
					yygl[114] = 170;  
					yygl[115] = 170;  
					yygl[116] = 170;  
					yygl[117] = 170;  
					yygl[118] = 170;  
					yygl[119] = 170;  
					yygl[120] = 170;  
					yygl[121] = 170;  
					yygl[122] = 170;  
					yygl[123] = 170;  
					yygl[124] = 171;  
					yygl[125] = 171;  
					yygl[126] = 188;  
					yygl[127] = 188;  
					yygl[128] = 188;  
					yygl[129] = 188;  
					yygl[130] = 189;  
					yygl[131] = 189;  
					yygl[132] = 206;  
					yygl[133] = 209;  
					yygl[134] = 212;  
					yygl[135] = 229;  
					yygl[136] = 253;  
					yygl[137] = 253;  
					yygl[138] = 277;  
					yygl[139] = 301;  
					yygl[140] = 301;  
					yygl[141] = 301;  
					yygl[142] = 301;  
					yygl[143] = 301;  
					yygl[144] = 302;  
					yygl[145] = 302;  
					yygl[146] = 302;  
					yygl[147] = 302;  
					yygl[148] = 302;  
					yygl[149] = 302;  
					yygl[150] = 309;  
					yygl[151] = 312;  
					yygl[152] = 315;  
					yygl[153] = 315;  
					yygl[154] = 315;  
					yygl[155] = 315;  
					yygl[156] = 315;  
					yygl[157] = 315;  
					yygl[158] = 315;  
					yygl[159] = 315;  
					yygl[160] = 318;  
					yygl[161] = 318;  
					yygl[162] = 318;  
					yygl[163] = 318;  
					yygl[164] = 318;  
					yygl[165] = 318;  
					yygl[166] = 323;  
					yygl[167] = 328;  
					yygl[168] = 328;  
					yygl[169] = 347;  
					yygl[170] = 347;  
					yygl[171] = 364;  
					yygl[172] = 381;  
					yygl[173] = 405;  
					yygl[174] = 405;  
					yygl[175] = 405;  
					yygl[176] = 405;  
					yygl[177] = 405;  
					yygl[178] = 405;  
					yygl[179] = 405;  
					yygl[180] = 405;  
					yygl[181] = 405;  
					yygl[182] = 405;  
					yygl[183] = 405;  
					yygl[184] = 405;  
					yygl[185] = 405;  
					yygl[186] = 419;  
					yygl[187] = 419;  
					yygl[188] = 419;  
					yygl[189] = 420;  
					yygl[190] = 421;  
					yygl[191] = 422;  
					yygl[192] = 423;  
					yygl[193] = 423;  
					yygl[194] = 423;  
					yygl[195] = 423;  
					yygl[196] = 423;  
					yygl[197] = 423;  
					yygl[198] = 423;  
					yygl[199] = 424;  
					yygl[200] = 448;  
					yygl[201] = 448;  
					yygl[202] = 448;  
					yygl[203] = 465;  
					yygl[204] = 465;  
					yygl[205] = 465;  
					yygl[206] = 465;  
					yygl[207] = 482;  
					yygl[208] = 499;  
					yygl[209] = 506;  
					yygl[210] = 506;  
					yygl[211] = 506;  
					yygl[212] = 506;  
					yygl[213] = 506;  
					yygl[214] = 506;  
					yygl[215] = 506;  
					yygl[216] = 506;  
					yygl[217] = 522;  
					yygl[218] = 522;  
					yygl[219] = 522;  
					yygl[220] = 522;  
					yygl[221] = 522;  
					yygl[222] = 522;  
					yygl[223] = 522;  
					yygl[224] = 522;  
					yygl[225] = 522;  
					yygl[226] = 523;  
					yygl[227] = 523;  
					yygl[228] = 523;  
					yygl[229] = 523;  
					yygl[230] = 523;  
					yygl[231] = 523;  
					yygl[232] = 523;  
					yygl[233] = 523;  
					yygl[234] = 523;  
					yygl[235] = 523;  
					yygl[236] = 541;  
					yygl[237] = 559;  
					yygl[238] = 576;  
					yygl[239] = 600;  
					yygl[240] = 600;  
					yygl[241] = 614;  
					yygl[242] = 614;  
					yygl[243] = 614;  
					yygl[244] = 614;  
					yygl[245] = 629;  
					yygl[246] = 629;  
					yygl[247] = 629;  
					yygl[248] = 645;  
					yygl[249] = 645;  
					yygl[250] = 645;  
					yygl[251] = 645;  
					yygl[252] = 645;  
					yygl[253] = 662;  
					yygl[254] = 662;  
					yygl[255] = 662;  
					yygl[256] = 680;  
					yygl[257] = 699;  
					yygl[258] = 719;  
					yygl[259] = 740;  
					yygl[260] = 762;  
					yygl[261] = 786;  
					yygl[262] = 786;  
					yygl[263] = 786;  
					yygl[264] = 803;  
					yygl[265] = 820;  
					yygl[266] = 820;  
					yygl[267] = 820;  
					yygl[268] = 820;  
					yygl[269] = 821;  
					yygl[270] = 821;  
					yygl[271] = 821;  
					yygl[272] = 821;  
					yygl[273] = 821;  
					yygl[274] = 821;  
					yygl[275] = 821;  
					yygl[276] = 821;  
					yygl[277] = 829;  
					yygl[278] = 837;  
					yygl[279] = 840;  
					yygl[280] = 840;  
					yygl[281] = 840;  
					yygl[282] = 859;  
					yygl[283] = 859;  
					yygl[284] = 859;  
					yygl[285] = 859;  
					yygl[286] = 859;  
					yygl[287] = 859;  
					yygl[288] = 859;  
					yygl[289] = 860;  
					yygl[290] = 861;  
					yygl[291] = 862;  
					yygl[292] = 863;  
					yygl[293] = 863;  
					yygl[294] = 863;  
					yygl[295] = 863;  
					yygl[296] = 863;  
					yygl[297] = 863;  
					yygl[298] = 863;  
					yygl[299] = 863;  
					yygl[300] = 863;  
					yygl[301] = 863;  
					yygl[302] = 868;  
					yygl[303] = 885;  
					yygl[304] = 890;  
					yygl[305] = 890;  
					yygl[306] = 890;  
					yygl[307] = 890;  
					yygl[308] = 890;  
					yygl[309] = 890;  
					yygl[310] = 890;  
					yygl[311] = 890;  
					yygl[312] = 890;  
					yygl[313] = 890;  
					yygl[314] = 890;  
					yygl[315] = 890;  
					yygl[316] = 890;  
					yygl[317] = 890;  
					yygl[318] = 890;  
					yygl[319] = 890;  
					yygl[320] = 890;  
					yygl[321] = 890;  
					yygl[322] = 890;  
					yygl[323] = 890;  
					yygl[324] = 890;  
					yygl[325] = 890;  
					yygl[326] = 890;  
					yygl[327] = 907;  
					yygl[328] = 914;  
					yygl[329] = 914;  
					yygl[330] = 914;  
					yygl[331] = 914; 

					yygh = new int[yynstates];
					yygh[0] = 17;  
					yygh[1] = 24;  
					yygh[2] = 24;  
					yygh[3] = 29;  
					yygh[4] = 35;  
					yygh[5] = 36;  
					yygh[6] = 37;  
					yygh[7] = 37;  
					yygh[8] = 37;  
					yygh[9] = 37;  
					yygh[10] = 37;  
					yygh[11] = 37;  
					yygh[12] = 37;  
					yygh[13] = 37;  
					yygh[14] = 37;  
					yygh[15] = 53;  
					yygh[16] = 53;  
					yygh[17] = 53;  
					yygh[18] = 56;  
					yygh[19] = 59;  
					yygh[20] = 62;  
					yygh[21] = 65;  
					yygh[22] = 66;  
					yygh[23] = 66;  
					yygh[24] = 66;  
					yygh[25] = 66;  
					yygh[26] = 66;  
					yygh[27] = 66;  
					yygh[28] = 66;  
					yygh[29] = 66;  
					yygh[30] = 66;  
					yygh[31] = 66;  
					yygh[32] = 66;  
					yygh[33] = 66;  
					yygh[34] = 66;  
					yygh[35] = 66;  
					yygh[36] = 66;  
					yygh[37] = 67;  
					yygh[38] = 67;  
					yygh[39] = 67;  
					yygh[40] = 67;  
					yygh[41] = 67;  
					yygh[42] = 67;  
					yygh[43] = 67;  
					yygh[44] = 67;  
					yygh[45] = 67;  
					yygh[46] = 67;  
					yygh[47] = 67;  
					yygh[48] = 67;  
					yygh[49] = 67;  
					yygh[50] = 67;  
					yygh[51] = 67;  
					yygh[52] = 67;  
					yygh[53] = 68;  
					yygh[54] = 69;  
					yygh[55] = 69;  
					yygh[56] = 69;  
					yygh[57] = 77;  
					yygh[58] = 77;  
					yygh[59] = 83;  
					yygh[60] = 83;  
					yygh[61] = 83;  
					yygh[62] = 83;  
					yygh[63] = 83;  
					yygh[64] = 83;  
					yygh[65] = 83;  
					yygh[66] = 83;  
					yygh[67] = 83;  
					yygh[68] = 83;  
					yygh[69] = 83;  
					yygh[70] = 83;  
					yygh[71] = 84;  
					yygh[72] = 84;  
					yygh[73] = 85;  
					yygh[74] = 85;  
					yygh[75] = 85;  
					yygh[76] = 85;  
					yygh[77] = 85;  
					yygh[78] = 85;  
					yygh[79] = 85;  
					yygh[80] = 85;  
					yygh[81] = 85;  
					yygh[82] = 85;  
					yygh[83] = 85;  
					yygh[84] = 85;  
					yygh[85] = 85;  
					yygh[86] = 85;  
					yygh[87] = 85;  
					yygh[88] = 86;  
					yygh[89] = 86;  
					yygh[90] = 103;  
					yygh[91] = 120;  
					yygh[92] = 120;  
					yygh[93] = 138;  
					yygh[94] = 138;  
					yygh[95] = 138;  
					yygh[96] = 155;  
					yygh[97] = 155;  
					yygh[98] = 162;  
					yygh[99] = 163;  
					yygh[100] = 163;  
					yygh[101] = 163;  
					yygh[102] = 163;  
					yygh[103] = 169;  
					yygh[104] = 169;  
					yygh[105] = 169;  
					yygh[106] = 169;  
					yygh[107] = 169;  
					yygh[108] = 169;  
					yygh[109] = 169;  
					yygh[110] = 169;  
					yygh[111] = 169;  
					yygh[112] = 169;  
					yygh[113] = 169;  
					yygh[114] = 169;  
					yygh[115] = 169;  
					yygh[116] = 169;  
					yygh[117] = 169;  
					yygh[118] = 169;  
					yygh[119] = 169;  
					yygh[120] = 169;  
					yygh[121] = 169;  
					yygh[122] = 169;  
					yygh[123] = 170;  
					yygh[124] = 170;  
					yygh[125] = 187;  
					yygh[126] = 187;  
					yygh[127] = 187;  
					yygh[128] = 187;  
					yygh[129] = 188;  
					yygh[130] = 188;  
					yygh[131] = 205;  
					yygh[132] = 208;  
					yygh[133] = 211;  
					yygh[134] = 228;  
					yygh[135] = 252;  
					yygh[136] = 252;  
					yygh[137] = 276;  
					yygh[138] = 300;  
					yygh[139] = 300;  
					yygh[140] = 300;  
					yygh[141] = 300;  
					yygh[142] = 300;  
					yygh[143] = 301;  
					yygh[144] = 301;  
					yygh[145] = 301;  
					yygh[146] = 301;  
					yygh[147] = 301;  
					yygh[148] = 301;  
					yygh[149] = 308;  
					yygh[150] = 311;  
					yygh[151] = 314;  
					yygh[152] = 314;  
					yygh[153] = 314;  
					yygh[154] = 314;  
					yygh[155] = 314;  
					yygh[156] = 314;  
					yygh[157] = 314;  
					yygh[158] = 314;  
					yygh[159] = 317;  
					yygh[160] = 317;  
					yygh[161] = 317;  
					yygh[162] = 317;  
					yygh[163] = 317;  
					yygh[164] = 317;  
					yygh[165] = 322;  
					yygh[166] = 327;  
					yygh[167] = 327;  
					yygh[168] = 346;  
					yygh[169] = 346;  
					yygh[170] = 363;  
					yygh[171] = 380;  
					yygh[172] = 404;  
					yygh[173] = 404;  
					yygh[174] = 404;  
					yygh[175] = 404;  
					yygh[176] = 404;  
					yygh[177] = 404;  
					yygh[178] = 404;  
					yygh[179] = 404;  
					yygh[180] = 404;  
					yygh[181] = 404;  
					yygh[182] = 404;  
					yygh[183] = 404;  
					yygh[184] = 404;  
					yygh[185] = 418;  
					yygh[186] = 418;  
					yygh[187] = 418;  
					yygh[188] = 419;  
					yygh[189] = 420;  
					yygh[190] = 421;  
					yygh[191] = 422;  
					yygh[192] = 422;  
					yygh[193] = 422;  
					yygh[194] = 422;  
					yygh[195] = 422;  
					yygh[196] = 422;  
					yygh[197] = 422;  
					yygh[198] = 423;  
					yygh[199] = 447;  
					yygh[200] = 447;  
					yygh[201] = 447;  
					yygh[202] = 464;  
					yygh[203] = 464;  
					yygh[204] = 464;  
					yygh[205] = 464;  
					yygh[206] = 481;  
					yygh[207] = 498;  
					yygh[208] = 505;  
					yygh[209] = 505;  
					yygh[210] = 505;  
					yygh[211] = 505;  
					yygh[212] = 505;  
					yygh[213] = 505;  
					yygh[214] = 505;  
					yygh[215] = 505;  
					yygh[216] = 521;  
					yygh[217] = 521;  
					yygh[218] = 521;  
					yygh[219] = 521;  
					yygh[220] = 521;  
					yygh[221] = 521;  
					yygh[222] = 521;  
					yygh[223] = 521;  
					yygh[224] = 521;  
					yygh[225] = 522;  
					yygh[226] = 522;  
					yygh[227] = 522;  
					yygh[228] = 522;  
					yygh[229] = 522;  
					yygh[230] = 522;  
					yygh[231] = 522;  
					yygh[232] = 522;  
					yygh[233] = 522;  
					yygh[234] = 522;  
					yygh[235] = 540;  
					yygh[236] = 558;  
					yygh[237] = 575;  
					yygh[238] = 599;  
					yygh[239] = 599;  
					yygh[240] = 613;  
					yygh[241] = 613;  
					yygh[242] = 613;  
					yygh[243] = 613;  
					yygh[244] = 628;  
					yygh[245] = 628;  
					yygh[246] = 628;  
					yygh[247] = 644;  
					yygh[248] = 644;  
					yygh[249] = 644;  
					yygh[250] = 644;  
					yygh[251] = 644;  
					yygh[252] = 661;  
					yygh[253] = 661;  
					yygh[254] = 661;  
					yygh[255] = 679;  
					yygh[256] = 698;  
					yygh[257] = 718;  
					yygh[258] = 739;  
					yygh[259] = 761;  
					yygh[260] = 785;  
					yygh[261] = 785;  
					yygh[262] = 785;  
					yygh[263] = 802;  
					yygh[264] = 819;  
					yygh[265] = 819;  
					yygh[266] = 819;  
					yygh[267] = 819;  
					yygh[268] = 820;  
					yygh[269] = 820;  
					yygh[270] = 820;  
					yygh[271] = 820;  
					yygh[272] = 820;  
					yygh[273] = 820;  
					yygh[274] = 820;  
					yygh[275] = 820;  
					yygh[276] = 828;  
					yygh[277] = 836;  
					yygh[278] = 839;  
					yygh[279] = 839;  
					yygh[280] = 839;  
					yygh[281] = 858;  
					yygh[282] = 858;  
					yygh[283] = 858;  
					yygh[284] = 858;  
					yygh[285] = 858;  
					yygh[286] = 858;  
					yygh[287] = 858;  
					yygh[288] = 859;  
					yygh[289] = 860;  
					yygh[290] = 861;  
					yygh[291] = 862;  
					yygh[292] = 862;  
					yygh[293] = 862;  
					yygh[294] = 862;  
					yygh[295] = 862;  
					yygh[296] = 862;  
					yygh[297] = 862;  
					yygh[298] = 862;  
					yygh[299] = 862;  
					yygh[300] = 862;  
					yygh[301] = 867;  
					yygh[302] = 884;  
					yygh[303] = 889;  
					yygh[304] = 889;  
					yygh[305] = 889;  
					yygh[306] = 889;  
					yygh[307] = 889;  
					yygh[308] = 889;  
					yygh[309] = 889;  
					yygh[310] = 889;  
					yygh[311] = 889;  
					yygh[312] = 889;  
					yygh[313] = 889;  
					yygh[314] = 889;  
					yygh[315] = 889;  
					yygh[316] = 889;  
					yygh[317] = 889;  
					yygh[318] = 889;  
					yygh[319] = 889;  
					yygh[320] = 889;  
					yygh[321] = 889;  
					yygh[322] = 889;  
					yygh[323] = 889;  
					yygh[324] = 889;  
					yygh[325] = 889;  
					yygh[326] = 906;  
					yygh[327] = 913;  
					yygh[328] = 913;  
					yygh[329] = 913;  
					yygh[330] = 913;  
					yygh[331] = 913; 

					yyr[yyrc] = new YYRRec(1,-2);yyrc++; 
					yyr[yyrc] = new YYRRec(2,-3);yyrc++; 
					yyr[yyrc] = new YYRRec(0,-3);yyrc++; 
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
					yyr[yyrc] = new YYRRec(5,-13);yyrc++; 
					yyr[yyrc] = new YYRRec(2,-13);yyrc++; 
					yyr[yyrc] = new YYRRec(5,-14);yyrc++; 
					yyr[yyrc] = new YYRRec(5,-14);yyrc++; 
					yyr[yyrc] = new YYRRec(5,-15);yyrc++; 
					yyr[yyrc] = new YYRRec(2,-15);yyrc++; 
					yyr[yyrc] = new YYRRec(5,-17);yyrc++; 
					yyr[yyrc] = new YYRRec(5,-17);yyrc++; 
					yyr[yyrc] = new YYRRec(5,-18);yyrc++; 
					yyr[yyrc] = new YYRRec(2,-18);yyrc++; 
					yyr[yyrc] = new YYRRec(5,-8);yyrc++; 
					yyr[yyrc] = new YYRRec(3,-8);yyrc++; 
					yyr[yyrc] = new YYRRec(3,-8);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-12);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-12);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-20);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-20);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-20);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-20);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-20);yyrc++; 
					yyr[yyrc] = new YYRRec(3,-5);yyrc++; 
					yyr[yyrc] = new YYRRec(4,-6);yyrc++; 
					yyr[yyrc] = new YYRRec(4,-6);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-31);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-31);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-31);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-31);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-28);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-28);yyrc++; 
					yyr[yyrc] = new YYRRec(2,-30);yyrc++; 
					yyr[yyrc] = new YYRRec(3,-30);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-32);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-32);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-32);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-32);yyrc++; 
					yyr[yyrc] = new YYRRec(7,-11);yyrc++; 
					yyr[yyrc] = new YYRRec(3,-36);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-36);yyrc++; 
					yyr[yyrc] = new YYRRec(0,-36);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-34);yyrc++; 
					yyr[yyrc] = new YYRRec(6,-9);yyrc++; 
					yyr[yyrc] = new YYRRec(5,-9);yyrc++; 
					yyr[yyrc] = new YYRRec(3,-9);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-39);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-38);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-38);yyrc++; 
					yyr[yyrc] = new YYRRec(3,-19);yyrc++; 
					yyr[yyrc] = new YYRRec(2,-19);yyrc++; 
					yyr[yyrc] = new YYRRec(0,-19);yyrc++; 
					yyr[yyrc] = new YYRRec(3,-40);yyrc++; 
					yyr[yyrc] = new YYRRec(2,-42);yyrc++; 
					yyr[yyrc] = new YYRRec(3,-42);yyrc++; 
					yyr[yyrc] = new YYRRec(3,-43);yyrc++; 
					yyr[yyrc] = new YYRRec(3,-43);yyrc++; 
					yyr[yyrc] = new YYRRec(3,-43);yyrc++; 
					yyr[yyrc] = new YYRRec(3,-43);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-43);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-43);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-43);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-43);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-43);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-43);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-43);yyrc++; 
					yyr[yyrc] = new YYRRec(6,-10);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-46);yyrc++; 
					yyr[yyrc] = new YYRRec(3,-16);yyrc++; 
					yyr[yyrc] = new YYRRec(4,-16);yyrc++; 
					yyr[yyrc] = new YYRRec(3,-16);yyrc++; 
					yyr[yyrc] = new YYRRec(3,-16);yyrc++; 
					yyr[yyrc] = new YYRRec(2,-16);yyrc++; 
					yyr[yyrc] = new YYRRec(2,-16);yyrc++; 
					yyr[yyrc] = new YYRRec(0,-16);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-49);yyrc++; 
					yyr[yyrc] = new YYRRec(3,-49);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-49);yyrc++; 
					yyr[yyrc] = new YYRRec(2,-53);yyrc++; 
					yyr[yyrc] = new YYRRec(3,-53);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-54);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-54);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-54);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-54);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-54);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-29);yyrc++; 
					yyr[yyrc] = new YYRRec(0,-29);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-55);yyrc++; 
					yyr[yyrc] = new YYRRec(3,-56);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-56);yyrc++; 
					yyr[yyrc] = new YYRRec(3,-57);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-57);yyrc++; 
					yyr[yyrc] = new YYRRec(3,-58);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-58);yyrc++; 
					yyr[yyrc] = new YYRRec(3,-59);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-59);yyrc++; 
					yyr[yyrc] = new YYRRec(3,-60);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-60);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-61);yyrc++; 
					yyr[yyrc] = new YYRRec(3,-61);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-62);yyrc++; 
					yyr[yyrc] = new YYRRec(3,-62);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-64);yyrc++; 
					yyr[yyrc] = new YYRRec(3,-64);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-66);yyrc++; 
					yyr[yyrc] = new YYRRec(3,-66);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-68);yyrc++; 
					yyr[yyrc] = new YYRRec(2,-68);yyrc++; 
					yyr[yyrc] = new YYRRec(4,-70);yyrc++; 
					yyr[yyrc] = new YYRRec(3,-70);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-70);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-70);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-63);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-63);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-65);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-65);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-65);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-65);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-67);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-67);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-69);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-69);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-69);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-71);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-71);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-71);yyrc++; 
					yyr[yyrc] = new YYRRec(2,-51);yyrc++; 
					yyr[yyrc] = new YYRRec(4,-51);yyrc++; 
					yyr[yyrc] = new YYRRec(3,-51);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-74);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-74);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-74);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-74);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-74);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-72);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-72);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-72);yyrc++; 
					yyr[yyrc] = new YYRRec(4,-50);yyrc++; 
					yyr[yyrc] = new YYRRec(5,-50);yyrc++; 
					yyr[yyrc] = new YYRRec(5,-50);yyrc++; 
					yyr[yyrc] = new YYRRec(3,-27);yyrc++; 
					yyr[yyrc] = new YYRRec(3,-27);yyrc++; 
					yyr[yyrc] = new YYRRec(3,-27);yyrc++; 
					yyr[yyrc] = new YYRRec(3,-27);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-27);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-27);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-27);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-27);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-44);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-44);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-44);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-44);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-41);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-41);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-41);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-41);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-41);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-41);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-41);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-41);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-41);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-41);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-52);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-52);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-52);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-75);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-75);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-75);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-26);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-26);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-73);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-73);yyrc++; 
					yyr[yyrc] = new YYRRec(2,-37);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-37);yyrc++; 
					yyr[yyrc] = new YYRRec(2,-76);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-76);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-22);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-24);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-35);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-35);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-21);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-21);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-45);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-45);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-45);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-33);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-33);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-33);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-33);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-33);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-33);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-33);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-33);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-33);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-47);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-47);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-48);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-48);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-25);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-23);yyrc++;
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

            if (yyerrflag==0) yyerror("syntax error");

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
            yysp -= yyr[-yyn].len;

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
            while (1 == 1)
            {
                AToken lasttoken = FindTokenOpt(Input);
                if (lasttoken.token == 0) break;
                if (lasttoken.token != t_ignore) TokenList.Add(lasttoken);
                if (Input.Length > lasttoken.val.Length)
                    Input = Input.Substring(lasttoken.val.Length);
                else return true;
            }
            System.Console.WriteLine(Input);
            System.Console.WriteLine();
            System.Console.WriteLine("No matching token found!");
            return false;
        }
        public AToken FindTokenOpt(string Rest)
        {
            ArrayList Results = new ArrayList();
            ArrayList ResultsV = new ArrayList();
            Match m;
            try
            {

                for (int idx = 0; idx < tList.Count; idx++)
                {
                    m = rList[idx].Match(Rest);
                    if (m.Success)
                    {
                        Results.Add(tList[idx]);
                        ResultsV.Add(m.Value);
                    }
                }

            }
            catch { }
            int maxlength = 0;
            int besttoken = 0;
            AToken ret = new AToken();
            ret.token = besttoken;
            for (int i = 0; i < Results.Count; i++)
            {
                if (ResultsV[i].ToString().Length > maxlength)
                {
                    maxlength = ResultsV[i].ToString().Length;
                    besttoken = (int)Results[i];
                    ret.token = besttoken;
                    if (besttoken != 0)
                        ret.val = ResultsV[i].ToString();
                }
            }
            return ret;
        }
        public static string SubScanner(string file)
        {
            StreamReader in_s = File.OpenText(file);
            string inputstream = in_s.ReadToEnd();
            in_s.Close();

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

			if (Regex.IsMatch(Rest,"^((,[\\s\\t\\x00]*)?;+)")){
				Results.Add (t_Char59);
				ResultsV.Add(Regex.Match(Rest,"^((,[\\s\\t\\x00]*)?;+)").Value);}

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

			if (Regex.IsMatch(Rest,"^(,|:=)")){
				Results.Add (t_Char44);
				ResultsV.Add(Regex.Match(Rest,"^(,|:=)").Value);}

			if (Regex.IsMatch(Rest,"^((?i)UNDEF)")){
				Results.Add (t_UNDEF);
				ResultsV.Add(Regex.Match(Rest,"^((?i)UNDEF)").Value);}

			if (Regex.IsMatch(Rest,"^((?i)INCLUDE)")){
				Results.Add (t_INCLUDE);
				ResultsV.Add(Regex.Match(Rest,"^((?i)INCLUDE)").Value);}

			if (Regex.IsMatch(Rest,"^(\\{)")){
				Results.Add (t_Char123);
				ResultsV.Add(Regex.Match(Rest,"^(\\{)").Value);}

			if (Regex.IsMatch(Rest,"^(\\}([\\t\\s\\x00]*;+)?)")){
				Results.Add (t_Char125);
				ResultsV.Add(Regex.Match(Rest,"^(\\}([\\t\\s\\x00]*;+)?)").Value);}

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

			if (Regex.IsMatch(Rest,"^((?i)(EACH_SEC|IF_(ALT|ANYKEY|APO|BKSL|BKSP|BRACKL|BRACKR|CAL|CAR|COMMA|CTRL|CUD|CUL|CUR|CUU|DEL|END|EQUALS|ESC|HOME|INS|JOY4|LEFT|LOAD|MIDDLE|MINUS|MSTOP|PAUSE|PERIOD|PGDN|PGUP|PLUS|RIGHT|SEMIC|SLASH|SPACE|START|SZ|TAB|TAST|F(1[0-2]|[1-9])|[0-9A-Z])|LAYERS|MESSAGES|PANELS))")){
				Results.Add (t_event);
				ResultsV.Add(Regex.Match(Rest,"^((?i)(EACH_SEC|IF_(ALT|ANYKEY|APO|BKSL|BKSP|BRACKL|BRACKR|CAL|CAR|COMMA|CTRL|CUD|CUL|CUR|CUU|DEL|END|EQUALS|ESC|HOME|INS|JOY4|LEFT|LOAD|MIDDLE|MINUS|MSTOP|PAUSE|PERIOD|PGDN|PGUP|PLUS|RIGHT|SEMIC|SLASH|SPACE|START|SZ|TAB|TAST|F(1[0-2]|[1-9])|[0-9A-Z])|LAYERS|MESSAGES|PANELS))").Value);}

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

			if (Regex.IsMatch(Rest,"^((?i)(ACOS|COS|TAN|SIGN|INT|EXP|LOG10|LOG2|LOG))")){
				Results.Add (t_math);
				ResultsV.Add(Regex.Match(Rest,"^((?i)(ACOS|COS|TAN|SIGN|INT|EXP|LOG10|LOG2|LOG))").Value);}

			if (Regex.IsMatch(Rest,"^((?i)(FLOOR_DESCEND|CEIL_DESCEND|FLOOR_ASCEND|FLOOR_LIFTED|CEIL_ASCEND|CEIL_LIFTED|TRANSPARENT|CANDELABER|DIAPHANOUS|IMMATERIAL|IMPASSABLE|PORTCULLIS|AUTORANGE|CAREFULLY|CONDENSED|FLAG[1-8]|INVISIBLE|SENSITIVE|BERKELEY|CENTER_X|CENTER_Y|LIGHTMAP|PASSABLE|SAVE_ALL|CLUSTER|CURTAIN|FRAGILE|NO_CLIP|ONESHOT|REFRESH|VISIBLE|ABSPOS|BEHIND|GROUND|LIFTED|MASTER|NARROW|RELPOS|SHADOW|STICKY|FENCE|GHOST|LIBER|MOVED|SLOOP|BASE|BLUR|CLIP|FLAT|HARD|PLAY|SEEN|WIRE|FAR|SKY))")){
				Results.Add (t_flag);
				ResultsV.Add(Regex.Match(Rest,"^((?i)(FLOOR_DESCEND|CEIL_DESCEND|FLOOR_ASCEND|FLOOR_LIFTED|CEIL_ASCEND|CEIL_LIFTED|TRANSPARENT|CANDELABER|DIAPHANOUS|IMMATERIAL|IMPASSABLE|PORTCULLIS|AUTORANGE|CAREFULLY|CONDENSED|FLAG[1-8]|INVISIBLE|SENSITIVE|BERKELEY|CENTER_X|CENTER_Y|LIGHTMAP|PASSABLE|SAVE_ALL|CLUSTER|CURTAIN|FRAGILE|NO_CLIP|ONESHOT|REFRESH|VISIBLE|ABSPOS|BEHIND|GROUND|LIFTED|MASTER|NARROW|RELPOS|SHADOW|STICKY|FENCE|GHOST|LIBER|MOVED|SLOOP|BASE|BLUR|CLIP|FLAT|HARD|PLAY|SEEN|WIRE|FAR|SKY))").Value);}

			if (Regex.IsMatch(Rest,"^((?i)(ALBEDO|ANGLE|ASPEED|ATTACH|BELOW|BMAPS|BUTTON|CEIL_(ANGLE|OFFS_[X-Y]|TEX)|CYCLES|CYCLE|DEFAULT|DELAY|DIGITS|DISTANCE|DIST|EACH_CYCLE_C|EACH_CYCLE|FLAGS|FLOOR_(ANGLE|OFFS_[X-Y]|TEX)|FRAME|GENIUS|HBAR|HEIGHT|HSLIDER|IF_(ARISE|ARRIVED|DIVE|FAR|LEAVE|NEAR|RELEASE|TOUCH)|INDEX|LAYER|LEFT|LENGTH|MAP_COLOR|MASK|MAX|MIN|MIRROR|OFFSET_[X-Y]|OVLYS|PALFILE|PAN_MAP|PICTURE|POSITION|POS_[X-Y]|RADIANCE|RANGE|REL_(ANGLE|DIST)|RIGHT|SCALE_(XY|X|Y)|SCYCLES|SCYCLE|SDIST|SIDES|SIDE|SIZE_[X-Y]|SKILL[1-8]|SPEED|STRINGS|SVDIST|SVOL|TARGET_(MAP|X|Y)|TARGET|TEXTURE[1-4]|THING_HGT|TITLE|TOP|TOUCH|TYPE|VAL|VBAR|VSLIDER|VSPEED|WAYPOINT|WINDOW|[X-Z][1-2]|[X-Z]))")){
				Results.Add (t_property);
				ResultsV.Add(Regex.Match(Rest,"^((?i)(ALBEDO|ANGLE|ASPEED|ATTACH|BELOW|BMAPS|BUTTON|CEIL_(ANGLE|OFFS_[X-Y]|TEX)|CYCLES|CYCLE|DEFAULT|DELAY|DIGITS|DISTANCE|DIST|EACH_CYCLE_C|EACH_CYCLE|FLAGS|FLOOR_(ANGLE|OFFS_[X-Y]|TEX)|FRAME|GENIUS|HBAR|HEIGHT|HSLIDER|IF_(ARISE|ARRIVED|DIVE|FAR|LEAVE|NEAR|RELEASE|TOUCH)|INDEX|LAYER|LEFT|LENGTH|MAP_COLOR|MASK|MAX|MIN|MIRROR|OFFSET_[X-Y]|OVLYS|PALFILE|PAN_MAP|PICTURE|POSITION|POS_[X-Y]|RADIANCE|RANGE|REL_(ANGLE|DIST)|RIGHT|SCALE_(XY|X|Y)|SCYCLES|SCYCLE|SDIST|SIDES|SIDE|SIZE_[X-Y]|SKILL[1-8]|SPEED|STRINGS|SVDIST|SVOL|TARGET_(MAP|X|Y)|TARGET|TEXTURE[1-4]|THING_HGT|TITLE|TOP|TOUCH|TYPE|VAL|VBAR|VSLIDER|VSPEED|WAYPOINT|WINDOW|[X-Z][1-2]|[X-Z]))").Value);}

			if (Regex.IsMatch(Rest,"^((?i)(ACCEL|ADD_STRING|ADDT|ADD|AND|BEEP|BRANCH|BREAK|CALL|DROP|END|EXCLUSIVE|EXEC_RULES|EXIT|EXPLODE|FADE_PAL|FIND|FREEZE|GETMIDI|GOTO|IF_(ABOVE|BELOW|EQUAL|MAX|MIN|NEQUAL)|INKEY|INPORT|LEVEL|LIFT|LOAD_INFO|LOAD|LOCATE|MAP|MIDI_COM|NEXT_(MY_THERE|MY|THERE)|NOP|OUTPORT|PLACE|PLAY_(CD|DEMO|FLICFILE|FLIC|SONG_ONCE|SONG|SOUNDFILE|SOUND)|PRINTFILE|PRINT_(STRING|VALUE)|PUSH|RANDOMIZE|ROTATE|SAVE_(DEMO|INFO)|SCAN|SCREENSHOT|SETMIDI|SET_(ALL|INFO|SKILL|STRING)|SET|SHAKE|SHIFT|SHOOT|SKIP|STOP_(DEMO|FLIC|SOUND)|SUBT|SUB|TILT|TO_STRING|WAITT|WAIT_TICKS|WAIT))")){
				Results.Add (t_command);
				ResultsV.Add(Regex.Match(Rest,"^((?i)(ACCEL|ADD_STRING|ADDT|ADD|AND|BEEP|BRANCH|BREAK|CALL|DROP|END|EXCLUSIVE|EXEC_RULES|EXIT|EXPLODE|FADE_PAL|FIND|FREEZE|GETMIDI|GOTO|IF_(ABOVE|BELOW|EQUAL|MAX|MIN|NEQUAL)|INKEY|INPORT|LEVEL|LIFT|LOAD_INFO|LOAD|LOCATE|MAP|MIDI_COM|NEXT_(MY_THERE|MY|THERE)|NOP|OUTPORT|PLACE|PLAY_(CD|DEMO|FLICFILE|FLIC|SONG_ONCE|SONG|SOUNDFILE|SOUND)|PRINTFILE|PRINT_(STRING|VALUE)|PUSH|RANDOMIZE|ROTATE|SAVE_(DEMO|INFO)|SCAN|SCREENSHOT|SETMIDI|SET_(ALL|INFO|SKILL|STRING)|SET|SHAKE|SHIFT|SHOOT|SKIP|STOP_(DEMO|FLIC|SOUND)|SUBT|SUB|TILT|TO_STRING|WAITT|WAIT_TICKS|WAIT))").Value);}

			if (Regex.IsMatch(Rest,"^((?i)((EACH_TICK|EACH_SEC|PANELS|LAYERS|MESSAGES)\\.(1[0-6]|[1-9])))")){
				Results.Add (t_list);
				ResultsV.Add(Regex.Match(Rest,"^((?i)((EACH_TICK|EACH_SEC|PANELS|LAYERS|MESSAGES)\\.(1[0-6]|[1-9])))").Value);}

			if (Regex.IsMatch(Rest,"^((?i)(ACCELERATION|ACTIONS|ACTIVE_(NEXUS|OBJTICKS|TARGETS)|ACTOR_(CEIL_HGT|CLIMB|DIST|FLOOR_HGT|IMPACT_V[X-Z]|WIDTH)|ASPECT|BLUR_MODE|BOUNCE_V[X-Y]|CDAUDIO_VOL|CD_TRACK|CHANNEL_[0-7]|CHANNEL|CLIPPED|CLIPPING|COLOR_(ACTORS|BORDER|PLAYER|THINGS|WALLS)|DARK_DIST|DEBUG_MODE|DELTA_ANGLE|ERROR|FLIC_FRAME|FLOOR_MODE|FORCE_(AHEAD|ROT|STRAFE|TILT|UP)|FRAME_COLOR|FRICTION|HALF_PI|HIT_(DIST|MINDIST|X|Y)|IMPACT_V(ROT|[X-Z])|INERTIA|INV_DIST|JOYSTICK_[X-Y]|JOY_(4|SENSE)|KEY_(ALT|ANY|APO|BKSL|BKSP|BRACKL|BRACKR|CAL|CAR|COMMA|CTRL|CUD|CUL|CUR|CUU|DEL|END|ENTER|EQUALS|ESC|HOME|INS|JOY4|MINUS|PAUSE|PERIOD|PGDN|PGUP|PLUS|SEMIC|SENSE|SHIFT|SLASH|SPACE|SZ|TAB|F(1[0-2]|[1-9])|[A-Z0-9])|LIGHT_DIST|LOAD_MODE|MAP_(CENTER[X-Y]|(EDGE_[X-Y][1-2])|LAYER|MAX[X-Y]|MIN[X-Y]|MODE|OFFS[X-Y]|ROT|SCALE)|MAX_DIST|MICKEY_[X-Y]|MINV_DIST|MOTION_BLUR|MOUSE_(ANGLE|CALM|LEFT|MIDDLE|MODE|MOVING|RIGHT|SENSE|TIME|X|Y)|MOVE_(ANGLE|MODE)|MUSIC_VOL|MY_(X[1-2]|Y[1-2]|Z[1-2]|X|Y)|PANEL_LAYER|PI|PLAYER_(ANGLE|ARC|CLIMB|COS|DEPTH|HGT|LAST_[X-Y]|LIGHT|MSIN|SIN|SIZE|SPEED|TILT|VROT|V[X-Z]|WIDTH|[X-Z])|PSOUND_(TONE|VOL)|REAL_SPEED|REMOTE_[0-1]|RENDER_MODE|SCREEN_(HGT|WIDTH|X|Y)|SECS|SHIFT_SENSE|SHOOT_(ANGLE|FAC|RANGE|SECTOR|X|Y)|SKIP_FRAMES|SKY_OFFS_[X-Y]|SLOPE_(AHEAD|SIDE|X|Y)|SOUND_VOL|SPANS|STEPS|STR_LEN|TEXT_LAYER|THING_(DIST|WIDTH)|TICKS|TIME_(ACTIONS|CLIPPING|CORR|DRAW|FAC|FBUFFER|SLICES|TARGETS|VERTICES)|TOUCH_(DIST|MODE|STATE)|TWO_PI|WALK_(PERIOD|TIME)|WALK|WAVE_PERIOD|WAVE|PALANIM_DELAY))")){
				Results.Add (t_skill);
				ResultsV.Add(Regex.Match(Rest,"^((?i)(ACCELERATION|ACTIONS|ACTIVE_(NEXUS|OBJTICKS|TARGETS)|ACTOR_(CEIL_HGT|CLIMB|DIST|FLOOR_HGT|IMPACT_V[X-Z]|WIDTH)|ASPECT|BLUR_MODE|BOUNCE_V[X-Y]|CDAUDIO_VOL|CD_TRACK|CHANNEL_[0-7]|CHANNEL|CLIPPED|CLIPPING|COLOR_(ACTORS|BORDER|PLAYER|THINGS|WALLS)|DARK_DIST|DEBUG_MODE|DELTA_ANGLE|ERROR|FLIC_FRAME|FLOOR_MODE|FORCE_(AHEAD|ROT|STRAFE|TILT|UP)|FRAME_COLOR|FRICTION|HALF_PI|HIT_(DIST|MINDIST|X|Y)|IMPACT_V(ROT|[X-Z])|INERTIA|INV_DIST|JOYSTICK_[X-Y]|JOY_(4|SENSE)|KEY_(ALT|ANY|APO|BKSL|BKSP|BRACKL|BRACKR|CAL|CAR|COMMA|CTRL|CUD|CUL|CUR|CUU|DEL|END|ENTER|EQUALS|ESC|HOME|INS|JOY4|MINUS|PAUSE|PERIOD|PGDN|PGUP|PLUS|SEMIC|SENSE|SHIFT|SLASH|SPACE|SZ|TAB|F(1[0-2]|[1-9])|[A-Z0-9])|LIGHT_DIST|LOAD_MODE|MAP_(CENTER[X-Y]|(EDGE_[X-Y][1-2])|LAYER|MAX[X-Y]|MIN[X-Y]|MODE|OFFS[X-Y]|ROT|SCALE)|MAX_DIST|MICKEY_[X-Y]|MINV_DIST|MOTION_BLUR|MOUSE_(ANGLE|CALM|LEFT|MIDDLE|MODE|MOVING|RIGHT|SENSE|TIME|X|Y)|MOVE_(ANGLE|MODE)|MUSIC_VOL|MY_(X[1-2]|Y[1-2]|Z[1-2]|X|Y)|PANEL_LAYER|PI|PLAYER_(ANGLE|ARC|CLIMB|COS|DEPTH|HGT|LAST_[X-Y]|LIGHT|MSIN|SIN|SIZE|SPEED|TILT|VROT|V[X-Z]|WIDTH|[X-Z])|PSOUND_(TONE|VOL)|REAL_SPEED|REMOTE_[0-1]|RENDER_MODE|SCREEN_(HGT|WIDTH|X|Y)|SECS|SHIFT_SENSE|SHOOT_(ANGLE|FAC|RANGE|SECTOR|X|Y)|SKIP_FRAMES|SKY_OFFS_[X-Y]|SLOPE_(AHEAD|SIDE|X|Y)|SOUND_VOL|SPANS|STEPS|STR_LEN|TEXT_LAYER|THING_(DIST|WIDTH)|TICKS|TIME_(ACTIONS|CLIPPING|CORR|DRAW|FAC|FBUFFER|SLICES|TARGETS|VERTICES)|TOUCH_(DIST|MODE|STATE)|TWO_PI|WALK_(PERIOD|TIME)|WALK|WAVE_PERIOD|WAVE|PALANIM_DELAY))").Value);}

			if (Regex.IsMatch(Rest,"^((?i)(THERE|MY|HIT|TOUCH_TEXT|TOUCHED|TOUCH_TEX|TOUCH_REG|COMMAND_LINE))")){
				Results.Add (t_synonym);
				ResultsV.Add(Regex.Match(Rest,"^((?i)(THERE|MY|HIT|TOUCH_TEXT|TOUCHED|TOUCH_TEX|TOUCH_REG|COMMAND_LINE))").Value);}

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

			if (Regex.IsMatch(Rest,"^([0-9]+)")){
				Results.Add (t_integer);
				ResultsV.Add(Regex.Match(Rest,"^([0-9]+)").Value);}

			if (Regex.IsMatch(Rest,"^(([0-9]*\\.[0-9]+)|([0-9]+\\.[0-9]*))")){
				Results.Add (t_fixed);
				ResultsV.Add(Regex.Match(Rest,"^(([0-9]*\\.[0-9]+)|([0-9]+\\.[0-9]*))").Value);}

			if (Regex.IsMatch(Rest,"^([A-Za-z0-9_][A-Za-z0-9_]*(\\.[1-9][0-9]?)?)")){
				Results.Add (t_identifier);
				ResultsV.Add(Regex.Match(Rest,"^([A-Za-z0-9_][A-Za-z0-9_]*(\\.[1-9][0-9]?)?)").Value);}

			if (Regex.IsMatch(Rest,"^(<[\\s]?[^<;:\" ]+\\.[^<;:\" ]+[\\s]?>)")){
				Results.Add (t_file);
				ResultsV.Add(Regex.Match(Rest,"^(<[\\s]?[^<;:\" ]+\\.[^<;:\" ]+[\\s]?>)").Value);}

			if (Regex.IsMatch(Rest,"^(\"(.|[\\r\\n])*?\")")){
				Results.Add (t_string);
				ResultsV.Add(Regex.Match(Rest,"^(\"(.|[\\r\\n])*?\")").Value);}

			if (Regex.IsMatch(Rest,"^([\\r\\n\\t\\s\\x00]|(#.*(\\n|$))|(//.*(\\n|$))|(/\\*(.|[\\r\\n])*?\\*/))")){
				Results.Add (t_ignore);
				ResultsV.Add(Regex.Match(Rest,"^([\\r\\n\\t\\s\\x00]|(#.*(\\n|$))|(//.*(\\n|$))|(/\\*(.|[\\r\\n])*?\\*/))").Value);}

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
