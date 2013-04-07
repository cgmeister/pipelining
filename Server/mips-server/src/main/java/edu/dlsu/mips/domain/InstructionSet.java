package edu.dlsu.mips.domain;

public class InstructionSet {

	private String type = null;
	private String binaryOpcode = null;
	private String name = null;
	private String rs = null;
	private String rd = null;
	private String rt = null;
	private String imm = null;
	private String zero = "00000";
	private String func = null;
	private String hexInstruction = null;
	private String binaryInstruction = null;
	private String instruction = "";
	private String opcode = "";
	private Integer targetLine = null;

	public String getHexInstruction() {
		return hexInstruction;
	}

	public void setHexInstruction(String hexInstruction) {
		this.hexInstruction = hexInstruction;
	}

	public String getType() {
		return type;
	}

	public void setType(String type) {
		this.type = type;
	}

	public String getBinaryOpcode() {
		return binaryOpcode;
	}

	public void setBinaryOpcode(String binaryOpcode) {
		this.binaryOpcode = binaryOpcode;
	}

	public String getRs() {
		return rs;
	}

	public void setRs(String rs) {
		this.rs = rs;
	}

	public String getRd() {
		return rd;
	}

	public void setRd(String rd) {
		this.rd = rd;
	}

	public String getRt() {
		return rt;
	}

	public void setRt(String rt) {
		this.rt = rt;
	}

	public String getImm() {
		return imm;
	}

	public void setImm(String imm) {
		this.imm = imm;
	}

	public String getZero() {
		return zero;
	}

	public void setZero(String zero) {
		this.zero = zero;
	}

	public String getInstruction() {
		return instruction;
	}

	public void setInstruction(String instruction) {
		this.instruction = instruction;
	}

	public String getBinaryInstruction() {
		return binaryInstruction;
	}

	public void setBinaryInstruction(String binaryInstruction) {
		this.binaryInstruction = binaryInstruction;
	}

	public String getName() {
		return name;
	}

	public void setName(String name) {
		this.name = name;
	}

	public String getFunc() {
		return func;
	}

	public void setFunc(String func) {
		this.func = func;
	}

	public String getOpcode() {
		return opcode;
	}

	public void setOpcode(String opcode) {
		this.opcode = opcode;
	}

	public Integer getTargetLine() {
		return targetLine;
	}

	public void setTargetLine(Integer targetLine) {
		this.targetLine = targetLine;
	}

}
