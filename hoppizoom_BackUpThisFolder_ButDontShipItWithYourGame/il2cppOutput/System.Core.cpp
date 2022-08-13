#include "pch-cpp.hpp"

#ifndef _MSC_VER
# include <alloca.h>
#else
# include <malloc.h>
#endif


#include <limits>
#include <stdint.h>


struct VirtualActionInvoker0
{
	typedef void (*Action)(void*, const RuntimeMethod*);

	static inline void Invoke (Il2CppMethodSlot slot, RuntimeObject* obj)
	{
		const VirtualInvokeData& invokeData = il2cpp_codegen_get_virtual_invoke_data(slot, obj);
		((Action)invokeData.methodPtr)(obj, invokeData.method);
	}
};
template <typename T1>
struct VirtualActionInvoker1
{
	typedef void (*Action)(void*, T1, const RuntimeMethod*);

	static inline void Invoke (Il2CppMethodSlot slot, RuntimeObject* obj, T1 p1)
	{
		const VirtualInvokeData& invokeData = il2cpp_codegen_get_virtual_invoke_data(slot, obj);
		((Action)invokeData.methodPtr)(obj, p1, invokeData.method);
	}
};
template <typename R>
struct VirtualFuncInvoker0
{
	typedef R (*Func)(void*, const RuntimeMethod*);

	static inline R Invoke (Il2CppMethodSlot slot, RuntimeObject* obj)
	{
		const VirtualInvokeData& invokeData = il2cpp_codegen_get_virtual_invoke_data(slot, obj);
		return ((Func)invokeData.methodPtr)(obj, invokeData.method);
	}
};
template <typename R, typename T1, typename T2>
struct VirtualFuncInvoker2
{
	typedef R (*Func)(void*, T1, T2, const RuntimeMethod*);

	static inline R Invoke (Il2CppMethodSlot slot, RuntimeObject* obj, T1 p1, T2 p2)
	{
		const VirtualInvokeData& invokeData = il2cpp_codegen_get_virtual_invoke_data(slot, obj);
		return ((Func)invokeData.methodPtr)(obj, p1, p2, invokeData.method);
	}
};
struct InterfaceActionInvoker0
{
	typedef void (*Action)(void*, const RuntimeMethod*);

	static inline void Invoke (Il2CppMethodSlot slot, RuntimeClass* declaringInterface, RuntimeObject* obj)
	{
		const VirtualInvokeData& invokeData = il2cpp_codegen_get_interface_invoke_data(slot, obj, declaringInterface);
		((Action)invokeData.methodPtr)(obj, invokeData.method);
	}
};
template <typename R>
struct InterfaceFuncInvoker0
{
	typedef R (*Func)(void*, const RuntimeMethod*);

	static inline R Invoke (Il2CppMethodSlot slot, RuntimeClass* declaringInterface, RuntimeObject* obj)
	{
		const VirtualInvokeData& invokeData = il2cpp_codegen_get_interface_invoke_data(slot, obj, declaringInterface);
		return ((Func)invokeData.methodPtr)(obj, invokeData.method);
	}
};

// System.Collections.Generic.IEnumerable`1<System.Int32>
struct IEnumerable_1_tCE758D940790D6D0D56B457E522C195F8C413AF2;
// System.Byte[]
struct ByteU5BU5D_tA6237BF417AE52AD70CFB4EF24A7A82613DF9031;
// System.IntPtr[]
struct IntPtrU5BU5D_tFD177F8C806A6921AD7150264CCC62FA00CAD832;
// System.Security.Cryptography.KeySizes[]
struct KeySizesU5BU5D_tDD87467B9CB683380B5DC92193576A3136DFAE03;
// System.Diagnostics.StackTrace[]
struct StackTraceU5BU5D_t32FBCB20930EAF5BAE3F450FF75228E5450DA0DF;
// System.Security.Cryptography.Aes
struct Aes_tC72E711D7751C8AEAF59C51CA0E61A3857068047;
// System.Security.Cryptography.AesManaged
struct AesManaged_t153AF8C6AF037AF4614B15A04CAD48A7F7DA0819;
// System.ArgumentException
struct ArgumentException_tAD90411542A20A9C72D5CDA3A84181D8B947A263;
// System.ArgumentNullException
struct ArgumentNullException_t327031E412FAB2351B0022DD5DAD47E67E597129;
// System.Security.Cryptography.CryptographicException
struct CryptographicException_tF1AE59E97557B5241330D5998C4D95B6D9D6669F;
// System.Exception
struct Exception_t;
// System.Security.Cryptography.ICryptoTransform
struct ICryptoTransform_tE6DA9E01069FDC62AB562B589C8E43EEC53B2377;
// System.Collections.IDictionary
struct IDictionary_t6D03155AF1FA9083817AA5B6AD7DEEACC26AB220;
// System.InvalidOperationException
struct InvalidOperationException_t5DDE4D49B7405FAAB1E4576F4715A42A3FAD4BAB;
// System.Security.Cryptography.RijndaelManaged
struct RijndaelManaged_t67DFB0E9542F21D9EED0227003A19D9F5ECF2E39;
// System.Runtime.Serialization.SafeSerializationManager
struct SafeSerializationManager_tCBB85B95DFD1634237140CD892E82D06ECB3F5E6;
// System.String
struct String_t;
// System.Security.Cryptography.SymmetricAlgorithm
struct SymmetricAlgorithm_t8C631E4E7B9073CCBD856F8D559A62EB5616BBE8;
// System.Void
struct Void_t4861ACF8F4594C3437BB48B6E56783494B843915;

IL2CPP_EXTERN_C RuntimeClass* Aes_tC72E711D7751C8AEAF59C51CA0E61A3857068047_il2cpp_TypeInfo_var;
IL2CPP_EXTERN_C RuntimeClass* ArgumentException_tAD90411542A20A9C72D5CDA3A84181D8B947A263_il2cpp_TypeInfo_var;
IL2CPP_EXTERN_C RuntimeClass* ArgumentNullException_t327031E412FAB2351B0022DD5DAD47E67E597129_il2cpp_TypeInfo_var;
IL2CPP_EXTERN_C RuntimeClass* CryptoConfig_t66E67DD67B2C3224C72C0CBD80A144E29EE016A5_il2cpp_TypeInfo_var;
IL2CPP_EXTERN_C RuntimeClass* CryptographicException_tF1AE59E97557B5241330D5998C4D95B6D9D6669F_il2cpp_TypeInfo_var;
IL2CPP_EXTERN_C RuntimeClass* IDisposable_t030E0496B4E0E4E4F086825007979AF51F7248C5_il2cpp_TypeInfo_var;
IL2CPP_EXTERN_C RuntimeClass* IEnumerable_1_tCE758D940790D6D0D56B457E522C195F8C413AF2_il2cpp_TypeInfo_var;
IL2CPP_EXTERN_C RuntimeClass* IEnumerator_1_tD6A90A7446DA8E6CF865EDFBBF18C1200BB6D452_il2cpp_TypeInfo_var;
IL2CPP_EXTERN_C RuntimeClass* IEnumerator_t7B609C2FFA6EB5167D9C62A0C32A21DE2F666DAA_il2cpp_TypeInfo_var;
IL2CPP_EXTERN_C RuntimeClass* InvalidOperationException_t5DDE4D49B7405FAAB1E4576F4715A42A3FAD4BAB_il2cpp_TypeInfo_var;
IL2CPP_EXTERN_C RuntimeClass* RijndaelManaged_t67DFB0E9542F21D9EED0227003A19D9F5ECF2E39_il2cpp_TypeInfo_var;
IL2CPP_EXTERN_C String_t* _stringLiteral167E6E6EB13F42BCA02A2CAA251B92A9CCA2C7E4;
IL2CPP_EXTERN_C String_t* _stringLiteral17BA770D472146F14CBDE2CFF7752F076430BCBE;
IL2CPP_EXTERN_C String_t* _stringLiteral55A326D360D02C44975E4A74CD36AA58A27ABDAE;
IL2CPP_EXTERN_C String_t* _stringLiteral66F9618FDA792CAB23AF2D7FFB50AB2D3E393DC5;
IL2CPP_EXTERN_C String_t* _stringLiteral9D0E978C2541B8A36DFB07E397656689CE9E713F;
IL2CPP_EXTERN_C String_t* _stringLiteralB7E78BE66617B9AE36B6A6E170E3545EE25C1D11;
IL2CPP_EXTERN_C String_t* _stringLiteralCBCF5ABE35600EAFD21FA4DC1F78EBBBCAD041EB;
IL2CPP_EXTERN_C String_t* _stringLiteralE7D028CCE3B6E7B61AE2C752D7AE970DA04AB7C6;
IL2CPP_EXTERN_C String_t* _stringLiteralF8BD9A7F60F0A925FBA0FA74162E5EC956585C8D;
IL2CPP_EXTERN_C const RuntimeMethod* AesManaged_CreateDecryptor_m3E4110743E227B1B7568B7D497573631E26C3246_RuntimeMethod_var;
IL2CPP_EXTERN_C const RuntimeMethod* AesManaged_CreateEncryptor_mB200E46332CFFD19367E042932042F4A1DFFD5AF_RuntimeMethod_var;
IL2CPP_EXTERN_C const RuntimeMethod* AesManaged__ctor_mDE017C6ECB4AF587B6C2A7A4BCBB2E0FA3E81241_RuntimeMethod_var;
IL2CPP_EXTERN_C const RuntimeMethod* AesManaged_set_Mode_m154C8319F26FA466C54C7B2BDA602909EEA258C0_RuntimeMethod_var;
IL2CPP_EXTERN_C const RuntimeMethod* Enumerable_Max_m30DE6D3273F1E770CC99EC43653F8F4BA4A94760_RuntimeMethod_var;
IL2CPP_EXTERN_C const RuntimeMethod* Enumerable_Min_m08CFCF12550397A30F094F24E2F68AD21E98EC9F_RuntimeMethod_var;
struct Exception_t_marshaled_com;
struct Exception_t_marshaled_pinvoke;

