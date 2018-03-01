# lyc.xuming.studio.api [![Build Status](https://travis-ci.org/TooYoungTooSimp/lyc.xuming.studio.api.svg?branch=master)](https://travis-ci.org/TooYoungTooSimp/lyc.xuming.studio.api)

## 首先是一个用于念诗的api
访问[这里](https://lyc.xuming.studio/api/poem)立即随机吟诗。  
[快捷养苟入口](https://lyc.xuming.studio/api/poem/0)

## 还有一个获取各平台Chromium的下载地址的api
访问[这里](https://lyc.xuming.studio/api/ChromiumUrls)。

## 加入获取虾米音乐直链功能
https://lyc.xuming.studio/api/MusicLink/Xiami/{MusicID}/ id得是数字  
支持自定义替换以支持https与可能的320k音质。  
https://lyc.xuming.studio/api/MusicLink/Xiami/{MusicID}/{rule}/  
rule: JSON格式的一个数组，每个元素均为一个长度为二的字符串数组，表示将第0个元素替换成第1个元素。
如 https://lyc.xuming.studio/api/MusicLink/Xiami/{MusicID}/[["5.","6."],["http:","https:"]]  
试图获取通过https传输的320k音乐。