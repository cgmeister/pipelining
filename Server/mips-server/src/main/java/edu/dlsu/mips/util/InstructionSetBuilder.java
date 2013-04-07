package edu.dlsu.mips.util;

import java.math.BigInteger;
import java.util.HashMap;
import java.util.Map;

import edu.dlsu.mips.domain.InstructionSet;
import edu.dlsu.mips.exception.BitLengthException;
import edu.dlsu.mips.exception.JumpAddressException;
import edu.dlsu.mips.exception.OpcodeNotSupportedException;
import edu.dlsu.mips.exception.OperandException;

public class InstructionSetBuilder {

	public static Map<String, String> opcodeType = null;
	public static Map<String, Integer> opcodeValue = null;
	public static Map<String, Integer> opcodeSubValue = null;

	static {
		opcodeType = new HashMap<String, String>();
		opcodeType.put("DADD", "R");
		opcodeType.put("DSUB", "R");
		opcodeType.put("XOR", "R");
		opcodeType.put("SLT", "R");
		opcodeType.put("AND", "R");

		opcodeType.put("BNEZ", "I");
		opcodeType.put("SD", "I");
		opcodeType.put("LD", "I");
		opcodeType.put("DADDI", "I");

		opcodeType.put("J", "J");

		opcodeValue = new HashMap<String, Integer>();
		opcodeValue.put("DADD", 0);
		opcodeValue.put("DSUB", 0);
		opcodeValue.put("XOR", 0);
		opcodeValue.put("SLT", 0);
		opcodeValue.put("AND", 0);

		opcodeValue.put("BNEZ", 5);
		opcodeValue.put("SD", 63);
		opcodeValue.put("LD", 55);
		opcodeValue.put("DADDI", 24);

		opcodeValue.put("J", 2);

		opcodeSubValue = new HashMap<String, Integer>();
		opcodeSubValue.put("DADD", 44);
		opcodeSubValue.put("DSUB", 46);
		opcodeSubValue.put("XOR", 38);
		opcodeSubValue.put("SLT", 42);
		opcodeSubValue.put("AND", 36);
	}

	public static InstructionSet buildInstructionSet(String instruction)
			throws JumpAddressException, OperandException, OpcodeNotSupportedException {
		String[] tokens = instruction.split(" ");
		InstructionSet instructionSet = new InstructionSet();
		instructionSet.setInstruction(instruction);
		String type = opcodeType.get(tokens[0]);
		instructionSet.setOpcode(tokens[0]);
		instructionSet.setType(type);
		if (type.equals("R")) {
			decodeRType(instructionSet);
		} else if (type.equals("I")) {
			decodeIType(instructionSet);
		} else if (type.equals("J")) {
			decodeJType(instructionSet);
		}else{
			throw new OpcodeNotSupportedException();
		}
		return instructionSet;
	}

	private static void decodeJType(InstructionSet instructionSet) throws JumpAddressException,
			OperandException {
		String instruction = instructionSet.getInstruction();
		String[] tokens = instruction.split(" ");
		int nameIntValue = Integer.parseInt(tokens[1]);
		if ((nameIntValue * 4) % 4 != 0) {
			throw new JumpAddressException();
		} else if (tokens.length != 2) {
			throw new OperandException("J Types must have 1 argument <J args0>");
		} else {
			String opcodeBitString = BitStringUtils
					.doUnsignedBinarySignExtention(opcodeValue.get(tokens[0]),
							6);
			instructionSet.setBinaryOpcode(opcodeBitString);
			String nameBitString = BitStringUtils
					.doUnsignedBinarySignExtention(nameIntValue, 26);
			instructionSet.setName(nameBitString);
			String binaryOpcode = opcodeBitString + nameBitString;
			instructionSet.setBinaryInstruction(binaryOpcode);
			BigInteger binaryIntVal = new BigInteger(binaryOpcode, 2);
			String hexOpcode = Long.toHexString(binaryIntVal.longValue());
			try {
				hexOpcode = BitStringUtils.doUnsignedHexSignExtension(
						hexOpcode, 32);
				instructionSet.setTargetLine(nameIntValue);

			} catch (BitLengthException e) {
				// TODO Auto-generated catch block
				e.printStackTrace();
			}
			instructionSet.setHexInstruction(hexOpcode);
			// System.out.println(opcode.getBinaryOpCode());
			// System.out.println(opcode.getHexOpCode());

		}

	}

