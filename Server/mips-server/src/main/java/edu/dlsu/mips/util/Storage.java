package edu.dlsu.mips.util;

import java.util.HashMap;
import java.util.Map;

import com.google.common.base.Splitter;
import com.google.common.collect.Iterables;

import edu.dlsu.mips.exception.BitLengthException;
import edu.dlsu.mips.exception.MemoryAddressOverFlowException;
import edu.dlsu.mips.exception.MemoryAddressRangeException;
import edu.dlsu.mips.exception.RegisterAddressOverFlowException;
import edu.dlsu.mips.exception.StorageInitializationException;

public class Storage {

	private static Map<String, String> memory = null;
	private static Map<String, String> registers = null;
	private static final int LAST_MEMORY_ADDRESS = 65535;
	private static final int LAST_REGISTER_ADDRESS = 31;

	public static void initializeStorage() {
		memory = initMemoryStorage();
		registers = initRegisterStorage();
	}

	private static Map<String, String> initMemoryStorage() {
		Map<String, String> memory = new HashMap<String, String>();
		for (int i = 0; i <= LAST_MEMORY_ADDRESS; i++) {
			String address = Integer.toHexString(i);
			try {
				address = BitStringUtils.doUnsignedHexSignExtension(address, 16);
				//System.out.println(address);
			} catch (BitLengthException e) {
				// TODO Auto-generated catch block
				e.printStackTrace();
			}
			memory.put(address.toUpperCase(), "00");
		}
		return memory;
	}

	private static Map<String, String> initRegisterStorage() {
		Map<String, String> registers = new HashMap<String, String>();
		for (int i = 0; i <= LAST_REGISTER_ADDRESS; i++) {
			String key = "R" + i;
			registers.put(key, "0000000000000000");
		}
		return registers;
	}

	public static String getMemoryContents(String address, String offset)
			throws StorageInitializationException,
			MemoryAddressOverFlowException {
		if (Storage.memory == null) {
			throw new StorageInitializationException();
		} else {
			int offsetVal = Integer.parseInt(offset, 16);
			int baseAddress = Integer.parseInt(address, 16);
			if (baseAddress + offsetVal > LAST_MEMORY_ADDRESS) {
				throw new MemoryAddressOverFlowException();
			} else {
				String memoryContents = "";
				for (int i = 0; i < offsetVal; i++) {
					int addressVal = baseAddress + i;
					String memoryAddress = Integer.toHexString(addressVal).toUpperCase();
					try {
						memoryAddress = BitStringUtils.doUnsignedHexSignExtension(
								memoryAddress, 16);
						memoryContents += memory.get(memoryAddress.toUpperCase());
					} catch (BitLengthException e) {
						// TODO Auto-generated catch block
						e.printStackTrace();
					}

				}
				return memoryContents;
			}
		}
	}

	public static void setMemoryContents(String address, String value)
			throws MemoryAddressOverFlowException,
			StorageInitializationException {
		if (Storage.memory == null) {
			throw new StorageInitializationException();
		} else {
			String[] tokens = Iterables.toArray(
					Splitter.fixedLength(2).split(value), String.class);
			for (int i = 0; i < tokens.length; i++) {
				if (tokens[i].length() == 1) {
					try {
						tokens[i] = BitStringUtils.doSignedHexSignExtend(
								tokens[i], 8);
					} catch (BitLengthException e) {
						// TODO Auto-generated catch block
						e.printStackTrace();
					}
				}
			}
			int baseAddress = Integer.parseInt(address, 16);
			if (baseAddress + tokens.length > LAST_MEMORY_ADDRESS) {
				throw new MemoryAddressOverFlowException();
			} else {
				for (int offset = tokens.length - 1; offset >= 0; offset--) {
					int addressValue = baseAddress + offset;
					String memoryAddress = Integer.toHexString(addressValue);
					try {
						memoryAddress = BitStringUtils.doUnsignedHexSignExtension(
								memoryAddress, 16).toUpperCase();
						// System.out.println(memoryAddress + "=>" +
						// tokens[offset]);
						memory.put(memoryAddress.toUpperCase(), tokens[offset]);
					} catch (BitLengthException e) {
						// TODO Auto-generated catch block
						e.printStackTrace();
					}

				}
				// System.out.println("-----------------------");
			}
		}
	}

	public static void setRegisterContents(String address, String value)
			throws StorageInitializationException,
			RegisterAddressOverFlowException {
		if (registers == null) {
			throw new StorageInitializationException();
		} else {
			int addressNumber = Integer.parseInt(address.substring(1,
					address.length()));
			if (addressNumber > LAST_REGISTER_ADDRESS) {
				throw new RegisterAddressOverFlowException();
			} else if (addressNumber != 0) {
				try {
					String registerValue = BitStringUtils
							.doSignedHexSignExtend(value, 64);
					registers.put(address.toUpperCase(), registerValue);
				} catch (BitLengthException e) {
					// TODO Auto-generated catch block
					e.printStackTrace();
				}

			}

		}

	}

	public static String getRegisterContents(String address)
			throws StorageInitializationException,
			RegisterAddressOverFlowException {
		if (registers == null) {
			throw new StorageInitializationException();
		} else {
			int addressNumber = Integer.parseInt(address.substring(1,
					address.length()));
			if (addressNumber > LAST_REGISTER_ADDRESS) {
				throw new RegisterAddressOverFlowException();
			} else {
				return registers.get(address.toUpperCase());

			}

		}
	}

	public static Map<String, String> getMemoryDump(String startAddress,
			String endAddress) throws StorageInitializationException,
			MemoryAddressOverFlowException, MemoryAddressRangeException {
		Map<String, String> memoryDump = new HashMap<String, String>();
		if (Storage.memory == null) {
			throw new StorageInitializationException();
		} else {

			int startAddressValue = Integer.parseInt(startAddress, 16);
			int endAddressValue = Integer.parseInt(endAddress, 16);
			if (startAddressValue > endAddressValue) {
				throw new MemoryAddressRangeException();
			} else if (endAddressValue > LAST_MEMORY_ADDRESS) {
				throw new MemoryAddressOverFlowException();
			} else {
				for (int i = startAddressValue; i <= endAddressValue; i++) {
					
					String memAddress = Integer.toHexString(i);
					try {
						memAddress = BitStringUtils.doUnsignedHexSignExtension(
								memAddress, 16);
						memoryDump.put(memAddress.toUpperCase(),
								memory.get(memAddress.toUpperCase()));
					} catch (BitLengthException e) {
						// TODO Auto-generated catch block
						e.printStackTrace();
					}
				}
			}
		}
		return memoryDump;
	}

	public static Map<String, String> getRegisterDump()
			throws StorageInitializationException {
		if (registers == null) {
			throw new StorageInitializationException();
		} else {
			Map<String, String> registerDump = new HashMap<String, String>();
			for (int i = 0; i <= LAST_REGISTER_ADDRESS; i++) {
				registerDump.put("R" + i, registers.get("R" + i));
			}
			return registerDump;
		}
	}

}