struct ByteU5BU5D_tA6237BF417AE52AD70CFB4EF24A7A82613DF9031;

IL2CPP_EXTERN_C_BEGIN
IL2CPP_EXTERN_C_END

#ifdef __clang__
#pragma clang diagnostic push
#pragma clang diagnostic ignored "-Winvalid-offsetof"
#pragma clang diagnostic ignored "-Wunused-variable"
#endif

// <Module>
struct U3CModuleU3E_tA3942657997767549ED3B944EB77AFA183BBF4B9 
{
};
struct Il2CppArrayBounds;

// System.Linq.Enumerable
struct Enumerable_t372195206D92B3F390693F9449282C31FD564C09  : public RuntimeObject
{
};

// System.Linq.Error
struct Error_tCE0C9D928B2D2CC69DDEC1A0ECF05131938959DB  : public RuntimeObject
{
};

// SR
struct SR_tF79CE2856F7D3AF3AE5D28C5C52C986CAFBE262F  : public RuntimeObject
{
};

// System.String
struct String_t  : public RuntimeObject
{
	// System.Int32 System.String::_stringLength
	int32_t ____stringLength_4;
	// System.Char System.String::_firstChar
	Il2CppChar ____firstChar_5;
};

struct String_t_StaticFields
{
	// System.String System.String::Empty
	String_t* ___Empty_6;
};

// System.Security.Cryptography.SymmetricAlgorithm
struct SymmetricAlgorithm_t8C631E4E7B9073CCBD856F8D559A62EB5616BBE8  : public RuntimeObject
{
	// System.Int32 System.Security.Cryptography.SymmetricAlgorithm::BlockSizeValue
	int32_t ___BlockSizeValue_0;
	// System.Int32 System.Security.Cryptography.SymmetricAlgorithm::FeedbackSizeValue
	int32_t ___FeedbackSizeValue_1;
	// System.Byte[] System.Security.Cryptography.SymmetricAlgorithm::IVValue
	ByteU5BU5D_tA6237BF417AE52AD70CFB4EF24A7A82613DF9031* ___IVValue_2;
	// System.Byte[] System.Security.Cryptography.SymmetricAlgorithm::KeyValue
	ByteU5BU5D_tA6237BF417AE52AD70CFB4EF24A7A82613DF9031* ___KeyValue_3;
	// System.Security.Cryptography.KeySizes[] System.Security.Cryptography.SymmetricAlgorithm::LegalBlockSizesValue
	KeySizesU5BU5D_tDD87467B9CB683380B5DC92193576A3136DFAE03* ___LegalBlockSizesValue_4;
	// System.Security.Cryptography.KeySizes[] System.Security.Cryptography.SymmetricAlgorithm::LegalKeySizesValue
	KeySizesU5BU5D_tDD87467B9CB683380B5DC92193576A3136DFAE03* ___LegalKeySizesValue_5;
	// System.Int32 System.Security.Cryptography.SymmetricAlgorithm::KeySizeValue
	int32_t ___KeySizeValue_6;
	// System.Security.Cryptography.CipherMode System.Security.Cryptography.SymmetricAlgorithm::ModeValue
	int32_t ___ModeValue_7;
	// System.Security.Cryptography.PaddingMode System.Security.Cryptography.SymmetricAlgorithm::PaddingValue
	int32_t ___PaddingValue_8;
};

// System.ValueType
struct ValueType_t6D9B272BD21782F0A9A14F2E41F85A50E97A986F  : public RuntimeObject
{
};
// Native definition for P/Invoke marshalling of System.ValueType
struct ValueType_t6D9B272BD21782F0A9A14F2E41F85A50E97A986F_marshaled_pinvoke
{
};
// Native definition for COM marshalling of System.ValueType
struct ValueType_t6D9B272BD21782F0A9A14F2E41F85A50E97A986F_marshaled_com
{
};

// System.Security.Cryptography.Aes
struct Aes_tC72E711D7751C8AEAF59C51CA0E61A3857068047  : public SymmetricAlgorithm_t8C631E4E7B9073CCBD856F8D559A62EB5616BBE8
{
};

struct Aes_tC72E711D7751C8AEAF59C51CA0E61A3857068047_StaticFields
{
	// System.Security.Cryptography.KeySizes[] System.Security.Cryptography.Aes::s_legalBlockSizes
	KeySizesU5BU5D_tDD87467B9CB683380B5DC92193576A3136DFAE03* ___s_legalBlockSizes_9;
	// System.Security.Cryptography.KeySizes[] System.Security.Cryptography.Aes::s_legalKeySizes
	KeySizesU5BU5D_tDD87467B9CB683380B5DC92193576A3136DFAE03* ___s_legalKeySizes_10;
};

// System.Boolean
struct Boolean_t09A6377A54BE2F9E6985A8149F19234FD7DDFE22 
{
	// System.Boolean System.Boolean::m_value
	bool ___m_value_0;
};

struct Boolean_t09A6377A54BE2F9E6985A8149F19234FD7DDFE22_StaticFields
{
	// System.String System.Boolean::TrueString
	String_t* ___TrueString_5;
	// System.String System.Boolean::FalseString
	String_t* ___FalseString_6;
};

// System.Byte
struct Byte_t94D9231AC217BE4D2E004C4CD32DF6D099EA41A3 
{
	// System.Byte System.Byte::m_value
	uint8_t ___m_value_0;
};

// System.Int32
struct Int32_t680FF22E76F6EFAD4375103CBBFFA0421349384C 
{
	// System.Int32 System.Int32::m_value
	int32_t ___m_value_0;
};

// System.IntPtr
struct IntPtr_t 
{
	// System.Void* System.IntPtr::m_value
	void* ___m_value_0;
};

struct IntPtr_t_StaticFields
{
	// System.IntPtr System.IntPtr::Zero
	intptr_t ___Zero_1;
};

// System.Security.Cryptography.Rijndael
struct Rijndael_t282661761E53E282457A31F5675ACF53D64EE807  : public SymmetricAlgorithm_t8C631E4E7B9073CCBD856F8D559A62EB5616BBE8
{
};

struct Rijndael_t282661761E53E282457A31F5675ACF53D64EE807_StaticFields
{
	// System.Security.Cryptography.KeySizes[] System.Security.Cryptography.Rijndael::s_legalBlockSizes
	KeySizesU5BU5D_tDD87467B9CB683380B5DC92193576A3136DFAE03* ___s_legalBlockSizes_9;
	// System.Security.Cryptography.KeySizes[] System.Security.Cryptography.Rijndael::s_legalKeySizes
	KeySizesU5BU5D_tDD87467B9CB683380B5DC92193576A3136DFAE03* ___s_legalKeySizes_10;
};

// System.Void
struct Void_t4861ACF8F4594C3437BB48B6E56783494B843915 
{
	union
	{
		struct
		{
		};
		uint8_t Void_t4861ACF8F4594C3437BB48B6E56783494B843915__padding[1];
	};
};

// System.Security.Cryptography.AesManaged
struct AesManaged_t153AF8C6AF037AF4614B15A04CAD48A7F7DA0819  : public Aes_tC72E711D7751C8AEAF59C51CA0E61A3857068047
{
	// System.Security.Cryptography.RijndaelManaged System.Security.Cryptography.AesManaged::m_rijndael
	RijndaelManaged_t67DFB0E9542F21D9EED0227003A19D9F5ECF2E39* ___m_rijndael_11;
};

// System.Exception
struct Exception_t  : public RuntimeObject
{
	// System.String System.Exception::_className
	String_t* ____className_1;
	// System.String System.Exception::_message
	String_t* ____message_2;
	// System.Collections.IDictionary System.Exception::_data
	RuntimeObject* ____data_3;
	// System.Exception System.Exception::_innerException
	Exception_t* ____innerException_4;
	// System.String System.Exception::_helpURL
	String_t* ____helpURL_5;
	// System.Object System.Exception::_stackTrace
	RuntimeObject* ____stackTrace_6;
	// System.String System.Exception::_stackTraceString
	String_t* ____stackTraceString_7;
	// System.String System.Exception::_remoteStackTraceString
	String_t* ____remoteStackTraceString_8;
	// System.Int32 System.Exception::_remoteStackIndex
	int32_t ____remoteStackIndex_9;
	// System.Object System.Exception::_dynamicMethods
	RuntimeObject* ____dynamicMethods_10;
	// System.Int32 System.Exception::_HResult
	int32_t ____HResult_11;
	// System.String System.Exception::_source
	String_t* ____source_12;
	// System.Runtime.Serialization.SafeSerializationManager System.Exception::_safeSerializationManager
	SafeSerializationManager_tCBB85B95DFD1634237140CD892E82D06ECB3F5E6* ____safeSerializationManager_13;
	// System.Diagnostics.StackTrace[] System.Exception::captured_traces
	StackTraceU5BU5D_t32FBCB20930EAF5BAE3F450FF75228E5450DA0DF* ___captured_traces_14;
	// System.IntPtr[] System.Exception::native_trace_ips
	IntPtrU5BU5D_tFD177F8C806A6921AD7150264CCC62FA00CAD832* ___native_trace_ips_15;
	// System.Int32 System.Exception::caught_in_unmanaged
	int32_t ___caught_in_unmanaged_16;
};