	private static void decodeRType(InstructionSet instructionSet) throws OperandException {
		String instruction = instructionSet.getInstruction();
		String[] tokens = instruction.split(" ");
		String[] operands = new String[3];
		int operandCount = 0;
		for (int i = 1; i < tokens.length; i++) {
			operandCount++;
			if (tokens[i].charAt(tokens[i].length() - 1) == ',') {
				tokens[i] = tokens[i].substring(0, tokens[i].length() - 1);
			}
			if (tokens[i] == null || tokens[i] == "") {
				break;
			} else if (!Character.toString(tokens[i].charAt(0))
					.equalsIgnoreCase("R")) {
				throw new OperandException(
						"Register Operands must begin with R");
			}
			operands[i - 1] = tokens[i];
		}
		if (operandCount != 3) {
			throw new OperandException(
					"R Types must have 3 arguments <R args0, args1, args2>");
		}
		int opcodeIntValue = opcodeValue.get(tokens[0]);
		int rdAddressIntVal = Integer.parseInt(operands[0].substring(1,
				operands[0].length()));
		int rsAddressIntVal = Integer.parseInt(operands[1].substring(1,
				operands[1].length()));
		int rtAddressIntVal = Integer.parseInt(operands[2].substring(1,
				operands[2].length()));
		int funcIntVal = opcodeSubValue.get(tokens[0]);
		String opcodeBitString = BitStringUtils.doUnsignedBinarySignExtention(
				opcodeIntValue, 6);
		instructionSet.setBinaryOpcode(opcodeBitString);
		String rsBitString = BitStringUtils.doUnsignedBinarySignExtention(
				rsAddressIntVal, 5);
		instructionSet.setRs(rsBitString);
		String rtBitString = BitStringUtils.doUnsignedBinarySignExtention(
				rtAddressIntVal, 5);
		instructionSet.setRt(rtBitString);
		String rdBitString = BitStringUtils.doUnsignedBinarySignExtention(
				rdAddressIntVal, 5);
		instructionSet.setRd(rdBitString);
		String zero = instructionSet.getZero();
		String funcBitString = BitStringUtils.doUnsignedBinarySignExtention(
				funcIntVal, 6);
		instructionSet.setFunc(funcBitString);
		String binaryOpcode = opcodeBitString + rsBitString + rtBitString
				+ rdBitString + zero + funcBitString;
		instructionSet.setBinaryInstruction(binaryOpcode);
		BigInteger binaryIntVal = new BigInteger(binaryOpcode, 2);
		String hexOpcode = Long.toHexString(binaryIntVal.longValue());
		try {
			hexOpcode = BitStringUtils
					.doUnsignedHexSignExtension(hexOpcode, 32);
		} catch (BitLengthException e) {
			// TODO Auto-generated catch block
			e.printStackTrace();
		}
		instructionSet.setHexInstruction(hexOpcode);
		// System.out.println(opcode.getBinaryOpCode());
		// System.out.println(opcode.getHexOpCode());

	}

