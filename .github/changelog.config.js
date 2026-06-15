const fs = require("fs");
const path = require("path");
const mainTemplate = fs.readFileSync(path.resolve(__dirname, "./release-template.hbs"), "utf8");

const types = [
  { type: "feat", section: "✨ 新增功能" },
  { type: "fix", section: "🐛 缺陷修复" },
  { type: "docs", section: "📖 文档更新", hidden: true },
  { type: "style", section: "🎨 格式优化", hidden: true },
  { type: "refactor", section: "♻️ 代码重构" },
  { type: "perf", section: "⚡ 性能优化", hidden: true },
  { type: "test", section: "🧪 测试相关", hidden: true },
  { type: "revert", section: "↩️ 代码回滚" },
  { type: "chore", section: "🛠️ 工程配置", hidden: true },
  { type: "*", section: "🔧 其他变更", hidden: false }
];
module.exports = {
  parserOpts: {
    headerPattern: /^\s*([✨🐛📖🎨♻️⚡🧪↩️🛠️])?\s*(\w+)?[:：]?\s*(.*)$/,
    headerCorrespondence: ["emoji", "type", "subject"]
  },
  writerOpts: {
    mainTemplate,
    groupBy: "type",
    commitGroupsSort: "type",
    commitsSort: ["subject"],
    transform: (commit) => {
      const map = {
        "✨": "feat",
        "🐛": "fix",
        "📖": "docs",
        "🎨": "style",
        "♻️": "refactor",
        "⚡": "perf",
        "🛠️": "chore",
        "↩️": "revert",
        "🧪": "test"
      };

      let type = commit.type;
      if (!type && commit.emoji && map[commit.emoji]) {
        type = map[commit.emoji];
      }
      if (!type) type = "*";

      const typeOpt = types.find(i => i.type === type)
      if (!typeOpt || typeOpt.hidden)
        return null;
      type = typeOpt.section
      return {
        ...commit,
        type,
        shortHash: commit.hash ? commit.hash.substring(0, 7) : ""
      };
    }
  },

};