struct Exception_t_StaticFields
{
	// System.Object System.Exception::s_EDILock
	RuntimeObject* ___s_EDILock_0;
};
// Native definition for P/Invoke marshalling of System.Exception
struct Exception_t_marshaled_pinvoke
{
	char* ____className_1;
	char* ____message_2;
	RuntimeObject* ____data_3;
	Exception_t_marshaled_pinvoke* ____innerException_4;
	char* ____helpURL_5;
	Il2CppIUnknown* ____stackTrace_6;
	char* ____stackTraceString_7;
	char* ____remoteStackTraceString_8;
	int32_t ____remoteStackIndex_9;
	Il2CppIUnknown* ____dynamicMethods_10;
	int32_t ____HResult_11;
	char* ____source_12;
	SafeSerializationManager_tCBB85B95DFD1634237140CD892E82D06ECB3F5E6* ____safeSerializationManager_13;
	StackTraceU5BU5D_t32FBCB20930EAF5BAE3F450FF75228E5450DA0DF* ___captured_traces_14;
	Il2CppSafeArray/*NONE*/* ___native_trace_ips_15;
	int32_t ___caught_in_unmanaged_16;
};
// Native definition for COM marshalling of System.Exception
struct Exception_t_marshaled_com
{
	Il2CppChar* ____className_1;
	Il2CppChar* ____message_2;
	RuntimeObject* ____data_3;
	Exception_t_marshaled_com* ____innerException_4;
	Il2CppChar* ____helpURL_5;
	Il2CppIUnknown* ____stackTrace_6;
	Il2CppChar* ____stackTraceString_7;
	Il2CppChar* ____remoteStackTraceString_8;
	int32_t ____remoteStackIndex_9;
	Il2CppIUnknown* ____dynamicMethods_10;
	int32_t ____HResult_11;
	Il2CppChar* ____source_12;
	SafeSerializationManager_tCBB85B95DFD1634237140CD892E82D06ECB3F5E6* ____safeSerializationManager_13;
	StackTraceU5BU5D_t32FBCB20930EAF5BAE3F450FF75228E5450DA0DF* ___captured_traces_14;
	Il2CppSafeArray/*NONE*/* ___native_trace_ips_15;
	int32_t ___caught_in_unmanaged_16;
};

// System.Security.Cryptography.RijndaelManaged
struct RijndaelManaged_t67DFB0E9542F21D9EED0227003A19D9F5ECF2E39  : public Rijndael_t282661761E53E282457A31F5675ACF53D64EE807
{
};

// System.SystemException
struct SystemException_tCC48D868298F4C0705279823E34B00F4FBDB7295  : public Exception_t
{
};

// System.ArgumentException
struct ArgumentException_tAD90411542A20A9C72D5CDA3A84181D8B947A263  : public SystemException_tCC48D868298F4C0705279823E34B00F4FBDB7295
{
	// System.String System.ArgumentException::_paramName
	String_t* ____paramName_18;
};

// System.Security.Cryptography.CryptographicException
struct CryptographicException_tF1AE59E97557B5241330D5998C4D95B6D9D6669F  : public SystemException_tCC48D868298F4C0705279823E34B00F4FBDB7295
{
};

// System.InvalidOperationException
struct InvalidOperationException_t5DDE4D49B7405FAAB1E4576F4715A42A3FAD4BAB  : public SystemException_tCC48D868298F4C0705279823E34B00F4FBDB7295
{
};

// System.ArgumentNullException
struct ArgumentNullException_t327031E412FAB2351B0022DD5DAD47E67E597129  : public ArgumentException_tAD90411542A20A9C72D5CDA3A84181D8B947A263
{
};
#ifdef __clang__
#pragma clang diagnostic pop
#endif
// System.Byte[]
struct ByteU5BU5D_tA6237BF417AE52AD70CFB4EF24A7A82613DF9031  : public RuntimeArray
{
	ALIGN_FIELD (8) uint8_t m_Items[1];

	inline uint8_t GetAt(il2cpp_array_size_t index) const
	{
		IL2CPP_ARRAY_BOUNDS_CHECK(index, (uint32_t)(this)->max_length);
		return m_Items[index];
	}
	inline uint8_t* GetAddressAt(il2cpp_array_size_t index)
	{
		IL2CPP_ARRAY_BOUNDS_CHECK(index, (uint32_t)(this)->max_length);
		return m_Items + index;
	}
	inline void SetAt(il2cpp_array_size_t index, uint8_t value)
	{
		IL2CPP_ARRAY_BOUNDS_CHECK(index, (uint32_t)(this)->max_length);
		m_Items[index] = value;
	}
	inline uint8_t GetAtUnchecked(il2cpp_array_size_t index) const
	{
		return m_Items[index];
	}
	inline uint8_t* GetAddressAtUnchecked(il2cpp_array_size_t index)
	{
		return m_Items + index;
	}
	inline void SetAtUnchecked(il2cpp_array_size_t index, uint8_t value)
	{
		m_Items[index] = value;
	}
};



// System.Void System.Security.Cryptography.Aes::.ctor()
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void Aes__ctor_mF4A70CEEFAF2A086CF7A82D915BFFB7E250528EA (Aes_tC72E711D7751C8AEAF59C51CA0E61A3857068047* __this, const RuntimeMethod* method) ;
// System.Boolean System.Security.Cryptography.CryptoConfig::get_AllowOnlyFipsAlgorithms()
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR bool CryptoConfig_get_AllowOnlyFipsAlgorithms_m5DC7A399B02DFC3631126E05816EA954F1CE3B5A (const RuntimeMethod* method) ;
// System.String SR::GetString(System.String)
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR String_t* SR_GetString_m237CD17C5AC69337B2D257D2EDFA83DDF1E2E1D0 (String_t* ___name0, const RuntimeMethod* method) ;
// System.Void System.InvalidOperationException::.ctor(System.String)
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void InvalidOperationException__ctor_mE4CB6F4712AB6D99A2358FBAE2E052B3EE976162 (InvalidOperationException_t5DDE4D49B7405FAAB1E4576F4715A42A3FAD4BAB* __this, String_t* ___message0, const RuntimeMethod* method) ;
// System.Void System.Security.Cryptography.RijndaelManaged::.ctor()
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void RijndaelManaged__ctor_mACBA07A36FCD3FE3858C4B1FBBC57D7CBB7109C7 (RijndaelManaged_t67DFB0E9542F21D9EED0227003A19D9F5ECF2E39* __this, const RuntimeMethod* method) ;
// System.Void System.Security.Cryptography.CryptographicException::.ctor(System.String)
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void CryptographicException__ctor_mE3E5B505DB6A9D573C48AC5BD702E7CF3DE8F01F (CryptographicException_tF1AE59E97557B5241330D5998C4D95B6D9D6669F* __this, String_t* ___message0, const RuntimeMethod* method) ;
// System.Void System.ArgumentNullException::.ctor(System.String)
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void ArgumentNullException__ctor_m444AE141157E333844FC1A9500224C2F9FD24F4B (ArgumentNullException_t327031E412FAB2351B0022DD5DAD47E67E597129* __this, String_t* ___paramName0, const RuntimeMethod* method) ;
// System.Boolean System.Security.Cryptography.SymmetricAlgorithm::ValidKeySize(System.Int32)
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR bool SymmetricAlgorithm_ValidKeySize_m2591E5739D56A66AAF534F57D5139C4F707B0D26 (SymmetricAlgorithm_t8C631E4E7B9073CCBD856F8D559A62EB5616BBE8* __this, int32_t ___bitLength0, const RuntimeMethod* method) ;
// System.Void System.ArgumentException::.ctor(System.String,System.String)
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void ArgumentException__ctor_m8F9D40CE19D19B698A70F9A258640EB52DB39B62 (ArgumentException_tAD90411542A20A9C72D5CDA3A84181D8B947A263* __this, String_t* ___message0, String_t* ___paramName1, const RuntimeMethod* method) ;
// System.Void System.Security.Cryptography.SymmetricAlgorithm::Dispose(System.Boolean)
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void SymmetricAlgorithm_Dispose_mAD39DD1B9C484B29882FAE75359114F377958CE3 (SymmetricAlgorithm_t8C631E4E7B9073CCBD856F8D559A62EB5616BBE8* __this, bool ___disposing0, const RuntimeMethod* method) ;
// System.Exception System.Linq.Error::ArgumentNull(System.String)
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR Exception_t* Error_ArgumentNull_m9157523765DB73FC9F7B984F2F740F2B5EDB7337 (String_t* ___s0, const RuntimeMethod* method) ;
// System.Exception System.Linq.Error::NoElements()
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR Exception_t* Error_NoElements_m0E3D3D6CC1E9C9ED28CD989485766C91DC71042F (const RuntimeMethod* method) ;
#ifdef __clang__
#pragma clang diagnostic push
#pragma clang diagnostic ignored "-Winvalid-offsetof"
#pragma clang diagnostic ignored "-Wunused-variable"
#endif
#ifdef __clang__
#pragma clang diagnostic pop
#endif
#ifdef __clang__
#pragma clang diagnostic push
#pragma clang diagnostic ignored "-Winvalid-offsetof"
#pragma clang diagnostic ignored "-Wunused-variable"
#endif
// System.String SR::GetString(System.String)
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR String_t* SR_GetString_m237CD17C5AC69337B2D257D2EDFA83DDF1E2E1D0 (String_t* ___name0, const RuntimeMethod* method) 
{
	{
		String_t* L_0 = ___name0;
		return L_0;
	}
}
#ifdef __clang__
#pragma clang diagnostic pop
#endif
#ifdef __clang__
#pragma clang diagnostic push
#pragma clang diagnostic ignored "-Winvalid-offsetof"
#pragma clang diagnostic ignored "-Wunused-variable"
#endif
// System.Void System.Security.Cryptography.AesManaged::.ctor()
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void AesManaged__ctor_mDE017C6ECB4AF587B6C2A7A4BCBB2E0FA3E81241 (AesManaged_t153AF8C6AF037AF4614B15A04CAD48A7F7DA0819* __this, const RuntimeMethod* method) 
{
	static bool s_Il2CppMethodInitialized;
	if (!s_Il2CppMethodInitialized)
	{
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&Aes_tC72E711D7751C8AEAF59C51CA0E61A3857068047_il2cpp_TypeInfo_var);
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&CryptoConfig_t66E67DD67B2C3224C72C0CBD80A144E29EE016A5_il2cpp_TypeInfo_var);
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&RijndaelManaged_t67DFB0E9542F21D9EED0227003A19D9F5ECF2E39_il2cpp_TypeInfo_var);
		s_Il2CppMethodInitialized = true;
	}
	{
		il2cpp_codegen_runtime_class_init_inline(Aes_tC72E711D7751C8AEAF59C51CA0E61A3857068047_il2cpp_TypeInfo_var);
		Aes__ctor_mF4A70CEEFAF2A086CF7A82D915BFFB7E250528EA(__this, NULL);
		il2cpp_codegen_runtime_class_init_inline(CryptoConfig_t66E67DD67B2C3224C72C0CBD80A144E29EE016A5_il2cpp_TypeInfo_var);
		bool L_0;
		L_0 = CryptoConfig_get_AllowOnlyFipsAlgorithms_m5DC7A399B02DFC3631126E05816EA954F1CE3B5A(NULL);
		if (!L_0)
		{
			goto IL_001d;
		}
	}
	{
		String_t* L_1;
		L_1 = SR_GetString_m237CD17C5AC69337B2D257D2EDFA83DDF1E2E1D0(((String_t*)il2cpp_codegen_initialize_runtime_metadata_inline((uintptr_t*)&_stringLiteral17BA770D472146F14CBDE2CFF7752F076430BCBE)), NULL);
		InvalidOperationException_t5DDE4D49B7405FAAB1E4576F4715A42A3FAD4BAB* L_2 = (InvalidOperationException_t5DDE4D49B7405FAAB1E4576F4715A42A3FAD4BAB*)il2cpp_codegen_object_new(((RuntimeClass*)il2cpp_codegen_initialize_runtime_metadata_inline((uintptr_t*)&InvalidOperationException_t5DDE4D49B7405FAAB1E4576F4715A42A3FAD4BAB_il2cpp_TypeInfo_var)));
		NullCheck(L_2);
		InvalidOperationException__ctor_mE4CB6F4712AB6D99A2358FBAE2E052B3EE976162(L_2, L_1, NULL);
		IL2CPP_RAISE_MANAGED_EXCEPTION(L_2, ((RuntimeMethod*)il2cpp_codegen_initialize_runtime_metadata_inline((uintptr_t*)&AesManaged__ctor_mDE017C6ECB4AF587B6C2A7A4BCBB2E0FA3E81241_RuntimeMethod_var)));
	}

