"use strict";
var __extends = (this && this.__extends) || function (d, b) {
    for (var p in b) if (b.hasOwnProperty(p)) d[p] = b[p];
    function __() { this.constructor = d; }
    d.prototype = b === null ? Object.create(b) : (__.prototype = b.prototype, new __());
};
var React = require('react');
var HelloWorldComponent = (function (_super) {
    __extends(HelloWorldComponent, _super);
    function HelloWorldComponent() {
        _super.apply(this, arguments);
    }
    HelloWorldComponent.prototype.render = function () {
        var name = this.props.name;
        return (React.createElement("div", null, "Hello World! Good work ", name));
    };
    return HelloWorldComponent;
}(React.Component));
Object.defineProperty(exports, "__esModule", { value: true });
exports.default = HelloWorldComponent;
//# sourceMappingURL=HelloWorld.js.map