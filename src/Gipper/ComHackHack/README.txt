This attempts to fix a bug in
IVsDropdownBarClient.GetTextEntry

It has the wrong signature in the VS extension DLLs, to we have to generate our own.

tlbimp "c:\Program Files (x86)\Microsoft Visual Studio 11.0\VSSDK\VisualStudioIntegration\Common\Inc\textmgr.tlb"
and ignore the dozens of warnings (including one about GetTextEntry).

TlbImp : warning TI3015 : At least one of the arguments for 'TextManagerInternal.IVsDropdownBarClient.GetEntryText' cann
ot be marshaled by the runtime marshaler.  Such arguments will therefore be passed as a pointer and may require unsafe c
ode to manipulate.

http://stackoverflow.com/questions/28458715/ivsdropdownbarclient-and-getentrytext-problems-with-text-buffers