IL_001d:
	{
		RijndaelManaged_t67DFB0E9542F21D9EED0227003A19D9F5ECF2E39* L_3 = (RijndaelManaged_t67DFB0E9542F21D9EED0227003A19D9F5ECF2E39*)il2cpp_codegen_object_new(RijndaelManaged_t67DFB0E9542F21D9EED0227003A19D9F5ECF2E39_il2cpp_TypeInfo_var);
		NullCheck(L_3);
		RijndaelManaged__ctor_mACBA07A36FCD3FE3858C4B1FBBC57D7CBB7109C7(L_3, NULL);
		__this->___m_rijndael_11 = L_3;
		Il2CppCodeGenWriteBarrier((void**)(&__this->___m_rijndael_11), (void*)L_3);
		RijndaelManaged_t67DFB0E9542F21D9EED0227003A19D9F5ECF2E39* L_4 = __this->___m_rijndael_11;
		int32_t L_5;
		L_5 = VirtualFuncInvoker0< int32_t >::Invoke(6 /* System.Int32 System.Security.Cryptography.SymmetricAlgorithm::get_BlockSize() */, __this);
		NullCheck(L_4);
		VirtualActionInvoker1< int32_t >::Invoke(7 /* System.Void System.Security.Cryptography.SymmetricAlgorithm::set_BlockSize(System.Int32) */, L_4, L_5);
		RijndaelManaged_t67DFB0E9542F21D9EED0227003A19D9F5ECF2E39* L_6 = __this->___m_rijndael_11;
		int32_t L_7;
		L_7 = VirtualFuncInvoker0< int32_t >::Invoke(12 /* System.Int32 System.Security.Cryptography.SymmetricAlgorithm::get_KeySize() */, __this);
		NullCheck(L_6);
		VirtualActionInvoker1< int32_t >::Invoke(13 /* System.Void System.Security.Cryptography.SymmetricAlgorithm::set_KeySize(System.Int32) */, L_6, L_7);
		return;
	}
}
// System.Byte[] System.Security.Cryptography.AesManaged::get_IV()
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR ByteU5BU5D_tA6237BF417AE52AD70CFB4EF24A7A82613DF9031* AesManaged_get_IV_m3CF0BDF7CC0EA199B5EE57F2CBD97404842C0D0B (AesManaged_t153AF8C6AF037AF4614B15A04CAD48A7F7DA0819* __this, const RuntimeMethod* method) 
{
	{
		RijndaelManaged_t67DFB0E9542F21D9EED0227003A19D9F5ECF2E39* L_0 = __this->___m_rijndael_11;
		NullCheck(L_0);
		ByteU5BU5D_tA6237BF417AE52AD70CFB4EF24A7A82613DF9031* L_1;
		L_1 = VirtualFuncInvoker0< ByteU5BU5D_tA6237BF417AE52AD70CFB4EF24A7A82613DF9031* >::Invoke(8 /* System.Byte[] System.Security.Cryptography.SymmetricAlgorithm::get_IV() */, L_0);
		return L_1;
	}
}
// System.Byte[] System.Security.Cryptography.AesManaged::get_Key()
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR ByteU5BU5D_tA6237BF417AE52AD70CFB4EF24A7A82613DF9031* AesManaged_get_Key_m2E60F74962BECA399636BDC297DF3AD2386D92DF (AesManaged_t153AF8C6AF037AF4614B15A04CAD48A7F7DA0819* __this, const RuntimeMethod* method) 
{
	{
		RijndaelManaged_t67DFB0E9542F21D9EED0227003A19D9F5ECF2E39* L_0 = __this->___m_rijndael_11;
		NullCheck(L_0);
		ByteU5BU5D_tA6237BF417AE52AD70CFB4EF24A7A82613DF9031* L_1;
		L_1 = VirtualFuncInvoker0< ByteU5BU5D_tA6237BF417AE52AD70CFB4EF24A7A82613DF9031* >::Invoke(9 /* System.Byte[] System.Security.Cryptography.SymmetricAlgorithm::get_Key() */, L_0);
		return L_1;
	}
}
// System.Void System.Security.Cryptography.AesManaged::set_Key(System.Byte[])
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void AesManaged_set_Key_m4FC5EDB90F6003EE726496D1A374AD0AD5B9911D (AesManaged_t153AF8C6AF037AF4614B15A04CAD48A7F7DA0819* __this, ByteU5BU5D_tA6237BF417AE52AD70CFB4EF24A7A82613DF9031* ___value0, const RuntimeMethod* method) 
{
	{
		RijndaelManaged_t67DFB0E9542F21D9EED0227003A19D9F5ECF2E39* L_0 = __this->___m_rijndael_11;
		ByteU5BU5D_tA6237BF417AE52AD70CFB4EF24A7A82613DF9031* L_1 = ___value0;
		NullCheck(L_0);
		VirtualActionInvoker1< ByteU5BU5D_tA6237BF417AE52AD70CFB4EF24A7A82613DF9031* >::Invoke(10 /* System.Void System.Security.Cryptography.SymmetricAlgorithm::set_Key(System.Byte[]) */, L_0, L_1);
		return;
	}
}
// System.Int32 System.Security.Cryptography.AesManaged::get_KeySize()
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR int32_t AesManaged_get_KeySize_mD9766E2E53FB15EA3761BF801D3ED2BC938A667F (AesManaged_t153AF8C6AF037AF4614B15A04CAD48A7F7DA0819* __this, const RuntimeMethod* method) 
{
	{
		RijndaelManaged_t67DFB0E9542F21D9EED0227003A19D9F5ECF2E39* L_0 = __this->___m_rijndael_11;
		NullCheck(L_0);
		int32_t L_1;
		L_1 = VirtualFuncInvoker0< int32_t >::Invoke(12 /* System.Int32 System.Security.Cryptography.SymmetricAlgorithm::get_KeySize() */, L_0);
		return L_1;
	}
}
// System.Void System.Security.Cryptography.AesManaged::set_KeySize(System.Int32)
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void AesManaged_set_KeySize_m2E4BE0748CCF8473E2AFE656DFBB143845FD1BE7 (AesManaged_t153AF8C6AF037AF4614B15A04CAD48A7F7DA0819* __this, int32_t ___value0, const RuntimeMethod* method) 
{
	{
		RijndaelManaged_t67DFB0E9542F21D9EED0227003A19D9F5ECF2E39* L_0 = __this->___m_rijndael_11;
		int32_t L_1 = ___value0;
		NullCheck(L_0);
		VirtualActionInvoker1< int32_t >::Invoke(13 /* System.Void System.Security.Cryptography.SymmetricAlgorithm::set_KeySize(System.Int32) */, L_0, L_1);
		return;
	}
}
// System.Void System.Security.Cryptography.AesManaged::set_Mode(System.Security.Cryptography.CipherMode)
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void AesManaged_set_Mode_m154C8319F26FA466C54C7B2BDA602909EEA258C0 (AesManaged_t153AF8C6AF037AF4614B15A04CAD48A7F7DA0819* __this, int32_t ___value0, const RuntimeMethod* method) 
{
	{
		int32_t L_0 = ___value0;
		if ((((int32_t)L_0) == ((int32_t)4)))
		{
			goto IL_0008;
		}
	}
	{
		int32_t L_1 = ___value0;
		if ((!(((uint32_t)L_1) == ((uint32_t)3))))
		{
			goto IL_0018;
		}
	}

IL_0008:
	{
		String_t* L_2;
		L_2 = SR_GetString_m237CD17C5AC69337B2D257D2EDFA83DDF1E2E1D0(((String_t*)il2cpp_codegen_initialize_runtime_metadata_inline((uintptr_t*)&_stringLiteral55A326D360D02C44975E4A74CD36AA58A27ABDAE)), NULL);
		CryptographicException_tF1AE59E97557B5241330D5998C4D95B6D9D6669F* L_3 = (CryptographicException_tF1AE59E97557B5241330D5998C4D95B6D9D6669F*)il2cpp_codegen_object_new(((RuntimeClass*)il2cpp_codegen_initialize_runtime_metadata_inline((uintptr_t*)&CryptographicException_tF1AE59E97557B5241330D5998C4D95B6D9D6669F_il2cpp_TypeInfo_var)));
		NullCheck(L_3);
		CryptographicException__ctor_mE3E5B505DB6A9D573C48AC5BD702E7CF3DE8F01F(L_3, L_2, NULL);
		IL2CPP_RAISE_MANAGED_EXCEPTION(L_3, ((RuntimeMethod*)il2cpp_codegen_initialize_runtime_metadata_inline((uintptr_t*)&AesManaged_set_Mode_m154C8319F26FA466C54C7B2BDA602909EEA258C0_RuntimeMethod_var)));
	}

IL_0018:
	{
		RijndaelManaged_t67DFB0E9542F21D9EED0227003A19D9F5ECF2E39* L_4 = __this->___m_rijndael_11;
		int32_t L_5 = ___value0;
		NullCheck(L_4);
		VirtualActionInvoker1< int32_t >::Invoke(14 /* System.Void System.Security.Cryptography.SymmetricAlgorithm::set_Mode(System.Security.Cryptography.CipherMode) */, L_4, L_5);
		return;
	}
}
// System.Void System.Security.Cryptography.AesManaged::set_Padding(System.Security.Cryptography.PaddingMode)
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void AesManaged_set_Padding_m9D52DA6A45835F49CC4385AC2767AA3A83C01BE3 (AesManaged_t153AF8C6AF037AF4614B15A04CAD48A7F7DA0819* __this, int32_t ___value0, const RuntimeMethod* method) 
{
	{
		RijndaelManaged_t67DFB0E9542F21D9EED0227003A19D9F5ECF2E39* L_0 = __this->___m_rijndael_11;
		int32_t L_1 = ___value0;
		NullCheck(L_0);
		VirtualActionInvoker1< int32_t >::Invoke(15 /* System.Void System.Security.Cryptography.SymmetricAlgorithm::set_Padding(System.Security.Cryptography.PaddingMode) */, L_0, L_1);
		return;
	}
}
// System.Security.Cryptography.ICryptoTransform System.Security.Cryptography.AesManaged::CreateDecryptor()
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR RuntimeObject* AesManaged_CreateDecryptor_m68C406635252C68B27FBDB1356574A3B5DEE25DB (AesManaged_t153AF8C6AF037AF4614B15A04CAD48A7F7DA0819* __this, const RuntimeMethod* method) 
{
	{
		RijndaelManaged_t67DFB0E9542F21D9EED0227003A19D9F5ECF2E39* L_0 = __this->___m_rijndael_11;
		NullCheck(L_0);
		RuntimeObject* L_1;
		L_1 = VirtualFuncInvoker0< RuntimeObject* >::Invoke(18 /* System.Security.Cryptography.ICryptoTransform System.Security.Cryptography.SymmetricAlgorithm::CreateDecryptor() */, L_0);
		return L_1;
	}
}
// System.Security.Cryptography.ICryptoTransform System.Security.Cryptography.AesManaged::CreateDecryptor(System.Byte[],System.Byte[])
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR RuntimeObject* AesManaged_CreateDecryptor_m3E4110743E227B1B7568B7D497573631E26C3246 (AesManaged_t153AF8C6AF037AF4614B15A04CAD48A7F7DA0819* __this, ByteU5BU5D_tA6237BF417AE52AD70CFB4EF24A7A82613DF9031* ___key0, ByteU5BU5D_tA6237BF417AE52AD70CFB4EF24A7A82613DF9031* ___iv1, const RuntimeMethod* method) 
{
	{
		ByteU5BU5D_tA6237BF417AE52AD70CFB4EF24A7A82613DF9031* L_0 = ___key0;
		if (L_0)
		{
			goto IL_000e;
		}
	}
	{
		ArgumentNullException_t327031E412FAB2351B0022DD5DAD47E67E597129* L_1 = (ArgumentNullException_t327031E412FAB2351B0022DD5DAD47E67E597129*)il2cpp_codegen_object_new(((RuntimeClass*)il2cpp_codegen_initialize_runtime_metadata_inline((uintptr_t*)&ArgumentNullException_t327031E412FAB2351B0022DD5DAD47E67E597129_il2cpp_TypeInfo_var)));
		NullCheck(L_1);
		ArgumentNullException__ctor_m444AE141157E333844FC1A9500224C2F9FD24F4B(L_1, ((String_t*)il2cpp_codegen_initialize_runtime_metadata_inline((uintptr_t*)&_stringLiteralE7D028CCE3B6E7B61AE2C752D7AE970DA04AB7C6)), NULL);
		IL2CPP_RAISE_MANAGED_EXCEPTION(L_1, ((RuntimeMethod*)il2cpp_codegen_initialize_runtime_metadata_inline((uintptr_t*)&AesManaged_CreateDecryptor_m3E4110743E227B1B7568B7D497573631E26C3246_RuntimeMethod_var)));
	}

IL_000e:
	{
		ByteU5BU5D_tA6237BF417AE52AD70CFB4EF24A7A82613DF9031* L_2 = ___key0;
		NullCheck(L_2);
		bool L_3;
		L_3 = SymmetricAlgorithm_ValidKeySize_m2591E5739D56A66AAF534F57D5139C4F707B0D26(__this, ((int32_t)il2cpp_codegen_multiply(((int32_t)(((RuntimeArray*)L_2)->max_length)), 8)), NULL);
		if (L_3)
		{
			goto IL_0030;
		}
	}
	{
		String_t* L_4;
		L_4 = SR_GetString_m237CD17C5AC69337B2D257D2EDFA83DDF1E2E1D0(((String_t*)il2cpp_codegen_initialize_runtime_metadata_inline((uintptr_t*)&_stringLiteral167E6E6EB13F42BCA02A2CAA251B92A9CCA2C7E4)), NULL);
		ArgumentException_tAD90411542A20A9C72D5CDA3A84181D8B947A263* L_5 = (ArgumentException_tAD90411542A20A9C72D5CDA3A84181D8B947A263*)il2cpp_codegen_object_new(((RuntimeClass*)il2cpp_codegen_initialize_runtime_metadata_inline((uintptr_t*)&ArgumentException_tAD90411542A20A9C72D5CDA3A84181D8B947A263_il2cpp_TypeInfo_var)));
		NullCheck(L_5);
		ArgumentException__ctor_m8F9D40CE19D19B698A70F9A258640EB52DB39B62(L_5, L_4, ((String_t*)il2cpp_codegen_initialize_runtime_metadata_inline((uintptr_t*)&_stringLiteralE7D028CCE3B6E7B61AE2C752D7AE970DA04AB7C6)), NULL);
		IL2CPP_RAISE_MANAGED_EXCEPTION(L_5, ((RuntimeMethod*)il2cpp_codegen_initialize_runtime_metadata_inline((uintptr_t*)&AesManaged_CreateDecryptor_m3E4110743E227B1B7568B7D497573631E26C3246_RuntimeMethod_var)));
	}

IL_0030:
	{
		ByteU5BU5D_tA6237BF417AE52AD70CFB4EF24A7A82613DF9031* L_6 = ___iv1;
		if (!L_6)
		{
			goto IL_0055;
		}
	}
	{
		ByteU5BU5D_tA6237BF417AE52AD70CFB4EF24A7A82613DF9031* L_7 = ___iv1;
		NullCheck(L_7);
		int32_t L_8 = ((SymmetricAlgorithm_t8C631E4E7B9073CCBD856F8D559A62EB5616BBE8*)__this)->___BlockSizeValue_0;
		if ((((int32_t)((int32_t)il2cpp_codegen_multiply(((int32_t)(((RuntimeArray*)L_7)->max_length)), 8))) == ((int32_t)L_8)))
		{
			goto IL_0055;
		}
	}
	{
		String_t* L_9;
		L_9 = SR_GetString_m237CD17C5AC69337B2D257D2EDFA83DDF1E2E1D0(((String_t*)il2cpp_codegen_initialize_runtime_metadata_inline((uintptr_t*)&_stringLiteralF8BD9A7F60F0A925FBA0FA74162E5EC956585C8D)), NULL);
		ArgumentException_tAD90411542A20A9C72D5CDA3A84181D8B947A263* L_10 = (ArgumentException_tAD90411542A20A9C72D5CDA3A84181D8B947A263*)il2cpp_codegen_object_new(((RuntimeClass*)il2cpp_codegen_initialize_runtime_metadata_inline((uintptr_t*)&ArgumentException_tAD90411542A20A9C72D5CDA3A84181D8B947A263_il2cpp_TypeInfo_var)));
		NullCheck(L_10);
		ArgumentException__ctor_m8F9D40CE19D19B698A70F9A258640EB52DB39B62(L_10, L_9, ((String_t*)il2cpp_codegen_initialize_runtime_metadata_inline((uintptr_t*)&_stringLiteralCBCF5ABE35600EAFD21FA4DC1F78EBBBCAD041EB)), NULL);
		IL2CPP_RAISE_MANAGED_EXCEPTION(L_10, ((RuntimeMethod*)il2cpp_codegen_initialize_runtime_metadata_inline((uintptr_t*)&AesManaged_CreateDecryptor_m3E4110743E227B1B7568B7D497573631E26C3246_RuntimeMethod_var)));
	}

IL_0055:
	{
		RijndaelManaged_t67DFB0E9542F21D9EED0227003A19D9F5ECF2E39* L_11 = __this->___m_rijndael_11;
		ByteU5BU5D_tA6237BF417AE52AD70CFB4EF24A7A82613DF9031* L_12 = ___key0;
		ByteU5BU5D_tA6237BF417AE52AD70CFB4EF24A7A82613DF9031* L_13 = ___iv1;
		NullCheck(L_11);
		RuntimeObject* L_14;
		L_14 = VirtualFuncInvoker2< RuntimeObject*, ByteU5BU5D_tA6237BF417AE52AD70CFB4EF24A7A82613DF9031*, ByteU5BU5D_tA6237BF417AE52AD70CFB4EF24A7A82613DF9031* >::Invoke(19 /* System.Security.Cryptography.ICryptoTransform System.Security.Cryptography.SymmetricAlgorithm::CreateDecryptor(System.Byte[],System.Byte[]) */, L_11, L_12, L_13);
		return L_14;
	}
}
// System.Security.Cryptography.ICryptoTransform System.Security.Cryptography.AesManaged::CreateEncryptor()
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR RuntimeObject* AesManaged_CreateEncryptor_mBC3DC59E0DF5DC791B45806DE319EAF3DDB0E646 (AesManaged_t153AF8C6AF037AF4614B15A04CAD48A7F7DA0819* __this, const RuntimeMethod* method) 
{
	{
		RijndaelManaged_t67DFB0E9542F21D9EED0227003A19D9F5ECF2E39* L_0 = __this->___m_rijndael_11;
		NullCheck(L_0);
		RuntimeObject* L_1;
		L_1 = VirtualFuncInvoker0< RuntimeObject* >::Invoke(16 /* System.Security.Cryptography.ICryptoTransform System.Security.Cryptography.SymmetricAlgorithm::CreateEncryptor() */, L_0);
		return L_1;
	}
}
// System.Security.Cryptography.ICryptoTransform System.Security.Cryptography.AesManaged::CreateEncryptor(System.Byte[],System.Byte[])
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR RuntimeObject* AesManaged_CreateEncryptor_mB200E46332CFFD19367E042932042F4A1DFFD5AF (AesManaged_t153AF8C6AF037AF4614B15A04CAD48A7F7DA0819* __this, ByteU5BU5D_tA6237BF417AE52AD70CFB4EF24A7A82613DF9031* ___key0, ByteU5BU5D_tA6237BF417AE52AD70CFB4EF24A7A82613DF9031* ___iv1, const RuntimeMethod* method) 
{
	{
		ByteU5BU5D_tA6237BF417AE52AD70CFB4EF24A7A82613DF9031* L_0 = ___key0;
		if (L_0)
		{
			goto IL_000e;
		}
	}
	{
		ArgumentNullException_t327031E412FAB2351B0022DD5DAD47E67E597129* L_1 = (ArgumentNullException_t327031E412FAB2351B0022DD5DAD47E67E597129*)il2cpp_codegen_object_new(((RuntimeClass*)il2cpp_codegen_initialize_runtime_metadata_inline((uintptr_t*)&ArgumentNullException_t327031E412FAB2351B0022DD5DAD47E67E597129_il2cpp_TypeInfo_var)));
		NullCheck(L_1);
		ArgumentNullException__ctor_m444AE141157E333844FC1A9500224C2F9FD24F4B(L_1, ((String_t*)il2cpp_codegen_initialize_runtime_metadata_inline((uintptr_t*)&_stringLiteralE7D028CCE3B6E7B61AE2C752D7AE970DA04AB7C6)), NULL);
		IL2CPP_RAISE_MANAGED_EXCEPTION(L_1, ((RuntimeMethod*)il2cpp_codegen_initialize_runtime_metadata_inline((uintptr_t*)&AesManaged_CreateEncryptor_mB200E46332CFFD19367E042932042F4A1DFFD5AF_RuntimeMethod_var)));
	}

IL_000e:
	{
		ByteU5BU5D_tA6237BF417AE52AD70CFB4EF24A7A82613DF9031* L_2 = ___key0;
		NullCheck(L_2);
		bool L_3;
		L_3 = SymmetricAlgorithm_ValidKeySize_m2591E5739D56A66AAF534F57D5139C4F707B0D26(__this, ((int32_t)il2cpp_codegen_multiply(((int32_t)(((RuntimeArray*)L_2)->max_length)), 8)), NULL);
		if (L_3)
		{
			goto IL_0030;
		}
	}
	{
		String_t* L_4;
		L_4 = SR_GetString_m237CD17C5AC69337B2D257D2EDFA83DDF1E2E1D0(((String_t*)il2cpp_codegen_initialize_runtime_metadata_inline((uintptr_t*)&_stringLiteral167E6E6EB13F42BCA02A2CAA251B92A9CCA2C7E4)), NULL);
		ArgumentException_tAD90411542A20A9C72D5CDA3A84181D8B947A263* L_5 = (ArgumentException_tAD90411542A20A9C72D5CDA3A84181D8B947A263*)il2cpp_codegen_object_new(((RuntimeClass*)il2cpp_codegen_initialize_runtime_metadata_inline((uintptr_t*)&ArgumentException_tAD90411542A20A9C72D5CDA3A84181D8B947A263_il2cpp_TypeInfo_var)));
		NullCheck(L_5);
		ArgumentException__ctor_m8F9D40CE19D19B698A70F9A258640EB52DB39B62(L_5, L_4, ((String_t*)il2cpp_codegen_initialize_runtime_metadata_inline((uintptr_t*)&_stringLiteralE7D028CCE3B6E7B61AE2C752D7AE970DA04AB7C6)), NULL);
		IL2CPP_RAISE_MANAGED_EXCEPTION(L_5, ((RuntimeMethod*)il2cpp_codegen_initialize_runtime_metadata_inline((uintptr_t*)&AesManaged_CreateEncryptor_mB200E46332CFFD19367E042932042F4A1DFFD5AF_RuntimeMethod_var)));
	}

IL_0030:
	{
		ByteU5BU5D_tA6237BF417AE52AD70CFB4EF24A7A82613DF9031* L_6 = ___iv1;
		if (!L_6)
		{
			goto IL_0055;
		}
	}
	{
		ByteU5BU5D_tA6237BF417AE52AD70CFB4EF24A7A82613DF9031* L_7 = ___iv1;
		NullCheck(L_7);
		int32_t L_8 = ((SymmetricAlgorithm_t8C631E4E7B9073CCBD856F8D559A62EB5616BBE8*)__this)->___BlockSizeValue_0;
		if ((((int32_t)((int32_t)il2cpp_codegen_multiply(((int32_t)(((RuntimeArray*)L_7)->max_length)), 8))) == ((int32_t)L_8)))
		{
			goto IL_0055;
		}
	}
	{
		String_t* L_9;
		L_9 = SR_GetString_m237CD17C5AC69337B2D257D2EDFA83DDF1E2E1D0(((String_t*)il2cpp_codegen_initialize_runtime_metadata_inline((uintptr_t*)&_stringLiteralF8BD9A7F60F0A925FBA0FA74162E5EC956585C8D)), NULL);
		ArgumentException_tAD90411542A20A9C72D5CDA3A84181D8B947A263* L_10 = (ArgumentException_tAD90411542A20A9C72D5CDA3A84181D8B947A263*)il2cpp_codegen_object_new(((RuntimeClass*)il2cpp_codegen_initialize_runtime_metadata_inline((uintptr_t*)&ArgumentException_tAD90411542A20A9C72D5CDA3A84181D8B947A263_il2cpp_TypeInfo_var)));
		NullCheck(L_10);
		ArgumentException__ctor_m8F9D40CE19D19B698A70F9A258640EB52DB39B62(L_10, L_9, ((String_t*)il2cpp_codegen_initialize_runtime_metadata_inline((uintptr_t*)&_stringLiteralCBCF5ABE35600EAFD21FA4DC1F78EBBBCAD041EB)), NULL);
		IL2CPP_RAISE_MANAGED_EXCEPTION(L_10, ((RuntimeMethod*)il2cpp_codegen_initialize_runtime_metadata_inline((uintptr_t*)&AesManaged_CreateEncryptor_mB200E46332CFFD19367E042932042F4A1DFFD5AF_RuntimeMethod_var)));
	}

IL_0055:
	{
		RijndaelManaged_t67DFB0E9542F21D9EED0227003A19D9F5ECF2E39* L_11 = __this->___m_rijndael_11;
		ByteU5BU5D_tA6237BF417AE52AD70CFB4EF24A7A82613DF9031* L_12 = ___key0;
		ByteU5BU5D_tA6237BF417AE52AD70CFB4EF24A7A82613DF9031* L_13 = ___iv1;
		NullCheck(L_11);
		RuntimeObject* L_14;
		L_14 = VirtualFuncInvoker2< RuntimeObject*, ByteU5BU5D_tA6237BF417AE52AD70CFB4EF24A7A82613DF9031*, ByteU5BU5D_tA6237BF417AE52AD70CFB4EF24A7A82613DF9031* >::Invoke(17 /* System.Security.Cryptography.ICryptoTransform System.Security.Cryptography.SymmetricAlgorithm::CreateEncryptor(System.Byte[],System.Byte[]) */, L_11, L_12, L_13);
		return L_14;
	}
}
// System.Void System.Security.Cryptography.AesManaged::Dispose(System.Boolean)
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void AesManaged_Dispose_m2C247F5F1B5CCB32C545F4DEE427295C801D7619 (AesManaged_t153AF8C6AF037AF4614B15A04CAD48A7F7DA0819* __this, bool ___disposing0, const RuntimeMethod* method) 
{
	static bool s_Il2CppMethodInitialized;
	if (!s_Il2CppMethodInitialized)
	{
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&IDisposable_t030E0496B4E0E4E4F086825007979AF51F7248C5_il2cpp_TypeInfo_var);
		s_Il2CppMethodInitialized = true;
	}
	{
		auto __finallyBlock = il2cpp::utils::Finally([&]
		{

FINALLY_0010:
			{// begin finally (depth: 1)
				bool L_0 = ___disposing0;
				SymmetricAlgorithm_Dispose_mAD39DD1B9C484B29882FAE75359114F377958CE3(__this, L_0, NULL);
				return;
			}// end finally (depth: 1)
		});
		try
		{// begin try (depth: 1)
			{
				bool L_1 = ___disposing0;
				if (!L_1)
				{
					goto IL_000e_1;
				}
			}
			{
				RijndaelManaged_t67DFB0E9542F21D9EED0227003A19D9F5ECF2E39* L_2 = __this->___m_rijndael_11;
				NullCheck(L_2);
				InterfaceActionInvoker0::Invoke(0 /* System.Void System.IDisposable::Dispose() */, IDisposable_t030E0496B4E0E4E4F086825007979AF51F7248C5_il2cpp_TypeInfo_var, L_2);
			}

IL_000e_1:
			{
				goto IL_0018;
			}
		}// end try (depth: 1)
		catch(Il2CppExceptionWrapper& e)
		{
			__finallyBlock.StoreException(e.ex);
		}
	}