	private static void decodeIType(InstructionSet instructionSet) throws OperandException {
		String instruction = instructionSet.getInstruction();
		String[] tokens = instruction.split(" ");
		String keyword = tokens[0];
		int opcodeIntValue = opcodeValue.get(tokens[0]);
		if (keyword.equals("BNEZ")) {
			int operandCount = 0;
			String[] operands = new String[3];
			for (int i = 1; i < tokens.length; i++) {
				operandCount++;
				if (tokens[i].charAt(tokens[i].length() - 1) == ',') {
					tokens[i] = tokens[i].substring(0, tokens[i].length() - 1);
				}
				if (tokens[i] == null || tokens[i] == "") {
					break;
				} else if (i == 1
						&& !Character.toString(tokens[i].charAt(0))
								.equalsIgnoreCase("R")) {
					throw new OperandException(
							"Register Operands must begin with R");
				}
				operands[i - 1] = tokens[i];
			}
			if (operandCount != 3) {
				throw new OperandException(
						"BNEZ must have 3 arguments <BNEZ rs, currentPc, targetPc>");
			}
			int rsAddressIntVal = Integer.parseInt(operands[0].substring(1,
					operands[0].length()));
			int nextPc = (Integer.parseInt(operands[1]) * 4) + 4;
			int targetPc = Integer.parseInt(operands[2]) * 4;
			int immIntVal = (targetPc - nextPc) / 4;
			// val * 4 + npc + 4
			String opcodeBitString = BitStringUtils
					.doUnsignedBinarySignExtention(opcodeIntValue, 6);
			instructionSet.setBinaryOpcode(opcodeBitString);
			String rsBitString = BitStringUtils.doUnsignedBinarySignExtention(
					rsAddressIntVal, 5);
			instructionSet.setRs(rsBitString);
			String rdBitString = "00000";
			instructionSet.setRd(rdBitString);
			String immBitString = BitStringUtils.doUnsignedBinarySignExtention(
					immIntVal, 16);
			instructionSet.setImm(immBitString);
			String binaryOpcode = opcodeBitString + rsBitString + rdBitString
					+ immBitString;
			instructionSet.setBinaryInstruction(binaryOpcode);
			BigInteger binaryIntVal = new BigInteger(binaryOpcode, 2);
			String hexOpcode = Long.toHexString(binaryIntVal.longValue());
			try {
				hexOpcode = BitStringUtils.doUnsignedHexSignExtension(
						hexOpcode, 32);
			} catch (BitLengthException e) {
				// TODO Auto-generated catch block
				e.printStackTrace();
			}
			instructionSet.setHexInstruction(hexOpcode);
			instructionSet.setTargetLine(Integer.parseInt(operands[2]));
			// System.out.println(opcode.getBinaryOpCode());
			// System.out.println(opcode.getHexOpCode());

		} else if (keyword.equals("LD") || keyword.equals("SD")) {
			int operandCount = 0;
			String rs = "";
			String imm = "";
			String[] operands = new String[3];
			for (int i = 1; i < tokens.length; i++) {
				operandCount++;
				if (tokens[i].charAt(tokens[i].length() - 1) == ',') {
					tokens[i] = tokens[i].substring(0, tokens[i].length() - 1);
				}
				if (tokens[i] == null || tokens[i] == "") {
					break;
				} else if (i == 1
						&& !Character.toString(tokens[i].charAt(0))
								.equalsIgnoreCase("R")) {
					throw new OperandException(
							"Register Operands must begin with R");
				} else if (i == 2) {
					String rsAndOffset = tokens[i];
					int j = 0;
					boolean isRegister = false;
					while (rsAndOffset.charAt(j) != ')') {
						if (rsAndOffset.charAt(j) != '(' && !isRegister) {
							imm += rsAndOffset.charAt(j);
						} else {
							isRegister = true;

						}
						if (isRegister) {
							if (rsAndOffset.charAt(j) != '(') {
								rs += rsAndOffset.charAt(j);
							}
						}
						j++;
					}
					operands[1] = rs;
					operands[2] = imm;
				}
				if (i == 1) {
					operands[i - 1] = tokens[i];
				}

			}
			if (operandCount != 2) {
				throw new OperandException(
						"LD/SD must have 2 arguments <LD/SD rd, offset(rs)>");
			}
			// System.out.println(operands[0]);
			// System.out.println(operands[1]);
			// System.out.println(operands[2]);
			int rdAddressIntVal = Integer.parseInt(operands[0].substring(1,
					operands[0].length()));
			int rsAddressIntVal = Integer.parseInt(operands[1].substring(1,
					operands[1].length()));
			int immIntVal = Integer.parseInt(operands[2], 16);
			String opcodeBitString = BitStringUtils
					.doUnsignedBinarySignExtention(opcodeIntValue, 6);
			instructionSet.setBinaryOpcode(opcodeBitString);
			String rsBitString = BitStringUtils.doUnsignedBinarySignExtention(
					rsAddressIntVal, 5);
			instructionSet.setRs(rsBitString);
			String rdBitString = BitStringUtils.doUnsignedBinarySignExtention(
					rdAddressIntVal, 5);
			instructionSet.setRd(rdBitString);
			String immBitString = BitStringUtils.doUnsignedBinarySignExtention(
					immIntVal, 16);
			instructionSet.setImm(immBitString);
			String binaryOpcode = opcodeBitString + rsBitString + rdBitString
					+ immBitString;
			instructionSet.setBinaryInstruction(binaryOpcode);
			BigInteger binaryIntVal = new BigInteger(binaryOpcode, 2);
			String hexOpcode = Long.toHexString(binaryIntVal.longValue());
			try {
				hexOpcode = BitStringUtils.doUnsignedHexSignExtension(
						hexOpcode, 32);
			} catch (BitLengthException e) {
				// TODO Auto-generated catch block
				e.printStackTrace();
			}
			instructionSet.setHexInstruction(hexOpcode);

			// System.out.println(opcode.getBinaryOpCode());
			// System.out.println(opcode.getHexOpCode());

		} else {
			int operandCount = 0;
			String[] operands = new String[3];
			for (int i = 1; i < tokens.length; i++) {
				operandCount++;
				if (tokens[i].charAt(tokens[i].length() - 1) == ',') {
					tokens[i] = tokens[i].substring(0, tokens[i].length() - 1);
				}
				if (tokens[i] == null || tokens[i] == "") {
					break;
				} else if (i < 2
						&& !Character.toString(tokens[i].charAt(0))
								.equalsIgnoreCase("R")) {
					throw new OperandException(
							"Register Operands must begin with R");
				} else if (i == 3
						&& !Character.toString(tokens[i].charAt(0))
								.equalsIgnoreCase("#")) {
					throw new OperandException(
							"Immediate Operands should begin with a #");
				}
				operands[i - 1] = tokens[i];

			}
			if (operandCount != 3) {
				throw new OperandException(
						"DADDI must have 3 arguments <DADDI rd, rs, imm>");
			}
			int rdAddressIntVal = Integer.parseInt(operands[0].substring(1,
					operands[0].length()));
			int rsAddressIntVal = Integer.parseInt(operands[1].substring(1,
					operands[1].length()));
			int immIntVal = Integer.parseInt(
					operands[2].substring(1, operands[2].length()), 16);
			String opcodeBitString = BitStringUtils
					.doUnsignedBinarySignExtention(opcodeIntValue, 6);
			instructionSet.setBinaryOpcode(opcodeBitString);
			String rsBitString = BitStringUtils.doUnsignedBinarySignExtention(
					rsAddressIntVal, 5);
			instructionSet.setRs(rsBitString);
			String rdBitString = BitStringUtils.doUnsignedBinarySignExtention(
					rdAddressIntVal, 5);
			instructionSet.setRd(rdBitString);
			String immBitString = BitStringUtils.doUnsignedBinarySignExtention(
					immIntVal, 16);
			instructionSet.setImm(immBitString);
			String binaryOpcode = opcodeBitString + rsBitString + rdBitString
					+ immBitString;
			instructionSet.setBinaryInstruction(binaryOpcode);
			BigInteger binaryIntVal = new BigInteger(binaryOpcode, 2);
			String hexOpcode = Long.toHexString(binaryIntVal.longValue());
			try {
				hexOpcode = BitStringUtils.doUnsignedHexSignExtension(
						hexOpcode, 32);
			} catch (BitLengthException e) {
				// TODO Auto-generated catch block
				e.printStackTrace();
			}
			instructionSet.setHexInstruction(hexOpcode);

		}
		// System.out.println(opcode.getBinaryOpCode());
		// System.out.println(opcode.getHexOpCode());

	}

}
