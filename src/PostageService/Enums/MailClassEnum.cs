using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Spring2.Core.PostageService.Enums {
    public enum MailClassEnum {
	Express,
	First,

	LibraryMail,
	MediaMail,
	ParcelPost,
	ParcelSelect,

	Priority,
	CriticalMail,
	StandardMail,
	ExpressMailInternational,
	FirstClassMailInternational,
	PriorityMailInternational,

	Domestic,
	International
    }

    // First/ParcelPost/ParcelSelect
}