IL_0018:
	{
		return;
	}
}
// System.Void System.Security.Cryptography.AesManaged::GenerateIV()
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void AesManaged_GenerateIV_m3B6E3EEFAA38CEF038DA0D5FF64B1B120343EBF4 (AesManaged_t153AF8C6AF037AF4614B15A04CAD48A7F7DA0819* __this, const RuntimeMethod* method) 
{
	{
		RijndaelManaged_t67DFB0E9542F21D9EED0227003A19D9F5ECF2E39* L_0 = __this->___m_rijndael_11;
		NullCheck(L_0);
		VirtualActionInvoker0::Invoke(21 /* System.Void System.Security.Cryptography.SymmetricAlgorithm::GenerateIV() */, L_0);
		return;
	}
}
// System.Void System.Security.Cryptography.AesManaged::GenerateKey()
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void AesManaged_GenerateKey_m6AC884BFD7DEA4A22DAD79479E69DF34BED96B33 (AesManaged_t153AF8C6AF037AF4614B15A04CAD48A7F7DA0819* __this, const RuntimeMethod* method) 
{
	{
		RijndaelManaged_t67DFB0E9542F21D9EED0227003A19D9F5ECF2E39* L_0 = __this->___m_rijndael_11;
		NullCheck(L_0);
		VirtualActionInvoker0::Invoke(20 /* System.Void System.Security.Cryptography.SymmetricAlgorithm::GenerateKey() */, L_0);
		return;
	}
}
#ifdef __clang__
#pragma clang diagnostic pop
#endif
#ifdef __clang__
#pragma clang diagnostic push
#pragma clang diagnostic ignored "-Winvalid-offsetof"
#pragma clang diagnostic ignored "-Wunused-variable"
#endif
// System.Exception System.Linq.Error::ArgumentNull(System.String)
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR Exception_t* Error_ArgumentNull_m9157523765DB73FC9F7B984F2F740F2B5EDB7337 (String_t* ___s0, const RuntimeMethod* method) 
{
	static bool s_Il2CppMethodInitialized;
	if (!s_Il2CppMethodInitialized)
	{
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&ArgumentNullException_t327031E412FAB2351B0022DD5DAD47E67E597129_il2cpp_TypeInfo_var);
		s_Il2CppMethodInitialized = true;
	}
	{
		String_t* L_0 = ___s0;
		ArgumentNullException_t327031E412FAB2351B0022DD5DAD47E67E597129* L_1 = (ArgumentNullException_t327031E412FAB2351B0022DD5DAD47E67E597129*)il2cpp_codegen_object_new(ArgumentNullException_t327031E412FAB2351B0022DD5DAD47E67E597129_il2cpp_TypeInfo_var);
		NullCheck(L_1);
		ArgumentNullException__ctor_m444AE141157E333844FC1A9500224C2F9FD24F4B(L_1, L_0, NULL);
		return L_1;
	}
}
// System.Exception System.Linq.Error::MoreThanOneMatch()
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR Exception_t* Error_MoreThanOneMatch_mADF388C1E5EACA4BA8E0CDAAA0834C595544BFAF (const RuntimeMethod* method) 
{
	static bool s_Il2CppMethodInitialized;
	if (!s_Il2CppMethodInitialized)
	{
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&InvalidOperationException_t5DDE4D49B7405FAAB1E4576F4715A42A3FAD4BAB_il2cpp_TypeInfo_var);
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&_stringLiteral9D0E978C2541B8A36DFB07E397656689CE9E713F);
		s_Il2CppMethodInitialized = true;
	}
	{
		InvalidOperationException_t5DDE4D49B7405FAAB1E4576F4715A42A3FAD4BAB* L_0 = (InvalidOperationException_t5DDE4D49B7405FAAB1E4576F4715A42A3FAD4BAB*)il2cpp_codegen_object_new(InvalidOperationException_t5DDE4D49B7405FAAB1E4576F4715A42A3FAD4BAB_il2cpp_TypeInfo_var);
		NullCheck(L_0);
		InvalidOperationException__ctor_mE4CB6F4712AB6D99A2358FBAE2E052B3EE976162(L_0, _stringLiteral9D0E978C2541B8A36DFB07E397656689CE9E713F, NULL);
		return L_0;
	}
}
// System.Exception System.Linq.Error::NoElements()
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR Exception_t* Error_NoElements_m0E3D3D6CC1E9C9ED28CD989485766C91DC71042F (const RuntimeMethod* method) 
{
	static bool s_Il2CppMethodInitialized;
	if (!s_Il2CppMethodInitialized)
	{
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&InvalidOperationException_t5DDE4D49B7405FAAB1E4576F4715A42A3FAD4BAB_il2cpp_TypeInfo_var);
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&_stringLiteralB7E78BE66617B9AE36B6A6E170E3545EE25C1D11);
		s_Il2CppMethodInitialized = true;
	}
	{
		InvalidOperationException_t5DDE4D49B7405FAAB1E4576F4715A42A3FAD4BAB* L_0 = (InvalidOperationException_t5DDE4D49B7405FAAB1E4576F4715A42A3FAD4BAB*)il2cpp_codegen_object_new(InvalidOperationException_t5DDE4D49B7405FAAB1E4576F4715A42A3FAD4BAB_il2cpp_TypeInfo_var);
		NullCheck(L_0);
		InvalidOperationException__ctor_mE4CB6F4712AB6D99A2358FBAE2E052B3EE976162(L_0, _stringLiteralB7E78BE66617B9AE36B6A6E170E3545EE25C1D11, NULL);
		return L_0;
	}
}
#ifdef __clang__
#pragma clang diagnostic pop
#endif
#ifdef __clang__
#pragma clang diagnostic push
#pragma clang diagnostic ignored "-Winvalid-offsetof"
#pragma clang diagnostic ignored "-Wunused-variable"
#endif
// System.Int32 System.Linq.Enumerable::Min(System.Collections.Generic.IEnumerable`1<System.Int32>)
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR int32_t Enumerable_Min_m08CFCF12550397A30F094F24E2F68AD21E98EC9F (RuntimeObject* ___source0, const RuntimeMethod* method) 
{
	static bool s_Il2CppMethodInitialized;
	if (!s_Il2CppMethodInitialized)
	{
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&IDisposable_t030E0496B4E0E4E4F086825007979AF51F7248C5_il2cpp_TypeInfo_var);
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&IEnumerable_1_tCE758D940790D6D0D56B457E522C195F8C413AF2_il2cpp_TypeInfo_var);
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&IEnumerator_1_tD6A90A7446DA8E6CF865EDFBBF18C1200BB6D452_il2cpp_TypeInfo_var);
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&IEnumerator_t7B609C2FFA6EB5167D9C62A0C32A21DE2F666DAA_il2cpp_TypeInfo_var);
		s_Il2CppMethodInitialized = true;
	}
	int32_t V_0 = 0;
	bool V_1 = false;
	RuntimeObject* V_2 = NULL;
	int32_t V_3 = 0;
	{
		RuntimeObject* L_0 = ___source0;
		if (L_0)
		{
			goto IL_000e;
		}
	}
	{
		Exception_t* L_1;
		L_1 = Error_ArgumentNull_m9157523765DB73FC9F7B984F2F740F2B5EDB7337(((String_t*)il2cpp_codegen_initialize_runtime_metadata_inline((uintptr_t*)&_stringLiteral66F9618FDA792CAB23AF2D7FFB50AB2D3E393DC5)), NULL);
		IL2CPP_RAISE_MANAGED_EXCEPTION(L_1, ((RuntimeMethod*)il2cpp_codegen_initialize_runtime_metadata_inline((uintptr_t*)&Enumerable_Min_m08CFCF12550397A30F094F24E2F68AD21E98EC9F_RuntimeMethod_var)));
	}

