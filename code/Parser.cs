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
            rList.Add(new Regex("\\G((?i)IFDEF)"));
            tList.Add(t_Char59);
            rList.Add(new Regex("\\G((,*[\\s\\t\\x00]*)?;)"));
            tList.Add(t_IFNDEF);
            rList.Add(new Regex("\\G((?i)IFNDEF)"));
            tList.Add(t_IFELSE);
            rList.Add(new Regex("\\G((?i)IFELSE)"));
            tList.Add(t_ENDIF);
            rList.Add(new Regex("\\G((?i)ENDIF)"));
            tList.Add(t_DEFINE);
            rList.Add(new Regex("\\G((?i)DEFINE)"));
            tList.Add(t_Char44);
            rList.Add(new Regex("\\G(,|:=)"));
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
            rList.Add(new Regex("\\G((?i)(FLOOR_DESCEND|CEIL_DESCEND|FLOOR_ASCEND|FLOOR_LIFTED|CEIL_ASCEND|CEIL_LIFTED|TRANSPARENT|CANDELABER|DIAPHANOUS|IMMATERIAL|IMPASSABLE|PORTCULLIS|AUTORANGE|CAREFULLY|CONDENSED|FLAG[1-8]|INVISIBLE|SENSITIVE|BERKELEY|CENTER_X|CENTER_Y|LIGHTMAP|PASSABLE|SAVE_ALL|CLUSTER|CURTAIN|FRAGILE|NO_CLIP|ONESHOT|REFRESH|VISIBLE|ABSPOS|BEHIND|GROUND|LIFTED|MASTER|NARROW|RELPOS|SHADOW|STICKY|FENCE|GHOST|LIBER|MOVED|SLOOP|BASE|BLUR|CLIP|FLAT|HARD|PLAY|SEEN|WIRE|FAR|SKY))"));
            tList.Add(t_property);
            rList.Add(new Regex("\\G((?i)(ALBEDO|ANGLE|ASPEED|ATTACH|BELOW|BMAPS|BUTTON|CEIL_(ANGLE|OFFS_[X-Y]|TEX)|CYCLES|CYCLE|DEFAULT|DELAY|DIGITS|DISTANCE|DIST|EACH_CYCLE_(C|F)|EACH_CYCLE|FLAGS|FLOOR_(ANGLE|OFFS_[X-Y]|TEX)|FOOT_HGT|FRAME|GENIUS|HBAR|HEIGHT|HSLIDER|IF_(ARISE|ARRIVED|DIVE|FAR|LEAVE|NEAR|RELEASE|TOUCH)|INDEX|LAYER|LEFT|LENGTH|MAP_COLOR|MASK|MAX|MIN|MIRROR|OFFSET_[X-Y]|OVLYS|PALFILE|PAN_MAP|PICTURE|POSITION|POS_[X-Y]|RADIANCE|RANGE|REL_(ANGLE|DIST)|RIGHT|SCALE_(XY|X|Y)|SCYCLES|SCYCLE|SCALE|SDIST|SIDES|SIDE|SIZE_[X-Y]|SKILL[1-8]|SPEED|STRINGS|SVDIST|SVOL|TARGET_(MAP|X|Y)|TARGET|TEXTURE[1-4]|THING_HGT|TITLE|TOP|TOUCH|TYPE|VAL|VBAR|VSLIDER|VSPEED|WAYPOINT|WINDOW|[X-Z][1-2]|[X-Z]))"));
            tList.Add(t_command);
            rList.Add(new Regex("\\G((?i)(ACCEL|ADD_STRING|ADDT|ADD|AND|BEEP|BRANCH|BREAK|CALL|DROP|END|EXCLUSIVE|EXEC_RULES|EXIT|EXPLODE|FADE_PAL|FIND|FREEZE|GETMIDI|GOTO|IF_(ABOVE|BELOW|EQUAL|MAX|MIN|NEQUAL)|INKEY|INPORT|LEVEL|LIFT|LOAD_INFO|LOAD|LOCATE|MAP|MIDI_COM|NEXT_(MY_THERE|MY|THERE)|NOP|OUTPORT|PLACE|PLAY_(CD|DEMO|FLICFILE|FLIC|SONG_ONCE|SONG|SOUNDFILE|SOUND)|PRINTFILE|PRINT_(STRING|VALUE)|PUSH|RANDOMIZE|ROTATE|SAVE_(DEMO|INFO)|SCAN|SCREENSHOT|SETMIDI|SET_(ALL|INFO|SKILL|STRING)|SET|SHAKE|SHIFT|SHOOT|SKIP|STOP_(DEMO|FLIC|SOUND)|SUBT|SUB|TILT|TO_STRING|WAITT|WAIT_TICKS|WAIT))"));
            tList.Add(t_list);
            rList.Add(new Regex("\\G((?i)((EACH_TICK|EACH_SEC|PANELS|LAYERS|MESSAGES)\\.(1[0-6]|[1-9])))"));
            tList.Add(t_skill);
            rList.Add(new Regex("\\G((?i)(ACCELERATION|ACTIONS|ACTIVE_(NEXUS|OBJTICKS|TARGETS)|ACTOR_(CEIL_HGT|CLIMB|DIST|FLOOR_HGT|IMPACT_V[X-Z]|WIDTH)|ASPECT|BLUR_MODE|BOUNCE_V[X-Y]|CDAUDIO_VOL|CD_TRACK|CHANNEL_[0-7]|CHANNEL|CLIPPED|CLIPPING|COLOR_(ACTORS|BORDER|PLAYER|THINGS|WALLS)|DARK_DIST|DEBUG_MODE|DELTA_ANGLE|ERROR|FLIC_FRAME|FLOOR_MODE|FORCE_(AHEAD|ROT|STRAFE|TILT|UP)|FRAME_COLOR|FRICTION|HALF_PI|HIT_(DIST|MINDIST|X|Y)|IMPACT_V(ROT|[X-Z])|INERTIA|INV_DIST|JOYSTICK_[X-Y]|JOY_(4|SENSE)|KEY_(ALT|ANY|APO|BKSL|BKSP|BRACKL|BRACKR|CAL|CAR|COMMA|CTRL|CUD|CUL|CUR|CUU|DEL|END|ENTER|EQUALS|ESC|HOME|INS|JOY4|MINUS|PAUSE|PERIOD|PGDN|PGUP|PLUS|SEMIC|SENSE|SHIFT|SLASH|SPACE|SZ|TAB|F(1[0-2]|[1-9])|[A-Z0-9])|LIGHT_DIST|LOAD_MODE|MAP_(CENTER[X-Y]|(EDGE_[X-Y][1-2])|LAYER|MAX[X-Y]|MIN[X-Y]|MODE|OFFS[X-Y]|ROT|SCALE)|MAX_DIST|MICKEY_[X-Y]|MINV_DIST|MOTION_BLUR|MOUSE_(ANGLE|CALM|LEFT|MIDDLE|MODE|MOVING|RIGHT|SENSE|TIME|X|Y)|MOVE_(ANGLE|MODE)|MUSIC_VOL|MY_(X[1-2]|Y[1-2]|Z[1-2]|X|Y)|PANEL_LAYER|PI|PLAYER_(ANGLE|ARC|CLIMB|COS|DEPTH|HGT|LAST_[X-Y]|LIGHT|MSIN|SIN|SIZE|SPEED|TILT|VROT|V[X-Z]|WIDTH|[X-Z])|PSOUND_(TONE|VOL)|REAL_SPEED|REMOTE_[0-1]|RENDER_MODE|SCREEN_(HGT|WIDTH|X|Y)|SECS|SHIFT_SENSE|SHOOT_(ANGLE|FAC|RANGE|SECTOR|X|Y)|SKIP_FRAMES|SKY_OFFS_[X-Y]|SLOPE_(AHEAD|SIDE|X|Y)|SOUND_VOL|SPANS|STEPS|STR_LEN|TEXT_LAYER|THING_(DIST|WIDTH)|TICKS|TIME_(ACTIONS|CLIPPING|CORR|DRAW|FAC|FBUFFER|SLICES|TARGETS|VERTICES)|TOUCH_(DIST|MODE|RANGE|STATE)|TWO_PI|WALK_(PERIOD|TIME)|WALK|WAVE_PERIOD|WAVE|PALANIM_DELAY))"));
            tList.Add(t_synonym);
            rList.Add(new Regex("\\G((?i)(THERE|MY|HIT|TOUCH_TEXT|TOUCHED|TOUCH_TEX|TOUCH_REG|COMMAND_LINE))"));
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
            tList.Add(t_integer);
            rList.Add(new Regex("\\G([0-9]+)"));
            tList.Add(t_fixed);
            rList.Add(new Regex("\\G(([0-9]*\\.[0-9]+)|([0-9]+\\.[0-9]*))"));
            tList.Add(t_identifier);
            rList.Add(new Regex("\\G([A-Za-z0-9_][A-Za-z0-9_]*(\\.[1-9][0-9]?)?)"));
            tList.Add(t_file);
            rList.Add(new Regex("\\G(<[\\s]?[^<;:\" ]+\\.[^<;:\" ]+[\\s]?>)"));
            tList.Add(t_string);
            rList.Add(new Regex("\\G(\"(\\\\\"|.|[\\r\\n])*?\")"));
            tList.Add(t_ignore);
            rList.Add(new Regex("\\G([\\r\\n\\t\\s\\x00]|(#.*(\\n|$))|(//.*(\\n|$))|(/\\*(.|[\\r\\n])*?\\*/))"));
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
         yyval = "";
         
       break;
							case    5 : 
         yyval = Sections.AddDummySection(yyv[yysp-0]);
         
       break;
							case    6 : 
         yyval = Sections.AddGlobalSection(yyv[yysp-0]);
         
       break;
							case    7 : 
         yyval = yyv[yysp-0];
         
       break;
							case    8 : 
         yyval = yyv[yysp-0];
         
       break;
							case    9 : 
         yyval = Sections.AddActionSection(yyv[yysp-0]);
         
       break;
							case   10 : 
         yyval = Sections.AddObjectSection(yyv[yysp-0]);
         
       break;
							case   11 : 
         yyval = Sections.AddDefineSection(yyv[yysp-0]);
         
       break;
							case   12 : 
         yyval = Sections.AddAssetSection(yyv[yysp-0]);
         
       break;
							case   13 : 
         //yyval = yyv[yysp-4] + yyv[yysp-3] + yyv[yysp-2] + yyv[yysp-1] + yyv[yysp-0];
         yyval = Sections.CreatePreProcIfCondition(yyv[yysp-3], yyv[yysp-1]);
         
       break;
							case   14 : 
         //yyval = yyv[yysp-4] + yyv[yysp-3] + yyv[yysp-2] + yyv[yysp-1] + yyv[yysp-0];
         yyval = Sections.CreatePreProcIfNotCondition(yyv[yysp-3], yyv[yysp-1]);
         
       break;
							case   15 : 
         //yyval = yyv[yysp-4] + yyv[yysp-3] + yyv[yysp-2] + yyv[yysp-1] + yyv[yysp-0];
         yyval = Sections.CreatePreProcElseCondition(yyv[yysp-4], yyv[yysp-1]);
         
       break;
							case   16 : 
         //yyval = yyv[yysp-1] + yyv[yysp-0];
         yyval = Sections.CreatePreProcEndCondition(yyv[yysp-1]);
         
       break;
							case   17 : 
         //yyval = yyv[yysp-2] + yyv[yysp-1] + yyv[yysp-0];
         yyval = Actions.CreatePreProcIfCondition(yyv[yysp-1], yyv[yysp-0]);
         
       break;
							case   18 : 
         //yyval = yyv[yysp-2] + yyv[yysp-1] + yyv[yysp-0];
         yyval = Actions.CreatePreProcIfNotCondition(yyv[yysp-1], yyv[yysp-0]);
         
       break;
							case   19 : 
         //yyval = yyv[yysp-3] + yyv[yysp-2] + yyv[yysp-1] + yyv[yysp-0];
         yyval = Actions.CreatePreProcElseCondition(yyv[yysp-3], yyv[yysp-1]);
         
       break;
							case   20 : 
         //yyval = yyv[yysp-1] + yyv[yysp-0];
         yyval = Actions.CreatePreProcEndCondition(yyv[yysp-1]);
         
       break;
							case   21 : 
         //yyval = yyv[yysp-4] + yyv[yysp-3] + yyv[yysp-2] + yyv[yysp-1] + yyv[yysp-0];
         yyval = Objects.CreatePreProcIfCondition(yyv[yysp-3], yyv[yysp-1]);
         
       break;
							case   22 : 
         //yyval = yyv[yysp-4] + yyv[yysp-3] + yyv[yysp-2] + yyv[yysp-1] + yyv[yysp-0];
         yyval = Objects.CreatePreProcIfNotCondition(yyv[yysp-3], yyv[yysp-1]);
         
       break;
							case   23 : 
         //yyval = yyv[yysp-4] + yyv[yysp-3] + yyv[yysp-2] + yyv[yysp-1] + yyv[yysp-0];
         yyval = Objects.CreatePreProcElseCondition(yyv[yysp-4], yyv[yysp-1]);
         
       break;
							case   24 : 
         //yyval = yyv[yysp-1] + yyv[yysp-0];
         yyval = Objects.CreatePreProcEndCondition(yyv[yysp-1]);
         
       break;
							case   25 : 
         //yyval = yyv[yysp-4] + yyv[yysp-3] + yyv[yysp-2] + yyv[yysp-1] + yyv[yysp-0];
         yyval = Defines.AddTransform(yyv[yysp-3]);
         
       break;
							case   26 : 
         //yyval = yyv[yysp-2] + yyv[yysp-1] + yyv[yysp-0];
         yyval = Defines.AddDefine(yyv[yysp-1]);
         
       break;
							case   27 : 
         //yyval = yyv[yysp-2] + yyv[yysp-1] + yyv[yysp-0];
         yyval = Defines.RemoveDefine(yyv[yysp-1]);
         
       break;
							case   28 : 
         yyval = Formatter.FormatPreprocessor(yyv[yysp-0]);
         
       break;
							case   29 : 
         yyval = Formatter.FormatPreprocessor(yyv[yysp-0]);
         
       break;
							case   30 : 
         //yyval = yyv[yysp-0];
         yyval = "";
         Defines.AddStringDefine(yyv[yysp-0]);
         
       break;
							case   31 : 
         //yyval = yyv[yysp-0];
         yyval = "";
         Defines.AddListDefine(yyv[yysp-0]);
         
       break;
							case   32 : 
         //yyval = yyv[yysp-0];
         yyval = "";
         Defines.AddFileDefine(yyv[yysp-0]);
         
       break;
							case   33 : 
         //yyval = yyv[yysp-0];
         yyval = "";
         Defines.AddNumberDefine(yyv[yysp-0]);
         
       break;
							case   34 : 
         //yyval = yyv[yysp-0];
         yyval = "";
         Defines.AddKeywordDefine(yyv[yysp-0]);
         
       break;
							case   35 : 
         //yyval = yyv[yysp-2] + yyv[yysp-1] + yyv[yysp-0];
         yyval = Include.Process(yyv[yysp-1]);
         
       break;
							case   36 : 
         //yyval = yyv[yysp-3] + yyv[yysp-2] + yyv[yysp-1] + yyv[yysp-0];
         yyval = Globals.AddEvent(yyv[yysp-3]);
         
       break;
							case   37 : 
         //yyval = yyv[yysp-3] + yyv[yysp-2] + yyv[yysp-1] + yyv[yysp-0];
         yyval = Globals.AddGlobal(yyv[yysp-3], yyv[yysp-1]);
         
       break;
							case   38 : 
         yyval = Formatter.FormatGlobal(yyv[yysp-0]);
         
       break;
							case   39 : 
         yyval = Formatter.FormatGlobal(yyv[yysp-0]);
         
       break;
							case   40 : 
         yyval = Formatter.FormatGlobal(yyv[yysp-0]);
         
       break;
							case   41 : 
         yyval = Formatter.FormatTargetSkill(yyv[yysp-0]);
         
       break;
							case   42 : 
         yyval = Formatter.FormatEvent(yyv[yysp-0]);
         
       break;
							case   43 : 
         yyval = Formatter.FormatEvent(yyv[yysp-0]);
         
       break;
							case   44 : 
         //yyval = yyv[yysp-1];
         yyval = "";
         Globals.AddParameter(yyv[yysp-1]);
         
       break;
							case   45 : 
         //yyval = yyv[yysp-2] + yyv[yysp-1] + yyv[yysp-0];
         yyval = "";
         Globals.AddParameter(yyv[yysp-2]);
         
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
         yyval = yyv[yysp-0];
         
       break;
							case   50 : 
         //yyval = yyv[yysp-6] + yyv[yysp-5] + yyv[yysp-4] + yyv[yysp-3] + yyv[yysp-2] + yyv[yysp-1] + yyv[yysp-0];
         yyval = Assets.AddAsset(yyv[yysp-6], yyv[yysp-5], yyv[yysp-3]);
         
       break;
							case   51 : 
         //yyval = yyv[yysp-2] + yyv[yysp-1] + yyv[yysp-0];
         yyval = "";
         Assets.AddParameter(yyv[yysp-2]);
         
       break;
							case   52 : 
         //yyval = yyv[yysp-0];
         yyval = "";
         Assets.AddParameter(yyv[yysp-0]);
         
       break;
							case   53 : 
         yyval = "";
         
       break;
							case   54 : 
         yyval = yyv[yysp-0];
         
       break;
							case   55 : 
         //yyval = yyv[yysp-5] + yyv[yysp-4] + yyv[yysp-3] + yyv[yysp-2] + yyv[yysp-1] + yyv[yysp-0];
         yyval = Objects.AddObject(yyv[yysp-5], yyv[yysp-4], yyv[yysp-1]);
         
       break;
							case   56 : 
         //yyval = yyv[yysp-4] + yyv[yysp-3] + yyv[yysp-2] + yyv[yysp-1] + yyv[yysp-0];
         yyval = Objects.AddStringObject(yyv[yysp-4], yyv[yysp-3], yyv[yysp-1]);
         
       break;
							case   57 : 
         //yyval = yyv[yysp-2] + yyv[yysp-1] + yyv[yysp-0];
         yyval = Objects.AddObject(yyv[yysp-2], yyv[yysp-1]);
         
       break;
							case   58 : 
         yyval = yyv[yysp-0];
         
       break;
							case   59 : 
         yyval = yyv[yysp-0];
         
       break;
							case   60 : 
         yyval = yyv[yysp-0];
         
       break;
							case   61 : 
         //yyval = yyv[yysp-2] + yyv[yysp-1] + yyv[yysp-0];
         yyval = yyv[yysp-2] + yyv[yysp-0];
         
       break;
							case   62 : 
         yyval = yyv[yysp-1] + yyv[yysp-0];
         
       break;
							case   63 : 
         yyval = "";
         
       break;
							case   64 : 
         //yyval = yyv[yysp-2] + yyv[yysp-1] + yyv[yysp-0];
         yyval = Objects.CreateProperty(yyv[yysp-2]);
         
       break;
							case   65 : 
         Objects.AddPropertyValue(yyv[yysp-1]);
         yyval = "";
         
       break;
							case   66 : 
         Objects.AddPropertyValue(yyv[yysp-2]);
         yyval = "";
         
       break;
							case   67 : 
         yyval = yyv[yysp-0];
         
       break;
							case   68 : 
         yyval = yyv[yysp-0];
         
       break;
							case   69 : 
         yyval = yyv[yysp-0];
         
       break;
							case   70 : 
         yyval = yyv[yysp-0];
         
       break;
							case   71 : 
         //yyval = yyv[yysp-5] + yyv[yysp-4] + yyv[yysp-3] + yyv[yysp-2] + yyv[yysp-1] + yyv[yysp-0];
         yyval = Actions.AddAction(yyv[yysp-4], yyv[yysp-1]);
         
       break;
							case   72 : 
         yyval = yyv[yysp-0];
         
       break;
							case   73 : 
         //yyval = yyv[yysp-2] + yyv[yysp-1] + yyv[yysp-0];
         yyval = Actions.CreateMarker(yyv[yysp-2], yyv[yysp-0]);
         
       break;
							case   74 : 
         //yyval = yyv[yysp-3] + yyv[yysp-2] + yyv[yysp-1] + yyv[yysp-0];
         yyval = yyv[yysp-2] + yyv[yysp-0];
         
       break;
							case   75 : 
         //yyval = yyv[yysp-2] + yyv[yysp-1] + yyv[yysp-0];
         yyval = Actions.CreateMarker(yyv[yysp-2], yyv[yysp-0]);
         
       break;
							case   76 : 
         yyval = yyv[yysp-1] + yyv[yysp-0];
         
       break;
							case   77 : 
         yyval = yyv[yysp-1] + yyv[yysp-0];
         
       break;
							case   78 : 
         yyval = yyv[yysp-1] + yyv[yysp-0];
         
       break;
							case   79 : 
         yyval = "";
         
       break;
							case   80 : 
         yyval = Defines.RemoveDefine(yyv[yysp-1]);
         
       break;
							case   81 : 
         //yyval = yyv[yysp-3] + yyv[yysp-2] + yyv[yysp-1];
         yyval = Actions.CreateInstruction(yyv[yysp-3]);
         
       break;
							case   82 : 
         //Capture and discard bogus code
         yyval = Actions.CreateInvalidInstruction(yyv[yysp-3]);
         
       break;
							case   83 : 
         //yyval = yyv[yysp-1];
         yyval = Actions.CreateInstruction(yyv[yysp-1]);
         
       break;
							case   84 : 
         yyval = yyv[yysp-1];
         
       break;
							case   85 : 
         yyval = "";
         
       break;
							case   86 : 
         //yyval = yyv[yysp-1] + yyv[yysp-0];
         yyval = "";
         Actions.AddInstructionParameter(yyv[yysp-1]);
         
       break;
							case   87 : 
         yyval = yyv[yysp-2] + yyv[yysp-1] + yyv[yysp-0];
         yyval = "";
         Actions.AddInstructionParameter(yyv[yysp-2]);
         
       break;
							case   88 : 
         yyval = yyv[yysp-0];
         
       break;
							case   89 : 
         yyval = yyv[yysp-0];
         
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
         yyval = yyv[yysp-0];
         
       break;
							case   94 : 
         yyval = "";
         
       break;
							case   95 : 
         yyval = yyv[yysp-0];
         
       break;
							case   96 : 
         yyval = yyv[yysp-2] + " || " + yyv[yysp-0];
         
       break;
							case   97 : 
         yyval = yyv[yysp-0];
         
       break;
							case   98 : 
         yyval = yyv[yysp-2] + " && " + yyv[yysp-0];
         
       break;
							case   99 : 
         yyval = yyv[yysp-0];
         
       break;
							case  100 : 
         yyval = yyv[yysp-2] + " | " + yyv[yysp-0];
         
       break;
							case  101 : 
         yyval = yyv[yysp-0];
         
       break;
							case  102 : 
         yyval = yyv[yysp-2] + " ^ " + yyv[yysp-0];
         
       break;
							case  103 : 
         yyval = yyv[yysp-0];
         
       break;
							case  104 : 
         yyval = yyv[yysp-2] + " & " + yyv[yysp-0];
         
       break;
							case  105 : 
         yyval = yyv[yysp-0];
         
       break;
							case  106 : 
         yyval = yyv[yysp-0];
         
       break;
							case  107 : 
         yyval = yyv[yysp-2] + yyv[yysp-1] + yyv[yysp-0];
         
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
         yyval = yyv[yysp-1] + yyv[yysp-0];
         
       break;
							case  116 : 
         yyval = Formatter.FormatMath(yyv[yysp-3]) + "(" + yyv[yysp-1] + ")";
         
       break;
							case  117 : 
         yyval = "(" + yyv[yysp-1] + ")";
         
       break;
							case  118 : 
         yyval = yyv[yysp-0];
         
       break;
							case  119 : 
         yyval = Formatter.FormatTargetSkill(yyv[yysp-0]);
         
       break;
							case  120 : 
         yyval = " != ";
         
       break;
							case  121 : 
         yyval = " == ";
         
       break;
							case  122 : 
         yyval = " < ";
         
       break;
							case  123 : 
         yyval = " <= ";
         
       break;
							case  124 : 
         yyval = " > ";
         
       break;
							case  125 : 
         yyval = " >= ";
         
       break;
							case  126 : 
         yyval = " + ";
         
       break;
							case  127 : 
         yyval = " - ";
         
       break;
							case  128 : 
         yyval = " % ";
         
       break;
							case  129 : 
         yyval = " * ";
         
       break;
							case  130 : 
         yyval = " / ";
         
       break;
							case  131 : 
         yyval = "!";
         
       break;
							case  132 : 
         yyval = "+";
         
       break;
							case  133 : 
         yyval = "-";
         
       break;
							case  134 : 
         //yyval = yyv[yysp-1] + yyv[yysp-0];
         yyval = Actions.CreateExpression(yyv[yysp-0]);
         
       break;
							case  135 : 
         //yyval = yyv[yysp-3] + yyv[yysp-2] + yyv[yysp-1] + yyv[yysp-0];
         yyval = Actions.CreateExpression(Formatter.FormatExpression(yyv[yysp-2], yyv[yysp-1], yyv[yysp-0]));
         
       break;
							case  136 : 
         //yyval = yyv[yysp-2] + yyv[yysp-1] + yyv[yysp-0];
         yyval = Actions.CreateExpression(Formatter.FormatExpression(yyv[yysp-2], yyv[yysp-1], yyv[yysp-0]));
         
       break;
							case  137 : 
         yyval = " *= ";
         
       break;
							case  138 : 
         yyval = " += ";
         
       break;
							case  139 : 
         yyval = " -= ";
         
       break;
							case  140 : 
         yyval = " /= ";
         
       break;
							case  141 : 
         yyval = " = ";
         
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
         //yyval = yyv[yysp-3] + yyv[yysp-2] + yyv[yysp-1] + yyv[yysp-0];
         yyval = Actions.CreateElseCondition(yyv[yysp-1]);
         
       break;
							case  146 : 
         //yyval = yyv[yysp-4] + yyv[yysp-3] + yyv[yysp-2] + yyv[yysp-1] + yyv[yysp-0];
         yyval = Actions.CreateIfCondition(yyv[yysp-3], yyv[yysp-1]);
         
       break;
							case  147 : 
         //yyval = yyv[yysp-4] + yyv[yysp-3] + yyv[yysp-2] + yyv[yysp-1] + yyv[yysp-0];
         yyval = Actions.CreateWhileCondition(yyv[yysp-3], yyv[yysp-1]);
         
       break;
							case  148 : 
         yyval = yyv[yysp-2] + "." + yyv[yysp-0];
         
       break;
							case  149 : 
         yyval = yyv[yysp-2] + "." + yyv[yysp-0];
         
       break;
							case  150 : 
         yyval = yyv[yysp-0];
         
       break;
							case  151 : 
         yyval = Formatter.FormatProperty(yyv[yysp-0]);
         
       break;
							case  152 : 
         yyval = Formatter.FormatProperty(yyv[yysp-0]);
         
       break;
							case  153 : 
         yyval = Formatter.FormatProperty(yyv[yysp-0]);
         
       break;
							case  154 : 
         yyval = Formatter.FormatProperty(yyv[yysp-0]);
         
       break;
							case  155 : 
         yyval = Formatter.FormatProperty(yyv[yysp-0]);
         
       break;
							case  156 : 
         yyval = Formatter.FormatProperty(yyv[yysp-0]);
         
       break;
							case  157 : 
         yyval = Formatter.FormatProperty(yyv[yysp-0]);
         
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
         yyval = Formatter.FormatCommand(yyv[yysp-0]);
         
       break;
							case  166 : 
         yyval = Formatter.FormatCommand(yyv[yysp-0]);
         
       break;
							case  167 : 
         yyval = Formatter.FormatCommand(yyv[yysp-0]);
         
       break;
							case  168 : 
         yyval = Formatter.FormatCommand(yyv[yysp-0]);
         
       break;
							case  169 : 
         yyval = Formatter.FormatCommand(yyv[yysp-0]);
         
       break;
							case  170 : 
         yyval = Formatter.FormatNumber(yyv[yysp-0]);
         
       break;
							case  171 : 
         yyval = Formatter.FormatNumber(yyv[yysp-0]);
         
       break;
							case  172 : 
         yyval = yyv[yysp-0];
         
       break;
							case  173 : 
         yyval = yyv[yysp-0];
         
       break;
							case  174 : 
         yyval = yyv[yysp-1] + yyv[yysp-0];
         
       break;
							case  175 : 
         yyval = yyv[yysp-0];
         
       break;
							case  176 : 
         yyval = yyv[yysp-1] + yyv[yysp-0];
         
       break;
							case  177 : 
         yyval = yyv[yysp-0];
         
       break;
							case  178 : 
         yyval = Formatter.FormatIdentifier(yyv[yysp-0]);
         
       break;
							case  179 : 
         yyval = Formatter.FormatList(yyv[yysp-0]);
         
       break;
							case  180 : 
         yyval = Formatter.FormatSkill(yyv[yysp-0]);
         
       break;
							case  181 : 
         yyval = Formatter.FormatSkill(yyv[yysp-0]);
         
       break;
							case  182 : 
         yyval = Formatter.FormatSkill(yyv[yysp-0]);
         
       break;
							case  183 : 
         yyval = Formatter.FormatIdentifier(yyv[yysp-0]);
         
       break;
							case  184 : 
         yyval = Formatter.FormatIdentifier(yyv[yysp-0]);
         
       break;
							case  185 : 
         yyval = Formatter.FormatEvent(yyv[yysp-0]);
         
       break;
							case  186 : 
         yyval = Formatter.FormatGlobal(yyv[yysp-0]);
         
       break;
							case  187 : 
         yyval = Formatter.FormatIdentifier(yyv[yysp-0]);
         
       break;
							case  188 : 
         yyval = Formatter.FormatIdentifier(yyv[yysp-0]);
         
       break;
							case  189 : 
         yyval = Formatter.FormatIdentifier(yyv[yysp-0]);
         
       break;
							case  190 : 
         yyval = Formatter.FormatIdentifier(yyv[yysp-0]);
         
       break;
							case  191 : 
         yyval = Formatter.FormatEvent(yyv[yysp-0]);
         
       break;
							case  192 : 
         yyval = Formatter.FormatIdentifier(yyv[yysp-0]);
         
       break;
							case  193 : 
         yyval = Formatter.FormatIdentifier(yyv[yysp-0]);
         
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
         yyval = Formatter.FormatGlobal(yyv[yysp-0]);
         
       break;
							case  199 : 
         yyval = Formatter.FormatSkill(yyv[yysp-0]);
         
       break;
							case  200 : 
         yyval = Formatter.FormatSkill(yyv[yysp-0]);
         
       break;
							case  201 : 
         yyval = Formatter.FormatGlobal(yyv[yysp-0]);
         
       break;
							case  202 : 
         yyval = Formatter.FormatSkill(yyv[yysp-0]);
         
       break;
							case  203 : 
         yyval = Formatter.FormatGlobal(yyv[yysp-0]);
         
       break;
							case  204 : 
         yyval = Formatter.FormatNull();
         
       break;
							case  205 : 
         yyval = yyv[yysp-0];
         
       break;
							case  206 : 
         yyval = yyv[yysp-0];
         
       break;
							case  207 : 
         yyval = yyv[yysp-0];
         
       break;
							case  208 : 
         yyval = yyv[yysp-0];
         
       break;
							case  209 : 
         yyval = yyv[yysp-0];
         
       break;
							case  210 : 
         yyval = yyv[yysp-0];
         
       break;
							case  211 : 
         yyval = yyv[yysp-0];
         
       break;
							case  212 : 
         yyval = yyv[yysp-0]; //TODO: FormatIdentifier?
         
       break;
							case  213 : 
         yyval = Formatter.FormatFile(yyv[yysp-0]);
         
       break;
							case  214 : 
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

					int yynacts   = 2399;
					int yyngotos  = 752;
					int yynstates = 335;
					int yynrules  = 214;
					yya = new YYARec[yynacts+1];  int yyac = 1;
					yyg = new YYARec[yyngotos+1]; int yygc = 1;
					yyr = new YYRRec[yynrules+1]; int yyrc = 1;

					yya[yyac] = new YYARec(257,18);yyac++; 
					yya[yyac] = new YYARec(258,19);yyac++; 
					yya[yyac] = new YYARec(259,20);yyac++; 
					yya[yyac] = new YYARec(262,21);yyac++; 
					yya[yyac] = new YYARec(264,22);yyac++; 
					yya[yyac] = new YYARec(265,23);yyac++; 
					yya[yyac] = new YYARec(267,24);yyac++; 
					yya[yyac] = new YYARec(299,25);yyac++; 
					yya[yyac] = new YYARec(300,26);yyac++; 
					yya[yyac] = new YYARec(301,27);yyac++; 
					yya[yyac] = new YYARec(302,28);yyac++; 
					yya[yyac] = new YYARec(303,29);yyac++; 
					yya[yyac] = new YYARec(309,30);yyac++; 
					yya[yyac] = new YYARec(311,31);yyac++; 
					yya[yyac] = new YYARec(312,32);yyac++; 
					yya[yyac] = new YYARec(313,33);yyac++; 
					yya[yyac] = new YYARec(315,34);yyac++; 
					yya[yyac] = new YYARec(317,35);yyac++; 
					yya[yyac] = new YYARec(319,36);yyac++; 
					yya[yyac] = new YYARec(0,-3 );yyac++; 
					yya[yyac] = new YYARec(298,42);yyac++; 
					yya[yyac] = new YYARec(299,43);yyac++; 
					yya[yyac] = new YYARec(300,44);yyac++; 
					yya[yyac] = new YYARec(301,45);yyac++; 
					yya[yyac] = new YYARec(302,46);yyac++; 
					yya[yyac] = new YYARec(303,47);yyac++; 
					yya[yyac] = new YYARec(304,48);yyac++; 
					yya[yyac] = new YYARec(305,49);yyac++; 
					yya[yyac] = new YYARec(306,50);yyac++; 
					yya[yyac] = new YYARec(307,51);yyac++; 
					yya[yyac] = new YYARec(309,52);yyac++; 
					yya[yyac] = new YYARec(310,53);yyac++; 
					yya[yyac] = new YYARec(311,54);yyac++; 
					yya[yyac] = new YYARec(312,55);yyac++; 
					yya[yyac] = new YYARec(313,56);yyac++; 
					yya[yyac] = new YYARec(314,57);yyac++; 
					yya[yyac] = new YYARec(315,58);yyac++; 
					yya[yyac] = new YYARec(316,59);yyac++; 
					yya[yyac] = new YYARec(317,60);yyac++; 
					yya[yyac] = new YYARec(318,61);yyac++; 
					yya[yyac] = new YYARec(319,62);yyac++; 
					yya[yyac] = new YYARec(320,63);yyac++; 
					yya[yyac] = new YYARec(323,64);yyac++; 
					yya[yyac] = new YYARec(298,42);yyac++; 
					yya[yyac] = new YYARec(299,43);yyac++; 
					yya[yyac] = new YYARec(300,44);yyac++; 
					yya[yyac] = new YYARec(301,45);yyac++; 
					yya[yyac] = new YYARec(302,46);yyac++; 
					yya[yyac] = new YYARec(303,47);yyac++; 
					yya[yyac] = new YYARec(304,48);yyac++; 
					yya[yyac] = new YYARec(305,49);yyac++; 
					yya[yyac] = new YYARec(306,50);yyac++; 
					yya[yyac] = new YYARec(307,51);yyac++; 
					yya[yyac] = new YYARec(309,52);yyac++; 
					yya[yyac] = new YYARec(310,53);yyac++; 
					yya[yyac] = new YYARec(311,54);yyac++; 
					yya[yyac] = new YYARec(312,55);yyac++; 
					yya[yyac] = new YYARec(313,56);yyac++; 
					yya[yyac] = new YYARec(314,57);yyac++; 
					yya[yyac] = new YYARec(315,58);yyac++; 
					yya[yyac] = new YYARec(316,59);yyac++; 
					yya[yyac] = new YYARec(317,60);yyac++; 
					yya[yyac] = new YYARec(318,61);yyac++; 
					yya[yyac] = new YYARec(319,62);yyac++; 
					yya[yyac] = new YYARec(320,63);yyac++; 
					yya[yyac] = new YYARec(323,64);yyac++; 
					yya[yyac] = new YYARec(298,42);yyac++; 
					yya[yyac] = new YYARec(299,43);yyac++; 
					yya[yyac] = new YYARec(300,44);yyac++; 
					yya[yyac] = new YYARec(301,45);yyac++; 
					yya[yyac] = new YYARec(302,46);yyac++; 
					yya[yyac] = new YYARec(303,47);yyac++; 
					yya[yyac] = new YYARec(304,48);yyac++; 
					yya[yyac] = new YYARec(305,49);yyac++; 
					yya[yyac] = new YYARec(306,50);yyac++; 
					yya[yyac] = new YYARec(307,51);yyac++; 
					yya[yyac] = new YYARec(309,52);yyac++; 
					yya[yyac] = new YYARec(310,53);yyac++; 
					yya[yyac] = new YYARec(311,54);yyac++; 
					yya[yyac] = new YYARec(312,55);yyac++; 
					yya[yyac] = new YYARec(313,56);yyac++; 
					yya[yyac] = new YYARec(314,57);yyac++; 
					yya[yyac] = new YYARec(315,58);yyac++; 
					yya[yyac] = new YYARec(316,59);yyac++; 
					yya[yyac] = new YYARec(317,60);yyac++; 
					yya[yyac] = new YYARec(318,61);yyac++; 
					yya[yyac] = new YYARec(319,62);yyac++; 
					yya[yyac] = new YYARec(320,63);yyac++; 
					yya[yyac] = new YYARec(321,69);yyac++; 
					yya[yyac] = new YYARec(323,64);yyac++; 
					yya[yyac] = new YYARec(263,71);yyac++; 
					yya[yyac] = new YYARec(282,-94 );yyac++; 
					yya[yyac] = new YYARec(283,-94 );yyac++; 
					yya[yyac] = new YYARec(287,-94 );yyac++; 
					yya[yyac] = new YYARec(321,-94 );yyac++; 
					yya[yyac] = new YYARec(322,-94 );yyac++; 
					yya[yyac] = new YYARec(323,-94 );yyac++; 
					yya[yyac] = new YYARec(324,-94 );yyac++; 
					yya[yyac] = new YYARec(325,-94 );yyac++; 
					yya[yyac] = new YYARec(263,71);yyac++; 
					yya[yyac] = new YYARec(298,-94 );yyac++; 
					yya[yyac] = new YYARec(299,-94 );yyac++; 
					yya[yyac] = new YYARec(300,-94 );yyac++; 
					yya[yyac] = new YYARec(301,-94 );yyac++; 
					yya[yyac] = new YYARec(302,-94 );yyac++; 
					yya[yyac] = new YYARec(303,-94 );yyac++; 
					yya[yyac] = new YYARec(304,-94 );yyac++; 
					yya[yyac] = new YYARec(305,-94 );yyac++; 
					yya[yyac] = new YYARec(306,-94 );yyac++; 
					yya[yyac] = new YYARec(307,-94 );yyac++; 
					yya[yyac] = new YYARec(309,-94 );yyac++; 
					yya[yyac] = new YYARec(310,-94 );yyac++; 
					yya[yyac] = new YYARec(311,-94 );yyac++; 
					yya[yyac] = new YYARec(312,-94 );yyac++; 
					yya[yyac] = new YYARec(313,-94 );yyac++; 
					yya[yyac] = new YYARec(314,-94 );yyac++; 
					yya[yyac] = new YYARec(315,-94 );yyac++; 
					yya[yyac] = new YYARec(316,-94 );yyac++; 
					yya[yyac] = new YYARec(317,-94 );yyac++; 
					yya[yyac] = new YYARec(318,-94 );yyac++; 
					yya[yyac] = new YYARec(319,-94 );yyac++; 
					yya[yyac] = new YYARec(320,-94 );yyac++; 
					yya[yyac] = new YYARec(323,-94 );yyac++; 
					yya[yyac] = new YYARec(257,18);yyac++; 
					yya[yyac] = new YYARec(258,19);yyac++; 
					yya[yyac] = new YYARec(259,20);yyac++; 
					yya[yyac] = new YYARec(262,21);yyac++; 
					yya[yyac] = new YYARec(264,22);yyac++; 
					yya[yyac] = new YYARec(265,23);yyac++; 
					yya[yyac] = new YYARec(267,24);yyac++; 
					yya[yyac] = new YYARec(299,25);yyac++; 
					yya[yyac] = new YYARec(300,26);yyac++; 
					yya[yyac] = new YYARec(301,27);yyac++; 
					yya[yyac] = new YYARec(302,28);yyac++; 
					yya[yyac] = new YYARec(303,29);yyac++; 
					yya[yyac] = new YYARec(309,30);yyac++; 
					yya[yyac] = new YYARec(311,31);yyac++; 
					yya[yyac] = new YYARec(312,32);yyac++; 
					yya[yyac] = new YYARec(313,33);yyac++; 
					yya[yyac] = new YYARec(315,34);yyac++; 
					yya[yyac] = new YYARec(317,35);yyac++; 
					yya[yyac] = new YYARec(319,36);yyac++; 
					yya[yyac] = new YYARec(0,-3 );yyac++; 
					yya[yyac] = new YYARec(260,-3 );yyac++; 
					yya[yyac] = new YYARec(261,-3 );yyac++; 
					yya[yyac] = new YYARec(0,0);yyac++; 
					yya[yyac] = new YYARec(309,30);yyac++; 
					yya[yyac] = new YYARec(315,34);yyac++; 
					yya[yyac] = new YYARec(317,35);yyac++; 
					yya[yyac] = new YYARec(323,64);yyac++; 
					yya[yyac] = new YYARec(309,30);yyac++; 
					yya[yyac] = new YYARec(315,34);yyac++; 
					yya[yyac] = new YYARec(317,35);yyac++; 
					yya[yyac] = new YYARec(323,64);yyac++; 
					yya[yyac] = new YYARec(309,30);yyac++; 
					yya[yyac] = new YYARec(315,34);yyac++; 
					yya[yyac] = new YYARec(317,35);yyac++; 
					yya[yyac] = new YYARec(323,64);yyac++; 
					yya[yyac] = new YYARec(309,30);yyac++; 
					yya[yyac] = new YYARec(315,34);yyac++; 
					yya[yyac] = new YYARec(317,35);yyac++; 
					yya[yyac] = new YYARec(323,64);yyac++; 
					yya[yyac] = new YYARec(324,81);yyac++; 
					yya[yyac] = new YYARec(263,71);yyac++; 
					yya[yyac] = new YYARec(266,-94 );yyac++; 
					yya[yyac] = new YYARec(258,84);yyac++; 
					yya[yyac] = new YYARec(263,71);yyac++; 
					yya[yyac] = new YYARec(266,-94 );yyac++; 
					yya[yyac] = new YYARec(325,-94 );yyac++; 
					yya[yyac] = new YYARec(263,71);yyac++; 
					yya[yyac] = new YYARec(324,-94 );yyac++; 
					yya[yyac] = new YYARec(282,94);yyac++; 
					yya[yyac] = new YYARec(283,95);yyac++; 
					yya[yyac] = new YYARec(287,96);yyac++; 
					yya[yyac] = new YYARec(321,97);yyac++; 
					yya[yyac] = new YYARec(322,98);yyac++; 
					yya[yyac] = new YYARec(323,64);yyac++; 
					yya[yyac] = new YYARec(324,81);yyac++; 
					yya[yyac] = new YYARec(325,99);yyac++; 
					yya[yyac] = new YYARec(298,42);yyac++; 
					yya[yyac] = new YYARec(299,43);yyac++; 
					yya[yyac] = new YYARec(300,44);yyac++; 
					yya[yyac] = new YYARec(301,45);yyac++; 
					yya[yyac] = new YYARec(302,46);yyac++; 
					yya[yyac] = new YYARec(303,47);yyac++; 
					yya[yyac] = new YYARec(304,48);yyac++; 
					yya[yyac] = new YYARec(305,49);yyac++; 
					yya[yyac] = new YYARec(306,50);yyac++; 
					yya[yyac] = new YYARec(307,51);yyac++; 
					yya[yyac] = new YYARec(309,52);yyac++; 
					yya[yyac] = new YYARec(310,53);yyac++; 
					yya[yyac] = new YYARec(311,54);yyac++; 
					yya[yyac] = new YYARec(312,55);yyac++; 
					yya[yyac] = new YYARec(313,56);yyac++; 
					yya[yyac] = new YYARec(314,57);yyac++; 
					yya[yyac] = new YYARec(315,58);yyac++; 
					yya[yyac] = new YYARec(316,59);yyac++; 
					yya[yyac] = new YYARec(317,60);yyac++; 
					yya[yyac] = new YYARec(318,61);yyac++; 
					yya[yyac] = new YYARec(319,62);yyac++; 
					yya[yyac] = new YYARec(320,63);yyac++; 
					yya[yyac] = new YYARec(323,64);yyac++; 
					yya[yyac] = new YYARec(258,102);yyac++; 
					yya[yyac] = new YYARec(258,103);yyac++; 
					yya[yyac] = new YYARec(258,104);yyac++; 
					yya[yyac] = new YYARec(263,105);yyac++; 
					yya[yyac] = new YYARec(258,106);yyac++; 
					yya[yyac] = new YYARec(258,107);yyac++; 
					yya[yyac] = new YYARec(266,108);yyac++; 
					yya[yyac] = new YYARec(266,110);yyac++; 
					yya[yyac] = new YYARec(325,99);yyac++; 
					yya[yyac] = new YYARec(324,81);yyac++; 
					yya[yyac] = new YYARec(321,112);yyac++; 
					yya[yyac] = new YYARec(322,113);yyac++; 
					yya[yyac] = new YYARec(258,114);yyac++; 
					yya[yyac] = new YYARec(263,71);yyac++; 
					yya[yyac] = new YYARec(258,-94 );yyac++; 
					yya[yyac] = new YYARec(298,-94 );yyac++; 
					yya[yyac] = new YYARec(299,-94 );yyac++; 
					yya[yyac] = new YYARec(300,-94 );yyac++; 
					yya[yyac] = new YYARec(301,-94 );yyac++; 
					yya[yyac] = new YYARec(302,-94 );yyac++; 
					yya[yyac] = new YYARec(303,-94 );yyac++; 
					yya[yyac] = new YYARec(304,-94 );yyac++; 
					yya[yyac] = new YYARec(305,-94 );yyac++; 
					yya[yyac] = new YYARec(306,-94 );yyac++; 
					yya[yyac] = new YYARec(307,-94 );yyac++; 
					yya[yyac] = new YYARec(309,-94 );yyac++; 
					yya[yyac] = new YYARec(310,-94 );yyac++; 
					yya[yyac] = new YYARec(311,-94 );yyac++; 
					yya[yyac] = new YYARec(312,-94 );yyac++; 
					yya[yyac] = new YYARec(313,-94 );yyac++; 
					yya[yyac] = new YYARec(314,-94 );yyac++; 
					yya[yyac] = new YYARec(315,-94 );yyac++; 
					yya[yyac] = new YYARec(316,-94 );yyac++; 
					yya[yyac] = new YYARec(317,-94 );yyac++; 
					yya[yyac] = new YYARec(318,-94 );yyac++; 
					yya[yyac] = new YYARec(319,-94 );yyac++; 
					yya[yyac] = new YYARec(320,-94 );yyac++; 
					yya[yyac] = new YYARec(323,-94 );yyac++; 
					yya[yyac] = new YYARec(258,116);yyac++; 
					yya[yyac] = new YYARec(257,18);yyac++; 
					yya[yyac] = new YYARec(258,19);yyac++; 
					yya[yyac] = new YYARec(259,20);yyac++; 
					yya[yyac] = new YYARec(262,21);yyac++; 
					yya[yyac] = new YYARec(264,22);yyac++; 
					yya[yyac] = new YYARec(265,23);yyac++; 
					yya[yyac] = new YYARec(267,24);yyac++; 
					yya[yyac] = new YYARec(299,25);yyac++; 
					yya[yyac] = new YYARec(300,26);yyac++; 
					yya[yyac] = new YYARec(301,27);yyac++; 
					yya[yyac] = new YYARec(302,28);yyac++; 
					yya[yyac] = new YYARec(303,29);yyac++; 
					yya[yyac] = new YYARec(309,30);yyac++; 
					yya[yyac] = new YYARec(311,31);yyac++; 
					yya[yyac] = new YYARec(312,32);yyac++; 
					yya[yyac] = new YYARec(313,33);yyac++; 
					yya[yyac] = new YYARec(315,34);yyac++; 
					yya[yyac] = new YYARec(317,35);yyac++; 
					yya[yyac] = new YYARec(319,36);yyac++; 
					yya[yyac] = new YYARec(260,-3 );yyac++; 
					yya[yyac] = new YYARec(261,-3 );yyac++; 
					yya[yyac] = new YYARec(257,18);yyac++; 
					yya[yyac] = new YYARec(258,19);yyac++; 
					yya[yyac] = new YYARec(259,20);yyac++; 
					yya[yyac] = new YYARec(262,21);yyac++; 
					yya[yyac] = new YYARec(264,22);yyac++; 
					yya[yyac] = new YYARec(265,23);yyac++; 
					yya[yyac] = new YYARec(267,24);yyac++; 
					yya[yyac] = new YYARec(299,25);yyac++; 
					yya[yyac] = new YYARec(300,26);yyac++; 
					yya[yyac] = new YYARec(301,27);yyac++; 
					yya[yyac] = new YYARec(302,28);yyac++; 
					yya[yyac] = new YYARec(303,29);yyac++; 
					yya[yyac] = new YYARec(309,30);yyac++; 
					yya[yyac] = new YYARec(311,31);yyac++; 
					yya[yyac] = new YYARec(312,32);yyac++; 
					yya[yyac] = new YYARec(313,33);yyac++; 
					yya[yyac] = new YYARec(315,34);yyac++; 
					yya[yyac] = new YYARec(317,35);yyac++; 
					yya[yyac] = new YYARec(319,36);yyac++; 
					yya[yyac] = new YYARec(260,-3 );yyac++; 
					yya[yyac] = new YYARec(261,-3 );yyac++; 
					yya[yyac] = new YYARec(282,94);yyac++; 
					yya[yyac] = new YYARec(283,95);yyac++; 
					yya[yyac] = new YYARec(287,96);yyac++; 
					yya[yyac] = new YYARec(298,42);yyac++; 
					yya[yyac] = new YYARec(299,43);yyac++; 
					yya[yyac] = new YYARec(300,44);yyac++; 
					yya[yyac] = new YYARec(301,45);yyac++; 
					yya[yyac] = new YYARec(302,46);yyac++; 
					yya[yyac] = new YYARec(303,47);yyac++; 
					yya[yyac] = new YYARec(304,48);yyac++; 
					yya[yyac] = new YYARec(305,49);yyac++; 
					yya[yyac] = new YYARec(306,50);yyac++; 
					yya[yyac] = new YYARec(307,51);yyac++; 
					yya[yyac] = new YYARec(308,127);yyac++; 
					yya[yyac] = new YYARec(309,52);yyac++; 
					yya[yyac] = new YYARec(310,53);yyac++; 
					yya[yyac] = new YYARec(311,54);yyac++; 
					yya[yyac] = new YYARec(312,55);yyac++; 
					yya[yyac] = new YYARec(313,56);yyac++; 
					yya[yyac] = new YYARec(314,57);yyac++; 
					yya[yyac] = new YYARec(315,58);yyac++; 
					yya[yyac] = new YYARec(316,59);yyac++; 
					yya[yyac] = new YYARec(317,60);yyac++; 
					yya[yyac] = new YYARec(318,61);yyac++; 
					yya[yyac] = new YYARec(319,62);yyac++; 
					yya[yyac] = new YYARec(320,63);yyac++; 
					yya[yyac] = new YYARec(321,97);yyac++; 
					yya[yyac] = new YYARec(322,98);yyac++; 
					yya[yyac] = new YYARec(323,64);yyac++; 
					yya[yyac] = new YYARec(324,81);yyac++; 
					yya[yyac] = new YYARec(325,99);yyac++; 
					yya[yyac] = new YYARec(257,138);yyac++; 
					yya[yyac] = new YYARec(258,139);yyac++; 
					yya[yyac] = new YYARec(259,140);yyac++; 
					yya[yyac] = new YYARec(264,141);yyac++; 
					yya[yyac] = new YYARec(266,142);yyac++; 
					yya[yyac] = new YYARec(288,143);yyac++; 
					yya[yyac] = new YYARec(294,144);yyac++; 
					yya[yyac] = new YYARec(295,145);yyac++; 
					yya[yyac] = new YYARec(296,146);yyac++; 
					yya[yyac] = new YYARec(298,42);yyac++; 
					yya[yyac] = new YYARec(299,43);yyac++; 
					yya[yyac] = new YYARec(300,44);yyac++; 
					yya[yyac] = new YYARec(301,45);yyac++; 
					yya[yyac] = new YYARec(302,46);yyac++; 
					yya[yyac] = new YYARec(303,47);yyac++; 
					yya[yyac] = new YYARec(304,48);yyac++; 
					yya[yyac] = new YYARec(305,49);yyac++; 
					yya[yyac] = new YYARec(306,50);yyac++; 
					yya[yyac] = new YYARec(307,147);yyac++; 
					yya[yyac] = new YYARec(309,52);yyac++; 
					yya[yyac] = new YYARec(310,53);yyac++; 
					yya[yyac] = new YYARec(311,54);yyac++; 
					yya[yyac] = new YYARec(312,55);yyac++; 
					yya[yyac] = new YYARec(313,56);yyac++; 
					yya[yyac] = new YYARec(314,148);yyac++; 
					yya[yyac] = new YYARec(315,58);yyac++; 
					yya[yyac] = new YYARec(316,59);yyac++; 
					yya[yyac] = new YYARec(317,60);yyac++; 
					yya[yyac] = new YYARec(318,149);yyac++; 
					yya[yyac] = new YYARec(319,62);yyac++; 
					yya[yyac] = new YYARec(320,150);yyac++; 
					yya[yyac] = new YYARec(323,151);yyac++; 
					yya[yyac] = new YYARec(267,-79 );yyac++; 
					yya[yyac] = new YYARec(258,152);yyac++; 
					yya[yyac] = new YYARec(257,160);yyac++; 
					yya[yyac] = new YYARec(259,161);yyac++; 
					yya[yyac] = new YYARec(301,27);yyac++; 
					yya[yyac] = new YYARec(302,28);yyac++; 
					yya[yyac] = new YYARec(306,162);yyac++; 
					yya[yyac] = new YYARec(311,163);yyac++; 
					yya[yyac] = new YYARec(312,164);yyac++; 
					yya[yyac] = new YYARec(315,165);yyac++; 
					yya[yyac] = new YYARec(317,166);yyac++; 
					yya[yyac] = new YYARec(319,167);yyac++; 
					yya[yyac] = new YYARec(320,168);yyac++; 
					yya[yyac] = new YYARec(323,64);yyac++; 
					yya[yyac] = new YYARec(267,-63 );yyac++; 
					yya[yyac] = new YYARec(263,71);yyac++; 
					yya[yyac] = new YYARec(258,-94 );yyac++; 
					yya[yyac] = new YYARec(282,-94 );yyac++; 
					yya[yyac] = new YYARec(283,-94 );yyac++; 
					yya[yyac] = new YYARec(287,-94 );yyac++; 
					yya[yyac] = new YYARec(321,-94 );yyac++; 
					yya[yyac] = new YYARec(298,42);yyac++; 
					yya[yyac] = new YYARec(299,43);yyac++; 
					yya[yyac] = new YYARec(300,44);yyac++; 
					yya[yyac] = new YYARec(301,45);yyac++; 
					yya[yyac] = new YYARec(302,46);yyac++; 
					yya[yyac] = new YYARec(303,47);yyac++; 
					yya[yyac] = new YYARec(304,48);yyac++; 
					yya[yyac] = new YYARec(305,49);yyac++; 
					yya[yyac] = new YYARec(306,50);yyac++; 
					yya[yyac] = new YYARec(307,51);yyac++; 
					yya[yyac] = new YYARec(309,52);yyac++; 
					yya[yyac] = new YYARec(310,53);yyac++; 
					yya[yyac] = new YYARec(311,54);yyac++; 
					yya[yyac] = new YYARec(312,55);yyac++; 
					yya[yyac] = new YYARec(313,56);yyac++; 
					yya[yyac] = new YYARec(314,57);yyac++; 
					yya[yyac] = new YYARec(315,58);yyac++; 
					yya[yyac] = new YYARec(316,59);yyac++; 
					yya[yyac] = new YYARec(317,60);yyac++; 
					yya[yyac] = new YYARec(318,61);yyac++; 
					yya[yyac] = new YYARec(319,62);yyac++; 
					yya[yyac] = new YYARec(320,63);yyac++; 
					yya[yyac] = new YYARec(323,64);yyac++; 
					yya[yyac] = new YYARec(258,-44 );yyac++; 
					yya[yyac] = new YYARec(258,171);yyac++; 
					yya[yyac] = new YYARec(260,172);yyac++; 
					yya[yyac] = new YYARec(261,173);yyac++; 
					yya[yyac] = new YYARec(258,174);yyac++; 
					yya[yyac] = new YYARec(297,175);yyac++; 
					yya[yyac] = new YYARec(258,-150 );yyac++; 
					yya[yyac] = new YYARec(263,-150 );yyac++; 
					yya[yyac] = new YYARec(266,-150 );yyac++; 
					yya[yyac] = new YYARec(269,-150 );yyac++; 
					yya[yyac] = new YYARec(270,-150 );yyac++; 
					yya[yyac] = new YYARec(271,-150 );yyac++; 
					yya[yyac] = new YYARec(272,-150 );yyac++; 
					yya[yyac] = new YYARec(273,-150 );yyac++; 
					yya[yyac] = new YYARec(275,-150 );yyac++; 
					yya[yyac] = new YYARec(276,-150 );yyac++; 
					yya[yyac] = new YYARec(277,-150 );yyac++; 
					yya[yyac] = new YYARec(278,-150 );yyac++; 
					yya[yyac] = new YYARec(279,-150 );yyac++; 
					yya[yyac] = new YYARec(280,-150 );yyac++; 
					yya[yyac] = new YYARec(281,-150 );yyac++; 
					yya[yyac] = new YYARec(282,-150 );yyac++; 
					yya[yyac] = new YYARec(283,-150 );yyac++; 
					yya[yyac] = new YYARec(284,-150 );yyac++; 
					yya[yyac] = new YYARec(285,-150 );yyac++; 
					yya[yyac] = new YYARec(286,-150 );yyac++; 
					yya[yyac] = new YYARec(287,-150 );yyac++; 
					yya[yyac] = new YYARec(289,-150 );yyac++; 
					yya[yyac] = new YYARec(290,-150 );yyac++; 
					yya[yyac] = new YYARec(291,-150 );yyac++; 
					yya[yyac] = new YYARec(292,-150 );yyac++; 
					yya[yyac] = new YYARec(293,-150 );yyac++; 
					yya[yyac] = new YYARec(298,-150 );yyac++; 
					yya[yyac] = new YYARec(299,-150 );yyac++; 
					yya[yyac] = new YYARec(300,-150 );yyac++; 
					yya[yyac] = new YYARec(301,-150 );yyac++; 
					yya[yyac] = new YYARec(302,-150 );yyac++; 
					yya[yyac] = new YYARec(303,-150 );yyac++; 
					yya[yyac] = new YYARec(304,-150 );yyac++; 
					yya[yyac] = new YYARec(305,-150 );yyac++; 
					yya[yyac] = new YYARec(306,-150 );yyac++; 
					yya[yyac] = new YYARec(307,-150 );yyac++; 
					yya[yyac] = new YYARec(308,-150 );yyac++; 
					yya[yyac] = new YYARec(309,-150 );yyac++; 
					yya[yyac] = new YYARec(310,-150 );yyac++; 
					yya[yyac] = new YYARec(311,-150 );yyac++; 
					yya[yyac] = new YYARec(312,-150 );yyac++; 
					yya[yyac] = new YYARec(313,-150 );yyac++; 
					yya[yyac] = new YYARec(314,-150 );yyac++; 
					yya[yyac] = new YYARec(315,-150 );yyac++; 
					yya[yyac] = new YYARec(316,-150 );yyac++; 
					yya[yyac] = new YYARec(317,-150 );yyac++; 
					yya[yyac] = new YYARec(318,-150 );yyac++; 
					yya[yyac] = new YYARec(319,-150 );yyac++; 
					yya[yyac] = new YYARec(320,-150 );yyac++; 
					yya[yyac] = new YYARec(321,-150 );yyac++; 
					yya[yyac] = new YYARec(322,-150 );yyac++; 
					yya[yyac] = new YYARec(323,-150 );yyac++; 
					yya[yyac] = new YYARec(324,-150 );yyac++; 
					yya[yyac] = new YYARec(325,-150 );yyac++; 
					yya[yyac] = new YYARec(258,176);yyac++; 
					yya[yyac] = new YYARec(297,175);yyac++; 
					yya[yyac] = new YYARec(289,-150 );yyac++; 
					yya[yyac] = new YYARec(290,-150 );yyac++; 
					yya[yyac] = new YYARec(291,-150 );yyac++; 
					yya[yyac] = new YYARec(292,-150 );yyac++; 
					yya[yyac] = new YYARec(293,-150 );yyac++; 
					yya[yyac] = new YYARec(268,-212 );yyac++; 
					yya[yyac] = new YYARec(258,177);yyac++; 
					yya[yyac] = new YYARec(258,179);yyac++; 
					yya[yyac] = new YYARec(263,71);yyac++; 
					yya[yyac] = new YYARec(282,-94 );yyac++; 
					yya[yyac] = new YYARec(283,-94 );yyac++; 
					yya[yyac] = new YYARec(287,-94 );yyac++; 
					yya[yyac] = new YYARec(298,-94 );yyac++; 
					yya[yyac] = new YYARec(299,-94 );yyac++; 
					yya[yyac] = new YYARec(300,-94 );yyac++; 
					yya[yyac] = new YYARec(301,-94 );yyac++; 
					yya[yyac] = new YYARec(302,-94 );yyac++; 
					yya[yyac] = new YYARec(303,-94 );yyac++; 
					yya[yyac] = new YYARec(304,-94 );yyac++; 
					yya[yyac] = new YYARec(305,-94 );yyac++; 
					yya[yyac] = new YYARec(306,-94 );yyac++; 
					yya[yyac] = new YYARec(307,-94 );yyac++; 
					yya[yyac] = new YYARec(308,-94 );yyac++; 
					yya[yyac] = new YYARec(309,-94 );yyac++; 
					yya[yyac] = new YYARec(310,-94 );yyac++; 
					yya[yyac] = new YYARec(311,-94 );yyac++; 
					yya[yyac] = new YYARec(312,-94 );yyac++; 
					yya[yyac] = new YYARec(313,-94 );yyac++; 
					yya[yyac] = new YYARec(314,-94 );yyac++; 
					yya[yyac] = new YYARec(315,-94 );yyac++; 
					yya[yyac] = new YYARec(316,-94 );yyac++; 
					yya[yyac] = new YYARec(317,-94 );yyac++; 
					yya[yyac] = new YYARec(318,-94 );yyac++; 
					yya[yyac] = new YYARec(319,-94 );yyac++; 
					yya[yyac] = new YYARec(320,-94 );yyac++; 
					yya[yyac] = new YYARec(321,-94 );yyac++; 
					yya[yyac] = new YYARec(322,-94 );yyac++; 
					yya[yyac] = new YYARec(323,-94 );yyac++; 
					yya[yyac] = new YYARec(324,-94 );yyac++; 
					yya[yyac] = new YYARec(325,-94 );yyac++; 
					yya[yyac] = new YYARec(257,138);yyac++; 
					yya[yyac] = new YYARec(258,139);yyac++; 
					yya[yyac] = new YYARec(259,140);yyac++; 
					yya[yyac] = new YYARec(264,141);yyac++; 
					yya[yyac] = new YYARec(266,142);yyac++; 
					yya[yyac] = new YYARec(288,143);yyac++; 
					yya[yyac] = new YYARec(294,144);yyac++; 
					yya[yyac] = new YYARec(295,145);yyac++; 
					yya[yyac] = new YYARec(296,146);yyac++; 
					yya[yyac] = new YYARec(298,42);yyac++; 
					yya[yyac] = new YYARec(299,43);yyac++; 
					yya[yyac] = new YYARec(300,44);yyac++; 
					yya[yyac] = new YYARec(301,45);yyac++; 
					yya[yyac] = new YYARec(302,46);yyac++; 
					yya[yyac] = new YYARec(303,47);yyac++; 
					yya[yyac] = new YYARec(304,48);yyac++; 
					yya[yyac] = new YYARec(305,49);yyac++; 
					yya[yyac] = new YYARec(306,50);yyac++; 
					yya[yyac] = new YYARec(307,147);yyac++; 
					yya[yyac] = new YYARec(309,52);yyac++; 
					yya[yyac] = new YYARec(310,53);yyac++; 
					yya[yyac] = new YYARec(311,54);yyac++; 
					yya[yyac] = new YYARec(312,55);yyac++; 
					yya[yyac] = new YYARec(313,56);yyac++; 
					yya[yyac] = new YYARec(314,148);yyac++; 
					yya[yyac] = new YYARec(315,58);yyac++; 
					yya[yyac] = new YYARec(316,59);yyac++; 
					yya[yyac] = new YYARec(317,60);yyac++; 
					yya[yyac] = new YYARec(318,149);yyac++; 
					yya[yyac] = new YYARec(319,62);yyac++; 
					yya[yyac] = new YYARec(320,150);yyac++; 
					yya[yyac] = new YYARec(323,151);yyac++; 
					yya[yyac] = new YYARec(260,-79 );yyac++; 
					yya[yyac] = new YYARec(261,-79 );yyac++; 
					yya[yyac] = new YYARec(267,-79 );yyac++; 
					yya[yyac] = new YYARec(257,138);yyac++; 
					yya[yyac] = new YYARec(258,139);yyac++; 
					yya[yyac] = new YYARec(259,140);yyac++; 
					yya[yyac] = new YYARec(264,141);yyac++; 
					yya[yyac] = new YYARec(266,142);yyac++; 
					yya[yyac] = new YYARec(288,143);yyac++; 
					yya[yyac] = new YYARec(294,144);yyac++; 
					yya[yyac] = new YYARec(295,145);yyac++; 
					yya[yyac] = new YYARec(296,146);yyac++; 
					yya[yyac] = new YYARec(298,42);yyac++; 
					yya[yyac] = new YYARec(299,43);yyac++; 
					yya[yyac] = new YYARec(300,44);yyac++; 
					yya[yyac] = new YYARec(301,45);yyac++; 
					yya[yyac] = new YYARec(302,46);yyac++; 
					yya[yyac] = new YYARec(303,47);yyac++; 
					yya[yyac] = new YYARec(304,48);yyac++; 
					yya[yyac] = new YYARec(305,49);yyac++; 
					yya[yyac] = new YYARec(306,50);yyac++; 
					yya[yyac] = new YYARec(307,147);yyac++; 
					yya[yyac] = new YYARec(309,52);yyac++; 
					yya[yyac] = new YYARec(310,53);yyac++; 
					yya[yyac] = new YYARec(311,54);yyac++; 
					yya[yyac] = new YYARec(312,55);yyac++; 
					yya[yyac] = new YYARec(313,56);yyac++; 
					yya[yyac] = new YYARec(314,148);yyac++; 
					yya[yyac] = new YYARec(315,58);yyac++; 
					yya[yyac] = new YYARec(316,59);yyac++; 
					yya[yyac] = new YYARec(317,60);yyac++; 
					yya[yyac] = new YYARec(318,149);yyac++; 
					yya[yyac] = new YYARec(319,62);yyac++; 
					yya[yyac] = new YYARec(320,150);yyac++; 
					yya[yyac] = new YYARec(323,151);yyac++; 
					yya[yyac] = new YYARec(260,-79 );yyac++; 
					yya[yyac] = new YYARec(261,-79 );yyac++; 
					yya[yyac] = new YYARec(267,-79 );yyac++; 
					yya[yyac] = new YYARec(268,182);yyac++; 
					yya[yyac] = new YYARec(289,184);yyac++; 
					yya[yyac] = new YYARec(290,185);yyac++; 
					yya[yyac] = new YYARec(291,186);yyac++; 
					yya[yyac] = new YYARec(292,187);yyac++; 
					yya[yyac] = new YYARec(293,188);yyac++; 
					yya[yyac] = new YYARec(263,71);yyac++; 
					yya[yyac] = new YYARec(282,-94 );yyac++; 
					yya[yyac] = new YYARec(283,-94 );yyac++; 
					yya[yyac] = new YYARec(287,-94 );yyac++; 
					yya[yyac] = new YYARec(298,-94 );yyac++; 
					yya[yyac] = new YYARec(299,-94 );yyac++; 
					yya[yyac] = new YYARec(300,-94 );yyac++; 
					yya[yyac] = new YYARec(301,-94 );yyac++; 
					yya[yyac] = new YYARec(302,-94 );yyac++; 
					yya[yyac] = new YYARec(303,-94 );yyac++; 
					yya[yyac] = new YYARec(304,-94 );yyac++; 
					yya[yyac] = new YYARec(305,-94 );yyac++; 
					yya[yyac] = new YYARec(306,-94 );yyac++; 
					yya[yyac] = new YYARec(307,-94 );yyac++; 
					yya[yyac] = new YYARec(308,-94 );yyac++; 
					yya[yyac] = new YYARec(309,-94 );yyac++; 
					yya[yyac] = new YYARec(310,-94 );yyac++; 
					yya[yyac] = new YYARec(311,-94 );yyac++; 
					yya[yyac] = new YYARec(312,-94 );yyac++; 
					yya[yyac] = new YYARec(313,-94 );yyac++; 
					yya[yyac] = new YYARec(314,-94 );yyac++; 
					yya[yyac] = new YYARec(315,-94 );yyac++; 
					yya[yyac] = new YYARec(316,-94 );yyac++; 
					yya[yyac] = new YYARec(317,-94 );yyac++; 
					yya[yyac] = new YYARec(318,-94 );yyac++; 
					yya[yyac] = new YYARec(319,-94 );yyac++; 
					yya[yyac] = new YYARec(320,-94 );yyac++; 
					yya[yyac] = new YYARec(321,-94 );yyac++; 
					yya[yyac] = new YYARec(322,-94 );yyac++; 
					yya[yyac] = new YYARec(323,-94 );yyac++; 
					yya[yyac] = new YYARec(324,-94 );yyac++; 
					yya[yyac] = new YYARec(325,-94 );yyac++; 
					yya[yyac] = new YYARec(268,-205 );yyac++; 
					yya[yyac] = new YYARec(289,-205 );yyac++; 
					yya[yyac] = new YYARec(290,-205 );yyac++; 
					yya[yyac] = new YYARec(291,-205 );yyac++; 
					yya[yyac] = new YYARec(292,-205 );yyac++; 
					yya[yyac] = new YYARec(293,-205 );yyac++; 
					yya[yyac] = new YYARec(297,-205 );yyac++; 
					yya[yyac] = new YYARec(267,190);yyac++; 
					yya[yyac] = new YYARec(257,138);yyac++; 
					yya[yyac] = new YYARec(258,139);yyac++; 
					yya[yyac] = new YYARec(259,140);yyac++; 
					yya[yyac] = new YYARec(264,141);yyac++; 
					yya[yyac] = new YYARec(266,142);yyac++; 
					yya[yyac] = new YYARec(288,143);yyac++; 
					yya[yyac] = new YYARec(294,144);yyac++; 
					yya[yyac] = new YYARec(295,145);yyac++; 
					yya[yyac] = new YYARec(296,146);yyac++; 
					yya[yyac] = new YYARec(298,42);yyac++; 
					yya[yyac] = new YYARec(299,43);yyac++; 
					yya[yyac] = new YYARec(300,44);yyac++; 
					yya[yyac] = new YYARec(301,45);yyac++; 
					yya[yyac] = new YYARec(302,46);yyac++; 
					yya[yyac] = new YYARec(303,47);yyac++; 
					yya[yyac] = new YYARec(304,48);yyac++; 
					yya[yyac] = new YYARec(305,49);yyac++; 
					yya[yyac] = new YYARec(306,50);yyac++; 
					yya[yyac] = new YYARec(307,147);yyac++; 
					yya[yyac] = new YYARec(309,52);yyac++; 
					yya[yyac] = new YYARec(310,53);yyac++; 
					yya[yyac] = new YYARec(311,54);yyac++; 
					yya[yyac] = new YYARec(312,55);yyac++; 
					yya[yyac] = new YYARec(313,56);yyac++; 
					yya[yyac] = new YYARec(314,148);yyac++; 
					yya[yyac] = new YYARec(315,58);yyac++; 
					yya[yyac] = new YYARec(316,59);yyac++; 
					yya[yyac] = new YYARec(317,60);yyac++; 
					yya[yyac] = new YYARec(318,149);yyac++; 
					yya[yyac] = new YYARec(319,62);yyac++; 
					yya[yyac] = new YYARec(320,150);yyac++; 
					yya[yyac] = new YYARec(323,151);yyac++; 
					yya[yyac] = new YYARec(260,-79 );yyac++; 
					yya[yyac] = new YYARec(261,-79 );yyac++; 
					yya[yyac] = new YYARec(267,-79 );yyac++; 
					yya[yyac] = new YYARec(309,30);yyac++; 
					yya[yyac] = new YYARec(315,34);yyac++; 
					yya[yyac] = new YYARec(317,35);yyac++; 
					yya[yyac] = new YYARec(323,64);yyac++; 
					yya[yyac] = new YYARec(309,30);yyac++; 
					yya[yyac] = new YYARec(315,34);yyac++; 
					yya[yyac] = new YYARec(317,35);yyac++; 
					yya[yyac] = new YYARec(323,64);yyac++; 
					yya[yyac] = new YYARec(309,30);yyac++; 
					yya[yyac] = new YYARec(315,34);yyac++; 
					yya[yyac] = new YYARec(317,35);yyac++; 
					yya[yyac] = new YYARec(323,64);yyac++; 
					yya[yyac] = new YYARec(257,138);yyac++; 
					yya[yyac] = new YYARec(258,139);yyac++; 
					yya[yyac] = new YYARec(259,140);yyac++; 
					yya[yyac] = new YYARec(264,141);yyac++; 
					yya[yyac] = new YYARec(266,142);yyac++; 
					yya[yyac] = new YYARec(288,143);yyac++; 
					yya[yyac] = new YYARec(294,144);yyac++; 
					yya[yyac] = new YYARec(295,145);yyac++; 
					yya[yyac] = new YYARec(296,146);yyac++; 
					yya[yyac] = new YYARec(298,42);yyac++; 
					yya[yyac] = new YYARec(299,43);yyac++; 
					yya[yyac] = new YYARec(300,44);yyac++; 
					yya[yyac] = new YYARec(301,45);yyac++; 
					yya[yyac] = new YYARec(302,46);yyac++; 
					yya[yyac] = new YYARec(303,47);yyac++; 
					yya[yyac] = new YYARec(304,48);yyac++; 
					yya[yyac] = new YYARec(305,49);yyac++; 
					yya[yyac] = new YYARec(306,50);yyac++; 
					yya[yyac] = new YYARec(307,147);yyac++; 
					yya[yyac] = new YYARec(309,52);yyac++; 
					yya[yyac] = new YYARec(310,53);yyac++; 
					yya[yyac] = new YYARec(311,54);yyac++; 
					yya[yyac] = new YYARec(312,55);yyac++; 
					yya[yyac] = new YYARec(313,56);yyac++; 
					yya[yyac] = new YYARec(314,148);yyac++; 
					yya[yyac] = new YYARec(315,58);yyac++; 
					yya[yyac] = new YYARec(316,59);yyac++; 
					yya[yyac] = new YYARec(317,60);yyac++; 
					yya[yyac] = new YYARec(318,149);yyac++; 
					yya[yyac] = new YYARec(319,62);yyac++; 
					yya[yyac] = new YYARec(320,150);yyac++; 
					yya[yyac] = new YYARec(323,151);yyac++; 
					yya[yyac] = new YYARec(267,-79 );yyac++; 
					yya[yyac] = new YYARec(274,212);yyac++; 
					yya[yyac] = new YYARec(282,94);yyac++; 
					yya[yyac] = new YYARec(283,95);yyac++; 
					yya[yyac] = new YYARec(287,96);yyac++; 
					yya[yyac] = new YYARec(298,42);yyac++; 
					yya[yyac] = new YYARec(299,43);yyac++; 
					yya[yyac] = new YYARec(300,44);yyac++; 
					yya[yyac] = new YYARec(301,45);yyac++; 
					yya[yyac] = new YYARec(302,46);yyac++; 
					yya[yyac] = new YYARec(303,47);yyac++; 
					yya[yyac] = new YYARec(304,213);yyac++; 
					yya[yyac] = new YYARec(305,49);yyac++; 
					yya[yyac] = new YYARec(306,50);yyac++; 
					yya[yyac] = new YYARec(307,51);yyac++; 
					yya[yyac] = new YYARec(309,52);yyac++; 
					yya[yyac] = new YYARec(310,53);yyac++; 
					yya[yyac] = new YYARec(311,54);yyac++; 
					yya[yyac] = new YYARec(312,55);yyac++; 
					yya[yyac] = new YYARec(313,56);yyac++; 
					yya[yyac] = new YYARec(314,214);yyac++; 
					yya[yyac] = new YYARec(315,215);yyac++; 
					yya[yyac] = new YYARec(316,59);yyac++; 
					yya[yyac] = new YYARec(317,60);yyac++; 
					yya[yyac] = new YYARec(318,61);yyac++; 
					yya[yyac] = new YYARec(319,62);yyac++; 
					yya[yyac] = new YYARec(320,63);yyac++; 
					yya[yyac] = new YYARec(321,216);yyac++; 
					yya[yyac] = new YYARec(322,217);yyac++; 
					yya[yyac] = new YYARec(323,64);yyac++; 
					yya[yyac] = new YYARec(266,218);yyac++; 
					yya[yyac] = new YYARec(258,-165 );yyac++; 
					yya[yyac] = new YYARec(263,-165 );yyac++; 
					yya[yyac] = new YYARec(282,-165 );yyac++; 
					yya[yyac] = new YYARec(283,-165 );yyac++; 
					yya[yyac] = new YYARec(287,-165 );yyac++; 
					yya[yyac] = new YYARec(298,-165 );yyac++; 
					yya[yyac] = new YYARec(299,-165 );yyac++; 
					yya[yyac] = new YYARec(300,-165 );yyac++; 
					yya[yyac] = new YYARec(301,-165 );yyac++; 
					yya[yyac] = new YYARec(302,-165 );yyac++; 
					yya[yyac] = new YYARec(303,-165 );yyac++; 
					yya[yyac] = new YYARec(304,-165 );yyac++; 
					yya[yyac] = new YYARec(305,-165 );yyac++; 
					yya[yyac] = new YYARec(306,-165 );yyac++; 
					yya[yyac] = new YYARec(307,-165 );yyac++; 
					yya[yyac] = new YYARec(308,-165 );yyac++; 
					yya[yyac] = new YYARec(309,-165 );yyac++; 
					yya[yyac] = new YYARec(310,-165 );yyac++; 
					yya[yyac] = new YYARec(311,-165 );yyac++; 
					yya[yyac] = new YYARec(312,-165 );yyac++; 
					yya[yyac] = new YYARec(313,-165 );yyac++; 
					yya[yyac] = new YYARec(314,-165 );yyac++; 
					yya[yyac] = new YYARec(315,-165 );yyac++; 
					yya[yyac] = new YYARec(316,-165 );yyac++; 
					yya[yyac] = new YYARec(317,-165 );yyac++; 
					yya[yyac] = new YYARec(318,-165 );yyac++; 
					yya[yyac] = new YYARec(319,-165 );yyac++; 
					yya[yyac] = new YYARec(320,-165 );yyac++; 
					yya[yyac] = new YYARec(321,-165 );yyac++; 
					yya[yyac] = new YYARec(322,-165 );yyac++; 
					yya[yyac] = new YYARec(323,-165 );yyac++; 
					yya[yyac] = new YYARec(324,-165 );yyac++; 
					yya[yyac] = new YYARec(325,-165 );yyac++; 
					yya[yyac] = new YYARec(274,212);yyac++; 
					yya[yyac] = new YYARec(282,94);yyac++; 
					yya[yyac] = new YYARec(283,95);yyac++; 
					yya[yyac] = new YYARec(287,96);yyac++; 
					yya[yyac] = new YYARec(298,42);yyac++; 
					yya[yyac] = new YYARec(299,43);yyac++; 
					yya[yyac] = new YYARec(300,44);yyac++; 
					yya[yyac] = new YYARec(301,45);yyac++; 
					yya[yyac] = new YYARec(302,46);yyac++; 
					yya[yyac] = new YYARec(303,47);yyac++; 
					yya[yyac] = new YYARec(304,213);yyac++; 
					yya[yyac] = new YYARec(305,49);yyac++; 
					yya[yyac] = new YYARec(306,50);yyac++; 
					yya[yyac] = new YYARec(307,51);yyac++; 
					yya[yyac] = new YYARec(309,52);yyac++; 
					yya[yyac] = new YYARec(310,53);yyac++; 
					yya[yyac] = new YYARec(311,54);yyac++; 
					yya[yyac] = new YYARec(312,55);yyac++; 
					yya[yyac] = new YYARec(313,56);yyac++; 
					yya[yyac] = new YYARec(314,214);yyac++; 
					yya[yyac] = new YYARec(315,215);yyac++; 
					yya[yyac] = new YYARec(316,59);yyac++; 
					yya[yyac] = new YYARec(317,60);yyac++; 
					yya[yyac] = new YYARec(318,61);yyac++; 
					yya[yyac] = new YYARec(319,62);yyac++; 
					yya[yyac] = new YYARec(320,63);yyac++; 
					yya[yyac] = new YYARec(321,216);yyac++; 
					yya[yyac] = new YYARec(322,217);yyac++; 
					yya[yyac] = new YYARec(323,64);yyac++; 
					yya[yyac] = new YYARec(274,212);yyac++; 
					yya[yyac] = new YYARec(282,94);yyac++; 
					yya[yyac] = new YYARec(283,95);yyac++; 
					yya[yyac] = new YYARec(287,96);yyac++; 
					yya[yyac] = new YYARec(298,42);yyac++; 
					yya[yyac] = new YYARec(299,43);yyac++; 
					yya[yyac] = new YYARec(300,44);yyac++; 
					yya[yyac] = new YYARec(301,45);yyac++; 
					yya[yyac] = new YYARec(302,46);yyac++; 
					yya[yyac] = new YYARec(303,47);yyac++; 
					yya[yyac] = new YYARec(304,213);yyac++; 
					yya[yyac] = new YYARec(305,49);yyac++; 
					yya[yyac] = new YYARec(306,50);yyac++; 
					yya[yyac] = new YYARec(307,51);yyac++; 
					yya[yyac] = new YYARec(309,52);yyac++; 
					yya[yyac] = new YYARec(310,53);yyac++; 
					yya[yyac] = new YYARec(311,54);yyac++; 
					yya[yyac] = new YYARec(312,55);yyac++; 
					yya[yyac] = new YYARec(313,56);yyac++; 
					yya[yyac] = new YYARec(314,214);yyac++; 
					yya[yyac] = new YYARec(315,215);yyac++; 
					yya[yyac] = new YYARec(316,59);yyac++; 
					yya[yyac] = new YYARec(317,60);yyac++; 
					yya[yyac] = new YYARec(318,61);yyac++; 
					yya[yyac] = new YYARec(319,62);yyac++; 
					yya[yyac] = new YYARec(320,63);yyac++; 
					yya[yyac] = new YYARec(321,216);yyac++; 
					yya[yyac] = new YYARec(322,217);yyac++; 
					yya[yyac] = new YYARec(323,64);yyac++; 
					yya[yyac] = new YYARec(258,-169 );yyac++; 
					yya[yyac] = new YYARec(263,-169 );yyac++; 
					yya[yyac] = new YYARec(282,-169 );yyac++; 
					yya[yyac] = new YYARec(283,-169 );yyac++; 
					yya[yyac] = new YYARec(287,-169 );yyac++; 
					yya[yyac] = new YYARec(298,-169 );yyac++; 
					yya[yyac] = new YYARec(299,-169 );yyac++; 
					yya[yyac] = new YYARec(300,-169 );yyac++; 
					yya[yyac] = new YYARec(301,-169 );yyac++; 
					yya[yyac] = new YYARec(302,-169 );yyac++; 
					yya[yyac] = new YYARec(303,-169 );yyac++; 
					yya[yyac] = new YYARec(304,-169 );yyac++; 
					yya[yyac] = new YYARec(305,-169 );yyac++; 
					yya[yyac] = new YYARec(306,-169 );yyac++; 
					yya[yyac] = new YYARec(307,-169 );yyac++; 
					yya[yyac] = new YYARec(308,-169 );yyac++; 
					yya[yyac] = new YYARec(309,-169 );yyac++; 
					yya[yyac] = new YYARec(310,-169 );yyac++; 
					yya[yyac] = new YYARec(311,-169 );yyac++; 
					yya[yyac] = new YYARec(312,-169 );yyac++; 
					yya[yyac] = new YYARec(313,-169 );yyac++; 
					yya[yyac] = new YYARec(314,-169 );yyac++; 
					yya[yyac] = new YYARec(315,-169 );yyac++; 
					yya[yyac] = new YYARec(316,-169 );yyac++; 
					yya[yyac] = new YYARec(317,-169 );yyac++; 
					yya[yyac] = new YYARec(318,-169 );yyac++; 
					yya[yyac] = new YYARec(319,-169 );yyac++; 
					yya[yyac] = new YYARec(320,-169 );yyac++; 
					yya[yyac] = new YYARec(321,-169 );yyac++; 
					yya[yyac] = new YYARec(322,-169 );yyac++; 
					yya[yyac] = new YYARec(323,-169 );yyac++; 
					yya[yyac] = new YYARec(324,-169 );yyac++; 
					yya[yyac] = new YYARec(325,-169 );yyac++; 
					yya[yyac] = new YYARec(268,-190 );yyac++; 
					yya[yyac] = new YYARec(289,-190 );yyac++; 
					yya[yyac] = new YYARec(290,-190 );yyac++; 
					yya[yyac] = new YYARec(291,-190 );yyac++; 
					yya[yyac] = new YYARec(292,-190 );yyac++; 
					yya[yyac] = new YYARec(293,-190 );yyac++; 
					yya[yyac] = new YYARec(297,-190 );yyac++; 
					yya[yyac] = new YYARec(258,-168 );yyac++; 
					yya[yyac] = new YYARec(263,-168 );yyac++; 
					yya[yyac] = new YYARec(282,-168 );yyac++; 
					yya[yyac] = new YYARec(283,-168 );yyac++; 
					yya[yyac] = new YYARec(287,-168 );yyac++; 
					yya[yyac] = new YYARec(298,-168 );yyac++; 
					yya[yyac] = new YYARec(299,-168 );yyac++; 
					yya[yyac] = new YYARec(300,-168 );yyac++; 
					yya[yyac] = new YYARec(301,-168 );yyac++; 
					yya[yyac] = new YYARec(302,-168 );yyac++; 
					yya[yyac] = new YYARec(303,-168 );yyac++; 
					yya[yyac] = new YYARec(304,-168 );yyac++; 
					yya[yyac] = new YYARec(305,-168 );yyac++; 
					yya[yyac] = new YYARec(306,-168 );yyac++; 
					yya[yyac] = new YYARec(307,-168 );yyac++; 
					yya[yyac] = new YYARec(308,-168 );yyac++; 
					yya[yyac] = new YYARec(309,-168 );yyac++; 
					yya[yyac] = new YYARec(310,-168 );yyac++; 
					yya[yyac] = new YYARec(311,-168 );yyac++; 
					yya[yyac] = new YYARec(312,-168 );yyac++; 
					yya[yyac] = new YYARec(313,-168 );yyac++; 
					yya[yyac] = new YYARec(314,-168 );yyac++; 
					yya[yyac] = new YYARec(315,-168 );yyac++; 
					yya[yyac] = new YYARec(316,-168 );yyac++; 
					yya[yyac] = new YYARec(317,-168 );yyac++; 
					yya[yyac] = new YYARec(318,-168 );yyac++; 
					yya[yyac] = new YYARec(319,-168 );yyac++; 
					yya[yyac] = new YYARec(320,-168 );yyac++; 
					yya[yyac] = new YYARec(321,-168 );yyac++; 
					yya[yyac] = new YYARec(322,-168 );yyac++; 
					yya[yyac] = new YYARec(323,-168 );yyac++; 
					yya[yyac] = new YYARec(324,-168 );yyac++; 
					yya[yyac] = new YYARec(325,-168 );yyac++; 
					yya[yyac] = new YYARec(268,-187 );yyac++; 
					yya[yyac] = new YYARec(289,-187 );yyac++; 
					yya[yyac] = new YYARec(290,-187 );yyac++; 
					yya[yyac] = new YYARec(291,-187 );yyac++; 
					yya[yyac] = new YYARec(292,-187 );yyac++; 
					yya[yyac] = new YYARec(293,-187 );yyac++; 
					yya[yyac] = new YYARec(297,-187 );yyac++; 
					yya[yyac] = new YYARec(258,-166 );yyac++; 
					yya[yyac] = new YYARec(263,-166 );yyac++; 
					yya[yyac] = new YYARec(282,-166 );yyac++; 
					yya[yyac] = new YYARec(283,-166 );yyac++; 
					yya[yyac] = new YYARec(287,-166 );yyac++; 
					yya[yyac] = new YYARec(298,-166 );yyac++; 
					yya[yyac] = new YYARec(299,-166 );yyac++; 
					yya[yyac] = new YYARec(300,-166 );yyac++; 
					yya[yyac] = new YYARec(301,-166 );yyac++; 
					yya[yyac] = new YYARec(302,-166 );yyac++; 
					yya[yyac] = new YYARec(303,-166 );yyac++; 
					yya[yyac] = new YYARec(304,-166 );yyac++; 
					yya[yyac] = new YYARec(305,-166 );yyac++; 
					yya[yyac] = new YYARec(306,-166 );yyac++; 
					yya[yyac] = new YYARec(307,-166 );yyac++; 
					yya[yyac] = new YYARec(308,-166 );yyac++; 
					yya[yyac] = new YYARec(309,-166 );yyac++; 
					yya[yyac] = new YYARec(310,-166 );yyac++; 
					yya[yyac] = new YYARec(311,-166 );yyac++; 
					yya[yyac] = new YYARec(312,-166 );yyac++; 
					yya[yyac] = new YYARec(313,-166 );yyac++; 
					yya[yyac] = new YYARec(314,-166 );yyac++; 
					yya[yyac] = new YYARec(315,-166 );yyac++; 
					yya[yyac] = new YYARec(316,-166 );yyac++; 
					yya[yyac] = new YYARec(317,-166 );yyac++; 
					yya[yyac] = new YYARec(318,-166 );yyac++; 
					yya[yyac] = new YYARec(319,-166 );yyac++; 
					yya[yyac] = new YYARec(320,-166 );yyac++; 
					yya[yyac] = new YYARec(321,-166 );yyac++; 
					yya[yyac] = new YYARec(322,-166 );yyac++; 
					yya[yyac] = new YYARec(323,-166 );yyac++; 
					yya[yyac] = new YYARec(324,-166 );yyac++; 
					yya[yyac] = new YYARec(325,-166 );yyac++; 
					yya[yyac] = new YYARec(268,-183 );yyac++; 
					yya[yyac] = new YYARec(289,-183 );yyac++; 
					yya[yyac] = new YYARec(290,-183 );yyac++; 
					yya[yyac] = new YYARec(291,-183 );yyac++; 
					yya[yyac] = new YYARec(292,-183 );yyac++; 
					yya[yyac] = new YYARec(293,-183 );yyac++; 
					yya[yyac] = new YYARec(297,-183 );yyac++; 
					yya[yyac] = new YYARec(258,-167 );yyac++; 
					yya[yyac] = new YYARec(263,-167 );yyac++; 
					yya[yyac] = new YYARec(282,-167 );yyac++; 
					yya[yyac] = new YYARec(283,-167 );yyac++; 
					yya[yyac] = new YYARec(287,-167 );yyac++; 
					yya[yyac] = new YYARec(298,-167 );yyac++; 
					yya[yyac] = new YYARec(299,-167 );yyac++; 
					yya[yyac] = new YYARec(300,-167 );yyac++; 
					yya[yyac] = new YYARec(301,-167 );yyac++; 
					yya[yyac] = new YYARec(302,-167 );yyac++; 
					yya[yyac] = new YYARec(303,-167 );yyac++; 
					yya[yyac] = new YYARec(304,-167 );yyac++; 
					yya[yyac] = new YYARec(305,-167 );yyac++; 
					yya[yyac] = new YYARec(306,-167 );yyac++; 
					yya[yyac] = new YYARec(307,-167 );yyac++; 
					yya[yyac] = new YYARec(308,-167 );yyac++; 
					yya[yyac] = new YYARec(309,-167 );yyac++; 
					yya[yyac] = new YYARec(310,-167 );yyac++; 
					yya[yyac] = new YYARec(311,-167 );yyac++; 
					yya[yyac] = new YYARec(312,-167 );yyac++; 
					yya[yyac] = new YYARec(313,-167 );yyac++; 
					yya[yyac] = new YYARec(314,-167 );yyac++; 
					yya[yyac] = new YYARec(315,-167 );yyac++; 
					yya[yyac] = new YYARec(316,-167 );yyac++; 
					yya[yyac] = new YYARec(317,-167 );yyac++; 
					yya[yyac] = new YYARec(318,-167 );yyac++; 
					yya[yyac] = new YYARec(319,-167 );yyac++; 
					yya[yyac] = new YYARec(320,-167 );yyac++; 
					yya[yyac] = new YYARec(321,-167 );yyac++; 
					yya[yyac] = new YYARec(322,-167 );yyac++; 
					yya[yyac] = new YYARec(323,-167 );yyac++; 
					yya[yyac] = new YYARec(324,-167 );yyac++; 
					yya[yyac] = new YYARec(325,-167 );yyac++; 
					yya[yyac] = new YYARec(268,-184 );yyac++; 
					yya[yyac] = new YYARec(289,-184 );yyac++; 
					yya[yyac] = new YYARec(290,-184 );yyac++; 
					yya[yyac] = new YYARec(291,-184 );yyac++; 
					yya[yyac] = new YYARec(292,-184 );yyac++; 
					yya[yyac] = new YYARec(293,-184 );yyac++; 
					yya[yyac] = new YYARec(297,-184 );yyac++; 
					yya[yyac] = new YYARec(258,222);yyac++; 
					yya[yyac] = new YYARec(263,-178 );yyac++; 
					yya[yyac] = new YYARec(268,-178 );yyac++; 
					yya[yyac] = new YYARec(282,-178 );yyac++; 
					yya[yyac] = new YYARec(283,-178 );yyac++; 
					yya[yyac] = new YYARec(287,-178 );yyac++; 
					yya[yyac] = new YYARec(289,-178 );yyac++; 
					yya[yyac] = new YYARec(290,-178 );yyac++; 
					yya[yyac] = new YYARec(291,-178 );yyac++; 
					yya[yyac] = new YYARec(292,-178 );yyac++; 
					yya[yyac] = new YYARec(293,-178 );yyac++; 
					yya[yyac] = new YYARec(297,-178 );yyac++; 
					yya[yyac] = new YYARec(298,-178 );yyac++; 
					yya[yyac] = new YYARec(299,-178 );yyac++; 
					yya[yyac] = new YYARec(300,-178 );yyac++; 
					yya[yyac] = new YYARec(301,-178 );yyac++; 
					yya[yyac] = new YYARec(302,-178 );yyac++; 
					yya[yyac] = new YYARec(303,-178 );yyac++; 
					yya[yyac] = new YYARec(304,-178 );yyac++; 
					yya[yyac] = new YYARec(305,-178 );yyac++; 
					yya[yyac] = new YYARec(306,-178 );yyac++; 
					yya[yyac] = new YYARec(307,-178 );yyac++; 
					yya[yyac] = new YYARec(308,-178 );yyac++; 
					yya[yyac] = new YYARec(309,-178 );yyac++; 
					yya[yyac] = new YYARec(310,-178 );yyac++; 
					yya[yyac] = new YYARec(311,-178 );yyac++; 
					yya[yyac] = new YYARec(312,-178 );yyac++; 
					yya[yyac] = new YYARec(313,-178 );yyac++; 
					yya[yyac] = new YYARec(314,-178 );yyac++; 
					yya[yyac] = new YYARec(315,-178 );yyac++; 
					yya[yyac] = new YYARec(316,-178 );yyac++; 
					yya[yyac] = new YYARec(317,-178 );yyac++; 
					yya[yyac] = new YYARec(318,-178 );yyac++; 
					yya[yyac] = new YYARec(319,-178 );yyac++; 
					yya[yyac] = new YYARec(320,-178 );yyac++; 
					yya[yyac] = new YYARec(321,-178 );yyac++; 
					yya[yyac] = new YYARec(322,-178 );yyac++; 
					yya[yyac] = new YYARec(323,-178 );yyac++; 
					yya[yyac] = new YYARec(324,-178 );yyac++; 
					yya[yyac] = new YYARec(325,-178 );yyac++; 
					yya[yyac] = new YYARec(263,71);yyac++; 
					yya[yyac] = new YYARec(282,-94 );yyac++; 
					yya[yyac] = new YYARec(283,-94 );yyac++; 
					yya[yyac] = new YYARec(287,-94 );yyac++; 
					yya[yyac] = new YYARec(298,-94 );yyac++; 
					yya[yyac] = new YYARec(299,-94 );yyac++; 
					yya[yyac] = new YYARec(300,-94 );yyac++; 
					yya[yyac] = new YYARec(301,-94 );yyac++; 
					yya[yyac] = new YYARec(302,-94 );yyac++; 
					yya[yyac] = new YYARec(303,-94 );yyac++; 
					yya[yyac] = new YYARec(304,-94 );yyac++; 
					yya[yyac] = new YYARec(305,-94 );yyac++; 
					yya[yyac] = new YYARec(306,-94 );yyac++; 
					yya[yyac] = new YYARec(307,-94 );yyac++; 
					yya[yyac] = new YYARec(309,-94 );yyac++; 
					yya[yyac] = new YYARec(310,-94 );yyac++; 
					yya[yyac] = new YYARec(311,-94 );yyac++; 
					yya[yyac] = new YYARec(312,-94 );yyac++; 
					yya[yyac] = new YYARec(313,-94 );yyac++; 
					yya[yyac] = new YYARec(314,-94 );yyac++; 
					yya[yyac] = new YYARec(315,-94 );yyac++; 
					yya[yyac] = new YYARec(316,-94 );yyac++; 
					yya[yyac] = new YYARec(317,-94 );yyac++; 
					yya[yyac] = new YYARec(318,-94 );yyac++; 
					yya[yyac] = new YYARec(319,-94 );yyac++; 
					yya[yyac] = new YYARec(320,-94 );yyac++; 
					yya[yyac] = new YYARec(321,-94 );yyac++; 
					yya[yyac] = new YYARec(322,-94 );yyac++; 
					yya[yyac] = new YYARec(323,-94 );yyac++; 
					yya[yyac] = new YYARec(324,-94 );yyac++; 
					yya[yyac] = new YYARec(325,-94 );yyac++; 
					yya[yyac] = new YYARec(258,224);yyac++; 
					yya[yyac] = new YYARec(267,225);yyac++; 
					yya[yyac] = new YYARec(257,160);yyac++; 
					yya[yyac] = new YYARec(259,161);yyac++; 
					yya[yyac] = new YYARec(301,27);yyac++; 
					yya[yyac] = new YYARec(302,28);yyac++; 
					yya[yyac] = new YYARec(306,162);yyac++; 
					yya[yyac] = new YYARec(311,163);yyac++; 
					yya[yyac] = new YYARec(312,164);yyac++; 
					yya[yyac] = new YYARec(315,165);yyac++; 
					yya[yyac] = new YYARec(317,166);yyac++; 
					yya[yyac] = new YYARec(319,167);yyac++; 
					yya[yyac] = new YYARec(320,168);yyac++; 
					yya[yyac] = new YYARec(323,64);yyac++; 
					yya[yyac] = new YYARec(260,-63 );yyac++; 
					yya[yyac] = new YYARec(261,-63 );yyac++; 
					yya[yyac] = new YYARec(267,-63 );yyac++; 
					yya[yyac] = new YYARec(309,30);yyac++; 
					yya[yyac] = new YYARec(315,34);yyac++; 
					yya[yyac] = new YYARec(317,35);yyac++; 
					yya[yyac] = new YYARec(323,64);yyac++; 
					yya[yyac] = new YYARec(309,30);yyac++; 
					yya[yyac] = new YYARec(315,34);yyac++; 
					yya[yyac] = new YYARec(317,35);yyac++; 
					yya[yyac] = new YYARec(323,64);yyac++; 
					yya[yyac] = new YYARec(282,94);yyac++; 
					yya[yyac] = new YYARec(283,95);yyac++; 
					yya[yyac] = new YYARec(287,96);yyac++; 
					yya[yyac] = new YYARec(321,97);yyac++; 
					yya[yyac] = new YYARec(258,-53 );yyac++; 
					yya[yyac] = new YYARec(258,232);yyac++; 
					yya[yyac] = new YYARec(301,27);yyac++; 
					yya[yyac] = new YYARec(302,28);yyac++; 
					yya[yyac] = new YYARec(305,235);yyac++; 
					yya[yyac] = new YYARec(306,162);yyac++; 
					yya[yyac] = new YYARec(311,163);yyac++; 
					yya[yyac] = new YYARec(312,164);yyac++; 
					yya[yyac] = new YYARec(313,236);yyac++; 
					yya[yyac] = new YYARec(315,165);yyac++; 
					yya[yyac] = new YYARec(316,237);yyac++; 
					yya[yyac] = new YYARec(317,166);yyac++; 
					yya[yyac] = new YYARec(318,238);yyac++; 
					yya[yyac] = new YYARec(319,167);yyac++; 
					yya[yyac] = new YYARec(320,168);yyac++; 
					yya[yyac] = new YYARec(323,64);yyac++; 
					yya[yyac] = new YYARec(282,94);yyac++; 
					yya[yyac] = new YYARec(283,95);yyac++; 
					yya[yyac] = new YYARec(287,96);yyac++; 
					yya[yyac] = new YYARec(298,42);yyac++; 
					yya[yyac] = new YYARec(299,43);yyac++; 
					yya[yyac] = new YYARec(300,44);yyac++; 
					yya[yyac] = new YYARec(301,45);yyac++; 
					yya[yyac] = new YYARec(302,46);yyac++; 
					yya[yyac] = new YYARec(303,47);yyac++; 
					yya[yyac] = new YYARec(304,48);yyac++; 
					yya[yyac] = new YYARec(305,49);yyac++; 
					yya[yyac] = new YYARec(306,50);yyac++; 
					yya[yyac] = new YYARec(307,51);yyac++; 
					yya[yyac] = new YYARec(308,127);yyac++; 
					yya[yyac] = new YYARec(309,52);yyac++; 
					yya[yyac] = new YYARec(310,53);yyac++; 
					yya[yyac] = new YYARec(311,54);yyac++; 
					yya[yyac] = new YYARec(312,55);yyac++; 
					yya[yyac] = new YYARec(313,56);yyac++; 
					yya[yyac] = new YYARec(314,57);yyac++; 
					yya[yyac] = new YYARec(315,58);yyac++; 
					yya[yyac] = new YYARec(316,59);yyac++; 
					yya[yyac] = new YYARec(317,60);yyac++; 
					yya[yyac] = new YYARec(318,61);yyac++; 
					yya[yyac] = new YYARec(319,62);yyac++; 
					yya[yyac] = new YYARec(320,63);yyac++; 
					yya[yyac] = new YYARec(321,97);yyac++; 
					yya[yyac] = new YYARec(322,98);yyac++; 
					yya[yyac] = new YYARec(323,64);yyac++; 
					yya[yyac] = new YYARec(324,81);yyac++; 
					yya[yyac] = new YYARec(325,99);yyac++; 
					yya[yyac] = new YYARec(257,138);yyac++; 
					yya[yyac] = new YYARec(258,139);yyac++; 
					yya[yyac] = new YYARec(259,140);yyac++; 
					yya[yyac] = new YYARec(264,141);yyac++; 
					yya[yyac] = new YYARec(266,142);yyac++; 
					yya[yyac] = new YYARec(288,143);yyac++; 
					yya[yyac] = new YYARec(294,144);yyac++; 
					yya[yyac] = new YYARec(295,145);yyac++; 
					yya[yyac] = new YYARec(296,146);yyac++; 
					yya[yyac] = new YYARec(298,42);yyac++; 
					yya[yyac] = new YYARec(299,43);yyac++; 
					yya[yyac] = new YYARec(300,44);yyac++; 
					yya[yyac] = new YYARec(301,45);yyac++; 
					yya[yyac] = new YYARec(302,46);yyac++; 
					yya[yyac] = new YYARec(303,47);yyac++; 
					yya[yyac] = new YYARec(304,48);yyac++; 
					yya[yyac] = new YYARec(305,49);yyac++; 
					yya[yyac] = new YYARec(306,50);yyac++; 
					yya[yyac] = new YYARec(307,147);yyac++; 
					yya[yyac] = new YYARec(309,52);yyac++; 
					yya[yyac] = new YYARec(310,53);yyac++; 
					yya[yyac] = new YYARec(311,54);yyac++; 
					yya[yyac] = new YYARec(312,55);yyac++; 
					yya[yyac] = new YYARec(313,56);yyac++; 
					yya[yyac] = new YYARec(314,148);yyac++; 
					yya[yyac] = new YYARec(315,58);yyac++; 
					yya[yyac] = new YYARec(316,59);yyac++; 
					yya[yyac] = new YYARec(317,60);yyac++; 
					yya[yyac] = new YYARec(318,149);yyac++; 
					yya[yyac] = new YYARec(319,62);yyac++; 
					yya[yyac] = new YYARec(320,150);yyac++; 
					yya[yyac] = new YYARec(323,151);yyac++; 
					yya[yyac] = new YYARec(260,-79 );yyac++; 
					yya[yyac] = new YYARec(261,-79 );yyac++; 
					yya[yyac] = new YYARec(267,-79 );yyac++; 
					yya[yyac] = new YYARec(274,212);yyac++; 
					yya[yyac] = new YYARec(282,94);yyac++; 
					yya[yyac] = new YYARec(283,95);yyac++; 
					yya[yyac] = new YYARec(287,96);yyac++; 
					yya[yyac] = new YYARec(298,42);yyac++; 
					yya[yyac] = new YYARec(299,43);yyac++; 
					yya[yyac] = new YYARec(300,44);yyac++; 
					yya[yyac] = new YYARec(301,45);yyac++; 
					yya[yyac] = new YYARec(302,46);yyac++; 
					yya[yyac] = new YYARec(303,47);yyac++; 
					yya[yyac] = new YYARec(304,213);yyac++; 
					yya[yyac] = new YYARec(305,49);yyac++; 
					yya[yyac] = new YYARec(306,50);yyac++; 
					yya[yyac] = new YYARec(307,51);yyac++; 
					yya[yyac] = new YYARec(309,52);yyac++; 
					yya[yyac] = new YYARec(310,53);yyac++; 
					yya[yyac] = new YYARec(311,54);yyac++; 
					yya[yyac] = new YYARec(312,55);yyac++; 
					yya[yyac] = new YYARec(313,56);yyac++; 
					yya[yyac] = new YYARec(314,214);yyac++; 
					yya[yyac] = new YYARec(315,215);yyac++; 
					yya[yyac] = new YYARec(316,59);yyac++; 
					yya[yyac] = new YYARec(317,60);yyac++; 
					yya[yyac] = new YYARec(318,61);yyac++; 
					yya[yyac] = new YYARec(319,62);yyac++; 
					yya[yyac] = new YYARec(320,63);yyac++; 
					yya[yyac] = new YYARec(321,216);yyac++; 
					yya[yyac] = new YYARec(322,217);yyac++; 
					yya[yyac] = new YYARec(323,64);yyac++; 
					yya[yyac] = new YYARec(282,94);yyac++; 
					yya[yyac] = new YYARec(283,95);yyac++; 
					yya[yyac] = new YYARec(287,96);yyac++; 
					yya[yyac] = new YYARec(298,42);yyac++; 
					yya[yyac] = new YYARec(299,43);yyac++; 
					yya[yyac] = new YYARec(300,44);yyac++; 
					yya[yyac] = new YYARec(301,45);yyac++; 
					yya[yyac] = new YYARec(302,46);yyac++; 
					yya[yyac] = new YYARec(303,47);yyac++; 
					yya[yyac] = new YYARec(304,48);yyac++; 
					yya[yyac] = new YYARec(305,49);yyac++; 
					yya[yyac] = new YYARec(306,50);yyac++; 
					yya[yyac] = new YYARec(307,51);yyac++; 
					yya[yyac] = new YYARec(308,127);yyac++; 
					yya[yyac] = new YYARec(309,52);yyac++; 
					yya[yyac] = new YYARec(310,53);yyac++; 
					yya[yyac] = new YYARec(311,54);yyac++; 
					yya[yyac] = new YYARec(312,55);yyac++; 
					yya[yyac] = new YYARec(313,56);yyac++; 
					yya[yyac] = new YYARec(314,57);yyac++; 
					yya[yyac] = new YYARec(315,58);yyac++; 
					yya[yyac] = new YYARec(316,59);yyac++; 
					yya[yyac] = new YYARec(317,60);yyac++; 
					yya[yyac] = new YYARec(318,61);yyac++; 
					yya[yyac] = new YYARec(319,62);yyac++; 
					yya[yyac] = new YYARec(320,63);yyac++; 
					yya[yyac] = new YYARec(321,97);yyac++; 
					yya[yyac] = new YYARec(322,98);yyac++; 
					yya[yyac] = new YYARec(323,64);yyac++; 
					yya[yyac] = new YYARec(324,81);yyac++; 
					yya[yyac] = new YYARec(325,99);yyac++; 
					yya[yyac] = new YYARec(257,138);yyac++; 
					yya[yyac] = new YYARec(258,139);yyac++; 
					yya[yyac] = new YYARec(259,140);yyac++; 
					yya[yyac] = new YYARec(264,141);yyac++; 
					yya[yyac] = new YYARec(266,142);yyac++; 
					yya[yyac] = new YYARec(288,143);yyac++; 
					yya[yyac] = new YYARec(294,144);yyac++; 
					yya[yyac] = new YYARec(295,145);yyac++; 
					yya[yyac] = new YYARec(296,146);yyac++; 
					yya[yyac] = new YYARec(298,42);yyac++; 
					yya[yyac] = new YYARec(299,43);yyac++; 
					yya[yyac] = new YYARec(300,44);yyac++; 
					yya[yyac] = new YYARec(301,45);yyac++; 
					yya[yyac] = new YYARec(302,46);yyac++; 
					yya[yyac] = new YYARec(303,47);yyac++; 
					yya[yyac] = new YYARec(304,48);yyac++; 
					yya[yyac] = new YYARec(305,49);yyac++; 
					yya[yyac] = new YYARec(306,50);yyac++; 
					yya[yyac] = new YYARec(307,147);yyac++; 
					yya[yyac] = new YYARec(309,52);yyac++; 
					yya[yyac] = new YYARec(310,53);yyac++; 
					yya[yyac] = new YYARec(311,54);yyac++; 
					yya[yyac] = new YYARec(312,55);yyac++; 
					yya[yyac] = new YYARec(313,56);yyac++; 
					yya[yyac] = new YYARec(314,148);yyac++; 
					yya[yyac] = new YYARec(315,58);yyac++; 
					yya[yyac] = new YYARec(316,59);yyac++; 
					yya[yyac] = new YYARec(317,60);yyac++; 
					yya[yyac] = new YYARec(318,149);yyac++; 
					yya[yyac] = new YYARec(319,62);yyac++; 
					yya[yyac] = new YYARec(320,150);yyac++; 
					yya[yyac] = new YYARec(323,151);yyac++; 
					yya[yyac] = new YYARec(260,-79 );yyac++; 
					yya[yyac] = new YYARec(261,-79 );yyac++; 
					yya[yyac] = new YYARec(257,138);yyac++; 
					yya[yyac] = new YYARec(258,139);yyac++; 
					yya[yyac] = new YYARec(259,140);yyac++; 
					yya[yyac] = new YYARec(264,141);yyac++; 
					yya[yyac] = new YYARec(266,142);yyac++; 
					yya[yyac] = new YYARec(288,143);yyac++; 
					yya[yyac] = new YYARec(294,144);yyac++; 
					yya[yyac] = new YYARec(295,145);yyac++; 
					yya[yyac] = new YYARec(296,146);yyac++; 
					yya[yyac] = new YYARec(298,42);yyac++; 
					yya[yyac] = new YYARec(299,43);yyac++; 
					yya[yyac] = new YYARec(300,44);yyac++; 
					yya[yyac] = new YYARec(301,45);yyac++; 
					yya[yyac] = new YYARec(302,46);yyac++; 
					yya[yyac] = new YYARec(303,47);yyac++; 
					yya[yyac] = new YYARec(304,48);yyac++; 
					yya[yyac] = new YYARec(305,49);yyac++; 
					yya[yyac] = new YYARec(306,50);yyac++; 
					yya[yyac] = new YYARec(307,147);yyac++; 
					yya[yyac] = new YYARec(309,52);yyac++; 
					yya[yyac] = new YYARec(310,53);yyac++; 
					yya[yyac] = new YYARec(311,54);yyac++; 
					yya[yyac] = new YYARec(312,55);yyac++; 
					yya[yyac] = new YYARec(313,56);yyac++; 
					yya[yyac] = new YYARec(314,148);yyac++; 
					yya[yyac] = new YYARec(315,58);yyac++; 
					yya[yyac] = new YYARec(316,59);yyac++; 
					yya[yyac] = new YYARec(317,60);yyac++; 
					yya[yyac] = new YYARec(318,149);yyac++; 
					yya[yyac] = new YYARec(319,62);yyac++; 
					yya[yyac] = new YYARec(320,150);yyac++; 
					yya[yyac] = new YYARec(323,151);yyac++; 
					yya[yyac] = new YYARec(260,-79 );yyac++; 
					yya[yyac] = new YYARec(261,-79 );yyac++; 
					yya[yyac] = new YYARec(258,252);yyac++; 
					yya[yyac] = new YYARec(267,253);yyac++; 
					yya[yyac] = new YYARec(274,254);yyac++; 
					yya[yyac] = new YYARec(274,212);yyac++; 
					yya[yyac] = new YYARec(282,94);yyac++; 
					yya[yyac] = new YYARec(283,95);yyac++; 
					yya[yyac] = new YYARec(287,96);yyac++; 
					yya[yyac] = new YYARec(298,42);yyac++; 
					yya[yyac] = new YYARec(299,43);yyac++; 
					yya[yyac] = new YYARec(300,44);yyac++; 
					yya[yyac] = new YYARec(301,45);yyac++; 
					yya[yyac] = new YYARec(302,46);yyac++; 
					yya[yyac] = new YYARec(303,47);yyac++; 
					yya[yyac] = new YYARec(304,213);yyac++; 
					yya[yyac] = new YYARec(305,49);yyac++; 
					yya[yyac] = new YYARec(306,50);yyac++; 
					yya[yyac] = new YYARec(307,51);yyac++; 
					yya[yyac] = new YYARec(309,52);yyac++; 
					yya[yyac] = new YYARec(310,53);yyac++; 
					yya[yyac] = new YYARec(311,54);yyac++; 
					yya[yyac] = new YYARec(312,55);yyac++; 
					yya[yyac] = new YYARec(313,56);yyac++; 
					yya[yyac] = new YYARec(314,214);yyac++; 
					yya[yyac] = new YYARec(315,215);yyac++; 
					yya[yyac] = new YYARec(316,59);yyac++; 
					yya[yyac] = new YYARec(317,60);yyac++; 
					yya[yyac] = new YYARec(318,61);yyac++; 
					yya[yyac] = new YYARec(319,62);yyac++; 
					yya[yyac] = new YYARec(320,63);yyac++; 
					yya[yyac] = new YYARec(321,216);yyac++; 
					yya[yyac] = new YYARec(322,217);yyac++; 
					yya[yyac] = new YYARec(323,64);yyac++; 
					yya[yyac] = new YYARec(284,257);yyac++; 
					yya[yyac] = new YYARec(285,258);yyac++; 
					yya[yyac] = new YYARec(286,259);yyac++; 
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
					yya[yyac] = new YYARec(278,-110 );yyac++; 
					yya[yyac] = new YYARec(279,-110 );yyac++; 
					yya[yyac] = new YYARec(280,-110 );yyac++; 
					yya[yyac] = new YYARec(281,-110 );yyac++; 
					yya[yyac] = new YYARec(282,-110 );yyac++; 
					yya[yyac] = new YYARec(283,-110 );yyac++; 
					yya[yyac] = new YYARec(282,261);yyac++; 
					yya[yyac] = new YYARec(283,262);yyac++; 
					yya[yyac] = new YYARec(258,-108 );yyac++; 
					yya[yyac] = new YYARec(266,-108 );yyac++; 
					yya[yyac] = new YYARec(269,-108 );yyac++; 
					yya[yyac] = new YYARec(270,-108 );yyac++; 
					yya[yyac] = new YYARec(271,-108 );yyac++; 
					yya[yyac] = new YYARec(272,-108 );yyac++; 
					yya[yyac] = new YYARec(273,-108 );yyac++; 
					yya[yyac] = new YYARec(275,-108 );yyac++; 
					yya[yyac] = new YYARec(276,-108 );yyac++; 
					yya[yyac] = new YYARec(277,-108 );yyac++; 
					yya[yyac] = new YYARec(278,-108 );yyac++; 
					yya[yyac] = new YYARec(279,-108 );yyac++; 
					yya[yyac] = new YYARec(280,-108 );yyac++; 
					yya[yyac] = new YYARec(281,-108 );yyac++; 
					yya[yyac] = new YYARec(278,264);yyac++; 
					yya[yyac] = new YYARec(279,265);yyac++; 
					yya[yyac] = new YYARec(280,266);yyac++; 
					yya[yyac] = new YYARec(281,267);yyac++; 
					yya[yyac] = new YYARec(258,-106 );yyac++; 
					yya[yyac] = new YYARec(266,-106 );yyac++; 
					yya[yyac] = new YYARec(269,-106 );yyac++; 
					yya[yyac] = new YYARec(270,-106 );yyac++; 
					yya[yyac] = new YYARec(271,-106 );yyac++; 
					yya[yyac] = new YYARec(272,-106 );yyac++; 
					yya[yyac] = new YYARec(273,-106 );yyac++; 
					yya[yyac] = new YYARec(275,-106 );yyac++; 
					yya[yyac] = new YYARec(276,-106 );yyac++; 
					yya[yyac] = new YYARec(277,-106 );yyac++; 
					yya[yyac] = new YYARec(276,269);yyac++; 
					yya[yyac] = new YYARec(277,270);yyac++; 
					yya[yyac] = new YYARec(258,-105 );yyac++; 
					yya[yyac] = new YYARec(266,-105 );yyac++; 
					yya[yyac] = new YYARec(269,-105 );yyac++; 
					yya[yyac] = new YYARec(270,-105 );yyac++; 
					yya[yyac] = new YYARec(271,-105 );yyac++; 
					yya[yyac] = new YYARec(272,-105 );yyac++; 
					yya[yyac] = new YYARec(273,-105 );yyac++; 
					yya[yyac] = new YYARec(275,-105 );yyac++; 
					yya[yyac] = new YYARec(273,271);yyac++; 
					yya[yyac] = new YYARec(258,-103 );yyac++; 
					yya[yyac] = new YYARec(266,-103 );yyac++; 
					yya[yyac] = new YYARec(269,-103 );yyac++; 
					yya[yyac] = new YYARec(270,-103 );yyac++; 
					yya[yyac] = new YYARec(271,-103 );yyac++; 
					yya[yyac] = new YYARec(272,-103 );yyac++; 
					yya[yyac] = new YYARec(275,-103 );yyac++; 
					yya[yyac] = new YYARec(272,272);yyac++; 
					yya[yyac] = new YYARec(258,-101 );yyac++; 
					yya[yyac] = new YYARec(266,-101 );yyac++; 
					yya[yyac] = new YYARec(269,-101 );yyac++; 
					yya[yyac] = new YYARec(270,-101 );yyac++; 
					yya[yyac] = new YYARec(271,-101 );yyac++; 
					yya[yyac] = new YYARec(275,-101 );yyac++; 
					yya[yyac] = new YYARec(271,273);yyac++; 
					yya[yyac] = new YYARec(258,-99 );yyac++; 
					yya[yyac] = new YYARec(266,-99 );yyac++; 
					yya[yyac] = new YYARec(269,-99 );yyac++; 
					yya[yyac] = new YYARec(270,-99 );yyac++; 
					yya[yyac] = new YYARec(275,-99 );yyac++; 
					yya[yyac] = new YYARec(270,274);yyac++; 
					yya[yyac] = new YYARec(258,-97 );yyac++; 
					yya[yyac] = new YYARec(266,-97 );yyac++; 
					yya[yyac] = new YYARec(269,-97 );yyac++; 
					yya[yyac] = new YYARec(275,-97 );yyac++; 
					yya[yyac] = new YYARec(269,275);yyac++; 
					yya[yyac] = new YYARec(258,-95 );yyac++; 
					yya[yyac] = new YYARec(266,-95 );yyac++; 
					yya[yyac] = new YYARec(275,-95 );yyac++; 
					yya[yyac] = new YYARec(289,184);yyac++; 
					yya[yyac] = new YYARec(290,185);yyac++; 
					yya[yyac] = new YYARec(291,186);yyac++; 
					yya[yyac] = new YYARec(292,187);yyac++; 
					yya[yyac] = new YYARec(293,188);yyac++; 
					yya[yyac] = new YYARec(258,-119 );yyac++; 
					yya[yyac] = new YYARec(269,-119 );yyac++; 
					yya[yyac] = new YYARec(270,-119 );yyac++; 
					yya[yyac] = new YYARec(271,-119 );yyac++; 
					yya[yyac] = new YYARec(272,-119 );yyac++; 
					yya[yyac] = new YYARec(273,-119 );yyac++; 
					yya[yyac] = new YYARec(276,-119 );yyac++; 
					yya[yyac] = new YYARec(277,-119 );yyac++; 
					yya[yyac] = new YYARec(278,-119 );yyac++; 
					yya[yyac] = new YYARec(279,-119 );yyac++; 
					yya[yyac] = new YYARec(280,-119 );yyac++; 
					yya[yyac] = new YYARec(281,-119 );yyac++; 
					yya[yyac] = new YYARec(282,-119 );yyac++; 
					yya[yyac] = new YYARec(283,-119 );yyac++; 
					yya[yyac] = new YYARec(284,-119 );yyac++; 
					yya[yyac] = new YYARec(285,-119 );yyac++; 
					yya[yyac] = new YYARec(286,-119 );yyac++; 
					yya[yyac] = new YYARec(274,212);yyac++; 
					yya[yyac] = new YYARec(282,94);yyac++; 
					yya[yyac] = new YYARec(283,95);yyac++; 
					yya[yyac] = new YYARec(287,96);yyac++; 
					yya[yyac] = new YYARec(298,42);yyac++; 
					yya[yyac] = new YYARec(299,43);yyac++; 
					yya[yyac] = new YYARec(300,44);yyac++; 
					yya[yyac] = new YYARec(301,45);yyac++; 
					yya[yyac] = new YYARec(302,46);yyac++; 
					yya[yyac] = new YYARec(303,47);yyac++; 
					yya[yyac] = new YYARec(304,213);yyac++; 
					yya[yyac] = new YYARec(305,49);yyac++; 
					yya[yyac] = new YYARec(306,50);yyac++; 
					yya[yyac] = new YYARec(307,51);yyac++; 
					yya[yyac] = new YYARec(309,52);yyac++; 
					yya[yyac] = new YYARec(310,53);yyac++; 
					yya[yyac] = new YYARec(311,54);yyac++; 
					yya[yyac] = new YYARec(312,55);yyac++; 
					yya[yyac] = new YYARec(313,56);yyac++; 
					yya[yyac] = new YYARec(314,214);yyac++; 
					yya[yyac] = new YYARec(315,215);yyac++; 
					yya[yyac] = new YYARec(316,59);yyac++; 
					yya[yyac] = new YYARec(317,60);yyac++; 
					yya[yyac] = new YYARec(318,61);yyac++; 
					yya[yyac] = new YYARec(319,62);yyac++; 
					yya[yyac] = new YYARec(320,63);yyac++; 
					yya[yyac] = new YYARec(321,216);yyac++; 
					yya[yyac] = new YYARec(322,217);yyac++; 
					yya[yyac] = new YYARec(323,64);yyac++; 
					yya[yyac] = new YYARec(274,-144 );yyac++; 
					yya[yyac] = new YYARec(258,-195 );yyac++; 
					yya[yyac] = new YYARec(266,-195 );yyac++; 
					yya[yyac] = new YYARec(269,-195 );yyac++; 
					yya[yyac] = new YYARec(270,-195 );yyac++; 
					yya[yyac] = new YYARec(271,-195 );yyac++; 
					yya[yyac] = new YYARec(272,-195 );yyac++; 
					yya[yyac] = new YYARec(273,-195 );yyac++; 
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
					yya[yyac] = new YYARec(286,-195 );yyac++; 
					yya[yyac] = new YYARec(289,-195 );yyac++; 
					yya[yyac] = new YYARec(290,-195 );yyac++; 
					yya[yyac] = new YYARec(291,-195 );yyac++; 
					yya[yyac] = new YYARec(292,-195 );yyac++; 
					yya[yyac] = new YYARec(293,-195 );yyac++; 
					yya[yyac] = new YYARec(297,-195 );yyac++; 
					yya[yyac] = new YYARec(274,-142 );yyac++; 
					yya[yyac] = new YYARec(258,-187 );yyac++; 
					yya[yyac] = new YYARec(266,-187 );yyac++; 
					yya[yyac] = new YYARec(269,-187 );yyac++; 
					yya[yyac] = new YYARec(270,-187 );yyac++; 
					yya[yyac] = new YYARec(271,-187 );yyac++; 
					yya[yyac] = new YYARec(272,-187 );yyac++; 
					yya[yyac] = new YYARec(273,-187 );yyac++; 
					yya[yyac] = new YYARec(275,-187 );yyac++; 
					yya[yyac] = new YYARec(276,-187 );yyac++; 
					yya[yyac] = new YYARec(277,-187 );yyac++; 
					yya[yyac] = new YYARec(278,-187 );yyac++; 
					yya[yyac] = new YYARec(279,-187 );yyac++; 
					yya[yyac] = new YYARec(280,-187 );yyac++; 
					yya[yyac] = new YYARec(281,-187 );yyac++; 
					yya[yyac] = new YYARec(282,-187 );yyac++; 
					yya[yyac] = new YYARec(283,-187 );yyac++; 
					yya[yyac] = new YYARec(284,-187 );yyac++; 
					yya[yyac] = new YYARec(285,-187 );yyac++; 
					yya[yyac] = new YYARec(286,-187 );yyac++; 
					yya[yyac] = new YYARec(289,-187 );yyac++; 
					yya[yyac] = new YYARec(290,-187 );yyac++; 
					yya[yyac] = new YYARec(291,-187 );yyac++; 
					yya[yyac] = new YYARec(292,-187 );yyac++; 
					yya[yyac] = new YYARec(293,-187 );yyac++; 
					yya[yyac] = new YYARec(297,-187 );yyac++; 
					yya[yyac] = new YYARec(274,-143 );yyac++; 
					yya[yyac] = new YYARec(258,-199 );yyac++; 
					yya[yyac] = new YYARec(266,-199 );yyac++; 
					yya[yyac] = new YYARec(269,-199 );yyac++; 
					yya[yyac] = new YYARec(270,-199 );yyac++; 
					yya[yyac] = new YYARec(271,-199 );yyac++; 
					yya[yyac] = new YYARec(272,-199 );yyac++; 
					yya[yyac] = new YYARec(273,-199 );yyac++; 
					yya[yyac] = new YYARec(275,-199 );yyac++; 
					yya[yyac] = new YYARec(276,-199 );yyac++; 
					yya[yyac] = new YYARec(277,-199 );yyac++; 
					yya[yyac] = new YYARec(278,-199 );yyac++; 
					yya[yyac] = new YYARec(279,-199 );yyac++; 
					yya[yyac] = new YYARec(280,-199 );yyac++; 
					yya[yyac] = new YYARec(281,-199 );yyac++; 
					yya[yyac] = new YYARec(282,-199 );yyac++; 
					yya[yyac] = new YYARec(283,-199 );yyac++; 
					yya[yyac] = new YYARec(284,-199 );yyac++; 
					yya[yyac] = new YYARec(285,-199 );yyac++; 
					yya[yyac] = new YYARec(286,-199 );yyac++; 
					yya[yyac] = new YYARec(289,-199 );yyac++; 
					yya[yyac] = new YYARec(290,-199 );yyac++; 
					yya[yyac] = new YYARec(291,-199 );yyac++; 
					yya[yyac] = new YYARec(292,-199 );yyac++; 
					yya[yyac] = new YYARec(293,-199 );yyac++; 
					yya[yyac] = new YYARec(297,-199 );yyac++; 
					yya[yyac] = new YYARec(257,138);yyac++; 
					yya[yyac] = new YYARec(258,139);yyac++; 
					yya[yyac] = new YYARec(259,140);yyac++; 
					yya[yyac] = new YYARec(264,141);yyac++; 
					yya[yyac] = new YYARec(266,142);yyac++; 
					yya[yyac] = new YYARec(288,143);yyac++; 
					yya[yyac] = new YYARec(294,144);yyac++; 
					yya[yyac] = new YYARec(295,145);yyac++; 
					yya[yyac] = new YYARec(296,146);yyac++; 
					yya[yyac] = new YYARec(298,42);yyac++; 
					yya[yyac] = new YYARec(299,43);yyac++; 
					yya[yyac] = new YYARec(300,44);yyac++; 
					yya[yyac] = new YYARec(301,45);yyac++; 
					yya[yyac] = new YYARec(302,46);yyac++; 
					yya[yyac] = new YYARec(303,47);yyac++; 
					yya[yyac] = new YYARec(304,48);yyac++; 
					yya[yyac] = new YYARec(305,49);yyac++; 
					yya[yyac] = new YYARec(306,50);yyac++; 
					yya[yyac] = new YYARec(307,147);yyac++; 
					yya[yyac] = new YYARec(309,52);yyac++; 
					yya[yyac] = new YYARec(310,53);yyac++; 
					yya[yyac] = new YYARec(311,54);yyac++; 
					yya[yyac] = new YYARec(312,55);yyac++; 
					yya[yyac] = new YYARec(313,56);yyac++; 
					yya[yyac] = new YYARec(314,148);yyac++; 
					yya[yyac] = new YYARec(315,58);yyac++; 
					yya[yyac] = new YYARec(316,59);yyac++; 
					yya[yyac] = new YYARec(317,60);yyac++; 
					yya[yyac] = new YYARec(318,149);yyac++; 
					yya[yyac] = new YYARec(319,62);yyac++; 
					yya[yyac] = new YYARec(320,150);yyac++; 
					yya[yyac] = new YYARec(323,151);yyac++; 
					yya[yyac] = new YYARec(267,-79 );yyac++; 
					yya[yyac] = new YYARec(266,279);yyac++; 
					yya[yyac] = new YYARec(266,280);yyac++; 
					yya[yyac] = new YYARec(257,138);yyac++; 
					yya[yyac] = new YYARec(258,139);yyac++; 
					yya[yyac] = new YYARec(259,140);yyac++; 
					yya[yyac] = new YYARec(264,141);yyac++; 
					yya[yyac] = new YYARec(266,142);yyac++; 
					yya[yyac] = new YYARec(288,143);yyac++; 
					yya[yyac] = new YYARec(294,144);yyac++; 
					yya[yyac] = new YYARec(295,145);yyac++; 
					yya[yyac] = new YYARec(296,146);yyac++; 
					yya[yyac] = new YYARec(298,42);yyac++; 
					yya[yyac] = new YYARec(299,43);yyac++; 
					yya[yyac] = new YYARec(300,44);yyac++; 
					yya[yyac] = new YYARec(301,45);yyac++; 
					yya[yyac] = new YYARec(302,46);yyac++; 
					yya[yyac] = new YYARec(303,47);yyac++; 
					yya[yyac] = new YYARec(304,48);yyac++; 
					yya[yyac] = new YYARec(305,49);yyac++; 
					yya[yyac] = new YYARec(306,50);yyac++; 
					yya[yyac] = new YYARec(307,147);yyac++; 
					yya[yyac] = new YYARec(309,52);yyac++; 
					yya[yyac] = new YYARec(310,53);yyac++; 
					yya[yyac] = new YYARec(311,54);yyac++; 
					yya[yyac] = new YYARec(312,55);yyac++; 
					yya[yyac] = new YYARec(313,56);yyac++; 
					yya[yyac] = new YYARec(314,148);yyac++; 
					yya[yyac] = new YYARec(315,58);yyac++; 
					yya[yyac] = new YYARec(316,59);yyac++; 
					yya[yyac] = new YYARec(317,60);yyac++; 
					yya[yyac] = new YYARec(318,149);yyac++; 
					yya[yyac] = new YYARec(319,62);yyac++; 
					yya[yyac] = new YYARec(320,150);yyac++; 
					yya[yyac] = new YYARec(323,151);yyac++; 
					yya[yyac] = new YYARec(260,-79 );yyac++; 
					yya[yyac] = new YYARec(261,-79 );yyac++; 
					yya[yyac] = new YYARec(267,-79 );yyac++; 
					yya[yyac] = new YYARec(282,94);yyac++; 
					yya[yyac] = new YYARec(283,95);yyac++; 
					yya[yyac] = new YYARec(287,96);yyac++; 
					yya[yyac] = new YYARec(298,42);yyac++; 
					yya[yyac] = new YYARec(299,43);yyac++; 
					yya[yyac] = new YYARec(300,44);yyac++; 
					yya[yyac] = new YYARec(301,45);yyac++; 
					yya[yyac] = new YYARec(302,46);yyac++; 
					yya[yyac] = new YYARec(303,47);yyac++; 
					yya[yyac] = new YYARec(304,48);yyac++; 
					yya[yyac] = new YYARec(305,49);yyac++; 
					yya[yyac] = new YYARec(306,50);yyac++; 
					yya[yyac] = new YYARec(307,51);yyac++; 
					yya[yyac] = new YYARec(309,52);yyac++; 
					yya[yyac] = new YYARec(310,53);yyac++; 
					yya[yyac] = new YYARec(311,54);yyac++; 
					yya[yyac] = new YYARec(312,55);yyac++; 
					yya[yyac] = new YYARec(313,56);yyac++; 
					yya[yyac] = new YYARec(314,57);yyac++; 
					yya[yyac] = new YYARec(315,58);yyac++; 
					yya[yyac] = new YYARec(316,59);yyac++; 
					yya[yyac] = new YYARec(317,60);yyac++; 
					yya[yyac] = new YYARec(318,61);yyac++; 
					yya[yyac] = new YYARec(319,62);yyac++; 
					yya[yyac] = new YYARec(320,63);yyac++; 
					yya[yyac] = new YYARec(321,97);yyac++; 
					yya[yyac] = new YYARec(322,98);yyac++; 
					yya[yyac] = new YYARec(323,64);yyac++; 
					yya[yyac] = new YYARec(324,81);yyac++; 
					yya[yyac] = new YYARec(325,99);yyac++; 
					yya[yyac] = new YYARec(257,160);yyac++; 
					yya[yyac] = new YYARec(259,161);yyac++; 
					yya[yyac] = new YYARec(301,27);yyac++; 
					yya[yyac] = new YYARec(302,28);yyac++; 
					yya[yyac] = new YYARec(306,162);yyac++; 
					yya[yyac] = new YYARec(311,163);yyac++; 
					yya[yyac] = new YYARec(312,164);yyac++; 
					yya[yyac] = new YYARec(315,165);yyac++; 
					yya[yyac] = new YYARec(317,166);yyac++; 
					yya[yyac] = new YYARec(319,167);yyac++; 
					yya[yyac] = new YYARec(320,168);yyac++; 
					yya[yyac] = new YYARec(323,64);yyac++; 
					yya[yyac] = new YYARec(260,-63 );yyac++; 
					yya[yyac] = new YYARec(261,-63 );yyac++; 
					yya[yyac] = new YYARec(267,-63 );yyac++; 
					yya[yyac] = new YYARec(258,289);yyac++; 
					yya[yyac] = new YYARec(258,290);yyac++; 
					yya[yyac] = new YYARec(321,112);yyac++; 
					yya[yyac] = new YYARec(263,291);yyac++; 
					yya[yyac] = new YYARec(258,-52 );yyac++; 
					yya[yyac] = new YYARec(258,292);yyac++; 
					yya[yyac] = new YYARec(257,18);yyac++; 
					yya[yyac] = new YYARec(258,19);yyac++; 
					yya[yyac] = new YYARec(259,20);yyac++; 
					yya[yyac] = new YYARec(262,21);yyac++; 
					yya[yyac] = new YYARec(264,22);yyac++; 
					yya[yyac] = new YYARec(265,23);yyac++; 
					yya[yyac] = new YYARec(267,24);yyac++; 
					yya[yyac] = new YYARec(299,25);yyac++; 
					yya[yyac] = new YYARec(300,26);yyac++; 
					yya[yyac] = new YYARec(301,27);yyac++; 
					yya[yyac] = new YYARec(302,28);yyac++; 
					yya[yyac] = new YYARec(303,29);yyac++; 
					yya[yyac] = new YYARec(309,30);yyac++; 
					yya[yyac] = new YYARec(311,31);yyac++; 
					yya[yyac] = new YYARec(312,32);yyac++; 
					yya[yyac] = new YYARec(313,33);yyac++; 
					yya[yyac] = new YYARec(315,34);yyac++; 
					yya[yyac] = new YYARec(317,35);yyac++; 
					yya[yyac] = new YYARec(319,36);yyac++; 
					yya[yyac] = new YYARec(261,-3 );yyac++; 
					yya[yyac] = new YYARec(263,71);yyac++; 
					yya[yyac] = new YYARec(258,-94 );yyac++; 
					yya[yyac] = new YYARec(282,-94 );yyac++; 
					yya[yyac] = new YYARec(283,-94 );yyac++; 
					yya[yyac] = new YYARec(287,-94 );yyac++; 
					yya[yyac] = new YYARec(298,-94 );yyac++; 
					yya[yyac] = new YYARec(299,-94 );yyac++; 
					yya[yyac] = new YYARec(300,-94 );yyac++; 
					yya[yyac] = new YYARec(301,-94 );yyac++; 
					yya[yyac] = new YYARec(302,-94 );yyac++; 
					yya[yyac] = new YYARec(303,-94 );yyac++; 
					yya[yyac] = new YYARec(304,-94 );yyac++; 
					yya[yyac] = new YYARec(305,-94 );yyac++; 
					yya[yyac] = new YYARec(306,-94 );yyac++; 
					yya[yyac] = new YYARec(307,-94 );yyac++; 
					yya[yyac] = new YYARec(308,-94 );yyac++; 
					yya[yyac] = new YYARec(309,-94 );yyac++; 
					yya[yyac] = new YYARec(310,-94 );yyac++; 
					yya[yyac] = new YYARec(311,-94 );yyac++; 
					yya[yyac] = new YYARec(312,-94 );yyac++; 
					yya[yyac] = new YYARec(313,-94 );yyac++; 
					yya[yyac] = new YYARec(314,-94 );yyac++; 
					yya[yyac] = new YYARec(315,-94 );yyac++; 
					yya[yyac] = new YYARec(316,-94 );yyac++; 
					yya[yyac] = new YYARec(317,-94 );yyac++; 
					yya[yyac] = new YYARec(318,-94 );yyac++; 
					yya[yyac] = new YYARec(319,-94 );yyac++; 
					yya[yyac] = new YYARec(320,-94 );yyac++; 
					yya[yyac] = new YYARec(321,-94 );yyac++; 
					yya[yyac] = new YYARec(322,-94 );yyac++; 
					yya[yyac] = new YYARec(323,-94 );yyac++; 
					yya[yyac] = new YYARec(324,-94 );yyac++; 
					yya[yyac] = new YYARec(325,-94 );yyac++; 
					yya[yyac] = new YYARec(258,295);yyac++; 
					yya[yyac] = new YYARec(258,296);yyac++; 
					yya[yyac] = new YYARec(260,297);yyac++; 
					yya[yyac] = new YYARec(261,298);yyac++; 
					yya[yyac] = new YYARec(257,138);yyac++; 
					yya[yyac] = new YYARec(258,139);yyac++; 
					yya[yyac] = new YYARec(259,140);yyac++; 
					yya[yyac] = new YYARec(264,141);yyac++; 
					yya[yyac] = new YYARec(266,142);yyac++; 
					yya[yyac] = new YYARec(288,143);yyac++; 
					yya[yyac] = new YYARec(294,144);yyac++; 
					yya[yyac] = new YYARec(295,145);yyac++; 
					yya[yyac] = new YYARec(296,146);yyac++; 
					yya[yyac] = new YYARec(298,42);yyac++; 
					yya[yyac] = new YYARec(299,43);yyac++; 
					yya[yyac] = new YYARec(300,44);yyac++; 
					yya[yyac] = new YYARec(301,45);yyac++; 
					yya[yyac] = new YYARec(302,46);yyac++; 
					yya[yyac] = new YYARec(303,47);yyac++; 
					yya[yyac] = new YYARec(304,48);yyac++; 
					yya[yyac] = new YYARec(305,49);yyac++; 
					yya[yyac] = new YYARec(306,50);yyac++; 
					yya[yyac] = new YYARec(307,147);yyac++; 
					yya[yyac] = new YYARec(309,52);yyac++; 
					yya[yyac] = new YYARec(310,53);yyac++; 
					yya[yyac] = new YYARec(311,54);yyac++; 
					yya[yyac] = new YYARec(312,55);yyac++; 
					yya[yyac] = new YYARec(313,56);yyac++; 
					yya[yyac] = new YYARec(314,148);yyac++; 
					yya[yyac] = new YYARec(315,58);yyac++; 
					yya[yyac] = new YYARec(316,59);yyac++; 
					yya[yyac] = new YYARec(317,60);yyac++; 
					yya[yyac] = new YYARec(318,149);yyac++; 
					yya[yyac] = new YYARec(319,62);yyac++; 
					yya[yyac] = new YYARec(320,150);yyac++; 
					yya[yyac] = new YYARec(323,151);yyac++; 
					yya[yyac] = new YYARec(260,-79 );yyac++; 
					yya[yyac] = new YYARec(261,-79 );yyac++; 
					yya[yyac] = new YYARec(267,-79 );yyac++; 
					yya[yyac] = new YYARec(274,212);yyac++; 
					yya[yyac] = new YYARec(282,94);yyac++; 
					yya[yyac] = new YYARec(283,95);yyac++; 
					yya[yyac] = new YYARec(287,96);yyac++; 
					yya[yyac] = new YYARec(298,42);yyac++; 
					yya[yyac] = new YYARec(299,43);yyac++; 
					yya[yyac] = new YYARec(300,44);yyac++; 
					yya[yyac] = new YYARec(301,45);yyac++; 
					yya[yyac] = new YYARec(302,46);yyac++; 
					yya[yyac] = new YYARec(303,47);yyac++; 
					yya[yyac] = new YYARec(304,213);yyac++; 
					yya[yyac] = new YYARec(305,49);yyac++; 
					yya[yyac] = new YYARec(306,50);yyac++; 
					yya[yyac] = new YYARec(307,51);yyac++; 
					yya[yyac] = new YYARec(309,52);yyac++; 
					yya[yyac] = new YYARec(310,53);yyac++; 
					yya[yyac] = new YYARec(311,54);yyac++; 
					yya[yyac] = new YYARec(312,55);yyac++; 
					yya[yyac] = new YYARec(313,56);yyac++; 
					yya[yyac] = new YYARec(314,214);yyac++; 
					yya[yyac] = new YYARec(315,215);yyac++; 
					yya[yyac] = new YYARec(316,59);yyac++; 
					yya[yyac] = new YYARec(317,60);yyac++; 
					yya[yyac] = new YYARec(318,61);yyac++; 
					yya[yyac] = new YYARec(319,62);yyac++; 
					yya[yyac] = new YYARec(320,63);yyac++; 
					yya[yyac] = new YYARec(321,216);yyac++; 
					yya[yyac] = new YYARec(322,217);yyac++; 
					yya[yyac] = new YYARec(323,64);yyac++; 
					yya[yyac] = new YYARec(274,212);yyac++; 
					yya[yyac] = new YYARec(282,94);yyac++; 
					yya[yyac] = new YYARec(283,95);yyac++; 
					yya[yyac] = new YYARec(287,96);yyac++; 
					yya[yyac] = new YYARec(298,42);yyac++; 
					yya[yyac] = new YYARec(299,43);yyac++; 
					yya[yyac] = new YYARec(300,44);yyac++; 
					yya[yyac] = new YYARec(301,45);yyac++; 
					yya[yyac] = new YYARec(302,46);yyac++; 
					yya[yyac] = new YYARec(303,47);yyac++; 
					yya[yyac] = new YYARec(304,213);yyac++; 
					yya[yyac] = new YYARec(305,49);yyac++; 
					yya[yyac] = new YYARec(306,50);yyac++; 
					yya[yyac] = new YYARec(307,51);yyac++; 
					yya[yyac] = new YYARec(309,52);yyac++; 
					yya[yyac] = new YYARec(310,53);yyac++; 
					yya[yyac] = new YYARec(311,54);yyac++; 
					yya[yyac] = new YYARec(312,55);yyac++; 
					yya[yyac] = new YYARec(313,56);yyac++; 
					yya[yyac] = new YYARec(314,214);yyac++; 
					yya[yyac] = new YYARec(315,215);yyac++; 
					yya[yyac] = new YYARec(316,59);yyac++; 
					yya[yyac] = new YYARec(317,60);yyac++; 
					yya[yyac] = new YYARec(318,61);yyac++; 
					yya[yyac] = new YYARec(319,62);yyac++; 
					yya[yyac] = new YYARec(320,63);yyac++; 
					yya[yyac] = new YYARec(321,216);yyac++; 
					yya[yyac] = new YYARec(322,217);yyac++; 
					yya[yyac] = new YYARec(323,64);yyac++; 
					yya[yyac] = new YYARec(274,212);yyac++; 
					yya[yyac] = new YYARec(282,94);yyac++; 
					yya[yyac] = new YYARec(283,95);yyac++; 
					yya[yyac] = new YYARec(287,96);yyac++; 
					yya[yyac] = new YYARec(298,42);yyac++; 
					yya[yyac] = new YYARec(299,43);yyac++; 
					yya[yyac] = new YYARec(300,44);yyac++; 
					yya[yyac] = new YYARec(301,45);yyac++; 
					yya[yyac] = new YYARec(302,46);yyac++; 
					yya[yyac] = new YYARec(303,47);yyac++; 
					yya[yyac] = new YYARec(304,213);yyac++; 
					yya[yyac] = new YYARec(305,49);yyac++; 
					yya[yyac] = new YYARec(306,50);yyac++; 
					yya[yyac] = new YYARec(307,51);yyac++; 
					yya[yyac] = new YYARec(309,52);yyac++; 
					yya[yyac] = new YYARec(310,53);yyac++; 
					yya[yyac] = new YYARec(311,54);yyac++; 
					yya[yyac] = new YYARec(312,55);yyac++; 
					yya[yyac] = new YYARec(313,56);yyac++; 
					yya[yyac] = new YYARec(314,214);yyac++; 
					yya[yyac] = new YYARec(315,215);yyac++; 
					yya[yyac] = new YYARec(316,59);yyac++; 
					yya[yyac] = new YYARec(317,60);yyac++; 
					yya[yyac] = new YYARec(318,61);yyac++; 
					yya[yyac] = new YYARec(319,62);yyac++; 
					yya[yyac] = new YYARec(320,63);yyac++; 
					yya[yyac] = new YYARec(321,216);yyac++; 
					yya[yyac] = new YYARec(322,217);yyac++; 
					yya[yyac] = new YYARec(323,64);yyac++; 
					yya[yyac] = new YYARec(274,212);yyac++; 
					yya[yyac] = new YYARec(282,94);yyac++; 
					yya[yyac] = new YYARec(283,95);yyac++; 
					yya[yyac] = new YYARec(287,96);yyac++; 
					yya[yyac] = new YYARec(298,42);yyac++; 
					yya[yyac] = new YYARec(299,43);yyac++; 
					yya[yyac] = new YYARec(300,44);yyac++; 
					yya[yyac] = new YYARec(301,45);yyac++; 
					yya[yyac] = new YYARec(302,46);yyac++; 
					yya[yyac] = new YYARec(303,47);yyac++; 
					yya[yyac] = new YYARec(304,213);yyac++; 
					yya[yyac] = new YYARec(305,49);yyac++; 
					yya[yyac] = new YYARec(306,50);yyac++; 
					yya[yyac] = new YYARec(307,51);yyac++; 
					yya[yyac] = new YYARec(309,52);yyac++; 
					yya[yyac] = new YYARec(310,53);yyac++; 
					yya[yyac] = new YYARec(311,54);yyac++; 
					yya[yyac] = new YYARec(312,55);yyac++; 
					yya[yyac] = new YYARec(313,56);yyac++; 
					yya[yyac] = new YYARec(314,214);yyac++; 
					yya[yyac] = new YYARec(315,215);yyac++; 
					yya[yyac] = new YYARec(316,59);yyac++; 
					yya[yyac] = new YYARec(317,60);yyac++; 
					yya[yyac] = new YYARec(318,61);yyac++; 
					yya[yyac] = new YYARec(319,62);yyac++; 
					yya[yyac] = new YYARec(320,63);yyac++; 
					yya[yyac] = new YYARec(321,216);yyac++; 
					yya[yyac] = new YYARec(322,217);yyac++; 
					yya[yyac] = new YYARec(323,64);yyac++; 
					yya[yyac] = new YYARec(274,212);yyac++; 
					yya[yyac] = new YYARec(282,94);yyac++; 
					yya[yyac] = new YYARec(283,95);yyac++; 
					yya[yyac] = new YYARec(287,96);yyac++; 
					yya[yyac] = new YYARec(298,42);yyac++; 
					yya[yyac] = new YYARec(299,43);yyac++; 
					yya[yyac] = new YYARec(300,44);yyac++; 
					yya[yyac] = new YYARec(301,45);yyac++; 
					yya[yyac] = new YYARec(302,46);yyac++; 
					yya[yyac] = new YYARec(303,47);yyac++; 
					yya[yyac] = new YYARec(304,213);yyac++; 
					yya[yyac] = new YYARec(305,49);yyac++; 
					yya[yyac] = new YYARec(306,50);yyac++; 
					yya[yyac] = new YYARec(307,51);yyac++; 
					yya[yyac] = new YYARec(309,52);yyac++; 
					yya[yyac] = new YYARec(310,53);yyac++; 
					yya[yyac] = new YYARec(311,54);yyac++; 
					yya[yyac] = new YYARec(312,55);yyac++; 
					yya[yyac] = new YYARec(313,56);yyac++; 
					yya[yyac] = new YYARec(314,214);yyac++; 
					yya[yyac] = new YYARec(315,215);yyac++; 
					yya[yyac] = new YYARec(316,59);yyac++; 
					yya[yyac] = new YYARec(317,60);yyac++; 
					yya[yyac] = new YYARec(318,61);yyac++; 
					yya[yyac] = new YYARec(319,62);yyac++; 
					yya[yyac] = new YYARec(320,63);yyac++; 
					yya[yyac] = new YYARec(321,216);yyac++; 
					yya[yyac] = new YYARec(322,217);yyac++; 
					yya[yyac] = new YYARec(323,64);yyac++; 
					yya[yyac] = new YYARec(274,212);yyac++; 
					yya[yyac] = new YYARec(282,94);yyac++; 
					yya[yyac] = new YYARec(283,95);yyac++; 
					yya[yyac] = new YYARec(287,96);yyac++; 
					yya[yyac] = new YYARec(298,42);yyac++; 
					yya[yyac] = new YYARec(299,43);yyac++; 
					yya[yyac] = new YYARec(300,44);yyac++; 
					yya[yyac] = new YYARec(301,45);yyac++; 
					yya[yyac] = new YYARec(302,46);yyac++; 
					yya[yyac] = new YYARec(303,47);yyac++; 
					yya[yyac] = new YYARec(304,213);yyac++; 
					yya[yyac] = new YYARec(305,49);yyac++; 
					yya[yyac] = new YYARec(306,50);yyac++; 
					yya[yyac] = new YYARec(307,51);yyac++; 
					yya[yyac] = new YYARec(309,52);yyac++; 
					yya[yyac] = new YYARec(310,53);yyac++; 
					yya[yyac] = new YYARec(311,54);yyac++; 
					yya[yyac] = new YYARec(312,55);yyac++; 
					yya[yyac] = new YYARec(313,56);yyac++; 
					yya[yyac] = new YYARec(314,214);yyac++; 
					yya[yyac] = new YYARec(315,215);yyac++; 
					yya[yyac] = new YYARec(316,59);yyac++; 
					yya[yyac] = new YYARec(317,60);yyac++; 
					yya[yyac] = new YYARec(318,61);yyac++; 
					yya[yyac] = new YYARec(319,62);yyac++; 
					yya[yyac] = new YYARec(320,63);yyac++; 
					yya[yyac] = new YYARec(321,216);yyac++; 
					yya[yyac] = new YYARec(322,217);yyac++; 
					yya[yyac] = new YYARec(323,64);yyac++; 
					yya[yyac] = new YYARec(274,212);yyac++; 
					yya[yyac] = new YYARec(282,94);yyac++; 
					yya[yyac] = new YYARec(283,95);yyac++; 
					yya[yyac] = new YYARec(287,96);yyac++; 
					yya[yyac] = new YYARec(298,42);yyac++; 
					yya[yyac] = new YYARec(299,43);yyac++; 
					yya[yyac] = new YYARec(300,44);yyac++; 
					yya[yyac] = new YYARec(301,45);yyac++; 
					yya[yyac] = new YYARec(302,46);yyac++; 
					yya[yyac] = new YYARec(303,47);yyac++; 
					yya[yyac] = new YYARec(304,213);yyac++; 
					yya[yyac] = new YYARec(305,49);yyac++; 
					yya[yyac] = new YYARec(306,50);yyac++; 
					yya[yyac] = new YYARec(307,51);yyac++; 
					yya[yyac] = new YYARec(309,52);yyac++; 
					yya[yyac] = new YYARec(310,53);yyac++; 
					yya[yyac] = new YYARec(311,54);yyac++; 
					yya[yyac] = new YYARec(312,55);yyac++; 
					yya[yyac] = new YYARec(313,56);yyac++; 
					yya[yyac] = new YYARec(314,214);yyac++; 
					yya[yyac] = new YYARec(315,215);yyac++; 
					yya[yyac] = new YYARec(316,59);yyac++; 
					yya[yyac] = new YYARec(317,60);yyac++; 
					yya[yyac] = new YYARec(318,61);yyac++; 
					yya[yyac] = new YYARec(319,62);yyac++; 
					yya[yyac] = new YYARec(320,63);yyac++; 
					yya[yyac] = new YYARec(321,216);yyac++; 
					yya[yyac] = new YYARec(322,217);yyac++; 
					yya[yyac] = new YYARec(323,64);yyac++; 
					yya[yyac] = new YYARec(274,212);yyac++; 
					yya[yyac] = new YYARec(282,94);yyac++; 
					yya[yyac] = new YYARec(283,95);yyac++; 
					yya[yyac] = new YYARec(287,96);yyac++; 
					yya[yyac] = new YYARec(298,42);yyac++; 
					yya[yyac] = new YYARec(299,43);yyac++; 
					yya[yyac] = new YYARec(300,44);yyac++; 
					yya[yyac] = new YYARec(301,45);yyac++; 
					yya[yyac] = new YYARec(302,46);yyac++; 
					yya[yyac] = new YYARec(303,47);yyac++; 
					yya[yyac] = new YYARec(304,213);yyac++; 
					yya[yyac] = new YYARec(305,49);yyac++; 
					yya[yyac] = new YYARec(306,50);yyac++; 
					yya[yyac] = new YYARec(307,51);yyac++; 
					yya[yyac] = new YYARec(309,52);yyac++; 
					yya[yyac] = new YYARec(310,53);yyac++; 
					yya[yyac] = new YYARec(311,54);yyac++; 
					yya[yyac] = new YYARec(312,55);yyac++; 
					yya[yyac] = new YYARec(313,56);yyac++; 
					yya[yyac] = new YYARec(314,214);yyac++; 
					yya[yyac] = new YYARec(315,215);yyac++; 
					yya[yyac] = new YYARec(316,59);yyac++; 
					yya[yyac] = new YYARec(317,60);yyac++; 
					yya[yyac] = new YYARec(318,61);yyac++; 
					yya[yyac] = new YYARec(319,62);yyac++; 
					yya[yyac] = new YYARec(320,63);yyac++; 
					yya[yyac] = new YYARec(321,216);yyac++; 
					yya[yyac] = new YYARec(322,217);yyac++; 
					yya[yyac] = new YYARec(323,64);yyac++; 
					yya[yyac] = new YYARec(274,212);yyac++; 
					yya[yyac] = new YYARec(282,94);yyac++; 
					yya[yyac] = new YYARec(283,95);yyac++; 
					yya[yyac] = new YYARec(287,96);yyac++; 
					yya[yyac] = new YYARec(298,42);yyac++; 
					yya[yyac] = new YYARec(299,43);yyac++; 
					yya[yyac] = new YYARec(300,44);yyac++; 
					yya[yyac] = new YYARec(301,45);yyac++; 
					yya[yyac] = new YYARec(302,46);yyac++; 
					yya[yyac] = new YYARec(303,47);yyac++; 
					yya[yyac] = new YYARec(304,213);yyac++; 
					yya[yyac] = new YYARec(305,49);yyac++; 
					yya[yyac] = new YYARec(306,50);yyac++; 
					yya[yyac] = new YYARec(307,51);yyac++; 
					yya[yyac] = new YYARec(309,52);yyac++; 
					yya[yyac] = new YYARec(310,53);yyac++; 
					yya[yyac] = new YYARec(311,54);yyac++; 
					yya[yyac] = new YYARec(312,55);yyac++; 
					yya[yyac] = new YYARec(313,56);yyac++; 
					yya[yyac] = new YYARec(314,214);yyac++; 
					yya[yyac] = new YYARec(315,215);yyac++; 
					yya[yyac] = new YYARec(316,59);yyac++; 
					yya[yyac] = new YYARec(317,60);yyac++; 
					yya[yyac] = new YYARec(318,61);yyac++; 
					yya[yyac] = new YYARec(319,62);yyac++; 
					yya[yyac] = new YYARec(320,63);yyac++; 
					yya[yyac] = new YYARec(321,216);yyac++; 
					yya[yyac] = new YYARec(322,217);yyac++; 
					yya[yyac] = new YYARec(323,64);yyac++; 
					yya[yyac] = new YYARec(274,212);yyac++; 
					yya[yyac] = new YYARec(282,94);yyac++; 
					yya[yyac] = new YYARec(283,95);yyac++; 
					yya[yyac] = new YYARec(287,96);yyac++; 
					yya[yyac] = new YYARec(298,42);yyac++; 
					yya[yyac] = new YYARec(299,43);yyac++; 
					yya[yyac] = new YYARec(300,44);yyac++; 
					yya[yyac] = new YYARec(301,45);yyac++; 
					yya[yyac] = new YYARec(302,46);yyac++; 
					yya[yyac] = new YYARec(303,47);yyac++; 
					yya[yyac] = new YYARec(304,213);yyac++; 
					yya[yyac] = new YYARec(305,49);yyac++; 
					yya[yyac] = new YYARec(306,50);yyac++; 
					yya[yyac] = new YYARec(307,51);yyac++; 
					yya[yyac] = new YYARec(309,52);yyac++; 
					yya[yyac] = new YYARec(310,53);yyac++; 
					yya[yyac] = new YYARec(311,54);yyac++; 
					yya[yyac] = new YYARec(312,55);yyac++; 
					yya[yyac] = new YYARec(313,56);yyac++; 
					yya[yyac] = new YYARec(314,214);yyac++; 
					yya[yyac] = new YYARec(315,215);yyac++; 
					yya[yyac] = new YYARec(316,59);yyac++; 
					yya[yyac] = new YYARec(317,60);yyac++; 
					yya[yyac] = new YYARec(318,61);yyac++; 
					yya[yyac] = new YYARec(319,62);yyac++; 
					yya[yyac] = new YYARec(320,63);yyac++; 
					yya[yyac] = new YYARec(321,216);yyac++; 
					yya[yyac] = new YYARec(322,217);yyac++; 
					yya[yyac] = new YYARec(323,64);yyac++; 
					yya[yyac] = new YYARec(274,212);yyac++; 
					yya[yyac] = new YYARec(282,94);yyac++; 
					yya[yyac] = new YYARec(283,95);yyac++; 
					yya[yyac] = new YYARec(287,96);yyac++; 
					yya[yyac] = new YYARec(298,42);yyac++; 
					yya[yyac] = new YYARec(299,43);yyac++; 
					yya[yyac] = new YYARec(300,44);yyac++; 
					yya[yyac] = new YYARec(301,45);yyac++; 
					yya[yyac] = new YYARec(302,46);yyac++; 
					yya[yyac] = new YYARec(303,47);yyac++; 
					yya[yyac] = new YYARec(304,213);yyac++; 
					yya[yyac] = new YYARec(305,49);yyac++; 
					yya[yyac] = new YYARec(306,50);yyac++; 
					yya[yyac] = new YYARec(307,51);yyac++; 
					yya[yyac] = new YYARec(309,52);yyac++; 
					yya[yyac] = new YYARec(310,53);yyac++; 
					yya[yyac] = new YYARec(311,54);yyac++; 
					yya[yyac] = new YYARec(312,55);yyac++; 
					yya[yyac] = new YYARec(313,56);yyac++; 
					yya[yyac] = new YYARec(314,214);yyac++; 
					yya[yyac] = new YYARec(315,215);yyac++; 
					yya[yyac] = new YYARec(316,59);yyac++; 
					yya[yyac] = new YYARec(317,60);yyac++; 
					yya[yyac] = new YYARec(318,61);yyac++; 
					yya[yyac] = new YYARec(319,62);yyac++; 
					yya[yyac] = new YYARec(320,63);yyac++; 
					yya[yyac] = new YYARec(321,216);yyac++; 
					yya[yyac] = new YYARec(322,217);yyac++; 
					yya[yyac] = new YYARec(323,64);yyac++; 
					yya[yyac] = new YYARec(275,311);yyac++; 
					yya[yyac] = new YYARec(267,312);yyac++; 
					yya[yyac] = new YYARec(257,138);yyac++; 
					yya[yyac] = new YYARec(258,139);yyac++; 
					yya[yyac] = new YYARec(259,140);yyac++; 
					yya[yyac] = new YYARec(264,141);yyac++; 
					yya[yyac] = new YYARec(266,142);yyac++; 
					yya[yyac] = new YYARec(288,143);yyac++; 
					yya[yyac] = new YYARec(294,144);yyac++; 
					yya[yyac] = new YYARec(295,145);yyac++; 
					yya[yyac] = new YYARec(296,146);yyac++; 
					yya[yyac] = new YYARec(298,42);yyac++; 
					yya[yyac] = new YYARec(299,43);yyac++; 
					yya[yyac] = new YYARec(300,44);yyac++; 
					yya[yyac] = new YYARec(301,45);yyac++; 
					yya[yyac] = new YYARec(302,46);yyac++; 
					yya[yyac] = new YYARec(303,47);yyac++; 
					yya[yyac] = new YYARec(304,48);yyac++; 
					yya[yyac] = new YYARec(305,49);yyac++; 
					yya[yyac] = new YYARec(306,50);yyac++; 
					yya[yyac] = new YYARec(307,147);yyac++; 
					yya[yyac] = new YYARec(309,52);yyac++; 
					yya[yyac] = new YYARec(310,53);yyac++; 
					yya[yyac] = new YYARec(311,54);yyac++; 
					yya[yyac] = new YYARec(312,55);yyac++; 
					yya[yyac] = new YYARec(313,56);yyac++; 
					yya[yyac] = new YYARec(314,148);yyac++; 
					yya[yyac] = new YYARec(315,58);yyac++; 
					yya[yyac] = new YYARec(316,59);yyac++; 
					yya[yyac] = new YYARec(317,60);yyac++; 
					yya[yyac] = new YYARec(318,149);yyac++; 
					yya[yyac] = new YYARec(319,62);yyac++; 
					yya[yyac] = new YYARec(320,150);yyac++; 
					yya[yyac] = new YYARec(323,151);yyac++; 
					yya[yyac] = new YYARec(267,-79 );yyac++; 
					yya[yyac] = new YYARec(257,138);yyac++; 
					yya[yyac] = new YYARec(258,139);yyac++; 
					yya[yyac] = new YYARec(259,140);yyac++; 
					yya[yyac] = new YYARec(264,141);yyac++; 
					yya[yyac] = new YYARec(266,142);yyac++; 
					yya[yyac] = new YYARec(288,143);yyac++; 
					yya[yyac] = new YYARec(294,144);yyac++; 
					yya[yyac] = new YYARec(295,145);yyac++; 
					yya[yyac] = new YYARec(296,146);yyac++; 
					yya[yyac] = new YYARec(298,42);yyac++; 
					yya[yyac] = new YYARec(299,43);yyac++; 
					yya[yyac] = new YYARec(300,44);yyac++; 
					yya[yyac] = new YYARec(301,45);yyac++; 
					yya[yyac] = new YYARec(302,46);yyac++; 
					yya[yyac] = new YYARec(303,47);yyac++; 
					yya[yyac] = new YYARec(304,48);yyac++; 
					yya[yyac] = new YYARec(305,49);yyac++; 
					yya[yyac] = new YYARec(306,50);yyac++; 
					yya[yyac] = new YYARec(307,147);yyac++; 
					yya[yyac] = new YYARec(309,52);yyac++; 
					yya[yyac] = new YYARec(310,53);yyac++; 
					yya[yyac] = new YYARec(311,54);yyac++; 
					yya[yyac] = new YYARec(312,55);yyac++; 
					yya[yyac] = new YYARec(313,56);yyac++; 
					yya[yyac] = new YYARec(314,148);yyac++; 
					yya[yyac] = new YYARec(315,58);yyac++; 
					yya[yyac] = new YYARec(316,59);yyac++; 
					yya[yyac] = new YYARec(317,60);yyac++; 
					yya[yyac] = new YYARec(318,149);yyac++; 
					yya[yyac] = new YYARec(319,62);yyac++; 
					yya[yyac] = new YYARec(320,150);yyac++; 
					yya[yyac] = new YYARec(323,151);yyac++; 
					yya[yyac] = new YYARec(267,-79 );yyac++; 
					yya[yyac] = new YYARec(263,71);yyac++; 
					yya[yyac] = new YYARec(258,-94 );yyac++; 
					yya[yyac] = new YYARec(282,-94 );yyac++; 
					yya[yyac] = new YYARec(283,-94 );yyac++; 
					yya[yyac] = new YYARec(287,-94 );yyac++; 
					yya[yyac] = new YYARec(298,-94 );yyac++; 
					yya[yyac] = new YYARec(299,-94 );yyac++; 
					yya[yyac] = new YYARec(300,-94 );yyac++; 
					yya[yyac] = new YYARec(301,-94 );yyac++; 
					yya[yyac] = new YYARec(302,-94 );yyac++; 
					yya[yyac] = new YYARec(303,-94 );yyac++; 
					yya[yyac] = new YYARec(304,-94 );yyac++; 
					yya[yyac] = new YYARec(305,-94 );yyac++; 
					yya[yyac] = new YYARec(306,-94 );yyac++; 
					yya[yyac] = new YYARec(307,-94 );yyac++; 
					yya[yyac] = new YYARec(309,-94 );yyac++; 
					yya[yyac] = new YYARec(310,-94 );yyac++; 
					yya[yyac] = new YYARec(311,-94 );yyac++; 
					yya[yyac] = new YYARec(312,-94 );yyac++; 
					yya[yyac] = new YYARec(313,-94 );yyac++; 
					yya[yyac] = new YYARec(314,-94 );yyac++; 
					yya[yyac] = new YYARec(315,-94 );yyac++; 
					yya[yyac] = new YYARec(316,-94 );yyac++; 
					yya[yyac] = new YYARec(317,-94 );yyac++; 
					yya[yyac] = new YYARec(318,-94 );yyac++; 
					yya[yyac] = new YYARec(319,-94 );yyac++; 
					yya[yyac] = new YYARec(320,-94 );yyac++; 
					yya[yyac] = new YYARec(321,-94 );yyac++; 
					yya[yyac] = new YYARec(322,-94 );yyac++; 
					yya[yyac] = new YYARec(323,-94 );yyac++; 
					yya[yyac] = new YYARec(324,-94 );yyac++; 
					yya[yyac] = new YYARec(325,-94 );yyac++; 
					yya[yyac] = new YYARec(257,160);yyac++; 
					yya[yyac] = new YYARec(259,161);yyac++; 
					yya[yyac] = new YYARec(301,27);yyac++; 
					yya[yyac] = new YYARec(302,28);yyac++; 
					yya[yyac] = new YYARec(306,162);yyac++; 
					yya[yyac] = new YYARec(311,163);yyac++; 
					yya[yyac] = new YYARec(312,164);yyac++; 
					yya[yyac] = new YYARec(315,165);yyac++; 
					yya[yyac] = new YYARec(317,166);yyac++; 
					yya[yyac] = new YYARec(319,167);yyac++; 
					yya[yyac] = new YYARec(320,168);yyac++; 
					yya[yyac] = new YYARec(323,64);yyac++; 
					yya[yyac] = new YYARec(260,-63 );yyac++; 
					yya[yyac] = new YYARec(261,-63 );yyac++; 
					yya[yyac] = new YYARec(257,160);yyac++; 
					yya[yyac] = new YYARec(259,161);yyac++; 
					yya[yyac] = new YYARec(301,27);yyac++; 
					yya[yyac] = new YYARec(302,28);yyac++; 
					yya[yyac] = new YYARec(306,162);yyac++; 
					yya[yyac] = new YYARec(311,163);yyac++; 
					yya[yyac] = new YYARec(312,164);yyac++; 
					yya[yyac] = new YYARec(315,165);yyac++; 
					yya[yyac] = new YYARec(317,166);yyac++; 
					yya[yyac] = new YYARec(319,167);yyac++; 
					yya[yyac] = new YYARec(320,168);yyac++; 
					yya[yyac] = new YYARec(323,64);yyac++; 
					yya[yyac] = new YYARec(260,-63 );yyac++; 
					yya[yyac] = new YYARec(261,-63 );yyac++; 
					yya[yyac] = new YYARec(282,94);yyac++; 
					yya[yyac] = new YYARec(283,95);yyac++; 
					yya[yyac] = new YYARec(287,96);yyac++; 
					yya[yyac] = new YYARec(321,97);yyac++; 
					yya[yyac] = new YYARec(258,-53 );yyac++; 
					yya[yyac] = new YYARec(261,320);yyac++; 
					yya[yyac] = new YYARec(282,94);yyac++; 
					yya[yyac] = new YYARec(283,95);yyac++; 
					yya[yyac] = new YYARec(287,96);yyac++; 
					yya[yyac] = new YYARec(298,42);yyac++; 
					yya[yyac] = new YYARec(299,43);yyac++; 
					yya[yyac] = new YYARec(300,44);yyac++; 
					yya[yyac] = new YYARec(301,45);yyac++; 
					yya[yyac] = new YYARec(302,46);yyac++; 
					yya[yyac] = new YYARec(303,47);yyac++; 
					yya[yyac] = new YYARec(304,48);yyac++; 
					yya[yyac] = new YYARec(305,49);yyac++; 
					yya[yyac] = new YYARec(306,50);yyac++; 
					yya[yyac] = new YYARec(307,51);yyac++; 
					yya[yyac] = new YYARec(308,127);yyac++; 
					yya[yyac] = new YYARec(309,52);yyac++; 
					yya[yyac] = new YYARec(310,53);yyac++; 
					yya[yyac] = new YYARec(311,54);yyac++; 
					yya[yyac] = new YYARec(312,55);yyac++; 
					yya[yyac] = new YYARec(313,56);yyac++; 
					yya[yyac] = new YYARec(314,57);yyac++; 
					yya[yyac] = new YYARec(315,58);yyac++; 
					yya[yyac] = new YYARec(316,59);yyac++; 
					yya[yyac] = new YYARec(317,60);yyac++; 
					yya[yyac] = new YYARec(318,61);yyac++; 
					yya[yyac] = new YYARec(319,62);yyac++; 
					yya[yyac] = new YYARec(320,63);yyac++; 
					yya[yyac] = new YYARec(321,97);yyac++; 
					yya[yyac] = new YYARec(322,98);yyac++; 
					yya[yyac] = new YYARec(323,64);yyac++; 
					yya[yyac] = new YYARec(324,81);yyac++; 
					yya[yyac] = new YYARec(325,99);yyac++; 
					yya[yyac] = new YYARec(258,-86 );yyac++; 
					yya[yyac] = new YYARec(257,138);yyac++; 
					yya[yyac] = new YYARec(258,139);yyac++; 
					yya[yyac] = new YYARec(259,140);yyac++; 
					yya[yyac] = new YYARec(264,141);yyac++; 
					yya[yyac] = new YYARec(266,142);yyac++; 
					yya[yyac] = new YYARec(288,143);yyac++; 
					yya[yyac] = new YYARec(294,144);yyac++; 
					yya[yyac] = new YYARec(295,145);yyac++; 
					yya[yyac] = new YYARec(296,146);yyac++; 
					yya[yyac] = new YYARec(298,42);yyac++; 
					yya[yyac] = new YYARec(299,43);yyac++; 
					yya[yyac] = new YYARec(300,44);yyac++; 
					yya[yyac] = new YYARec(301,45);yyac++; 
					yya[yyac] = new YYARec(302,46);yyac++; 
					yya[yyac] = new YYARec(303,47);yyac++; 
					yya[yyac] = new YYARec(304,48);yyac++; 
					yya[yyac] = new YYARec(305,49);yyac++; 
					yya[yyac] = new YYARec(306,50);yyac++; 
					yya[yyac] = new YYARec(307,147);yyac++; 
					yya[yyac] = new YYARec(309,52);yyac++; 
					yya[yyac] = new YYARec(310,53);yyac++; 
					yya[yyac] = new YYARec(311,54);yyac++; 
					yya[yyac] = new YYARec(312,55);yyac++; 
					yya[yyac] = new YYARec(313,56);yyac++; 
					yya[yyac] = new YYARec(314,148);yyac++; 
					yya[yyac] = new YYARec(315,58);yyac++; 
					yya[yyac] = new YYARec(316,59);yyac++; 
					yya[yyac] = new YYARec(317,60);yyac++; 
					yya[yyac] = new YYARec(318,149);yyac++; 
					yya[yyac] = new YYARec(319,62);yyac++; 
					yya[yyac] = new YYARec(320,150);yyac++; 
					yya[yyac] = new YYARec(323,151);yyac++; 
					yya[yyac] = new YYARec(261,-79 );yyac++; 
					yya[yyac] = new YYARec(275,323);yyac++; 
					yya[yyac] = new YYARec(284,257);yyac++; 
					yya[yyac] = new YYARec(285,258);yyac++; 
					yya[yyac] = new YYARec(286,259);yyac++; 
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
					yya[yyac] = new YYARec(282,-111 );yyac++; 
					yya[yyac] = new YYARec(283,-111 );yyac++; 
					yya[yyac] = new YYARec(282,261);yyac++; 
					yya[yyac] = new YYARec(283,262);yyac++; 
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
					yya[yyac] = new YYARec(278,-109 );yyac++; 
					yya[yyac] = new YYARec(279,-109 );yyac++; 
					yya[yyac] = new YYARec(280,-109 );yyac++; 
					yya[yyac] = new YYARec(281,-109 );yyac++; 
					yya[yyac] = new YYARec(278,264);yyac++; 
					yya[yyac] = new YYARec(279,265);yyac++; 
					yya[yyac] = new YYARec(280,266);yyac++; 
					yya[yyac] = new YYARec(281,267);yyac++; 
					yya[yyac] = new YYARec(258,-107 );yyac++; 
					yya[yyac] = new YYARec(266,-107 );yyac++; 
					yya[yyac] = new YYARec(269,-107 );yyac++; 
					yya[yyac] = new YYARec(270,-107 );yyac++; 
					yya[yyac] = new YYARec(271,-107 );yyac++; 
					yya[yyac] = new YYARec(272,-107 );yyac++; 
					yya[yyac] = new YYARec(273,-107 );yyac++; 
					yya[yyac] = new YYARec(275,-107 );yyac++; 
					yya[yyac] = new YYARec(276,-107 );yyac++; 
					yya[yyac] = new YYARec(277,-107 );yyac++; 
					yya[yyac] = new YYARec(276,269);yyac++; 
					yya[yyac] = new YYARec(277,270);yyac++; 
					yya[yyac] = new YYARec(258,-104 );yyac++; 
					yya[yyac] = new YYARec(266,-104 );yyac++; 
					yya[yyac] = new YYARec(269,-104 );yyac++; 
					yya[yyac] = new YYARec(270,-104 );yyac++; 
					yya[yyac] = new YYARec(271,-104 );yyac++; 
					yya[yyac] = new YYARec(272,-104 );yyac++; 
					yya[yyac] = new YYARec(273,-104 );yyac++; 
					yya[yyac] = new YYARec(275,-104 );yyac++; 
					yya[yyac] = new YYARec(273,271);yyac++; 
					yya[yyac] = new YYARec(258,-102 );yyac++; 
					yya[yyac] = new YYARec(266,-102 );yyac++; 
					yya[yyac] = new YYARec(269,-102 );yyac++; 
					yya[yyac] = new YYARec(270,-102 );yyac++; 
					yya[yyac] = new YYARec(271,-102 );yyac++; 
					yya[yyac] = new YYARec(272,-102 );yyac++; 
					yya[yyac] = new YYARec(275,-102 );yyac++; 
					yya[yyac] = new YYARec(272,272);yyac++; 
					yya[yyac] = new YYARec(258,-100 );yyac++; 
					yya[yyac] = new YYARec(266,-100 );yyac++; 
					yya[yyac] = new YYARec(269,-100 );yyac++; 
					yya[yyac] = new YYARec(270,-100 );yyac++; 
					yya[yyac] = new YYARec(271,-100 );yyac++; 
					yya[yyac] = new YYARec(275,-100 );yyac++; 
					yya[yyac] = new YYARec(271,273);yyac++; 
					yya[yyac] = new YYARec(258,-98 );yyac++; 
					yya[yyac] = new YYARec(266,-98 );yyac++; 
					yya[yyac] = new YYARec(269,-98 );yyac++; 
					yya[yyac] = new YYARec(270,-98 );yyac++; 
					yya[yyac] = new YYARec(275,-98 );yyac++; 
					yya[yyac] = new YYARec(270,274);yyac++; 
					yya[yyac] = new YYARec(258,-96 );yyac++; 
					yya[yyac] = new YYARec(266,-96 );yyac++; 
					yya[yyac] = new YYARec(269,-96 );yyac++; 
					yya[yyac] = new YYARec(275,-96 );yyac++; 
					yya[yyac] = new YYARec(267,324);yyac++; 
					yya[yyac] = new YYARec(267,325);yyac++; 
					yya[yyac] = new YYARec(282,94);yyac++; 
					yya[yyac] = new YYARec(283,95);yyac++; 
					yya[yyac] = new YYARec(287,96);yyac++; 
					yya[yyac] = new YYARec(298,42);yyac++; 
					yya[yyac] = new YYARec(299,43);yyac++; 
					yya[yyac] = new YYARec(300,44);yyac++; 
					yya[yyac] = new YYARec(301,45);yyac++; 
					yya[yyac] = new YYARec(302,46);yyac++; 
					yya[yyac] = new YYARec(303,47);yyac++; 
					yya[yyac] = new YYARec(304,48);yyac++; 
					yya[yyac] = new YYARec(305,49);yyac++; 
					yya[yyac] = new YYARec(306,50);yyac++; 
					yya[yyac] = new YYARec(307,51);yyac++; 
					yya[yyac] = new YYARec(309,52);yyac++; 
					yya[yyac] = new YYARec(310,53);yyac++; 
					yya[yyac] = new YYARec(311,54);yyac++; 
					yya[yyac] = new YYARec(312,55);yyac++; 
					yya[yyac] = new YYARec(313,56);yyac++; 
					yya[yyac] = new YYARec(314,57);yyac++; 
					yya[yyac] = new YYARec(315,58);yyac++; 
					yya[yyac] = new YYARec(316,59);yyac++; 
					yya[yyac] = new YYARec(317,60);yyac++; 
					yya[yyac] = new YYARec(318,61);yyac++; 
					yya[yyac] = new YYARec(319,62);yyac++; 
					yya[yyac] = new YYARec(320,63);yyac++; 
					yya[yyac] = new YYARec(321,97);yyac++; 
					yya[yyac] = new YYARec(322,98);yyac++; 
					yya[yyac] = new YYARec(323,64);yyac++; 
					yya[yyac] = new YYARec(324,81);yyac++; 
					yya[yyac] = new YYARec(325,99);yyac++; 
					yya[yyac] = new YYARec(258,-65 );yyac++; 
					yya[yyac] = new YYARec(260,327);yyac++; 
					yya[yyac] = new YYARec(261,328);yyac++; 
					yya[yyac] = new YYARec(258,329);yyac++; 
					yya[yyac] = new YYARec(258,330);yyac++; 
					yya[yyac] = new YYARec(261,331);yyac++; 
					yya[yyac] = new YYARec(258,332);yyac++; 
					yya[yyac] = new YYARec(257,160);yyac++; 
					yya[yyac] = new YYARec(259,161);yyac++; 
					yya[yyac] = new YYARec(301,27);yyac++; 
					yya[yyac] = new YYARec(302,28);yyac++; 
					yya[yyac] = new YYARec(306,162);yyac++; 
					yya[yyac] = new YYARec(311,163);yyac++; 
					yya[yyac] = new YYARec(312,164);yyac++; 
					yya[yyac] = new YYARec(315,165);yyac++; 
					yya[yyac] = new YYARec(317,166);yyac++; 
					yya[yyac] = new YYARec(319,167);yyac++; 
					yya[yyac] = new YYARec(320,168);yyac++; 
					yya[yyac] = new YYARec(323,64);yyac++; 
					yya[yyac] = new YYARec(261,-63 );yyac++; 
					yya[yyac] = new YYARec(261,334);yyac++;

					yyg[yygc] = new YYARec(-44,1);yygc++; 
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
					yyg[yygc] = new YYARec(-77,37);yygc++; 
					yyg[yygc] = new YYARec(-76,38);yygc++; 
					yyg[yygc] = new YYARec(-73,39);yygc++; 
					yyg[yygc] = new YYARec(-45,40);yygc++; 
					yyg[yygc] = new YYARec(-22,41);yygc++; 
					yyg[yygc] = new YYARec(-77,37);yygc++; 
					yyg[yygc] = new YYARec(-76,38);yygc++; 
					yyg[yygc] = new YYARec(-73,65);yygc++; 
					yyg[yygc] = new YYARec(-33,66);yygc++; 
					yyg[yygc] = new YYARec(-22,41);yygc++; 
					yyg[yygc] = new YYARec(-77,37);yygc++; 
					yyg[yygc] = new YYARec(-76,38);yygc++; 
					yyg[yygc] = new YYARec(-73,67);yygc++; 
					yyg[yygc] = new YYARec(-35,68);yygc++; 
					yyg[yygc] = new YYARec(-22,41);yygc++; 
					yyg[yygc] = new YYARec(-29,70);yygc++; 
					yyg[yygc] = new YYARec(-29,72);yygc++; 
					yyg[yygc] = new YYARec(-44,1);yygc++; 
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
					yyg[yygc] = new YYARec(-3,73);yygc++; 
					yyg[yygc] = new YYARec(-22,74);yygc++; 
					yyg[yygc] = new YYARec(-21,75);yygc++; 
					yyg[yygc] = new YYARec(-12,76);yygc++; 
					yyg[yygc] = new YYARec(-22,74);yygc++; 
					yyg[yygc] = new YYARec(-21,75);yygc++; 
					yyg[yygc] = new YYARec(-12,77);yygc++; 
					yyg[yygc] = new YYARec(-22,74);yygc++; 
					yyg[yygc] = new YYARec(-21,75);yygc++; 
					yyg[yygc] = new YYARec(-12,78);yygc++; 
					yyg[yygc] = new YYARec(-22,74);yygc++; 
					yyg[yygc] = new YYARec(-21,75);yygc++; 
					yyg[yygc] = new YYARec(-12,79);yygc++; 
					yyg[yygc] = new YYARec(-25,80);yygc++; 
					yyg[yygc] = new YYARec(-29,82);yygc++; 
					yyg[yygc] = new YYARec(-29,83);yygc++; 
					yyg[yygc] = new YYARec(-29,85);yygc++; 
					yyg[yygc] = new YYARec(-75,86);yygc++; 
					yyg[yygc] = new YYARec(-69,87);yygc++; 
					yyg[yygc] = new YYARec(-37,88);yygc++; 
					yyg[yygc] = new YYARec(-32,89);yygc++; 
					yyg[yygc] = new YYARec(-26,90);yygc++; 
					yyg[yygc] = new YYARec(-25,91);yygc++; 
					yyg[yygc] = new YYARec(-23,92);yygc++; 
					yyg[yygc] = new YYARec(-22,93);yygc++; 
					yyg[yygc] = new YYARec(-77,37);yygc++; 
					yyg[yygc] = new YYARec(-76,38);yygc++; 
					yyg[yygc] = new YYARec(-73,65);yygc++; 
					yyg[yygc] = new YYARec(-33,100);yygc++; 
					yyg[yygc] = new YYARec(-30,101);yygc++; 
					yyg[yygc] = new YYARec(-22,41);yygc++; 
					yyg[yygc] = new YYARec(-23,109);yygc++; 
					yyg[yygc] = new YYARec(-25,111);yygc++; 
					yyg[yygc] = new YYARec(-29,115);yygc++; 
					yyg[yygc] = new YYARec(-44,1);yygc++; 
					yyg[yygc] = new YYARec(-39,2);yygc++; 
					yyg[yygc] = new YYARec(-38,3);yygc++; 
					yyg[yygc] = new YYARec(-34,4);yygc++; 
					yyg[yygc] = new YYARec(-31,5);yygc++; 
					yyg[yygc] = new YYARec(-28,6);yygc++; 
					yyg[yygc] = new YYARec(-21,7);yygc++; 
					yyg[yygc] = new YYARec(-13,117);yygc++; 
					yyg[yygc] = new YYARec(-11,8);yygc++; 
					yyg[yygc] = new YYARec(-10,9);yygc++; 
					yyg[yygc] = new YYARec(-9,10);yygc++; 
					yyg[yygc] = new YYARec(-8,11);yygc++; 
					yyg[yygc] = new YYARec(-7,12);yygc++; 
					yyg[yygc] = new YYARec(-6,13);yygc++; 
					yyg[yygc] = new YYARec(-5,14);yygc++; 
					yyg[yygc] = new YYARec(-4,15);yygc++; 
					yyg[yygc] = new YYARec(-3,118);yygc++; 
					yyg[yygc] = new YYARec(-44,1);yygc++; 
					yyg[yygc] = new YYARec(-39,2);yygc++; 
					yyg[yygc] = new YYARec(-38,3);yygc++; 
					yyg[yygc] = new YYARec(-34,4);yygc++; 
					yyg[yygc] = new YYARec(-31,5);yygc++; 
					yyg[yygc] = new YYARec(-28,6);yygc++; 
					yyg[yygc] = new YYARec(-21,7);yygc++; 
					yyg[yygc] = new YYARec(-13,119);yygc++; 
					yyg[yygc] = new YYARec(-11,8);yygc++; 
					yyg[yygc] = new YYARec(-10,9);yygc++; 
					yyg[yygc] = new YYARec(-9,10);yygc++; 
					yyg[yygc] = new YYARec(-8,11);yygc++; 
					yyg[yygc] = new YYARec(-7,12);yygc++; 
					yyg[yygc] = new YYARec(-6,13);yygc++; 
					yyg[yygc] = new YYARec(-5,14);yygc++; 
					yyg[yygc] = new YYARec(-4,15);yygc++; 
					yyg[yygc] = new YYARec(-3,118);yygc++; 
					yyg[yygc] = new YYARec(-77,37);yygc++; 
					yyg[yygc] = new YYARec(-76,38);yygc++; 
					yyg[yygc] = new YYARec(-75,86);yygc++; 
					yyg[yygc] = new YYARec(-73,120);yygc++; 
					yyg[yygc] = new YYARec(-69,87);yygc++; 
					yyg[yygc] = new YYARec(-37,88);yygc++; 
					yyg[yygc] = new YYARec(-27,121);yygc++; 
					yyg[yygc] = new YYARec(-26,122);yygc++; 
					yyg[yygc] = new YYARec(-25,123);yygc++; 
					yyg[yygc] = new YYARec(-24,124);yygc++; 
					yyg[yygc] = new YYARec(-23,125);yygc++; 
					yyg[yygc] = new YYARec(-22,41);yygc++; 
					yyg[yygc] = new YYARec(-20,126);yygc++; 
					yyg[yygc] = new YYARec(-77,37);yygc++; 
					yyg[yygc] = new YYARec(-76,38);yygc++; 
					yyg[yygc] = new YYARec(-73,128);yygc++; 
					yyg[yygc] = new YYARec(-51,129);yygc++; 
					yyg[yygc] = new YYARec(-49,130);yygc++; 
					yyg[yygc] = new YYARec(-48,131);yygc++; 
					yyg[yygc] = new YYARec(-47,132);yygc++; 
					yyg[yygc] = new YYARec(-46,133);yygc++; 
					yyg[yygc] = new YYARec(-27,134);yygc++; 
					yyg[yygc] = new YYARec(-22,135);yygc++; 
					yyg[yygc] = new YYARec(-16,136);yygc++; 
					yyg[yygc] = new YYARec(-14,137);yygc++; 
					yyg[yygc] = new YYARec(-41,153);yygc++; 
					yyg[yygc] = new YYARec(-40,154);yygc++; 
					yyg[yygc] = new YYARec(-39,155);yygc++; 
					yyg[yygc] = new YYARec(-34,156);yygc++; 
					yyg[yygc] = new YYARec(-22,157);yygc++; 
					yyg[yygc] = new YYARec(-19,158);yygc++; 
					yyg[yygc] = new YYARec(-17,159);yygc++; 
					yyg[yygc] = new YYARec(-29,169);yygc++; 
					yyg[yygc] = new YYARec(-77,37);yygc++; 
					yyg[yygc] = new YYARec(-76,38);yygc++; 
					yyg[yygc] = new YYARec(-73,65);yygc++; 
					yyg[yygc] = new YYARec(-33,100);yygc++; 
					yyg[yygc] = new YYARec(-30,170);yygc++; 
					yyg[yygc] = new YYARec(-22,41);yygc++; 
					yyg[yygc] = new YYARec(-29,178);yygc++; 
					yyg[yygc] = new YYARec(-77,37);yygc++; 
					yyg[yygc] = new YYARec(-76,38);yygc++; 
					yyg[yygc] = new YYARec(-73,128);yygc++; 
					yyg[yygc] = new YYARec(-51,129);yygc++; 
					yyg[yygc] = new YYARec(-49,130);yygc++; 
					yyg[yygc] = new YYARec(-48,131);yygc++; 
					yyg[yygc] = new YYARec(-47,132);yygc++; 
					yyg[yygc] = new YYARec(-46,133);yygc++; 
					yyg[yygc] = new YYARec(-27,134);yygc++; 
					yyg[yygc] = new YYARec(-22,135);yygc++; 
					yyg[yygc] = new YYARec(-16,180);yygc++; 
					yyg[yygc] = new YYARec(-14,137);yygc++; 
					yyg[yygc] = new YYARec(-77,37);yygc++; 
					yyg[yygc] = new YYARec(-76,38);yygc++; 
					yyg[yygc] = new YYARec(-73,128);yygc++; 
					yyg[yygc] = new YYARec(-51,129);yygc++; 
					yyg[yygc] = new YYARec(-49,130);yygc++; 
					yyg[yygc] = new YYARec(-48,131);yygc++; 
					yyg[yygc] = new YYARec(-47,132);yygc++; 
					yyg[yygc] = new YYARec(-46,133);yygc++; 
					yyg[yygc] = new YYARec(-27,134);yygc++; 
					yyg[yygc] = new YYARec(-22,135);yygc++; 
					yyg[yygc] = new YYARec(-16,181);yygc++; 
					yyg[yygc] = new YYARec(-14,137);yygc++; 
					yyg[yygc] = new YYARec(-72,183);yygc++; 
					yyg[yygc] = new YYARec(-29,189);yygc++; 
					yyg[yygc] = new YYARec(-77,37);yygc++; 
					yyg[yygc] = new YYARec(-76,38);yygc++; 
					yyg[yygc] = new YYARec(-73,128);yygc++; 
					yyg[yygc] = new YYARec(-51,129);yygc++; 
					yyg[yygc] = new YYARec(-49,130);yygc++; 
					yyg[yygc] = new YYARec(-48,131);yygc++; 
					yyg[yygc] = new YYARec(-47,132);yygc++; 
					yyg[yygc] = new YYARec(-46,133);yygc++; 
					yyg[yygc] = new YYARec(-27,134);yygc++; 
					yyg[yygc] = new YYARec(-22,135);yygc++; 
					yyg[yygc] = new YYARec(-16,191);yygc++; 
					yyg[yygc] = new YYARec(-14,137);yygc++; 
					yyg[yygc] = new YYARec(-22,74);yygc++; 
					yyg[yygc] = new YYARec(-21,75);yygc++; 
					yyg[yygc] = new YYARec(-12,192);yygc++; 
					yyg[yygc] = new YYARec(-22,74);yygc++; 
					yyg[yygc] = new YYARec(-21,75);yygc++; 
					yyg[yygc] = new YYARec(-12,193);yygc++; 
					yyg[yygc] = new YYARec(-22,74);yygc++; 
					yyg[yygc] = new YYARec(-21,75);yygc++; 
					yyg[yygc] = new YYARec(-12,194);yygc++; 
					yyg[yygc] = new YYARec(-77,37);yygc++; 
					yyg[yygc] = new YYARec(-76,38);yygc++; 
					yyg[yygc] = new YYARec(-73,128);yygc++; 
					yyg[yygc] = new YYARec(-51,129);yygc++; 
					yyg[yygc] = new YYARec(-49,130);yygc++; 
					yyg[yygc] = new YYARec(-48,131);yygc++; 
					yyg[yygc] = new YYARec(-47,132);yygc++; 
					yyg[yygc] = new YYARec(-46,133);yygc++; 
					yyg[yygc] = new YYARec(-27,134);yygc++; 
					yyg[yygc] = new YYARec(-22,135);yygc++; 
					yyg[yygc] = new YYARec(-16,195);yygc++; 
					yyg[yygc] = new YYARec(-14,137);yygc++; 
					yyg[yygc] = new YYARec(-77,37);yygc++; 
					yyg[yygc] = new YYARec(-76,38);yygc++; 
					yyg[yygc] = new YYARec(-73,120);yygc++; 
					yyg[yygc] = new YYARec(-71,196);yygc++; 
					yyg[yygc] = new YYARec(-70,197);yygc++; 
					yyg[yygc] = new YYARec(-69,198);yygc++; 
					yyg[yygc] = new YYARec(-68,199);yygc++; 
					yyg[yygc] = new YYARec(-66,200);yygc++; 
					yyg[yygc] = new YYARec(-64,201);yygc++; 
					yyg[yygc] = new YYARec(-62,202);yygc++; 
					yyg[yygc] = new YYARec(-60,203);yygc++; 
					yyg[yygc] = new YYARec(-59,204);yygc++; 
					yyg[yygc] = new YYARec(-58,205);yygc++; 
					yyg[yygc] = new YYARec(-57,206);yygc++; 
					yyg[yygc] = new YYARec(-56,207);yygc++; 
					yyg[yygc] = new YYARec(-55,208);yygc++; 
					yyg[yygc] = new YYARec(-54,209);yygc++; 
					yyg[yygc] = new YYARec(-53,210);yygc++; 
					yyg[yygc] = new YYARec(-27,211);yygc++; 
					yyg[yygc] = new YYARec(-22,41);yygc++; 
					yyg[yygc] = new YYARec(-77,37);yygc++; 
					yyg[yygc] = new YYARec(-76,38);yygc++; 
					yyg[yygc] = new YYARec(-73,120);yygc++; 
					yyg[yygc] = new YYARec(-71,196);yygc++; 
					yyg[yygc] = new YYARec(-70,197);yygc++; 
					yyg[yygc] = new YYARec(-69,198);yygc++; 
					yyg[yygc] = new YYARec(-68,199);yygc++; 
					yyg[yygc] = new YYARec(-66,200);yygc++; 
					yyg[yygc] = new YYARec(-64,201);yygc++; 
					yyg[yygc] = new YYARec(-62,202);yygc++; 
					yyg[yygc] = new YYARec(-60,203);yygc++; 
					yyg[yygc] = new YYARec(-59,204);yygc++; 
					yyg[yygc] = new YYARec(-58,205);yygc++; 
					yyg[yygc] = new YYARec(-57,206);yygc++; 
					yyg[yygc] = new YYARec(-56,207);yygc++; 
					yyg[yygc] = new YYARec(-55,208);yygc++; 
					yyg[yygc] = new YYARec(-54,209);yygc++; 
					yyg[yygc] = new YYARec(-53,219);yygc++; 
					yyg[yygc] = new YYARec(-27,220);yygc++; 
					yyg[yygc] = new YYARec(-22,41);yygc++; 
					yyg[yygc] = new YYARec(-77,37);yygc++; 
					yyg[yygc] = new YYARec(-76,38);yygc++; 
					yyg[yygc] = new YYARec(-73,120);yygc++; 
					yyg[yygc] = new YYARec(-71,196);yygc++; 
					yyg[yygc] = new YYARec(-70,197);yygc++; 
					yyg[yygc] = new YYARec(-69,198);yygc++; 
					yyg[yygc] = new YYARec(-68,199);yygc++; 
					yyg[yygc] = new YYARec(-66,200);yygc++; 
					yyg[yygc] = new YYARec(-64,201);yygc++; 
					yyg[yygc] = new YYARec(-62,202);yygc++; 
					yyg[yygc] = new YYARec(-60,203);yygc++; 
					yyg[yygc] = new YYARec(-59,204);yygc++; 
					yyg[yygc] = new YYARec(-58,205);yygc++; 
					yyg[yygc] = new YYARec(-57,206);yygc++; 
					yyg[yygc] = new YYARec(-56,207);yygc++; 
					yyg[yygc] = new YYARec(-55,208);yygc++; 
					yyg[yygc] = new YYARec(-54,209);yygc++; 
					yyg[yygc] = new YYARec(-53,221);yygc++; 
					yyg[yygc] = new YYARec(-27,220);yygc++; 
					yyg[yygc] = new YYARec(-22,41);yygc++; 
					yyg[yygc] = new YYARec(-29,223);yygc++; 
					yyg[yygc] = new YYARec(-41,153);yygc++; 
					yyg[yygc] = new YYARec(-40,154);yygc++; 
					yyg[yygc] = new YYARec(-39,155);yygc++; 
					yyg[yygc] = new YYARec(-34,156);yygc++; 
					yyg[yygc] = new YYARec(-22,157);yygc++; 
					yyg[yygc] = new YYARec(-19,226);yygc++; 
					yyg[yygc] = new YYARec(-17,159);yygc++; 
					yyg[yygc] = new YYARec(-22,74);yygc++; 
					yyg[yygc] = new YYARec(-21,75);yygc++; 
					yyg[yygc] = new YYARec(-12,227);yygc++; 
					yyg[yygc] = new YYARec(-22,74);yygc++; 
					yyg[yygc] = new YYARec(-21,75);yygc++; 
					yyg[yygc] = new YYARec(-12,228);yygc++; 
					yyg[yygc] = new YYARec(-69,229);yygc++; 
					yyg[yygc] = new YYARec(-37,230);yygc++; 
					yyg[yygc] = new YYARec(-36,231);yygc++; 
					yyg[yygc] = new YYARec(-74,233);yygc++; 
					yyg[yygc] = new YYARec(-41,234);yygc++; 
					yyg[yygc] = new YYARec(-39,155);yygc++; 
					yyg[yygc] = new YYARec(-34,156);yygc++; 
					yyg[yygc] = new YYARec(-22,157);yygc++; 
					yyg[yygc] = new YYARec(-77,37);yygc++; 
					yyg[yygc] = new YYARec(-76,38);yygc++; 
					yyg[yygc] = new YYARec(-75,86);yygc++; 
					yyg[yygc] = new YYARec(-73,120);yygc++; 
					yyg[yygc] = new YYARec(-69,87);yygc++; 
					yyg[yygc] = new YYARec(-52,239);yygc++; 
					yyg[yygc] = new YYARec(-50,240);yygc++; 
					yyg[yygc] = new YYARec(-37,88);yygc++; 
					yyg[yygc] = new YYARec(-27,241);yygc++; 
					yyg[yygc] = new YYARec(-26,242);yygc++; 
					yyg[yygc] = new YYARec(-25,243);yygc++; 
					yyg[yygc] = new YYARec(-24,244);yygc++; 
					yyg[yygc] = new YYARec(-23,245);yygc++; 
					yyg[yygc] = new YYARec(-22,41);yygc++; 
					yyg[yygc] = new YYARec(-77,37);yygc++; 
					yyg[yygc] = new YYARec(-76,38);yygc++; 
					yyg[yygc] = new YYARec(-73,128);yygc++; 
					yyg[yygc] = new YYARec(-51,129);yygc++; 
					yyg[yygc] = new YYARec(-49,130);yygc++; 
					yyg[yygc] = new YYARec(-48,131);yygc++; 
					yyg[yygc] = new YYARec(-47,132);yygc++; 
					yyg[yygc] = new YYARec(-46,133);yygc++; 
					yyg[yygc] = new YYARec(-27,134);yygc++; 
					yyg[yygc] = new YYARec(-22,135);yygc++; 
					yyg[yygc] = new YYARec(-16,246);yygc++; 
					yyg[yygc] = new YYARec(-14,137);yygc++; 
					yyg[yygc] = new YYARec(-77,37);yygc++; 
					yyg[yygc] = new YYARec(-76,38);yygc++; 
					yyg[yygc] = new YYARec(-73,120);yygc++; 
					yyg[yygc] = new YYARec(-71,196);yygc++; 
					yyg[yygc] = new YYARec(-70,197);yygc++; 
					yyg[yygc] = new YYARec(-69,198);yygc++; 
					yyg[yygc] = new YYARec(-68,199);yygc++; 
					yyg[yygc] = new YYARec(-66,200);yygc++; 
					yyg[yygc] = new YYARec(-64,201);yygc++; 
					yyg[yygc] = new YYARec(-62,202);yygc++; 
					yyg[yygc] = new YYARec(-60,203);yygc++; 
					yyg[yygc] = new YYARec(-59,204);yygc++; 
					yyg[yygc] = new YYARec(-58,205);yygc++; 
					yyg[yygc] = new YYARec(-57,206);yygc++; 
					yyg[yygc] = new YYARec(-56,207);yygc++; 
					yyg[yygc] = new YYARec(-55,208);yygc++; 
					yyg[yygc] = new YYARec(-54,209);yygc++; 
					yyg[yygc] = new YYARec(-53,247);yygc++; 
					yyg[yygc] = new YYARec(-27,220);yygc++; 
					yyg[yygc] = new YYARec(-22,41);yygc++; 
					yyg[yygc] = new YYARec(-77,37);yygc++; 
					yyg[yygc] = new YYARec(-76,38);yygc++; 
					yyg[yygc] = new YYARec(-75,86);yygc++; 
					yyg[yygc] = new YYARec(-73,120);yygc++; 
					yyg[yygc] = new YYARec(-69,87);yygc++; 
					yyg[yygc] = new YYARec(-52,239);yygc++; 
					yyg[yygc] = new YYARec(-50,248);yygc++; 
					yyg[yygc] = new YYARec(-37,88);yygc++; 
					yyg[yygc] = new YYARec(-27,241);yygc++; 
					yyg[yygc] = new YYARec(-26,242);yygc++; 
					yyg[yygc] = new YYARec(-25,243);yygc++; 
					yyg[yygc] = new YYARec(-24,244);yygc++; 
					yyg[yygc] = new YYARec(-23,245);yygc++; 
					yyg[yygc] = new YYARec(-22,41);yygc++; 
					yyg[yygc] = new YYARec(-77,37);yygc++; 
					yyg[yygc] = new YYARec(-76,38);yygc++; 
					yyg[yygc] = new YYARec(-73,128);yygc++; 
					yyg[yygc] = new YYARec(-51,129);yygc++; 
					yyg[yygc] = new YYARec(-49,130);yygc++; 
					yyg[yygc] = new YYARec(-48,131);yygc++; 
					yyg[yygc] = new YYARec(-47,132);yygc++; 
					yyg[yygc] = new YYARec(-46,133);yygc++; 
					yyg[yygc] = new YYARec(-27,134);yygc++; 
					yyg[yygc] = new YYARec(-22,135);yygc++; 
					yyg[yygc] = new YYARec(-16,249);yygc++; 
					yyg[yygc] = new YYARec(-15,250);yygc++; 
					yyg[yygc] = new YYARec(-14,137);yygc++; 
					yyg[yygc] = new YYARec(-77,37);yygc++; 
					yyg[yygc] = new YYARec(-76,38);yygc++; 
					yyg[yygc] = new YYARec(-73,128);yygc++; 
					yyg[yygc] = new YYARec(-51,129);yygc++; 
					yyg[yygc] = new YYARec(-49,130);yygc++; 
					yyg[yygc] = new YYARec(-48,131);yygc++; 
					yyg[yygc] = new YYARec(-47,132);yygc++; 
					yyg[yygc] = new YYARec(-46,133);yygc++; 
					yyg[yygc] = new YYARec(-27,134);yygc++; 
					yyg[yygc] = new YYARec(-22,135);yygc++; 
					yyg[yygc] = new YYARec(-16,249);yygc++; 
					yyg[yygc] = new YYARec(-15,251);yygc++; 
					yyg[yygc] = new YYARec(-14,137);yygc++; 
					yyg[yygc] = new YYARec(-77,37);yygc++; 
					yyg[yygc] = new YYARec(-76,38);yygc++; 
					yyg[yygc] = new YYARec(-73,120);yygc++; 
					yyg[yygc] = new YYARec(-71,196);yygc++; 
					yyg[yygc] = new YYARec(-70,197);yygc++; 
					yyg[yygc] = new YYARec(-69,198);yygc++; 
					yyg[yygc] = new YYARec(-68,199);yygc++; 
					yyg[yygc] = new YYARec(-66,255);yygc++; 
					yyg[yygc] = new YYARec(-27,220);yygc++; 
					yyg[yygc] = new YYARec(-22,41);yygc++; 
					yyg[yygc] = new YYARec(-67,256);yygc++; 
					yyg[yygc] = new YYARec(-65,260);yygc++; 
					yyg[yygc] = new YYARec(-63,263);yygc++; 
					yyg[yygc] = new YYARec(-61,268);yygc++; 
					yyg[yygc] = new YYARec(-72,276);yygc++; 
					yyg[yygc] = new YYARec(-77,37);yygc++; 
					yyg[yygc] = new YYARec(-76,38);yygc++; 
					yyg[yygc] = new YYARec(-73,120);yygc++; 
					yyg[yygc] = new YYARec(-71,196);yygc++; 
					yyg[yygc] = new YYARec(-70,197);yygc++; 
					yyg[yygc] = new YYARec(-69,198);yygc++; 
					yyg[yygc] = new YYARec(-68,199);yygc++; 
					yyg[yygc] = new YYARec(-66,200);yygc++; 
					yyg[yygc] = new YYARec(-64,201);yygc++; 
					yyg[yygc] = new YYARec(-62,202);yygc++; 
					yyg[yygc] = new YYARec(-60,203);yygc++; 
					yyg[yygc] = new YYARec(-59,204);yygc++; 
					yyg[yygc] = new YYARec(-58,205);yygc++; 
					yyg[yygc] = new YYARec(-57,206);yygc++; 
					yyg[yygc] = new YYARec(-56,207);yygc++; 
					yyg[yygc] = new YYARec(-55,208);yygc++; 
					yyg[yygc] = new YYARec(-54,209);yygc++; 
					yyg[yygc] = new YYARec(-53,277);yygc++; 
					yyg[yygc] = new YYARec(-27,220);yygc++; 
					yyg[yygc] = new YYARec(-22,41);yygc++; 
					yyg[yygc] = new YYARec(-77,37);yygc++; 
					yyg[yygc] = new YYARec(-76,38);yygc++; 
					yyg[yygc] = new YYARec(-73,128);yygc++; 
					yyg[yygc] = new YYARec(-51,129);yygc++; 
					yyg[yygc] = new YYARec(-49,130);yygc++; 
					yyg[yygc] = new YYARec(-48,131);yygc++; 
					yyg[yygc] = new YYARec(-47,132);yygc++; 
					yyg[yygc] = new YYARec(-46,133);yygc++; 
					yyg[yygc] = new YYARec(-27,134);yygc++; 
					yyg[yygc] = new YYARec(-22,135);yygc++; 
					yyg[yygc] = new YYARec(-16,278);yygc++; 
					yyg[yygc] = new YYARec(-14,137);yygc++; 
					yyg[yygc] = new YYARec(-77,37);yygc++; 
					yyg[yygc] = new YYARec(-76,38);yygc++; 
					yyg[yygc] = new YYARec(-73,128);yygc++; 
					yyg[yygc] = new YYARec(-51,129);yygc++; 
					yyg[yygc] = new YYARec(-49,130);yygc++; 
					yyg[yygc] = new YYARec(-48,131);yygc++; 
					yyg[yygc] = new YYARec(-47,132);yygc++; 
					yyg[yygc] = new YYARec(-46,133);yygc++; 
					yyg[yygc] = new YYARec(-27,134);yygc++; 
					yyg[yygc] = new YYARec(-22,135);yygc++; 
					yyg[yygc] = new YYARec(-16,281);yygc++; 
					yyg[yygc] = new YYARec(-14,137);yygc++; 
					yyg[yygc] = new YYARec(-77,37);yygc++; 
					yyg[yygc] = new YYARec(-76,38);yygc++; 
					yyg[yygc] = new YYARec(-75,86);yygc++; 
					yyg[yygc] = new YYARec(-73,120);yygc++; 
					yyg[yygc] = new YYARec(-69,87);yygc++; 
					yyg[yygc] = new YYARec(-43,282);yygc++; 
					yyg[yygc] = new YYARec(-42,283);yygc++; 
					yyg[yygc] = new YYARec(-37,88);yygc++; 
					yyg[yygc] = new YYARec(-27,284);yygc++; 
					yyg[yygc] = new YYARec(-26,285);yygc++; 
					yyg[yygc] = new YYARec(-25,286);yygc++; 
					yyg[yygc] = new YYARec(-23,287);yygc++; 
					yyg[yygc] = new YYARec(-22,41);yygc++; 
					yyg[yygc] = new YYARec(-41,153);yygc++; 
					yyg[yygc] = new YYARec(-40,154);yygc++; 
					yyg[yygc] = new YYARec(-39,155);yygc++; 
					yyg[yygc] = new YYARec(-34,156);yygc++; 
					yyg[yygc] = new YYARec(-22,157);yygc++; 
					yyg[yygc] = new YYARec(-19,288);yygc++; 
					yyg[yygc] = new YYARec(-17,159);yygc++; 
					yyg[yygc] = new YYARec(-44,1);yygc++; 
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
					yyg[yygc] = new YYARec(-3,293);yygc++; 
					yyg[yygc] = new YYARec(-29,294);yygc++; 
					yyg[yygc] = new YYARec(-77,37);yygc++; 
					yyg[yygc] = new YYARec(-76,38);yygc++; 
					yyg[yygc] = new YYARec(-73,128);yygc++; 
					yyg[yygc] = new YYARec(-51,129);yygc++; 
					yyg[yygc] = new YYARec(-49,130);yygc++; 
					yyg[yygc] = new YYARec(-48,131);yygc++; 
					yyg[yygc] = new YYARec(-47,132);yygc++; 
					yyg[yygc] = new YYARec(-46,133);yygc++; 
					yyg[yygc] = new YYARec(-27,134);yygc++; 
					yyg[yygc] = new YYARec(-22,135);yygc++; 
					yyg[yygc] = new YYARec(-16,299);yygc++; 
					yyg[yygc] = new YYARec(-14,137);yygc++; 
					yyg[yygc] = new YYARec(-77,37);yygc++; 
					yyg[yygc] = new YYARec(-76,38);yygc++; 
					yyg[yygc] = new YYARec(-73,120);yygc++; 
					yyg[yygc] = new YYARec(-71,196);yygc++; 
					yyg[yygc] = new YYARec(-70,197);yygc++; 
					yyg[yygc] = new YYARec(-69,198);yygc++; 
					yyg[yygc] = new YYARec(-68,199);yygc++; 
					yyg[yygc] = new YYARec(-66,200);yygc++; 
					yyg[yygc] = new YYARec(-64,201);yygc++; 
					yyg[yygc] = new YYARec(-62,202);yygc++; 
					yyg[yygc] = new YYARec(-60,203);yygc++; 
					yyg[yygc] = new YYARec(-59,204);yygc++; 
					yyg[yygc] = new YYARec(-58,205);yygc++; 
					yyg[yygc] = new YYARec(-57,206);yygc++; 
					yyg[yygc] = new YYARec(-56,207);yygc++; 
					yyg[yygc] = new YYARec(-55,208);yygc++; 
					yyg[yygc] = new YYARec(-54,209);yygc++; 
					yyg[yygc] = new YYARec(-53,300);yygc++; 
					yyg[yygc] = new YYARec(-27,220);yygc++; 
					yyg[yygc] = new YYARec(-22,41);yygc++; 
					yyg[yygc] = new YYARec(-77,37);yygc++; 
					yyg[yygc] = new YYARec(-76,38);yygc++; 
					yyg[yygc] = new YYARec(-73,120);yygc++; 
					yyg[yygc] = new YYARec(-71,196);yygc++; 
					yyg[yygc] = new YYARec(-70,197);yygc++; 
					yyg[yygc] = new YYARec(-69,198);yygc++; 
					yyg[yygc] = new YYARec(-68,199);yygc++; 
					yyg[yygc] = new YYARec(-66,301);yygc++; 
					yyg[yygc] = new YYARec(-27,220);yygc++; 
					yyg[yygc] = new YYARec(-22,41);yygc++; 
					yyg[yygc] = new YYARec(-77,37);yygc++; 
					yyg[yygc] = new YYARec(-76,38);yygc++; 
					yyg[yygc] = new YYARec(-73,120);yygc++; 
					yyg[yygc] = new YYARec(-71,196);yygc++; 
					yyg[yygc] = new YYARec(-70,197);yygc++; 
					yyg[yygc] = new YYARec(-69,198);yygc++; 
					yyg[yygc] = new YYARec(-68,199);yygc++; 
					yyg[yygc] = new YYARec(-66,200);yygc++; 
					yyg[yygc] = new YYARec(-64,302);yygc++; 
					yyg[yygc] = new YYARec(-27,220);yygc++; 
					yyg[yygc] = new YYARec(-22,41);yygc++; 
					yyg[yygc] = new YYARec(-77,37);yygc++; 
					yyg[yygc] = new YYARec(-76,38);yygc++; 
					yyg[yygc] = new YYARec(-73,120);yygc++; 
					yyg[yygc] = new YYARec(-71,196);yygc++; 
					yyg[yygc] = new YYARec(-70,197);yygc++; 
					yyg[yygc] = new YYARec(-69,198);yygc++; 
					yyg[yygc] = new YYARec(-68,199);yygc++; 
					yyg[yygc] = new YYARec(-66,200);yygc++; 
					yyg[yygc] = new YYARec(-64,201);yygc++; 
					yyg[yygc] = new YYARec(-62,303);yygc++; 
					yyg[yygc] = new YYARec(-27,220);yygc++; 
					yyg[yygc] = new YYARec(-22,41);yygc++; 
					yyg[yygc] = new YYARec(-77,37);yygc++; 
					yyg[yygc] = new YYARec(-76,38);yygc++; 
					yyg[yygc] = new YYARec(-73,120);yygc++; 
					yyg[yygc] = new YYARec(-71,196);yygc++; 
					yyg[yygc] = new YYARec(-70,197);yygc++; 
					yyg[yygc] = new YYARec(-69,198);yygc++; 
					yyg[yygc] = new YYARec(-68,199);yygc++; 
					yyg[yygc] = new YYARec(-66,200);yygc++; 
					yyg[yygc] = new YYARec(-64,201);yygc++; 
					yyg[yygc] = new YYARec(-62,202);yygc++; 
					yyg[yygc] = new YYARec(-60,304);yygc++; 
					yyg[yygc] = new YYARec(-27,220);yygc++; 
					yyg[yygc] = new YYARec(-22,41);yygc++; 
					yyg[yygc] = new YYARec(-77,37);yygc++; 
					yyg[yygc] = new YYARec(-76,38);yygc++; 
					yyg[yygc] = new YYARec(-73,120);yygc++; 
					yyg[yygc] = new YYARec(-71,196);yygc++; 
					yyg[yygc] = new YYARec(-70,197);yygc++; 
					yyg[yygc] = new YYARec(-69,198);yygc++; 
					yyg[yygc] = new YYARec(-68,199);yygc++; 
					yyg[yygc] = new YYARec(-66,200);yygc++; 
					yyg[yygc] = new YYARec(-64,201);yygc++; 
					yyg[yygc] = new YYARec(-62,202);yygc++; 
					yyg[yygc] = new YYARec(-60,203);yygc++; 
					yyg[yygc] = new YYARec(-59,305);yygc++; 
					yyg[yygc] = new YYARec(-27,220);yygc++; 
					yyg[yygc] = new YYARec(-22,41);yygc++; 
					yyg[yygc] = new YYARec(-77,37);yygc++; 
					yyg[yygc] = new YYARec(-76,38);yygc++; 
					yyg[yygc] = new YYARec(-73,120);yygc++; 
					yyg[yygc] = new YYARec(-71,196);yygc++; 
					yyg[yygc] = new YYARec(-70,197);yygc++; 
					yyg[yygc] = new YYARec(-69,198);yygc++; 
					yyg[yygc] = new YYARec(-68,199);yygc++; 
					yyg[yygc] = new YYARec(-66,200);yygc++; 
					yyg[yygc] = new YYARec(-64,201);yygc++; 
					yyg[yygc] = new YYARec(-62,202);yygc++; 
					yyg[yygc] = new YYARec(-60,203);yygc++; 
					yyg[yygc] = new YYARec(-59,204);yygc++; 
					yyg[yygc] = new YYARec(-58,306);yygc++; 
					yyg[yygc] = new YYARec(-27,220);yygc++; 
					yyg[yygc] = new YYARec(-22,41);yygc++; 
					yyg[yygc] = new YYARec(-77,37);yygc++; 
					yyg[yygc] = new YYARec(-76,38);yygc++; 
					yyg[yygc] = new YYARec(-73,120);yygc++; 
					yyg[yygc] = new YYARec(-71,196);yygc++; 
					yyg[yygc] = new YYARec(-70,197);yygc++; 
					yyg[yygc] = new YYARec(-69,198);yygc++; 
					yyg[yygc] = new YYARec(-68,199);yygc++; 
					yyg[yygc] = new YYARec(-66,200);yygc++; 
					yyg[yygc] = new YYARec(-64,201);yygc++; 
					yyg[yygc] = new YYARec(-62,202);yygc++; 
					yyg[yygc] = new YYARec(-60,203);yygc++; 
					yyg[yygc] = new YYARec(-59,204);yygc++; 
					yyg[yygc] = new YYARec(-58,205);yygc++; 
					yyg[yygc] = new YYARec(-57,307);yygc++; 
					yyg[yygc] = new YYARec(-27,220);yygc++; 
					yyg[yygc] = new YYARec(-22,41);yygc++; 
					yyg[yygc] = new YYARec(-77,37);yygc++; 
					yyg[yygc] = new YYARec(-76,38);yygc++; 
					yyg[yygc] = new YYARec(-73,120);yygc++; 
					yyg[yygc] = new YYARec(-71,196);yygc++; 
					yyg[yygc] = new YYARec(-70,197);yygc++; 
					yyg[yygc] = new YYARec(-69,198);yygc++; 
					yyg[yygc] = new YYARec(-68,199);yygc++; 
					yyg[yygc] = new YYARec(-66,200);yygc++; 
					yyg[yygc] = new YYARec(-64,201);yygc++; 
					yyg[yygc] = new YYARec(-62,202);yygc++; 
					yyg[yygc] = new YYARec(-60,203);yygc++; 
					yyg[yygc] = new YYARec(-59,204);yygc++; 
					yyg[yygc] = new YYARec(-58,205);yygc++; 
					yyg[yygc] = new YYARec(-57,206);yygc++; 
					yyg[yygc] = new YYARec(-56,308);yygc++; 
					yyg[yygc] = new YYARec(-27,220);yygc++; 
					yyg[yygc] = new YYARec(-22,41);yygc++; 
					yyg[yygc] = new YYARec(-77,37);yygc++; 
					yyg[yygc] = new YYARec(-76,38);yygc++; 
					yyg[yygc] = new YYARec(-73,120);yygc++; 
					yyg[yygc] = new YYARec(-71,196);yygc++; 
					yyg[yygc] = new YYARec(-70,197);yygc++; 
					yyg[yygc] = new YYARec(-69,198);yygc++; 
					yyg[yygc] = new YYARec(-68,199);yygc++; 
					yyg[yygc] = new YYARec(-66,200);yygc++; 
					yyg[yygc] = new YYARec(-64,201);yygc++; 
					yyg[yygc] = new YYARec(-62,202);yygc++; 
					yyg[yygc] = new YYARec(-60,203);yygc++; 
					yyg[yygc] = new YYARec(-59,204);yygc++; 
					yyg[yygc] = new YYARec(-58,205);yygc++; 
					yyg[yygc] = new YYARec(-57,206);yygc++; 
					yyg[yygc] = new YYARec(-56,207);yygc++; 
					yyg[yygc] = new YYARec(-55,309);yygc++; 
					yyg[yygc] = new YYARec(-27,220);yygc++; 
					yyg[yygc] = new YYARec(-22,41);yygc++; 
					yyg[yygc] = new YYARec(-77,37);yygc++; 
					yyg[yygc] = new YYARec(-76,38);yygc++; 
					yyg[yygc] = new YYARec(-73,120);yygc++; 
					yyg[yygc] = new YYARec(-71,196);yygc++; 
					yyg[yygc] = new YYARec(-70,197);yygc++; 
					yyg[yygc] = new YYARec(-69,198);yygc++; 
					yyg[yygc] = new YYARec(-68,199);yygc++; 
					yyg[yygc] = new YYARec(-66,200);yygc++; 
					yyg[yygc] = new YYARec(-64,201);yygc++; 
					yyg[yygc] = new YYARec(-62,202);yygc++; 
					yyg[yygc] = new YYARec(-60,203);yygc++; 
					yyg[yygc] = new YYARec(-59,204);yygc++; 
					yyg[yygc] = new YYARec(-58,205);yygc++; 
					yyg[yygc] = new YYARec(-57,206);yygc++; 
					yyg[yygc] = new YYARec(-56,207);yygc++; 
					yyg[yygc] = new YYARec(-55,208);yygc++; 
					yyg[yygc] = new YYARec(-54,209);yygc++; 
					yyg[yygc] = new YYARec(-53,310);yygc++; 
					yyg[yygc] = new YYARec(-27,220);yygc++; 
					yyg[yygc] = new YYARec(-22,41);yygc++; 
					yyg[yygc] = new YYARec(-77,37);yygc++; 
					yyg[yygc] = new YYARec(-76,38);yygc++; 
					yyg[yygc] = new YYARec(-73,128);yygc++; 
					yyg[yygc] = new YYARec(-51,129);yygc++; 
					yyg[yygc] = new YYARec(-49,130);yygc++; 
					yyg[yygc] = new YYARec(-48,131);yygc++; 
					yyg[yygc] = new YYARec(-47,132);yygc++; 
					yyg[yygc] = new YYARec(-46,133);yygc++; 
					yyg[yygc] = new YYARec(-27,134);yygc++; 
					yyg[yygc] = new YYARec(-22,135);yygc++; 
					yyg[yygc] = new YYARec(-16,313);yygc++; 
					yyg[yygc] = new YYARec(-14,137);yygc++; 
					yyg[yygc] = new YYARec(-77,37);yygc++; 
					yyg[yygc] = new YYARec(-76,38);yygc++; 
					yyg[yygc] = new YYARec(-73,128);yygc++; 
					yyg[yygc] = new YYARec(-51,129);yygc++; 
					yyg[yygc] = new YYARec(-49,130);yygc++; 
					yyg[yygc] = new YYARec(-48,131);yygc++; 
					yyg[yygc] = new YYARec(-47,132);yygc++; 
					yyg[yygc] = new YYARec(-46,133);yygc++; 
					yyg[yygc] = new YYARec(-27,134);yygc++; 
					yyg[yygc] = new YYARec(-22,135);yygc++; 
					yyg[yygc] = new YYARec(-16,314);yygc++; 
					yyg[yygc] = new YYARec(-14,137);yygc++; 
					yyg[yygc] = new YYARec(-29,315);yygc++; 
					yyg[yygc] = new YYARec(-41,153);yygc++; 
					yyg[yygc] = new YYARec(-40,154);yygc++; 
					yyg[yygc] = new YYARec(-39,155);yygc++; 
					yyg[yygc] = new YYARec(-34,156);yygc++; 
					yyg[yygc] = new YYARec(-22,157);yygc++; 
					yyg[yygc] = new YYARec(-19,316);yygc++; 
					yyg[yygc] = new YYARec(-18,317);yygc++; 
					yyg[yygc] = new YYARec(-17,159);yygc++; 
					yyg[yygc] = new YYARec(-41,153);yygc++; 
					yyg[yygc] = new YYARec(-40,154);yygc++; 
					yyg[yygc] = new YYARec(-39,155);yygc++; 
					yyg[yygc] = new YYARec(-34,156);yygc++; 
					yyg[yygc] = new YYARec(-22,157);yygc++; 
					yyg[yygc] = new YYARec(-19,316);yygc++; 
					yyg[yygc] = new YYARec(-18,318);yygc++; 
					yyg[yygc] = new YYARec(-17,159);yygc++; 
					yyg[yygc] = new YYARec(-69,229);yygc++; 
					yyg[yygc] = new YYARec(-37,230);yygc++; 
					yyg[yygc] = new YYARec(-36,319);yygc++; 
					yyg[yygc] = new YYARec(-77,37);yygc++; 
					yyg[yygc] = new YYARec(-76,38);yygc++; 
					yyg[yygc] = new YYARec(-75,86);yygc++; 
					yyg[yygc] = new YYARec(-73,120);yygc++; 
					yyg[yygc] = new YYARec(-69,87);yygc++; 
					yyg[yygc] = new YYARec(-52,239);yygc++; 
					yyg[yygc] = new YYARec(-50,321);yygc++; 
					yyg[yygc] = new YYARec(-37,88);yygc++; 
					yyg[yygc] = new YYARec(-27,241);yygc++; 
					yyg[yygc] = new YYARec(-26,242);yygc++; 
					yyg[yygc] = new YYARec(-25,243);yygc++; 
					yyg[yygc] = new YYARec(-24,244);yygc++; 
					yyg[yygc] = new YYARec(-23,245);yygc++; 
					yyg[yygc] = new YYARec(-22,41);yygc++; 
					yyg[yygc] = new YYARec(-77,37);yygc++; 
					yyg[yygc] = new YYARec(-76,38);yygc++; 
					yyg[yygc] = new YYARec(-73,128);yygc++; 
					yyg[yygc] = new YYARec(-51,129);yygc++; 
					yyg[yygc] = new YYARec(-49,130);yygc++; 
					yyg[yygc] = new YYARec(-48,131);yygc++; 
					yyg[yygc] = new YYARec(-47,132);yygc++; 
					yyg[yygc] = new YYARec(-46,133);yygc++; 
					yyg[yygc] = new YYARec(-27,134);yygc++; 
					yyg[yygc] = new YYARec(-22,135);yygc++; 
					yyg[yygc] = new YYARec(-16,322);yygc++; 
					yyg[yygc] = new YYARec(-14,137);yygc++; 
					yyg[yygc] = new YYARec(-67,256);yygc++; 
					yyg[yygc] = new YYARec(-65,260);yygc++; 
					yyg[yygc] = new YYARec(-63,263);yygc++; 
					yyg[yygc] = new YYARec(-61,268);yygc++; 
					yyg[yygc] = new YYARec(-77,37);yygc++; 
					yyg[yygc] = new YYARec(-76,38);yygc++; 
					yyg[yygc] = new YYARec(-75,86);yygc++; 
					yyg[yygc] = new YYARec(-73,120);yygc++; 
					yyg[yygc] = new YYARec(-69,87);yygc++; 
					yyg[yygc] = new YYARec(-43,282);yygc++; 
					yyg[yygc] = new YYARec(-42,326);yygc++; 
					yyg[yygc] = new YYARec(-37,88);yygc++; 
					yyg[yygc] = new YYARec(-27,284);yygc++; 
					yyg[yygc] = new YYARec(-26,285);yygc++; 
					yyg[yygc] = new YYARec(-25,286);yygc++; 
					yyg[yygc] = new YYARec(-23,287);yygc++; 
					yyg[yygc] = new YYARec(-22,41);yygc++; 
					yyg[yygc] = new YYARec(-41,153);yygc++; 
					yyg[yygc] = new YYARec(-40,154);yygc++; 
					yyg[yygc] = new YYARec(-39,155);yygc++; 
					yyg[yygc] = new YYARec(-34,156);yygc++; 
					yyg[yygc] = new YYARec(-22,157);yygc++; 
					yyg[yygc] = new YYARec(-19,333);yygc++; 
					yyg[yygc] = new YYARec(-17,159);yygc++;

					yyd = new int[yynstates];
					yyd[0] = 0;  
					yyd[1] = 0;  
					yyd[2] = -60;  
					yyd[3] = 0;  
					yyd[4] = 0;  
					yyd[5] = 0;  
					yyd[6] = 0;  
					yyd[7] = -41;  
					yyd[8] = -12;  
					yyd[9] = -11;  
					yyd[10] = -10;  
					yyd[11] = -9;  
					yyd[12] = -8;  
					yyd[13] = -7;  
					yyd[14] = -6;  
					yyd[15] = 0;  
					yyd[16] = -1;  
					yyd[17] = 0;  
					yyd[18] = 0;  
					yyd[19] = -4;  
					yyd[20] = 0;  
					yyd[21] = 0;  
					yyd[22] = 0;  
					yyd[23] = 0;  
					yyd[24] = -5;  
					yyd[25] = -43;  
					yyd[26] = -40;  
					yyd[27] = -54;  
					yyd[28] = -58;  
					yyd[29] = -72;  
					yyd[30] = -182;  
					yyd[31] = -38;  
					yyd[32] = -42;  
					yyd[33] = -59;  
					yyd[34] = -180;  
					yyd[35] = -181;  
					yyd[36] = -39;  
					yyd[37] = -207;  
					yyd[38] = -206;  
					yyd[39] = -209;  
					yyd[40] = 0;  
					yyd[41] = -205;  
					yyd[42] = -204;  
					yyd[43] = -191;  
					yyd[44] = -194;  
					yyd[45] = -189;  
					yyd[46] = -196;  
					yyd[47] = -193;  
					yyd[48] = -195;  
					yyd[49] = -192;  
					yyd[50] = -197;  
					yyd[51] = -190;  
					yyd[52] = -202;  
					yyd[53] = -203;  
					yyd[54] = -186;  
					yyd[55] = -185;  
					yyd[56] = -188;  
					yyd[57] = -187;  
					yyd[58] = -199;  
					yyd[59] = -201;  
					yyd[60] = -200;  
					yyd[61] = -183;  
					yyd[62] = -198;  
					yyd[63] = -184;  
					yyd[64] = -178;  
					yyd[65] = -208;  
					yyd[66] = 0;  
					yyd[67] = -211;  
					yyd[68] = 0;  
					yyd[69] = -210;  
					yyd[70] = 0;  
					yyd[71] = -93;  
					yyd[72] = 0;  
					yyd[73] = -2;  
					yyd[74] = -29;  
					yyd[75] = -28;  
					yyd[76] = 0;  
					yyd[77] = 0;  
					yyd[78] = 0;  
					yyd[79] = 0;  
					yyd[80] = 0;  
					yyd[81] = -213;  
					yyd[82] = 0;  
					yyd[83] = 0;  
					yyd[84] = -57;  
					yyd[85] = 0;  
					yyd[86] = -170;  
					yyd[87] = 0;  
					yyd[88] = -171;  
					yyd[89] = 0;  
					yyd[90] = -48;  
					yyd[91] = -49;  
					yyd[92] = -46;  
					yyd[93] = -47;  
					yyd[94] = -132;  
					yyd[95] = -133;  
					yyd[96] = -131;  
					yyd[97] = -175;  
					yyd[98] = -177;  
					yyd[99] = -214;  
					yyd[100] = 0;  
					yyd[101] = 0;  
					yyd[102] = 0;  
					yyd[103] = 0;  
					yyd[104] = -26;  
					yyd[105] = 0;  
					yyd[106] = -27;  
					yyd[107] = -35;  
					yyd[108] = 0;  
					yyd[109] = 0;  
					yyd[110] = 0;  
					yyd[111] = 0;  
					yyd[112] = -174;  
					yyd[113] = -176;  
					yyd[114] = -37;  
					yyd[115] = 0;  
					yyd[116] = -36;  
					yyd[117] = 0;  
					yyd[118] = 0;  
					yyd[119] = 0;  
					yyd[120] = 0;  
					yyd[121] = -34;  
					yyd[122] = -33;  
					yyd[123] = -32;  
					yyd[124] = -31;  
					yyd[125] = -30;  
					yyd[126] = 0;  
					yyd[127] = -179;  
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
					yyd[139] = -85;  
					yyd[140] = 0;  
					yyd[141] = 0;  
					yyd[142] = 0;  
					yyd[143] = 0;  
					yyd[144] = 0;  
					yyd[145] = 0;  
					yyd[146] = 0;  
					yyd[147] = 0;  
					yyd[148] = 0;  
					yyd[149] = 0;  
					yyd[150] = 0;  
					yyd[151] = 0;  
					yyd[152] = -56;  
					yyd[153] = 0;  
					yyd[154] = 0;  
					yyd[155] = -163;  
					yyd[156] = -164;  
					yyd[157] = -162;  
					yyd[158] = 0;  
					yyd[159] = 0;  
					yyd[160] = 0;  
					yyd[161] = 0;  
					yyd[162] = -161;  
					yyd[163] = -157;  
					yyd[164] = -156;  
					yyd[165] = -159;  
					yyd[166] = -160;  
					yyd[167] = -158;  
					yyd[168] = -155;  
					yyd[169] = 0;  
					yyd[170] = -45;  
					yyd[171] = -13;  
					yyd[172] = 0;  
					yyd[173] = -16;  
					yyd[174] = -14;  
					yyd[175] = 0;  
					yyd[176] = -25;  
					yyd[177] = -84;  
					yyd[178] = 0;  
					yyd[179] = -83;  
					yyd[180] = -78;  
					yyd[181] = -76;  
					yyd[182] = 0;  
					yyd[183] = 0;  
					yyd[184] = -137;  
					yyd[185] = -138;  
					yyd[186] = -139;  
					yyd[187] = -140;  
					yyd[188] = -141;  
					yyd[189] = 0;  
					yyd[190] = -71;  
					yyd[191] = -77;  
					yyd[192] = 0;  
					yyd[193] = 0;  
					yyd[194] = 0;  
					yyd[195] = 0;  
					yyd[196] = -118;  
					yyd[197] = 0;  
					yyd[198] = 0;  
					yyd[199] = -114;  
					yyd[200] = -112;  
					yyd[201] = 0;  
					yyd[202] = 0;  
					yyd[203] = 0;  
					yyd[204] = 0;  
					yyd[205] = 0;  
					yyd[206] = 0;  
					yyd[207] = 0;  
					yyd[208] = 0;  
					yyd[209] = 0;  
					yyd[210] = -134;  
					yyd[211] = 0;  
					yyd[212] = 0;  
					yyd[213] = 0;  
					yyd[214] = 0;  
					yyd[215] = 0;  
					yyd[216] = -173;  
					yyd[217] = -172;  
					yyd[218] = 0;  
					yyd[219] = 0;  
					yyd[220] = -119;  
					yyd[221] = 0;  
					yyd[222] = 0;  
					yyd[223] = 0;  
					yyd[224] = 0;  
					yyd[225] = -55;  
					yyd[226] = -62;  
					yyd[227] = 0;  
					yyd[228] = 0;  
					yyd[229] = 0;  
					yyd[230] = 0;  
					yyd[231] = 0;  
					yyd[232] = 0;  
					yyd[233] = -148;  
					yyd[234] = -149;  
					yyd[235] = -154;  
					yyd[236] = -152;  
					yyd[237] = -153;  
					yyd[238] = -151;  
					yyd[239] = 0;  
					yyd[240] = 0;  
					yyd[241] = -88;  
					yyd[242] = -91;  
					yyd[243] = -92;  
					yyd[244] = -89;  
					yyd[245] = -90;  
					yyd[246] = -75;  
					yyd[247] = -136;  
					yyd[248] = 0;  
					yyd[249] = 0;  
					yyd[250] = -17;  
					yyd[251] = -18;  
					yyd[252] = -80;  
					yyd[253] = 0;  
					yyd[254] = 0;  
					yyd[255] = -115;  
					yyd[256] = 0;  
					yyd[257] = -128;  
					yyd[258] = -129;  
					yyd[259] = -130;  
					yyd[260] = 0;  
					yyd[261] = -126;  
					yyd[262] = -127;  
					yyd[263] = 0;  
					yyd[264] = -122;  
					yyd[265] = -123;  
					yyd[266] = -124;  
					yyd[267] = -125;  
					yyd[268] = 0;  
					yyd[269] = -120;  
					yyd[270] = -121;  
					yyd[271] = 0;  
					yyd[272] = 0;  
					yyd[273] = 0;  
					yyd[274] = 0;  
					yyd[275] = 0;  
					yyd[276] = 0;  
					yyd[277] = 0;  
					yyd[278] = 0;  
					yyd[279] = 0;  
					yyd[280] = 0;  
					yyd[281] = -73;  
					yyd[282] = 0;  
					yyd[283] = -64;  
					yyd[284] = -68;  
					yyd[285] = -69;  
					yyd[286] = -70;  
					yyd[287] = -67;  
					yyd[288] = -61;  
					yyd[289] = 0;  
					yyd[290] = 0;  
					yyd[291] = 0;  
					yyd[292] = -50;  
					yyd[293] = 0;  
					yyd[294] = 0;  
					yyd[295] = -81;  
					yyd[296] = -82;  
					yyd[297] = 0;  
					yyd[298] = -20;  
					yyd[299] = -74;  
					yyd[300] = 0;  
					yyd[301] = -113;  
					yyd[302] = 0;  
					yyd[303] = 0;  
					yyd[304] = 0;  
					yyd[305] = 0;  
					yyd[306] = 0;  
					yyd[307] = 0;  
					yyd[308] = 0;  
					yyd[309] = 0;  
					yyd[310] = -135;  
					yyd[311] = -117;  
					yyd[312] = -145;  
					yyd[313] = 0;  
					yyd[314] = 0;  
					yyd[315] = 0;  
					yyd[316] = 0;  
					yyd[317] = 0;  
					yyd[318] = 0;  
					yyd[319] = -51;  
					yyd[320] = -15;  
					yyd[321] = -87;  
					yyd[322] = 0;  
					yyd[323] = -116;  
					yyd[324] = -146;  
					yyd[325] = -147;  
					yyd[326] = -66;  
					yyd[327] = 0;  
					yyd[328] = -24;  
					yyd[329] = -21;  
					yyd[330] = -22;  
					yyd[331] = -19;  
					yyd[332] = 0;  
					yyd[333] = 0;  
					yyd[334] = -23; 

					yyal = new int[yynstates];
					yyal[0] = 1;  
					yyal[1] = 21;  
					yyal[2] = 44;  
					yyal[3] = 44;  
					yyal[4] = 67;  
					yyal[5] = 91;  
					yyal[6] = 100;  
					yyal[7] = 124;  
					yyal[8] = 124;  
					yyal[9] = 124;  
					yyal[10] = 124;  
					yyal[11] = 124;  
					yyal[12] = 124;  
					yyal[13] = 124;  
					yyal[14] = 124;  
					yyal[15] = 124;  
					yyal[16] = 146;  
					yyal[17] = 146;  
					yyal[18] = 147;  
					yyal[19] = 151;  
					yyal[20] = 151;  
					yyal[21] = 155;  
					yyal[22] = 159;  
					yyal[23] = 163;  
					yyal[24] = 164;  
					yyal[25] = 164;  
					yyal[26] = 164;  
					yyal[27] = 164;  
					yyal[28] = 164;  
					yyal[29] = 164;  
					yyal[30] = 164;  
					yyal[31] = 164;  
					yyal[32] = 164;  
					yyal[33] = 164;  
					yyal[34] = 164;  
					yyal[35] = 164;  
					yyal[36] = 164;  
					yyal[37] = 164;  
					yyal[38] = 164;  
					yyal[39] = 164;  
					yyal[40] = 164;  
					yyal[41] = 166;  
					yyal[42] = 166;  
					yyal[43] = 166;  
					yyal[44] = 166;  
					yyal[45] = 166;  
					yyal[46] = 166;  
					yyal[47] = 166;  
					yyal[48] = 166;  
					yyal[49] = 166;  
					yyal[50] = 166;  
					yyal[51] = 166;  
					yyal[52] = 166;  
					yyal[53] = 166;  
					yyal[54] = 166;  
					yyal[55] = 166;  
					yyal[56] = 166;  
					yyal[57] = 166;  
					yyal[58] = 166;  
					yyal[59] = 166;  
					yyal[60] = 166;  
					yyal[61] = 166;  
					yyal[62] = 166;  
					yyal[63] = 166;  
					yyal[64] = 166;  
					yyal[65] = 166;  
					yyal[66] = 166;  
					yyal[67] = 170;  
					yyal[68] = 170;  
					yyal[69] = 172;  
					yyal[70] = 172;  
					yyal[71] = 180;  
					yyal[72] = 180;  
					yyal[73] = 203;  
					yyal[74] = 203;  
					yyal[75] = 203;  
					yyal[76] = 203;  
					yyal[77] = 204;  
					yyal[78] = 205;  
					yyal[79] = 207;  
					yyal[80] = 208;  
					yyal[81] = 209;  
					yyal[82] = 209;  
					yyal[83] = 210;  
					yyal[84] = 212;  
					yyal[85] = 212;  
					yyal[86] = 213;  
					yyal[87] = 213;  
					yyal[88] = 215;  
					yyal[89] = 215;  
					yyal[90] = 216;  
					yyal[91] = 216;  
					yyal[92] = 216;  
					yyal[93] = 216;  
					yyal[94] = 216;  
					yyal[95] = 216;  
					yyal[96] = 216;  
					yyal[97] = 216;  
					yyal[98] = 216;  
					yyal[99] = 216;  
					yyal[100] = 216;  
					yyal[101] = 241;  
					yyal[102] = 242;  
					yyal[103] = 263;  
					yyal[104] = 284;  
					yyal[105] = 284;  
					yyal[106] = 315;  
					yyal[107] = 315;  
					yyal[108] = 315;  
					yyal[109] = 348;  
					yyal[110] = 349;  
					yyal[111] = 362;  
					yyal[112] = 368;  
					yyal[113] = 368;  
					yyal[114] = 368;  
					yyal[115] = 368;  
					yyal[116] = 392;  
					yyal[117] = 392;  
					yyal[118] = 393;  
					yyal[119] = 395;  
					yyal[120] = 396;  
					yyal[121] = 451;  
					yyal[122] = 451;  
					yyal[123] = 451;  
					yyal[124] = 451;  
					yyal[125] = 451;  
					yyal[126] = 451;  
					yyal[127] = 452;  
					yyal[128] = 452;  
					yyal[129] = 459;  
					yyal[130] = 460;  
					yyal[131] = 493;  
					yyal[132] = 528;  
					yyal[133] = 563;  
					yyal[134] = 564;  
					yyal[135] = 569;  
					yyal[136] = 608;  
					yyal[137] = 609;  
					yyal[138] = 644;  
					yyal[139] = 648;  
					yyal[140] = 648;  
					yyal[141] = 652;  
					yyal[142] = 656;  
					yyal[143] = 689;  
					yyal[144] = 718;  
					yyal[145] = 752;  
					yyal[146] = 781;  
					yyal[147] = 810;  
					yyal[148] = 850;  
					yyal[149] = 890;  
					yyal[150] = 930;  
					yyal[151] = 970;  
					yyal[152] = 1010;  
					yyal[153] = 1010;  
					yyal[154] = 1041;  
					yyal[155] = 1042;  
					yyal[156] = 1042;  
					yyal[157] = 1042;  
					yyal[158] = 1042;  
					yyal[159] = 1043;  
					yyal[160] = 1058;  
					yyal[161] = 1062;  
					yyal[162] = 1066;  
					yyal[163] = 1066;  
					yyal[164] = 1066;  
					yyal[165] = 1066;  
					yyal[166] = 1066;  
					yyal[167] = 1066;  
					yyal[168] = 1066;  
					yyal[169] = 1066;  
					yyal[170] = 1071;  
					yyal[171] = 1071;  
					yyal[172] = 1071;  
					yyal[173] = 1072;  
					yyal[174] = 1072;  
					yyal[175] = 1072;  
					yyal[176] = 1086;  
					yyal[177] = 1086;  
					yyal[178] = 1086;  
					yyal[179] = 1117;  
					yyal[180] = 1117;  
					yyal[181] = 1117;  
					yyal[182] = 1117;  
					yyal[183] = 1152;  
					yyal[184] = 1181;  
					yyal[185] = 1181;  
					yyal[186] = 1181;  
					yyal[187] = 1181;  
					yyal[188] = 1181;  
					yyal[189] = 1181;  
					yyal[190] = 1212;  
					yyal[191] = 1212;  
					yyal[192] = 1212;  
					yyal[193] = 1246;  
					yyal[194] = 1280;  
					yyal[195] = 1281;  
					yyal[196] = 1282;  
					yyal[197] = 1282;  
					yyal[198] = 1283;  
					yyal[199] = 1312;  
					yyal[200] = 1312;  
					yyal[201] = 1312;  
					yyal[202] = 1331;  
					yyal[203] = 1347;  
					yyal[204] = 1361;  
					yyal[205] = 1371;  
					yyal[206] = 1379;  
					yyal[207] = 1386;  
					yyal[208] = 1392;  
					yyal[209] = 1397;  
					yyal[210] = 1401;  
					yyal[211] = 1401;  
					yyal[212] = 1423;  
					yyal[213] = 1452;  
					yyal[214] = 1478;  
					yyal[215] = 1504;  
					yyal[216] = 1530;  
					yyal[217] = 1530;  
					yyal[218] = 1530;  
					yyal[219] = 1563;  
					yyal[220] = 1564;  
					yyal[221] = 1564;  
					yyal[222] = 1565;  
					yyal[223] = 1600;  
					yyal[224] = 1630;  
					yyal[225] = 1645;  
					yyal[226] = 1645;  
					yyal[227] = 1645;  
					yyal[228] = 1646;  
					yyal[229] = 1647;  
					yyal[230] = 1648;  
					yyal[231] = 1650;  
					yyal[232] = 1651;  
					yyal[233] = 1671;  
					yyal[234] = 1671;  
					yyal[235] = 1671;  
					yyal[236] = 1671;  
					yyal[237] = 1671;  
					yyal[238] = 1671;  
					yyal[239] = 1671;  
					yyal[240] = 1704;  
					yyal[241] = 1705;  
					yyal[242] = 1705;  
					yyal[243] = 1705;  
					yyal[244] = 1705;  
					yyal[245] = 1705;  
					yyal[246] = 1705;  
					yyal[247] = 1705;  
					yyal[248] = 1705;  
					yyal[249] = 1706;  
					yyal[250] = 1708;  
					yyal[251] = 1708;  
					yyal[252] = 1708;  
					yyal[253] = 1708;  
					yyal[254] = 1743;  
					yyal[255] = 1772;  
					yyal[256] = 1772;  
					yyal[257] = 1801;  
					yyal[258] = 1801;  
					yyal[259] = 1801;  
					yyal[260] = 1801;  
					yyal[261] = 1830;  
					yyal[262] = 1830;  
					yyal[263] = 1830;  
					yyal[264] = 1859;  
					yyal[265] = 1859;  
					yyal[266] = 1859;  
					yyal[267] = 1859;  
					yyal[268] = 1859;  
					yyal[269] = 1888;  
					yyal[270] = 1888;  
					yyal[271] = 1888;  
					yyal[272] = 1917;  
					yyal[273] = 1946;  
					yyal[274] = 1975;  
					yyal[275] = 2004;  
					yyal[276] = 2033;  
					yyal[277] = 2062;  
					yyal[278] = 2063;  
					yyal[279] = 2064;  
					yyal[280] = 2097;  
					yyal[281] = 2130;  
					yyal[282] = 2130;  
					yyal[283] = 2162;  
					yyal[284] = 2162;  
					yyal[285] = 2162;  
					yyal[286] = 2162;  
					yyal[287] = 2162;  
					yyal[288] = 2162;  
					yyal[289] = 2162;  
					yyal[290] = 2176;  
					yyal[291] = 2190;  
					yyal[292] = 2195;  
					yyal[293] = 2195;  
					yyal[294] = 2196;  
					yyal[295] = 2228;  
					yyal[296] = 2228;  
					yyal[297] = 2228;  
					yyal[298] = 2261;  
					yyal[299] = 2261;  
					yyal[300] = 2261;  
					yyal[301] = 2262;  
					yyal[302] = 2262;  
					yyal[303] = 2281;  
					yyal[304] = 2297;  
					yyal[305] = 2311;  
					yyal[306] = 2321;  
					yyal[307] = 2329;  
					yyal[308] = 2336;  
					yyal[309] = 2342;  
					yyal[310] = 2347;  
					yyal[311] = 2347;  
					yyal[312] = 2347;  
					yyal[313] = 2347;  
					yyal[314] = 2348;  
					yyal[315] = 2349;  
					yyal[316] = 2380;  
					yyal[317] = 2382;  
					yyal[318] = 2383;  
					yyal[319] = 2384;  
					yyal[320] = 2384;  
					yyal[321] = 2384;  
					yyal[322] = 2384;  
					yyal[323] = 2385;  
					yyal[324] = 2385;  
					yyal[325] = 2385;  
					yyal[326] = 2385;  
					yyal[327] = 2385;  
					yyal[328] = 2386;  
					yyal[329] = 2386;  
					yyal[330] = 2386;  
					yyal[331] = 2386;  
					yyal[332] = 2386;  
					yyal[333] = 2399;  
					yyal[334] = 2400; 

					yyah = new int[yynstates];
					yyah[0] = 20;  
					yyah[1] = 43;  
					yyah[2] = 43;  
					yyah[3] = 66;  
					yyah[4] = 90;  
					yyah[5] = 99;  
					yyah[6] = 123;  
					yyah[7] = 123;  
					yyah[8] = 123;  
					yyah[9] = 123;  
					yyah[10] = 123;  
					yyah[11] = 123;  
					yyah[12] = 123;  
					yyah[13] = 123;  
					yyah[14] = 123;  
					yyah[15] = 145;  
					yyah[16] = 145;  
					yyah[17] = 146;  
					yyah[18] = 150;  
					yyah[19] = 150;  
					yyah[20] = 154;  
					yyah[21] = 158;  
					yyah[22] = 162;  
					yyah[23] = 163;  
					yyah[24] = 163;  
					yyah[25] = 163;  
					yyah[26] = 163;  
					yyah[27] = 163;  
					yyah[28] = 163;  
					yyah[29] = 163;  
					yyah[30] = 163;  
					yyah[31] = 163;  
					yyah[32] = 163;  
					yyah[33] = 163;  
					yyah[34] = 163;  
					yyah[35] = 163;  
					yyah[36] = 163;  
					yyah[37] = 163;  
					yyah[38] = 163;  
					yyah[39] = 163;  
					yyah[40] = 165;  
					yyah[41] = 165;  
					yyah[42] = 165;  
					yyah[43] = 165;  
					yyah[44] = 165;  
					yyah[45] = 165;  
					yyah[46] = 165;  
					yyah[47] = 165;  
					yyah[48] = 165;  
					yyah[49] = 165;  
					yyah[50] = 165;  
					yyah[51] = 165;  
					yyah[52] = 165;  
					yyah[53] = 165;  
					yyah[54] = 165;  
					yyah[55] = 165;  
					yyah[56] = 165;  
					yyah[57] = 165;  
					yyah[58] = 165;  
					yyah[59] = 165;  
					yyah[60] = 165;  
					yyah[61] = 165;  
					yyah[62] = 165;  
					yyah[63] = 165;  
					yyah[64] = 165;  
					yyah[65] = 165;  
					yyah[66] = 169;  
					yyah[67] = 169;  
					yyah[68] = 171;  
					yyah[69] = 171;  
					yyah[70] = 179;  
					yyah[71] = 179;  
					yyah[72] = 202;  
					yyah[73] = 202;  
					yyah[74] = 202;  
					yyah[75] = 202;  
					yyah[76] = 203;  
					yyah[77] = 204;  
					yyah[78] = 206;  
					yyah[79] = 207;  
					yyah[80] = 208;  
					yyah[81] = 208;  
					yyah[82] = 209;  
					yyah[83] = 211;  
					yyah[84] = 211;  
					yyah[85] = 212;  
					yyah[86] = 212;  
					yyah[87] = 214;  
					yyah[88] = 214;  
					yyah[89] = 215;  
					yyah[90] = 215;  
					yyah[91] = 215;  
					yyah[92] = 215;  
					yyah[93] = 215;  
					yyah[94] = 215;  
					yyah[95] = 215;  
					yyah[96] = 215;  
					yyah[97] = 215;  
					yyah[98] = 215;  
					yyah[99] = 215;  
					yyah[100] = 240;  
					yyah[101] = 241;  
					yyah[102] = 262;  
					yyah[103] = 283;  
					yyah[104] = 283;  
					yyah[105] = 314;  
					yyah[106] = 314;  
					yyah[107] = 314;  
					yyah[108] = 347;  
					yyah[109] = 348;  
					yyah[110] = 361;  
					yyah[111] = 367;  
					yyah[112] = 367;  
					yyah[113] = 367;  
					yyah[114] = 367;  
					yyah[115] = 391;  
					yyah[116] = 391;  
					yyah[117] = 392;  
					yyah[118] = 394;  
					yyah[119] = 395;  
					yyah[120] = 450;  
					yyah[121] = 450;  
					yyah[122] = 450;  
					yyah[123] = 450;  
					yyah[124] = 450;  
					yyah[125] = 450;  
					yyah[126] = 451;  
					yyah[127] = 451;  
					yyah[128] = 458;  
					yyah[129] = 459;  
					yyah[130] = 492;  
					yyah[131] = 527;  
					yyah[132] = 562;  
					yyah[133] = 563;  
					yyah[134] = 568;  
					yyah[135] = 607;  
					yyah[136] = 608;  
					yyah[137] = 643;  
					yyah[138] = 647;  
					yyah[139] = 647;  
					yyah[140] = 651;  
					yyah[141] = 655;  
					yyah[142] = 688;  
					yyah[143] = 717;  
					yyah[144] = 751;  
					yyah[145] = 780;  
					yyah[146] = 809;  
					yyah[147] = 849;  
					yyah[148] = 889;  
					yyah[149] = 929;  
					yyah[150] = 969;  
					yyah[151] = 1009;  
					yyah[152] = 1009;  
					yyah[153] = 1040;  
					yyah[154] = 1041;  
					yyah[155] = 1041;  
					yyah[156] = 1041;  
					yyah[157] = 1041;  
					yyah[158] = 1042;  
					yyah[159] = 1057;  
					yyah[160] = 1061;  
					yyah[161] = 1065;  
					yyah[162] = 1065;  
					yyah[163] = 1065;  
					yyah[164] = 1065;  
					yyah[165] = 1065;  
					yyah[166] = 1065;  
					yyah[167] = 1065;  
					yyah[168] = 1065;  
					yyah[169] = 1070;  
					yyah[170] = 1070;  
					yyah[171] = 1070;  
					yyah[172] = 1071;  
					yyah[173] = 1071;  
					yyah[174] = 1071;  
					yyah[175] = 1085;  
					yyah[176] = 1085;  
					yyah[177] = 1085;  
					yyah[178] = 1116;  
					yyah[179] = 1116;  
					yyah[180] = 1116;  
					yyah[181] = 1116;  
					yyah[182] = 1151;  
					yyah[183] = 1180;  
					yyah[184] = 1180;  
					yyah[185] = 1180;  
					yyah[186] = 1180;  
					yyah[187] = 1180;  
					yyah[188] = 1180;  
					yyah[189] = 1211;  
					yyah[190] = 1211;  
					yyah[191] = 1211;  
					yyah[192] = 1245;  
					yyah[193] = 1279;  
					yyah[194] = 1280;  
					yyah[195] = 1281;  
					yyah[196] = 1281;  
					yyah[197] = 1282;  
					yyah[198] = 1311;  
					yyah[199] = 1311;  
					yyah[200] = 1311;  
					yyah[201] = 1330;  
					yyah[202] = 1346;  
					yyah[203] = 1360;  
					yyah[204] = 1370;  
					yyah[205] = 1378;  
					yyah[206] = 1385;  
					yyah[207] = 1391;  
					yyah[208] = 1396;  
					yyah[209] = 1400;  
					yyah[210] = 1400;  
					yyah[211] = 1422;  
					yyah[212] = 1451;  
					yyah[213] = 1477;  
					yyah[214] = 1503;  
					yyah[215] = 1529;  
					yyah[216] = 1529;  
					yyah[217] = 1529;  
					yyah[218] = 1562;  
					yyah[219] = 1563;  
					yyah[220] = 1563;  
					yyah[221] = 1564;  
					yyah[222] = 1599;  
					yyah[223] = 1629;  
					yyah[224] = 1644;  
					yyah[225] = 1644;  
					yyah[226] = 1644;  
					yyah[227] = 1645;  
					yyah[228] = 1646;  
					yyah[229] = 1647;  
					yyah[230] = 1649;  
					yyah[231] = 1650;  
					yyah[232] = 1670;  
					yyah[233] = 1670;  
					yyah[234] = 1670;  
					yyah[235] = 1670;  
					yyah[236] = 1670;  
					yyah[237] = 1670;  
					yyah[238] = 1670;  
					yyah[239] = 1703;  
					yyah[240] = 1704;  
					yyah[241] = 1704;  
					yyah[242] = 1704;  
					yyah[243] = 1704;  
					yyah[244] = 1704;  
					yyah[245] = 1704;  
					yyah[246] = 1704;  
					yyah[247] = 1704;  
					yyah[248] = 1705;  
					yyah[249] = 1707;  
					yyah[250] = 1707;  
					yyah[251] = 1707;  
					yyah[252] = 1707;  
					yyah[253] = 1742;  
					yyah[254] = 1771;  
					yyah[255] = 1771;  
					yyah[256] = 1800;  
					yyah[257] = 1800;  
					yyah[258] = 1800;  
					yyah[259] = 1800;  
					yyah[260] = 1829;  
					yyah[261] = 1829;  
					yyah[262] = 1829;  
					yyah[263] = 1858;  
					yyah[264] = 1858;  
					yyah[265] = 1858;  
					yyah[266] = 1858;  
					yyah[267] = 1858;  
					yyah[268] = 1887;  
					yyah[269] = 1887;  
					yyah[270] = 1887;  
					yyah[271] = 1916;  
					yyah[272] = 1945;  
					yyah[273] = 1974;  
					yyah[274] = 2003;  
					yyah[275] = 2032;  
					yyah[276] = 2061;  
					yyah[277] = 2062;  
					yyah[278] = 2063;  
					yyah[279] = 2096;  
					yyah[280] = 2129;  
					yyah[281] = 2129;  
					yyah[282] = 2161;  
					yyah[283] = 2161;  
					yyah[284] = 2161;  
					yyah[285] = 2161;  
					yyah[286] = 2161;  
					yyah[287] = 2161;  
					yyah[288] = 2161;  
					yyah[289] = 2175;  
					yyah[290] = 2189;  
					yyah[291] = 2194;  
					yyah[292] = 2194;  
					yyah[293] = 2195;  
					yyah[294] = 2227;  
					yyah[295] = 2227;  
					yyah[296] = 2227;  
					yyah[297] = 2260;  
					yyah[298] = 2260;  
					yyah[299] = 2260;  
					yyah[300] = 2261;  
					yyah[301] = 2261;  
					yyah[302] = 2280;  
					yyah[303] = 2296;  
					yyah[304] = 2310;  
					yyah[305] = 2320;  
					yyah[306] = 2328;  
					yyah[307] = 2335;  
					yyah[308] = 2341;  
					yyah[309] = 2346;  
					yyah[310] = 2346;  
					yyah[311] = 2346;  
					yyah[312] = 2346;  
					yyah[313] = 2347;  
					yyah[314] = 2348;  
					yyah[315] = 2379;  
					yyah[316] = 2381;  
					yyah[317] = 2382;  
					yyah[318] = 2383;  
					yyah[319] = 2383;  
					yyah[320] = 2383;  
					yyah[321] = 2383;  
					yyah[322] = 2384;  
					yyah[323] = 2384;  
					yyah[324] = 2384;  
					yyah[325] = 2384;  
					yyah[326] = 2384;  
					yyah[327] = 2385;  
					yyah[328] = 2385;  
					yyah[329] = 2385;  
					yyah[330] = 2385;  
					yyah[331] = 2385;  
					yyah[332] = 2398;  
					yyah[333] = 2399;  
					yyah[334] = 2399; 

					yygl = new int[yynstates];
					yygl[0] = 1;  
					yygl[1] = 18;  
					yygl[2] = 23;  
					yygl[3] = 23;  
					yygl[4] = 28;  
					yygl[5] = 33;  
					yygl[6] = 34;  
					yygl[7] = 35;  
					yygl[8] = 35;  
					yygl[9] = 35;  
					yygl[10] = 35;  
					yygl[11] = 35;  
					yygl[12] = 35;  
					yygl[13] = 35;  
					yygl[14] = 35;  
					yygl[15] = 35;  
					yygl[16] = 51;  
					yygl[17] = 51;  
					yygl[18] = 51;  
					yygl[19] = 54;  
					yygl[20] = 54;  
					yygl[21] = 57;  
					yygl[22] = 60;  
					yygl[23] = 63;  
					yygl[24] = 64;  
					yygl[25] = 64;  
					yygl[26] = 64;  
					yygl[27] = 64;  
					yygl[28] = 64;  
					yygl[29] = 64;  
					yygl[30] = 64;  
					yygl[31] = 64;  
					yygl[32] = 64;  
					yygl[33] = 64;  
					yygl[34] = 64;  
					yygl[35] = 64;  
					yygl[36] = 64;  
					yygl[37] = 64;  
					yygl[38] = 64;  
					yygl[39] = 64;  
					yygl[40] = 64;  
					yygl[41] = 65;  
					yygl[42] = 65;  
					yygl[43] = 65;  
					yygl[44] = 65;  
					yygl[45] = 65;  
					yygl[46] = 65;  
					yygl[47] = 65;  
					yygl[48] = 65;  
					yygl[49] = 65;  
					yygl[50] = 65;  
					yygl[51] = 65;  
					yygl[52] = 65;  
					yygl[53] = 65;  
					yygl[54] = 65;  
					yygl[55] = 65;  
					yygl[56] = 65;  
					yygl[57] = 65;  
					yygl[58] = 65;  
					yygl[59] = 65;  
					yygl[60] = 65;  
					yygl[61] = 65;  
					yygl[62] = 65;  
					yygl[63] = 65;  
					yygl[64] = 65;  
					yygl[65] = 65;  
					yygl[66] = 65;  
					yygl[67] = 66;  
					yygl[68] = 66;  
					yygl[69] = 67;  
					yygl[70] = 67;  
					yygl[71] = 75;  
					yygl[72] = 75;  
					yygl[73] = 81;  
					yygl[74] = 81;  
					yygl[75] = 81;  
					yygl[76] = 81;  
					yygl[77] = 81;  
					yygl[78] = 81;  
					yygl[79] = 81;  
					yygl[80] = 81;  
					yygl[81] = 81;  
					yygl[82] = 81;  
					yygl[83] = 81;  
					yygl[84] = 82;  
					yygl[85] = 82;  
					yygl[86] = 83;  
					yygl[87] = 83;  
					yygl[88] = 83;  
					yygl[89] = 83;  
					yygl[90] = 83;  
					yygl[91] = 83;  
					yygl[92] = 83;  
					yygl[93] = 83;  
					yygl[94] = 83;  
					yygl[95] = 83;  
					yygl[96] = 83;  
					yygl[97] = 83;  
					yygl[98] = 83;  
					yygl[99] = 83;  
					yygl[100] = 83;  
					yygl[101] = 84;  
					yygl[102] = 84;  
					yygl[103] = 101;  
					yygl[104] = 118;  
					yygl[105] = 118;  
					yygl[106] = 131;  
					yygl[107] = 131;  
					yygl[108] = 131;  
					yygl[109] = 143;  
					yygl[110] = 143;  
					yygl[111] = 150;  
					yygl[112] = 151;  
					yygl[113] = 151;  
					yygl[114] = 151;  
					yygl[115] = 151;  
					yygl[116] = 157;  
					yygl[117] = 157;  
					yygl[118] = 157;  
					yygl[119] = 157;  
					yygl[120] = 157;  
					yygl[121] = 157;  
					yygl[122] = 157;  
					yygl[123] = 157;  
					yygl[124] = 157;  
					yygl[125] = 157;  
					yygl[126] = 157;  
					yygl[127] = 157;  
					yygl[128] = 157;  
					yygl[129] = 157;  
					yygl[130] = 157;  
					yygl[131] = 158;  
					yygl[132] = 170;  
					yygl[133] = 182;  
					yygl[134] = 182;  
					yygl[135] = 183;  
					yygl[136] = 184;  
					yygl[137] = 184;  
					yygl[138] = 196;  
					yygl[139] = 199;  
					yygl[140] = 199;  
					yygl[141] = 202;  
					yygl[142] = 205;  
					yygl[143] = 217;  
					yygl[144] = 237;  
					yygl[145] = 237;  
					yygl[146] = 257;  
					yygl[147] = 277;  
					yygl[148] = 277;  
					yygl[149] = 277;  
					yygl[150] = 277;  
					yygl[151] = 277;  
					yygl[152] = 277;  
					yygl[153] = 277;  
					yygl[154] = 278;  
					yygl[155] = 278;  
					yygl[156] = 278;  
					yygl[157] = 278;  
					yygl[158] = 278;  
					yygl[159] = 278;  
					yygl[160] = 285;  
					yygl[161] = 288;  
					yygl[162] = 291;  
					yygl[163] = 291;  
					yygl[164] = 291;  
					yygl[165] = 291;  
					yygl[166] = 291;  
					yygl[167] = 291;  
					yygl[168] = 291;  
					yygl[169] = 291;  
					yygl[170] = 294;  
					yygl[171] = 294;  
					yygl[172] = 294;  
					yygl[173] = 294;  
					yygl[174] = 294;  
					yygl[175] = 294;  
					yygl[176] = 299;  
					yygl[177] = 299;  
					yygl[178] = 299;  
					yygl[179] = 313;  
					yygl[180] = 313;  
					yygl[181] = 313;  
					yygl[182] = 313;  
					yygl[183] = 325;  
					yygl[184] = 345;  
					yygl[185] = 345;  
					yygl[186] = 345;  
					yygl[187] = 345;  
					yygl[188] = 345;  
					yygl[189] = 345;  
					yygl[190] = 359;  
					yygl[191] = 359;  
					yygl[192] = 359;  
					yygl[193] = 372;  
					yygl[194] = 385;  
					yygl[195] = 385;  
					yygl[196] = 385;  
					yygl[197] = 385;  
					yygl[198] = 385;  
					yygl[199] = 395;  
					yygl[200] = 395;  
					yygl[201] = 395;  
					yygl[202] = 396;  
					yygl[203] = 397;  
					yygl[204] = 398;  
					yygl[205] = 399;  
					yygl[206] = 399;  
					yygl[207] = 399;  
					yygl[208] = 399;  
					yygl[209] = 399;  
					yygl[210] = 399;  
					yygl[211] = 399;  
					yygl[212] = 400;  
					yygl[213] = 420;  
					yygl[214] = 420;  
					yygl[215] = 420;  
					yygl[216] = 420;  
					yygl[217] = 420;  
					yygl[218] = 420;  
					yygl[219] = 432;  
					yygl[220] = 432;  
					yygl[221] = 432;  
					yygl[222] = 432;  
					yygl[223] = 444;  
					yygl[224] = 457;  
					yygl[225] = 464;  
					yygl[226] = 464;  
					yygl[227] = 464;  
					yygl[228] = 464;  
					yygl[229] = 464;  
					yygl[230] = 464;  
					yygl[231] = 464;  
					yygl[232] = 464;  
					yygl[233] = 480;  
					yygl[234] = 480;  
					yygl[235] = 480;  
					yygl[236] = 480;  
					yygl[237] = 480;  
					yygl[238] = 480;  
					yygl[239] = 480;  
					yygl[240] = 481;  
					yygl[241] = 481;  
					yygl[242] = 481;  
					yygl[243] = 481;  
					yygl[244] = 481;  
					yygl[245] = 481;  
					yygl[246] = 481;  
					yygl[247] = 481;  
					yygl[248] = 481;  
					yygl[249] = 481;  
					yygl[250] = 481;  
					yygl[251] = 481;  
					yygl[252] = 481;  
					yygl[253] = 481;  
					yygl[254] = 493;  
					yygl[255] = 513;  
					yygl[256] = 513;  
					yygl[257] = 523;  
					yygl[258] = 523;  
					yygl[259] = 523;  
					yygl[260] = 523;  
					yygl[261] = 534;  
					yygl[262] = 534;  
					yygl[263] = 534;  
					yygl[264] = 546;  
					yygl[265] = 546;  
					yygl[266] = 546;  
					yygl[267] = 546;  
					yygl[268] = 546;  
					yygl[269] = 559;  
					yygl[270] = 559;  
					yygl[271] = 559;  
					yygl[272] = 573;  
					yygl[273] = 588;  
					yygl[274] = 604;  
					yygl[275] = 621;  
					yygl[276] = 639;  
					yygl[277] = 659;  
					yygl[278] = 659;  
					yygl[279] = 659;  
					yygl[280] = 671;  
					yygl[281] = 683;  
					yygl[282] = 683;  
					yygl[283] = 684;  
					yygl[284] = 684;  
					yygl[285] = 684;  
					yygl[286] = 684;  
					yygl[287] = 684;  
					yygl[288] = 684;  
					yygl[289] = 684;  
					yygl[290] = 692;  
					yygl[291] = 700;  
					yygl[292] = 703;  
					yygl[293] = 703;  
					yygl[294] = 703;  
					yygl[295] = 717;  
					yygl[296] = 717;  
					yygl[297] = 717;  
					yygl[298] = 729;  
					yygl[299] = 729;  
					yygl[300] = 729;  
					yygl[301] = 729;  
					yygl[302] = 729;  
					yygl[303] = 730;  
					yygl[304] = 731;  
					yygl[305] = 732;  
					yygl[306] = 733;  
					yygl[307] = 733;  
					yygl[308] = 733;  
					yygl[309] = 733;  
					yygl[310] = 733;  
					yygl[311] = 733;  
					yygl[312] = 733;  
					yygl[313] = 733;  
					yygl[314] = 733;  
					yygl[315] = 733;  
					yygl[316] = 746;  
					yygl[317] = 746;  
					yygl[318] = 746;  
					yygl[319] = 746;  
					yygl[320] = 746;  
					yygl[321] = 746;  
					yygl[322] = 746;  
					yygl[323] = 746;  
					yygl[324] = 746;  
					yygl[325] = 746;  
					yygl[326] = 746;  
					yygl[327] = 746;  
					yygl[328] = 746;  
					yygl[329] = 746;  
					yygl[330] = 746;  
					yygl[331] = 746;  
					yygl[332] = 746;  
					yygl[333] = 753;  
					yygl[334] = 753; 

					yygh = new int[yynstates];
					yygh[0] = 17;  
					yygh[1] = 22;  
					yygh[2] = 22;  
					yygh[3] = 27;  
					yygh[4] = 32;  
					yygh[5] = 33;  
					yygh[6] = 34;  
					yygh[7] = 34;  
					yygh[8] = 34;  
					yygh[9] = 34;  
					yygh[10] = 34;  
					yygh[11] = 34;  
					yygh[12] = 34;  
					yygh[13] = 34;  
					yygh[14] = 34;  
					yygh[15] = 50;  
					yygh[16] = 50;  
					yygh[17] = 50;  
					yygh[18] = 53;  
					yygh[19] = 53;  
					yygh[20] = 56;  
					yygh[21] = 59;  
					yygh[22] = 62;  
					yygh[23] = 63;  
					yygh[24] = 63;  
					yygh[25] = 63;  
					yygh[26] = 63;  
					yygh[27] = 63;  
					yygh[28] = 63;  
					yygh[29] = 63;  
					yygh[30] = 63;  
					yygh[31] = 63;  
					yygh[32] = 63;  
					yygh[33] = 63;  
					yygh[34] = 63;  
					yygh[35] = 63;  
					yygh[36] = 63;  
					yygh[37] = 63;  
					yygh[38] = 63;  
					yygh[39] = 63;  
					yygh[40] = 64;  
					yygh[41] = 64;  
					yygh[42] = 64;  
					yygh[43] = 64;  
					yygh[44] = 64;  
					yygh[45] = 64;  
					yygh[46] = 64;  
					yygh[47] = 64;  
					yygh[48] = 64;  
					yygh[49] = 64;  
					yygh[50] = 64;  
					yygh[51] = 64;  
					yygh[52] = 64;  
					yygh[53] = 64;  
					yygh[54] = 64;  
					yygh[55] = 64;  
					yygh[56] = 64;  
					yygh[57] = 64;  
					yygh[58] = 64;  
					yygh[59] = 64;  
					yygh[60] = 64;  
					yygh[61] = 64;  
					yygh[62] = 64;  
					yygh[63] = 64;  
					yygh[64] = 64;  
					yygh[65] = 64;  
					yygh[66] = 65;  
					yygh[67] = 65;  
					yygh[68] = 66;  
					yygh[69] = 66;  
					yygh[70] = 74;  
					yygh[71] = 74;  
					yygh[72] = 80;  
					yygh[73] = 80;  
					yygh[74] = 80;  
					yygh[75] = 80;  
					yygh[76] = 80;  
					yygh[77] = 80;  
					yygh[78] = 80;  
					yygh[79] = 80;  
					yygh[80] = 80;  
					yygh[81] = 80;  
					yygh[82] = 80;  
					yygh[83] = 81;  
					yygh[84] = 81;  
					yygh[85] = 82;  
					yygh[86] = 82;  
					yygh[87] = 82;  
					yygh[88] = 82;  
					yygh[89] = 82;  
					yygh[90] = 82;  
					yygh[91] = 82;  
					yygh[92] = 82;  
					yygh[93] = 82;  
					yygh[94] = 82;  
					yygh[95] = 82;  
					yygh[96] = 82;  
					yygh[97] = 82;  
					yygh[98] = 82;  
					yygh[99] = 82;  
					yygh[100] = 83;  
					yygh[101] = 83;  
					yygh[102] = 100;  
					yygh[103] = 117;  
					yygh[104] = 117;  
					yygh[105] = 130;  
					yygh[106] = 130;  
					yygh[107] = 130;  
					yygh[108] = 142;  
					yygh[109] = 142;  
					yygh[110] = 149;  
					yygh[111] = 150;  
					yygh[112] = 150;  
					yygh[113] = 150;  
					yygh[114] = 150;  
					yygh[115] = 156;  
					yygh[116] = 156;  
					yygh[117] = 156;  
					yygh[118] = 156;  
					yygh[119] = 156;  
					yygh[120] = 156;  
					yygh[121] = 156;  
					yygh[122] = 156;  
					yygh[123] = 156;  
					yygh[124] = 156;  
					yygh[125] = 156;  
					yygh[126] = 156;  
					yygh[127] = 156;  
					yygh[128] = 156;  
					yygh[129] = 156;  
					yygh[130] = 157;  
					yygh[131] = 169;  
					yygh[132] = 181;  
					yygh[133] = 181;  
					yygh[134] = 182;  
					yygh[135] = 183;  
					yygh[136] = 183;  
					yygh[137] = 195;  
					yygh[138] = 198;  
					yygh[139] = 198;  
					yygh[140] = 201;  
					yygh[141] = 204;  
					yygh[142] = 216;  
					yygh[143] = 236;  
					yygh[144] = 236;  
					yygh[145] = 256;  
					yygh[146] = 276;  
					yygh[147] = 276;  
					yygh[148] = 276;  
					yygh[149] = 276;  
					yygh[150] = 276;  
					yygh[151] = 276;  
					yygh[152] = 276;  
					yygh[153] = 277;  
					yygh[154] = 277;  
					yygh[155] = 277;  
					yygh[156] = 277;  
					yygh[157] = 277;  
					yygh[158] = 277;  
					yygh[159] = 284;  
					yygh[160] = 287;  
					yygh[161] = 290;  
					yygh[162] = 290;  
					yygh[163] = 290;  
					yygh[164] = 290;  
					yygh[165] = 290;  
					yygh[166] = 290;  
					yygh[167] = 290;  
					yygh[168] = 290;  
					yygh[169] = 293;  
					yygh[170] = 293;  
					yygh[171] = 293;  
					yygh[172] = 293;  
					yygh[173] = 293;  
					yygh[174] = 293;  
					yygh[175] = 298;  
					yygh[176] = 298;  
					yygh[177] = 298;  
					yygh[178] = 312;  
					yygh[179] = 312;  
					yygh[180] = 312;  
					yygh[181] = 312;  
					yygh[182] = 324;  
					yygh[183] = 344;  
					yygh[184] = 344;  
					yygh[185] = 344;  
					yygh[186] = 344;  
					yygh[187] = 344;  
					yygh[188] = 344;  
					yygh[189] = 358;  
					yygh[190] = 358;  
					yygh[191] = 358;  
					yygh[192] = 371;  
					yygh[193] = 384;  
					yygh[194] = 384;  
					yygh[195] = 384;  
					yygh[196] = 384;  
					yygh[197] = 384;  
					yygh[198] = 394;  
					yygh[199] = 394;  
					yygh[200] = 394;  
					yygh[201] = 395;  
					yygh[202] = 396;  
					yygh[203] = 397;  
					yygh[204] = 398;  
					yygh[205] = 398;  
					yygh[206] = 398;  
					yygh[207] = 398;  
					yygh[208] = 398;  
					yygh[209] = 398;  
					yygh[210] = 398;  
					yygh[211] = 399;  
					yygh[212] = 419;  
					yygh[213] = 419;  
					yygh[214] = 419;  
					yygh[215] = 419;  
					yygh[216] = 419;  
					yygh[217] = 419;  
					yygh[218] = 431;  
					yygh[219] = 431;  
					yygh[220] = 431;  
					yygh[221] = 431;  
					yygh[222] = 443;  
					yygh[223] = 456;  
					yygh[224] = 463;  
					yygh[225] = 463;  
					yygh[226] = 463;  
					yygh[227] = 463;  
					yygh[228] = 463;  
					yygh[229] = 463;  
					yygh[230] = 463;  
					yygh[231] = 463;  
					yygh[232] = 479;  
					yygh[233] = 479;  
					yygh[234] = 479;  
					yygh[235] = 479;  
					yygh[236] = 479;  
					yygh[237] = 479;  
					yygh[238] = 479;  
					yygh[239] = 480;  
					yygh[240] = 480;  
					yygh[241] = 480;  
					yygh[242] = 480;  
					yygh[243] = 480;  
					yygh[244] = 480;  
					yygh[245] = 480;  
					yygh[246] = 480;  
					yygh[247] = 480;  
					yygh[248] = 480;  
					yygh[249] = 480;  
					yygh[250] = 480;  
					yygh[251] = 480;  
					yygh[252] = 480;  
					yygh[253] = 492;  
					yygh[254] = 512;  
					yygh[255] = 512;  
					yygh[256] = 522;  
					yygh[257] = 522;  
					yygh[258] = 522;  
					yygh[259] = 522;  
					yygh[260] = 533;  
					yygh[261] = 533;  
					yygh[262] = 533;  
					yygh[263] = 545;  
					yygh[264] = 545;  
					yygh[265] = 545;  
					yygh[266] = 545;  
					yygh[267] = 545;  
					yygh[268] = 558;  
					yygh[269] = 558;  
					yygh[270] = 558;  
					yygh[271] = 572;  
					yygh[272] = 587;  
					yygh[273] = 603;  
					yygh[274] = 620;  
					yygh[275] = 638;  
					yygh[276] = 658;  
					yygh[277] = 658;  
					yygh[278] = 658;  
					yygh[279] = 670;  
					yygh[280] = 682;  
					yygh[281] = 682;  
					yygh[282] = 683;  
					yygh[283] = 683;  
					yygh[284] = 683;  
					yygh[285] = 683;  
					yygh[286] = 683;  
					yygh[287] = 683;  
					yygh[288] = 683;  
					yygh[289] = 691;  
					yygh[290] = 699;  
					yygh[291] = 702;  
					yygh[292] = 702;  
					yygh[293] = 702;  
					yygh[294] = 716;  
					yygh[295] = 716;  
					yygh[296] = 716;  
					yygh[297] = 728;  
					yygh[298] = 728;  
					yygh[299] = 728;  
					yygh[300] = 728;  
					yygh[301] = 728;  
					yygh[302] = 729;  
					yygh[303] = 730;  
					yygh[304] = 731;  
					yygh[305] = 732;  
					yygh[306] = 732;  
					yygh[307] = 732;  
					yygh[308] = 732;  
					yygh[309] = 732;  
					yygh[310] = 732;  
					yygh[311] = 732;  
					yygh[312] = 732;  
					yygh[313] = 732;  
					yygh[314] = 732;  
					yygh[315] = 745;  
					yygh[316] = 745;  
					yygh[317] = 745;  
					yygh[318] = 745;  
					yygh[319] = 745;  
					yygh[320] = 745;  
					yygh[321] = 745;  
					yygh[322] = 745;  
					yygh[323] = 745;  
					yygh[324] = 745;  
					yygh[325] = 745;  
					yygh[326] = 745;  
					yygh[327] = 745;  
					yygh[328] = 745;  
					yygh[329] = 745;  
					yygh[330] = 745;  
					yygh[331] = 745;  
					yygh[332] = 752;  
					yygh[333] = 752;  
					yygh[334] = 752; 

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
					yyr[yyrc] = new YYRRec(1,-4);yyrc++; 
					yyr[yyrc] = new YYRRec(5,-6);yyrc++; 
					yyr[yyrc] = new YYRRec(5,-6);yyrc++; 
					yyr[yyrc] = new YYRRec(5,-13);yyrc++; 
					yyr[yyrc] = new YYRRec(2,-13);yyrc++; 
					yyr[yyrc] = new YYRRec(3,-14);yyrc++; 
					yyr[yyrc] = new YYRRec(3,-14);yyrc++; 
					yyr[yyrc] = new YYRRec(4,-15);yyrc++; 
					yyr[yyrc] = new YYRRec(2,-15);yyrc++; 
					yyr[yyrc] = new YYRRec(5,-17);yyrc++; 
					yyr[yyrc] = new YYRRec(5,-17);yyrc++; 
					yyr[yyrc] = new YYRRec(5,-18);yyrc++; 
					yyr[yyrc] = new YYRRec(2,-18);yyrc++; 
					yyr[yyrc] = new YYRRec(5,-10);yyrc++; 
					yyr[yyrc] = new YYRRec(3,-10);yyrc++; 
					yyr[yyrc] = new YYRRec(3,-10);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-12);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-12);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-20);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-20);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-20);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-20);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-20);yyrc++; 
					yyr[yyrc] = new YYRRec(3,-7);yyrc++; 
					yyr[yyrc] = new YYRRec(4,-5);yyrc++; 
					yyr[yyrc] = new YYRRec(4,-5);yyrc++; 
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
					yyr[yyrc] = new YYRRec(1,-43);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-43);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-43);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-43);yyrc++; 
					yyr[yyrc] = new YYRRec(6,-8);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-44);yyrc++; 
					yyr[yyrc] = new YYRRec(3,-16);yyrc++; 
					yyr[yyrc] = new YYRRec(4,-16);yyrc++; 
					yyr[yyrc] = new YYRRec(3,-16);yyrc++; 
					yyr[yyrc] = new YYRRec(2,-16);yyrc++; 
					yyr[yyrc] = new YYRRec(2,-16);yyrc++; 
					yyr[yyrc] = new YYRRec(2,-16);yyrc++; 
					yyr[yyrc] = new YYRRec(0,-16);yyrc++; 
					yyr[yyrc] = new YYRRec(3,-48);yyrc++; 
					yyr[yyrc] = new YYRRec(4,-48);yyrc++; 
					yyr[yyrc] = new YYRRec(4,-48);yyrc++; 
					yyr[yyrc] = new YYRRec(2,-48);yyrc++; 
					yyr[yyrc] = new YYRRec(2,-48);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-48);yyrc++; 
					yyr[yyrc] = new YYRRec(2,-50);yyrc++; 
					yyr[yyrc] = new YYRRec(3,-50);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-52);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-52);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-52);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-52);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-52);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-29);yyrc++; 
					yyr[yyrc] = new YYRRec(0,-29);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-53);yyrc++; 
					yyr[yyrc] = new YYRRec(3,-54);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-54);yyrc++; 
					yyr[yyrc] = new YYRRec(3,-55);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-55);yyrc++; 
					yyr[yyrc] = new YYRRec(3,-56);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-56);yyrc++; 
					yyr[yyrc] = new YYRRec(3,-57);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-57);yyrc++; 
					yyr[yyrc] = new YYRRec(3,-58);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-58);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-59);yyrc++; 
					yyr[yyrc] = new YYRRec(3,-59);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-60);yyrc++; 
					yyr[yyrc] = new YYRRec(3,-60);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-62);yyrc++; 
					yyr[yyrc] = new YYRRec(3,-62);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-64);yyrc++; 
					yyr[yyrc] = new YYRRec(3,-64);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-66);yyrc++; 
					yyr[yyrc] = new YYRRec(2,-66);yyrc++; 
					yyr[yyrc] = new YYRRec(4,-68);yyrc++; 
					yyr[yyrc] = new YYRRec(3,-68);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-68);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-68);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-61);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-61);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-63);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-63);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-63);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-63);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-65);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-65);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-67);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-67);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-67);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-69);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-69);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-69);yyrc++; 
					yyr[yyrc] = new YYRRec(2,-51);yyrc++; 
					yyr[yyrc] = new YYRRec(4,-51);yyrc++; 
					yyr[yyrc] = new YYRRec(3,-51);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-72);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-72);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-72);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-72);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-72);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-70);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-70);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-70);yyrc++; 
					yyr[yyrc] = new YYRRec(4,-47);yyrc++; 
					yyr[yyrc] = new YYRRec(5,-47);yyrc++; 
					yyr[yyrc] = new YYRRec(5,-47);yyrc++; 
					yyr[yyrc] = new YYRRec(3,-27);yyrc++; 
					yyr[yyrc] = new YYRRec(3,-27);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-27);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-74);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-74);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-74);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-74);yyrc++; 
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
					yyr[yyrc] = new YYRRec(1,-49);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-49);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-49);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-49);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-49);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-26);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-26);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-71);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-71);yyrc++; 
					yyr[yyrc] = new YYRRec(2,-37);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-37);yyrc++; 
					yyr[yyrc] = new YYRRec(2,-75);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-75);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-22);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-24);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-21);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-21);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-21);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-76);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-76);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-76);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-76);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-76);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-76);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-76);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-76);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-76);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-76);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-76);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-76);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-76);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-76);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-76);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-77);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-77);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-77);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-77);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-77);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-77);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-73);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-73);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-73);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-73);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-33);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-45);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-35);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-35);yyrc++; 
					yyr[yyrc] = new YYRRec(1,-46);yyrc++; 
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

			if (Regex.IsMatch(Rest,"^((,*[\\s\\t\\x00]*)?;)")){
				Results.Add (t_Char59);
				ResultsV.Add(Regex.Match(Rest,"^((,*[\\s\\t\\x00]*)?;)").Value);}

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

			if (Regex.IsMatch(Rest,"^((?i)(FLOOR_DESCEND|CEIL_DESCEND|FLOOR_ASCEND|FLOOR_LIFTED|CEIL_ASCEND|CEIL_LIFTED|TRANSPARENT|CANDELABER|DIAPHANOUS|IMMATERIAL|IMPASSABLE|PORTCULLIS|AUTORANGE|CAREFULLY|CONDENSED|FLAG[1-8]|INVISIBLE|SENSITIVE|BERKELEY|CENTER_X|CENTER_Y|LIGHTMAP|PASSABLE|SAVE_ALL|CLUSTER|CURTAIN|FRAGILE|NO_CLIP|ONESHOT|REFRESH|VISIBLE|ABSPOS|BEHIND|GROUND|LIFTED|MASTER|NARROW|RELPOS|SHADOW|STICKY|FENCE|GHOST|LIBER|MOVED|SLOOP|BASE|BLUR|CLIP|FLAT|HARD|PLAY|SEEN|WIRE|FAR|SKY))")){
				Results.Add (t_flag);
				ResultsV.Add(Regex.Match(Rest,"^((?i)(FLOOR_DESCEND|CEIL_DESCEND|FLOOR_ASCEND|FLOOR_LIFTED|CEIL_ASCEND|CEIL_LIFTED|TRANSPARENT|CANDELABER|DIAPHANOUS|IMMATERIAL|IMPASSABLE|PORTCULLIS|AUTORANGE|CAREFULLY|CONDENSED|FLAG[1-8]|INVISIBLE|SENSITIVE|BERKELEY|CENTER_X|CENTER_Y|LIGHTMAP|PASSABLE|SAVE_ALL|CLUSTER|CURTAIN|FRAGILE|NO_CLIP|ONESHOT|REFRESH|VISIBLE|ABSPOS|BEHIND|GROUND|LIFTED|MASTER|NARROW|RELPOS|SHADOW|STICKY|FENCE|GHOST|LIBER|MOVED|SLOOP|BASE|BLUR|CLIP|FLAT|HARD|PLAY|SEEN|WIRE|FAR|SKY))").Value);}

			if (Regex.IsMatch(Rest,"^((?i)(ALBEDO|ANGLE|ASPEED|ATTACH|BELOW|BMAPS|BUTTON|CEIL_(ANGLE|OFFS_[X-Y]|TEX)|CYCLES|CYCLE|DEFAULT|DELAY|DIGITS|DISTANCE|DIST|EACH_CYCLE_(C|F)|EACH_CYCLE|FLAGS|FLOOR_(ANGLE|OFFS_[X-Y]|TEX)|FOOT_HGT|FRAME|GENIUS|HBAR|HEIGHT|HSLIDER|IF_(ARISE|ARRIVED|DIVE|FAR|LEAVE|NEAR|RELEASE|TOUCH)|INDEX|LAYER|LEFT|LENGTH|MAP_COLOR|MASK|MAX|MIN|MIRROR|OFFSET_[X-Y]|OVLYS|PALFILE|PAN_MAP|PICTURE|POSITION|POS_[X-Y]|RADIANCE|RANGE|REL_(ANGLE|DIST)|RIGHT|SCALE_(XY|X|Y)|SCYCLES|SCYCLE|SCALE|SDIST|SIDES|SIDE|SIZE_[X-Y]|SKILL[1-8]|SPEED|STRINGS|SVDIST|SVOL|TARGET_(MAP|X|Y)|TARGET|TEXTURE[1-4]|THING_HGT|TITLE|TOP|TOUCH|TYPE|VAL|VBAR|VSLIDER|VSPEED|WAYPOINT|WINDOW|[X-Z][1-2]|[X-Z]))")){
				Results.Add (t_property);
				ResultsV.Add(Regex.Match(Rest,"^((?i)(ALBEDO|ANGLE|ASPEED|ATTACH|BELOW|BMAPS|BUTTON|CEIL_(ANGLE|OFFS_[X-Y]|TEX)|CYCLES|CYCLE|DEFAULT|DELAY|DIGITS|DISTANCE|DIST|EACH_CYCLE_(C|F)|EACH_CYCLE|FLAGS|FLOOR_(ANGLE|OFFS_[X-Y]|TEX)|FOOT_HGT|FRAME|GENIUS|HBAR|HEIGHT|HSLIDER|IF_(ARISE|ARRIVED|DIVE|FAR|LEAVE|NEAR|RELEASE|TOUCH)|INDEX|LAYER|LEFT|LENGTH|MAP_COLOR|MASK|MAX|MIN|MIRROR|OFFSET_[X-Y]|OVLYS|PALFILE|PAN_MAP|PICTURE|POSITION|POS_[X-Y]|RADIANCE|RANGE|REL_(ANGLE|DIST)|RIGHT|SCALE_(XY|X|Y)|SCYCLES|SCYCLE|SCALE|SDIST|SIDES|SIDE|SIZE_[X-Y]|SKILL[1-8]|SPEED|STRINGS|SVDIST|SVOL|TARGET_(MAP|X|Y)|TARGET|TEXTURE[1-4]|THING_HGT|TITLE|TOP|TOUCH|TYPE|VAL|VBAR|VSLIDER|VSPEED|WAYPOINT|WINDOW|[X-Z][1-2]|[X-Z]))").Value);}

			if (Regex.IsMatch(Rest,"^((?i)(ACCEL|ADD_STRING|ADDT|ADD|AND|BEEP|BRANCH|BREAK|CALL|DROP|END|EXCLUSIVE|EXEC_RULES|EXIT|EXPLODE|FADE_PAL|FIND|FREEZE|GETMIDI|GOTO|IF_(ABOVE|BELOW|EQUAL|MAX|MIN|NEQUAL)|INKEY|INPORT|LEVEL|LIFT|LOAD_INFO|LOAD|LOCATE|MAP|MIDI_COM|NEXT_(MY_THERE|MY|THERE)|NOP|OUTPORT|PLACE|PLAY_(CD|DEMO|FLICFILE|FLIC|SONG_ONCE|SONG|SOUNDFILE|SOUND)|PRINTFILE|PRINT_(STRING|VALUE)|PUSH|RANDOMIZE|ROTATE|SAVE_(DEMO|INFO)|SCAN|SCREENSHOT|SETMIDI|SET_(ALL|INFO|SKILL|STRING)|SET|SHAKE|SHIFT|SHOOT|SKIP|STOP_(DEMO|FLIC|SOUND)|SUBT|SUB|TILT|TO_STRING|WAITT|WAIT_TICKS|WAIT))")){
				Results.Add (t_command);
				ResultsV.Add(Regex.Match(Rest,"^((?i)(ACCEL|ADD_STRING|ADDT|ADD|AND|BEEP|BRANCH|BREAK|CALL|DROP|END|EXCLUSIVE|EXEC_RULES|EXIT|EXPLODE|FADE_PAL|FIND|FREEZE|GETMIDI|GOTO|IF_(ABOVE|BELOW|EQUAL|MAX|MIN|NEQUAL)|INKEY|INPORT|LEVEL|LIFT|LOAD_INFO|LOAD|LOCATE|MAP|MIDI_COM|NEXT_(MY_THERE|MY|THERE)|NOP|OUTPORT|PLACE|PLAY_(CD|DEMO|FLICFILE|FLIC|SONG_ONCE|SONG|SOUNDFILE|SOUND)|PRINTFILE|PRINT_(STRING|VALUE)|PUSH|RANDOMIZE|ROTATE|SAVE_(DEMO|INFO)|SCAN|SCREENSHOT|SETMIDI|SET_(ALL|INFO|SKILL|STRING)|SET|SHAKE|SHIFT|SHOOT|SKIP|STOP_(DEMO|FLIC|SOUND)|SUBT|SUB|TILT|TO_STRING|WAITT|WAIT_TICKS|WAIT))").Value);}

			if (Regex.IsMatch(Rest,"^((?i)((EACH_TICK|EACH_SEC|PANELS|LAYERS|MESSAGES)\\.(1[0-6]|[1-9])))")){
				Results.Add (t_list);
				ResultsV.Add(Regex.Match(Rest,"^((?i)((EACH_TICK|EACH_SEC|PANELS|LAYERS|MESSAGES)\\.(1[0-6]|[1-9])))").Value);}

			if (Regex.IsMatch(Rest,"^((?i)(ACCELERATION|ACTIONS|ACTIVE_(NEXUS|OBJTICKS|TARGETS)|ACTOR_(CEIL_HGT|CLIMB|DIST|FLOOR_HGT|IMPACT_V[X-Z]|WIDTH)|ASPECT|BLUR_MODE|BOUNCE_V[X-Y]|CDAUDIO_VOL|CD_TRACK|CHANNEL_[0-7]|CHANNEL|CLIPPED|CLIPPING|COLOR_(ACTORS|BORDER|PLAYER|THINGS|WALLS)|DARK_DIST|DEBUG_MODE|DELTA_ANGLE|ERROR|FLIC_FRAME|FLOOR_MODE|FORCE_(AHEAD|ROT|STRAFE|TILT|UP)|FRAME_COLOR|FRICTION|HALF_PI|HIT_(DIST|MINDIST|X|Y)|IMPACT_V(ROT|[X-Z])|INERTIA|INV_DIST|JOYSTICK_[X-Y]|JOY_(4|SENSE)|KEY_(ALT|ANY|APO|BKSL|BKSP|BRACKL|BRACKR|CAL|CAR|COMMA|CTRL|CUD|CUL|CUR|CUU|DEL|END|ENTER|EQUALS|ESC|HOME|INS|JOY4|MINUS|PAUSE|PERIOD|PGDN|PGUP|PLUS|SEMIC|SENSE|SHIFT|SLASH|SPACE|SZ|TAB|F(1[0-2]|[1-9])|[A-Z0-9])|LIGHT_DIST|LOAD_MODE|MAP_(CENTER[X-Y]|(EDGE_[X-Y][1-2])|LAYER|MAX[X-Y]|MIN[X-Y]|MODE|OFFS[X-Y]|ROT|SCALE)|MAX_DIST|MICKEY_[X-Y]|MINV_DIST|MOTION_BLUR|MOUSE_(ANGLE|CALM|LEFT|MIDDLE|MODE|MOVING|RIGHT|SENSE|TIME|X|Y)|MOVE_(ANGLE|MODE)|MUSIC_VOL|MY_(X[1-2]|Y[1-2]|Z[1-2]|X|Y)|PANEL_LAYER|PI|PLAYER_(ANGLE|ARC|CLIMB|COS|DEPTH|HGT|LAST_[X-Y]|LIGHT|MSIN|SIN|SIZE|SPEED|TILT|VROT|V[X-Z]|WIDTH|[X-Z])|PSOUND_(TONE|VOL)|REAL_SPEED|REMOTE_[0-1]|RENDER_MODE|SCREEN_(HGT|WIDTH|X|Y)|SECS|SHIFT_SENSE|SHOOT_(ANGLE|FAC|RANGE|SECTOR|X|Y)|SKIP_FRAMES|SKY_OFFS_[X-Y]|SLOPE_(AHEAD|SIDE|X|Y)|SOUND_VOL|SPANS|STEPS|STR_LEN|TEXT_LAYER|THING_(DIST|WIDTH)|TICKS|TIME_(ACTIONS|CLIPPING|CORR|DRAW|FAC|FBUFFER|SLICES|TARGETS|VERTICES)|TOUCH_(DIST|MODE|RANGE|STATE)|TWO_PI|WALK_(PERIOD|TIME)|WALK|WAVE_PERIOD|WAVE|PALANIM_DELAY))")){
				Results.Add (t_skill);
				ResultsV.Add(Regex.Match(Rest,"^((?i)(ACCELERATION|ACTIONS|ACTIVE_(NEXUS|OBJTICKS|TARGETS)|ACTOR_(CEIL_HGT|CLIMB|DIST|FLOOR_HGT|IMPACT_V[X-Z]|WIDTH)|ASPECT|BLUR_MODE|BOUNCE_V[X-Y]|CDAUDIO_VOL|CD_TRACK|CHANNEL_[0-7]|CHANNEL|CLIPPED|CLIPPING|COLOR_(ACTORS|BORDER|PLAYER|THINGS|WALLS)|DARK_DIST|DEBUG_MODE|DELTA_ANGLE|ERROR|FLIC_FRAME|FLOOR_MODE|FORCE_(AHEAD|ROT|STRAFE|TILT|UP)|FRAME_COLOR|FRICTION|HALF_PI|HIT_(DIST|MINDIST|X|Y)|IMPACT_V(ROT|[X-Z])|INERTIA|INV_DIST|JOYSTICK_[X-Y]|JOY_(4|SENSE)|KEY_(ALT|ANY|APO|BKSL|BKSP|BRACKL|BRACKR|CAL|CAR|COMMA|CTRL|CUD|CUL|CUR|CUU|DEL|END|ENTER|EQUALS|ESC|HOME|INS|JOY4|MINUS|PAUSE|PERIOD|PGDN|PGUP|PLUS|SEMIC|SENSE|SHIFT|SLASH|SPACE|SZ|TAB|F(1[0-2]|[1-9])|[A-Z0-9])|LIGHT_DIST|LOAD_MODE|MAP_(CENTER[X-Y]|(EDGE_[X-Y][1-2])|LAYER|MAX[X-Y]|MIN[X-Y]|MODE|OFFS[X-Y]|ROT|SCALE)|MAX_DIST|MICKEY_[X-Y]|MINV_DIST|MOTION_BLUR|MOUSE_(ANGLE|CALM|LEFT|MIDDLE|MODE|MOVING|RIGHT|SENSE|TIME|X|Y)|MOVE_(ANGLE|MODE)|MUSIC_VOL|MY_(X[1-2]|Y[1-2]|Z[1-2]|X|Y)|PANEL_LAYER|PI|PLAYER_(ANGLE|ARC|CLIMB|COS|DEPTH|HGT|LAST_[X-Y]|LIGHT|MSIN|SIN|SIZE|SPEED|TILT|VROT|V[X-Z]|WIDTH|[X-Z])|PSOUND_(TONE|VOL)|REAL_SPEED|REMOTE_[0-1]|RENDER_MODE|SCREEN_(HGT|WIDTH|X|Y)|SECS|SHIFT_SENSE|SHOOT_(ANGLE|FAC|RANGE|SECTOR|X|Y)|SKIP_FRAMES|SKY_OFFS_[X-Y]|SLOPE_(AHEAD|SIDE|X|Y)|SOUND_VOL|SPANS|STEPS|STR_LEN|TEXT_LAYER|THING_(DIST|WIDTH)|TICKS|TIME_(ACTIONS|CLIPPING|CORR|DRAW|FAC|FBUFFER|SLICES|TARGETS|VERTICES)|TOUCH_(DIST|MODE|RANGE|STATE)|TWO_PI|WALK_(PERIOD|TIME)|WALK|WAVE_PERIOD|WAVE|PALANIM_DELAY))").Value);}

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

			if (Regex.IsMatch(Rest,"^(\"(\\\\\"|.|[\\r\\n])*?\")")){
				Results.Add (t_string);
				ResultsV.Add(Regex.Match(Rest,"^(\"(\\\\\"|.|[\\r\\n])*?\")").Value);}

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
