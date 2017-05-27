//logs.js
var util = require('../../utils/util.js')
var md5Util = require('../../utils/md5.js')
Page({
  data: {
    logs: []
  },
  onLoad: function () {
    var that = this
    var urlPars = 'http://localhost:63276/api/values/GetLogs'
    urlPars+='?id=1'
    var sign = md5Util.hexMD5('id=1')
    urlPars+='&sign='+sign
    wx.request({
      url: urlPars,
      success: function (res) {
        that.setData({
          logs: res.data
        })
      }
    })

    // this.setData({
    //   logs: (wx.getStorageSync('logs') || []).map(function (log) {
    //     return util.formatTime(new Date(log))
    //   })
    // })
  }
})