IL_000e:
	{
		V_0 = 0;
		V_1 = (bool)0;
		RuntimeObject* L_2 = ___source0;
		NullCheck(L_2);
		RuntimeObject* L_3;
		L_3 = InterfaceFuncInvoker0< RuntimeObject* >::Invoke(0 /* System.Collections.Generic.IEnumerator`1<T> System.Collections.Generic.IEnumerable`1<System.Int32>::GetEnumerator() */, IEnumerable_1_tCE758D940790D6D0D56B457E522C195F8C413AF2_il2cpp_TypeInfo_var, L_2);
		V_2 = L_3;
	}
	{
		auto __finallyBlock = il2cpp::utils::Finally([&]
		{

FINALLY_003b:
			{// begin finally (depth: 1)
				{
					RuntimeObject* L_4 = V_2;
					if (!L_4)
					{
						goto IL_0044;
					}
				}
				{
					RuntimeObject* L_5 = V_2;
					NullCheck(L_5);
					InterfaceActionInvoker0::Invoke(0 /* System.Void System.IDisposable::Dispose() */, IDisposable_t030E0496B4E0E4E4F086825007979AF51F7248C5_il2cpp_TypeInfo_var, L_5);
				}

IL_0044:
				{
					return;
				}
			}// end finally (depth: 1)
		});
		try
		{// begin try (depth: 1)
			{
				goto IL_0031_1;
			}

IL_001b_1:
			{
				RuntimeObject* L_6 = V_2;
				NullCheck(L_6);
				int32_t L_7;
				L_7 = InterfaceFuncInvoker0< int32_t >::Invoke(0 /* T System.Collections.Generic.IEnumerator`1<System.Int32>::get_Current() */, IEnumerator_1_tD6A90A7446DA8E6CF865EDFBBF18C1200BB6D452_il2cpp_TypeInfo_var, L_6);
				V_3 = L_7;
				bool L_8 = V_1;
				if (!L_8)
				{
					goto IL_002d_1;
				}
			}
			{
				int32_t L_9 = V_3;
				int32_t L_10 = V_0;
				if ((((int32_t)L_9) >= ((int32_t)L_10)))
				{
					goto IL_0031_1;
				}
			}
			{
				int32_t L_11 = V_3;
				V_0 = L_11;
				goto IL_0031_1;
			}

IL_002d_1:
			{
				int32_t L_12 = V_3;
				V_0 = L_12;
				V_1 = (bool)1;
			}

IL_0031_1:
			{
				RuntimeObject* L_13 = V_2;
				NullCheck(L_13);
				bool L_14;
				L_14 = InterfaceFuncInvoker0< bool >::Invoke(0 /* System.Boolean System.Collections.IEnumerator::MoveNext() */, IEnumerator_t7B609C2FFA6EB5167D9C62A0C32A21DE2F666DAA_il2cpp_TypeInfo_var, L_13);
				if (L_14)
				{
					goto IL_001b_1;
				}
			}
			{
				goto IL_0045;
			}
		}// end try (depth: 1)
		catch(Il2CppExceptionWrapper& e)
		{
			__finallyBlock.StoreException(e.ex);
		}
	}

