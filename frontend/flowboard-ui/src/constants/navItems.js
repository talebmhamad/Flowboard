const getCounts = (obj) => ({
  today: obj?.today ?? 0,
  total: obj?.total ?? 0
});

export const buildNavItems = (summary) => [
  {
    label: "Draft",
    icon: "✉️",
    c1: getCounts(summary?.draft).today,
    c2: getCounts(summary?.draft).total
  },
  {
    label: "Inbox",
    icon: "📩",
    c1: getCounts(summary?.inbox).today,
    c2: getCounts(summary?.inbox).total,
    active: true
  },
  {
    label: "Completed",
    icon: "✔️",
    c1: getCounts(summary?.completed).today,
    c2: getCounts(summary?.completed).total
  },
  {
    label: "Closed",
    icon: "✖️",
    c1: getCounts(summary?.closed).today,
    c2: getCounts(summary?.closed).total
  }
];