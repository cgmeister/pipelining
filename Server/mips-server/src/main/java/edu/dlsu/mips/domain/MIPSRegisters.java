package edu.dlsu.mips.domain;

public class MIPSRegisters {
	
	public static MIPSRegisters mipsRegister;
	
	static{
		mipsRegister = new MIPSRegisters();
	}

	public static String PC = "00000000";
	public static String IFIDNPC = "00000000";
	public static String IFIDIR;

	public static String IDEXIR;
	public static String IDEXA;
	public static String IDEXB;
	public static String IDEXIMM;
	public static String IDEXNPC;

	public static String EXMEMIR;
	public static String EXMEMALUOUTPUT;
	public static String EXMEMB;
	public static String EXMEMCOND;

	public static String MEMWBIR;
	public static String MEMWBALUOUTPUT;
	public static String MEMWBLMD;
	
	public static MIPSRegisters getInstance(){
		return mipsRegister;
	}
	
	public String getPC(){
		return PC;
	}
	
	public String getIFIDNPC(){
		return IFIDNPC;
	}
	
	public String getIFIDIR(){
		return IFIDIR;
	}
	
	public String getIDEXIR(){
		return IDEXIR;
	}
	public String getIDEXA(){
		return IDEXA;
	}
	public String getIDEXB(){
		return IDEXB;
	}
	public String getIDEXIMM(){
		return IDEXIMM;
	}
	public String getIDEXNPC(){
		return IDEXNPC;
	}
	
	public String getEXMEMIR(){

		return EXMEMIR;
	}
	public String getEXMEMALUOUTPUT(){
		return EXMEMALUOUTPUT;
	}
	public String getEXMEMB(){
		return EXMEMB;
	}
	public String getEXMEMCOND(){
		return EXMEMCOND;
	}
	
	public String getMEMWBIR(){
		return MEMWBIR;
	}
	
	public String getMEMWBALUOUTPUT(){
		return MEMWBALUOUTPUT;
	}
	public String getMEMWBLMD(){
		return MEMWBLMD;
	}
	

}