IL_0045:
	{
		bool L_15 = V_1;
		if (!L_15)
		{
			goto IL_004a;
		}
	}
	{
		int32_t L_16 = V_0;
		return L_16;
	}

IL_004a:
	{
		Exception_t* L_17;
		L_17 = Error_NoElements_m0E3D3D6CC1E9C9ED28CD989485766C91DC71042F(NULL);
		IL2CPP_RAISE_MANAGED_EXCEPTION(L_17, ((RuntimeMethod*)il2cpp_codegen_initialize_runtime_metadata_inline((uintptr_t*)&Enumerable_Min_m08CFCF12550397A30F094F24E2F68AD21E98EC9F_RuntimeMethod_var)));
	}
}
// System.Int32 System.Linq.Enumerable::Max(System.Collections.Generic.IEnumerable`1<System.Int32>)
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR int32_t Enumerable_Max_m30DE6D3273F1E770CC99EC43653F8F4BA4A94760 (RuntimeObject* ___source0, const RuntimeMethod* method) 
{
	static bool s_Il2CppMethodInitialized;
	if (!s_Il2CppMethodInitialized)
	{
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&IDisposable_t030E0496B4E0E4E4F086825007979AF51F7248C5_il2cpp_TypeInfo_var);
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&IEnumerable_1_tCE758D940790D6D0D56B457E522C195F8C413AF2_il2cpp_TypeInfo_var);
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&IEnumerator_1_tD6A90A7446DA8E6CF865EDFBBF18C1200BB6D452_il2cpp_TypeInfo_var);
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&IEnumerator_t7B609C2FFA6EB5167D9C62A0C32A21DE2F666DAA_il2cpp_TypeInfo_var);
		s_Il2CppMethodInitialized = true;
	}
	int32_t V_0 = 0;
	bool V_1 = false;
	RuntimeObject* V_2 = NULL;
	int32_t V_3 = 0;
	{
		RuntimeObject* L_0 = ___source0;
		if (L_0)
		{
			goto IL_000e;
		}
	}
	{
		Exception_t* L_1;
		L_1 = Error_ArgumentNull_m9157523765DB73FC9F7B984F2F740F2B5EDB7337(((String_t*)il2cpp_codegen_initialize_runtime_metadata_inline((uintptr_t*)&_stringLiteral66F9618FDA792CAB23AF2D7FFB50AB2D3E393DC5)), NULL);
		IL2CPP_RAISE_MANAGED_EXCEPTION(L_1, ((RuntimeMethod*)il2cpp_codegen_initialize_runtime_metadata_inline((uintptr_t*)&Enumerable_Max_m30DE6D3273F1E770CC99EC43653F8F4BA4A94760_RuntimeMethod_var)));
	}

IL_000e:
	{
		V_0 = 0;
		V_1 = (bool)0;
		RuntimeObject* L_2 = ___source0;
		NullCheck(L_2);
		RuntimeObject* L_3;
		L_3 = InterfaceFuncInvoker0< RuntimeObject* >::Invoke(0 /* System.Collections.Generic.IEnumerator`1<T> System.Collections.Generic.IEnumerable`1<System.Int32>::GetEnumerator() */, IEnumerable_1_tCE758D940790D6D0D56B457E522C195F8C413AF2_il2cpp_TypeInfo_var, L_2);
		V_2 = L_3;
	}
	{
		auto __finallyBlock = il2cpp::utils::Finally([&]
		{

FINALLY_003b:
			{// begin finally (depth: 1)
				{
					RuntimeObject* L_4 = V_2;
					if (!L_4)
					{
						goto IL_0044;
					}
				}
				{
					RuntimeObject* L_5 = V_2;
					NullCheck(L_5);
					InterfaceActionInvoker0::Invoke(0 /* System.Void System.IDisposable::Dispose() */, IDisposable_t030E0496B4E0E4E4F086825007979AF51F7248C5_il2cpp_TypeInfo_var, L_5);
				}

IL_0044:
				{
					return;
				}
			}// end finally (depth: 1)
		});
		try
		{// begin try (depth: 1)
			{
				goto IL_0031_1;
			}

IL_001b_1:
			{
				RuntimeObject* L_6 = V_2;
				NullCheck(L_6);
				int32_t L_7;
				L_7 = InterfaceFuncInvoker0< int32_t >::Invoke(0 /* T System.Collections.Generic.IEnumerator`1<System.Int32>::get_Current() */, IEnumerator_1_tD6A90A7446DA8E6CF865EDFBBF18C1200BB6D452_il2cpp_TypeInfo_var, L_6);
				V_3 = L_7;
				bool L_8 = V_1;
				if (!L_8)
				{
					goto IL_002d_1;
				}
			}
			{
				int32_t L_9 = V_3;
				int32_t L_10 = V_0;
				if ((((int32_t)L_9) <= ((int32_t)L_10)))
				{
					goto IL_0031_1;
				}
			}
			{
				int32_t L_11 = V_3;
				V_0 = L_11;
				goto IL_0031_1;
			}

IL_002d_1:
			{
				int32_t L_12 = V_3;
				V_0 = L_12;
				V_1 = (bool)1;
			}

IL_0031_1:
			{
				RuntimeObject* L_13 = V_2;
				NullCheck(L_13);
				bool L_14;
				L_14 = InterfaceFuncInvoker0< bool >::Invoke(0 /* System.Boolean System.Collections.IEnumerator::MoveNext() */, IEnumerator_t7B609C2FFA6EB5167D9C62A0C32A21DE2F666DAA_il2cpp_TypeInfo_var, L_13);
				if (L_14)
				{
					goto IL_001b_1;
				}
			}
			{
				goto IL_0045;
			}
		}// end try (depth: 1)
		catch(Il2CppExceptionWrapper& e)
		{
			__finallyBlock.StoreException(e.ex);
		}
	}

IL_0045:
	{
		bool L_15 = V_1;
		if (!L_15)
		{
			goto IL_004a;
		}
	}
	{
		int32_t L_16 = V_0;
		return L_16;
	}

IL_004a:
	{
		Exception_t* L_17;
		L_17 = Error_NoElements_m0E3D3D6CC1E9C9ED28CD989485766C91DC71042F(NULL);
		IL2CPP_RAISE_MANAGED_EXCEPTION(L_17, ((RuntimeMethod*)il2cpp_codegen_initialize_runtime_metadata_inline((uintptr_t*)&Enumerable_Max_m30DE6D3273F1E770CC99EC43653F8F4BA4A94760_RuntimeMethod_var)));
	}
}
#ifdef __clang__
#pragma clang diagnostic pop
#endif
